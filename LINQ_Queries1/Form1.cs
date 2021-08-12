using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LINQ_Queries1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dbo_EntityFrameworkDemoEntities db = new Dbo_EntityFrameworkDemoEntities();
            if (radioButton1.Checked == true)
            {
                dataGridView1.DataSource = db.Tbl_CourseGrades.Select(y => new { Name = y.CourseGrade1 }).Where(x => x.Name <= 50).ToList();
            }
            if (radioButton2.Checked == true)
            {
                if (textBox1.Text != "")
                {
                    dataGridView1.DataSource = db.Tbl_Student.Select(
                        x => new
                        {
                            FirstName = x.FirstName,
                            LastName = x.LastName
                        }).Where(x => x.FirstName == textBox1.Text | x.LastName == textBox1.Text).ToList();
                }
                else
                {
                    MessageBox.Show("Enter name or surname");
                }
            }

        }
    }
}
