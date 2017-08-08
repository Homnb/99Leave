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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        public class Register
        {
            public string Name { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string Address { get; set; }
            public string Phone { get; set; }
        }
        Register c = new Register();
            static string mystrngconn = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        private void btnSignup_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrEmpty(txtAddress.Text) || string.IsNullOrEmpty(txtPhone.Text))
            {
                MessageBox.Show("Please fill up the form properly");
            }
            else
            {
                c.Name = txtName.Text;
                c.Username = txtUsername.Text;
                c.Password = txtPassword.Text;
                c.Address = txtPhone.Text;
                c.Phone = txtPhone.Text;

                try
                {
                    string query = "INSERT INTO EmpDetails(Name,Username,Password,Address,Phone) VALUES ('" + this.txtName.Text + "','" + this.txtUsername.Text + "','" + this.txtPassword.Text + "','" + this.txtAddress.Text + "','" + this.txtPhone.Text + "');";
                    SqlConnection con = new SqlConnection(mystrngconn);
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader sdr;
                    con.Open();
                    sdr = cmd.ExecuteReader();
                    MessageBox.Show("Successfully Registered ");
                    this.Hide();
                    Form1 fm = new Form1();
                    fm.Show();
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
        }
    }
}
