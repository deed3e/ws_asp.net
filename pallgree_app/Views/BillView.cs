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
    public partial class BillView : Form
    {
        public BillView()
        {
            InitializeComponent();
        }

        private void BillView_Load(object sender, EventArgs e)
        {

            using (var context = new pallgree_cafeContext())
            {
                List<Bill> list;
                list = context.Bills.ToList();
                dataGridView1.DataSource = list.Select(x => new { 
                       CheckIn = x.TimeCheckin,
                       CheckOut = x.TimeCheckout,
                       Status  = (x.Status == 1)?"Pending":"Done",
                       Employee = x.EmployeeCheckout,
                       TotalPrice = x.Total,

                });
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime dateTime1 = dateTimePicker1.Value;
            DateTime dateTime2 = dateTimePicker2.Value;
            using (var context = new pallgree_cafeContext())
            {
                List<Bill> list;
                list = context.Bills.Where(x => x.TimeCheckout <= dateTime2 && x.TimeCheckin >= dateTime1).ToList();
                dataGridView1.DataSource = list.Select(x => new {
                    CheckIn = x.TimeCheckin,
                    CheckOut = x.TimeCheckout,
                    Status = (x.Status == 1) ? "Pending" : "Done",
                    Employee = x.EmployeeCheckout,
                    TotalPrice = x.Total,

                });
            }
        }
    }
}
