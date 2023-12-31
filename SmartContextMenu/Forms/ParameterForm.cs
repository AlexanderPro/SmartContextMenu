﻿using System;
using System.Windows.Forms;

namespace SmartContextMenu.Forms
{
    partial class ParameterForm : Form
    {
        public string ParameterValue { get; private set; }

        public ParameterForm(LanguageManager manager, string parameter)
        {
            InitializeComponent();
            InitializeControls(manager, parameter);
        }

        private void InitializeControls(LanguageManager manager, string parameter)
        {
            lblParameter.Text = parameter;
            btnApply.Text = manager.GetString("parameter_btn_apply");
            Text = manager.GetString("parameter_form");
            DialogResult = DialogResult.Cancel;
        }

        private void ButtonApplyClick(object sender, EventArgs e)
        {
            ParameterValue = txtParameterValue.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void FormKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                ButtonApplyClick(sender, e);
            }

            if (e.KeyValue == 27)
            {
                Close();
            }
        }
    }
}