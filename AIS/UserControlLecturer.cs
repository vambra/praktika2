using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIS
{
    public partial class UserControlLecturer : UserControl
    {
        private Lecturer user;
        
        public UserControlLecturer(int LecturerId)
        {
            InitializeComponent();
            
            try
            {
                user = new Lecturer();
                user.SetProfile(LecturerId);
                //Suject ComboBox
                Subject subject = new Subject();
                DataTable subjectlist = subject.GetSubjectsByLecturer(user.GetId());
                DataRow dr = subjectlist.NewRow();
                dr["id"] = "0";
                dr["dalykas"] = "Visi dalykai";
                subjectlist.Rows.InsertAt(dr, 0);
                comboBoxSubject.DisplayMember = "dalykas";
                comboBoxSubject.ValueMember = "id";
                comboBoxSubject.DataSource = subjectlist;
                comboBoxSubject.SelectedIndex = 0;
                
                //Group ComboBox
                StudentGroup group = new StudentGroup();
                DataTable grouptlist = group.GetGroupsByLecturer(user.GetId());
                dr = grouptlist.NewRow();
                dr["id"] = "0";
                dr["pavadinimas"] = "Visos grupės";
                grouptlist.Rows.InsertAt(dr, 0);
                comboBoxStudentGroup.DisplayMember = "pavadinimas";
                comboBoxStudentGroup.ValueMember = "id";
                comboBoxStudentGroup.DataSource = grouptlist;
                comboBoxStudentGroup.SelectedIndex = 0;

                //Student ComboBox
                Student student = new Student();
                DataTable studentlist = student.GetStudentsByLecturer(user.GetId());
                dr = studentlist.NewRow();
                dr["id"] = "0";
                dr["vardas"] = "Visi studentai";
                studentlist.Rows.InsertAt(dr, 0);
                comboBoxStudent.DisplayMember = "vardas";
                comboBoxStudent.ValueMember = "id";
                comboBoxStudent.DataSource = studentlist;
                comboBoxStudent.SelectedIndex = 0;

                DataTable table = user.GetGrades(int.Parse(comboBoxSubject.SelectedValue.ToString()),
                                                 int.Parse(comboBoxStudentGroup.SelectedValue.ToString()),
                                                 int.Parse(comboBoxStudent.SelectedValue.ToString()),
                                                 checkBox1.Checked);
                if (table.Rows.Count > 0)
                {
                    dataGridView1.DataSource = table;
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    dataGridView1.Columns["dalykas"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dataGridView1.Columns[0].ReadOnly = true;
                    dataGridView1.Columns[1].ReadOnly = true;
                    dataGridView1.Columns[2].ReadOnly = true;
                    dataGridView1.Columns[3].ReadOnly = true;
                    dataGridView1.Columns[4].ReadOnly = true;
                    dataGridView1.Columns[5].ReadOnly = true;
                    dataGridView1.Columns[6].ReadOnly = false;
                    checkBox1.Checked = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FilterDGV()
        {
            try
            {
                if (comboBoxSubject.SelectedValue != null && 
                    comboBoxStudentGroup.SelectedValue != null && 
                    comboBoxStudent.SelectedValue != null)
                {
                    DataTable table = user.GetGrades(int.Parse(comboBoxSubject.SelectedValue.ToString()),
                                                 int.Parse(comboBoxStudentGroup.SelectedValue.ToString()),
                                                 int.Parse(comboBoxStudent.SelectedValue.ToString()),
                                                 checkBox1.Checked);
                    dataGridView1.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBoxSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterDGV();
        }

        private void comboBoxStudentGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterDGV();
        }

        private void comboBoxStudent_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterDGV();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            FilterDGV();
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception != null)
            {
                MessageBox.Show("Prašome įvesti skaičių tarp 2 ir 10");
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            string selection = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            if (int.TryParse(selection, out int result))
            {
                if (result < 2 || result > 10)
                {
                    dataGridView1.Rows[e.RowIndex].Cells[6].Value = ""; //triggers dataGridView1_DataError
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            FilterDGV();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    int gradeId = int.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
                    int grade;
                    if (String.IsNullOrWhiteSpace(dataGridView1.Rows[i].Cells[6].Value.ToString()))
                        grade = 0;
                    else
                        grade = int.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString());
                    user.UpdateGrade(gradeId, grade);
                }
                MessageBox.Show("Pakeitimai išsaugoti");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
