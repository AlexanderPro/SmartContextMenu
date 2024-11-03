namespace SmartContextMenu.Forms
{
    partial class StartProgramForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartProgramForm));
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblFileName = new System.Windows.Forms.Label();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.lblArguments = new System.Windows.Forms.Label();
            this.txtArguments = new System.Windows.Forms.TextBox();
            this.btnBrowseFile = new System.Windows.Forms.Button();
            this.lblBegin = new System.Windows.Forms.Label();
            this.txtBegin = new System.Windows.Forms.TextBox();
            this.lblEnd = new System.Windows.Forms.Label();
            this.txtEnd = new System.Windows.Forms.TextBox();
            this.txtParameter = new System.Windows.Forms.TextBox();
            this.chkShowWindow = new System.Windows.Forms.CheckBox();
            this.chkUseWindowWorkingDirectory = new System.Windows.Forms.CheckBox();
            this.lblKey3 = new System.Windows.Forms.Label();
            this.lblKey2 = new System.Windows.Forms.Label();
            this.lblKey1 = new System.Windows.Forms.Label();
            this.cmbKey3 = new System.Windows.Forms.ComboBox();
            this.cmbKey2 = new System.Windows.Forms.ComboBox();
            this.cmbKey1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(12, 28);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(398, 20);
            this.txtTitle.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(12, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(0, 13);
            this.lblTitle.TabIndex = 0;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(220, 400);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(91, 26);
            this.btnApply.TabIndex = 20;
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.ButtonApplyClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(319, 400);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(91, 26);
            this.btnCancel.TabIndex = 21;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.ButtonCancelClick);
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Location = new System.Drawing.Point(12, 64);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(0, 13);
            this.lblFileName.TabIndex = 2;
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(12, 80);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(357, 20);
            this.txtFileName.TabIndex = 3;
            // 
            // lblArguments
            // 
            this.lblArguments.AutoSize = true;
            this.lblArguments.Location = new System.Drawing.Point(12, 116);
            this.lblArguments.Name = "lblArguments";
            this.lblArguments.Size = new System.Drawing.Size(0, 13);
            this.lblArguments.TabIndex = 5;
            // 
            // txtArguments
            // 
            this.txtArguments.Location = new System.Drawing.Point(12, 132);
            this.txtArguments.Name = "txtArguments";
            this.txtArguments.Size = new System.Drawing.Size(398, 20);
            this.txtArguments.TabIndex = 6;
            // 
            // btnBrowseFile
            // 
            this.btnBrowseFile.Location = new System.Drawing.Point(375, 78);
            this.btnBrowseFile.Name = "btnBrowseFile";
            this.btnBrowseFile.Size = new System.Drawing.Size(35, 23);
            this.btnBrowseFile.TabIndex = 4;
            this.btnBrowseFile.Text = "...";
            this.btnBrowseFile.UseVisualStyleBackColor = true;
            this.btnBrowseFile.Click += new System.EventHandler(this.ButtonBrowseFileClick);
            // 
            // lblBegin
            // 
            this.lblBegin.AutoSize = true;
            this.lblBegin.Location = new System.Drawing.Point(12, 168);
            this.lblBegin.Name = "lblBegin";
            this.lblBegin.Size = new System.Drawing.Size(0, 13);
            this.lblBegin.TabIndex = 7;
            // 
            // txtBegin
            // 
            this.txtBegin.Location = new System.Drawing.Point(12, 184);
            this.txtBegin.Name = "txtBegin";
            this.txtBegin.Size = new System.Drawing.Size(180, 20);
            this.txtBegin.TabIndex = 8;
            this.txtBegin.TextChanged += new System.EventHandler(this.BeginParameterTextChanged);
            // 
            // lblEnd
            // 
            this.lblEnd.AutoSize = true;
            this.lblEnd.Location = new System.Drawing.Point(226, 168);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(0, 13);
            this.lblEnd.TabIndex = 9;
            // 
            // txtEnd
            // 
            this.txtEnd.Location = new System.Drawing.Point(226, 184);
            this.txtEnd.Name = "txtEnd";
            this.txtEnd.Size = new System.Drawing.Size(184, 20);
            this.txtEnd.TabIndex = 10;
            this.txtEnd.TextChanged += new System.EventHandler(this.EndParameterTextChanged);
            // 
            // txtParameter
            // 
            this.txtParameter.Location = new System.Drawing.Point(12, 236);
            this.txtParameter.Name = "txtParameter";
            this.txtParameter.ReadOnly = true;
            this.txtParameter.Size = new System.Drawing.Size(398, 20);
            this.txtParameter.TabIndex = 11;
            // 
            // chkShowWindow
            // 
            this.chkShowWindow.AutoSize = true;
            this.chkShowWindow.Location = new System.Drawing.Point(12, 335);
            this.chkShowWindow.Name = "chkShowWindow";
            this.chkShowWindow.Size = new System.Drawing.Size(95, 17);
            this.chkShowWindow.TabIndex = 18;
            this.chkShowWindow.Text = "Show Window";
            this.chkShowWindow.UseVisualStyleBackColor = true;
            // 
            // chkUseWindowWorkingDirectory
            // 
            this.chkUseWindowWorkingDirectory.AutoSize = true;
            this.chkUseWindowWorkingDirectory.Location = new System.Drawing.Point(12, 372);
            this.chkUseWindowWorkingDirectory.Name = "chkUseWindowWorkingDirectory";
            this.chkUseWindowWorkingDirectory.Size = new System.Drawing.Size(176, 17);
            this.chkUseWindowWorkingDirectory.TabIndex = 19;
            this.chkUseWindowWorkingDirectory.Text = "Use a window working directory";
            this.chkUseWindowWorkingDirectory.UseVisualStyleBackColor = true;
            // 
            // lblKey3
            // 
            this.lblKey3.AutoSize = true;
            this.lblKey3.Location = new System.Drawing.Point(278, 272);
            this.lblKey3.Name = "lblKey3";
            this.lblKey3.Size = new System.Drawing.Size(34, 13);
            this.lblKey3.TabIndex = 16;
            this.lblKey3.Text = "Key 3";
            // 
            // lblKey2
            // 
            this.lblKey2.AutoSize = true;
            this.lblKey2.Location = new System.Drawing.Point(143, 272);
            this.lblKey2.Name = "lblKey2";
            this.lblKey2.Size = new System.Drawing.Size(34, 13);
            this.lblKey2.TabIndex = 14;
            this.lblKey2.Text = "Key 2";
            // 
            // lblKey1
            // 
            this.lblKey1.AutoSize = true;
            this.lblKey1.Location = new System.Drawing.Point(9, 272);
            this.lblKey1.Name = "lblKey1";
            this.lblKey1.Size = new System.Drawing.Size(34, 13);
            this.lblKey1.TabIndex = 12;
            this.lblKey1.Text = "Key 1";
            // 
            // cmbKey3
            // 
            this.cmbKey3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKey3.FormattingEnabled = true;
            this.cmbKey3.Location = new System.Drawing.Point(281, 288);
            this.cmbKey3.Name = "cmbKey3";
            this.cmbKey3.Size = new System.Drawing.Size(129, 21);
            this.cmbKey3.TabIndex = 17;
            // 
            // cmbKey2
            // 
            this.cmbKey2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKey2.FormattingEnabled = true;
            this.cmbKey2.Location = new System.Drawing.Point(146, 288);
            this.cmbKey2.Name = "cmbKey2";
            this.cmbKey2.Size = new System.Drawing.Size(129, 21);
            this.cmbKey2.TabIndex = 15;
            // 
            // cmbKey1
            // 
            this.cmbKey1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKey1.FormattingEnabled = true;
            this.cmbKey1.Location = new System.Drawing.Point(12, 288);
            this.cmbKey1.Name = "cmbKey1";
            this.cmbKey1.Size = new System.Drawing.Size(129, 21);
            this.cmbKey1.TabIndex = 13;
            // 
            // StartProgramForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 450);
            this.Controls.Add(this.lblKey3);
            this.Controls.Add(this.lblKey2);
            this.Controls.Add(this.lblKey1);
            this.Controls.Add(this.cmbKey3);
            this.Controls.Add(this.cmbKey2);
            this.Controls.Add(this.cmbKey1);
            this.Controls.Add(this.chkUseWindowWorkingDirectory);
            this.Controls.Add(this.chkShowWindow);
            this.Controls.Add(this.txtParameter);
            this.Controls.Add(this.lblEnd);
            this.Controls.Add(this.txtEnd);
            this.Controls.Add(this.lblBegin);
            this.Controls.Add(this.txtBegin);
            this.Controls.Add(this.btnBrowseFile);
            this.Controls.Add(this.lblArguments);
            this.Controls.Add(this.txtArguments);
            this.Controls.Add(this.lblFileName);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.txtTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StartProgramForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDownClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Label lblArguments;
        private System.Windows.Forms.TextBox txtArguments;
        private System.Windows.Forms.Button btnBrowseFile;
        private System.Windows.Forms.Label lblBegin;
        private System.Windows.Forms.TextBox txtBegin;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.TextBox txtEnd;
        private System.Windows.Forms.TextBox txtParameter;
        private System.Windows.Forms.CheckBox chkShowWindow;
        private System.Windows.Forms.CheckBox chkUseWindowWorkingDirectory;
        private System.Windows.Forms.Label lblKey3;
        private System.Windows.Forms.Label lblKey2;
        private System.Windows.Forms.Label lblKey1;
        private System.Windows.Forms.ComboBox cmbKey3;
        private System.Windows.Forms.ComboBox cmbKey2;
        private System.Windows.Forms.ComboBox cmbKey1;
    }
}