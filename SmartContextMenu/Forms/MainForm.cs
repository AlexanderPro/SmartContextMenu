using System;
using System.Windows.Forms;

namespace SmartContextMenu.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            Visible = false;
            ShowInTaskbar = false;
            Opacity = 0;

            base.OnLoad(e);
        }
    }
}
