using Microsoft.Extensions.Configuration;
using pallgree_app.Logics;
using pallgree_app.Models;
using pallgree_app.Views;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace pallgree_app
{
    public partial class Login : Form
    {
        
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var config = new ConfigurationBuilder().AddJsonFile("jsconfig.json").Build();
            string salt = config.GetConnectionString("SaltHash");
            bool check = false;
            if (textBox1.Text.Equals("") || textBox2.Text.Equals("")) {
                MessageBox.Show("You need fill usename and password");
                return;
            }
            String username = textBox1.Text;
            String password = textBox2.Text;
            using (var context = new pallgree_cafeContext())
            {
                context.Accounts.Where(a => a.Username == username).ToList().ForEach(a => {
                    if (Crypto.AreEqual(password, a.Password, salt)) {
                        check = true;
                    }
                });
               
            }

            if (check)
            {
                Program.SetMainForm(new Home_admin());
                Program.ShowMainForm();
                this.Close();
            }
            else {
                MessageBox.Show("Usename or password incorrect");
                return;
            }
        }
    }
}
