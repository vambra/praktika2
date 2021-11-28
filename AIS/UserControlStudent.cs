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
    public partial class UserControlStudent : UserControl
    {
        private Student user;
        private StudentGroup userGroup;
        public UserControlStudent(int studentId)
        {
            InitializeComponent();
            
            try
            {
                user = new Student();
                user.SetProfile(studentId);
                userGroup = new StudentGroup();
                userGroup.SetGroup(user.GetGroupId());

                //load label information
                labelId.Text = user.GetId().ToString();
                labelFullName.Text = user.GetName() + " " + user.GetSurname();
                labelFaculty.Text = userGroup.GetFaculty();
                labelProgram.Text = userGroup.GetProgram();
                labelGroupName.Text = userGroup.GetName();

                //load semesters
                dataGridViewSemester.DataSource = user.GetSemesters();
                dataGridViewSemester.Rows[0].Selected = true;
                dataGridViewSemester.RowHeadersVisible = false;
                dataGridViewSemester.ColumnHeadersVisible = false;
                dataGridViewSemester.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                //load grades
                string selected = dataGridViewSemester.Rows[0].Cells[0].Value.ToString();
                dataGridViewGrades.DataSource = user.GetGrades(selected);
                dataGridViewGrades.Columns[0].Visible = false;
                dataGridViewGrades.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridViewSemester_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string selected = dataGridViewSemester.Rows[e.RowIndex].Cells[0].Value.ToString();
                dataGridViewGrades.DataSource = user.GetGrades(selected);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
