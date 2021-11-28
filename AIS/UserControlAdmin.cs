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
    public partial class UserControlAdmin : UserControl
    {
        public UserControlAdmin(int adminId)
        {
            InitializeComponent();
            LoadFaculties();
            LoadPrograms();
            LoadGroups();
            LoadLecturers();
            LoadSubjects();
            LoadStudents();
            LoadGroupSubjects();
            comboBoxNewUserType.Items.Add("studentas");
            comboBoxNewUserType.Items.Add("dėstytojas");
            comboBoxNewUserType.SelectedIndex = 0;
            comboBox3Lecturer.SelectedIndex = -1;
        }

        private bool StringCharacterCheck(string str)
        {
            if (String.IsNullOrWhiteSpace(str))
                return false;
            foreach (var ch in str)
            {
                if (ch.ToString() == "'")
                    return false;
            }
            return true;
        }
        private void LoadFaculties()
        {
            StudentGroup group = new StudentGroup();

            comboBoxFaculty.DataSource = group.GetFaculties();
            comboBoxFaculty.DisplayMember = "pavadinimas";
            comboBoxFaculty.ValueMember = "id";
            comboBoxFaculty.SelectedIndex = -1;

            comboBoxProgramFaculty.DataSource = group.GetFaculties();
            comboBoxProgramFaculty.DisplayMember = "pavadinimas";
            comboBoxProgramFaculty.ValueMember = "id";
            comboBoxProgramFaculty.SelectedIndex = -1;
        }
        private void LoadPrograms()
        {
            StudentGroup group = new StudentGroup();

            comboBoxProgram.DataSource = group.GetPrograms();
            comboBoxProgram.DisplayMember = "display";
            comboBoxProgram.ValueMember = "id";
            comboBoxProgram.SelectedIndex = -1;

            comboBoxGroupProgram.DataSource = group.GetPrograms();
            comboBoxGroupProgram.DisplayMember = "display";
            comboBoxGroupProgram.ValueMember = "id";
            comboBoxGroupProgram.SelectedIndex = -1;
        }
        private void LoadGroups()
        {
            StudentGroup group = new StudentGroup();

            comboBoxGroup.DataSource = group.GetGroups();
            comboBoxGroup.DisplayMember = "pavadinimas";
            comboBoxGroup.ValueMember = "id";
            comboBoxGroup.SelectedIndex = -1;

            DataTable list = group.GetGroups();
            DataRow dr = list.NewRow();
            dr["id"] = "0";
            dr["pavadinimas"] = "Jokia grupė";
            list.Rows.InsertAt(dr, 0);
            comboBox2Group.DisplayMember = "pavadinimas";
            comboBox2Group.ValueMember = "id";
            comboBox2Group.DataSource = list;
            comboBox2Group.SelectedIndex = 0;

            comboBox3Group.DisplayMember = "pavadinimas";
            comboBox3Group.ValueMember = "id";
            comboBox3Group.DataSource = group.GetGroups();
            comboBox3Group.SelectedIndex = -1;
        }
        private void LoadSubjects()
        {
            Subject subject = new Subject();

            comboBoxSubject.DataSource = subject.GetSubjects(); ;
            comboBoxSubject.DisplayMember = "display";
            comboBoxSubject.ValueMember = "id";
            comboBoxSubject.SelectedIndex = -1;
            if (comboBox1SubjectAdd.Enabled == true)
            {
                comboBox1SubjectAdd.DataSource = null;
                comboBox1SubjectAdd.DataSource = subject.GetSubjectsByLecturerOpposite(int.Parse(comboBox1Lecturer.SelectedValue.ToString()));
                comboBox1SubjectAdd.DisplayMember = "dalykas";
                comboBox1SubjectAdd.ValueMember = "id";
            }
            if (comboBox1SubjectDelete.Enabled == true)
            {
                comboBox1SubjectDelete.DataSource = null;
                comboBox1SubjectDelete.DataSource = subject.GetSubjectsByLecturer(int.Parse(comboBox1Lecturer.SelectedValue.ToString()));
                comboBox1SubjectDelete.DisplayMember = "dalykas";
                comboBox1SubjectDelete.ValueMember = "id";
            }
            int LecturerId = 0;
            if (comboBox1Lecturer.SelectedValue != null)
                int.TryParse(comboBox1Lecturer.SelectedIndex.ToString(), out LecturerId);
            dataGridViewLecturerToSubject.DataSource = subject.GetSubjectsByLecturer(LecturerId);
            dataGridViewLecturerToSubject.Columns["id"].Visible = false;
            dataGridViewLecturerToSubject.Columns["dalykas"].Visible = false;
            dataGridViewLecturerToSubject.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewLecturerToSubject.Columns["pavadinimas"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            comboBox3Subject.DataSource = subject.GetSubjects(); ;
            comboBox3Subject.DisplayMember = "display";
            comboBox3Subject.ValueMember = "id";
            comboBox3Subject.SelectedIndex = -1;
        }
        private void LoadLecturers()
        {
            Lecturer lecturer = new Lecturer();
            DataTable list = lecturer.GetLecturersBySubject(0);
            DataRow dr = list.NewRow();
            dr["id"] = "0";
            dr["destytojas"] = "-";
            list.Rows.InsertAt(dr, 0);
            comboBox1Lecturer.DisplayMember = "destytojas";
            comboBox1Lecturer.ValueMember = "id";
            comboBox1Lecturer.DataSource = list;
            comboBox1Lecturer.SelectedIndex = 0;

            if (comboBox3Lecturer.Enabled == true)
            {
                int selection = 0;
                if (comboBox3Subject.SelectedValue != null)
                    int.TryParse(comboBox3Subject.SelectedValue.ToString(), out selection);
                comboBox3Lecturer.DataSource = null;
                comboBox3Lecturer.DisplayMember = "destytojas";
                comboBox3Lecturer.ValueMember = "id";
                comboBox3Lecturer.DataSource = lecturer.GetLecturersBySubject(selection);
                if (comboBox3Lecturer.Items.Count > 0)
                    comboBox3Lecturer.SelectedIndex = 0;
                else
                    comboBox3Lecturer.SelectedIndex = -1;
            }
        }
        private void LoadStudents()
        {
            Student student = new Student();
            comboBox2Student.DataSource = student.GetStudentsByGroup(0);
            comboBox2Student.DisplayMember = "display";
            comboBox2Student.ValueMember = "studento id";
            comboBox2Student.SelectedIndex = -1;

            int groupId = 0;
            if (comboBox2Group.SelectedValue != null)
                int.TryParse(comboBox2Group.SelectedValue.ToString(), out groupId);
            dataGridViewStudentToGroup.DataSource = student.GetStudentsByGroup(groupId);
            dataGridViewStudentToGroup.Columns["display"].Visible = false;
            dataGridViewStudentToGroup.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewStudentToGroup.Columns["fakultetas"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        private void LoadGroupSubjects()
        {
            int groupId = 0;
            if (comboBox3Group.SelectedValue != null)
                int.TryParse(comboBox3Group.SelectedValue.ToString(), out groupId);
            int subjectId = 0;
            if(comboBox3Subject.SelectedValue != null)
                int.TryParse(comboBox3Subject.SelectedValue.ToString(), out subjectId);
            StudentGroup studentGroup = new StudentGroup();
            dataGridViewSubjectToGroup.DataSource = studentGroup.GetGroupSubjects(groupId, subjectId);
            dataGridViewSubjectToGroup.Columns["id"].Visible = false;
            dataGridViewSubjectToGroup.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void buttonFacultyAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (StringCharacterCheck(textBoxFacultyName.Text.ToString()))
                {
                    StudentGroup group = new StudentGroup();
                    DataTable list = group.GetFaculties();
                    for (int i = 0; i < list.Rows.Count; i++)
                    {
                        if (list.Rows[i]["pavadinimas"].ToString() == textBoxFacultyName.Text.ToString())
                        {
                            MessageBox.Show("Fakultetas pavadinimu '" + textBoxFacultyName.Text.ToString() + "' jau egzistuoja");
                            textBoxFacultyName.Text = "";
                            return;
                        }
                    }
                    group.AddFaculty(textBoxFacultyName.Text.ToString());
                    LoadFaculties();
                    textBoxFacultyName.Text = "";
                }
                else
                {
                    MessageBox.Show("Prašome užpildyti fakulteto pavadinimą, nenaudokite ' simbolio");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonFacultyDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxFaculty.SelectedIndex < 0)
                {
                    MessageBox.Show("Prašome pasirinkti fakultetą");
                    return;
                }
                DialogResult dialogResult = MessageBox.Show("Ar tikrai norite ištrinti '" + comboBoxFaculty.Text.ToString() + "'?", "", MessageBoxButtons.OKCancel);
                if (dialogResult == DialogResult.OK)
                {
                    StudentGroup group = new StudentGroup();
                    group.DeleteFaculty(int.Parse(comboBoxFaculty.SelectedValue.ToString()));
                    LoadFaculties();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonProgramAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (StringCharacterCheck(textBoxProgramName.Text.ToString()) && StringCharacterCheck(textBoxProgramCode.Text.ToString()))
                {
                    if (comboBoxProgramFaculty.SelectedIndex < 0)
                    {
                        MessageBox.Show("Prašome pasirinkti fakultetą");
                        return;
                    }
                    StudentGroup group = new StudentGroup();
                    DataTable list = group.GetPrograms();
                    int sameNameCount = 0;
                    for (int i = 0; i < list.Rows.Count; i++)
                    {
                        if (list.Rows[i]["kodas"].ToString() == textBoxProgramCode.Text.ToString())
                        {
                            MessageBox.Show("Studijų programa kodu '" + textBoxProgramCode.Text.ToString() + "' jau egzistuoja");
                            textBoxProgramCode.Text = "";
                            return;
                        }
                        if (list.Rows[i]["pavadinimas"].ToString() == textBoxProgramName.Text.ToString())
                        {
                            sameNameCount++;
                        }
                    }
                    if (sameNameCount > 0)
                    {
                        DialogResult dialogResult = MessageBox.Show("'" + textBoxProgramName.Text.ToString() + "' jau egzistuoja. Ar tikrai norite tęsti?", "", MessageBoxButtons.OKCancel);
                        if (dialogResult == DialogResult.Cancel)
                        {
                            textBoxProgramName.Text = "";
                            return;
                        }
                    }
                    group.AddProgram(textBoxProgramName.Text.ToString(), textBoxProgramCode.Text.ToString(), int.Parse(comboBoxProgramFaculty.SelectedValue.ToString()));
                    LoadPrograms();
                    textBoxProgramName.Text = "";
                    textBoxProgramCode.Text = "";
                }
                else
                {
                    MessageBox.Show("Prašome užpildyti studijų programos pavadinimą ir kodą, nenaudokite ' simbolio");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonProgramDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxProgram.SelectedIndex < 0)
                {
                    MessageBox.Show("Prašome pasirinkti studijų programą");
                    return;
                }
                DialogResult dialogResult = MessageBox.Show("Ar tikrai norite ištrinti '" + comboBoxProgram.Text.ToString() + "'?", "", MessageBoxButtons.OKCancel);
                if (dialogResult == DialogResult.OK)
                {
                    StudentGroup group = new StudentGroup();
                    group.DeleteProgram(int.Parse(comboBoxProgram.SelectedValue.ToString()));
                    LoadPrograms();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonGroupAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (StringCharacterCheck(textBoxGroupName.Text.ToString()) && StringCharacterCheck(textBoxGroupYear.Text.ToString())
                    && int.TryParse(textBoxGroupYear.Text.ToString(), out int year) && year > 1900 && year < 3000)
                {
                    if (comboBoxGroupProgram.SelectedIndex < 0)
                    {
                        MessageBox.Show("Prašome pasirinkti studijų programą");
                        return;
                    }
                    StudentGroup group = new StudentGroup();
                    DataTable list = group.GetGroups();
                    int sameNameCount = 0;
                    for (int i = 0; i < list.Rows.Count; i++)
                    {
                        if (list.Rows[i]["pavadinimas"].ToString() == textBoxGroupName.Text.ToString())
                        {
                            sameNameCount++;
                        }
                    }
                    if (sameNameCount > 0)
                    {
                        DialogResult dialogResult = MessageBox.Show("'" + textBoxGroupName.Text.ToString() + "' jau egzistuoja. Ar tikrai norite tęsti?", "", MessageBoxButtons.OKCancel);
                        if (dialogResult == DialogResult.Cancel)
                        {
                            textBoxGroupName.Text = "";
                            return;
                        }
                    }
                    group.AddGroup(textBoxGroupName.Text.ToString(), year, int.Parse(comboBoxGroupProgram.SelectedValue.ToString()));
                    LoadGroups();
                    textBoxGroupName.Text = "";
                    textBoxGroupYear.Text = "";
                }
                else
                {
                    MessageBox.Show("Prašome užpildyti studentų grupės pavadinimą ir stojimo metus, nenaudokite ' simbolio");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonGroupDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxGroup.SelectedIndex < 0)
                {
                    MessageBox.Show("Prašome pasirinkti grupę");
                    return;
                }
                DialogResult dialogResult = MessageBox.Show("Ar tikrai norite ištrinti '" + comboBoxGroup.Text.ToString() + "'?", "", MessageBoxButtons.OKCancel);
                if (dialogResult == DialogResult.OK)
                {
                    StudentGroup group = new StudentGroup();
                    group.DeleteGroup(int.Parse(comboBoxGroup.SelectedValue.ToString()));
                    LoadGroups();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonSubjectAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (StringCharacterCheck(textBoxSubjectName.Text.ToString()) && StringCharacterCheck(textBoxSubjectCode.Text.ToString()))
                {
                    Subject subject = new Subject();
                    DataTable list = subject.GetSubjects();
                    int sameNameCount = 0;
                    for (int i = 0; i < list.Rows.Count; i++)
                    {
                        if (list.Rows[i]["kodas"].ToString() == textBoxSubjectCode.Text.ToString())
                        {
                            MessageBox.Show("Studijų programa kodu '" + textBoxSubjectCode.Text.ToString() + "' jau egzistuoja");
                            textBoxSubjectCode.Text = "";
                            return;
                        }
                        if (list.Rows[i]["pavadinimas"].ToString() == textBoxSubjectName.Text.ToString())
                        {
                            sameNameCount++;
                        }
                    }
                    if (sameNameCount > 0)
                    {
                        DialogResult dialogResult = MessageBox.Show("'" + textBoxSubjectName.Text.ToString() + "' jau egzistuoja. Ar tikrai norite tęsti?", "", MessageBoxButtons.OKCancel);
                        if (dialogResult == DialogResult.Cancel)
                        {
                            textBoxSubjectName.Text = "";
                            return;
                        }
                    }
                    subject.AddSubject(textBoxSubjectName.Text.ToString(), textBoxSubjectCode.Text.ToString());
                    LoadSubjects();
                    textBoxSubjectName.Text = "";
                    textBoxSubjectCode.Text = "";
                }
                else
                {
                    MessageBox.Show("Prašome užpildyti dalyko pavadinimą ir kodą, nenaudokite ' simbolio");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonSubjectDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxSubject.SelectedIndex < 0)
                {
                    MessageBox.Show("Prašome pasirinkti dalyką");
                    return;
                }
                DialogResult dialogResult = MessageBox.Show("Ar tikrai norite ištrinti '" + comboBoxSubject.Text.ToString() + "'?", "", MessageBoxButtons.OKCancel);
                if (dialogResult == DialogResult.OK)
                {
                    Subject subject = new Subject();
                    subject.DeleteSubject(int.Parse(comboBoxSubject.SelectedValue.ToString()));
                    LoadSubjects();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonNewUserAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (StringCharacterCheck(textBoxNewUserName.Text.ToString()) && StringCharacterCheck(textBoxNewUserSurname.Text.ToString()))
                {
                    string login, password;
                    if (String.IsNullOrWhiteSpace(textBoxNewUserLoginName.Text.ToString()) && String.IsNullOrWhiteSpace(textBoxNewUserPassword.Text.ToString()))
                    {
                        login = textBoxNewUserName.Text.ToString();
                        password = textBoxNewUserSurname.Text.ToString();
                    }
                    else if (String.IsNullOrWhiteSpace(textBoxNewUserLoginName.Text.ToString()) || String.IsNullOrWhiteSpace(textBoxNewUserPassword.Text.ToString()))
                    {
                        MessageBox.Show("Prašome užpildyti naujo naudotojo prisijungimo vardą ir slaptažodį arba palikti abu laukus tuščius");
                        return;
                    }
                    else if (StringCharacterCheck(textBoxNewUserLoginName.Text.ToString()) && StringCharacterCheck(textBoxNewUserPassword.Text.ToString()))
                    {
                        MessageBox.Show("Prašome užpildyti naujo naudotojo prisijungimo vardą ir slaptažodį be ' simbolio");
                        return;
                    }
                    else
                    {
                        login = textBoxNewUserLoginName.Text.ToString();
                        password = textBoxNewUserPassword.Text.ToString();
                    }
                    if (comboBoxNewUserType.SelectedIndex < 0)
                    {
                        MessageBox.Show("Prašome pasirinkti naujo naudotojo tipą");
                        return;
                    }

                    User user = new User();
                    DataTable list = user.GetAllUserLogins();
                    for (int i = 0; i < list.Rows.Count; i++)
                    {
                        if (list.Rows[i]["prisijungimo_vardas"].ToString() == login)
                        {
                            MessageBox.Show("Naudotojas su prisijungimo vardu '" + login + "' jau egzistuoja. Prašome nurodyti kitą prisijungimo vardą");
                            return;
                        }
                    }
                    user.AddUser(textBoxNewUserName.Text.ToString(), textBoxNewUserSurname.Text.ToString(), login, password, comboBoxNewUserType.SelectedValue.ToString());
                    textBoxNewUserName.Text = "";
                    textBoxNewUserSurname.Text = "";
                    textBoxNewUserLoginName.Text = "";
                    textBoxNewUserPassword.Text = "";
                    LoadStudents();
                    LoadLecturers();
                }
                else
                {
                    MessageBox.Show("Prašome užpildyti naujo naudotojo vardą ir pavardę, nenaudokite ' simbolio");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox1Lecturer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1Lecturer.SelectedIndex > 0)
            {
                comboBox1SubjectAdd.Enabled = true;
                comboBox1SubjectDelete.Enabled = true;
                LoadSubjects();
                if (comboBox1SubjectAdd.Items.Count == 0)
                    button1SubjectAdd.Enabled = false;
                else
                    button1SubjectAdd.Enabled = true;
                if (comboBox1SubjectDelete.Items.Count == 0)
                    button1SubjectDelete.Enabled = false;
                else
                    button1SubjectDelete.Enabled = true;
            }
            else
            {
                comboBox1SubjectAdd.Enabled = false;
                comboBox1SubjectDelete.Enabled = false;
                button1SubjectAdd.Enabled = false;
                button1SubjectDelete.Enabled = false;
                if (comboBox1Lecturer.SelectedIndex == 0)
                    LoadSubjects();
            }
        }
        private void comboBox1Lecturer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (comboBox1Lecturer.SelectedIndex < 0)
            {
                comboBox1SubjectAdd.Enabled = false;
                comboBox1SubjectDelete.Enabled = false;
                button1SubjectAdd.Enabled = false;
                button1SubjectDelete.Enabled = false;
            }
        }
        private void button1SubjectAdd_Click(object sender, EventArgs e)
        {
            Lecturer lecturer = new Lecturer();
            int LecturerId = int.Parse(comboBox1Lecturer.SelectedValue.ToString());
            int SubjectId = int.Parse(comboBox1SubjectAdd.SelectedValue.ToString());
            lecturer.AddSubject(LecturerId, SubjectId);
            LoadSubjects();
            if (comboBox1SubjectAdd.Items.Count == 0)
                button1SubjectAdd.Enabled = false;
            else
                button1SubjectAdd.Enabled = true;
            if (comboBox1SubjectDelete.Items.Count == 0)
                button1SubjectDelete.Enabled = false;
            else
                button1SubjectDelete.Enabled = true;
        }

        private void button1SubjectDelete_Click(object sender, EventArgs e)
        {
            Lecturer lecturer = new Lecturer();
            int LecturerId = int.Parse(comboBox1Lecturer.SelectedValue.ToString());
            int SubjectId = int.Parse(comboBox1SubjectDelete.SelectedValue.ToString());
            lecturer.RemoveSubject(LecturerId, SubjectId);
            LoadSubjects();
            if (comboBox1SubjectAdd.Items.Count == 0)
                button1SubjectAdd.Enabled = false;
            else
                button1SubjectAdd.Enabled = true;
            if (comboBox1SubjectDelete.Items.Count == 0)
                button1SubjectDelete.Enabled = false;
            else
                button1SubjectDelete.Enabled = true;
        }

        private void comboBox2Student_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedId = 0;
            if (comboBox2Student.SelectedValue != null)
                int.TryParse(comboBox2Student.SelectedValue.ToString(), out selectedId);
            if (selectedId > 0)
            {
                Student student = new Student();
                DataTable profile = student.GetProfile(selectedId);
                string groupName = profile.Rows[0]["grupe"].ToString();
                if (groupName == "")
                    groupName = "nepriskirta";
                labelStudentCurrentGroup.Text = groupName;
                labelStudentCurrentGroup.Visible = true;
                labelStudentCurrentGroupTitle.Visible = true;
                button2GroupSet.Enabled = true;
            }
            else
            {
                labelStudentCurrentGroup.Visible = false;
                labelStudentCurrentGroupTitle.Visible = false;
                button2GroupSet.Enabled = false;
            }
        }

        private void comboBox2Student_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (comboBox2Student.SelectedIndex < 0)
            {
                labelStudentCurrentGroup.Visible = false;
                labelStudentCurrentGroupTitle.Visible = false;
            }
        }

        private void comboBox2Group_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBox2Student.SelectedIndex;
            LoadStudents();
            comboBox2Student.SelectedIndex = index;
        }

        private void button2GroupSet_Click(object sender, EventArgs e)
        {
            Student student = new Student();
            student.SetGroup(int.Parse(comboBox2Student.SelectedValue.ToString()), int.Parse(comboBox2Group.SelectedValue.ToString()));
            comboBox2Student.SelectedIndex = -1;
            int index = comboBox2Group.SelectedIndex;
            LoadGroups();
            comboBox2Group.SelectedIndex = index;
        }

        private void comboBox3Group_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGroupSubjects();
        }

        private void comboBox3Subject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3Subject.SelectedIndex > -1)
            {
                comboBox3Lecturer.Enabled = true;
                int lecturerId = 0;
                if (comboBox3Subject.SelectedValue != null)
                    int.TryParse(comboBox3Subject.SelectedValue.ToString(), out lecturerId);
                Lecturer lecturer = new Lecturer();
                comboBox3Lecturer.DataSource = null;
                comboBox3Lecturer.DisplayMember = "destytojas";
                comboBox3Lecturer.ValueMember = "id";
                comboBox3Lecturer.DataSource = lecturer.GetLecturersBySubject(lecturerId);
                if (comboBox3Lecturer.Items.Count > 0)
                    comboBox3Lecturer.SelectedIndex = 0;
                else
                    comboBox3Lecturer.SelectedIndex = -1;
                LoadGroupSubjects();
            }
            else
                comboBox3Lecturer.Enabled = false;
        }

        private void comboBox3Lecturer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3Lecturer.SelectedIndex > -1 && !String.IsNullOrWhiteSpace(textBox3Semester.Text))
                button3SubjectSet.Enabled = true;
            else
                button3SubjectSet.Enabled = false;
        }

        private void textBox3Semester_TextChanged(object sender, EventArgs e)
        {
            if (comboBox3Lecturer.SelectedIndex > -1 && !String.IsNullOrWhiteSpace(textBox3Semester.Text))
                button3SubjectSet.Enabled = true;
            else
                button3SubjectSet.Enabled = false;
        }

        private void button3SubjectSet_Click(object sender, EventArgs e)
        {
            if(int.TryParse(textBox3Semester.Text, out int semester))
            {
                if (semester < 1 || semester > 20)
                {
                    MessageBox.Show("Prašome įvesti tinkamą semestro numerį");
                    textBox3Semester.Text = "";
                }
                else
                {
                    StudentGroup studentGroup = new StudentGroup();
                    int GroupId = int.Parse(comboBox3Group.SelectedValue.ToString());
                    int SubjectId = int.Parse(comboBox3Subject.SelectedValue.ToString());
                    int LecturerId = int.Parse(comboBox3Lecturer.SelectedValue.ToString());
                    studentGroup.SetSubject(SubjectId, GroupId, LecturerId, semester);
                    textBox3Semester.Text = "";
                    comboBox3Lecturer.SelectedIndex = -1;
                    //comboBox3Subject.SelectedIndex = -1;
                    //comboBox3Group.SelectedIndex = -1;
                    LoadGroupSubjects();
                }
            }
            else
            {
                MessageBox.Show("Prašome įvesti semestro numerį");
                textBox3Semester.Text = "";
            }
        }
    }
}
