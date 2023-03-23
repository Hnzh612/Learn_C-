using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace progress_bar
{
    public partial class Form1 : Form
    {
        private Thread th;
        public Form1()
        {
            InitializeComponent();
        }

        private void ThreadTask()
        {
            int stp;
            int newval;
            Random rnd = new Random();

            while (true)
            {
                stp = this.progressBar1.Step * rnd.Next(-1, 2);
                newval = this.progressBar1.Value + stp;
                if (newval > this.progressBar1.Maximum)
                    newval = this.progressBar1.Maximum;
                else if (newval < this.progressBar1.Minimum)
                    newval = this.progressBar1.Minimum;
                this.progressBar1.Value = newval;
                Thread.Sleep(100);
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "开始")
            {
                button1.Text = "停止";
                th = new Thread(new ThreadStart((delegate { Control.CheckForIllegalCrossThreadCalls = false; ThreadTask(); })));
                th.IsBackground = true;
                th.Start();
            }
            else
            {
                th.Abort();
                button1.Text = "开始";
            }
        }

    }
}
