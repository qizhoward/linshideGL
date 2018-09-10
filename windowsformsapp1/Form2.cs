using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using WindowsFormsApp1.Dummy;


namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        private bool m_bSaveLayout = true;
        private DeserializeDockContent m_deserializeDockContent;
        private SolutionExplorer m_solutionExplorer;
        private PropertyWindow m_propertyWindow;
        private Toolbox m_toolbox;
        private OutputWindow m_outputWindow;
        private DrawWindow m_drawWindow;
        private TaskList m_taskList;

        public Form2()
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Dpi;

            CreateStandardControls();

            showRightToLeft.Checked = (RightToLeft == RightToLeft.Yes);
            RightToLeftLayout = showRightToLeft.Checked;
            m_solutionExplorer.RightToLeftLayout = RightToLeftLayout;
            m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);

            vsToolStripExtender1.DefaultRenderer = _toolStripProfessionalRenderer;

        }

        #region Methods

        private IDockContent FindDocument(string text)
        {
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                foreach (Form form in MdiChildren)
                    if (form.Text == text)
                        return form as IDockContent;

                return null;
            }
            else
            {
                foreach (IDockContent content in dockPanel.Documents)
                    if (content.DockHandler.TabText == text)
                        return content;

                return null;
            }
        }

        private Doc CreateNewDocument()
        {
            Doc Doc = new Doc();

            int count = 1;
            string text = $"Document{count}";
            while (FindDocument(text) != null)
            {
                count++;
                text = $"Document{count}";
            }

            Doc.Text = text;
            return Doc;
        }

        private Doc CreateNewDocument(string text)
        {
            Doc Doc = new Doc();
            Doc.Text = text;
            return Doc;
        }

        private void CloseAllDocuments()
        {
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                foreach (Form form in MdiChildren)
                    form.Close();
            }
            else
            {
                foreach (IDockContent document in dockPanel.DocumentsToArray())
                {
                    // IMPORANT: dispose all panes.
                    document.DockHandler.DockPanel = null;
                    document.DockHandler.Close();
                }
            }
        }

        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(SolutionExplorer).ToString())
                return m_solutionExplorer;
            else if (persistString == typeof(PropertyWindow).ToString())
                return m_propertyWindow;
            else if (persistString == typeof(Toolbox).ToString())
                return m_toolbox;
            else if (persistString == typeof(OutputWindow).ToString())
                return m_outputWindow;
            else if (persistString == typeof(TaskList).ToString())
                return m_taskList;


            else
            {
                // DummyDoc overrides GetPersistString to add extra information into persistString.
                // Any DockContent may override this value to add any needed information for deserialization.

                string[] parsedStrings = persistString.Split(new char[] { ',' });
                if (parsedStrings.Length != 3)
                    return null;

                if (parsedStrings[0] != typeof(Doc).ToString())
                    return null;

                Doc dummyDoc = new Doc();
                if (parsedStrings[1] != string.Empty)
                    dummyDoc.FileName = parsedStrings[1];
                if (parsedStrings[2] != string.Empty)
                    dummyDoc.Text = parsedStrings[2];

                return dummyDoc;
            }
        }

        private void CloseAllContents()
        {
            // we don't want to create another instance of tool window, set DockPanel to null
            m_solutionExplorer.DockPanel = null;
            m_propertyWindow.DockPanel = null;
            m_toolbox.DockPanel = null;
            m_outputWindow.DockPanel = null;
            m_taskList.DockPanel = null;


            // Close all other document windows
            CloseAllDocuments();

            // IMPORTANT: dispose all float windows.
            foreach (var window in dockPanel.FloatWindows.ToList())
                window.Dispose();

            System.Diagnostics.Debug.Assert(dockPanel.Panes.Count == 0);
            System.Diagnostics.Debug.Assert(dockPanel.Contents.Count == 0);
            System.Diagnostics.Debug.Assert(dockPanel.FloatWindows.Count == 0);
        }

        private readonly ToolStripRenderer _toolStripProfessionalRenderer = new ToolStripProfessionalRenderer();

        private void SetSchema(object sender, System.EventArgs e)
        {
            // Persist settings when rebuilding UI
            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.temp.config");

            dockPanel.SaveAsXml(configFile);
            CloseAllContents();

            if (sender == this.menuItemSchemaVS2015Dark)
            {
                this.dockPanel.Theme = this.vS2015DarkTheme1;
                this.EnableVSRenderer(VisualStudioToolStripExtender.VsVersion.Vs2015, vS2015DarkTheme1);
            }

          
            else if (sender == this.menuItemSchemaVS2015Blue)
            {
                this.dockPanel.Theme = this.vS2015BlueTheme1;
                this.EnableVSRenderer(VisualStudioToolStripExtender.VsVersion.Vs2015, vS2015BlueTheme1);
            }

          
            menuItemSchemaVS2015Blue.Checked = (sender == menuItemSchemaVS2015Blue);
            menuItemSchemaVS2015Dark.Checked = (sender == menuItemSchemaVS2015Dark);
            if (dockPanel.Theme.ColorPalette != null)
            {
                statusBar.BackColor = dockPanel.Theme.ColorPalette.MainWindowStatusBarDefault.Background;
            }

            if (File.Exists(configFile))
                dockPanel.LoadFromXml(configFile, m_deserializeDockContent);
        }

        private void EnableVSRenderer(VisualStudioToolStripExtender.VsVersion version, ThemeBase theme)
        {
            vsToolStripExtender1.SetStyle(mainMenu, version, theme);
            vsToolStripExtender1.SetStyle(statusBar, version, theme);
        }


        private void SetDocumentStyle(object sender, System.EventArgs e)
        {
            DocumentStyle oldStyle = dockPanel.DocumentStyle;
            DocumentStyle newStyle;
             if (sender == menuItemDockingWindow)
                newStyle = DocumentStyle.DockingWindow;
            else 
                newStyle = DocumentStyle.DockingWindow;

            if (oldStyle == newStyle)
                return;

            if (oldStyle == DocumentStyle.DockingWindow || newStyle == DocumentStyle.DockingWindow)
                CloseAllDocuments();

            dockPanel.DocumentStyle = newStyle;
      
            menuItemDockingWindow.Checked = (newStyle == DocumentStyle.DockingWindow);
          
          
            menuItemLayoutByCode.Enabled = (newStyle != DocumentStyle.SystemMdi);

        }

        #endregion

        #region Event Handlers

        private void MenuItemExit_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void MenuItemSolutionExplorer_Click(object sender, System.EventArgs e)
        {
            m_solutionExplorer.Show(dockPanel);
        }

        private void MenuItemPropertyWindow_Click(object sender, System.EventArgs e)
        {
            m_propertyWindow.Show(dockPanel);
        }

        private void MenuItemToolbox_Click(object sender, System.EventArgs e)
        {
            m_toolbox.Show(dockPanel);
        }

        private void MenuItemOutputWindow_Click(object sender, System.EventArgs e)
        {
            m_outputWindow.Show(dockPanel);
        }

        private void MenuItemDrawWindow_Click(object sender, EventArgs e)
        {
            m_drawWindow.Show(dockPanel);
           
        }


        private void MenuItemTaskList_Click(object sender, System.EventArgs e)
        {
            m_taskList.Show(dockPanel);
        }

        private void MenuItemAbout_Click(object sender, System.EventArgs e)
        {
            AboutDialog aboutDialog = new AboutDialog();
            aboutDialog.ShowDialog(this);
        }

        private void MenuItemNew_Click(object sender, System.EventArgs e)
        {
            Doc Doc = CreateNewDocument();
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                Doc.MdiParent = this;
                Doc.Show();
            }
            else
                Doc.Show(dockPanel);
        }

        private void MenuItemOpen_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();

            openFile.InitialDirectory = Application.ExecutablePath;
            openFile.Filter =
                "rtf files (*.rtf)|*.rtf|" +
                "txt files (*.txt)|*.txt|" +
                "Revit project files(*.rvt)| *.rvt |" +
                "Revit family files(*.rfa)|*.rfa |" +
                "Revit family template files(*.rft) | *.rft |" +
                "All files(=.=) | *.* ";
            openFile.FilterIndex = 1;
            openFile.RestoreDirectory = true;

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                string fullName = openFile.FileName;
                string fileName = Path.GetFileName(fullName);

                if (FindDocument(fileName) != null)
                {
                    MessageBox.Show("The document: " + fileName + " has already opened!");
                    return;
                }

                Doc Doc = new Doc();
                Doc.Text = fileName;
                if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
                {
                    Doc.MdiParent = this;
                    Doc.Show();
                }
                else
                    Doc.Show(dockPanel);
                try
                {
                    Doc.FileName = fullName;
                }
                catch (Exception exception)
                {
                    Doc.Close();
                    MessageBox.Show(exception.Message);
                }

            }
        }

        private void MenuItemPrint_Click(object sender, System.EventArgs e)
        {
            PrintDialog print = new PrintDialog();
            print.ShowDialog();

        }

        private void MenuItemFile_Popup(object sender, System.EventArgs e)
        {

        }

        private void MenuItemClose_Click(object sender, System.EventArgs e)
        {
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
                ActiveMdiChild.Close();
            else if (dockPanel.ActiveDocument != null)
                dockPanel.ActiveDocument.DockHandler.Close();
        }

        private void MenuItemCloseAll_Click(object sender, System.EventArgs e)
        {
            CloseAllDocuments();
        }

        private void Form2_Load(object sender, System.EventArgs e)
        {
            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");

            if (File.Exists(configFile))
                dockPanel.LoadFromXml(configFile, m_deserializeDockContent);
        }

        private void Form2_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");
            if (m_bSaveLayout)
                dockPanel.SaveAsXml(configFile);
            else if (File.Exists(configFile))
                File.Delete(configFile);
        }

        private void MenuItemToolBar_Click(object sender, System.EventArgs e)
        {
            toolStrip.Visible = menuItemToolBar.Checked = !menuItemToolBar.Checked;
        }

        private void MenuItemStatusBar_Click(object sender, System.EventArgs e)
        {
            statusBar.Visible = menuItemStatusBar.Checked = !menuItemStatusBar.Checked;
        }

        private void ToolBar_ButtonClick(object sender, System.Windows.Forms.ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == 新建NToolStripButton)
                MenuItemNew_Click(null, null);
            else if (e.ClickedItem == 打开OToolStripButton)
                MenuItemOpen_Click(null, null);
            else if (e.ClickedItem == 打印PToolStripButton)
                MenuItemPrint_Click(null, null);
        }

        private void MenuItemNewWindow_Click(object sender, System.EventArgs e)
        {
           
            m_drawWindow.Show();

        }

        private void MenuItemTools_Popup(object sender, System.EventArgs e)
        {
            menuItemLockLayout.Checked = !this.dockPanel.AllowEndUserDocking;
        }

        private void MenuItemLockLayout_Click(object sender, System.EventArgs e)
        {
            dockPanel.AllowEndUserDocking = !dockPanel.AllowEndUserDocking;
        }

        private void MenuItemLayoutByCode_Click(object sender, System.EventArgs e)
        {
            dockPanel.SuspendLayout(true);

            CloseAllContents();

            CreateStandardControls();

            m_solutionExplorer.Show(dockPanel, DockState.DockRight);
            m_propertyWindow.Show(m_solutionExplorer.Pane, m_solutionExplorer);
            m_toolbox.Show(dockPanel, new Rectangle(98, 133, 200, 383));
            m_outputWindow.Show(m_solutionExplorer.Pane, DockAlignment.Bottom, 0.35);
            m_taskList.Show(m_toolbox.Pane, DockAlignment.Left, 0.4);
            


            Doc doc1 = CreateNewDocument("Document1");
            Doc doc2 = CreateNewDocument("Document2");
            Doc doc3 = CreateNewDocument("Document3");
            Doc doc4 = CreateNewDocument("Document4");
            doc1.Show(dockPanel, DockState.Document);
            doc2.Show(doc1.Pane, null);
            doc3.Show(doc1.Pane, DockAlignment.Bottom, 0.5);
            doc4.Show(doc3.Pane, DockAlignment.Right, 0.5);

            dockPanel.ResumeLayout(true, true);
        }

        private void CreateStandardControls()
        {
            m_solutionExplorer = new SolutionExplorer();
            m_propertyWindow = new PropertyWindow();
            m_toolbox = new Toolbox();
            m_outputWindow = new OutputWindow();
            m_taskList = new TaskList();
            m_drawWindow = new DrawWindow();
        }

        private void MenuItemCloseAllButThisOne_Click(object sender, System.EventArgs e)
        {
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                Form activeMdi = ActiveMdiChild;
                foreach (Form form in MdiChildren)
                {
                    if (form != activeMdi)
                        form.Close();
                }
            }
            else
            {
                foreach (IDockContent document in dockPanel.DocumentsToArray())
                {
                    if (!document.DockHandler.IsActivated)
                        document.DockHandler.Close();
                }
            }
        }

        private void MenuItemShowDocumentIcon_Click(object sender, System.EventArgs e)
        {
            dockPanel.ShowDocumentIcon = menuItemShowDocumentIcon.Checked = !menuItemShowDocumentIcon.Checked;
        }

        private void ShowRightToLeft_Click(object sender, EventArgs e)
        {
            CloseAllContents();
            if (showRightToLeft.Checked)
            {
                this.RightToLeft = RightToLeft.No;
                this.RightToLeftLayout = false;
            }
            else
            {
                this.RightToLeft = RightToLeft.Yes;
                this.RightToLeftLayout = true;
            }
            m_solutionExplorer.RightToLeftLayout = this.RightToLeftLayout;
            showRightToLeft.Checked = !showRightToLeft.Checked;
        }

        private void ExitWithoutSavingLayout_Click(object sender, EventArgs e)
        {
            m_bSaveLayout = false;
            Close();
            m_bSaveLayout = true;
        }



        #endregion


    

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //  Show a file open dialog.
            OpenFileDialog openFile = new OpenFileDialog();
            

            openFile.InitialDirectory = Application.ExecutablePath;
            openFile.Filter =
                "rtf files (*.rtf)|*.rtf|" +
                "txt files (*.txt)|*.txt|" +
                "Revit project files(*.rvt)| *.rvt |" +
                "Revit family files(*.rfa)|*.rfa |" +
                "Revit family template files(*.rft) | *.rft |" +
                "All files(=.=) | *.* ";
            openFile.FilterIndex = 1;
            openFile.RestoreDirectory = true;

            if (openFile.ShowDialog() == DialogResult.OK)
            {

               

            }
        }

        private void dockPanel_ActiveContentChanged(object sender, EventArgs e)
        {
            m_drawWindow.Show(dockPanel);
        }
    }

}
