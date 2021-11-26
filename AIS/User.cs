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
    class User : DBconnect
    {
        protected int id;
        private string type;    // admin / lecturer / student
        
        public bool Login(string username, string password)
        {
            try
            {
                if (this.OpenConnection())
                {
                    bool login = false;
                    string query = "SELECT id, prisijungimo_vardas, slaptazodis, 'admin' AS vartotojo_tipas FROM administratorius " + 
                                   "UNION SELECT id, prisijungimo_vardas, slaptazodis, 'lecturer' FROM destytojas " + 
                                   "UNION SELECT id, prisijungimo_vardas, slaptazodis, 'student' FROM studentas;";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        if (username == dataReader["prisijungimo_vardas"].ToString() && password == dataReader["slaptazodis"].ToString())
                        {
                            if (int.TryParse(dataReader["id"].ToString(), out int result))
                            {
                                id = result;
                                type = dataReader["vartotojo_tipas"].ToString();
                                login = true;
                            }
                            else
                                MessageBox.Show("Database error");
                        }
                    }
                    dataReader.Close();
                    this.CloseConnection();
                    return login;
                }
                return false;
            }
            catch (MySqlException ex1)
            {
                if (ex1.Number == 0)
                    MessageBox.Show("Negalima prisijunti prie serverio.");
                else
                    MessageBox.Show(ex1.Message);
                this.CloseConnection();
                return false;
            }
            catch (Exception ex2)
            {
                MessageBox.Show(ex2.Message);
                this.CloseConnection();
                return false;
            }
        }
        public int GetId()
        {
            return id;
        }
        public string GetUserType()
        {
            return type;
        }
        public DataTable GetAllUserLogins()
        {
            string query = "SELECT administratorius.prisijungimo_vardas, administratorius.slaptazodis FROM administratorius " +
                                   "UNION SELECT destytojas.prisijungimo_vardas, destytojas.slaptazodis FROM destytojas " +
                                   "UNION SELECT studentas.prisijungimo_vardas, studentas.slaptazodis FROM studentas;";
            return GetDataTable(query);
        }
        public void AddUser(string Name, string Surname, string LoginName, string Password, string UserType)
        {
            string query = "INSERT INTO " + UserType + " (vardas, pavarde, grupes_id, prisijungimo_vardas, slaptazodis) " +
                           "VALUES ('" + Name + "', '" + Surname + "', '" + LoginName + "', '" + Password + "')";
            if (DatabaseNonQuery(query) > 0)
                MessageBox.Show("Naudotojas pridėtas");
        }
    }
}
