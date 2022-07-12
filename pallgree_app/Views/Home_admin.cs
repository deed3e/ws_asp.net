using System;
using pallgree_app.Views;
using System.Windows.Forms;
using pallgree_app.Models;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace pallgree_app.Views
{
    public partial class Home_admin : Form
    {
        bool isFisrtChange = false; 
        String user_name;
        public Home_admin(String user)
        {
            InitializeComponent();
            user_name = user;
        }

        private void LoadTable() 
        {
            flowLayoutPanel1.Controls.Clear();
            using (var context = new pallgree_cafeContext())
            {
                List<Table> list = context.Tables.ToList();
                foreach (Table t in list)
                {
                    Button btn = new Button() { Width = 250, Height = 250 };
                    String _status="";
                    if (t.Status == 0)
                    {
                        _status="Status: Empty";
                    }
                    else if (t.Status == 1) {
                        _status = "Status: Fully";
                        _status = "Status: Fully";
                    }
                    btn.Text = t.Name +" "+ Environment.NewLine+_status;
                    btn.Click += new System.EventHandler(this.button_Click);
                    flowLayoutPanel1.Controls.Add(btn);
                }
            }
            LoadLabel();
        }

        private void LoadLabel() {
            using (var context = new pallgree_cafeContext())
            {
                List<Table> list = context.Tables.ToList();

                int size = list.Count;
                int full = 0;
                foreach (Table t in list)
                {
                    if (t.Status == 1)
                    {
                        full++;
                    }
                }
                String _status = $"Table full: {full}/{size}";
                label1.Text = _status;
            }
        }

        private void LoadBill(int idTable) {
            using (var context = new pallgree_cafeContext())
            {
                int idBill = -1;
                context.Bills.ToList().ForEach(t =>
                {
                    if (t.IdTable == idTable && t.Status == 1)
                    {
                        idBill = t.Id;
                    }
                });
                context.BillDetails.ToList();
                context.Foods.ToList();
                var result =(from a in context.BillDetails
                            from b in context.Foods
                            where a.IdFood == b.Id && a.IdBill == idBill
                            select new { 
                                NameFood =  b.Name,
                                Count = a.Count,
                                Total = a.Count*b.Price + " Usd",
                                }).ToList();
                dataGridView1.DataSource = result;
             
                     
            }
        }

        private void Home_admin_Load(object sender, EventArgs e)
        {
            
            LoadTable();
            using (var context = new pallgree_cafeContext())
            {
                List<FoodCategory> list;
                list = context.FoodCategories.ToList();
                FoodCategory f = new FoodCategory();
                f.Id = -1;
                f.Name = "---Choose category---";
                list.Insert(0, f);
                comboBox1.DataSource = list;
                comboBox1.SelectedIndex = 0;
                comboBox1.DisplayMember = "Name";
                comboBox1.ValueMember = "id";

            }
            List<String> list2 = new List<String>();
            list2.Add("---Choose Food---");
            comboBox2.DataSource = list2;
            isFisrtChange = true;
           
        }
        private void button_Click(object sender, EventArgs e)
        {
            String[] vs = sender.ToString().Split(' ');
            label4.Text = vs[3].ToString();
            LoadBill(Int32.Parse(vs[3].ToString()));

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Employee emp = new Employee();
            emp.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Food_category emp = new Food_category();
            emp.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FoodView emp = new FoodView();
            emp.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TableView emp = new TableView();
            emp.Show();
            emp.FormClosed += TableView_FormClosed;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            BillView emp = new BillView();
            emp.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var context = new pallgree_cafeContext())
            {
                if (isFisrtChange) {
                    List<Food> list;
                    int id = Int32.Parse(comboBox1.SelectedValue.ToString());
                    list = context.Foods.Where(f => f.IdCategory == id).ToList();
                    Food food = new Food();
                    food.Name = "---Choose Food---";
                    list.Insert(0, food);
                    comboBox2.DataSource = list;
                    comboBox2.SelectedIndex = 0;
                    comboBox2.DisplayMember = "Name";
                    comboBox2.ValueMember = "id";
                }


            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try {
                int cb1 = Int32.Parse(comboBox1.SelectedValue.ToString());
                int cb2 = Int32.Parse(comboBox2.SelectedValue.ToString());
                if ( cb1 == 0 || cb2 == 0)
                {
                    throw new Exception();
                }
            } catch(Exception ex) {
                MessageBox.Show("You need choose Food and Category Food");
                return;
            }
            if (label4.Text.Contains("please")) {
                MessageBox.Show("You need choose Table");
                return;
            }
            
            bool check = true; 
            if (!label4.Text.Contains("please")) {
                int table = Int32.Parse(label4.Text.Trim());
                using (var context = new pallgree_cafeContext())
                {
                    context.Bills.ToList().ForEach(b =>
                    {
                        if (b.IdTable == table && b.Status == 1)
                        {
                            // truong hop ban da ton tai va chua thanh toan
                            check=false;
                        }
                       
                    });
                    if (check) {
                     
                            // tao ban moi
                            // danh dau ban
                            context.Tables.ToList().ForEach(t =>
                            {
                                if (t.Id == table) { t.Status = 1; }
                                context.Tables.Update(t);
                            });
                            //tao bill
                            Bill bill = new Bill();
                            bill.IdTable = table;
                            bill.Status = 1;
                            bill.TimeCheckin = DateTime.Now;
                            context.Bills.Add(bill);
                        
                    }  
                        context.SaveChanges();
                        
                   
                }

                using (var context = new pallgree_cafeContext())
                {
                    int idBill=-1;
                    context.Bills.ToList().ForEach(t =>
                    {
                        if (t.IdTable == table && t.Status == 1) {
                            idBill = t.Id;
                        } 
                    });


                    bool check2 = true;
                    context.BillDetails.ToList().ForEach(t =>
                    {
                        if (t.IdFood == Int32.Parse(comboBox2.SelectedValue.ToString()) && t.IdBill == idBill) {
                            check2 = false;
                            t.Count += (int)numericUpDown1.Value;
                            if (t.Count < 0) t.Count = 0;
                            context.BillDetails.Update(t);
                        }
                    });
                    if (check2) {
                        BillDetail bd = new BillDetail();
                        bd.IdBill = idBill;
                        bd.IdFood = Int32.Parse(comboBox2.SelectedValue.ToString());
                        bd.Count = (int)numericUpDown1.Value;
                        if (bd.Count < 0) bd.Count = 0;
                        context.BillDetails.Add(bd);
                       
                    }
                    context.SaveChanges();


                }

                LoadBill(Int32.Parse(label4.Text.Trim()));
                LoadTable();
            }
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            CustomerView emp = new CustomerView();
            emp.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DiscountView emp = new DiscountView();
            emp.Show();

        }

        private void button9_Click(object sender, EventArgs e)
        {
            int idBill=-1;
            bool condition = false;
            if (!label4.Text.Contains("please"))
            {
                int table = Int32.Parse(label4.Text.Trim());
                using (var context = new pallgree_cafeContext())
                {
                    context.Bills.ToList().ForEach(t =>
                    {
                        if (t.IdTable == table && t.Status == 1)
                        {
                            idBill = t.Id;
                            condition = true;   
                        }
                    });
                }

                if (condition) {
                    CheckOutView emp = new CheckOutView(idBill,table,user_name);
                    emp.Show();
                    emp.FormClosed += Emp_FormClosed;
                }
                
            }
        }

        private void Emp_FormClosed(object? sender, FormClosedEventArgs e)
        {
            LoadBill(Int32.Parse(label4.Text.Trim()));
            LoadTable();
        }

        private void Home_admin_FormClosing(object sender, FormClosingEventArgs e)
        {
            LoadTable();
        }

        private void TableView_FormClosed(object sender, FormClosedEventArgs e)
        {
            LoadTable();
        }
    }
}
