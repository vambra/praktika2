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
                DataTable subjectlist = subject.GetSubjectsById(user.GetId());
                DataRow dr = subjectlist.NewRow();
                dr[1] = "Visi dalykai";
                subjectlist.Rows.InsertAt(dr, 0);
                comboBoxSubject.DataSource = subjectlist;
                comboBoxSubject.DisplayMember = "pavadinimas";
                comboBoxSubject.SelectedIndex = 0;

                //Group ComboBox
                StudentGroup group = new StudentGroup();
                DataTable grouptlist = group.GetGroupsByID(user.GetId());
                dr = grouptlist.NewRow();
                dr[0] = "Visos grupės";
                grouptlist.Rows.InsertAt(dr, 0);
                comboBoxStudentGroup.DataSource = grouptlist;
                comboBoxStudentGroup.DisplayMember = "pavadinimas";
                comboBoxStudentGroup.SelectedIndex = 0;

                //Student ComboBox
                Student student = new Student();
                DataTable studentlist = student.GetStudentsByID(user.GetId());
                dr = studentlist.NewRow();
                dr[0] = "Visi studentai";
                studentlist.Rows.InsertAt(dr, 0);
                comboBoxStudent.DataSource = studentlist;
                comboBoxStudent.DisplayMember = "vardas";
                comboBoxStudent.SelectedIndex = 0;

                dataGridView1.DataSource = user.GetGrades();
                if (user.GetGrades() != null)
                {
                    dataGridView1.DataSource = user.GetGrades();
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
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
                if (user.GetGrades() != null)
                {
                    dataGridView1.DataSource = user.GetGrades();
                    string selectedSubject = comboBoxSubject.GetItemText(comboBoxSubject.SelectedItem);
                    string selectedGroup = comboBoxStudentGroup.GetItemText(comboBoxStudentGroup.SelectedItem);
                    string selectedStudent = comboBoxStudent.GetItemText(comboBoxStudent.SelectedItem);

                    CurrencyManager currencyManager = (CurrencyManager)BindingContext[dataGridView1.DataSource];
                    currencyManager.SuspendBinding();

                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        if (selectedSubject != "Visi dalykai")
                        {
                            if (dataGridView1.Rows[i].Cells[2].Value.ToString() != selectedSubject)
                            {
                                dataGridView1.Rows[i].Visible = false;
                            }
                        }
                        if (selectedGroup != "Visos grupės")
                        {
                            if (dataGridView1.Rows[i].Cells[3].Value.ToString() != selectedGroup)
                            {
                                dataGridView1.Rows[i].Visible = false;
                            }
                        }
                        if (selectedStudent != "Visi studentai")
                        {
                            if (dataGridView1.Rows[i].Cells[5].Value.ToString() != selectedStudent)
                            {
                                dataGridView1.Rows[i].Visible = false;
                            }
                        }
                        if (checkBox1.Checked == false)
                        {
                            if (!String.IsNullOrWhiteSpace(dataGridView1.Rows[i].Cells[6].Value.ToString()))
                            {
                                dataGridView1.Rows[i].Visible = false;
                            }
                        }
                    }
                    currencyManager.ResumeBinding();
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
                if (user.GetGrades() != null)
                {
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        if (dataGridView1.Rows[i].Visible == true)
                        {
                            int gradeId = int.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
                            int grade;
                            if (String.IsNullOrWhiteSpace(dataGridView1.Rows[i].Cells[6].Value.ToString()))
                                grade = 0;
                            else
                                grade = int.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString());
                            MessageBox.Show("[" + grade + "]");
                            user.UpdateGrade(gradeId, grade);
                        }
                    }
                    MessageBox.Show("Pakeitimai išsaugoti");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
