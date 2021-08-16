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
            if (radioButton2.Checked == true)
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
            if (radioButton3.Checked == true)
            {
                var query = from t2 in db.Tbl_Lessons
                            join t1 in db.Tbl_CourseGrades
                            on t2.LessonID equals t1.Lesson
                            join t3 in db.Tbl_Student
                            on t1.Student equals t3.ID
                            select new
                            {
                                Name = t3.FirstName + " " + t3.LastName,
                                Lesson = t2.LessonName,
                                Average = t1.Average,
                                Pass = t1.Average > 50

                            };
                dataGridView1.DataSource = query.ToList();

            }
            if (radioButton4.Checked == true)
            {
                var result = from t1 in db.Tbl_Lessons
                             join t2 in db.Tbl_CourseGrades
                             on t1.LessonID equals t2.Lesson
                             join t3 in db.Tbl_Student
                             on t2.Student equals t3.ID
                             select new
                             {
                                 StundentID = t3.ID,
                                 Name = t3.FirstName + " " + t3.LastName,
                                 Lesson = t1.LessonName,
                                 CourseGrade1 = t2.CourseGrade1,
                                 CourseGrade2 = t2.CourseGrade2,
                                 Average = t2.Average,
                                 IsPassed = t2.Status == true ? "successful" : "unsuccessful"
                             };
                dataGridView1.DataSource = result.ToList();
            }
            if (Rb5.Checked == true)
            {
                var result = db.Tbl_CourseGrades.SelectMany(x => db.Tbl_Student.Where(y => y.ID == x.Student),
                    (x, y) => new {adi= y.FirstName, x.CourseGrade1, x.CourseGrade2, x.Average });
                dataGridView1.DataSource = result.ToList();
            }
            if (RB6.Checked==true)
            {
                var result = db.Tbl_Student.Take(3).ToList();
                dataGridView1.DataSource = result;

            }
            if (RB7.Checked == true)
            {
                var result = db.Tbl_Student.OrderByDescending(x=>x.ID).Take(3).ToList();
                dataGridView1.DataSource = result;

            }





            //else
            //{
            //    MessageBox.Show("Choose Action", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
