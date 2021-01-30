
namespace DirScan.Client
{
    partial class PreferenceForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbLogger = new System.Windows.Forms.GroupBox();
            this.rbSqlLogger = new System.Windows.Forms.RadioButton();
            this.rbFileLogger = new System.Windows.Forms.RadioButton();
            this.txtConnectionString = new System.Windows.Forms.TextBox();
            this.lblConnectionString = new System.Windows.Forms.Label();
            this.gbSqlLoggerConfig = new System.Windows.Forms.GroupBox();
            this.btnTableSqlClipboard = new System.Windows.Forms.Button();
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkLogDirectories = new System.Windows.Forms.CheckBox();
            this.gbLogger.SuspendLayout();
            this.gbSqlLoggerConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbLogger
            // 
            this.gbLogger.Controls.Add(this.rbSqlLogger);
            this.gbLogger.Controls.Add(this.rbFileLogger);
            this.gbLogger.Location = new System.Drawing.Point(12, 12);
            this.gbLogger.Name = "gbLogger";
            this.gbLogger.Size = new System.Drawing.Size(167, 74);
            this.gbLogger.TabIndex = 0;
            this.gbLogger.TabStop = false;
            this.gbLogger.Text = "Logger";
            // 
            // rbSqlLogger
            // 
            this.rbSqlLogger.AutoSize = true;
            this.rbSqlLogger.Location = new System.Drawing.Point(27, 43);
            this.rbSqlLogger.Name = "rbSqlLogger";
            this.rbSqlLogger.Size = new System.Drawing.Size(116, 17);
            this.rbSqlLogger.TabIndex = 1;
            this.rbSqlLogger.TabStop = true;
            this.rbSqlLogger.Text = "SQL Server Logger";
            this.rbSqlLogger.UseVisualStyleBackColor = true;
            this.rbSqlLogger.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // rbFileLogger
            // 
            this.rbFileLogger.AutoSize = true;
            this.rbFileLogger.Location = new System.Drawing.Point(27, 20);
            this.rbFileLogger.Name = "rbFileLogger";
            this.rbFileLogger.Size = new System.Drawing.Size(74, 17);
            this.rbFileLogger.TabIndex = 0;
            this.rbFileLogger.TabStop = true;
            this.rbFileLogger.Text = "FileLogger";
            this.rbFileLogger.UseVisualStyleBackColor = true;
            this.rbFileLogger.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConnectionString.Location = new System.Drawing.Point(100, 22);
            this.txtConnectionString.Multiline = true;
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Size = new System.Drawing.Size(334, 65);
            this.txtConnectionString.TabIndex = 1;
            this.txtConnectionString.TextChanged += new System.EventHandler(this.txtConnectionString_TextChanged);
            // 
            // lblConnectionString
            // 
            this.lblConnectionString.AutoSize = true;
            this.lblConnectionString.Location = new System.Drawing.Point(6, 22);
            this.lblConnectionString.Name = "lblConnectionString";
            this.lblConnectionString.Size = new System.Drawing.Size(91, 13);
            this.lblConnectionString.TabIndex = 2;
            this.lblConnectionString.Text = "Connection String";
            this.lblConnectionString.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // gbSqlLoggerConfig
            // 
            this.gbSqlLoggerConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSqlLoggerConfig.Controls.Add(this.btnTableSqlClipboard);
            this.gbSqlLoggerConfig.Controls.Add(this.btnTestConnection);
            this.gbSqlLoggerConfig.Controls.Add(this.lblConnectionString);
            this.gbSqlLoggerConfig.Controls.Add(this.txtConnectionString);
            this.gbSqlLoggerConfig.Location = new System.Drawing.Point(12, 92);
            this.gbSqlLoggerConfig.Name = "gbSqlLoggerConfig";
            this.gbSqlLoggerConfig.Size = new System.Drawing.Size(440, 122);
            this.gbSqlLoggerConfig.TabIndex = 3;
            this.gbSqlLoggerConfig.TabStop = false;
            this.gbSqlLoggerConfig.Text = "SQL Logger Configuration";
            // 
            // btnTableSqlClipboard
            // 
            this.btnTableSqlClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTableSqlClipboard.Location = new System.Drawing.Point(6, 93);
            this.btnTableSqlClipboard.Name = "btnTableSqlClipboard";
            this.btnTableSqlClipboard.Size = new System.Drawing.Size(428, 23);
            this.btnTableSqlClipboard.TabIndex = 6;
            this.btnTableSqlClipboard.Text = "Put Sql Required To Create Logging Table On Clipboard";
            this.btnTableSqlClipboard.UseVisualStyleBackColor = true;
            this.btnTableSqlClipboard.Click += new System.EventHandler(this.btnTableSqlClipboard_Click);
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.Location = new System.Drawing.Point(6, 47);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(88, 41);
            this.btnTestConnection.TabIndex = 5;
            this.btnTestConnection.Text = "Test Connection";
            this.btnTestConnection.UseVisualStyleBackColor = true;
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(298, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(380, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // chkLogDirectories
            // 
            this.chkLogDirectories.AutoSize = true;
            this.chkLogDirectories.Location = new System.Drawing.Point(287, 69);
            this.chkLogDirectories.Name = "chkLogDirectories";
            this.chkLogDirectories.Size = new System.Drawing.Size(159, 17);
            this.chkLogDirectories.TabIndex = 6;
            this.chkLogDirectories.Text = "Log Directories During Scan";
            this.chkLogDirectories.UseVisualStyleBackColor = true;
            // 
            // PreferenceForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(464, 221);
            this.Controls.Add(this.chkLogDirectories);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.gbSqlLoggerConfig);
            this.Controls.Add(this.gbLogger);
            this.MinimumSize = new System.Drawing.Size(480, 260);
            this.Name = "PreferenceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Preferences";
            this.gbLogger.ResumeLayout(false);
            this.gbLogger.PerformLayout();
            this.gbSqlLoggerConfig.ResumeLayout(false);
            this.gbSqlLoggerConfig.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbLogger;
        public System.Windows.Forms.RadioButton rbSqlLogger;
        public System.Windows.Forms.RadioButton rbFileLogger;
        public System.Windows.Forms.TextBox txtConnectionString;
        private System.Windows.Forms.Label lblConnectionString;
        public System.Windows.Forms.GroupBox gbSqlLoggerConfig;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnTableSqlClipboard;
        private System.Windows.Forms.Button btnTestConnection;
        private System.Windows.Forms.CheckBox chkLogDirectories;
    }
}