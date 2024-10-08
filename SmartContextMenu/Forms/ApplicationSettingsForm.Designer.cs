﻿namespace SmartContextMenu.Forms
{
    partial class ApplicationSettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplicationSettingsForm));
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabpGeneral = new System.Windows.Forms.TabPage();
            this.grpbDisplay = new System.Windows.Forms.GroupBox();
            this.chkEnableHighDPI = new System.Windows.Forms.CheckBox();
            this.grpbLanguage = new System.Windows.Forms.GroupBox();
            this.listBoxLanguage = new System.Windows.Forms.ListBox();
            this.grpbMouseHotkeys = new System.Windows.Forms.GroupBox();
            this.lblMouseButton = new System.Windows.Forms.Label();
            this.cmbMouseButton = new System.Windows.Forms.ComboBox();
            this.lblKey4 = new System.Windows.Forms.Label();
            this.cmbKey4 = new System.Windows.Forms.ComboBox();
            this.lblKey3 = new System.Windows.Forms.Label();
            this.lblKey2 = new System.Windows.Forms.Label();
            this.lblKey1 = new System.Windows.Forms.Label();
            this.cmbKey3 = new System.Windows.Forms.ComboBox();
            this.cmbKey2 = new System.Windows.Forms.ComboBox();
            this.cmbKey1 = new System.Windows.Forms.ComboBox();
            this.tabpMenu = new System.Windows.Forms.TabPage();
            this.grpbHotkeys = new System.Windows.Forms.GroupBox();
            this.btnMenuItemDown = new System.Windows.Forms.Button();
            this.btnMenuItemUp = new System.Windows.Forms.Button();
            this.gvHotkeys = new System.Windows.Forms.DataGridView();
            this.tabpMenuSize = new System.Windows.Forms.TabPage();
            this.grpbSizer = new System.Windows.Forms.GroupBox();
            this.cmbSizer = new System.Windows.Forms.ComboBox();
            this.grpbWindowSize = new System.Windows.Forms.GroupBox();
            this.btnWindowSizeDown = new System.Windows.Forms.Button();
            this.btnWindowSizeUp = new System.Windows.Forms.Button();
            this.btnAddWindowSize = new System.Windows.Forms.Button();
            this.gvWindowSize = new System.Windows.Forms.DataGridView();
            this.clmWindowSizeTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmWindowSizeLeft = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmWindowSizeTop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmWindowSizeWidth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmWindowSizeHeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmWindowSizeHotKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmWindowSizeEdit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.clmWindowSizeDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tabpMenuStart = new System.Windows.Forms.TabPage();
            this.grpbStartProgram = new System.Windows.Forms.GroupBox();
            this.btnStartProgramDown = new System.Windows.Forms.Button();
            this.btnStartProgramUp = new System.Windows.Forms.Button();
            this.btnAddStartProgram = new System.Windows.Forms.Button();
            this.gvStartProgram = new System.Windows.Forms.DataGridView();
            this.clmStartProgramTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmStartProgramPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmStartProgramArguments = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmStartProgramEdit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.clmStartProgramDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tabpMenuDimmer = new System.Windows.Forms.TabPage();
            this.grpbDimmerColor = new System.Windows.Forms.GroupBox();
            this.btnChooseDimmerColor = new System.Windows.Forms.Button();
            this.txtDimmerColor = new System.Windows.Forms.TextBox();
            this.grpbDimmerTransparency = new System.Windows.Forms.GroupBox();
            this.lblTransparencyValue = new System.Windows.Forms.Label();
            this.lblTransparencyToValue = new System.Windows.Forms.Label();
            this.lblTransparencyFromValue = new System.Windows.Forms.Label();
            this.trackbDimmerTransparency = new System.Windows.Forms.TrackBar();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.toolTipAddProcessName = new System.Windows.Forms.ToolTip(this.components);
            this.dataGridViewDisableButtonColumn1 = new SmartContextMenu.Controls.DataGridViewDisableButtonColumn();
            this.dataGridViewButtonColumn1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewButtonColumn2 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewButtonColumn3 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewButtonColumn4 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.clmnMenuItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmnHotkeys = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmnShow = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clmnChangeHotkey = new SmartContextMenu.Controls.DataGridViewDisableButtonColumn();
            this.tabMain.SuspendLayout();
            this.tabpGeneral.SuspendLayout();
            this.grpbDisplay.SuspendLayout();
            this.grpbLanguage.SuspendLayout();
            this.grpbMouseHotkeys.SuspendLayout();
            this.tabpMenu.SuspendLayout();
            this.grpbHotkeys.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvHotkeys)).BeginInit();
            this.tabpMenuSize.SuspendLayout();
            this.grpbSizer.SuspendLayout();
            this.grpbWindowSize.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvWindowSize)).BeginInit();
            this.tabpMenuStart.SuspendLayout();
            this.grpbStartProgram.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvStartProgram)).BeginInit();
            this.tabpMenuDimmer.SuspendLayout();
            this.grpbDimmerColor.SuspendLayout();
            this.grpbDimmerTransparency.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackbDimmerTransparency)).BeginInit();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabpGeneral);
            this.tabMain.Controls.Add(this.tabpMenu);
            this.tabMain.Controls.Add(this.tabpMenuSize);
            this.tabMain.Controls.Add(this.tabpMenuStart);
            this.tabMain.Controls.Add(this.tabpMenuDimmer);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(565, 416);
            this.tabMain.TabIndex = 0;
            // 
            // tabpGeneral
            // 
            this.tabpGeneral.Controls.Add(this.grpbDisplay);
            this.tabpGeneral.Controls.Add(this.grpbLanguage);
            this.tabpGeneral.Controls.Add(this.grpbMouseHotkeys);
            this.tabpGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabpGeneral.Name = "tabpGeneral";
            this.tabpGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabpGeneral.Size = new System.Drawing.Size(557, 390);
            this.tabpGeneral.TabIndex = 0;
            this.tabpGeneral.UseVisualStyleBackColor = true;
            // 
            // grpbDisplay
            // 
            this.grpbDisplay.Controls.Add(this.chkEnableHighDPI);
            this.grpbDisplay.Location = new System.Drawing.Point(8, 115);
            this.grpbDisplay.Name = "grpbDisplay";
            this.grpbDisplay.Size = new System.Drawing.Size(541, 69);
            this.grpbDisplay.TabIndex = 1;
            this.grpbDisplay.TabStop = false;
            // 
            // chkEnableHighDPI
            // 
            this.chkEnableHighDPI.AutoSize = true;
            this.chkEnableHighDPI.Location = new System.Drawing.Point(6, 28);
            this.chkEnableHighDPI.Name = "chkEnableHighDPI";
            this.chkEnableHighDPI.Size = new System.Drawing.Size(15, 14);
            this.chkEnableHighDPI.TabIndex = 0;
            this.chkEnableHighDPI.UseVisualStyleBackColor = true;
            // 
            // grpbLanguage
            // 
            this.grpbLanguage.Controls.Add(this.listBoxLanguage);
            this.grpbLanguage.Location = new System.Drawing.Point(8, 190);
            this.grpbLanguage.Name = "grpbLanguage";
            this.grpbLanguage.Size = new System.Drawing.Size(541, 194);
            this.grpbLanguage.TabIndex = 2;
            this.grpbLanguage.TabStop = false;
            // 
            // listBoxLanguage
            // 
            this.listBoxLanguage.FormattingEnabled = true;
            this.listBoxLanguage.Location = new System.Drawing.Point(6, 19);
            this.listBoxLanguage.Name = "listBoxLanguage";
            this.listBoxLanguage.Size = new System.Drawing.Size(206, 160);
            this.listBoxLanguage.TabIndex = 0;
            // 
            // grpbMouseHotkeys
            // 
            this.grpbMouseHotkeys.Controls.Add(this.lblMouseButton);
            this.grpbMouseHotkeys.Controls.Add(this.cmbMouseButton);
            this.grpbMouseHotkeys.Controls.Add(this.lblKey4);
            this.grpbMouseHotkeys.Controls.Add(this.cmbKey4);
            this.grpbMouseHotkeys.Controls.Add(this.lblKey3);
            this.grpbMouseHotkeys.Controls.Add(this.lblKey2);
            this.grpbMouseHotkeys.Controls.Add(this.lblKey1);
            this.grpbMouseHotkeys.Controls.Add(this.cmbKey3);
            this.grpbMouseHotkeys.Controls.Add(this.cmbKey2);
            this.grpbMouseHotkeys.Controls.Add(this.cmbKey1);
            this.grpbMouseHotkeys.Location = new System.Drawing.Point(8, 9);
            this.grpbMouseHotkeys.Name = "grpbMouseHotkeys";
            this.grpbMouseHotkeys.Size = new System.Drawing.Size(541, 100);
            this.grpbMouseHotkeys.TabIndex = 0;
            this.grpbMouseHotkeys.TabStop = false;
            // 
            // lblMouseButton
            // 
            this.lblMouseButton.AutoSize = true;
            this.lblMouseButton.Location = new System.Drawing.Point(427, 29);
            this.lblMouseButton.Name = "lblMouseButton";
            this.lblMouseButton.Size = new System.Drawing.Size(73, 13);
            this.lblMouseButton.TabIndex = 8;
            this.lblMouseButton.Text = "Mouse Button";
            // 
            // cmbMouseButton
            // 
            this.cmbMouseButton.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMouseButton.FormattingEnabled = true;
            this.cmbMouseButton.Location = new System.Drawing.Point(430, 45);
            this.cmbMouseButton.Name = "cmbMouseButton";
            this.cmbMouseButton.Size = new System.Drawing.Size(100, 21);
            this.cmbMouseButton.TabIndex = 9;
            // 
            // lblKey4
            // 
            this.lblKey4.AutoSize = true;
            this.lblKey4.Location = new System.Drawing.Point(321, 29);
            this.lblKey4.Name = "lblKey4";
            this.lblKey4.Size = new System.Drawing.Size(34, 13);
            this.lblKey4.TabIndex = 6;
            this.lblKey4.Text = "Key 4";
            // 
            // cmbKey4
            // 
            this.cmbKey4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKey4.FormattingEnabled = true;
            this.cmbKey4.Location = new System.Drawing.Point(324, 45);
            this.cmbKey4.Name = "cmbKey4";
            this.cmbKey4.Size = new System.Drawing.Size(100, 21);
            this.cmbKey4.TabIndex = 7;
            // 
            // lblKey3
            // 
            this.lblKey3.AutoSize = true;
            this.lblKey3.Location = new System.Drawing.Point(215, 29);
            this.lblKey3.Name = "lblKey3";
            this.lblKey3.Size = new System.Drawing.Size(34, 13);
            this.lblKey3.TabIndex = 4;
            this.lblKey3.Text = "Key 3";
            // 
            // lblKey2
            // 
            this.lblKey2.AutoSize = true;
            this.lblKey2.Location = new System.Drawing.Point(109, 29);
            this.lblKey2.Name = "lblKey2";
            this.lblKey2.Size = new System.Drawing.Size(34, 13);
            this.lblKey2.TabIndex = 2;
            this.lblKey2.Text = "Key 2";
            // 
            // lblKey1
            // 
            this.lblKey1.AutoSize = true;
            this.lblKey1.Location = new System.Drawing.Point(3, 29);
            this.lblKey1.Name = "lblKey1";
            this.lblKey1.Size = new System.Drawing.Size(34, 13);
            this.lblKey1.TabIndex = 0;
            this.lblKey1.Text = "Key 1";
            // 
            // cmbKey3
            // 
            this.cmbKey3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKey3.FormattingEnabled = true;
            this.cmbKey3.Location = new System.Drawing.Point(218, 45);
            this.cmbKey3.Name = "cmbKey3";
            this.cmbKey3.Size = new System.Drawing.Size(100, 21);
            this.cmbKey3.TabIndex = 5;
            // 
            // cmbKey2
            // 
            this.cmbKey2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKey2.FormattingEnabled = true;
            this.cmbKey2.Location = new System.Drawing.Point(112, 45);
            this.cmbKey2.Name = "cmbKey2";
            this.cmbKey2.Size = new System.Drawing.Size(100, 21);
            this.cmbKey2.TabIndex = 3;
            // 
            // cmbKey1
            // 
            this.cmbKey1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKey1.FormattingEnabled = true;
            this.cmbKey1.Location = new System.Drawing.Point(6, 45);
            this.cmbKey1.Name = "cmbKey1";
            this.cmbKey1.Size = new System.Drawing.Size(100, 21);
            this.cmbKey1.TabIndex = 1;
            // 
            // tabpMenu
            // 
            this.tabpMenu.Controls.Add(this.grpbHotkeys);
            this.tabpMenu.Location = new System.Drawing.Point(4, 22);
            this.tabpMenu.Name = "tabpMenu";
            this.tabpMenu.Padding = new System.Windows.Forms.Padding(3);
            this.tabpMenu.Size = new System.Drawing.Size(557, 390);
            this.tabpMenu.TabIndex = 2;
            this.tabpMenu.UseVisualStyleBackColor = true;
            // 
            // grpbHotkeys
            // 
            this.grpbHotkeys.Controls.Add(this.btnMenuItemDown);
            this.grpbHotkeys.Controls.Add(this.btnMenuItemUp);
            this.grpbHotkeys.Controls.Add(this.gvHotkeys);
            this.grpbHotkeys.Location = new System.Drawing.Point(8, 16);
            this.grpbHotkeys.Name = "grpbHotkeys";
            this.grpbHotkeys.Size = new System.Drawing.Size(541, 368);
            this.grpbHotkeys.TabIndex = 3;
            this.grpbHotkeys.TabStop = false;
            // 
            // btnMenuItemDown
            // 
            this.btnMenuItemDown.BackgroundImage = global::SmartContextMenu.Properties.Resources.ArrowDown;
            this.btnMenuItemDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnMenuItemDown.Location = new System.Drawing.Point(504, 339);
            this.btnMenuItemDown.Name = "btnMenuItemDown";
            this.btnMenuItemDown.Size = new System.Drawing.Size(31, 23);
            this.btnMenuItemDown.TabIndex = 4;
            this.btnMenuItemDown.UseVisualStyleBackColor = true;
            this.btnMenuItemDown.Click += new System.EventHandler(this.ButtonMenuItemDownClick);
            // 
            // btnMenuItemUp
            // 
            this.btnMenuItemUp.BackgroundImage = global::SmartContextMenu.Properties.Resources.ArrowUp;
            this.btnMenuItemUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnMenuItemUp.Location = new System.Drawing.Point(467, 339);
            this.btnMenuItemUp.Name = "btnMenuItemUp";
            this.btnMenuItemUp.Size = new System.Drawing.Size(31, 23);
            this.btnMenuItemUp.TabIndex = 3;
            this.btnMenuItemUp.UseVisualStyleBackColor = true;
            this.btnMenuItemUp.Click += new System.EventHandler(this.ButtonMenuItemUpClick);
            // 
            // gvHotkeys
            // 
            this.gvHotkeys.AllowUserToAddRows = false;
            this.gvHotkeys.AllowUserToDeleteRows = false;
            this.gvHotkeys.AllowUserToResizeColumns = false;
            this.gvHotkeys.AllowUserToResizeRows = false;
            this.gvHotkeys.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.gvHotkeys.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gvHotkeys.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.gvHotkeys.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvHotkeys.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmnMenuItemName,
            this.clmnHotkeys,
            this.clmnShow,
            this.clmnChangeHotkey});
            this.gvHotkeys.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gvHotkeys.GridColor = System.Drawing.SystemColors.Control;
            this.gvHotkeys.Location = new System.Drawing.Point(6, 19);
            this.gvHotkeys.MultiSelect = false;
            this.gvHotkeys.Name = "gvHotkeys";
            this.gvHotkeys.RowHeadersVisible = false;
            this.gvHotkeys.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gvHotkeys.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvHotkeys.Size = new System.Drawing.Size(529, 314);
            this.gvHotkeys.TabIndex = 0;
            this.gvHotkeys.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridViewHotkeysCellContentClick);
            this.gvHotkeys.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridViewHotkeysCellDoubleClick);
            // 
            // tabpMenuSize
            // 
            this.tabpMenuSize.Controls.Add(this.grpbSizer);
            this.tabpMenuSize.Controls.Add(this.grpbWindowSize);
            this.tabpMenuSize.Location = new System.Drawing.Point(4, 22);
            this.tabpMenuSize.Name = "tabpMenuSize";
            this.tabpMenuSize.Padding = new System.Windows.Forms.Padding(3);
            this.tabpMenuSize.Size = new System.Drawing.Size(557, 390);
            this.tabpMenuSize.TabIndex = 3;
            this.tabpMenuSize.UseVisualStyleBackColor = true;
            // 
            // grpbSizer
            // 
            this.grpbSizer.Controls.Add(this.cmbSizer);
            this.grpbSizer.Location = new System.Drawing.Point(8, 16);
            this.grpbSizer.Name = "grpbSizer";
            this.grpbSizer.Size = new System.Drawing.Size(541, 68);
            this.grpbSizer.TabIndex = 0;
            this.grpbSizer.TabStop = false;
            // 
            // cmbSizer
            // 
            this.cmbSizer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSizer.FormattingEnabled = true;
            this.cmbSizer.Location = new System.Drawing.Point(6, 28);
            this.cmbSizer.Name = "cmbSizer";
            this.cmbSizer.Size = new System.Drawing.Size(166, 21);
            this.cmbSizer.TabIndex = 0;
            // 
            // grpbWindowSize
            // 
            this.grpbWindowSize.Controls.Add(this.btnWindowSizeDown);
            this.grpbWindowSize.Controls.Add(this.btnWindowSizeUp);
            this.grpbWindowSize.Controls.Add(this.btnAddWindowSize);
            this.grpbWindowSize.Controls.Add(this.gvWindowSize);
            this.grpbWindowSize.Location = new System.Drawing.Point(8, 87);
            this.grpbWindowSize.Name = "grpbWindowSize";
            this.grpbWindowSize.Size = new System.Drawing.Size(541, 297);
            this.grpbWindowSize.TabIndex = 1;
            this.grpbWindowSize.TabStop = false;
            // 
            // btnWindowSizeDown
            // 
            this.btnWindowSizeDown.BackgroundImage = global::SmartContextMenu.Properties.Resources.ArrowDown;
            this.btnWindowSizeDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnWindowSizeDown.Location = new System.Drawing.Point(450, 268);
            this.btnWindowSizeDown.Name = "btnWindowSizeDown";
            this.btnWindowSizeDown.Size = new System.Drawing.Size(31, 23);
            this.btnWindowSizeDown.TabIndex = 2;
            this.btnWindowSizeDown.UseVisualStyleBackColor = true;
            this.btnWindowSizeDown.Click += new System.EventHandler(this.ButtonArrowDownClick);
            // 
            // btnWindowSizeUp
            // 
            this.btnWindowSizeUp.BackgroundImage = global::SmartContextMenu.Properties.Resources.ArrowUp;
            this.btnWindowSizeUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnWindowSizeUp.Location = new System.Drawing.Point(413, 268);
            this.btnWindowSizeUp.Name = "btnWindowSizeUp";
            this.btnWindowSizeUp.Size = new System.Drawing.Size(31, 23);
            this.btnWindowSizeUp.TabIndex = 1;
            this.btnWindowSizeUp.UseVisualStyleBackColor = true;
            this.btnWindowSizeUp.Click += new System.EventHandler(this.ButtonArrowUpClick);
            // 
            // btnAddWindowSize
            // 
            this.btnAddWindowSize.Location = new System.Drawing.Point(504, 268);
            this.btnAddWindowSize.Name = "btnAddWindowSize";
            this.btnAddWindowSize.Size = new System.Drawing.Size(31, 23);
            this.btnAddWindowSize.TabIndex = 3;
            this.btnAddWindowSize.Text = "+";
            this.btnAddWindowSize.UseVisualStyleBackColor = true;
            this.btnAddWindowSize.Click += new System.EventHandler(this.ButtonAddWindowSizeClick);
            // 
            // gvWindowSize
            // 
            this.gvWindowSize.AllowUserToAddRows = false;
            this.gvWindowSize.AllowUserToDeleteRows = false;
            this.gvWindowSize.AllowUserToResizeColumns = false;
            this.gvWindowSize.AllowUserToResizeRows = false;
            this.gvWindowSize.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.gvWindowSize.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gvWindowSize.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.gvWindowSize.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvWindowSize.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmWindowSizeTitle,
            this.clmWindowSizeLeft,
            this.clmWindowSizeTop,
            this.clmWindowSizeWidth,
            this.clmWindowSizeHeight,
            this.clmWindowSizeHotKey,
            this.clmWindowSizeEdit,
            this.clmWindowSizeDelete});
            this.gvWindowSize.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gvWindowSize.GridColor = System.Drawing.SystemColors.Control;
            this.gvWindowSize.Location = new System.Drawing.Point(6, 19);
            this.gvWindowSize.MultiSelect = false;
            this.gvWindowSize.Name = "gvWindowSize";
            this.gvWindowSize.RowHeadersVisible = false;
            this.gvWindowSize.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gvWindowSize.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvWindowSize.Size = new System.Drawing.Size(529, 243);
            this.gvWindowSize.TabIndex = 0;
            this.gvWindowSize.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridViewWindowSizeCellContentClick);
            this.gvWindowSize.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridViewWindowSizeCellDoubleClick);
            // 
            // clmWindowSizeTitle
            // 
            this.clmWindowSizeTitle.HeaderText = "clmWindowSizeTitle";
            this.clmWindowSizeTitle.Name = "clmWindowSizeTitle";
            this.clmWindowSizeTitle.ReadOnly = true;
            this.clmWindowSizeTitle.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // clmWindowSizeLeft
            // 
            this.clmWindowSizeLeft.HeaderText = "clmWindowSizeLeft";
            this.clmWindowSizeLeft.Name = "clmWindowSizeLeft";
            this.clmWindowSizeLeft.ReadOnly = true;
            this.clmWindowSizeLeft.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmWindowSizeLeft.Width = 50;
            // 
            // clmWindowSizeTop
            // 
            this.clmWindowSizeTop.HeaderText = "clmWindowSizeTop";
            this.clmWindowSizeTop.Name = "clmWindowSizeTop";
            this.clmWindowSizeTop.ReadOnly = true;
            this.clmWindowSizeTop.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmWindowSizeTop.Width = 50;
            // 
            // clmWindowSizeWidth
            // 
            this.clmWindowSizeWidth.HeaderText = "clmWindowSizeWidth";
            this.clmWindowSizeWidth.Name = "clmWindowSizeWidth";
            this.clmWindowSizeWidth.ReadOnly = true;
            this.clmWindowSizeWidth.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmWindowSizeWidth.Width = 50;
            // 
            // clmWindowSizeHeight
            // 
            this.clmWindowSizeHeight.HeaderText = "clmWindowSizeHeight";
            this.clmWindowSizeHeight.Name = "clmWindowSizeHeight";
            this.clmWindowSizeHeight.ReadOnly = true;
            this.clmWindowSizeHeight.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmWindowSizeHeight.Width = 50;
            // 
            // clmWindowSizeHotKey
            // 
            this.clmWindowSizeHotKey.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clmWindowSizeHotKey.HeaderText = "";
            this.clmWindowSizeHotKey.MinimumWidth = 30;
            this.clmWindowSizeHotKey.Name = "clmWindowSizeHotKey";
            this.clmWindowSizeHotKey.ReadOnly = true;
            this.clmWindowSizeHotKey.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // clmWindowSizeEdit
            // 
            this.clmWindowSizeEdit.HeaderText = "";
            this.clmWindowSizeEdit.Name = "clmWindowSizeEdit";
            this.clmWindowSizeEdit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.clmWindowSizeEdit.Text = "...";
            this.clmWindowSizeEdit.UseColumnTextForButtonValue = true;
            this.clmWindowSizeEdit.Width = 30;
            // 
            // clmWindowSizeDelete
            // 
            this.clmWindowSizeDelete.HeaderText = "";
            this.clmWindowSizeDelete.Name = "clmWindowSizeDelete";
            this.clmWindowSizeDelete.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.clmWindowSizeDelete.Text = "-";
            this.clmWindowSizeDelete.UseColumnTextForButtonValue = true;
            this.clmWindowSizeDelete.Width = 30;
            // 
            // tabpMenuStart
            // 
            this.tabpMenuStart.Controls.Add(this.grpbStartProgram);
            this.tabpMenuStart.Location = new System.Drawing.Point(4, 22);
            this.tabpMenuStart.Name = "tabpMenuStart";
            this.tabpMenuStart.Padding = new System.Windows.Forms.Padding(3);
            this.tabpMenuStart.Size = new System.Drawing.Size(557, 390);
            this.tabpMenuStart.TabIndex = 1;
            this.tabpMenuStart.UseVisualStyleBackColor = true;
            // 
            // grpbStartProgram
            // 
            this.grpbStartProgram.Controls.Add(this.btnStartProgramDown);
            this.grpbStartProgram.Controls.Add(this.btnStartProgramUp);
            this.grpbStartProgram.Controls.Add(this.btnAddStartProgram);
            this.grpbStartProgram.Controls.Add(this.gvStartProgram);
            this.grpbStartProgram.Location = new System.Drawing.Point(8, 16);
            this.grpbStartProgram.Name = "grpbStartProgram";
            this.grpbStartProgram.Size = new System.Drawing.Size(541, 368);
            this.grpbStartProgram.TabIndex = 0;
            this.grpbStartProgram.TabStop = false;
            // 
            // btnStartProgramDown
            // 
            this.btnStartProgramDown.BackgroundImage = global::SmartContextMenu.Properties.Resources.ArrowDown;
            this.btnStartProgramDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnStartProgramDown.Location = new System.Drawing.Point(450, 339);
            this.btnStartProgramDown.Name = "btnStartProgramDown";
            this.btnStartProgramDown.Size = new System.Drawing.Size(31, 23);
            this.btnStartProgramDown.TabIndex = 2;
            this.btnStartProgramDown.UseVisualStyleBackColor = true;
            this.btnStartProgramDown.Click += new System.EventHandler(this.ButtonArrowDownClick);
            // 
            // btnStartProgramUp
            // 
            this.btnStartProgramUp.BackgroundImage = global::SmartContextMenu.Properties.Resources.ArrowUp;
            this.btnStartProgramUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnStartProgramUp.Location = new System.Drawing.Point(413, 339);
            this.btnStartProgramUp.Name = "btnStartProgramUp";
            this.btnStartProgramUp.Size = new System.Drawing.Size(31, 23);
            this.btnStartProgramUp.TabIndex = 1;
            this.btnStartProgramUp.UseVisualStyleBackColor = true;
            this.btnStartProgramUp.Click += new System.EventHandler(this.ButtonArrowUpClick);
            // 
            // btnAddStartProgram
            // 
            this.btnAddStartProgram.Location = new System.Drawing.Point(504, 339);
            this.btnAddStartProgram.Name = "btnAddStartProgram";
            this.btnAddStartProgram.Size = new System.Drawing.Size(31, 23);
            this.btnAddStartProgram.TabIndex = 3;
            this.btnAddStartProgram.Text = "+";
            this.btnAddStartProgram.UseVisualStyleBackColor = true;
            this.btnAddStartProgram.Click += new System.EventHandler(this.ButtonAddStartProgramClick);
            // 
            // gvStartProgram
            // 
            this.gvStartProgram.AllowUserToAddRows = false;
            this.gvStartProgram.AllowUserToDeleteRows = false;
            this.gvStartProgram.AllowUserToResizeColumns = false;
            this.gvStartProgram.AllowUserToResizeRows = false;
            this.gvStartProgram.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.gvStartProgram.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gvStartProgram.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.gvStartProgram.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvStartProgram.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmStartProgramTitle,
            this.clmStartProgramPath,
            this.clmStartProgramArguments,
            this.clmStartProgramEdit,
            this.clmStartProgramDelete});
            this.gvStartProgram.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gvStartProgram.GridColor = System.Drawing.SystemColors.Control;
            this.gvStartProgram.Location = new System.Drawing.Point(6, 19);
            this.gvStartProgram.MultiSelect = false;
            this.gvStartProgram.Name = "gvStartProgram";
            this.gvStartProgram.RowHeadersVisible = false;
            this.gvStartProgram.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gvStartProgram.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvStartProgram.Size = new System.Drawing.Size(529, 314);
            this.gvStartProgram.TabIndex = 0;
            this.gvStartProgram.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridViewStartProgramCellContentClick);
            this.gvStartProgram.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridViewStartProgramCellDoubleClick);
            // 
            // clmStartProgramTitle
            // 
            this.clmStartProgramTitle.HeaderText = "clmStartProgramTitle";
            this.clmStartProgramTitle.MinimumWidth = 6;
            this.clmStartProgramTitle.Name = "clmStartProgramTitle";
            this.clmStartProgramTitle.ReadOnly = true;
            this.clmStartProgramTitle.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmStartProgramTitle.Width = 160;
            // 
            // clmStartProgramPath
            // 
            this.clmStartProgramPath.HeaderText = "clmStartProgramPath";
            this.clmStartProgramPath.MinimumWidth = 6;
            this.clmStartProgramPath.Name = "clmStartProgramPath";
            this.clmStartProgramPath.ReadOnly = true;
            this.clmStartProgramPath.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmStartProgramPath.Width = 160;
            // 
            // clmStartProgramArguments
            // 
            this.clmStartProgramArguments.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clmStartProgramArguments.HeaderText = "clmStartProgramArguments";
            this.clmStartProgramArguments.MinimumWidth = 30;
            this.clmStartProgramArguments.Name = "clmStartProgramArguments";
            this.clmStartProgramArguments.ReadOnly = true;
            this.clmStartProgramArguments.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // clmStartProgramEdit
            // 
            this.clmStartProgramEdit.HeaderText = "";
            this.clmStartProgramEdit.MinimumWidth = 6;
            this.clmStartProgramEdit.Name = "clmStartProgramEdit";
            this.clmStartProgramEdit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.clmStartProgramEdit.Text = "...";
            this.clmStartProgramEdit.UseColumnTextForButtonValue = true;
            this.clmStartProgramEdit.Width = 30;
            // 
            // clmStartProgramDelete
            // 
            this.clmStartProgramDelete.HeaderText = "";
            this.clmStartProgramDelete.MinimumWidth = 6;
            this.clmStartProgramDelete.Name = "clmStartProgramDelete";
            this.clmStartProgramDelete.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.clmStartProgramDelete.Text = "-";
            this.clmStartProgramDelete.UseColumnTextForButtonValue = true;
            this.clmStartProgramDelete.Width = 30;
            // 
            // tabpMenuDimmer
            // 
            this.tabpMenuDimmer.Controls.Add(this.grpbDimmerColor);
            this.tabpMenuDimmer.Controls.Add(this.grpbDimmerTransparency);
            this.tabpMenuDimmer.Location = new System.Drawing.Point(4, 22);
            this.tabpMenuDimmer.Name = "tabpMenuDimmer";
            this.tabpMenuDimmer.Size = new System.Drawing.Size(557, 390);
            this.tabpMenuDimmer.TabIndex = 4;
            this.tabpMenuDimmer.UseVisualStyleBackColor = true;
            // 
            // grpbDimmerColor
            // 
            this.grpbDimmerColor.Controls.Add(this.btnChooseDimmerColor);
            this.grpbDimmerColor.Controls.Add(this.txtDimmerColor);
            this.grpbDimmerColor.Location = new System.Drawing.Point(8, 16);
            this.grpbDimmerColor.Name = "grpbDimmerColor";
            this.grpbDimmerColor.Size = new System.Drawing.Size(541, 68);
            this.grpbDimmerColor.TabIndex = 0;
            this.grpbDimmerColor.TabStop = false;
            // 
            // btnChooseDimmerColor
            // 
            this.btnChooseDimmerColor.Location = new System.Drawing.Point(155, 26);
            this.btnChooseDimmerColor.Name = "btnChooseDimmerColor";
            this.btnChooseDimmerColor.Size = new System.Drawing.Size(54, 23);
            this.btnChooseDimmerColor.TabIndex = 1;
            this.btnChooseDimmerColor.Text = "...";
            this.btnChooseDimmerColor.UseVisualStyleBackColor = true;
            this.btnChooseDimmerColor.Click += new System.EventHandler(this.ButtonChooseDimmerColorClick);
            // 
            // txtDimmerColor
            // 
            this.txtDimmerColor.Location = new System.Drawing.Point(6, 28);
            this.txtDimmerColor.Name = "txtDimmerColor";
            this.txtDimmerColor.Size = new System.Drawing.Size(143, 20);
            this.txtDimmerColor.TabIndex = 0;
            // 
            // grpbDimmerTransparency
            // 
            this.grpbDimmerTransparency.Controls.Add(this.lblTransparencyValue);
            this.grpbDimmerTransparency.Controls.Add(this.lblTransparencyToValue);
            this.grpbDimmerTransparency.Controls.Add(this.lblTransparencyFromValue);
            this.grpbDimmerTransparency.Controls.Add(this.trackbDimmerTransparency);
            this.grpbDimmerTransparency.Location = new System.Drawing.Point(8, 90);
            this.grpbDimmerTransparency.Name = "grpbDimmerTransparency";
            this.grpbDimmerTransparency.Size = new System.Drawing.Size(541, 100);
            this.grpbDimmerTransparency.TabIndex = 1;
            this.grpbDimmerTransparency.TabStop = false;
            // 
            // lblTransparencyValue
            // 
            this.lblTransparencyValue.AutoSize = true;
            this.lblTransparencyValue.Location = new System.Drawing.Point(259, 23);
            this.lblTransparencyValue.Name = "lblTransparencyValue";
            this.lblTransparencyValue.Size = new System.Drawing.Size(0, 13);
            this.lblTransparencyValue.TabIndex = 1;
            // 
            // lblTransparencyToValue
            // 
            this.lblTransparencyToValue.AutoSize = true;
            this.lblTransparencyToValue.Location = new System.Drawing.Point(502, 23);
            this.lblTransparencyToValue.Name = "lblTransparencyToValue";
            this.lblTransparencyToValue.Size = new System.Drawing.Size(33, 13);
            this.lblTransparencyToValue.TabIndex = 2;
            this.lblTransparencyToValue.Text = "100%";
            // 
            // lblTransparencyFromValue
            // 
            this.lblTransparencyFromValue.AutoSize = true;
            this.lblTransparencyFromValue.Location = new System.Drawing.Point(6, 23);
            this.lblTransparencyFromValue.Name = "lblTransparencyFromValue";
            this.lblTransparencyFromValue.Size = new System.Drawing.Size(21, 13);
            this.lblTransparencyFromValue.TabIndex = 0;
            this.lblTransparencyFromValue.Text = "0%";
            // 
            // trackbDimmerTransparency
            // 
            this.trackbDimmerTransparency.Location = new System.Drawing.Point(6, 39);
            this.trackbDimmerTransparency.Maximum = 100;
            this.trackbDimmerTransparency.Name = "trackbDimmerTransparency";
            this.trackbDimmerTransparency.Size = new System.Drawing.Size(529, 45);
            this.trackbDimmerTransparency.TabIndex = 3;
            this.trackbDimmerTransparency.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackbDimmerTransparency.ValueChanged += new System.EventHandler(this.TrackbDimmerTransparencyValueChanged);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(384, 432);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(81, 35);
            this.btnApply.TabIndex = 1;
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.ButtonApplyClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(473, 432);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 35);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.ButtonCancelClick);
            // 
            // dataGridViewDisableButtonColumn1
            // 
            this.dataGridViewDisableButtonColumn1.HeaderText = "";
            this.dataGridViewDisableButtonColumn1.Name = "dataGridViewDisableButtonColumn1";
            this.dataGridViewDisableButtonColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewDisableButtonColumn1.Text = "...";
            this.dataGridViewDisableButtonColumn1.UseColumnTextForButtonValue = true;
            this.dataGridViewDisableButtonColumn1.Width = 30;
            // 
            // dataGridViewButtonColumn1
            // 
            this.dataGridViewButtonColumn1.HeaderText = "";
            this.dataGridViewButtonColumn1.Name = "dataGridViewButtonColumn1";
            this.dataGridViewButtonColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewButtonColumn1.Text = "...";
            this.dataGridViewButtonColumn1.UseColumnTextForButtonValue = true;
            this.dataGridViewButtonColumn1.Width = 30;
            // 
            // dataGridViewButtonColumn2
            // 
            this.dataGridViewButtonColumn2.HeaderText = "";
            this.dataGridViewButtonColumn2.Name = "dataGridViewButtonColumn2";
            this.dataGridViewButtonColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewButtonColumn2.Text = "-";
            this.dataGridViewButtonColumn2.UseColumnTextForButtonValue = true;
            this.dataGridViewButtonColumn2.Width = 30;
            // 
            // dataGridViewButtonColumn3
            // 
            this.dataGridViewButtonColumn3.HeaderText = "";
            this.dataGridViewButtonColumn3.Name = "dataGridViewButtonColumn3";
            this.dataGridViewButtonColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewButtonColumn3.Text = "...";
            this.dataGridViewButtonColumn3.UseColumnTextForButtonValue = true;
            this.dataGridViewButtonColumn3.Width = 30;
            // 
            // dataGridViewButtonColumn4
            // 
            this.dataGridViewButtonColumn4.HeaderText = "";
            this.dataGridViewButtonColumn4.Name = "dataGridViewButtonColumn4";
            this.dataGridViewButtonColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewButtonColumn4.Text = "-";
            this.dataGridViewButtonColumn4.UseColumnTextForButtonValue = true;
            this.dataGridViewButtonColumn4.Width = 30;
            // 
            // clmnMenuItemName
            // 
            this.clmnMenuItemName.HeaderText = "clmnMenuItemName";
            this.clmnMenuItemName.MinimumWidth = 6;
            this.clmnMenuItemName.Name = "clmnMenuItemName";
            this.clmnMenuItemName.ReadOnly = true;
            this.clmnMenuItemName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmnMenuItemName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clmnMenuItemName.Width = 260;
            // 
            // clmnHotkeys
            // 
            this.clmnHotkeys.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clmnHotkeys.HeaderText = "clmnHotkeys";
            this.clmnHotkeys.MinimumWidth = 30;
            this.clmnHotkeys.Name = "clmnHotkeys";
            this.clmnHotkeys.ReadOnly = true;
            this.clmnHotkeys.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmnHotkeys.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // clmnShow
            // 
            this.clmnShow.HeaderText = "";
            this.clmnShow.Name = "clmnShow";
            this.clmnShow.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.clmnShow.Width = 30;
            // 
            // clmnChangeHotkey
            // 
            this.clmnChangeHotkey.HeaderText = "";
            this.clmnChangeHotkey.Name = "clmnChangeHotkey";
            this.clmnChangeHotkey.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.clmnChangeHotkey.Text = "...";
            this.clmnChangeHotkey.UseColumnTextForButtonValue = true;
            this.clmnChangeHotkey.Width = 30;
            // 
            // ApplicationSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 492);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.tabMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ApplicationSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDownClick);
            this.tabMain.ResumeLayout(false);
            this.tabpGeneral.ResumeLayout(false);
            this.grpbDisplay.ResumeLayout(false);
            this.grpbDisplay.PerformLayout();
            this.grpbLanguage.ResumeLayout(false);
            this.grpbMouseHotkeys.ResumeLayout(false);
            this.grpbMouseHotkeys.PerformLayout();
            this.tabpMenu.ResumeLayout(false);
            this.grpbHotkeys.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvHotkeys)).EndInit();
            this.tabpMenuSize.ResumeLayout(false);
            this.grpbSizer.ResumeLayout(false);
            this.grpbWindowSize.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvWindowSize)).EndInit();
            this.tabpMenuStart.ResumeLayout(false);
            this.grpbStartProgram.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvStartProgram)).EndInit();
            this.tabpMenuDimmer.ResumeLayout(false);
            this.grpbDimmerColor.ResumeLayout(false);
            this.grpbDimmerColor.PerformLayout();
            this.grpbDimmerTransparency.ResumeLayout(false);
            this.grpbDimmerTransparency.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackbDimmerTransparency)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabpGeneral;
        private System.Windows.Forms.TabPage tabpMenuStart;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ToolTip toolTipAddProcessName;
        private System.Windows.Forms.GroupBox grpbStartProgram;
        private System.Windows.Forms.Button btnAddStartProgram;
        private System.Windows.Forms.DataGridView gvStartProgram;
        private System.Windows.Forms.Button btnStartProgramDown;
        private System.Windows.Forms.Button btnStartProgramUp;
        private System.Windows.Forms.GroupBox grpbMouseHotkeys;
        private System.Windows.Forms.TabPage tabpMenu;
        private System.Windows.Forms.GroupBox grpbHotkeys;
        private System.Windows.Forms.DataGridView gvHotkeys;
        private System.Windows.Forms.TabPage tabpMenuSize;
        private System.Windows.Forms.GroupBox grpbWindowSize;
        private System.Windows.Forms.Button btnWindowSizeDown;
        private System.Windows.Forms.Button btnWindowSizeUp;
        private System.Windows.Forms.Button btnAddWindowSize;
        private System.Windows.Forms.DataGridView gvWindowSize;
        private System.Windows.Forms.GroupBox grpbLanguage;
        private System.Windows.Forms.GroupBox grpbSizer;
        private System.Windows.Forms.ComboBox cmbSizer;
        private System.Windows.Forms.Button btnMenuItemDown;
        private System.Windows.Forms.Button btnMenuItemUp;
        private System.Windows.Forms.GroupBox grpbDisplay;
        private System.Windows.Forms.CheckBox chkEnableHighDPI;
        private System.Windows.Forms.Label lblMouseButton;
        private System.Windows.Forms.ComboBox cmbMouseButton;
        private System.Windows.Forms.Label lblKey4;
        private System.Windows.Forms.ComboBox cmbKey4;
        private System.Windows.Forms.Label lblKey3;
        private System.Windows.Forms.Label lblKey2;
        private System.Windows.Forms.Label lblKey1;
        private System.Windows.Forms.ComboBox cmbKey3;
        private System.Windows.Forms.ComboBox cmbKey2;
        private System.Windows.Forms.ComboBox cmbKey1;
        private System.Windows.Forms.ListBox listBoxLanguage;
        private Controls.DataGridViewDisableButtonColumn dataGridViewDisableButtonColumn1;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn1;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn2;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn3;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn4;
        private System.Windows.Forms.TabPage tabpMenuDimmer;
        private System.Windows.Forms.GroupBox grpbDimmerColor;
        private System.Windows.Forms.TextBox txtDimmerColor;
        private System.Windows.Forms.Button btnChooseDimmerColor;
        private System.Windows.Forms.GroupBox grpbDimmerTransparency;
        private System.Windows.Forms.Label lblTransparencyToValue;
        private System.Windows.Forms.Label lblTransparencyFromValue;
        private System.Windows.Forms.Label lblTransparencyValue;
        private System.Windows.Forms.TrackBar trackbDimmerTransparency;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmWindowSizeTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmWindowSizeLeft;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmWindowSizeTop;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmWindowSizeWidth;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmWindowSizeHeight;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmWindowSizeHotKey;
        private System.Windows.Forms.DataGridViewButtonColumn clmWindowSizeEdit;
        private System.Windows.Forms.DataGridViewButtonColumn clmWindowSizeDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmStartProgramTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmStartProgramPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmStartProgramArguments;
        private System.Windows.Forms.DataGridViewButtonColumn clmStartProgramEdit;
        private System.Windows.Forms.DataGridViewButtonColumn clmStartProgramDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmnMenuItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmnHotkeys;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clmnShow;
        private Controls.DataGridViewDisableButtonColumn clmnChangeHotkey;
    }
}