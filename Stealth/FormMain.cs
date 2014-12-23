using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Stealth
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        public List<WindowInstanceInfo> windowInstanceInfoList;
        public Dictionary<long, WindowInstanceDetail> windowInstanceDetailList;

        private void FormMain_Load(object sender, EventArgs e)
        {
            //notifyIconMain.ShowBalloonTip(1000, "Stealth", "Stealth is Running", ToolTipIcon.Info);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshWindowInfoList();
            RefreshWindowInstanceDetailList();
        }

        #region Regenerate the list of WindowInstance Information

        private void RefreshWindowInfoList()
        {
            if (windowInstanceInfoList == null)
            {
                windowInstanceInfoList = new List<WindowInstanceInfo>();
            }
            windowInstanceInfoList.Clear();

            //get all desktop windows handler
            User32.EnumDelegate filter = delegate(IntPtr hWnd, int lParam)
            {
                StringBuilder strbTitle = new StringBuilder(255);
                int nLength = User32.GetWindowText(hWnd, strbTitle, strbTitle.Capacity + 1);
                string strTitle = strbTitle.ToString();

                if (User32.IsWindowVisible(hWnd) && !string.IsNullOrEmpty(strTitle))
                {
                    windowInstanceInfoList.Add(new WindowInstanceInfo() { hWnd = hWnd, WindowTitle = strTitle });
                }
                return true;
            };

            if (User32.EnumDesktopWindows(IntPtr.Zero, filter, IntPtr.Zero))
            {
                foreach (var item in windowInstanceInfoList)
                {
                    Console.WriteLine(item.hWnd + ", " + item.WindowTitle);
                }
            }

        }



        #endregion

        #region

        private void RefreshWindowInstanceDetailList()
        {
            if (windowInstanceDetailList == null)
            {
                windowInstanceDetailList = new Dictionary<long, WindowInstanceDetail>();
            }
            windowInstanceDetailList.Clear();  //todo: the list will audit any updates
            foreach (WindowInstanceInfo windowInfo in windowInstanceInfoList)
            {
                WindowInstanceDetail detail = new WindowInstanceDetail();
                detail.hWnd = windowInfo.hWnd;
                detail.WindowTitle = windowInfo.WindowTitle;
                detail.IsAlive = true;
                detail.IsModified = false;
                uint crKey, dwFlags;
                byte bAlpha=255;
                User32.GetLayeredWindowAttributes(windowInfo.hWnd, out crKey, out bAlpha, out dwFlags);

                //todo: need determine dwFlags
                detail.Opacity = (int)bAlpha;

                windowInstanceDetailList.Add((long)windowInfo.hWnd, detail);
            }
            dataGridViewMain.DataSource = windowInstanceDetailList.Values.ToArray();
        }
        #endregion


    }
}
