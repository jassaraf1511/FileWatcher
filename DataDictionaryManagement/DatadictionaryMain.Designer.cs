namespace DataDictionaryManagement
{
    partial class DatadictionaryMain
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.DataMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.NewDataDictionary = new System.Windows.Forms.ToolStripMenuItem();
            this.LoadDataDictionary = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveDataDictionary = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAsDataDictionary = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitApplication = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.lblDataDictionaryFile = new System.Windows.Forms.Label();
            this.DataDictionaryView = new System.Windows.Forms.DataGridView();
            this.menuStrip1.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataDictionaryView)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DataMenu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(2824, 49);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // DataMenu
            // 
            this.DataMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewDataDictionary,
            this.LoadDataDictionary,
            this.SaveDataDictionary,
            this.SaveAsDataDictionary,
            this.ExitApplication});
            this.DataMenu.Name = "DataMenu";
            this.DataMenu.Size = new System.Drawing.Size(176, 45);
            this.DataMenu.Text = "Dictionary";
            // 
            // NewDataDictionary
            // 
            this.NewDataDictionary.Name = "NewDataDictionary";
            this.NewDataDictionary.Size = new System.Drawing.Size(488, 54);
            this.NewDataDictionary.Text = "New Data Dictionary";
            this.NewDataDictionary.Click += new System.EventHandler(this.NewDataDictionary_Click);
            // 
            // LoadDataDictionary
            // 
            this.LoadDataDictionary.Name = "LoadDataDictionary";
            this.LoadDataDictionary.Size = new System.Drawing.Size(488, 54);
            this.LoadDataDictionary.Text = "Load Data Dictionary";
            this.LoadDataDictionary.Click += new System.EventHandler(this.LoadDataDictionary_Click);
            // 
            // SaveDataDictionary
            // 
            this.SaveDataDictionary.Name = "SaveDataDictionary";
            this.SaveDataDictionary.Size = new System.Drawing.Size(488, 54);
            this.SaveDataDictionary.Text = "Save Data Dictionary";
            this.SaveDataDictionary.Click += new System.EventHandler(this.SaveDataDicionary_click);
            // 
            // SaveAsDataDictionary
            // 
            this.SaveAsDataDictionary.Name = "SaveAsDataDictionary";
            this.SaveAsDataDictionary.Size = new System.Drawing.Size(488, 54);
            this.SaveAsDataDictionary.Text = "Save As DataDictionary";
            this.SaveAsDataDictionary.Click += new System.EventHandler(this.SaveAsDataDictionary_Click);
            // 
            // ExitApplication
            // 
            this.ExitApplication.Name = "ExitApplication";
            this.ExitApplication.Size = new System.Drawing.Size(488, 54);
            this.ExitApplication.Text = "Exit Application";
            this.ExitApplication.Click += new System.EventHandler(this.DataDictionary_Exit_Click);
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.lblDataDictionaryFile);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.DataDictionaryView);
            this.toolStripContainer1.ContentPanel.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(2824, 1438);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(2824, 1498);
            this.toolStripContainer1.TabIndex = 1;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
            // 
            // lblDataDictionaryFile
            // 
            this.lblDataDictionaryFile.AutoSize = true;
            this.lblDataDictionaryFile.BackColor = System.Drawing.Color.Cornsilk;
            this.lblDataDictionaryFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataDictionaryFile.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblDataDictionaryFile.Location = new System.Drawing.Point(145, 474);
            this.lblDataDictionaryFile.Name = "lblDataDictionaryFile";
            this.lblDataDictionaryFile.Size = new System.Drawing.Size(373, 44);
            this.lblDataDictionaryFile.TabIndex = 1;
            this.lblDataDictionaryFile.Text = "Data Dictionary File :";
            // 
            // DataDictionaryView
            // 
            this.DataDictionaryView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataDictionaryView.Location = new System.Drawing.Point(139, 513);
            this.DataDictionaryView.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.DataDictionaryView.Name = "DataDictionaryView";
            this.DataDictionaryView.RowHeadersWidth = 102;
            this.DataDictionaryView.Size = new System.Drawing.Size(3541, 1247);
            this.DataDictionaryView.TabIndex = 0;
            this.DataDictionaryView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataDictionaryView_CellContentClick);
            this.DataDictionaryView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataDictionaryView_CellContentClick);
            this.DataDictionaryView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DataDictionaryView_CellFormatting);
            this.DataDictionaryView.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataDictionaryView_CellMouseClick);
            this.DataDictionaryView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataDictionaryView_CellMouseClick);
            this.DataDictionaryView.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataDictionaryView_RowEnter);
            this.DataDictionaryView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.DataDictionaryView_RowsAdded);
            this.DataDictionaryView.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.DataDictionaryView_RowsRemoved);
            // 
            // DatadictionaryMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSlateGray;
            this.ClientSize = new System.Drawing.Size(2824, 1498);
            this.Controls.Add(this.toolStripContainer1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.Name = "DatadictionaryMain";
            this.Text = "Data Dictionary Management";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DatadictionaryMain_FormClosed);
            this.Load += new System.EventHandler(this.DatadictionaryMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ContentPanel.PerformLayout();
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataDictionaryView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem DataMenu;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStripMenuItem NewDataDictionary;
        private System.Windows.Forms.ToolStripMenuItem LoadDataDictionary;
        private System.Windows.Forms.DataGridView DataDictionaryView;
        private System.Windows.Forms.ToolStripMenuItem SaveDataDictionary;
        private System.Windows.Forms.ToolStripMenuItem SaveAsDataDictionary;
        private System.Windows.Forms.Label lblDataDictionaryFile;
        private System.Windows.Forms.ToolStripMenuItem ExitApplication;
    }
}

