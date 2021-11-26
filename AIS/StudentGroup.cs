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
        public DataTable GetGroupsByID(int LecturerId)
        {
            string query = "SELECT DISTINCT grupe.pavadinimas FROM grupe, grupes_dalykas, destytojas " +
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
    }
}
