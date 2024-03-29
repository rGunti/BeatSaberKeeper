﻿
namespace BeatSaberKeeper.App
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
            this.NewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.UnpackRunMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UnpackMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.CloneMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UpdateMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RenameMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ShowHistoryMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowInSystemExplorerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PropertiesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.RefreshMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ArchiveMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.DownloadVanillaArchiveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SongExplorerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.OpenDataDirectoryMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenGameDirectoryMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.SetGameDirectoryMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GameDirectoryTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.CheckForUpdatesOnStartupMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PreReleaseMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.whereAreMyArchivesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.checkTheWebsiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewSourceCodeOnGitHubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutBeatSaberKeeperToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TabContainer = new System.Windows.Forms.TabControl();
            this.BackupTabPage = new System.Windows.Forms.TabPage();
            this.BackupArchivesListView = new System.Windows.Forms.ListView();
            this.BackupNameColumn = new System.Windows.Forms.ColumnHeader();
            this.BackupGameVersionColumn = new System.Windows.Forms.ColumnHeader();
            this.BackupArchiveVersionColumn = new System.Windows.Forms.ColumnHeader();
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
            this.showHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showInExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.VanillaTabPage = new System.Windows.Forms.TabPage();
            this.VanillaArchivesListView = new System.Windows.Forms.ListView();
            this.VanillaGameVersionColumn = new System.Windows.Forms.ColumnHeader();
            this.VanillaSizeColumn = new System.Windows.Forms.ColumnHeader();
            this.StatusBar = new System.Windows.Forms.StatusStrip();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.menuStrip1.SuspendLayout();
            this.TabContainer.SuspendLayout();
            this.BackupTabPage.SuspendLayout();
            this.ArchiveContextMenuStrip.SuspendLayout();
            this.VanillaTabPage.SuspendLayout();
            this.StatusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenu,
            this.ViewMenu,
            this.ArchiveMenu,
            this.SettingsMenu,
            this.HelpMenu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(620, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // FileMenu
            // 
            this.FileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewMenuItem,
            this.toolStripSeparator4,
            this.UnpackRunMenuItem,
            this.UnpackMenuItem,
            this.toolStripSeparator1,
            this.CloneMenuItem,
            this.UpdateMenuItem,
            this.RenameMenuItem,
            this.DeleteMenuItem,
            this.toolStripSeparator2,
            this.ShowHistoryMenuItem,
            this.ShowInSystemExplorerMenuItem,
            this.PropertiesMenuItem,
            this.toolStripSeparator3,
            this.ExitMenuItem});
            this.FileMenu.Name = "FileMenu";
            this.FileMenu.Size = new System.Drawing.Size(37, 20);
            this.FileMenu.Text = "&File";
            // 
            // NewMenuItem
            // 
            this.NewMenuItem.Name = "NewMenuItem";
            this.NewMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.NewMenuItem.Size = new System.Drawing.Size(184, 22);
            this.NewMenuItem.Text = "&New Archive";
            this.NewMenuItem.Click += new System.EventHandler(this.NewMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(181, 6);
            // 
            // UnpackRunMenuItem
            // 
            this.UnpackRunMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.UnpackRunMenuItem.Name = "UnpackRunMenuItem";
            this.UnpackRunMenuItem.Size = new System.Drawing.Size(184, 22);
            this.UnpackRunMenuItem.Text = "Unpack && Run";
            this.UnpackRunMenuItem.Click += new System.EventHandler(this.UnpackRunMenuItem_Click);
            // 
            // UnpackMenuItem
            // 
            this.UnpackMenuItem.Name = "UnpackMenuItem";
            this.UnpackMenuItem.Size = new System.Drawing.Size(184, 22);
            this.UnpackMenuItem.Text = "Unpack";
            this.UnpackMenuItem.Click += new System.EventHandler(this.UnpackMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(181, 6);
            // 
            // CloneMenuItem
            // 
            this.CloneMenuItem.Name = "CloneMenuItem";
            this.CloneMenuItem.Size = new System.Drawing.Size(184, 22);
            this.CloneMenuItem.Text = "Clone";
            this.CloneMenuItem.Click += new System.EventHandler(this.CloneMenuItem_Click);
            // 
            // UpdateMenuItem
            // 
            this.UpdateMenuItem.Name = "UpdateMenuItem";
            this.UpdateMenuItem.Size = new System.Drawing.Size(184, 22);
            this.UpdateMenuItem.Text = "Update";
            this.UpdateMenuItem.Click += new System.EventHandler(this.UpdateMenuItem_Click);
            // 
            // RenameMenuItem
            // 
            this.RenameMenuItem.Name = "RenameMenuItem";
            this.RenameMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.RenameMenuItem.Size = new System.Drawing.Size(184, 22);
            this.RenameMenuItem.Text = "Rename";
            this.RenameMenuItem.Click += new System.EventHandler(this.RenameMenuItem_Click);
            // 
            // DeleteMenuItem
            // 
            this.DeleteMenuItem.Name = "DeleteMenuItem";
            this.DeleteMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.DeleteMenuItem.Size = new System.Drawing.Size(184, 22);
            this.DeleteMenuItem.Text = "Delete";
            this.DeleteMenuItem.Click += new System.EventHandler(this.DeleteMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(181, 6);
            // 
            // ShowHistoryMenuItem
            // 
            this.ShowHistoryMenuItem.Name = "ShowHistoryMenuItem";
            this.ShowHistoryMenuItem.Size = new System.Drawing.Size(184, 22);
            this.ShowHistoryMenuItem.Text = "Show History";
            this.ShowHistoryMenuItem.Click += new System.EventHandler(this.ShowHistoryMenuItem_Click);
            // 
            // ShowInSystemExplorerMenuItem
            // 
            this.ShowInSystemExplorerMenuItem.Name = "ShowInSystemExplorerMenuItem";
            this.ShowInSystemExplorerMenuItem.Size = new System.Drawing.Size(184, 22);
            this.ShowInSystemExplorerMenuItem.Text = "Show in Explorer";
            this.ShowInSystemExplorerMenuItem.Click += new System.EventHandler(this.ShowInSystemExplorerMenuItem_Click);
            // 
            // PropertiesMenuItem
            // 
            this.PropertiesMenuItem.Name = "PropertiesMenuItem";
            this.PropertiesMenuItem.Size = new System.Drawing.Size(184, 22);
            this.PropertiesMenuItem.Text = "Properties";
            this.PropertiesMenuItem.Click += new System.EventHandler(this.PropertiesMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(181, 6);
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Name = "ExitMenuItem";
            this.ExitMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.ExitMenuItem.Size = new System.Drawing.Size(184, 22);
            this.ExitMenuItem.Text = "E&xit";
            this.ExitMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // ViewMenu
            // 
            this.ViewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RefreshMenuItem});
            this.ViewMenu.Name = "ViewMenu";
            this.ViewMenu.Size = new System.Drawing.Size(44, 20);
            this.ViewMenu.Text = "&View";
            // 
            // RefreshMenuItem
            // 
            this.RefreshMenuItem.Name = "RefreshMenuItem";
            this.RefreshMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.RefreshMenuItem.Size = new System.Drawing.Size(132, 22);
            this.RefreshMenuItem.Text = "Refresh";
            this.RefreshMenuItem.Click += new System.EventHandler(this.RefreshMenuItem_Click);
            // 
            // ArchiveMenu
            // 
            this.ArchiveMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DownloadVanillaArchiveMenuItem,
            this.SongExplorerMenuItem,
            this.toolStripMenuItem4,
            this.OpenDataDirectoryMenuItem,
            this.OpenGameDirectoryMenuItem});
            this.ArchiveMenu.Name = "ArchiveMenu";
            this.ArchiveMenu.Size = new System.Drawing.Size(46, 20);
            this.ArchiveMenu.Text = "&Tools";
            // 
            // DownloadVanillaArchiveMenuItem
            // 
            this.DownloadVanillaArchiveMenuItem.Name = "DownloadVanillaArchiveMenuItem";
            this.DownloadVanillaArchiveMenuItem.Size = new System.Drawing.Size(213, 22);
            this.DownloadVanillaArchiveMenuItem.Text = "Download Vanilla Archives";
            this.DownloadVanillaArchiveMenuItem.Click += new System.EventHandler(this.DownloadVanillaArchiveMenuItem_Click);
            // 
            // SongExplorerMenuItem
            // 
            this.SongExplorerMenuItem.Name = "SongExplorerMenuItem";
            this.SongExplorerMenuItem.Size = new System.Drawing.Size(213, 22);
            this.SongExplorerMenuItem.Text = "Song Explorer (Beta)";
            this.SongExplorerMenuItem.Click += new System.EventHandler(this.SongExplorerMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(210, 6);
            // 
            // OpenDataDirectoryMenuItem
            // 
            this.OpenDataDirectoryMenuItem.Name = "OpenDataDirectoryMenuItem";
            this.OpenDataDirectoryMenuItem.Size = new System.Drawing.Size(213, 22);
            this.OpenDataDirectoryMenuItem.Text = "Open Data Directory";
            this.OpenDataDirectoryMenuItem.Click += new System.EventHandler(this.OpenDataDirectoryMenuItem_Click);
            // 
            // OpenGameDirectoryMenuItem
            // 
            this.OpenGameDirectoryMenuItem.Name = "OpenGameDirectoryMenuItem";
            this.OpenGameDirectoryMenuItem.Size = new System.Drawing.Size(213, 22);
            this.OpenGameDirectoryMenuItem.Text = "Open Game Directory";
            this.OpenGameDirectoryMenuItem.Click += new System.EventHandler(this.OpenGameDirectoryMenuItem_Click);
            // 
            // SettingsMenu
            // 
            this.SettingsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SetGameDirectoryMenuItem,
            this.GameDirectoryTextBox,
            this.toolStripSeparator6,
            this.CheckForUpdatesOnStartupMenuItem,
            this.PreReleaseMenuItem});
            this.SettingsMenu.Name = "SettingsMenu";
            this.SettingsMenu.Size = new System.Drawing.Size(61, 20);
            this.SettingsMenu.Text = "&Settings";
            // 
            // SetGameDirectoryMenuItem
            // 
            this.SetGameDirectoryMenuItem.Name = "SetGameDirectoryMenuItem";
            this.SetGameDirectoryMenuItem.Size = new System.Drawing.Size(310, 22);
            this.SetGameDirectoryMenuItem.Text = "Set Game Directory";
            this.SetGameDirectoryMenuItem.Click += new System.EventHandler(this.SetGameDirectoryMenuItem_Click);
            // 
            // GameDirectoryTextBox
            // 
            this.GameDirectoryTextBox.AutoSize = false;
            this.GameDirectoryTextBox.Name = "GameDirectoryTextBox";
            this.GameDirectoryTextBox.ReadOnly = true;
            this.GameDirectoryTextBox.Size = new System.Drawing.Size(250, 23);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(307, 6);
            // 
            // CheckForUpdatesOnStartupMenuItem
            // 
            this.CheckForUpdatesOnStartupMenuItem.Name = "CheckForUpdatesOnStartupMenuItem";
            this.CheckForUpdatesOnStartupMenuItem.Size = new System.Drawing.Size(310, 22);
            this.CheckForUpdatesOnStartupMenuItem.Text = "Check for Updates on Startup";
            this.CheckForUpdatesOnStartupMenuItem.Click += new System.EventHandler(this.CheckForUpdatesOnStartupMenuItem_Click);
            // 
            // PreReleaseMenuItem
            // 
            this.PreReleaseMenuItem.Name = "PreReleaseMenuItem";
            this.PreReleaseMenuItem.Size = new System.Drawing.Size(310, 22);
            this.PreReleaseMenuItem.Text = "Opt in to Pre-Releases";
            this.PreReleaseMenuItem.ToolTipText = "If enabled, you will get updates sooner but they might not be as stable as regula" +
    "r releases or contain unfinished features.";
            this.PreReleaseMenuItem.Click += new System.EventHandler(this.PreReleaseMenuItem_Click);
            // 
            // HelpMenu
            // 
            this.HelpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.whereAreMyArchivesToolStripMenuItem,
            this.toolStripMenuItem3,
            this.checkTheWebsiteToolStripMenuItem,
            this.viewSourceCodeOnGitHubToolStripMenuItem,
            this.toolStripSeparator5,
            this.checkForUpdatesToolStripMenuItem,
            this.aboutBeatSaberKeeperToolStripMenuItem});
            this.HelpMenu.Name = "HelpMenu";
            this.HelpMenu.Size = new System.Drawing.Size(24, 20);
            this.HelpMenu.Text = "&?";
            // 
            // whereAreMyArchivesToolStripMenuItem
            // 
            this.whereAreMyArchivesToolStripMenuItem.Name = "whereAreMyArchivesToolStripMenuItem";
            this.whereAreMyArchivesToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.whereAreMyArchivesToolStripMenuItem.Tag = "https://beatsaberkeeper.rgunti.ch/help/where-are-my-archives";
            this.whereAreMyArchivesToolStripMenuItem.Text = "Where are my archives?";
            this.whereAreMyArchivesToolStripMenuItem.Click += new System.EventHandler(this.openUrlToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(224, 6);
            // 
            // checkTheWebsiteToolStripMenuItem
            // 
            this.checkTheWebsiteToolStripMenuItem.Name = "checkTheWebsiteToolStripMenuItem";
            this.checkTheWebsiteToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.checkTheWebsiteToolStripMenuItem.Tag = "https://beatsaberkeeper.rgunti.ch/";
            this.checkTheWebsiteToolStripMenuItem.Text = "BeatSaberKeeper Website";
            this.checkTheWebsiteToolStripMenuItem.Click += new System.EventHandler(this.openUrlToolStripMenuItem_Click);
            // 
            // viewSourceCodeOnGitHubToolStripMenuItem
            // 
            this.viewSourceCodeOnGitHubToolStripMenuItem.Name = "viewSourceCodeOnGitHubToolStripMenuItem";
            this.viewSourceCodeOnGitHubToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.viewSourceCodeOnGitHubToolStripMenuItem.Tag = "https://github.com/rGunti/BeatSaberKeeper";
            this.viewSourceCodeOnGitHubToolStripMenuItem.Text = "View Source Code on GitHub";
            this.viewSourceCodeOnGitHubToolStripMenuItem.Click += new System.EventHandler(this.openUrlToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(224, 6);
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.checkForUpdatesToolStripMenuItem.Text = "Check for Updates";
            this.checkForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdatesToolStripMenuItem_Click);
            // 
            // aboutBeatSaberKeeperToolStripMenuItem
            // 
            this.aboutBeatSaberKeeperToolStripMenuItem.Name = "aboutBeatSaberKeeperToolStripMenuItem";
            this.aboutBeatSaberKeeperToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.aboutBeatSaberKeeperToolStripMenuItem.Text = "About BeatSaberKeeper ...";
            this.aboutBeatSaberKeeperToolStripMenuItem.Click += new System.EventHandler(this.aboutBeatSaberKeeperToolStripMenuItem_Click);
            // 
            // TabContainer
            // 
            this.TabContainer.Controls.Add(this.BackupTabPage);
            this.TabContainer.Controls.Add(this.VanillaTabPage);
            this.TabContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabContainer.Location = new System.Drawing.Point(0, 24);
            this.TabContainer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TabContainer.Name = "TabContainer";
            this.TabContainer.SelectedIndex = 0;
            this.TabContainer.Size = new System.Drawing.Size(620, 374);
            this.TabContainer.TabIndex = 1;
            this.TabContainer.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // BackupTabPage
            // 
            this.BackupTabPage.Controls.Add(this.BackupArchivesListView);
            this.BackupTabPage.Location = new System.Drawing.Point(4, 24);
            this.BackupTabPage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BackupTabPage.Name = "BackupTabPage";
            this.BackupTabPage.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BackupTabPage.Size = new System.Drawing.Size(612, 346);
            this.BackupTabPage.TabIndex = 0;
            this.BackupTabPage.Text = "Backups";
            this.BackupTabPage.UseVisualStyleBackColor = true;
            // 
            // BackupArchivesListView
            // 
            this.BackupArchivesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.BackupNameColumn,
            this.BackupGameVersionColumn,
            this.BackupArchiveVersionColumn,
            this.BackupAgeColumn,
            this.BackupSizeColumn});
            this.BackupArchivesListView.ContextMenuStrip = this.ArchiveContextMenuStrip;
            this.BackupArchivesListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BackupArchivesListView.FullRowSelect = true;
            this.BackupArchivesListView.HideSelection = false;
            this.BackupArchivesListView.Location = new System.Drawing.Point(4, 3);
            this.BackupArchivesListView.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BackupArchivesListView.Name = "BackupArchivesListView";
            this.BackupArchivesListView.Size = new System.Drawing.Size(604, 340);
            this.BackupArchivesListView.SmallImageList = this.imageList1;
            this.BackupArchivesListView.TabIndex = 0;
            this.BackupArchivesListView.UseCompatibleStateImageBehavior = false;
            this.BackupArchivesListView.View = System.Windows.Forms.View.Details;
            this.BackupArchivesListView.SelectedIndexChanged += new System.EventHandler(this.ListView_SelectedIndexChanged);
            this.BackupArchivesListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListView_MouseDoubleClick);
            // 
            // BackupNameColumn
            // 
            this.BackupNameColumn.Name = "BackupNameColumn";
            this.BackupNameColumn.Text = "Name";
            this.BackupNameColumn.Width = 265;
            // 
            // BackupGameVersionColumn
            // 
            this.BackupGameVersionColumn.Name = "BackupGameVersionColumn";
            this.BackupGameVersionColumn.Text = "Game Ver.";
            this.BackupGameVersionColumn.Width = 65;
            // 
            // BackupArchiveVersionColumn
            // 
            this.BackupArchiveVersionColumn.Text = "Arc. Ver.";
            this.BackupArchiveVersionColumn.Width = 65;
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
            this.ArchiveContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.unpackRunToolStripMenuItem,
            this.unpackToolStripMenuItem,
            this.toolStripMenuItem1,
            this.cloneToolStripMenuItem,
            this.updateToolStripMenuItem,
            this.renameToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.toolStripMenuItem2,
            this.showHistoryToolStripMenuItem,
            this.showInExplorerToolStripMenuItem,
            this.propertiesToolStripMenuItem});
            this.ArchiveContextMenuStrip.Name = "ArchiveContextMenuStrip";
            this.ArchiveContextMenuStrip.Size = new System.Drawing.Size(163, 214);
            this.ArchiveContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.ArchiveContextMenuStrip_Opening);
            // 
            // unpackRunToolStripMenuItem
            // 
            this.unpackRunToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.unpackRunToolStripMenuItem.Name = "unpackRunToolStripMenuItem";
            this.unpackRunToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.unpackRunToolStripMenuItem.Text = "Unpack && Run";
            this.unpackRunToolStripMenuItem.Click += new System.EventHandler(this.unpackRunToolStripMenuItem_Click);
            // 
            // unpackToolStripMenuItem
            // 
            this.unpackToolStripMenuItem.Name = "unpackToolStripMenuItem";
            this.unpackToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.unpackToolStripMenuItem.Text = "Unpack";
            this.unpackToolStripMenuItem.Click += new System.EventHandler(this.unpackToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 6);
            // 
            // cloneToolStripMenuItem
            // 
            this.cloneToolStripMenuItem.Name = "cloneToolStripMenuItem";
            this.cloneToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.cloneToolStripMenuItem.Text = "Clone";
            this.cloneToolStripMenuItem.Click += new System.EventHandler(this.cloneToolStripMenuItem_Click);
            // 
            // updateToolStripMenuItem
            // 
            this.updateToolStripMenuItem.Name = "updateToolStripMenuItem";
            this.updateToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.updateToolStripMenuItem.Text = "Update";
            this.updateToolStripMenuItem.Click += new System.EventHandler(this.updateToolStripMenuItem_Click);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.renameToolStripMenuItem.Text = "Rename";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(177, 6);
            // 
            // showHistoryToolStripMenuItem
            // 
            this.showHistoryToolStripMenuItem.Name = "showHistoryToolStripMenuItem";
            this.showHistoryToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.showHistoryToolStripMenuItem.Text = "Show History";
            this.showHistoryToolStripMenuItem.Click += new System.EventHandler(this.showHistoryToolStripMenuItem_Click);
            // 
            // showInExplorerToolStripMenuItem
            // 
            this.showInExplorerToolStripMenuItem.Name = "showInExplorerToolStripMenuItem";
            this.showInExplorerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.showInExplorerToolStripMenuItem.Text = "Show in Explorer";
            this.showInExplorerToolStripMenuItem.Click += new System.EventHandler(this.showInExplorerToolStripMenuItem_Click);
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.propertiesToolStripMenuItem.Text = "Properties";
            this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.propertiesToolStripMenuItem_Click);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Saber");
            this.imageList1.Images.SetKeyName(1, "SaberPack");
            this.imageList1.Images.SetKeyName(2, "Defect");
            // 
            // VanillaTabPage
            // 
            this.VanillaTabPage.Controls.Add(this.VanillaArchivesListView);
            this.VanillaTabPage.Location = new System.Drawing.Point(4, 24);
            this.VanillaTabPage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.VanillaTabPage.Name = "VanillaTabPage";
            this.VanillaTabPage.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.VanillaTabPage.Size = new System.Drawing.Size(612, 346);
            this.VanillaTabPage.TabIndex = 1;
            this.VanillaTabPage.Text = "Vanilla Archives";
            this.VanillaTabPage.UseVisualStyleBackColor = true;
            // 
            // VanillaArchivesListView
            // 
            this.VanillaArchivesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.VanillaGameVersionColumn,
            this.VanillaSizeColumn});
            this.VanillaArchivesListView.ContextMenuStrip = this.ArchiveContextMenuStrip;
            this.VanillaArchivesListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VanillaArchivesListView.FullRowSelect = true;
            this.VanillaArchivesListView.HideSelection = false;
            this.VanillaArchivesListView.Location = new System.Drawing.Point(4, 3);
            this.VanillaArchivesListView.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.VanillaArchivesListView.Name = "VanillaArchivesListView";
            this.VanillaArchivesListView.Size = new System.Drawing.Size(604, 340);
            this.VanillaArchivesListView.SmallImageList = this.imageList1;
            this.VanillaArchivesListView.TabIndex = 0;
            this.VanillaArchivesListView.UseCompatibleStateImageBehavior = false;
            this.VanillaArchivesListView.View = System.Windows.Forms.View.Details;
            this.VanillaArchivesListView.SelectedIndexChanged += new System.EventHandler(this.ListView_SelectedIndexChanged);
            this.VanillaArchivesListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListView_MouseDoubleClick);
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
            this.StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel,
            this.StatusProgressBar});
            this.StatusBar.Location = new System.Drawing.Point(0, 398);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(620, 24);
            this.StatusBar.TabIndex = 2;
            this.StatusBar.Text = "StatusBar";
            // 
            // StatusLabel
            // 
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(486, 19);
            this.StatusLabel.Spring = true;
            this.StatusLabel.Text = "Ready";
            this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StatusProgressBar
            // 
            this.StatusProgressBar.Name = "StatusProgressBar";
            this.StatusProgressBar.Size = new System.Drawing.Size(117, 18);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 422);
            this.Controls.Add(this.TabContainer);
            this.Controls.Add(this.StatusBar);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "MainForm";
            this.Text = "BeatSaberKeeper";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.TabContainer.ResumeLayout(false);
            this.BackupTabPage.ResumeLayout(false);
            this.ArchiveContextMenuStrip.ResumeLayout(false);
            this.VanillaTabPage.ResumeLayout(false);
            this.StatusBar.ResumeLayout(false);
            this.StatusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.ToolStripMenuItem SongExplorerMenuItem;

        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem NewMenuItem;

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

        private System.Windows.Forms.ToolStripMenuItem ExitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutBeatSaberKeeperToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewSourceCodeOnGitHubToolStripMenuItem;

        private System.Windows.Forms.ImageList imageList1;

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.TabControl TabContainer;
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
        private System.Windows.Forms.ToolStripMenuItem DeleteMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RenameMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem CloneMenuItem;
        private System.Windows.Forms.ToolStripMenuItem UpdateMenuItem;
        private System.Windows.Forms.ToolStripMenuItem UnpackRunMenuItem;
        private System.Windows.Forms.ToolStripMenuItem UnpackMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem ShowInSystemExplorerMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PropertiesMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem SettingsMenu;
        private System.Windows.Forms.ToolStripMenuItem SetGameDirectoryMenuItem;
        private System.Windows.Forms.ToolStripTextBox GameDirectoryTextBox;
        private System.Windows.Forms.ToolStripMenuItem checkTheWebsiteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem whereAreMyArchivesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem PreReleaseMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CheckForUpdatesOnStartupMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ViewMenu;
        private System.Windows.Forms.ToolStripMenuItem RefreshMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem OpenDataDirectoryMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenGameDirectoryMenuItem;
        private System.Windows.Forms.ColumnHeader BackupArchiveVersionColumn;
        private System.Windows.Forms.ToolStripMenuItem ShowHistoryMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showHistoryToolStripMenuItem;
    }
}

