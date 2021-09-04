using System.ComponentModel;

namespace BeatSaberKeeper.App.Tools
{
    partial class HistoryExplorer
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
            this.VersionListView = new System.Windows.Forms.ListView();
            this.ArchiveDateColumn = new System.Windows.Forms.ColumnHeader();
            this.NoFilesColumn = new System.Windows.Forms.ColumnHeader();
            this.FileListView = new System.Windows.Forms.ListView();
            this.FileNameColumn = new System.Windows.Forms.ColumnHeader();
            this.FileSizeColumn = new System.Windows.Forms.ColumnHeader();
            this.FileHashColumn = new System.Windows.Forms.ColumnHeader();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // VersionListView
            // 
            this.VersionListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ArchiveDateColumn,
            this.NoFilesColumn});
            this.VersionListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VersionListView.FullRowSelect = true;
            this.VersionListView.HideSelection = false;
            this.VersionListView.Location = new System.Drawing.Point(3, 3);
            this.VersionListView.Name = "VersionListView";
            this.VersionListView.Size = new System.Drawing.Size(244, 475);
            this.VersionListView.TabIndex = 0;
            this.VersionListView.UseCompatibleStateImageBehavior = false;
            this.VersionListView.View = System.Windows.Forms.View.Details;
            this.VersionListView.SelectedIndexChanged += new System.EventHandler(this.VersionListView_SelectedIndexChanged);
            // 
            // ArchiveDateColumn
            // 
            this.ArchiveDateColumn.Text = "Packed at";
            this.ArchiveDateColumn.Width = 140;
            // 
            // NoFilesColumn
            // 
            this.NoFilesColumn.Text = "# Files";
            // 
            // FileListView
            // 
            this.FileListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.FileNameColumn,
            this.FileSizeColumn,
            this.FileHashColumn});
            this.FileListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FileListView.FullRowSelect = true;
            this.FileListView.HideSelection = false;
            this.FileListView.Location = new System.Drawing.Point(253, 3);
            this.FileListView.Name = "FileListView";
            this.FileListView.Size = new System.Drawing.Size(601, 475);
            this.FileListView.TabIndex = 0;
            this.FileListView.UseCompatibleStateImageBehavior = false;
            this.FileListView.View = System.Windows.Forms.View.Details;
            // 
            // FileNameColumn
            // 
            this.FileNameColumn.Text = "File Name";
            this.FileNameColumn.Width = 250;
            // 
            // FileSizeColumn
            // 
            this.FileSizeColumn.Text = "Size";
            this.FileSizeColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.FileSizeColumn.Width = 100;
            // 
            // FileHashColumn
            // 
            this.FileHashColumn.Text = "Hash";
            this.FileHashColumn.Width = 100;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.VersionListView, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.FileListView, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(857, 481);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // HistoryExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 481);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "HistoryExplorer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "History Explorer";
            this.Load += new System.EventHandler(this.HistoryExplorer_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListView VersionListView;
        private System.Windows.Forms.ColumnHeader ArchiveDateColumn;
        private System.Windows.Forms.ColumnHeader NoFilesColumn;
        private System.Windows.Forms.ListView FileListView;
        private System.Windows.Forms.ColumnHeader FileNameColumn;
        private System.Windows.Forms.ColumnHeader FileSizeColumn;
        private System.Windows.Forms.ColumnHeader FileHashColumn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}