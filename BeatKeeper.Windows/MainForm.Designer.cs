using BeatKeeper.Windows.Utils;
using BrightIdeasSoftware;

namespace BeatKeeper.Windows
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ArtifactListView = new BrightIdeasSoftware.ObjectListView();
            this.ArtifactNameColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.GameVersionColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.ArtifactSizeColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.ArtifactTypeColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.ArtifactContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ArtifactImageList = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadVanillaGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadModManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modAssistantToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.beatDropToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.archivesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createArchiveFromCurrentStateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.initializeSteamCMDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loginToSteamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.sourceCodeOnGitHubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wikiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutBeatKeeperToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.ArtifactListView)).BeginInit();
            this.ArtifactContextMenu.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ArtifactListView
            // 
            this.ArtifactListView.AllColumns.Add(this.ArtifactNameColumn);
            this.ArtifactListView.AllColumns.Add(this.GameVersionColumn);
            this.ArtifactListView.AllColumns.Add(this.ArtifactSizeColumn);
            this.ArtifactListView.AllColumns.Add(this.ArtifactTypeColumn);
            this.ArtifactListView.CellEditUseWholeCell = false;
            this.ArtifactListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ArtifactNameColumn,
            this.GameVersionColumn,
            this.ArtifactSizeColumn,
            this.ArtifactTypeColumn});
            this.ArtifactListView.ContextMenuStrip = this.ArtifactContextMenu;
            this.ArtifactListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.ArtifactListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ArtifactListView.EmptyListMsg = "No archives were found";
            this.ArtifactListView.FullRowSelect = true;
            this.ArtifactListView.GridLines = true;
            this.ArtifactListView.HideSelection = false;
            this.ArtifactListView.Location = new System.Drawing.Point(0, 24);
            this.ArtifactListView.Name = "ArtifactListView";
            this.ArtifactListView.Size = new System.Drawing.Size(784, 515);
            this.ArtifactListView.SmallImageList = this.ArtifactImageList;
            this.ArtifactListView.TabIndex = 0;
            this.ArtifactListView.UseCompatibleStateImageBehavior = false;
            this.ArtifactListView.View = System.Windows.Forms.View.Details;
            this.ArtifactListView.SelectedIndexChanged += new System.EventHandler(this.ArtifactListView_SelectedIndexChanged);
            this.ArtifactListView.DoubleClick += new System.EventHandler(this.ArtifactListView_DoubleClick);
            // 
            // ArtifactNameColumn
            // 
            this.ArtifactNameColumn.AspectName = "Name";
            this.ArtifactNameColumn.FillsFreeSpace = true;
            this.ArtifactNameColumn.ImageAspectName = "Type";
            this.ArtifactNameColumn.Text = "Name";
            this.ArtifactNameColumn.UseInitialLetterForGroup = true;
            this.ArtifactNameColumn.Width = 400;
            // 
            // GameVersionColumn
            // 
            this.GameVersionColumn.AspectName = "GameVersion";
            this.GameVersionColumn.Text = "Game Version";
            this.GameVersionColumn.Width = 100;
            // 
            // ArtifactSizeColumn
            // 
            this.ArtifactSizeColumn.AspectName = "HumanReadableSize";
            this.ArtifactSizeColumn.Groupable = false;
            this.ArtifactSizeColumn.Text = "Size";
            this.ArtifactSizeColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ArtifactSizeColumn.Width = 100;
            // 
            // ArtifactTypeColumn
            // 
            this.ArtifactTypeColumn.AspectName = "Type";
            this.ArtifactTypeColumn.Text = "Type";
            this.ArtifactTypeColumn.Width = 100;
            // 
            // ArtifactContextMenu
            // 
            this.ArtifactContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.toolStripMenuItem2,
            this.deleteToolStripMenuItem});
            this.ArtifactContextMenu.Name = "ArtifactContextMenu";
            this.ArtifactContextMenu.Size = new System.Drawing.Size(108, 54);
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Enabled = false;
            this.startToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(104, 6);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // ArtifactImageList
            // 
            this.ArtifactImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ArtifactImageList.ImageStream")));
            this.ArtifactImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.ArtifactImageList.Images.SetKeyName(0, "Vanilla");
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.gameToolStripMenuItem,
            this.modsToolStripMenuItem,
            this.archivesToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // gameToolStripMenuItem
            // 
            this.gameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.downloadVanillaGameToolStripMenuItem});
            this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            this.gameToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.gameToolStripMenuItem.Text = "&Game";
            // 
            // downloadVanillaGameToolStripMenuItem
            // 
            this.downloadVanillaGameToolStripMenuItem.Name = "downloadVanillaGameToolStripMenuItem";
            this.downloadVanillaGameToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.downloadVanillaGameToolStripMenuItem.Text = "Download Vanilla Game";
            this.downloadVanillaGameToolStripMenuItem.Click += new System.EventHandler(this.downloadVanillaGameToolStripMenuItem_Click);
            // 
            // modsToolStripMenuItem
            // 
            this.modsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.downloadModManagerToolStripMenuItem});
            this.modsToolStripMenuItem.Name = "modsToolStripMenuItem";
            this.modsToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.modsToolStripMenuItem.Text = "&Mods";
            // 
            // downloadModManagerToolStripMenuItem
            // 
            this.downloadModManagerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modAssistantToolStripMenuItem,
            this.beatDropToolStripMenuItem});
            this.downloadModManagerToolStripMenuItem.Name = "downloadModManagerToolStripMenuItem";
            this.downloadModManagerToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.downloadModManagerToolStripMenuItem.Text = "Download Mod Manager";
            // 
            // modAssistantToolStripMenuItem
            // 
            this.modAssistantToolStripMenuItem.Name = "modAssistantToolStripMenuItem";
            this.modAssistantToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.modAssistantToolStripMenuItem.Tag = "https://github.com/Assistant/ModAssistant";
            this.modAssistantToolStripMenuItem.Text = "ModAssistant";
            this.modAssistantToolStripMenuItem.Click += new System.EventHandler(this.openUrlToolStripMenuItem_Click);
            // 
            // beatDropToolStripMenuItem
            // 
            this.beatDropToolStripMenuItem.Name = "beatDropToolStripMenuItem";
            this.beatDropToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.beatDropToolStripMenuItem.Tag = "https://bsaber.com/beatdrop/";
            this.beatDropToolStripMenuItem.Text = "BeatDrop 2";
            this.beatDropToolStripMenuItem.Click += new System.EventHandler(this.openUrlToolStripMenuItem_Click);
            // 
            // archivesToolStripMenuItem
            // 
            this.archivesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createArchiveFromCurrentStateToolStripMenuItem});
            this.archivesToolStripMenuItem.Name = "archivesToolStripMenuItem";
            this.archivesToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.archivesToolStripMenuItem.Text = "&Archives";
            // 
            // createArchiveFromCurrentStateToolStripMenuItem
            // 
            this.createArchiveFromCurrentStateToolStripMenuItem.Name = "createArchiveFromCurrentStateToolStripMenuItem";
            this.createArchiveFromCurrentStateToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.createArchiveFromCurrentStateToolStripMenuItem.Text = "&Create Archive from current state";
            this.createArchiveFromCurrentStateToolStripMenuItem.Click += new System.EventHandler(this.createArchiveFromCurrentStateToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.initializeSteamCMDToolStripMenuItem,
            this.loginToSteamToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+,";
            this.settingsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Oemcomma)));
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.settingsToolStripMenuItem.Text = "&Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // initializeSteamCMDToolStripMenuItem
            // 
            this.initializeSteamCMDToolStripMenuItem.Name = "initializeSteamCMDToolStripMenuItem";
            this.initializeSteamCMDToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.initializeSteamCMDToolStripMenuItem.Text = "Initialize Steam&CMD";
            this.initializeSteamCMDToolStripMenuItem.Click += new System.EventHandler(this.initializeSteamCMDToolStripMenuItem_Click);
            // 
            // loginToSteamToolStripMenuItem
            // 
            this.loginToSteamToolStripMenuItem.Name = "loginToSteamToolStripMenuItem";
            this.loginToSteamToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.loginToSteamToolStripMenuItem.Text = "Login to Steam";
            this.loginToSteamToolStripMenuItem.Click += new System.EventHandler(this.loginToSteamToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sourceCodeOnGitHubToolStripMenuItem,
            this.wikiToolStripMenuItem,
            this.toolStripMenuItem3,
            this.checkForUpdatesToolStripMenuItem,
            this.aboutBeatKeeperToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(24, 20);
            this.toolStripMenuItem1.Text = "&?";
            // 
            // sourceCodeOnGitHubToolStripMenuItem
            // 
            this.sourceCodeOnGitHubToolStripMenuItem.Name = "sourceCodeOnGitHubToolStripMenuItem";
            this.sourceCodeOnGitHubToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.sourceCodeOnGitHubToolStripMenuItem.Tag = "https://github.com/rGunti/BeatSaberKeeper";
            this.sourceCodeOnGitHubToolStripMenuItem.Text = "Source &Code on GitHub";
            this.sourceCodeOnGitHubToolStripMenuItem.Click += new System.EventHandler(this.openUrlToolStripMenuItem_Click);
            // 
            // wikiToolStripMenuItem
            // 
            this.wikiToolStripMenuItem.Name = "wikiToolStripMenuItem";
            this.wikiToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.wikiToolStripMenuItem.Tag = "https://github.com/rGunti/BeatSaberKeeper/wiki";
            this.wikiToolStripMenuItem.Text = "Wiki";
            this.wikiToolStripMenuItem.Click += new System.EventHandler(this.openUrlToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(196, 6);
            // 
            // aboutBeatKeeperToolStripMenuItem
            // 
            this.aboutBeatKeeperToolStripMenuItem.Name = "aboutBeatKeeperToolStripMenuItem";
            this.aboutBeatKeeperToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.aboutBeatKeeperToolStripMenuItem.Text = "&About BeatSaberKeeper";
            this.aboutBeatKeeperToolStripMenuItem.Click += new System.EventHandler(this.aboutBeatKeeperToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 539);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusLabel
            // 
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(39, 17);
            this.StatusLabel.Text = "Ready";
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.checkForUpdatesToolStripMenuItem.Text = "Check for &Updates";
            this.checkForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdatesToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.ArtifactListView);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BeatSaberKeeper - Keep your BeatSaber versions!";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ArtifactListView)).EndInit();
            this.ArtifactContextMenu.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ObjectListView ArtifactListView;
        private OLVColumn ArtifactNameColumn;
        private OLVColumn GameVersionColumn;
        private OLVColumn ArtifactSizeColumn;
        private OLVColumn ArtifactTypeColumn;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadVanillaGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem archivesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutBeatKeeperToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem initializeSteamCMDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loginToSteamToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.ContextMenuStrip ArtifactContextMenu;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ImageList ArtifactImageList;
        private System.Windows.Forms.ToolStripMenuItem createArchiveFromCurrentStateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sourceCodeOnGitHubToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wikiToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem downloadModManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modAssistantToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem beatDropToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
    }
}

