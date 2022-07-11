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
    public partial class FoodView : Form
    {
        public FoodView()
        {
            InitializeComponent();
        }

        private void LoadDGV() {
            List<Food> list;
            using (var context = new pallgree_cafeContext())
            {
                list = context.Foods.ToList();
                dataGridView1.DataSource = list;


            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

            openFileDialog1.InitialDirectory = "C://Desktop";
            openFileDialog1.Title = "Select image to be upload.";
            openFileDialog1.Filter = "Image Only(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            openFileDialog1.FilterIndex = 1;
            try
            {
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (openFileDialog1.CheckFileExists)
                    {
                        string path = System.IO.Path.GetFullPath(openFileDialog1.FileName);
                        label8.Text = path;
                        pictureBox1.Image = new Bitmap(openFileDialog1.FileName);
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                }

            }
            catch (Exception ex)
            {
                //it will give if file is already exits..
                MessageBox.Show(ex.Message);
            }
        }

        private void Food_Load(object sender, EventArgs e)
        {
            using (var context = new pallgree_cafeContext())
            {
                List<FoodCategory> list;
                list = context.FoodCategories.ToList();
                comboBox1.DataSource = list;    
                comboBox1.SelectedIndex = 0;
                comboBox1.DisplayMember = "Name";
                comboBox1.ValueMember = "id";

            }
            radioButton1.Checked = true;
            LoadDGV();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool condition = true;
            int  category = Int32.Parse(comboBox1.SelectedValue.ToString());
            String name = textBox1.Text.Trim();
            String price = textBox2.Text.Trim();
            String desc = richTextBox1.Text.Trim();
            String image = "";
            int status = 1;
            if (radioButton2.Checked == true) {
                status = 0;
            }
            if (!label8.Text.Contains("directory"))
                image = label8.Text;
            if (name.Equals("") || price.Equals(""))
            {
                MessageBox.Show("You need fill name and price");
                return;
            }

            Food f = new Food();
            f.Name = name;

            try {
                f.Price = Int32.Parse(price);
            }
            catch{
                MessageBox.Show("Price is integer");
                return;
            }
           
            f.Description = desc;   
            f.IdCategory = category;
            f.Status = status;
            f.Image = image;

            using (var context = new pallgree_cafeContext())
            {

                context.Foods.ToList().ForEach(x =>
                {
                    if (x.Name.Equals(name)) {
                        MessageBox.Show("This food is exits");
                        condition = false;
                        return;
                    }
                });
                if (condition) {
                    try
                    {
                        context.Foods.Add(f);
                        context.SaveChanges();
                    }
                    catch
                    {
                        MessageBox.Show("Username exited");
                        return;
                    }
                }
               


            }
            if (condition)
            {
                MessageBox.Show($"Add success for account: {name} ");
            }
                
            LoadDGV();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            List<Food> list;
            using (var context = new pallgree_cafeContext())
            {
                list = context.Foods.ToList();
                dataGridView1.DataSource = list;
            }

            comboBox1.SelectedValue = list[e.RowIndex].IdCategory;
            textBox1.Text = list[e.RowIndex].Name;
            textBox2.Text = list[e.RowIndex].Price.ToString();
            richTextBox1.Text = list[e.RowIndex].Description;
            if (list[e.RowIndex].Status == 1) {
                radioButton1.Checked = true;
            }else radioButton2.Checked = true;
            try {
                label8.Text = list[e.RowIndex].Image;
                pictureBox1.Image = new Bitmap(label8.Text);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            } catch  {
                label8.Text = "";
                pictureBox1.Image = null;
            }
               label10.Text = list[e.RowIndex].Id.ToString();




        }

        private void button3_Click(object sender, EventArgs e)
        {
     
            int category = Int32.Parse(comboBox1.SelectedValue.ToString());
            String name = textBox1.Text.Trim();
            String price = textBox2.Text.Trim();
            String desc = richTextBox1.Text.Trim();
            String image = "";
            int status = 1;
            if (radioButton2.Checked == true)
            {
                status = 0;
            }
            if (!label8.Text.Contains("directory"))
                image = label8.Text;
            if (name.Equals("") || price.Equals(""))
            {
                MessageBox.Show("You need fill name and price");
                return;
            }

            
            try
            {
                Int32.Parse(price);
            }
            catch
            {
                MessageBox.Show("Price is integer");
                return;
            }
           

            using (var context = new pallgree_cafeContext())
            {
                
                    try
                    {
                        List<Food> list = context.Foods.ToList();
                        context.Foods.ToList().ForEach(f =>
                        {

                            if (f.Id == Int32.Parse(label10.Text))
                            {
                                f.Name = name;
                                f.Description = desc;
                                f.IdCategory = category;
                                f.Status = status;
                                f.Image = image;
                                f.Price = Int32.Parse(price);
                                context.Foods.Update(f);
                                MessageBox.Show($"Change success ");
                            }

                        });
                        context.SaveChanges();
                    }
                    catch
                    {
                        MessageBox.Show("Failse");
                        return;
                    }
                



            }
            LoadDGV();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (var context = new pallgree_cafeContext())
            {
                List<FoodCategory> list;
                list = context.FoodCategories.ToList();
                comboBox1.DataSource = list;
                comboBox1.SelectedIndex = 0;
                comboBox1.DisplayMember = "Name";
                comboBox1.ValueMember = "id";

            }
            radioButton1.Checked = true;
            textBox1.Text = "";
            textBox2.Text = "";
            richTextBox1.Text = "";
            label8.Text = "";
            pictureBox1.Image = null;
            label10.Text = "~";
            comboBox1.SelectedItem = 0;
            LoadDGV();

        }
    }
}
