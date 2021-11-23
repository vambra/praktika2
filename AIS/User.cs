﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public int GetId()
        {
            return id;
        }
        public string GetType()
        {
            return type;
        }
    }
}
