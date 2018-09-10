using System;

namespace WindowsFormsApp1.Dummy
{
    public partial class SolutionExplorer : ToolWindow
    {
        public SolutionExplorer()
        {
            InitializeComponent();
        }
        protected override void OnRightToLeftLayoutChanged(EventArgs e)
        {
            treeView1.RightToLeftLayout = RightToLeftLayout;
        }
    }
}
