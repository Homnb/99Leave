using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _99Leave
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        static string mystrngconn = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        private void btnCheckin_Click(object sender, EventArgs e)
        {
            DateTime time = DateTime.Now;
            string format = "yyyy-MM-dd HH:mm:ss";
            try
            {
                string query = @"INSERT INTO UsrTime(EmpID,Checkin) VALUES ((select  top 1 ID from EmpDetails) , '" + time.ToString(format) + "') ;";
                SqlConnection con = new SqlConnection(mystrngconn);
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                int rows = cmd.ExecuteNonQuery();
                con.Close();
                if (rows > 0)
                {
                    MessageBox.Show("Successfully Checkedin");
                    data_Display();
                }
                else
                {
                    MessageBox.Show("Not Checkedin");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            DateTime time = DateTime.Now;
            string format = "yyyy-MM-dd HH:mm:ss";
            try
            {
                string query = @"UPDATE u set Checkout ='" + time.ToString(format) + "' from UsrTime  as u inner join EmpDetails e on u.ID=e.ID;";
                SqlConnection con = new SqlConnection(mystrngconn);
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sdr;
                con.Open();
                sdr = cmd.ExecuteReader();
                MessageBox.Show("Successfully Checkedout");
                data_Display();
                while (sdr.Read())
                {

                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void data_Display()
        {
            try
            {
                SqlConnection con = new SqlConnection(mystrngconn);
                con.Open();
                SqlCommand cmd = new SqlCommand(@"Select e.Name, u.CheckIn, u.CheckOut From UsrTime u inner join EmpDetails e on u.EmpID = e.ID where e.ID = 2 order by u.CheckIn asc ", con);
                cmd.Parameters.AddWithValue("@Checkin", btnCheckin.Text);
                cmd.Parameters.AddWithValue("@Checkout", btnCheckout.Text);
                
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dgvDisplay.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            data_Display();
        }
    }
}
