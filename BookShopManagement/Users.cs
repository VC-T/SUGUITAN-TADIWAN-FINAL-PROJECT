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


namespace BookShopManagement
{
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
            populate();
            this.UserDGV.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.UserDGV_CellContentClick);
            this.UserDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            UserDGV.MultiSelect = false;
            this.UserDGV.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSkyBlue;
            this.UserDGV.EnableHeadersVisualStyles = false;
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BookShopDB;Integrated Security=True;Connect Timeout=30;Encrypt=False");
        private void populate()
        {
            try
            {


                Con.Open();
                string query = "select * from UserTbl";
                SqlDataAdapter sda = new SqlDataAdapter(query, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
                sda.Fill(ds);
                UserDGV.DataSource = ds.Tables[0];
                

                for (int i = 0; i < UserDGV.Columns.Count; i++)
                {
                    UserDGV.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }

                Con.Close();
                // Clear selection
                UserDGV.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error populating DataGridView: " + ex.Message);
                Con.Close();
            }

        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (UnameTb.Text == "" || AddTb.Text == "" || PassTb.Text == "" || PhoneTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into UserTbl values('" + UnameTb.Text + "','" + PhoneTb.Text + "', '" + AddTb.Text + "','" + PassTb.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Saved Succesfully!");
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
        private void Reset()
        {
            UnameTb.Text = "";
            PhoneTb.Text = "";
            AddTb.Text = "";
            PassTb.Text = "";
        }

        private void ResetBtn_Click(object sender, EventArgs e)
        {
            Reset();
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
                    string query = "delete from UserTbl where UId=" + key + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Deleted Succesfully!");
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
        int key = 0;
        private void UserDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure the row index is valid
            if (e.RowIndex >= 0 && e.RowIndex < UserDGV.Rows.Count && !UserDGV.Rows[e.RowIndex].IsNewRow)
            {
                DataGridViewRow row = UserDGV.Rows[e.RowIndex];
                
                if (row.Cells.Count > 4)
                {
                    UnameTb.Text = row.Cells[1].Value?.ToString();
                    PhoneTb.Text = row.Cells[2].Value?.ToString();                   
                    AddTb.Text = row.Cells[3].Value?.ToString();
                    PassTb.Text = row.Cells[4].Value?.ToString();
                    if (UnameTb.Text == "")
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

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (UnameTb.Text == "" || AddTb.Text == "" || PassTb.Text == "" || PhoneTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "update UserTbl set UName='" + UnameTb.Text +
                                   "', UPhone='" + PhoneTb.Text +
                                   "', UAdd='" + AddTb.Text +
                                   "', UPass='" + PassTb.Text +
                                   "' where UId=" + key + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Updated Succesfully!");
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

        private void label6_Click(object sender, EventArgs e)
        {
            Books Obj = new Books();
            Obj.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Dashboard Obj = new Dashboard();
            Obj.Show();
            this.Hide();
        }
    }
}

