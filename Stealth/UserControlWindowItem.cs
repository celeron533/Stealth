using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Stealth
{
    public partial class UserControlWindowItem : UserControl
    {

        public UserControlWindowItem()
        {
            InitializeComponent();
        }

        public void SetWindowTitle(string title)
        {
            this.WindowTitle.Text = title;
        }

        public void SethWnd(IntPtr hWnd)
        {
            this.hWnd.Text = hWnd.ToString();
        }

        public void SetStatusColor(StatusColorTag tagColor)
        {
            switch (tagColor)
            {
                case StatusColorTag.NORMAL:
                    panelLeft.BackColor = Color.LightGray; break;
                case StatusColorTag.BLOCKED:
                    panelLeft.BackColor = Color.Red; break;
                case StatusColorTag.MATCHED:
                    panelLeft.BackColor = Color.Lime; break;
                case StatusColorTag.MODIFIED:
                    panelLeft.BackColor = Color.Gold; break;
            }
        }

    }

    public enum StatusColorTag
    {
        NORMAL,
        BLOCKED,
        MATCHED,
        MODIFIED
    }
}
