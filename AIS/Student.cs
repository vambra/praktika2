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
        public DataTable GetStudentsByID(int LecturerId)
        {
            string query = "SELECT DISTINCT CONCAT(studentas.pavarde, ' ', studentas.vardas) AS vardas " +
                                   "FROM studentas, grupes_dalykas, destytojas " +
                                   "WHERE grupes_dalykas.grupes_id = studentas.grupes_id AND grupes_dalykas.destytojo_id = '" + LecturerId + "';";
            return GetDataTable(query);
        }
    }
}
