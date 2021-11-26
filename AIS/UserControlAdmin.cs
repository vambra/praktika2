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
            LoadSubjects();
            comboBoxNewUserType.Items.Add("studentas");
            comboBoxNewUserType.Items.Add("dėstytojas");
            comboBoxNewUserType.SelectedIndex = 0;
            LoadLecturers();
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

            comboBoxProgramFaculty.DataSource = group.GetFaculties();
            comboBoxProgramFaculty.DisplayMember = "pavadinimas";
            comboBoxProgramFaculty.ValueMember = "id";
        }
        private void LoadPrograms()
        {
            StudentGroup group = new StudentGroup();

            comboBoxProgram.DataSource = group.GetPrograms();
            comboBoxProgram.DisplayMember = "display";
            comboBoxProgram.ValueMember = "id";

            comboBoxGroupProgram.DataSource = group.GetPrograms();
            comboBoxGroupProgram.DisplayMember = "display";
            comboBoxGroupProgram.ValueMember = "id";
        }
        private void LoadGroups()
        {
            StudentGroup group = new StudentGroup();

            comboBoxGroup.DataSource = group.GetGroups();
            comboBoxGroup.DisplayMember = "pavadinimas";
            comboBoxGroup.ValueMember = "id";
        }
        private void LoadSubjects()
        {
            Subject subject = new Subject();

            comboBoxSubject.DataSource = subject.GetSubjects(); ;
            comboBoxSubject.DisplayMember = "display";
            comboBoxSubject.ValueMember = "id";
            if (comboBox1SubjectAdd.Enabled == true)
                comboBox1SubjectAdd.DataSource = subject.GetSubjectsById(int.Parse(comboBox1Lecturer.SelectedValue.ToString()));
            comboBox1SubjectAdd.DisplayMember = "pavadinimas";
            comboBox1SubjectAdd.ValueMember = "id";
            if (comboBox1SubjectDelete.Enabled == true)
                comboBox1SubjectDelete.DataSource = subject.GetSubjectsByIdOpposite(int.Parse(comboBox1Lecturer.SelectedValue.ToString()));
            comboBox1SubjectDelete.DisplayMember = "pavadinimas";
            comboBox1SubjectDelete.ValueMember = "id";
        }
        private void LoadLecturers()
        {
            Lecturer lecturer = new Lecturer();
            DataTable list = lecturer.GetLecturers();
            DataRow dr = list.NewRow();
            dr[1] = "-";
            list.Rows.InsertAt(dr, 0);
            comboBox1Lecturer.DataSource = list;
            comboBox1Lecturer.DisplayMember = "vardas";
            comboBox1Lecturer.SelectedIndex = 0;
            comboBox1Lecturer.ValueMember = "id";
        }

        //  first tab
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

        //second tab
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

        //third tab
        private void comboBox1Lecturer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1Lecturer.SelectedIndex > 0)
            {
                comboBox1SubjectAdd.Enabled = true;
                comboBox1SubjectDelete.Enabled = true;
                Subject subject = new Subject();
                comboBox1SubjectAdd.DataSource = null;
                comboBox1SubjectAdd.DataSource = subject.GetSubjectsByIdOpposite(int.Parse(comboBox1Lecturer.SelectedValue.ToString()));
                comboBox1SubjectAdd.DisplayMember = "pavadinimas";
                comboBox1SubjectAdd.ValueMember = "id";
                comboBox1SubjectDelete.DataSource = null;
                comboBox1SubjectDelete.DataSource = subject.GetSubjectsById(int.Parse(comboBox1Lecturer.SelectedValue.ToString()));
                comboBox1SubjectDelete.DisplayMember = "pavadinimas";
                comboBox1SubjectDelete.ValueMember = "id";
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
            }
        }
        private void comboBox1Lecturer_KeyPress(object sender, EventArgs e)
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

        }
    }
}
