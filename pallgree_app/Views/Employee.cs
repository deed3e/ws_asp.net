using Microsoft.Extensions.Configuration;
using pallgree_app.Logics;
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
                catch
                {
                    MessageBox.Show("Username exited");
                    return;
                }


            }
            MessageBox.Show($"Register success for account: {display_name} ");
            loadDGV();
        }

        public void loadDGV()
        {
            List<Account> list;
            using (var context = new pallgree_cafeContext())
            {
                list = context.Accounts.ToList();
                dataGridView1.DataSource = list.Select(x => new { x.DisplayName, x.Username, x.Role, x.Status }).ToList();


            }

        }

        private void Employee_Load(object sender, EventArgs e)
        {
            DataGridViewButtonColumn deleteCol = new DataGridViewButtonColumn();
            deleteCol.Name = "deleteCol";
            deleteCol.Text = "DeActive";
            deleteCol.Width = 120;
            deleteCol.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(deleteCol);
            loadDGV();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (dataGridView1.Columns[e.ColumnIndex].Name.Equals("deleteCol"))
            {
                using (var context = new pallgree_cafeContext())
                {
                    List<Account> list = context.Accounts.ToList();
                    context.Accounts.ToList().ForEach(x =>
                    {
                        if (x.Username == list[e.RowIndex].Username)
                        {
                            if(x.Status==1)
                            x.Status = 0;
                            else x.Status = 1;
                            context.Accounts.Update(x);
                            MessageBox.Show($"Change status success for: {x.Username}");
                        }

                    });
                    context.SaveChanges();
                }

            }
            loadDGV();
        }
    }
}
