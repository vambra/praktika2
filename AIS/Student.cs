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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public DataTable GetGrades(int semester)
        {
            try
            {
                if(this.OpenConnection())
                {
                    string query = "SELECT dalykas.kodas AS `Kodas`, dalykas.pavadinimas AS `Dalyko pavadinimas`, pazymys.ivertinimas AS `Įvertinimas` " +
                                   "FROM dalykas, pazymys, grupes_dalykas, studentas " +
                                   "WHERE pazymys.studento_id = studentas.id AND pazymys.grupes_dalyko_id = grupes_dalykas.id AND grupes_dalykas.dalyko_id = dalykas.id AND " +
                                   "studentas.id = '" + id + "' AND " +
                                   "grupes_dalykas.semestras = '" + semester + "';";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    this.CloseConnection();
                    return table;
                }
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }


    }
}
