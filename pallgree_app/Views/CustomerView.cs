using pallgree_app.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pallgree_app.Views
{
    public partial class CustomerView : Form
    {
        public CustomerView()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String name = textBox1.Text.Trim();
            String number_phone = textBox2.Text.Trim();

            if (name.Length == 0 || number_phone.Length == 0) {
                MessageBox.Show("You need fill name and number for customer");
            }

            Customer x = new Customer();
            x.Name = name;
            x.NumberPhone = number_phone;

            using (var context = new pallgree_cafeContext())
            {
                try
                {
                    context.Customers.Add(x);
                    context.SaveChanges();
                    LoadDGV();
                }
                catch
                {
                    MessageBox.Show("Number phone đã tồn tại");
                    return;
                }


            }
            
        }

        private void LoadDGV() {
            List<Customer> list;
            using (var context = new pallgree_cafeContext())
            {
                list = context.Customers.ToList();
                dataGridView1.DataSource = list;


            }
        }
        private void CustomerView_Load(object sender, EventArgs e)
        {
            LoadDGV();
        }
    }
}
