namespace Stealth.Winform
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView_WindowList = new System.Windows.Forms.DataGridView();
            this.hWnd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button_Refresh = new System.Windows.Forms.Button();
            this.groupBox_WindowDetail = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel_WindowDetail = new System.Windows.Forms.TableLayoutPanel();
            this.textBox_Title = new System.Windows.Forms.TextBox();
            this.button_Reset = new System.Windows.Forms.Button();
            this.checkBox_Top = new System.Windows.Forms.CheckBox();
            this.button_Set = new System.Windows.Forms.Button();
            this.trackBar_Trans = new System.Windows.Forms.TrackBar();
            this.label_Trans = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel_WindowList = new System.Windows.Forms.TableLayoutPanel();
            this.textBox_Filter = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_WindowList)).BeginInit();
            this.groupBox_WindowDetail.SuspendLayout();
            this.tableLayoutPanel_WindowDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Trans)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel_WindowList.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView_WindowList
            // 
            this.dataGridView_WindowList.AllowUserToAddRows = false;
            this.dataGridView_WindowList.AllowUserToDeleteRows = false;
            this.dataGridView_WindowList.AllowUserToResizeRows = false;
            this.dataGridView_WindowList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_WindowList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.hWnd,
            this.Title});
            this.tableLayoutPanel_WindowList.SetColumnSpan(this.dataGridView_WindowList, 2);
            this.dataGridView_WindowList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_WindowList.Location = new System.Drawing.Point(3, 3);
            this.dataGridView_WindowList.MultiSelect = false;
            this.dataGridView_WindowList.Name = "dataGridView_WindowList";
            this.dataGridView_WindowList.ReadOnly = true;
            this.dataGridView_WindowList.RowHeadersVisible = false;
            this.dataGridView_WindowList.RowTemplate.Height = 23;
            this.dataGridView_WindowList.RowTemplate.ReadOnly = true;
            this.dataGridView_WindowList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_WindowList.ShowCellErrors = false;
            this.dataGridView_WindowList.ShowEditingIcon = false;
            this.dataGridView_WindowList.ShowRowErrors = false;
            this.dataGridView_WindowList.Size = new System.Drawing.Size(479, 201);
            this.dataGridView_WindowList.TabIndex = 0;
            this.dataGridView_WindowList.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_WindowList_RowEnter);
            // 
            // hWnd
            // 
            this.hWnd.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.hWnd.DataPropertyName = "hWnd";
            this.hWnd.HeaderText = "hWnd";
            this.hWnd.Name = "hWnd";
            this.hWnd.ReadOnly = true;
            this.hWnd.Width = 54;
            // 
            // Title
            // 
            this.Title.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Title.DataPropertyName = "windowTitle";
            this.Title.HeaderText = "Title";
            this.Title.Name = "Title";
            this.Title.ReadOnly = true;
            this.Title.Width = 60;
            // 
            // button_Refresh
            // 
            this.button_Refresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Refresh.Location = new System.Drawing.Point(419, 210);
            this.button_Refresh.Name = "button_Refresh";
            this.button_Refresh.Size = new System.Drawing.Size(63, 23);
            this.button_Refresh.TabIndex = 1;
            this.button_Refresh.Text = "Refresh";
            this.button_Refresh.UseVisualStyleBackColor = true;
            this.button_Refresh.Click += new System.EventHandler(this.button_Refresh_Click);
            // 
            // groupBox_WindowDetail
            // 
            this.groupBox_WindowDetail.Controls.Add(this.tableLayoutPanel_WindowDetail);
            this.groupBox_WindowDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_WindowDetail.Location = new System.Drawing.Point(3, 245);
            this.groupBox_WindowDetail.Name = "groupBox_WindowDetail";
            this.groupBox_WindowDetail.Size = new System.Drawing.Size(485, 158);
            this.groupBox_WindowDetail.TabIndex = 2;
            this.groupBox_WindowDetail.TabStop = false;
            this.groupBox_WindowDetail.Text = "Window Detail";
            // 
            // tableLayoutPanel_WindowDetail
            // 
            this.tableLayoutPanel_WindowDetail.ColumnCount = 3;
            this.tableLayoutPanel_WindowDetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel_WindowDetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_WindowDetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel_WindowDetail.Controls.Add(this.textBox_Title, 0, 0);
            this.tableLayoutPanel_WindowDetail.Controls.Add(this.button_Reset, 0, 3);
            this.tableLayoutPanel_WindowDetail.Controls.Add(this.checkBox_Top, 2, 2);
            this.tableLayoutPanel_WindowDetail.Controls.Add(this.button_Set, 2, 3);
            this.tableLayoutPanel_WindowDetail.Controls.Add(this.trackBar_Trans, 0, 1);
            this.tableLayoutPanel_WindowDetail.Controls.Add(this.label_Trans, 1, 2);
            this.tableLayoutPanel_WindowDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_WindowDetail.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel_WindowDetail.Name = "tableLayoutPanel_WindowDetail";
            this.tableLayoutPanel_WindowDetail.RowCount = 4;
            this.tableLayoutPanel_WindowDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel_WindowDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel_WindowDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel_WindowDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel_WindowDetail.Size = new System.Drawing.Size(479, 138);
            this.tableLayoutPanel_WindowDetail.TabIndex = 6;
            // 
            // textBox_Title
            // 
            this.tableLayoutPanel_WindowDetail.SetColumnSpan(this.textBox_Title, 3);
            this.textBox_Title.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_Title.Location = new System.Drawing.Point(3, 3);
            this.textBox_Title.Name = "textBox_Title";
            this.textBox_Title.ReadOnly = true;
            this.textBox_Title.Size = new System.Drawing.Size(473, 21);
            this.textBox_Title.TabIndex = 4;
            // 
            // button_Reset
            // 
            this.button_Reset.Location = new System.Drawing.Point(3, 105);
            this.button_Reset.Name = "button_Reset";
            this.button_Reset.Size = new System.Drawing.Size(75, 23);
            this.button_Reset.TabIndex = 3;
            this.button_Reset.Text = "Reset";
            this.button_Reset.UseVisualStyleBackColor = true;
            this.button_Reset.Click += new System.EventHandler(this.button_Reset_Click);
            // 
            // checkBox_Top
            // 
            this.checkBox_Top.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_Top.AutoSize = true;
            this.checkBox_Top.Location = new System.Drawing.Point(392, 71);
            this.checkBox_Top.Name = "checkBox_Top";
            this.checkBox_Top.Size = new System.Drawing.Size(84, 28);
            this.checkBox_Top.TabIndex = 5;
            this.checkBox_Top.Text = "Pin on top";
            this.checkBox_Top.UseVisualStyleBackColor = true;
            // 
            // button_Set
            // 
            this.button_Set.Location = new System.Drawing.Point(392, 105);
            this.button_Set.Name = "button_Set";
            this.button_Set.Size = new System.Drawing.Size(75, 23);
            this.button_Set.TabIndex = 1;
            this.button_Set.Text = "Set";
            this.button_Set.UseVisualStyleBackColor = true;
            this.button_Set.Click += new System.EventHandler(this.button_Set_Click);
            // 
            // trackBar_Trans
            // 
            this.tableLayoutPanel_WindowDetail.SetColumnSpan(this.trackBar_Trans, 3);
            this.trackBar_Trans.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar_Trans.Location = new System.Drawing.Point(3, 37);
            this.trackBar_Trans.Maximum = 255;
            this.trackBar_Trans.Minimum = 31;
            this.trackBar_Trans.Name = "trackBar_Trans";
            this.trackBar_Trans.Size = new System.Drawing.Size(473, 28);
            this.trackBar_Trans.TabIndex = 0;
            this.trackBar_Trans.TickFrequency = 15;
            this.trackBar_Trans.Value = 255;
            this.trackBar_Trans.Scroll += new System.EventHandler(this.trackBar_Trans_Scroll);
            // 
            // label_Trans
            // 
            this.label_Trans.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_Trans.AutoSize = true;
            this.label_Trans.Location = new System.Drawing.Point(84, 68);
            this.label_Trans.Name = "label_Trans";
            this.label_Trans.Size = new System.Drawing.Size(302, 34);
            this.label_Trans.TabIndex = 2;
            this.label_Trans.Text = "255/255";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel_WindowList, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox_WindowDetail, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 25);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(491, 406);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // tableLayoutPanel_WindowList
            // 
            this.tableLayoutPanel_WindowList.ColumnCount = 2;
            this.tableLayoutPanel_WindowList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_WindowList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel_WindowList.Controls.Add(this.dataGridView_WindowList, 0, 0);
            this.tableLayoutPanel_WindowList.Controls.Add(this.button_Refresh, 1, 1);
            this.tableLayoutPanel_WindowList.Controls.Add(this.textBox_Filter, 0, 1);
            this.tableLayoutPanel_WindowList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_WindowList.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel_WindowList.Name = "tableLayoutPanel_WindowList";
            this.tableLayoutPanel_WindowList.RowCount = 2;
            this.tableLayoutPanel_WindowList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_WindowList.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_WindowList.Size = new System.Drawing.Size(485, 236);
            this.tableLayoutPanel_WindowList.TabIndex = 0;
            // 
            // textBox_Filter
            // 
            this.textBox_Filter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_Filter.Location = new System.Drawing.Point(3, 210);
            this.textBox_Filter.Name = "textBox_Filter";
            this.textBox_Filter.Size = new System.Drawing.Size(410, 21);
            this.textBox_Filter.TabIndex = 2;
            this.textBox_Filter.TextChanged += new System.EventHandler(this.textBox_Filter_TextChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(491, 25);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(39, 21);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(96, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(47, 21);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 431);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Stealth";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_WindowList)).EndInit();
            this.groupBox_WindowDetail.ResumeLayout(false);
            this.tableLayoutPanel_WindowDetail.ResumeLayout(false);
            this.tableLayoutPanel_WindowDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Trans)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel_WindowList.ResumeLayout(false);
            this.tableLayoutPanel_WindowList.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button_Refresh;
        private System.Windows.Forms.GroupBox groupBox_WindowDetail;
        private System.Windows.Forms.Label label_Trans;
        private System.Windows.Forms.Button button_Set;
        private System.Windows.Forms.TrackBar trackBar_Trans;
        private System.Windows.Forms.Button button_Reset;
        private System.Windows.Forms.TextBox textBox_Title;
        private System.Windows.Forms.DataGridView dataGridView_WindowList;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title;
        private System.Windows.Forms.DataGridViewTextBoxColumn hWnd;
        private System.Windows.Forms.CheckBox checkBox_Top;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_WindowDetail;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_WindowList;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.TextBox textBox_Filter;
    }
}

