using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm
{
    public partial class Form1 : Form
    {
        private long receive_count = 0; // 接收字节计数，作用相当于全局变量
        private long send_count = 0; // 接收字节计数，作用相当于全局变量
        private StringBuilder sb = new StringBuilder(); // 为了避免重复调用
        private DateTime current_time = new DateTime();
        public Form1()
        {
            InitializeComponent();
        }
        private void HandleError(Exception ex)
        {
            // 捕获可能发生的异常并进行处理

            // 捕获到异常，创建一个新的对象，之前的不可以再用
            serialPort1.Close();
            serialPort1 = new SerialPort();
            // 刷新COM口选项
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(SerialPort.GetPortNames());
            // 响铃并显示异常给用户
            System.Media.SystemSounds.Beep.Play();
            button1.Text = "打开串口";
            button1.BackColor = Color.Aqua;

            LogCore.CreateInstance().AsyncLog(ex.Message);
            MessageBox.Show(ex.Message);
            comboBox1.Enabled = true;
            comboBox2.Enabled = true;
            comboBox3.Enabled = true;
            comboBox4.Enabled = true;
            comboBox5.Enabled = true;
            button6.Enabled = false;
        }
        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int i;
            // 单个添加 
            for (i = 300; i <= 38400; i = i * 2)
            {
                comboBox2.Items.Add(i.ToString()); // 添加波特率列表
            }
            comboBox1.Items.AddRange(SerialPort.GetPortNames());
            comboBox1.Text = comboBox1.Items[0].ToString();
            timer1.Interval = 1000;
            timer1.Start();
        }

        private bool search_port_is_exist(String item, String[] port_list)
        {
            for (int i = 0; i < port_list.Length; i++)
            {
                if (port_list[i].Equals(item))
                {
                    return true;
                }
            }

            return false;
        }

        /* 扫描串口列表并添加到选择框 */
        private void Update_Serial_List()
        {
            try
            {
                /* 搜索串口 */
                string[] cur_port_list = SerialPort.GetPortNames();
                /* 刷新串口列表comboBox */
                int count = comboBox1.Items.Count;
                if (count == 0)
                {
                    //combox中无内容，将当前串口列表全部加入
                    comboBox1.Items.AddRange(cur_port_list);
                    return;
                }
                else
                {
                    //combox中有内容

                    //判断有无新插入的串口
                    for (int i = 0; i < cur_port_list.Length; i++)
                    {
                        if (!comboBox1.Items.Contains(cur_port_list[i]))
                        {
                            //找到新插入串口，添加到combox中
                            comboBox1.Items.Add(cur_port_list[i]);
                        }
                    }

                    //判断有无拔掉的串口
                    for (int i = 0; i < count; i++)
                    {
                        if (!search_port_is_exist(comboBox1.Items[i].ToString(), cur_port_list))
                        {
                            //找到已被拔掉的串口，从combox中移除
                            comboBox1.Items.RemoveAt(i);
                        }
                    }
                }

                /* 如果当前选中项为空，则默认选择第一项 */
                if (comboBox1.Items.Count > 0)
                {
                    if (comboBox1.Text.Equals(""))
                    {
                        //软件刚启动时，列表项的文本值为空
                        comboBox1.Text = comboBox1.Items[0].ToString();
                    }
                }
                else
                {
                    //无可用列表，清空文本值
                    comboBox1.Text = "";
                }


            }
            catch (Exception ex)
            {
                //当下拉框被打开时，修改下拉框会发生异常
                return;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // 将可能产生异常的代码放置在try块中
                // 根据当前串口属性来判断是否打开
                if (serialPort1.IsOpen)
                {
                    //button1.BackgroundImage = Properties.Resources.connection;
                    // 串口已经处于打开状态
                    serialPort1.Close(); // 关闭串口
                    button1.Text = "打开串口";
                    button1.BackColor = Color.Aqua;
                    button2.Enabled = false;
                    button6.Enabled = false;
                    comboBox1.Enabled = true;
                    comboBox2.Enabled = true;
                    comboBox3.Enabled = true;
                    comboBox4.Enabled = true;
                    comboBox5.Enabled = true;
                    textBox1.Text = ""; // 清空接受区
                    textBox2.Text = ""; // 清空发送区
                    label6.Text = "串口已关闭";
                    label6.ForeColor = Color.Red;
                    checkBox2.Enabled = false;
                    timer1.Interval = 1000;
                    timer1.Start();
                }
                else
                {
                    //button1.BackgroundImage = Properties.Resources.disconnect;
                    // 串口已经处于关闭状态，则设置好串口属性后打开
                    comboBox1.Enabled = false;
                    comboBox2.Enabled = false;
                    comboBox3.Enabled = false;
                    comboBox4.Enabled = false;
                    comboBox5.Enabled = false;
                    serialPort1.PortName = comboBox1.Text;
                    serialPort1.BaudRate = Convert.ToInt32(comboBox2.Text);
                    serialPort1.DataBits = Convert.ToInt16(comboBox3.Text);

                    if (comboBox4.Text.Equals("None"))
                        serialPort1.Parity = System.IO.Ports.Parity.None;
                    else if (comboBox4.Text.Equals("Odd"))
                        serialPort1.Parity = System.IO.Ports.Parity.Odd;
                    else if (comboBox4.Text.Equals("Even"))
                        serialPort1.Parity = System.IO.Ports.Parity.Even;
                    else if (comboBox4.Text.Equals("Mark"))
                        serialPort1.Parity = System.IO.Ports.Parity.Mark;
                    else if (comboBox4.Text.Equals("Space"))
                        serialPort1.Parity = System.IO.Ports.Parity.Space;

                    if (comboBox5.Text.Equals("1"))
                        serialPort1.StopBits = System.IO.Ports.StopBits.One;
                    else if (comboBox5.Text.Equals("1.5"))
                        serialPort1.StopBits = System.IO.Ports.StopBits.OnePointFive;
                    else if (comboBox5.Text.Equals("2"))
                        serialPort1.StopBits = System.IO.Ports.StopBits.Two;

                    serialPort1.ReceivedBytesThreshold = 1;
                    serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);
                    serialPort1.RtsEnable = true;
                    serialPort1.DtrEnable = true;
                    serialPort1.NewLine = "\r\n";
                    serialPort1.WriteTimeout = 3000;

                    serialPort1.Open();
                    serialPort1.DiscardOutBuffer();
                    serialPort1.DiscardInBuffer();
                    button1.Text = "关闭串口";
                    button1.BackColor = Color.Firebrick;
                    button2.Enabled = true;
                    button6.Enabled = true;
                    label6.Text = "串口已打开";
                    label6.ForeColor = Color.Green;
                    checkBox2.Enabled = true;
                    timer1.Stop();
                }
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] temp = new byte[1];
            /* 记录发送数据*/
            // 先检查当前是否存在该项

            try
            {
                // 首先判断串口是否打开
                if (serialPort1.IsOpen)
                {
                    int num = 0; // 定义本次发送字节数
                    // 串口处于开启窗台，将发送区文本发送

                    // 判断发送模式
                    if (radioButton3.Checked)
                    {
                        // 以HEX模式发送
                        // 首先需要用正则表达式将用户输入字符中的十六进制字符匹配出来
                        string buf = textBox2.Text;
                        Regex rgx = new Regex(@"\s");
                        string send_data = rgx.Replace(buf, "");

                        num = (send_data.Length - send_data.Length % 2) / 2;
                        for (int i = 0; i < num; i++)
                        {
                            temp[0] = Convert.ToByte(send_data.Substring(i * 2, 2), 32);
                            serialPort1.Write(temp, 0, 1); // 循环发送
                        }
                        // 如果用户输入的字符是奇数，则单独处理
                        if (send_data.Length % 2 != 0)
                        {
                            temp[0] = Convert.ToByte(send_data.Substring(textBox2.Text.Length - 1, 1), 32);
                            serialPort1.Write(temp, 0, 1);
                            num++;
                        }
                        if (checkBox1.Checked)
                        {
                            // 自动发送新行
                            serialPort1.WriteLine("");
                        }
                    }
                    else
                    {
                        // 以ASCII模式发送
                        // 判断是否需要发送新行
                        if (checkBox1.Checked)
                        {
                            serialPort1.WriteLine(textBox2.Text);
                            num = textBox2.Text.Length + 2;
                        }
                        else
                        {
                            serialPort1.Write(textBox2.Text);
                            num = textBox2.Text.Length;
                        }
                    }
                    send_count += num;
                    label7.Text = "Tx：" + send_count.ToString() + " Bytes"; // 刷新界面
                    if (comboBox6.Items.Contains(textBox2.Text) == true)
                    {
                        return;
                    }
                    else
                    {
                        comboBox6.Items.Add(textBox2.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(100);
            byte[] received_buf = new byte[serialPort1.BytesToRead];
            serialPort1.Read(received_buf, 0, received_buf.Length);

            receive_count += received_buf.Length; // 接收字节计数变量增加num

            if (Utils.Utils.IsContains(received_buf, new byte[] { 0x55, 0x00, 0x00, 0x00, 0x55 }))
                MessageBox.Show("连接质谱仪成功");
            sb.Clear(); // 防止出错，首先清空字符串构造器
            if (radioButton2.Checked)
            {
                // 选中HEX模式显示
                foreach (byte b in received_buf)
                {
                    sb.Append(b.ToString("X2") + " ");
                }
            }
            else
            {
                sb.Append(Encoding.ASCII.GetString(received_buf)); // 将整个数组解码为ASCII数组
            }

            try
            {
                // 因为要访问UI资源，所以需要使用invoke方式同步UI
                Invoke((EventHandler)delegate
                {
                    if (checkBox3.Checked)
                    {
                        current_time = DateTime.Now;
                        textBox1.AppendText(current_time.ToString("HH:mm:ss") + "  " + sb.ToString());
                    }
                    else
                    {
                        textBox1.AppendText(sb.ToString());
                    }
                    label8.Text = $"RX:" + receive_count.ToString() + " Byets";
                });
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            label8.Text = $"Rx" + 0 + " Bytes";
            label7.Text = $"Tx" + 0 + " Bytes";
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                // 自动发送功能选中，开始自动发送
                numericUpDown1.Enabled = false;
                timer1.Interval = (int)numericUpDown1.Value; // 定时器赋初值
                timer1.Start();
                label6.Text = "串口已打开" + " 自动发送中...";
            }
            else
            {
                numericUpDown1.Enabled = true;
                timer1.Stop();
                label6.Text = "串口已打开";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                button2_Click(button2, new EventArgs()); // 调用发送按钮回调函数
            }
            else
            {
                Update_Serial_List();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DateTime time = new DateTime();
            string fileName;

            /* 获取当前接收区内容 */
            string recv_data = textBox1.Text;

            if (recv_data.Equals(""))
            {
                MessageBox.Show("接收数据为空，无需保存");
                return;
            }

            /*  获取当前时间，用于填充文件名 */
            time = DateTime.Now;
            string _dirPath = $"{Environment.CurrentDirectory}/SaveData/{DateTime.Now.ToString("yyyy_MM")}";
            if (!Directory.Exists(_dirPath)) { Directory.CreateDirectory(_dirPath); };
            string FilePath = $"{_dirPath}/{time.ToString("dd_HH_mm_ss")} .txt";
            if (!File.Exists(FilePath)) { File.Create(FilePath).Close(); };

            try
            {
                /* 保存串口接收区的内容 */
                // 创建 FileStream 类的实例

                using (StreamWriter sw = new StreamWriter(FilePath))
                {
                    sw.WriteLineAsync(recv_data);
                }
                //FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate,FileAccess.ReadWrite,FileShare.ReadWrite);

                //// 将字符串转换为字节数组
                //byte[] bytes = Encoding.UTF8.GetBytes(recv_data);

                //// 向文件中写入字节数组
                //fileStream.Write(bytes, 0, bytes.Length);

                //// 刷新缓冲区
                //fileStream.Flush();

                //// 关闭流
                //fileStream.Close();

                MessageBox.Show($"日志已保存！ 路径：{FilePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"发生异常！({ex})");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string file;

            /* 弹出文件选择框供用户选择*/
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Title = "请选择要加载的文件（文本格式）";
            openFileDialog.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                file = openFileDialog.FileName;
                textBox2.AppendText(file);
            }
            else
            {
                return;
            }

            /* 读取文件内容*/
            try
            {
                // 清空发送缓冲区
                textBox2.Text = "";

                // 使用 StreamReader 来读取文件
                using (StreamReader sr = new StreamReader(file))
                {
                    string line;

                    // 从文件读取并显示行，直到文件的末尾
                    while ((line = sr.ReadLine()) != null)
                    {
                        line = line + "\r\n";
                        textBox2.AppendText($"{line}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载文件发生异常！({ex})");
            }
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox2.Text = comboBox6.SelectedItem.ToString();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel1.Links[linkLabel1.Links.IndexOf(e.Link)].Visited = true;
            string targetUrl = "https://github.com/Hnzh612/financialsystem";


            try
            {
                //尝试用edge打开
                System.Diagnostics.Process.Start("msedge.exe", targetUrl);
                return;
            }
            catch (Exception)
            {
                //edge它不香吗
            }

            try
            {
                //好吧，那用chrome
                System.Diagnostics.Process.Start("chrome.exe", targetUrl);
                return;
            }
            catch
            {
                //chrome不好用吗
            }
            try
            {
                //IE也不是不可以
                System.Diagnostics.Process.Start("iexplore.exe", targetUrl);
            }

            catch
            {
                //没救了，砸了吧！
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            byte[] sendData = new byte[] { 0xAA, 0x00, 0x00, 0xAA };
            serialPort1.Write(sendData, 0, sendData.Length);
        }
    }
}
