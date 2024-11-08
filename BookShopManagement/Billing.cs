using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Diagnostics;

namespace BookShopManagement
{
    public partial class Billing : Form
    {
        public Billing()
        {
            InitializeComponent();
            populate();
            this.BookDGV.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.BookDGV_CellContentClick);
            this.BookDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            BookDGV.MultiSelect = false;
            this.BookDGV.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSalmon;
            this.BookDGV.EnableHeadersVisualStyles = false;
            printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(printDocument1_PrintPage);

           
        }
       

        SqlConnection Con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BookShopDB;Integrated Security=True;Connect Timeout=30;Encrypt=False");
        private void populate()
        {
            try
            {


                Con.Open();
                string query = "select * from BookTbl";
                SqlDataAdapter sda = new SqlDataAdapter(query, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
                sda.Fill(ds);
                BookDGV.DataSource = ds.Tables[0];

                for (int i = 0; i < BookDGV.Columns.Count; i++)
                {
                    BookDGV.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }

                Con.Close();
                // Clear selection
                BookDGV.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error populating DataGridView: " + ex.Message);
                Con.Close();
            }

        }
        private void UpdateBook()
        {
            int newQty = stock - Convert.ToInt32(QtyTb.Text);

            try
            {
                Con.Open();
                string query = "update BookTbl set BQty=" + newQty + " where BId=" + key + ";"; 
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
               // MessageBox.Show("Book Updated Succesfully!");
                Con.Close();
                populate();
                //Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        int n = 0, GrdTotal =0;
        
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (QtyTb.Text == "" || !int.TryParse(QtyTb.Text, out int quantity))
            {
                MessageBox.Show("Invalid quantity.");
            }
            else if (quantity > stock)
            {
                MessageBox.Show("No enough stock.");
            }
            else if (PriceTb.Text == "" || !decimal.TryParse(PriceTb.Text, out decimal price))
            {
                MessageBox.Show("Invalid price.");
            }
            else
            {
                decimal total = quantity * price;
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(BillDGV);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = BTitleTb.Text;
                newRow.Cells[2].Value = quantity;
                newRow.Cells[3].Value = price;
                newRow.Cells[4].Value = total;
                BillDGV.Rows.Add(newRow);
                n++;
                UpdateBook();
                GrdTotal += (int)total;
                TotalLbl.Text = "Php " + GrdTotal;
            }
        }

        int key = 0, stock = 0;

        private void BookDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure the row index is valid
            if (e.RowIndex >= 0 && e.RowIndex < BookDGV.Rows.Count && !BookDGV.Rows[e.RowIndex].IsNewRow)
            {
                DataGridViewRow row = BookDGV.Rows[e.RowIndex];

                if (row.Cells.Count > 5)
                {
                    BTitleTb.Text = row.Cells[1].Value?.ToString();           
                    //QtyTb.Text = row.Cells[4].Value?.ToString();
                    PriceTb.Text = row.Cells[5].Value?.ToString();
                    if (BTitleTb.Text == "")
                    {
                        key = 0;
                        stock = 0;
                    }
                    else
                    {
                        //Store the key for further operations (if needed)
                        key = Convert.ToInt32(row.Cells[0].Value);
                        stock = Convert.ToInt32(row.Cells[4].Value);

                    }


                }
                else
                {
                    MessageBox.Show("The selected row does not have the required number of cells.");
                }
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }      
           
        int prodid, prodqty, prodprice, tottal, pos = 60;

        private void label11_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }       

        private void label8_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Billing_Load(object sender, EventArgs e)
        {
            UserNameLbl.Text = Login.UserName;
        }

        string prodname;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            e.Graphics.DrawString("Book Shop", new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Red, new Point(80));
            e.Graphics.DrawString("ID  PRODUCT   QUANTITY  PRICE  TOTAL", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Red, new Point(26, 40));

            
            foreach (DataGridViewRow row in BillDGV.Rows)
            {                
                    prodid = Convert.ToInt32(row.Cells[0].Value);
                    prodname = "" + row.Cells[1].Value?.ToString();
                    prodprice = Convert.ToInt32(row.Cells[2].Value);
                    prodqty = Convert.ToInt32(row.Cells[3].Value);
                    tottal = Convert.ToInt32(row.Cells[4].Value);
                    e.Graphics.DrawString("" + prodid, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(26, pos));
                    e.Graphics.DrawString("" + prodname, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(45, pos));
                    e.Graphics.DrawString("       " + prodprice, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(120, pos));
                    e.Graphics.DrawString("        " + prodqty, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(170, pos));
                    e.Graphics.DrawString("    " + tottal, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(235, pos));
                    pos = pos + 20;
                    

            }
            e.Graphics.DrawString("Grand Total: Php " + GrdTotal, new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Crimson, new Point(60, pos + 50));
            e.Graphics.DrawString("***********BookStore***********", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Crimson, new Point(40, pos + 85));
            BillDGV.Rows.Clear();
            BillDGV.Refresh();
            pos = 100;
            GrdTotal = 0;
            
        }
       
        private void button5_Click(object sender, EventArgs e)
        {          
            if (ClientNameTb.Text == "" || BTitleTb.Text == "")
            {
                MessageBox.Show("Select Client Name.");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into BillTbl values('" + UserNameLbl.Text + "','" + ClientNameTb.Text + "'," + GrdTotal + ")";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Bill Saved Succesfully!");
                    Con.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, 600);
                if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();
                }
                
            }

        }
        
        private void Reset()
        {
            BTitleTb.Text = "";
            QtyTb.Text = "";
            PriceTb.Text = "";
            ClientNameTb.Text = "";
        }
        private void ResetBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }
    }
    

}

