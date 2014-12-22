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


        private void FormMain_Load(object sender, EventArgs e)
        {
            notifyIconMain.ShowBalloonTip(1000, "Stealth", "Stealth is Running", ToolTipIcon.Info);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {

        }

        #region

        private void RefreshWindowDic()
        {


        }

        #endregion

    }
}
