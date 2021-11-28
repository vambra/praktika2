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
            string query = "SELECT id, pavadinimas, kodas, CONCAT(kodas, ' | ', pavadinimas) AS display FROM dalykas";
            return GetDataTable(query);
        }
        public DataTable GetSubjectsByLecturer(int LecturerId)
        {
            string query = "SELECT dalykas.id, dalykas.kodas, dalykas.pavadinimas, CONCAT(dalykas.kodas, ' | ', dalykas.pavadinimas) AS dalykas, " +
                           "destytojas.id AS 'destytojo id', CONCAT(destytojas.pavarde, ' ', destytojas.vardas) AS destytojas " +
                           "FROM dalykas, destytojo_dalykas, destytojas " +
                           "WHERE dalykas.id = destytojo_dalykas.dalyko_id AND destytojas.id = destytojo_dalykas.destytojo_id";
            if (LecturerId != 0)
                query += " AND destytojas.id = '" + LecturerId + "'";
            return GetDataTable(query);
        }
        public DataTable GetSubjectsByLecturerOpposite(int LecturerId)
        {
            string query = "SELECT DISTINCT dalykas.id, CONCAT(dalykas.kodas, ' | ', dalykas.pavadinimas) AS dalykas FROM dalykas, destytojo_dalykas " +
                           "WHERE dalykas.id NOT IN (SELECT dalykas.id FROM dalykas, destytojo_dalykas WHERE dalykas.id = destytojo_dalykas.dalyko_id " +
                           "AND destytojo_dalykas.destytojo_id = '" + LecturerId + "')";
            return GetDataTable(query);
        }
        public void AddSubject(string Name, string Code)
        {
            string query = "INSERT INTO dalykas (pavadinimas, kodas) VALUES ('" + Name + "', '" + Code + "')";
            if (DatabaseNonQuery(query) > 0)
                MessageBox.Show("Dalykas pridėtas");
        }
        public void DeleteSubject(int Id)
        {
            string query = "DELETE FROM dalykas WHERE id = '" + Id + "'";
            if (DatabaseNonQuery(query) > 0)
                MessageBox.Show("Dalykas ištrintas");
        }
    }
}
