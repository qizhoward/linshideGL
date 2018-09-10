namespace WindowsFormsApp1.Dummy
{
    partial class DrawWindow
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
            this.sceneControl = new SharpGL.SceneControl();
            ((System.ComponentModel.ISupportInitialize)(this.sceneControl)).BeginInit();
            this.SuspendLayout();
            // 
            // sceneControl
            // 
            this.sceneControl.Cursor = System.Windows.Forms.Cursors.Default;
            this.sceneControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sceneControl.DrawFPS = false;
            this.sceneControl.Location = new System.Drawing.Point(0, 0);
            this.sceneControl.Name = "sceneControl";
            this.sceneControl.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.sceneControl.RenderContextType = SharpGL.RenderContextType.NativeWindow;
            this.sceneControl.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.sceneControl.Size = new System.Drawing.Size(776, 610);
            this.sceneControl.TabIndex = 0;
            this.sceneControl.OpenGLInitialized += new System.EventHandler(this.sceneControl_OpenGLInitialized);
            this.sceneControl.OpenGLDraw += new SharpGL.RenderEventHandler(this.sceneControl_OpenGLDraw);
            this.sceneControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.sceneControl_KeyDown);
            this.sceneControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.sceneControl_MouseDown);
            this.sceneControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.sceneControl_MouseMove);
            this.sceneControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.sceneControl_MouseUp);
            // 
            // DrawWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 610);
            this.Controls.Add(this.sceneControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DrawWindow";
            this.Text = "DrawWindow";
            this.Resize += new System.EventHandler(this.FormMain_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.sceneControl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private SharpGL.SceneControl sceneControl;
    }
}