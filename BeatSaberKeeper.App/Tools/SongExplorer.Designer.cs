using System.ComponentModel;

namespace BeatSaberKeeper.App.Tools
{
    partial class SongExplorer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SongExplorer));
            this.SongExplorerFrame = new System.Windows.Forms.ToolStripContainer();
            this.SEStatusBar = new System.Windows.Forms.StatusStrip();
            this.SEStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.SEList = new System.Windows.Forms.ListView();
            this.SongNameColumn = new System.Windows.Forms.ColumnHeader();
            this.SongAuthorColumn = new System.Windows.Forms.ColumnHeader();
            this.LevelAuthorColumn = new System.Windows.Forms.ColumnHeader();
            this.LevelBpmColumn = new System.Windows.Forms.ColumnHeader();
            this.LevelDifficultiesColumn = new System.Windows.Forms.ColumnHeader();
            this.SEContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.PlaySongContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SEImageList = new System.Windows.Forms.ImageList(this.components);
            this.SEMainMenu = new System.Windows.Forms.MenuStrip();
            this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CloseMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RefreshMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SEToolBar = new System.Windows.Forms.ToolStrip();
            this.SEPlayerStopButton = new System.Windows.Forms.ToolStripButton();
            this.SEPlayerPauseButton = new System.Windows.Forms.ToolStripButton();
            this.SEPlayerTimeLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.SEPlayerVolumeDownButton = new System.Windows.Forms.ToolStripButton();
            this.SEPlayerVolumeDisplay = new System.Windows.Forms.ToolStripLabel();
            this.SEPlayerVolumeUpButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.SECurrentSongLabel = new System.Windows.Forms.ToolStripLabel();
            this.SEToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SEStatusUpdateTimer = new System.Timers.Timer();
            this.SongExplorerFrame.BottomToolStripPanel.SuspendLayout();
            this.SongExplorerFrame.ContentPanel.SuspendLayout();
            this.SongExplorerFrame.TopToolStripPanel.SuspendLayout();
            this.SongExplorerFrame.SuspendLayout();
            this.SEStatusBar.SuspendLayout();
            this.SEContextMenu.SuspendLayout();
            this.SEMainMenu.SuspendLayout();
            this.SEToolBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SEStatusUpdateTimer)).BeginInit();
            this.SuspendLayout();
            // 
            // SongExplorerFrame
            // 
            // 
            // SongExplorerFrame.BottomToolStripPanel
            // 
            this.SongExplorerFrame.BottomToolStripPanel.Controls.Add(this.SEStatusBar);
            // 
            // SongExplorerFrame.ContentPanel
            // 
            this.SongExplorerFrame.ContentPanel.Controls.Add(this.SEList);
            this.SongExplorerFrame.ContentPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SongExplorerFrame.ContentPanel.Size = new System.Drawing.Size(791, 387);
            this.SongExplorerFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SongExplorerFrame.Location = new System.Drawing.Point(0, 0);
            this.SongExplorerFrame.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SongExplorerFrame.Name = "SongExplorerFrame";
            this.SongExplorerFrame.Size = new System.Drawing.Size(791, 458);
            this.SongExplorerFrame.TabIndex = 0;
            this.SongExplorerFrame.Text = "toolStripContainer1";
            // 
            // SongExplorerFrame.TopToolStripPanel
            // 
            this.SongExplorerFrame.TopToolStripPanel.Controls.Add(this.SEMainMenu);
            this.SongExplorerFrame.TopToolStripPanel.Controls.Add(this.SEToolBar);
            // 
            // SEStatusBar
            // 
            this.SEStatusBar.Dock = System.Windows.Forms.DockStyle.None;
            this.SEStatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SEStatusLabel});
            this.SEStatusBar.Location = new System.Drawing.Point(0, 0);
            this.SEStatusBar.Name = "SEStatusBar";
            this.SEStatusBar.Size = new System.Drawing.Size(791, 22);
            this.SEStatusBar.TabIndex = 0;
            // 
            // SEStatusLabel
            // 
            this.SEStatusLabel.Name = "SEStatusLabel";
            this.SEStatusLabel.Size = new System.Drawing.Size(39, 17);
            this.SEStatusLabel.Text = "Ready";
            // 
            // SEList
            // 
            this.SEList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.SongNameColumn,
            this.SongAuthorColumn,
            this.LevelAuthorColumn,
            this.LevelBpmColumn,
            this.LevelDifficultiesColumn});
            this.SEList.ContextMenuStrip = this.SEContextMenu;
            this.SEList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SEList.FullRowSelect = true;
            this.SEList.GridLines = true;
            this.SEList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.SEList.HideSelection = false;
            this.SEList.Location = new System.Drawing.Point(0, 0);
            this.SEList.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SEList.Name = "SEList";
            this.SEList.ShowItemToolTips = true;
            this.SEList.Size = new System.Drawing.Size(791, 387);
            this.SEList.SmallImageList = this.SEImageList;
            this.SEList.TabIndex = 0;
            this.SEToolTip.SetToolTip(this.SEList, "A list of songs");
            this.SEList.UseCompatibleStateImageBehavior = false;
            this.SEList.View = System.Windows.Forms.View.Details;
            // 
            // SongNameColumn
            // 
            this.SongNameColumn.Text = "Song";
            this.SongNameColumn.Width = 250;
            // 
            // SongAuthorColumn
            // 
            this.SongAuthorColumn.Text = "Artist";
            this.SongAuthorColumn.Width = 150;
            // 
            // LevelAuthorColumn
            // 
            this.LevelAuthorColumn.Text = "Level Author";
            this.LevelAuthorColumn.Width = 150;
            // 
            // LevelBpmColumn
            // 
            this.LevelBpmColumn.Text = "BPM";
            this.LevelBpmColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.LevelBpmColumn.Width = 45;
            // 
            // LevelDifficultiesColumn
            // 
            this.LevelDifficultiesColumn.Text = "Difficulties";
            this.LevelDifficultiesColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // SEContextMenu
            // 
            this.SEContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PlaySongContextMenuItem});
            this.SEContextMenu.Name = "SEContextMenu";
            this.SEContextMenu.Size = new System.Drawing.Size(127, 26);
            this.SEContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.SEContextMenu_Opening);
            // 
            // PlaySongContextMenuItem
            // 
            this.PlaySongContextMenuItem.Name = "PlaySongContextMenuItem";
            this.PlaySongContextMenuItem.Size = new System.Drawing.Size(126, 22);
            this.PlaySongContextMenuItem.Text = "Play Song";
            this.PlaySongContextMenuItem.Click += new System.EventHandler(this.PlaySongContextMenuItem_Click);
            // 
            // SEImageList
            // 
            this.SEImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.SEImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("SEImageList.ImageStream")));
            this.SEImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.SEImageList.Images.SetKeyName(0, "stop");
            this.SEImageList.Images.SetKeyName(1, "play");
            this.SEImageList.Images.SetKeyName(2, "pause");
            this.SEImageList.Images.SetKeyName(3, "audio");
            // 
            // SEMainMenu
            // 
            this.SEMainMenu.Dock = System.Windows.Forms.DockStyle.None;
            this.SEMainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem,
            this.ViewMenuItem});
            this.SEMainMenu.Location = new System.Drawing.Point(0, 0);
            this.SEMainMenu.Name = "SEMainMenu";
            this.SEMainMenu.Size = new System.Drawing.Size(791, 24);
            this.SEMainMenu.TabIndex = 0;
            this.SEMainMenu.Text = "menuStrip1";
            // 
            // FileMenuItem
            // 
            this.FileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CloseMenuItem});
            this.FileMenuItem.Name = "FileMenuItem";
            this.FileMenuItem.Size = new System.Drawing.Size(37, 20);
            this.FileMenuItem.Text = "&File";
            // 
            // CloseMenuItem
            // 
            this.CloseMenuItem.Name = "CloseMenuItem";
            this.CloseMenuItem.Size = new System.Drawing.Size(103, 22);
            this.CloseMenuItem.Text = "&Close";
            this.CloseMenuItem.Click += new System.EventHandler(this.CloseMenuItem_Click);
            // 
            // ViewMenuItem
            // 
            this.ViewMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RefreshMenuItem});
            this.ViewMenuItem.Name = "ViewMenuItem";
            this.ViewMenuItem.Size = new System.Drawing.Size(44, 20);
            this.ViewMenuItem.Text = "&View";
            // 
            // RefreshMenuItem
            // 
            this.RefreshMenuItem.Name = "RefreshMenuItem";
            this.RefreshMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.RefreshMenuItem.Size = new System.Drawing.Size(132, 22);
            this.RefreshMenuItem.Text = "Refresh";
            this.RefreshMenuItem.Click += new System.EventHandler(this.RefreshMenuItem_Click);
            // 
            // SEToolBar
            // 
            this.SEToolBar.Dock = System.Windows.Forms.DockStyle.None;
            this.SEToolBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.SEToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SEPlayerStopButton,
            this.SEPlayerPauseButton,
            this.SEPlayerTimeLabel,
            this.toolStripSeparator1,
            this.SEPlayerVolumeDownButton,
            this.SEPlayerVolumeDisplay,
            this.SEPlayerVolumeUpButton,
            this.toolStripSeparator2,
            this.SECurrentSongLabel});
            this.SEToolBar.Location = new System.Drawing.Point(3, 24);
            this.SEToolBar.Name = "SEToolBar";
            this.SEToolBar.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.SEToolBar.Size = new System.Drawing.Size(310, 25);
            this.SEToolBar.TabIndex = 1;
            // 
            // SEPlayerStopButton
            // 
            this.SEPlayerStopButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SEPlayerStopButton.Image = global::BeatSaberKeeper.App.Properties.Resources.stop;
            this.SEPlayerStopButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SEPlayerStopButton.Name = "SEPlayerStopButton";
            this.SEPlayerStopButton.Size = new System.Drawing.Size(23, 22);
            this.SEPlayerStopButton.Text = "Stop";
            this.SEPlayerStopButton.Click += new System.EventHandler(this.SEPlayerStopButton_Click);
            // 
            // SEPlayerPauseButton
            // 
            this.SEPlayerPauseButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SEPlayerPauseButton.Image = global::BeatSaberKeeper.App.Properties.Resources.play;
            this.SEPlayerPauseButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SEPlayerPauseButton.Name = "SEPlayerPauseButton";
            this.SEPlayerPauseButton.Size = new System.Drawing.Size(23, 22);
            this.SEPlayerPauseButton.Text = "Play / Pause";
            this.SEPlayerPauseButton.Click += new System.EventHandler(this.SEPlayerPauseButton_Click);
            // 
            // SEPlayerTimeLabel
            // 
            this.SEPlayerTimeLabel.Name = "SEPlayerTimeLabel";
            this.SEPlayerTimeLabel.Size = new System.Drawing.Size(43, 22);
            this.SEPlayerTimeLabel.Text = "0:00:00";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // SEPlayerVolumeDownButton
            // 
            this.SEPlayerVolumeDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.SEPlayerVolumeDownButton.Image = ((System.Drawing.Image)(resources.GetObject("SEPlayerVolumeDownButton.Image")));
            this.SEPlayerVolumeDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SEPlayerVolumeDownButton.Name = "SEPlayerVolumeDownButton";
            this.SEPlayerVolumeDownButton.Size = new System.Drawing.Size(35, 22);
            this.SEPlayerVolumeDownButton.Text = "Vol -";
            this.SEPlayerVolumeDownButton.ToolTipText = "Decrease Volume";
            this.SEPlayerVolumeDownButton.Click += new System.EventHandler(this.SEPlayerVolumeDownButton_Click);
            // 
            // SEPlayerVolumeDisplay
            // 
            this.SEPlayerVolumeDisplay.Name = "SEPlayerVolumeDisplay";
            this.SEPlayerVolumeDisplay.Size = new System.Drawing.Size(72, 22);
            this.SEPlayerVolumeDisplay.Text = "Volume: 0 %";
            // 
            // SEPlayerVolumeUpButton
            // 
            this.SEPlayerVolumeUpButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.SEPlayerVolumeUpButton.Image = ((System.Drawing.Image)(resources.GetObject("SEPlayerVolumeUpButton.Image")));
            this.SEPlayerVolumeUpButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SEPlayerVolumeUpButton.Name = "SEPlayerVolumeUpButton";
            this.SEPlayerVolumeUpButton.Size = new System.Drawing.Size(38, 22);
            this.SEPlayerVolumeUpButton.Text = "Vol +";
            this.SEPlayerVolumeUpButton.ToolTipText = "Increase Volume";
            this.SEPlayerVolumeUpButton.Click += new System.EventHandler(this.SEPlayerVolumeUpButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // SECurrentSongLabel
            // 
            this.SECurrentSongLabel.Name = "SECurrentSongLabel";
            this.SECurrentSongLabel.Size = new System.Drawing.Size(61, 22);
            this.SECurrentSongLabel.Text = "< Ready >";
            // 
            // SEStatusUpdateTimer
            // 
            this.SEStatusUpdateTimer.Enabled = true;
            this.SEStatusUpdateTimer.Interval = 499D;
            this.SEStatusUpdateTimer.SynchronizingObject = this;
            this.SEStatusUpdateTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.SEStatusUpdateTimer_Elapsed);
            // 
            // SongExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(791, 458);
            this.Controls.Add(this.SongExplorerFrame);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.SEMainMenu;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "SongExplorer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BSK Song Explorer";
            this.Load += new System.EventHandler(this.SongExplorer_Load);
            this.SongExplorerFrame.BottomToolStripPanel.ResumeLayout(false);
            this.SongExplorerFrame.BottomToolStripPanel.PerformLayout();
            this.SongExplorerFrame.ContentPanel.ResumeLayout(false);
            this.SongExplorerFrame.TopToolStripPanel.ResumeLayout(false);
            this.SongExplorerFrame.TopToolStripPanel.PerformLayout();
            this.SongExplorerFrame.ResumeLayout(false);
            this.SongExplorerFrame.PerformLayout();
            this.SEStatusBar.ResumeLayout(false);
            this.SEStatusBar.PerformLayout();
            this.SEContextMenu.ResumeLayout(false);
            this.SEMainMenu.ResumeLayout(false);
            this.SEMainMenu.PerformLayout();
            this.SEToolBar.ResumeLayout(false);
            this.SEToolBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SEStatusUpdateTimer)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.ImageList SEImageList;

        private System.Windows.Forms.ToolStripLabel SEPlayerTimeLabel;

        private System.Windows.Forms.ToolStripButton SEPlayerStopButton;
        private System.Windows.Forms.ToolStripButton SEPlayerPauseButton;

        private System.Timers.Timer SEStatusUpdateTimer;

        private System.Windows.Forms.ContextMenuStrip SEContextMenu;
        private System.Windows.Forms.ToolStripMenuItem PlaySongContextMenuItem;

        private System.Windows.Forms.ToolTip SEToolTip;

        private System.Windows.Forms.ColumnHeader LevelBpmColumn;
        private System.Windows.Forms.ColumnHeader SongNameColumn;
        private System.Windows.Forms.ColumnHeader SongAuthorColumn;
        private System.Windows.Forms.ColumnHeader LevelAuthorColumn;
        private System.Windows.Forms.ColumnHeader LevelDifficultiesColumn;
        private System.Windows.Forms.ToolStripMenuItem ViewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RefreshMenuItem;

        private System.Windows.Forms.ListView SEList;

        private System.Windows.Forms.ToolStripStatusLabel SEStatusLabel;
        private System.Windows.Forms.StatusStrip SEStatusBar;
        private System.Windows.Forms.MenuStrip SEMainMenu;
        private System.Windows.Forms.ToolStripMenuItem FileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CloseMenuItem;

        private System.Windows.Forms.ToolStrip SEToolBar;

        private System.Windows.Forms.ToolStripContainer SongExplorerFrame;

        #endregion

        private System.Windows.Forms.ToolStripLabel SECurrentSongLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton SEPlayerVolumeDownButton;
        private System.Windows.Forms.ToolStripButton SEPlayerVolumeUpButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel SEPlayerVolumeDisplay;
    }
}