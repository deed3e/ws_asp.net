using System;
using pallgree_app.Views;
using System.Windows.Forms;

namespace pallgree_app.Views
{
    public partial class Home_admin : Form
    {
        public Home_admin()
        {
            InitializeComponent();
        }

        private void Home_admin_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Employee emp = new Employee();
            emp.Show();

        }
    }
}
