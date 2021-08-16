
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
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ArchiveMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.DownloadVanillaArchiveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.viewSourceCodeOnGitHubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutBeatSaberKeeperToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.BackupTabPage = new System.Windows.Forms.TabPage();
            this.BackupArchivesListView = new System.Windows.Forms.ListView();
            this.BackupNameColumn = new System.Windows.Forms.ColumnHeader();
            this.BackupGameVersionColumn = new System.Windows.Forms.ColumnHeader();
            this.BackupAgeColumn = new System.Windows.Forms.ColumnHeader();
            this.BackupSizeColumn = new System.Windows.Forms.ColumnHeader();
            this.ArchiveContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.unpackRunToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unpackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.cloneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.VanillaTabPage = new System.Windows.Forms.TabPage();
            this.VanillaArchivesListView = new System.Windows.Forms.ListView();
            this.VanillaGameVersionColumn = new System.Windows.Forms.ColumnHeader();
            this.VanillaSizeColumn = new System.Windows.Forms.ColumnHeader();
            this.StatusBar = new System.Windows.Forms.StatusStrip();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.showInExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.BackupTabPage.SuspendLayout();
            this.ArchiveContextMenuStrip.SuspendLayout();
            this.VanillaTabPage.SuspendLayout();
            this.StatusBar.SuspendLayout();
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
            this.FileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.exitToolStripMenuItem});
            this.FileMenu.Name = "FileMenu";
            this.FileMenu.Size = new System.Drawing.Size(37, 20);
            this.FileMenu.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
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
            this.HelpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.viewSourceCodeOnGitHubToolStripMenuItem, this.aboutBeatSaberKeeperToolStripMenuItem});
            this.HelpMenu.Name = "HelpMenu";
            this.HelpMenu.Size = new System.Drawing.Size(24, 20);
            this.HelpMenu.Text = "&?";
            // 
            // viewSourceCodeOnGitHubToolStripMenuItem
            // 
            this.viewSourceCodeOnGitHubToolStripMenuItem.Name = "viewSourceCodeOnGitHubToolStripMenuItem";
            this.viewSourceCodeOnGitHubToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.viewSourceCodeOnGitHubToolStripMenuItem.Text = "View Source Code on GitHub";
            // 
            // aboutBeatSaberKeeperToolStripMenuItem
            // 
            this.aboutBeatSaberKeeperToolStripMenuItem.Name = "aboutBeatSaberKeeperToolStripMenuItem";
            this.aboutBeatSaberKeeperToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.aboutBeatSaberKeeperToolStripMenuItem.Text = "About Beat Saber Keeper ...";
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
            this.BackupArchivesListView.ContextMenuStrip = this.ArchiveContextMenuStrip;
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
            // ArchiveContextMenuStrip
            // 
            this.ArchiveContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.unpackRunToolStripMenuItem, this.unpackToolStripMenuItem, this.toolStripMenuItem1, this.cloneToolStripMenuItem, this.updateToolStripMenuItem, this.renameToolStripMenuItem, this.deleteToolStripMenuItem, this.toolStripMenuItem2, this.showInExplorerToolStripMenuItem, this.propertiesToolStripMenuItem});
            this.ArchiveContextMenuStrip.Name = "ArchiveContextMenuStrip";
            this.ArchiveContextMenuStrip.Size = new System.Drawing.Size(163, 214);
            this.ArchiveContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.ArchiveContextMenuStrip_Opening);
            // 
            // unpackRunToolStripMenuItem
            // 
            this.unpackRunToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.unpackRunToolStripMenuItem.Name = "unpackRunToolStripMenuItem";
            this.unpackRunToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.unpackRunToolStripMenuItem.Text = "Unpack && Run";
            // 
            // unpackToolStripMenuItem
            // 
            this.unpackToolStripMenuItem.Name = "unpackToolStripMenuItem";
            this.unpackToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.unpackToolStripMenuItem.Text = "Unpack";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(159, 6);
            // 
            // cloneToolStripMenuItem
            // 
            this.cloneToolStripMenuItem.Name = "cloneToolStripMenuItem";
            this.cloneToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.cloneToolStripMenuItem.Text = "Clone";
            this.cloneToolStripMenuItem.Click += new System.EventHandler(this.cloneToolStripMenuItem_Click);
            // 
            // updateToolStripMenuItem
            // 
            this.updateToolStripMenuItem.Name = "updateToolStripMenuItem";
            this.updateToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.updateToolStripMenuItem.Text = "Update";
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.renameToolStripMenuItem.Text = "Rename";
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(159, 6);
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.propertiesToolStripMenuItem.Text = "Properties";
            this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.propertiesToolStripMenuItem_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer) (resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Saber");
            this.imageList1.Images.SetKeyName(1, "SaberPack");
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
            this.VanillaArchivesListView.ContextMenuStrip = this.ArchiveContextMenuStrip;
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
            // StatusBar
            // 
            this.StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.StatusLabel, this.StatusProgressBar});
            this.StatusBar.Location = new System.Drawing.Point(0, 344);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
            this.StatusBar.Size = new System.Drawing.Size(531, 22);
            this.StatusBar.TabIndex = 2;
            this.StatusBar.Text = "StatusBar";
            // 
            // StatusLabel
            // 
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(416, 17);
            this.StatusLabel.Spring = true;
            this.StatusLabel.Text = "Ready";
            this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StatusProgressBar
            // 
            this.StatusProgressBar.Name = "StatusProgressBar";
            this.StatusProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // showInExplorerToolStripMenuItem
            // 
            this.showInExplorerToolStripMenuItem.Name = "showInExplorerToolStripMenuItem";
            this.showInExplorerToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.showInExplorerToolStripMenuItem.Text = "Show in Explorer";
            this.showInExplorerToolStripMenuItem.Click += new System.EventHandler(this.showInExplorerToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 366);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.StatusBar);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Beat Saber Keeper";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.BackupTabPage.ResumeLayout(false);
            this.ArchiveContextMenuStrip.ResumeLayout(false);
            this.VanillaTabPage.ResumeLayout(false);
            this.StatusBar.ResumeLayout(false);
            this.StatusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ToolStripMenuItem showInExplorerToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem cloneToolStripMenuItem;

        private System.Windows.Forms.ContextMenuStrip ArchiveContextMenuStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem unpackRunToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unpackToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem updateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;

        private System.Windows.Forms.StatusStrip StatusBar;

        private System.Windows.Forms.ToolStripProgressBar StatusProgressBar;

        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutBeatSaberKeeperToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewSourceCodeOnGitHubToolStripMenuItem;

        private System.Windows.Forms.ImageList imageList1;

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage BackupTabPage;
        private System.Windows.Forms.TabPage VanillaTabPage;
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

