using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Form_Table
{
    public partial class Form1 : Form
    {
        public class Student
        {
            public int Id;
            public string Name;
            public int Sex;
            public string Mobile;

            public Student() { }

            public Student(int id, string name, int sex, string mobile)
            {
                Id = id;
                Name = name;
                Sex = sex;
                Mobile = mobile;
            }
        }
        public Form1()
        {
            InitializeComponent();
            InitGridView();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void InitGridView()
        {
            dataGridView1.ColumnCount = 4;
            dataGridView1.Columns[0].Name = "学号";
            dataGridView1.Columns[1].Name = "姓名";
            dataGridView1.Columns[2].Name = "性别";
            dataGridView1.Columns[3].Name = "联系方式";
            DataGridViewButtonColumn dbColumn = new DataGridViewButtonColumn();
            dbColumn.HeaderText = "操作";
            dbColumn.UseColumnTextForButtonValue = true;
            dbColumn.Text = "下单";
            DataGridViewCheckBoxColumn cbColumn = new DataGridViewCheckBoxColumn();
            cbColumn.HeaderText = "选择";
            dataGridView1 .Columns.Add(dbColumn);
            dataGridView1 .Columns.Add(cbColumn);

            AddRow(new Student(1719640317, "user1", 0, "15120376892"));
            AddRow(new Student(1719640318, "user2", 1, "15120376893"));
            AddRow(new Student(1719640319, "user3", 2, "15120376894"));
            AddRow(new Student(1719640320, "user4", 1, "15120376895"));
        }

        private void AddRow(Student student)
        {
            string temp = "";
            switch (student.Sex)
            {
                case 0:
                    temp = "未知";break;
                case 1:
                    temp = "男"; break;
                case 2:
                    temp = "女"; break;
            }
            object[] row =
            {
                student.Id,
                student.Name,
                temp,
                student.Mobile,
            };
            dataGridView1.Rows.Add(row);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // 取消默认选中第一个
            dataGridView1.Rows[0].Selected = false;
        }
    }
}
