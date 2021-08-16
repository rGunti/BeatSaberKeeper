
namespace BeatKeeper.App
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ArchiveMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.DownloadVanillaArchiveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.BackupTabPage = new System.Windows.Forms.TabPage();
            this.BackupArchivesListView = new System.Windows.Forms.ListView();
            this.BackupNameColumn = new System.Windows.Forms.ColumnHeader();
            this.BackupGameVersionColumn = new System.Windows.Forms.ColumnHeader();
            this.BackupAgeColumn = new System.Windows.Forms.ColumnHeader();
            this.BackupSizeColumn = new System.Windows.Forms.ColumnHeader();
            this.VanillaTabPage = new System.Windows.Forms.TabPage();
            this.VanillaArchivesListView = new System.Windows.Forms.ListView();
            this.VanillaGameVersionColumn = new System.Windows.Forms.ColumnHeader();
            this.VanillaSizeColumn = new System.Windows.Forms.ColumnHeader();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.BackupTabPage.SuspendLayout();
            this.VanillaTabPage.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.FileMenu, this.ArchiveMenu, this.HelpMenu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(531, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // FileMenu
            // 
            this.FileMenu.Name = "FileMenu";
            this.FileMenu.Size = new System.Drawing.Size(37, 20);
            this.FileMenu.Text = "&File";
            // 
            // ArchiveMenu
            // 
            this.ArchiveMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.DownloadVanillaArchiveMenuItem});
            this.ArchiveMenu.Name = "ArchiveMenu";
            this.ArchiveMenu.Size = new System.Drawing.Size(64, 20);
            this.ArchiveMenu.Text = "&Archives";
            // 
            // DownloadVanillaArchiveMenuItem
            // 
            this.DownloadVanillaArchiveMenuItem.Name = "DownloadVanillaArchiveMenuItem";
            this.DownloadVanillaArchiveMenuItem.Size = new System.Drawing.Size(213, 22);
            this.DownloadVanillaArchiveMenuItem.Text = "Download Vanilla Archives";
            this.DownloadVanillaArchiveMenuItem.Click += new System.EventHandler(this.DownloadVanillaArchiveMenuItem_Click);
            // 
            // HelpMenu
            // 
            this.HelpMenu.Name = "HelpMenu";
            this.HelpMenu.Size = new System.Drawing.Size(24, 20);
            this.HelpMenu.Text = "&?";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.BackupTabPage);
            this.tabControl1.Controls.Add(this.VanillaTabPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(531, 320);
            this.tabControl1.TabIndex = 1;
            // 
            // BackupTabPage
            // 
            this.BackupTabPage.Controls.Add(this.BackupArchivesListView);
            this.BackupTabPage.Location = new System.Drawing.Point(4, 22);
            this.BackupTabPage.Name = "BackupTabPage";
            this.BackupTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.BackupTabPage.Size = new System.Drawing.Size(523, 294);
            this.BackupTabPage.TabIndex = 0;
            this.BackupTabPage.Text = "Backups";
            this.BackupTabPage.UseVisualStyleBackColor = true;
            // 
            // BackupArchivesListView
            // 
            this.BackupArchivesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {this.BackupNameColumn, this.BackupGameVersionColumn, this.BackupAgeColumn, this.BackupSizeColumn});
            this.BackupArchivesListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BackupArchivesListView.FullRowSelect = true;
            this.BackupArchivesListView.HideSelection = false;
            this.BackupArchivesListView.Location = new System.Drawing.Point(3, 3);
            this.BackupArchivesListView.Name = "BackupArchivesListView";
            this.BackupArchivesListView.Size = new System.Drawing.Size(517, 288);
            this.BackupArchivesListView.SmallImageList = this.imageList1;
            this.BackupArchivesListView.TabIndex = 0;
            this.BackupArchivesListView.UseCompatibleStateImageBehavior = false;
            this.BackupArchivesListView.View = System.Windows.Forms.View.Details;
            // 
            // BackupNameColumn
            // 
            this.BackupNameColumn.Name = "BackupNameColumn";
            this.BackupNameColumn.Text = "Name";
            this.BackupNameColumn.Width = 290;
            // 
            // BackupGameVersionColumn
            // 
            this.BackupGameVersionColumn.Name = "BackupGameVersionColumn";
            this.BackupGameVersionColumn.Text = "Game Version";
            this.BackupGameVersionColumn.Width = 85;
            // 
            // BackupAgeColumn
            // 
            this.BackupAgeColumn.Name = "BackupAgeColumn";
            this.BackupAgeColumn.Text = "Last updated at";
            this.BackupAgeColumn.Width = 115;
            // 
            // BackupSizeColumn
            // 
            this.BackupSizeColumn.Name = "BackupSizeColumn";
            this.BackupSizeColumn.Text = "Size";
            this.BackupSizeColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.BackupSizeColumn.Width = 90;
            // 
            // VanillaTabPage
            // 
            this.VanillaTabPage.Controls.Add(this.VanillaArchivesListView);
            this.VanillaTabPage.Location = new System.Drawing.Point(4, 22);
            this.VanillaTabPage.Name = "VanillaTabPage";
            this.VanillaTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.VanillaTabPage.Size = new System.Drawing.Size(523, 294);
            this.VanillaTabPage.TabIndex = 1;
            this.VanillaTabPage.Text = "Vanilla Archives";
            this.VanillaTabPage.UseVisualStyleBackColor = true;
            // 
            // VanillaArchivesListView
            // 
            this.VanillaArchivesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {this.VanillaGameVersionColumn, this.VanillaSizeColumn});
            this.VanillaArchivesListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VanillaArchivesListView.FullRowSelect = true;
            this.VanillaArchivesListView.HideSelection = false;
            this.VanillaArchivesListView.Location = new System.Drawing.Point(3, 3);
            this.VanillaArchivesListView.Name = "VanillaArchivesListView";
            this.VanillaArchivesListView.Size = new System.Drawing.Size(517, 288);
            this.VanillaArchivesListView.SmallImageList = this.imageList1;
            this.VanillaArchivesListView.TabIndex = 0;
            this.VanillaArchivesListView.UseCompatibleStateImageBehavior = false;
            this.VanillaArchivesListView.View = System.Windows.Forms.View.Details;
            // 
            // VanillaGameVersionColumn
            // 
            this.VanillaGameVersionColumn.Name = "VanillaGameVersionColumn";
            this.VanillaGameVersionColumn.Text = "Game Version";
            this.VanillaGameVersionColumn.Width = 350;
            // 
            // VanillaSizeColumn
            // 
            this.VanillaSizeColumn.Name = "VanillaSizeColumn";
            this.VanillaSizeColumn.Text = "Size";
            this.VanillaSizeColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.VanillaSizeColumn.Width = 100;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.StatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 344);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
            this.statusStrip1.Size = new System.Drawing.Size(531, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusLabel
            // 
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(39, 17);
            this.StatusLabel.Text = "Ready";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer) (resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Saber");
            this.imageList1.Images.SetKeyName(1, "SaberPack");
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 366);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Beat Saber Keeper";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.BackupTabPage.ResumeLayout(false);
            this.VanillaTabPage.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ImageList imageList1;

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage BackupTabPage;
        private System.Windows.Forms.TabPage VanillaTabPage;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.ListView BackupArchivesListView;
        private System.Windows.Forms.ColumnHeader BackupNameColumn;
        private System.Windows.Forms.ColumnHeader BackupGameVersionColumn;
        private System.Windows.Forms.ColumnHeader BackupAgeColumn;
        private System.Windows.Forms.ColumnHeader BackupSizeColumn;
        private System.Windows.Forms.ListView VanillaArchivesListView;
        private System.Windows.Forms.ColumnHeader VanillaGameVersionColumn;
        private System.Windows.Forms.ColumnHeader VanillaSizeColumn;
        private System.Windows.Forms.ToolStripMenuItem FileMenu;
        private System.Windows.Forms.ToolStripMenuItem ArchiveMenu;
        private System.Windows.Forms.ToolStripMenuItem DownloadVanillaArchiveMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HelpMenu;
    }
}

