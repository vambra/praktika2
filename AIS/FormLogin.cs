using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIS
{
    public partial class FormLogin : Form
    {
        public int UserId = 0;
        public string UserType;

        public FormLogin()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            try
            {
                User user = new User();
                if (user.Login(textBoxLoginName.Text, textBoxPassword.Text))
                {
                    UserId = user.GetId();
                    UserType = user.GetType();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Neteisingi prisijungimo duomenys");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
