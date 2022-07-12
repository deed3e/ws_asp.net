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
    public partial class TableView : Form
    {
        public TableView()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool isDone= false;
            int amount = Int32.Parse(textBox1.Text); 
            using (var context = new pallgree_cafeContext())
            {
                List<Table> list= context.Tables.ToList();
                for (int i = list.Count()+1 ; i <= amount; i++) { 
                    Table a = new Table();
                    a.Name = $"Table {i.ToString()}";
                    a.Status = 0;
                    context.Tables.Add(a);
                    isDone = true;
                }
                context.SaveChanges();
            }

            if (isDone) {
                MessageBox.Show("Update amount table success");
                this.Close();
            }else MessageBox.Show("New amount need > current amount");
        }

        private void TableView_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
