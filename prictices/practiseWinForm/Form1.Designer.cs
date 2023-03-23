namespace practiseWinForm
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        ///  必须的设计器变量
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        ///  清理所有正在使用的资源
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            comboBox5 = new ComboBox();
            comboBox4 = new ComboBox();
            comboBox3 = new ComboBox();
            comboBox2 = new ComboBox();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            comboBox1 = new ComboBox();
            label1 = new Label();
            panel2 = new Panel();
            panel5 = new Panel();
            panel6 = new Panel();
            panel3 = new Panel();
            panel4 = new Panel();
            panel7 = new Panel();
            panel8 = new Panel();
            panel9 = new Panel();
            button1 = new Button();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            button2 = new Button();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel5.SuspendLayout();
            panel8.SuspendLayout();
            panel9.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(button1);
            panel1.Controls.Add(comboBox5);
            panel1.Controls.Add(comboBox4);
            panel1.Controls.Add(comboBox3);
            panel1.Controls.Add(comboBox2);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(comboBox1);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(0, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(126, 211);
            panel1.TabIndex = 0;
            // 
            // comboBox5
            // 
            comboBox5.FormattingEnabled = true;
            comboBox5.Items.AddRange(new object[] { "1", "1.5", "2" });
            comboBox5.Location = new Point(50, 128);
            comboBox5.Name = "comboBox5";
            comboBox5.Size = new Size(69, 25);
            comboBox5.TabIndex = 9;
            comboBox5.Text = "1";
            // 
            // comboBox4
            // 
            comboBox4.FormattingEnabled = true;
            comboBox4.Items.AddRange(new object[] { "None" });
            comboBox4.Location = new Point(50, 97);
            comboBox4.Name = "comboBox4";
            comboBox4.Size = new Size(69, 25);
            comboBox4.TabIndex = 8;
            comboBox4.Text = "None";
            // 
            // comboBox3
            // 
            comboBox3.FormattingEnabled = true;
            comboBox3.Items.AddRange(new object[] { "8" });
            comboBox3.Location = new Point(50, 66);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(69, 25);
            comboBox3.TabIndex = 7;
            comboBox3.Text = "8";
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "115200" });
            comboBox2.Location = new Point(50, 35);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(69, 25);
            comboBox2.TabIndex = 6;
            comboBox2.Text = "115200";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(2, 132);
            label5.Name = "label5";
            label5.Size = new Size(44, 17);
            label5.TabIndex = 5;
            label5.Text = "停止位";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(2, 101);
            label4.Name = "label4";
            label4.Size = new Size(44, 17);
            label4.TabIndex = 4;
            label4.Text = "检验位";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(2, 70);
            label3.Name = "label3";
            label3.Size = new Size(44, 17);
            label3.TabIndex = 3;
            label3.Text = "数据位";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(2, 39);
            label2.Name = "label2";
            label2.Size = new Size(44, 17);
            label2.TabIndex = 2;
            label2.Text = "波特率";
            label2.Click += label2_Click;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "COM10" });
            comboBox1.Location = new Point(50, 4);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(69, 25);
            comboBox1.TabIndex = 1;
            comboBox1.Text = "COM10";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 9);
            label1.Name = "label1";
            label1.Size = new Size(40, 17);
            label1.TabIndex = 0;
            label1.Text = "串  口";
            // 
            // panel2
            // 
            panel2.Controls.Add(panel5);
            panel2.Controls.Add(panel3);
            panel2.Location = new Point(0, 229);
            panel2.Name = "panel2";
            panel2.Size = new Size(126, 61);
            panel2.TabIndex = 1;
            // 
            // panel5
            // 
            panel5.Controls.Add(panel6);
            panel5.Location = new Point(2, 73);
            panel5.Name = "panel5";
            panel5.Size = new Size(126, 61);
            panel5.TabIndex = 3;
            panel5.Paint += panel5_Paint;
            // 
            // panel6
            // 
            panel6.Location = new Point(1, 70);
            panel6.Name = "panel6";
            panel6.Size = new Size(126, 61);
            panel6.TabIndex = 2;
            // 
            // panel3
            // 
            panel3.Location = new Point(1, 70);
            panel3.Name = "panel3";
            panel3.Size = new Size(126, 61);
            panel3.TabIndex = 2;
            // 
            // panel4
            // 
            panel4.Location = new Point(0, 298);
            panel4.Name = "panel4";
            panel4.Size = new Size(126, 61);
            panel4.TabIndex = 2;
            panel4.Paint += panel4_Paint;
            // 
            // panel7
            // 
            panel7.Location = new Point(0, 366);
            panel7.Name = "panel7";
            panel7.Size = new Size(443, 23);
            panel7.TabIndex = 3;
            // 
            // panel8
            // 
            panel8.Controls.Add(textBox1);
            panel8.Location = new Point(132, 12);
            panel8.Name = "panel8";
            panel8.Size = new Size(305, 248);
            panel8.TabIndex = 4;
            // 
            // panel9
            // 
            panel9.Controls.Add(button2);
            panel9.Controls.Add(textBox2);
            panel9.Location = new Point(132, 266);
            panel9.Name = "panel9";
            panel9.Size = new Size(305, 91);
            panel9.TabIndex = 5;
            // 
            // button1
            // 
            button1.BackColor = Color.Lime;
            button1.Location = new Point(3, 163);
            button1.Name = "button1";
            button1.Size = new Size(116, 37);
            button1.TabIndex = 10;
            button1.Text = "打开串口";
            button1.UseVisualStyleBackColor = false;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(3, 4);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.Size = new Size(298, 241);
            textBox1.TabIndex = 0;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(3, 3);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.ScrollBars = ScrollBars.Vertical;
            textBox2.Size = new Size(228, 85);
            textBox2.TabIndex = 0;
            // 
            // button2
            // 
            button2.Location = new Point(253, 21);
            button2.Name = "button2";
            button2.Size = new Size(48, 41);
            button2.TabIndex = 1;
            button2.Text = "发送";
            button2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(445, 394);
            Controls.Add(panel9);
            Controls.Add(panel8);
            Controls.Add(panel7);
            Controls.Add(panel4);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel8.ResumeLayout(false);
            panel8.PerformLayout();
            panel9.ResumeLayout(false);
            panel9.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Panel panel5;
        private Panel panel6;
        private Panel panel3;
        private Panel panel4;
        private Panel panel7;
        private ComboBox comboBox1;
        private Label label1;
        private Panel panel8;
        private Panel panel9;
        private Label label2;
        private ComboBox comboBox5;
        private ComboBox comboBox4;
        private ComboBox comboBox3;
        private ComboBox comboBox2;
        private Label label5;
        private Label label4;
        private Label label3;
        private Button button1;
        private TextBox textBox1;
        private Button button2;
        private TextBox textBox2;
    }
}