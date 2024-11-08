using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace BookShopManagement
{
    public partial class Books : Form
    {
        public Books()
        {
            InitializeComponent();
            this.BookDGV.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.BookDGV_CellContentClick);
            this.BookDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            BookDGV.MultiSelect = false;
            this.BookDGV.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSkyBlue;           
            this.BookDGV.EnableHeadersVisualStyles = false;
            populate();
            
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

        private void Filter ()
        {
            Con.Open();
            string query = "select * from BookTbl where BCat='"+CatCbSearchCb.SelectedItem.ToString()+"'";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BookDGV.DataSource = ds.Tables[0];
              
            Con.Close();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (BTitleTb.Text == "" || BautTb.Text == "" || PriceTb.Text == "" || BCatTb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into BookTbl values('" + BTitleTb.Text + "','" + BautTb.Text + "', '" + BCatTb.SelectedItem.ToString() + "'," + QtyTb.Text + "," + PriceTb.Text + ")";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Book Saved Succesfully!");
                    Con.Close();
                    populate();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CatCbSearchCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filter();
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            populate();
            

        }
        private void Reset()
        {
            BTitleTb.Text = "";
            BautTb.Text = "";
            BCatTb.SelectedIndex = -1;
            PriceTb.Text = "";
            QtyTb.Text = "";
        }
        private void ResetBtn_Click(object sender, EventArgs e)
        {
            Reset();    
            
        }

        int key = 0;     
        private void BookDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure the row index is valid
            if (e.RowIndex >= 0 && e.RowIndex < BookDGV.Rows.Count && !BookDGV.Rows[e.RowIndex].IsNewRow)
            {
                DataGridViewRow row = BookDGV.Rows[e.RowIndex];

                if (row.Cells.Count > 5)
                {
                    BTitleTb.Text = row.Cells[1].Value?.ToString() ;
                    BautTb.Text = row.Cells[2].Value?.ToString() ;
                    BCatTb.SelectedItem = row.Cells[3].Value?.ToString() ;
                    QtyTb.Text = row.Cells[4].Value?.ToString() ;
                    PriceTb.Text = row.Cells[5].Value?.ToString() ;
                    if(BTitleTb.Text == "")
                    {
                        key = 0;
                    }
                    else
                    {
                        //Store the key for further operations (if needed)
                          key = Convert.ToInt32(row.Cells[0].Value);
                    }

                    
                }
                else
                {
                    MessageBox.Show("The selected row does not have the required number of cells.");
                }
            }
        }      

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "delete from BookTbl where BId=" + key + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Book Deleted Succesfully!");
                    Con.Close();
                    populate();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (BTitleTb.Text == "" || BautTb.Text == "" || PriceTb.Text == "" || BCatTb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "update BookTbl set BTitle='"+BTitleTb.Text+"',BAuthor='"+BautTb.Text+"',BCat='"+BCatTb.SelectedItem.ToString()+"',BQty="+QtyTb.Text+",BPrice="+PriceTb.Text+" where BId="+key+ ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Book Updated Succesfully!");
                    Con.Close();
                    populate();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }       

        private void label11_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Users Obj = new Users();
            Obj.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Dashboard Obj = new Dashboard();
            Obj.Show();
            this.Hide();
        }

        private void label8_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

