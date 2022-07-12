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
    public partial class CheckOutView : Form
    {
        int tmpTotal = 0;
        int idBill;
        double tax = 0;
        int discount = 0;
        double total = 0;
        int idTable;
        String user_name ="";
        public CheckOutView(int _idBill, int _idTable,String user)
        {
            InitializeComponent();
            idBill = _idBill;
            idTable = _idTable;
            user_name = user;


        }

        private int GetPrice(int idFood) {

            int n = 0;
            using (var context = new pallgree_cafeContext())
            {
                context.Foods.ToList().ForEach(x => {
                    if (x.Id == idFood) n = x.Price;
                });

            }
            return n;
        }

        private void CheckOutView_Load(object sender, EventArgs e)
        {
            using (var context = new pallgree_cafeContext())
            { 
                List<BillDetail> list = context.BillDetails.ToList();  
                textBox1.Text = list.Count.ToString();
                list.ForEach(x =>
                {
                    if (x.IdBill == idBill)
                        tmpTotal += (int)x.Count * GetPrice(x.IdFood);
                });
                textBox2.Text = tmpTotal.ToString()+" Usd";
                tax = Math.Round(tmpTotal * 0.02, 2);
                textBox3.Text = tax.ToString()+" Usd";
            
            }
            total = tmpTotal + tax ;
            textBox6.Text = total.ToString() + " Usd";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CustomerView emp = new CustomerView();
            emp.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int spending = 0;
            
            String phone = textBox4.Text.Trim();
            using (var context = new pallgree_cafeContext())
            {
                context.Customers.ToList().ForEach(x =>
                {
                    if (x.NumberPhone.Equals(phone)) {
                        spending = x.Spending;
                    }
                });

                context.Discounts.ToList().ForEach(x => {
                    if (spending > x.MinSpending) {
                        discount = x.Discount1;
                        return;
                    }
                });
            }
            textBox5.Text = discount.ToString()+"%";
            double discount2= Math.Round((tmpTotal * ((double)discount/100)), 2);
            total = tmpTotal + tax - discount2;
            total = Math.Round(total, 2);
            textBox6.Text = total.ToString()+" Usd";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Khách hàng đã thanh toán đủ số tiền?", "Want something else?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            String phone = textBox4.Text.Trim();
            if (dr == DialogResult.Yes)
            {
                // 
                using (var context = new pallgree_cafeContext())
                {
                    //
                    context.Customers.ToList().ForEach(x =>
                    {
                        if (x.NumberPhone.Equals(phone))
                        {
                            x.Spending += (int)Math.Round(total, 0);
                            context.Customers.Update(x);
                        }
                    });
                    // status bill = 2 + table =0 
                    context.Tables.ToList().ForEach(x =>
                    {
                        if (x.Id == idTable)
                        {
                            x.Status = 0;
                            context.Tables.Update(x);
                        }
                    });

                    context.Bills.ToList().ForEach(x =>
                    {
                        if (x.Id == idBill)
                        {
                            x.Status = 2;
                            x.EmployeeCheckout = user_name;
                            x.TimeCheckout = DateTime.Now;
                            x.Total = total;
                            context.Bills.Update(x);
                        }
                    });

                    context.SaveChanges();
                    MessageBox.Show("Thanh toán thành công!!!");
                    this.Close();
                }

                }
           
        }
    }
}
