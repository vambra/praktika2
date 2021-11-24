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
    class Lecturer : User
    {
        protected string name;
        protected string surname;

        public void SetProfile(int LecturerId)
        {
            try
            {
                id = LecturerId;
                if (this.OpenConnection())
                {
                    string query = "SELECT destytojas.vardas, destytojas.pavarde FROM destytojas " +
                                   "WHERE destytojas.id = '" + this.id + "';";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        name = dataReader["vardas"].ToString();
                        surname = dataReader["pavarde"].ToString();
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
        public DataTable GetGrades()
        {
            try
            {
                if (this.OpenConnection())
                {
                    string query = "SELECT pazymys.id, dalykas.kodas AS Kodas, dalykas.pavadinimas AS Dalykas, grupe.pavadinimas AS Grupė, studentas.id AS 'Studento ID', " +
                                   "CONCAT(studentas.pavarde, ' ', studentas.vardas) AS 'Studento vardas', pazymys.ivertinimas AS Įvertinimas " +
                                   "FROM pazymys, dalykas, grupes_dalykas, grupe, studentas, destytojas " +
                                   "WHERE grupes_dalykas.grupes_id = grupe.id AND grupes_dalykas.dalyko_id = dalykas.id AND " +
                                   "grupes_dalykas.id = pazymys.grupes_dalyko_id AND studentas.id = pazymys.studento_id AND " +
                                   "destytojas.id = grupes_dalykas.destytojo_id AND destytojas.id = '" + id + "';";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    this.CloseConnection();
                    return table;
                }
                else
                    return null;
            }
            catch (MySqlException ex1)
            {
                if(ex1.Number == 0)
                    MessageBox.Show("Negalima prisijunti prie serverio.");
                else
                    MessageBox.Show(ex1.Message);
                this.CloseConnection();
                return null;
            }
            catch (Exception ex2)
            {
                MessageBox.Show(ex2.Message);
                this.CloseConnection();
                return null;
            }
        }
        public void UpdateGrade(int gradeId, int grade)
        {
            try
            {
                if (this.OpenConnection())
                {
                    string query;
                    if (grade == 0)
                        query = "UPDATE pazymys SET ivertinimas = NULL WHERE id = '" + gradeId + "';";
                    else
                        query = "UPDATE pazymys SET ivertinimas = '" + grade + "' WHERE id = '" + gradeId + "';";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
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
    }
}
