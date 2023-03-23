using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MeunControl
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void listBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right )
            {
                // 是否再空白处点击
                int index = listBox1.IndexFromPoint(e.Location);
                if(index > 0 )
                {
                    listBox1.SetSelected(index, true);

                    UpdateToolStripMenuItem.Enabled = true;
                    DelToolStripMenuItem.Enabled = true;
                }

                else
                {
                    UpdateToolStripMenuItem.Enabled = false;
                    DelToolStripMenuItem.Enabled = false;
                }

                contextMenuStrip1.Show(listBox1, e.Location);
            }
        }

        private void AddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add("New Student");
        }
    }
}
