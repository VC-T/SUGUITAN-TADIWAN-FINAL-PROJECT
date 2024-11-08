
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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
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

        private void label9_Click(object sender, EventArgs e)
        {
            Users Obj = new Users();
            Obj.Show();
            this.Hide();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BookShopDB;Integrated Security=True;Connect Timeout=30;Encrypt=False");

        private void Dashboard_Load(object sender, EventArgs e)
        {
            try
            {
                Con.Open();

                // Book Stock
                SqlDataAdapter sda = new SqlDataAdapter("select sum(BQty) from BookTbl", Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
                {
                    BookStockLbl.Text = dt.Rows[0][0].ToString();
                }
                else
                {
                    BookStockLbl.Text = "0";
                }

                // Total Amount (QTY * Price)
                SqlDataAdapter sda1 = new SqlDataAdapter("select sum(BQty * BPrice) from BookTbl", Con);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                if (dt1.Rows.Count > 0 && dt1.Rows[0][0] != DBNull.Value)
                {
                    AmountLbl.Text = dt1.Rows[0][0].ToString();
                }
                else
                {
                    AmountLbl.Text = "0";
                }

                // User Count
                SqlDataAdapter sda2 = new SqlDataAdapter("select Count(*) from UserTbl", Con);
                DataTable dt2 = new DataTable();
                sda2.Fill(dt2);
                if (dt2.Rows.Count > 0 && dt2.Rows[0][0] != DBNull.Value)
                {
                    UserTotalLbl.Text = dt2.Rows[0][0].ToString();
                }
                else
                {
                    UserTotalLbl.Text = "0";
                }

                Con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading dashboard data: " + ex.Message);
                if (Con.State == ConnectionState.Open)
                {
                    Con.Close();
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
