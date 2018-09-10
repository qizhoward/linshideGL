using System;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace WindowsFormsApp1.Dummy
{
    public partial class AboutDialog : Form
    {
        public AboutDialog()
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Dpi;
        }

        private void AboutDialog_Load(object sender, EventArgs e)
        {
            labelAppVersion.Text = typeof(Form2).Assembly.GetName().Version.ToString();
            labelLibVersion.Text = typeof(DockPanel).Assembly.GetName().Version.ToString();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void labelLibVersion_Click(object sender, EventArgs e)
        {

        }

        private void labelAppVersion_Click(object sender, EventArgs e)
        {

        }
    }
}
