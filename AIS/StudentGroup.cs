using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace AIS
{
    class StudentGroup : DBconnect
    {
        protected int id;
        protected string name;
        protected string program;
        protected string faculty;
        protected int startYear;

        public string GetName() { return name; }
        public string GetProgram() { return program; }
        public string GetFaculty() { return faculty; }

        public void SetGroup(int GroupId)
        {
            try
            {
                id = GroupId;
                if (this.OpenConnection())
                {
                    string query = "SELECT grupe.pavadinimas, grupe.stojimo_metai, studiju_programa.pavadinimas AS studiju_programa, fakultetas.pavadinimas AS fakultetas " +
                                   "FROM grupe, studiju_programa, fakultetas " +
                                   "WHERE grupe.studiju_programos_id = studiju_programa.id AND studiju_programa.fakulteto_id = fakultetas.id AND grupe.id = '" + this.id + "';";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        name = dataReader["pavadinimas"].ToString();
                        startYear = int.Parse(dataReader["stojimo_metai"].ToString());
                        program = dataReader["studiju_programa"].ToString();
                        faculty = dataReader["fakultetas"].ToString();
                    }
                    dataReader.Close();
                    this.CloseConnection();
                }
            }
            catch (MySqlException ex1)
            {
                if (ex1.Number == 0)
                    MessageBox.Show("Negalima prisijunti prie serverio.");
                this.CloseConnection();
            }
            catch (Exception ex2)
            {
                MessageBox.Show(ex2.Message);
                this.CloseConnection();
            }
        }
        public DataTable GetGroups()
        {
            string query = "SELECT id, pavadinimas, stojimo_metai, studiju_programos_id FROM grupe;";
            return GetDataTable(query);
        }
        public DataTable GetGroupsByLecturer(int LecturerId)
        {
            string query = "SELECT DISTINCT grupe.id, grupe.pavadinimas FROM grupe, grupes_dalykas, destytojas " +
                           "WHERE grupes_dalykas.grupes_id = grupe.id AND grupes_dalykas.destytojo_id = '" + LecturerId + "';";
            return GetDataTable(query);
        }
        public DataTable GetFaculties()
        {
            string query = "SELECT id, pavadinimas FROM fakultetas;";
            return GetDataTable(query);
        }
        public DataTable GetPrograms()
        {
            string query = "SELECT id, pavadinimas, kodas, fakulteto_id, CONCAT(kodas, ' | ', pavadinimas) AS display FROM studiju_programa;";
            return GetDataTable(query);
        }
        public void AddFaculty(string Name)
        {
            string query = "INSERT INTO fakultetas (pavadinimas) VALUES ('" + Name + "')";
            if (DatabaseNonQuery(query) > 0)
                MessageBox.Show("Fakultetas pridėtas");
        }
        public void DeleteFaculty(int Id)
        {
            string query = "DELETE FROM fakultetas WHERE id = '" + Id + "'";
            if (DatabaseNonQuery(query) > 0)
                MessageBox.Show("Fakulteto įrašas ištrintas");
        }
        public void AddProgram(string Name, string Code, int FacultyId)
        {
            string query = "INSERT INTO studiju_programa (pavadinimas, kodas, fakulteto_id) VALUES ('" + Name + "', '" + Code + "', '" + FacultyId + "')";
            if (DatabaseNonQuery(query) > 0)
                MessageBox.Show("Studijų programa pridėta");
        }
        public void DeleteProgram(int Id)
        {
            string query = "DELETE FROM studiju_programa WHERE id = '" + Id + "'";
            if (DatabaseNonQuery(query) > 0)
                MessageBox.Show("Studijų programa ištrinta");
        }
        public void AddGroup(string Name, int StartYear, int FacultyId)
        {
            string query = "INSERT INTO grupe (pavadinimas, stojimo_metai, studiju_programos_id) VALUES ('" + Name + "', '" + StartYear + "', '" + FacultyId + "')";
            if (DatabaseNonQuery(query) > 0)
                MessageBox.Show("Grupė pridėta");
        }
        public void DeleteGroup(int Id)
        {
            string query = "DELETE FROM grupe WHERE id = '" + Id + "'";
            if (DatabaseNonQuery(query) > 0)
                MessageBox.Show("Grupė ištrinta");
        }
        public void SetSubject(int SubjectId, int GroupId, int LecturerId, int Semester)
        {
            string query = "INSERT INTO grupes_dalykas (grupes_dalykas.grupes_id, grupes_dalykas.dalyko_id, grupes_dalykas.destytojo_id, grupes_dalykas.semestras) " +
                           "VALUES ('" + GroupId + "', '" + SubjectId + "', '" + LecturerId + "', '" + Semester + "'); " +
                           "SELECT grupes_dalykas.id FROM grupes_dalykas WHERE grupes_dalykas.id = @@Identity;";
            DataTable temp = GetDataTable(query);
            if (temp != null)
            {
                int groupSubjectId = int.Parse(temp.Rows[0][0].ToString());
                Student student = new Student();
                DataTable studentList = student.GetStudentsByGroup(GroupId);
                for (int i = 0; i < studentList.Rows.Count; i++)
                {
                    string studentId = studentList.Rows[i]["studento id"].ToString();
                    query = "INSERT INTO pazymys(pazymys.studento_id, pazymys.grupes_dalyko_id) VALUES('" + studentId + "', '" + groupSubjectId + "')";
                    DatabaseNonQuery(query);
                }
                MessageBox.Show("Dalykas pridėtas");
            }
            else
            {
                MessageBox.Show("Dalykas jau yra pridėtas");
            }
        }
        public DataTable GetGroupSubjects(int GroupId, int SubjectId)
        {
            string query = "SELECT grupes_dalykas.id, fakultetas.pavadinimas AS fakultetas, studiju_programa.pavadinimas AS 'studiju programa', " +
                           "grupe.pavadinimas AS 'grupe', dalykas.kodas AS 'dalyko kodas', dalykas.pavadinimas AS 'dalykas', destytojas.id AS 'destytojo id', " +
                           "CONCAT(destytojas.pavarde, ' ', destytojas.vardas) AS destytojas, grupes_dalykas.semestras " +
                           "FROM grupes_dalykas, fakultetas, studiju_programa, grupe, destytojas, dalykas WHERE fakultetas.id = studiju_programa.fakulteto_id " +
                           "AND studiju_programa.id = grupe.studiju_programos_id AND grupes_dalykas.grupes_id = grupe.id AND grupes_dalykas.dalyko_id = dalykas.id " +
                           "AND grupes_dalykas.destytojo_id = destytojas.id";
            if (GroupId != 0)
                query += " AND grupe.id = '" + GroupId + "'";
            if (SubjectId != 0)
                query += " AND dalykas.id = '" + SubjectId + "'";
            return GetDataTable(query);
        }
        public void DeleteGroupSubject(int GroupSubjectId)
        {
            string query = "DELETE FROM pazymys WHERE pazymys.grupes_dalyko_id = '" + GroupSubjectId + "'";
            DatabaseNonQuery(query);
            query = "DELETE FROM grupes_dalykas WHERE grupes_dalykas.id = '" + GroupSubjectId + "'";
            if (DatabaseNonQuery(query) > 0)
                MessageBox.Show("Grupės dalykas ištrintas");
        }
    }
}
