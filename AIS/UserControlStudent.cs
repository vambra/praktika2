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
                dataGridViewSemester.RowHeadersVisible = false;
                dataGridViewSemester.ColumnHeadersVisible = false;
                dataGridViewSemester.ColumnCount = 2;
                dataGridViewSemester.Columns[0].Visible = false;
                dataGridViewSemester.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                int year = userGroup.GetStartYear();
                for (int i = 1; i <= userGroup.GetSemesterCount(); i++)
                {
                    int rowId = dataGridViewSemester.Rows.Add();
                    DataGridViewRow row = dataGridViewSemester.Rows[rowId];
                    row.Cells[0].Value = i;
                    row.Cells[1].Value = i + " semestras (" + year + " / " + (year + 1) + " m. m.)";
                    if (i % 2 == 0)
                        year++;
                }

                //load grades
                dataGridViewSemester.Rows[0].Selected = true;
                int selected = int.Parse(dataGridViewSemester.Rows[0].Cells[0].Value.ToString());
                dataGridViewGrades.DataSource = user.GetGrades(selected);
                dataGridViewGrades.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridViewGrades.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                //dataGridViewGrades.DefaultCellStyle.SelectionBackColor = dataGridViewGrades.DefaultCellStyle.BackColor;
                //dataGridViewGrades.DefaultCellStyle.SelectionForeColor = dataGridViewGrades.DefaultCellStyle.ForeColor;
                dataGridViewGrades.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridViewGrades.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridViewGrades.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
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
                int selected = int.Parse(dataGridViewSemester.Rows[e.RowIndex].Cells[0].Value.ToString());
                dataGridViewGrades.DataSource = user.GetGrades(selected);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
