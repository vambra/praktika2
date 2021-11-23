using System;
using System.Collections.Generic;
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

        public int GetId() { return id; }
        public string GetName() { return name; }
        public string GetProgram() { return program; }
        public string GetFaculty() { return faculty; }
        public int GetStartYear() { return startYear; }


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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public int GetSemesterCount()
        {
            try
            {
                if (this.OpenConnection())
                {
                    int semesterCount = 0;
                    string query = "SELECT MAX(semestras) AS semester_count FROM grupes_dalykas WHERE grupes_id = '" + id + "';";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        semesterCount = int.Parse(dataReader["semester_count"].ToString());
                    }
                    dataReader.Close();
                    this.CloseConnection();
                    return semesterCount;
                }
                else
                    return 0;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }
    }
}
