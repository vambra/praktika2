using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data;

namespace AIS
{
    abstract class DBconnect
    {
        protected MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        public DBconnect()
        {
            Initialize();
        }
        protected void Initialize()
        {
            server = "localhost";
            database = "akademine_informacine_sistema";
            uid = "root";
            password = "";
            string connectionString = "SERVER=" + server + ";DATABASE=" + database + ";UID=" + uid + ";PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);
        }
        protected bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex1)
            {
                switch (ex1.Number)
                {
                    case 0:
                        MessageBox.Show("Nepavyko prisijungti prie serverio");
                        break;
                    default:
                        MessageBox.Show("MySqlException " + ex1.Number + ": " + ex1.Message);
                        break;
                }
                return false;
            }
            catch (Exception ex2)
            {
                MessageBox.Show(ex2.Message);
                return false;
            }
        }
        protected bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        protected DataTable GetDataTable(string query)
        {
            try
            {
                if (this.OpenConnection())
                {
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
                switch (ex1.Number)
                {
                    case 0:
                        break;
                    case 1062:
                        break;
                    case 1451:
                        MessageBox.Show("Įrašui, kurį bandote ištrinti, priklauso kiti įrašai. Prašome pirmiau ištrinti ar atskirti šiuos įrašus.");
                        break;
                    default:
                        MessageBox.Show("MySqlException " + ex1.Number + ": " + ex1.Message);
                        break;
                }
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
        protected int DatabaseNonQuery(string query)
        {
            try
            {
                if (this.OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    int result = cmd.ExecuteNonQuery();
                    this.CloseConnection();
                    return result;
                }
                return 0;
            }
            catch (MySqlException ex1)
            {
                switch (ex1.Number)
                {
                    case 0:
                        break;
                    case 1062:
                        break;
                    case 1451:
                        MessageBox.Show("Įrašui, kurį bandote ištrinti, priklauso kiti įrašai. Prašome pirmiau ištrinti ar atskirti šiuos įrašus.");
                        break;
                    default:
                        MessageBox.Show("MySqlException " + ex1.Number + ": " + ex1.Message);
                        break;
                }
                this.CloseConnection();
                return 0;
            }
            catch (Exception ex2)
            {
                MessageBox.Show(ex2.Message);
                this.CloseConnection();
                return 0;
            }
        }
    }
}
