using Microsoft.Extensions.Configuration;
using pallgree.Logic;
using pallgree_app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace pallgree_app.Views
{
    public partial class Employee : Form
    {
        public Employee()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var config = new ConfigurationBuilder().AddJsonFile("jsconfig.json").Build();
            string salt = config.GetConnectionString("SaltHash");

            String display_name = textBox1.Text;
            String username = textBox2.Text;
            String password = textBox3.Text;

            if (display_name.Equals("") || username.Equals("") || password.Equals(""))
            {
                MessageBox.Show("You need fill usename and password");
                return;
            }

            Account account = new Account();
            account.Username = username;
            account.Password = Crypto.GenerateHash(password, salt);
            account.DisplayName = display_name;

            using (var context = new pallgree_cafeContext())
            {
                try
                {
                    context.Accounts.Add(account);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Username exited");
                }


            }
            MessageBox.Show($"Register success for account: {display_name} ");
            loadDGV();
        }

        public void loadDGV() {
            List<Account> list;
            using (var context = new pallgree_cafeContext())
            {
                list = context.Accounts.ToList();
                dataGridView1.DataSource = list;
                                           

            }
        }

        private void Employee_Load(object sender, EventArgs e)
        {
            loadDGV();
        }
    }
}

