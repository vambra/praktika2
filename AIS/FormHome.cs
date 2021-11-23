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
    public partial class FormHome : Form
    {
        public FormHome(int UserId, string UserType)
        {
            InitializeComponent();
            UserControl userControl = new UserControl();
            if (UserType == "admin")
                userControl = new UserControlAdmin(UserId);
            if (UserType == "lecturer")
                userControl = new UserControlLecturer(UserId);
            if (UserType == "student")
                userControl = new UserControlStudent(UserId);
            panel1.Dock = DockStyle.Fill;
            userControl.Dock = DockStyle.Fill;
            panel1.Controls.Add(userControl);
        }
    }
}
