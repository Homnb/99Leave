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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static string mystrngconn = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text) || (string.IsNullOrEmpty(txtPassword.Text)))
            {
                MessageBox.Show("Username and Password can not be empty");
            }
            try
            {
                SqlConnection con = new SqlConnection(mystrngconn);
                SqlCommand cmd = new SqlCommand("SELECT * FROM EmpDetails WHERE Username=@Username and Password=@Password", con);
                cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                sda.Fill(ds);
                con.Close();
                int count = ds.Tables[0].Rows.Count;
                //int id = ds.Tables[0].Rows.
                if (count == 1)
                {
                    MessageBox.Show("Login Successful");
                    this.Hide();
                    Form2 fm = new Form2();
                    fm.Show();
                }
                else
                {
                    MessageBox.Show("Login Failed");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 fm = new Form3();
            fm.Show();
        }
    }
}
