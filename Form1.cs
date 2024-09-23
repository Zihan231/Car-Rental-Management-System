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

namespace CarBazar
{
    public partial class Form1 : Form
    {
        public static int LoginID;
        public Form1()
        {
            InitializeComponent();
        }
        

        string UserType;
        public bool isEmpty()
        {
            if (UserType == null)
            {
                MessageBox.Show("Please select a role to proceed.", "Role Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (txtID.Text == string.Empty)
            {
                MessageBox.Show("ID is required. Please enter a valid ID to continue.", "ID Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if(txtPass.Text == string.Empty)
            {
                MessageBox.Show("Password is required. Please enter your password to proceed.", "Password Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            

            return true;
        }
        public void ClearValue()
        {
            RdAdmin.Checked = false;
            RdCustomer.Checked = false;
            txtID.Clear();
            txtPass.Clear();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            UserType = "Customer";
        }

        private void exit_Click(object sender, EventArgs e)
        {
            // Show a confirmation message box
                DialogResult result = MessageBox.Show("Are you sure you want to exit?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // Check the user's response
                if (result == DialogResult.Yes)
                {
                    Application.Exit(); // Exit the application if user selects "Yes"
                }
                // Do nothing if user selects "No"
        }

        private void login_Click(object sender, EventArgs e)
        {
            if (isEmpty()) {
                
                if (UserType == "Customer")
                {
                    try
                    {
                        SqlConnection con = new SqlConnection("Data Source=LAPTOP-J7JBFG4A\\SQLEXPRESS;Initial Catalog=CarBazar;Integrated Security=True");
                        con.Open();
                        SqlCommand cmd = new SqlCommand("select id from Customer where id = @id and password = @password", con);
                        cmd.Parameters.AddWithValue("@id", int.Parse(txtID.Text));
                        cmd.Parameters.AddWithValue("@password", txtPass.Text);
                        var result = cmd.ExecuteScalar();
                        con.Close();
                        if (result != null)
                        {
                            LoginID = int.Parse(txtID.Text);
                            MessageBox.Show("Login successful! Welcome back.", "Login Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Hide();
                            new Form2().Show();
                            ClearValue();
                        }
                        else
                        {
                            MessageBox.Show("The ID or password you entered is incorrect. Please try again.", "Authentication Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            ClearValue();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
                else if (UserType == "Admin")
                {
                    try
                    {
                        SqlConnection con = new SqlConnection("Data Source=LAPTOP-J7JBFG4A\\SQLEXPRESS;Initial Catalog=CarBazar;Integrated Security=True");
                        con.Open();
                        SqlCommand cmd = new SqlCommand("select id from Admin where id = @id and password = @password", con);
                        cmd.Parameters.AddWithValue("@id", int.Parse(txtID.Text));
                        cmd.Parameters.AddWithValue("@password", txtPass.Text);
                        var result = cmd.ExecuteScalar();
                        con.Close();
                        if (result != null)
                        {
                            MessageBox.Show("Login successful! Welcome back.", "Login Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoginID = int.Parse(txtID.Text);    
                            this.Hide();
                            Form3 obj =  new Form3();
                            obj.Show();
                            ClearValue();
                        }
                        else
                        {
                            MessageBox.Show("The ID or password you entered is incorrect. Please try again.", "Authentication Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            ClearValue();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }

            }


            

        }

        private void RdAdmin_CheckedChanged(object sender, EventArgs e)
        {
            UserType = "Admin";
        }

        private void signUp_Click(object sender, EventArgs e)
        {
            this.Hide();
            new SignUp().Show();
        }
    }
}
