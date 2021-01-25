
namespace DirScan.Client
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
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.lblSelectedFolder = new System.Windows.Forms.Label();
            this.btnScanStats = new System.Windows.Forms.Button();
            this.lvStats = new System.Windows.Forms.ListView();
            this.colDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colScanValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.status = new System.Windows.Forms.StatusStrip();
            this.statusMessage = new DirScan.Client.BindableToolStripStatusLabel();
            this.progress = new System.Windows.Forms.ToolStripProgressBar();
            this.statusVersion = new DirScan.Client.BindableToolStripStatusLabel();
            this.lvFileTypes = new System.Windows.Forms.ListView();
            this.colType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblFileTypes = new System.Windows.Forms.Label();
            this.btnOpenLogFile = new System.Windows.Forms.Button();
            this.status.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnSelectFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectFolder.Location = new System.Drawing.Point(3, 3);
            this.btnSelectFolder.Margin = new System.Windows.Forms.Padding(2);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(143, 34);
            this.btnSelectFolder.TabIndex = 0;
            this.btnSelectFolder.Text = "Select Folder";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // lblSelectedFolder
            // 
            this.lblSelectedFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSelectedFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedFolder.Location = new System.Drawing.Point(150, 3);
            this.lblSelectedFolder.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSelectedFolder.Name = "lblSelectedFolder";
            this.lblSelectedFolder.Size = new System.Drawing.Size(473, 34);
            this.lblSelectedFolder.TabIndex = 1;
            this.lblSelectedFolder.Text = "Folder Selected";
            this.lblSelectedFolder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnScanStats
            // 
            this.btnScanStats.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnScanStats.Location = new System.Drawing.Point(3, 50);
            this.btnScanStats.Margin = new System.Windows.Forms.Padding(2);
            this.btnScanStats.Name = "btnScanStats";
            this.btnScanStats.Size = new System.Drawing.Size(143, 34);
            this.btnScanStats.TabIndex = 2;
            this.btnScanStats.Text = "Scan Statistics";
            this.btnScanStats.UseVisualStyleBackColor = true;
            this.btnScanStats.Click += new System.EventHandler(this.btnScanStats_Click);
            // 
            // lvStats
            // 
            this.lvStats.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvStats.AutoArrange = false;
            this.lvStats.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colDescription,
            this.colScanValue});
            this.lvStats.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvStats.GridLines = true;
            this.lvStats.HideSelection = false;
            this.lvStats.Location = new System.Drawing.Point(150, 50);
            this.lvStats.Margin = new System.Windows.Forms.Padding(2);
            this.lvStats.Name = "lvStats";
            this.lvStats.Size = new System.Drawing.Size(473, 130);
            this.lvStats.TabIndex = 3;
            this.lvStats.UseCompatibleStateImageBehavior = false;
            this.lvStats.View = System.Windows.Forms.View.Details;
            // 
            // colDescription
            // 
            this.colDescription.Text = "Description";
            this.colDescription.Width = 200;
            // 
            // colScanValue
            // 
            this.colScanValue.Text = "Value";
            this.colScanValue.Width = 200;
            // 
            // status
            // 
            this.status.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusMessage,
            this.progress,
            this.statusVersion});
            this.status.Location = new System.Drawing.Point(0, 567);
            this.status.Name = "status";
            this.status.Padding = new System.Windows.Forms.Padding(1, 0, 9, 0);
            this.status.Size = new System.Drawing.Size(627, 28);
            this.status.TabIndex = 4;
            this.status.Resize += new System.EventHandler(this.status_Resize);
            // 
            // statusMessage
            // 
            this.statusMessage.AutoSize = false;
            this.statusMessage.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusMessage.Name = "statusMessage";
            this.statusMessage.Size = new System.Drawing.Size(300, 23);
            this.statusMessage.Text = "Message";
            this.statusMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // progress
            // 
            this.progress.AutoSize = false;
            this.progress.MarqueeAnimationSpeed = 0;
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(200, 22);
            // 
            // statusVersion
            // 
            this.statusVersion.AutoSize = false;
            this.statusVersion.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusVersion.Name = "statusVersion";
            this.statusVersion.Size = new System.Drawing.Size(100, 23);
            this.statusVersion.Text = "Version";
            this.statusVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lvFileTypes
            // 
            this.lvFileTypes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvFileTypes.AutoArrange = false;
            this.lvFileTypes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colType,
            this.colSize});
            this.lvFileTypes.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvFileTypes.GridLines = true;
            this.lvFileTypes.HideSelection = false;
            this.lvFileTypes.Location = new System.Drawing.Point(150, 220);
            this.lvFileTypes.Margin = new System.Windows.Forms.Padding(2);
            this.lvFileTypes.Name = "lvFileTypes";
            this.lvFileTypes.Size = new System.Drawing.Size(473, 339);
            this.lvFileTypes.TabIndex = 5;
            this.lvFileTypes.UseCompatibleStateImageBehavior = false;
            this.lvFileTypes.View = System.Windows.Forms.View.Details;
            // 
            // colType
            // 
            this.colType.Text = "Type";
            this.colType.Width = 200;
            // 
            // colSize
            // 
            this.colSize.Text = "Size";
            this.colSize.Width = 200;
            // 
            // lblFileTypes
            // 
            this.lblFileTypes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFileTypes.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileTypes.Location = new System.Drawing.Point(149, 182);
            this.lblFileTypes.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFileTypes.Name = "lblFileTypes";
            this.lblFileTypes.Size = new System.Drawing.Size(473, 34);
            this.lblFileTypes.TabIndex = 6;
            this.lblFileTypes.Text = "File Types";
            this.lblFileTypes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnOpenLogFile
            // 
            this.btnOpenLogFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOpenLogFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenLogFile.Location = new System.Drawing.Point(3, 525);
            this.btnOpenLogFile.Margin = new System.Windows.Forms.Padding(2);
            this.btnOpenLogFile.Name = "btnOpenLogFile";
            this.btnOpenLogFile.Size = new System.Drawing.Size(143, 34);
            this.btnOpenLogFile.TabIndex = 7;
            this.btnOpenLogFile.Text = "Open Log File";
            this.btnOpenLogFile.UseVisualStyleBackColor = true;
            this.btnOpenLogFile.Click += new System.EventHandler(this.btnOpenLogFile_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 595);
            this.Controls.Add(this.btnOpenLogFile);
            this.Controls.Add(this.lblFileTypes);
            this.Controls.Add(this.lvFileTypes);
            this.Controls.Add(this.status);
            this.Controls.Add(this.lvStats);
            this.Controls.Add(this.btnScanStats);
            this.Controls.Add(this.lblSelectedFolder);
            this.Controls.Add(this.btnSelectFolder);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DirScan";
            this.status.ResumeLayout(false);
            this.status.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.Label lblSelectedFolder;
        private System.Windows.Forms.Button btnScanStats;
        private System.Windows.Forms.ColumnHeader colDescription;
        private System.Windows.Forms.ColumnHeader colScanValue;
        public System.Windows.Forms.ListView lvStats;
        private System.Windows.Forms.StatusStrip status;
        private BindableToolStripStatusLabel statusMessage;
        private System.Windows.Forms.ToolStripProgressBar progress;
        private BindableToolStripStatusLabel statusVersion;
        public System.Windows.Forms.ListView lvFileTypes;
        private System.Windows.Forms.ColumnHeader colType;
        private System.Windows.Forms.ColumnHeader colSize;
        private System.Windows.Forms.Label lblFileTypes;
        public System.Windows.Forms.Button btnOpenLogFile;
    }
}

