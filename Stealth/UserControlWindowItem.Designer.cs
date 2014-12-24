namespace Stealth
{
    partial class UserControlWindowItem
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.panelMiddle = new System.Windows.Forms.Panel();
            this.panelRight = new System.Windows.Forms.Panel();
            this.Thumbnail = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanelMiddle = new System.Windows.Forms.TableLayoutPanel();
            this.WindowTitle = new System.Windows.Forms.Label();
            this.hWnd = new System.Windows.Forms.Label();
            this.tableLayoutPanelMain.SuspendLayout();
            this.panelMiddle.SuspendLayout();
            this.panelRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Thumbnail)).BeginInit();
            this.tableLayoutPanelMiddle.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 3;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanelMain.Controls.Add(this.panelLeft, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.panelMiddle, 1, 0);
            this.tableLayoutPanelMain.Controls.Add(this.panelRight, 2, 0);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 1;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(600, 100);
            this.tableLayoutPanelMain.TabIndex = 1;
            // 
            // panelLeft
            // 
            this.panelLeft.BackColor = System.Drawing.Color.Lime;
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLeft.Location = new System.Drawing.Point(3, 3);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(14, 94);
            this.panelLeft.TabIndex = 0;
            // 
            // panelMiddle
            // 
            this.panelMiddle.AutoScroll = true;
            this.panelMiddle.Controls.Add(this.tableLayoutPanelMiddle);
            this.panelMiddle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMiddle.Location = new System.Drawing.Point(23, 3);
            this.panelMiddle.Name = "panelMiddle";
            this.panelMiddle.Size = new System.Drawing.Size(474, 94);
            this.panelMiddle.TabIndex = 1;
            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this.Thumbnail);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(503, 3);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(94, 94);
            this.panelRight.TabIndex = 2;
            // 
            // Thumbnail
            // 
            this.Thumbnail.BackColor = System.Drawing.Color.LightCyan;
            this.Thumbnail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Thumbnail.InitialImage = null;
            this.Thumbnail.Location = new System.Drawing.Point(0, 0);
            this.Thumbnail.Name = "Thumbnail";
            this.Thumbnail.Size = new System.Drawing.Size(94, 94);
            this.Thumbnail.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Thumbnail.TabIndex = 0;
            this.Thumbnail.TabStop = false;
            // 
            // tableLayoutPanelMiddle
            // 
            this.tableLayoutPanelMiddle.ColumnCount = 1;
            this.tableLayoutPanelMiddle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMiddle.Controls.Add(this.WindowTitle, 0, 0);
            this.tableLayoutPanelMiddle.Controls.Add(this.hWnd, 0, 1);
            this.tableLayoutPanelMiddle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMiddle.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMiddle.Name = "tableLayoutPanelMiddle";
            this.tableLayoutPanelMiddle.RowCount = 3;
            this.tableLayoutPanelMiddle.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMiddle.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMiddle.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMiddle.Size = new System.Drawing.Size(474, 94);
            this.tableLayoutPanelMiddle.TabIndex = 0;
            // 
            // WindowTitle
            // 
            this.WindowTitle.AutoSize = true;
            this.WindowTitle.Location = new System.Drawing.Point(3, 0);
            this.WindowTitle.Name = "WindowTitle";
            this.WindowTitle.Size = new System.Drawing.Size(71, 12);
            this.WindowTitle.TabIndex = 0;
            this.WindowTitle.Text = "WindowTitle";
            // 
            // hWnd
            // 
            this.hWnd.AutoSize = true;
            this.hWnd.Location = new System.Drawing.Point(3, 12);
            this.hWnd.Name = "hWnd";
            this.hWnd.Size = new System.Drawing.Size(29, 12);
            this.hWnd.TabIndex = 1;
            this.hWnd.Text = "hWnd";
            // 
            // UserControlWindowItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Name = "UserControlWindowItem";
            this.Size = new System.Drawing.Size(600, 100);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.panelMiddle.ResumeLayout(false);
            this.panelRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Thumbnail)).EndInit();
            this.tableLayoutPanelMiddle.ResumeLayout(false);
            this.tableLayoutPanelMiddle.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panelMiddle;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.PictureBox Thumbnail;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMiddle;
        private System.Windows.Forms.Label WindowTitle;
        private System.Windows.Forms.Label hWnd;


    }
}
