using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIS
{
    class Subject : DBconnect
    {
        public DataTable GetSubjects()
        {
            try
            {
                if (this.OpenConnection())
                {
                    string query = "SELECT id, pavadinimas AS name, kodas AS code FROM dalykas";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    this.CloseConnection();
                    return table;
                }
                return null;
            }
            catch (MySqlException ex1)
            {
                if (ex1.Number == 0)
                    MessageBox.Show("Negalima prisijunti prie serverio.");
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

        public DataTable GetSubjectsById(int LecturerId)
        {
            try
            {
                if (this.OpenConnection())
                {
                    string query = "SELECT dalykas.pavadinimas FROM dalykas, destytojo_dalykas " +
                           "WHERE dalykas.id = destytojo_dalykas.dalyko_id AND destytojo_dalykas.destytojo_id = '" + LecturerId + "';";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    this.CloseConnection();
                    return table;
                }
                return null;
            }
            catch (MySqlException ex1)
            {
                if (ex1.Number == 0)
                    MessageBox.Show("Negalima prisijunti prie serverio.");
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
    }
}
