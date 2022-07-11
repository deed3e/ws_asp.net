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
    public partial class DiscountView : Form
    {
        public DiscountView()
        {
            InitializeComponent();
        }

        private void LoadDGV() {
            List<Discount> list;
            using (var context = new pallgree_cafeContext())
            {
                list = context.Discounts.ToList();
                dataGridView1.DataSource = list;


            }
        }
        private void DiscountView_Load(object sender, EventArgs e)
        {
            LoadDGV();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            
                using (var context = new pallgree_cafeContext())
                { 
                     List<Discount> list = context.Discounts.ToList();
                    textBox1.Text = list[e.RowIndex].Name;
                    textBox2.Text = list[e.RowIndex].Discount1.ToString();
                    textBox3.Text = list[e.RowIndex].MinSpending.ToString();
                }
                }

        private void button1_ChangeUICues(object sender, UICuesEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            String discount = textBox2.Text.Trim();
            String min_spending = textBox3.Text.Trim();

            if (discount.Length == 0 || min_spending.Length == 0)
            {
                MessageBox.Show("You need fill discount and number for min spending");
                return;
            }
            if (Int32.Parse(discount)>99 || Int32.Parse(discount) <1)
            {
                MessageBox.Show("Discount cần bé hơn 100 và lớn hơn 0");
                return;
            }


            using (var context = new pallgree_cafeContext())
            {
                try
                {
                    context.Discounts.ToList().ForEach(x =>
                    {
                        if (x.Name.Equals(textBox1.Text)) { 
                           x.Discount1=Int32.Parse(discount);
                            x.MinSpending=Int32.Parse(min_spending);
                            context.Discounts.Update(x);    
                        }
                    });
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

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
