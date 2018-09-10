namespace WindowsFormsApp1.Dummy
{
    partial class Future
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(47, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(237, 58);
            this.label1.TabIndex = 0;
            this.label1.Text = "In the future:\r\n未来功能：\r\n";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(48, 143);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 684);
            this.label2.TabIndex = 0;
            this.label2.Text = "功能\r\n2D\r\n2.5D\r\n3D\r\n格式转换\r\nmars火星\r\nearth地球\r\nsun太阳\r\n动作捕捉技术\r\n录制宏\r\n暂停宏\r\n播放宏\r\n曲线建模\r\n弧线绘制" +
    "\r\n脚本编辑\r\n渲染\r\n材质\r\n动画\r\n轴\r\n漫游\r\n模型\r\n全球定位系统\r\n加强打印\r\n黑屏定位\r\n屏蔽\r\n加密功能\r\n外部扩展\r\n预留编码扩展\r\n预留食物\r" +
    "\n预留人\r\n预留\r\n圆弧尺寸校准\r\n基点校准\r\n \r\n智能钥匙\r\n\r\n";
            // 
            // Future
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 826);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Future";
            this.Text = "Future";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}