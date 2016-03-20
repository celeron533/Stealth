using Stealth.Core.Utilities;
using Stealth.Core.WindowInstance;
using System;
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
        private List<WindowInstanceInfoDetail> windowInfoList = new List<WindowInstanceInfoDetail>();
        private List<WindowInstanceInfoDetail> filteredWindowList = new List<WindowInstanceInfoDetail>();

        private WindowInstanceInfoDetail selectedWindow = null;

        #region WindowList

        private void button_Refresh_Click(object sender, EventArgs e)
        {
            RefreshWindowList();
        }

        //refresh the entire the window info list, the filter will be kept
        private void RefreshWindowList()
        {
            windowInfoList = windowInstanceService.GetWindowInstanceInfoDetailList()
                .Where(c => c.isWindowVisible && !string.IsNullOrEmpty(c.windowTitle)).ToList();
            dataGridView_WindowList.AutoGenerateColumns = false;
            WindowListFilter();
            //dataGridView_WindowList.DataSource = windowInfoList;
        }

        //realtime window filter: by Title name
        private void WindowListFilter()
        {
            filteredWindowList = windowInfoList.Where(c => c.windowTitle.ToLower().Contains(textBox_Filter.Text.ToLower())).ToList();
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
            selectedWindow = filteredWindowList[e.RowIndex];
            textBox_Title.Text = selectedWindow.windowTitle;
        }

        #endregion

        #region WindowDetail
        private void trackBar_Trans_Scroll(object sender, EventArgs e)
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
        }

        #endregion

        #region Menu

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog();
        }

        #endregion

    }
}
