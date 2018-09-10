using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private bool _showSplash;
        private SplashScreen _splashScreen;

        Point mouseOff;//鼠标移动位置变量  
        bool leftFlag;//标签是否为左键 

        public Form1()
        {
            InitializeComponent();

            SetSplashScreen();
        }
        private void SetSplashScreen()
        {

            _showSplash = true;
            _splashScreen = new SplashScreen();

            ResizeSplash();
            _splashScreen.Visible = true;
            _splashScreen.TopMost = true;

            Timer _timer = new Timer();
            _timer.Tick += (sender, e) =>
            {
                _splashScreen.Visible = false;
                _timer.Enabled = false;
                _showSplash = false;
            };
            _timer.Interval = 1000;
            _timer.Enabled = true;
        }

        private void ResizeSplash()
        {
            if (_showSplash)
            {

                var centerXMain = (this.Location.X + this.Width) / 2.0;
                var LocationXSplash = Math.Max(0, centerXMain - (_splashScreen.Width / 2.0));

                var centerYMain = (this.Location.Y + this.Height) / 2.0;
                var LocationYSplash = Math.Max(0, centerYMain - (_splashScreen.Height / 2.0));

                _splashScreen.Location = new Point((int)Math.Round(LocationXSplash), (int)Math.Round(LocationYSplash));
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //得到鼠标相对于窗体左上角的 x 坐标和 y 坐标（以像素为单位）  
                //加负号是便于后续窗体移动时计算左上角的坐标  
                mouseOff = new Point(-e.X, -e.Y);
                leftFlag = true;                  //点击左键按下时标注为true;  
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(mouseOff.X, mouseOff.Y);  //设置移动后的位置  
                Location = mouseSet;
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                leftFlag = false;//释放鼠标后标注为false;  
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {         
            this.Hide();
            Form2 f2 = new Form2();
            f2.Show();
            
            
        }

        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int num = 0;
             num++;
            if (num == 2)
            {
                this.Show();
            }
            else {
                this.Hide();
            }
            
        }

        private void ShowForm()
        {
            //this.Show();
            if (this.Visible)
                显示ToolStripMenuItem.Text = "显示";
            else
                关闭toolStripMenuItem1.Text = "关闭";

            this.Visible = !this.Visible;
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
 }