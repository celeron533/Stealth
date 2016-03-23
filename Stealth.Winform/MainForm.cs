using Stealth.Core.Utilities;
using Stealth.Core.WindowInstance;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Stealth.Winform
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            FormInit();
        }

        private void FormInit()
        {
            RefreshWindowList();
            UpdateTransLabel();
        }

        private WindowInstanceService windowInstanceService = new WindowInstanceService();
        private List<WindowInstanceInfoDetail> windowList = new List<WindowInstanceInfoDetail>();
        private List<WindowInstanceInfoDetail> scanWindowList = new List<WindowInstanceInfoDetail>();
        private List<WindowInstanceInfoDetail> filteredWindowList = new List<WindowInstanceInfoDetail>();

        private WindowInstanceInfoDetail selectedWindow = null;

        #region WindowList Section

        private void button_Refresh_Click(object sender, EventArgs e)
        {
            RefreshWindowList();
        }

        //refresh the entire the window info list, the filter will be kept
        private void RefreshWindowList()
        {
            scanWindowList = windowInstanceService.GetWindowInstanceInfoDetailList()
                .Where(c => c.isWindowVisible && !string.IsNullOrEmpty(c.windowTitle)).ToList();

            WindowComparer<WindowInstanceInfoDetail> windowComparer = new WindowComparer<WindowInstanceInfoDetail>();

            //check the removed windows
            windowList.ForEach(c =>
            {
                c.isRemoved = !scanWindowList.Contains(c, windowComparer);
            });

            //add/update scan result to current list
            scanWindowList.ForEach(c =>
            {
                //update
                if (windowList.Contains(c, windowComparer))
                {
                    //update
                    var target = windowList.Where(d => d.hWnd == c.hWnd).FirstOrDefault();
                    bool isModified = target.isModified;
                    target = c;
                    target.isModified = isModified;
                }
                //add
                else
                {
                    windowList.Add(c);
                }
            });

            dataGridView_WindowList.AutoGenerateColumns = false;
            WindowListFilter();
            //dataGridView_WindowList.DataSource = windowInfoList;
        }

        //realtime window filter: by Title name
        private void WindowListFilter()
        {
            filteredWindowList =
                //text filter
                windowList.Where(c => c.windowTitle.ToLower().Contains(textBox_Filter.Text.ToLower()))
                //show modified/removed
                .Where(c =>
                    //show modified only
                    !((modifiedToolStripMenuItem.Checked && !c.isModified)
                        //hide removed
                    || (removedToolStripMenuItem.Checked && c.isRemoved))
                
                    // logic:
                    //{
                    //    //show modified only
                    //    if (modifiedToolStripMenuItem.Checked)
                    //        if (!c.isModified)
                    //            return false;

                    //    //hide removed
                    //    if (removedToolStripMenuItem.Checked)
                    //        if (c.isRemoved)
                    //            return false;
                    //    return true;
                    //}
                )
                .ToList();

            dataGridView_WindowList.DataSource = filteredWindowList;
        }

        //filter
        private void textBox_Filter_TextChanged(object sender, EventArgs e)
        {
            WindowListFilter();
        }

        //when user select a row
        private void dataGridView_WindowList_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //use hwnd to find from windowList
            selectedWindow = windowList.Find(c => c.hWnd == filteredWindowList[e.RowIndex].hWnd);
            textBox_Title.Text = selectedWindow.windowTitle;
            if (selectedWindow.isModified)
            {
                trackBar_Trans.Value = selectedWindow.transparencyProperty.bAlpha;
            }
            else
            {
                trackBar_Trans.Value = trackBar_Trans.Maximum;
            }
        }

        //style
        DataGridViewCellStyle isRemovedStyle = new DataGridViewCellStyle()
        {
            ForeColor = Color.Gray
        };

        DataGridViewCellStyle isModifiedStyle = new DataGridViewCellStyle()
        {
            BackColor = Color.LightYellow
        };

        #endregion

        #region WindowDetail Section
        private void trackBar_Trans_ValueChanged(object sender, EventArgs e)
        {
            UpdateTransLabel();
        }

        private void UpdateTransLabel()
        {
            label_Trans.Text = trackBar_Trans.Value.ToString() + "/255";
        }

        private void button_Reset_Click(object sender, EventArgs e)
        {
            trackBar_Trans.Value = trackBar_Trans.Maximum;
            checkBox_Top.Checked = false;
            SetWindow();
        }

        private void button_Set_Click(object sender, EventArgs e)
        {
            SetWindow();
        }

        private void SetWindow()
        {
            if (selectedWindow == null)
                return;
            selectedWindow.isTopMost = checkBox_Top.Checked;
            selectedWindow.isLayered = true;
            selectedWindow.transparencyProperty.bAlpha = (byte)trackBar_Trans.Value;
            selectedWindow.transparencyProperty.dwFlags = (uint)User32.LWA.LWA_ALPHA;
            selectedWindow.isModified = true;
        }

        #endregion

        #region Menu

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void modifiedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowListFilter();
        }

        private void removedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowListFilter();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog();
        }

        #endregion



    }

    public class WindowComparer<T> : IEqualityComparer<T>
        where T : WindowInstanceInfoDetail
    {
        public bool Equals(T x, T y)
        {
            return x.hWnd == y.hWnd;
        }

        public int GetHashCode(T obj)
        {
            return base.GetHashCode();
        }
    }
}
