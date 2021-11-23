using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

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
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server");
                        break;
                    default:
                        MessageBox.Show(ex.Message);
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        //public abstract void DBread();
        //public abstract void DBinsert();
        //public abstract void DBupdate();
        //public abstract void DBdelete();
    }
}
