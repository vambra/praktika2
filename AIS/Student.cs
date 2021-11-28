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
    class Student : User
    {
        protected string name;
        protected string surname;
        protected int groupId;

        public string GetName() { return name; }
        public string GetSurname() { return surname; }
        public int GetGroupId() { return groupId; }

        public void SetProfile(int StudentId)
        {
            try
            {
                id = StudentId;
                if (this.OpenConnection())
                {
                    string query = "SELECT vardas, pavarde, grupes_id FROM studentas WHERE id = '" + this.id + "';";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        name = dataReader["vardas"].ToString();
                        surname = dataReader["pavarde"].ToString();
                        int.TryParse(dataReader["grupes_id"].ToString(), out groupId);
                    }
                    dataReader.Close();
                    this.CloseConnection();
                }
            }
            catch (MySqlException ex1)
            {
                if (ex1.Number == 0)
                    MessageBox.Show("Negalima prisijunti prie serverio.");
                else
                    MessageBox.Show(ex1.Message);
                this.CloseConnection();
            }
            catch (Exception ex2)
            {
                MessageBox.Show(ex2.Message);
                this.CloseConnection();
            }
        }

        public DataTable GetGrades(string semester)
        {
            string query = "SELECT CONCAT(FLOOR(grupe.stojimo_metai + (grupes_dalykas.semestras / 2) - 0.5), ' - ', " +
                                   "FLOOR(grupe.stojimo_metai + (grupes_dalykas.semestras / 2) - 0.5) + 1, ' m. m., ', grupes_dalykas.semestras, ' semestras') AS metai, " +
                                   "studiju_programa.pavadinimas AS 'studijų programa', grupe.pavadinimas AS grupė, dalykas.kodas, dalykas.pavadinimas AS dalykas, " +
                                   "pazymys.ivertinimas AS įvertinimas " +
                                   "FROM grupe, grupes_dalykas, dalykas, pazymys, studiju_programa " +
                                   "WHERE pazymys.grupes_dalyko_id = grupes_dalykas.id AND grupes_dalykas.grupes_id = grupe.id AND grupes_dalykas.dalyko_id = dalykas.id " +
                                   "AND studiju_programa.id = grupe.studiju_programos_id AND pazymys.studento_id = '" + id + "'";
            if (semester != "")
                query += " AND CONCAT(FLOOR(grupe.stojimo_metai + (grupes_dalykas.semestras / 2) - 0.5), ' - ', " +
                         "FLOOR(grupe.stojimo_metai + (grupes_dalykas.semestras / 2) - 0.5) + 1, ' m. m., ', grupes_dalykas.semestras, ' semestras') = '" +
                         semester + "'";
            return GetDataTable(query);
        }
        public DataTable GetSemesters()
        {
            string query = "SELECT DISTINCT CONCAT(FLOOR(grupe.stojimo_metai + (grupes_dalykas.semestras / 2) - 0.5), ' - ', " +
                                   "FLOOR(grupe.stojimo_metai + (grupes_dalykas.semestras / 2) - 0.5) + 1, ' m. m., ', grupes_dalykas.semestras, ' semestras') AS metai " +
                                   "FROM grupe, grupes_dalykas, pazymys WHERE pazymys.grupes_dalyko_id = grupes_dalykas.id AND grupes_dalykas.grupes_id = grupe.id " +
                                   "AND pazymys.studento_id = '" + id + "'";
            return GetDataTable(query);
        }
        public DataTable GetStudentsByLecturer(int LecturerId)
        {
            string query = "SELECT DISTINCT studentas.id, CONCAT(studentas.pavarde, ' ', studentas.vardas, ' | ID:', studentas.id) AS vardas " +
                                   "FROM studentas, grupes_dalykas, destytojas " +
                                   "WHERE grupes_dalykas.grupes_id = studentas.grupes_id AND grupes_dalykas.destytojo_id = '" + LecturerId + "';";
            return GetDataTable(query);
        }
        public DataTable GetStudentsByGroup(int GroupId)
        {
            string query = "SELECT studentas.id AS 'studento id', CONCAT(studentas.pavarde, ' ', studentas.pavarde) AS 'studento vardas', " +
                           "grupe.pavadinimas AS 'grupe', studiju_programa.pavadinimas AS 'studiju programa', fakultetas.pavadinimas AS 'fakultetas', " +
                           "CONCAT(studentas.id, ' | ', studentas.pavarde, ' ', studentas.vardas) AS display FROM studentas " +
                           "LEFT JOIN grupe ON studentas.grupes_id = grupe.id LEFT JOIN studiju_programa ON grupe.studiju_programos_id = studiju_programa.id " +
                           "LEFT JOIN fakultetas ON studiju_programa.fakulteto_id = fakultetas.id";
            if (GroupId != 0)
                query += " WHERE grupe.id = '" + GroupId + "'";
            return GetDataTable(query);
        }
        public DataTable GetProfile(int StudentId)
        {
            string query = "SELECT studentas.id AS 'studento id', CONCAT(studentas.pavarde, ' ', studentas.pavarde) AS 'studento vardas', " +
                           "grupe.id as 'grupes id', grupe.pavadinimas AS 'grupe' FROM studentas LEFT JOIN grupe ON studentas.grupes_id = grupe.id " +
                           "WHERE studentas.id = '" + StudentId + "'";
            return GetDataTable(query);
        }
        public void SetGroup(int StudentId, int GroupId)
        {
            string Group;
            if (GroupId != 0)
                Group = "'" + GroupId + "'";
            else
                Group = "NULL";
            string query = "UPDATE studentas SET studentas.grupes_id = " + Group + " WHERE studentas.id = '" + StudentId + "'";
            if (DatabaseNonQuery(query) > 0)
                MessageBox.Show("Studentas priskirtas");
            if (GroupId != 0)
            {
                query = "SELECT grupes_dalykas.id FROM grupes_dalykas WHERE grupes_dalykas.grupes_id = '" + GroupId + "'";
                DataTable groupSubjectIdList = GetDataTable(query);
                for (int i = 0; i < groupSubjectIdList.Rows.Count; i++)
                {
                    string groupSubjectId = groupSubjectIdList.Rows[i][0].ToString();
                    query = "INSERT INTO pazymys(pazymys.studento_id, pazymys.grupes_dalyko_id) VALUES('" + StudentId + "', '" + groupSubjectId + "')";
                    DatabaseNonQuery(query);
                }
            }
        }
    }
}
