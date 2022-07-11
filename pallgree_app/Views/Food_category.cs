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
    public partial class Food_category : Form
    {
        public Food_category()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           

        }
        public void LoadDGV(){
            List<FoodCategory> list;
            using (var context = new pallgree_cafeContext())
            {
                list = context.FoodCategories.ToList();
                dataGridView1.DataSource = list; ;
            }
        }

        private void Food_category_Load(object sender, EventArgs e)
        {
            DataGridViewButtonColumn editCol = new DataGridViewButtonColumn();
            editCol.Name = "editCol";
            editCol.Text = "DeActive";
            editCol.Width = 100;
            editCol.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(editCol);
            LoadDGV();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String name = textBox1.Text;

            if (name.Equals(""))
            {
                MessageBox.Show("You need fill input");
                return;
            }

            FoodCategory account = new FoodCategory();
            account.Name = name;


            using (var context = new pallgree_cafeContext())
            {
                try
                {
                    
                    context.FoodCategories.Add(account);
                    context.SaveChanges();
                }
                catch
                {
                    //MessageBox.Show(ex.Message);
                    MessageBox.Show("Add failse");
                    return;
                }


            }
            MessageBox.Show($"Add success for category: {name} ");
            textBox1.Text = "";
            LoadDGV();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (dataGridView1.Columns[e.ColumnIndex].Name.Equals("editCol"))
            {
                using (var context = new pallgree_cafeContext())
                {
                    List<FoodCategory> list = context.FoodCategories.ToList();
                    context.FoodCategories.ToList().ForEach(x =>
                    {
                        
                        if (x.Id == list[e.RowIndex].Id)
                        {
                            if (x.Status == 1)
                                x.Status = 0;
                            else x.Status = 1;
                            context.FoodCategories.Update(x);
                            MessageBox.Show($"Change status success for: {x.Name}");
                        }

                    });
                    context.SaveChanges();
                }

            }
            LoadDGV();
        }
    }
}
