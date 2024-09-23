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
    public partial class AddCustomercs : Form
    {
        public AddCustomercs()
        {
            InitializeComponent();
        }
        //Check Validty
        private bool isEmpty()
        {
            // Check if any of the textboxes are empty or contain only whitespace
            if (string.IsNullOrWhiteSpace(txtBoxID_Cus.Text) ||
                string.IsNullOrWhiteSpace(txtBoxName_Cus.Text) ||
                string.IsNullOrWhiteSpace(txtBoxPhone_Cus.Text) ||
                string.IsNullOrWhiteSpace(txtBoxPass_Cus.Text) ||
                string.IsNullOrWhiteSpace(txtBoxLicense_Cus.Text) ||
                string.IsNullOrWhiteSpace(txtBoxAddress_Cus.Text) ||
                string.IsNullOrWhiteSpace(txtBoxAge_Cus.Text))
            {
                // Display an error message to the user
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // Return true if any textbox is empty
            }

            return true; // Return false if all textboxes are filled
        }

        public void LoadGrid(string Q)
        {
            SqlConnection con = new SqlConnection("Data Source=LAPTOP-J7JBFG4A\\SQLEXPRESS;Initial Catalog=CarBazar;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand(Q, con);
            cmd.Parameters.AddWithValue("@id", int.Parse(txtBoxID_Cus.Text));
            DataTable dt = new DataTable();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            dataGridView1.DataSource = dt.DefaultView;
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void SaveAdmin_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=LAPTOP-J7JBFG4A\\SQLEXPRESS;Initial Catalog=CarBazar;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from Customer", con);
            DataTable dt = new DataTable();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            dataGridView1.DataSource = dt.DefaultView;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            new Form3().Show();
        }

        private void SaveCus_Click(object sender, EventArgs e)
        {
            if (isEmpty())
            {
                try
                {
                    using (SqlConnection con = new SqlConnection("Data Source=LAPTOP-J7JBFG4A\\SQLEXPRESS;Initial Catalog=CarBazar;Integrated Security=True"))
                    {
                        con.Open();

                        using (SqlCommand cmd = new SqlCommand("insert into Customer (id,name,age,address,phone,licenseNo,password,UserType) values (@id,@name,@age,@address,@phone,@licenseNo,@password,@UserType)", con))
                        {
                            // Add parameters with proper data types
                            cmd.Parameters.AddWithValue("@id", int.Parse(txtBoxID_Cus.Text));
                            cmd.Parameters.AddWithValue("@name", txtBoxName_Cus.Text);
                            cmd.Parameters.AddWithValue("@phone", txtBoxPhone_Cus.Text);
                            cmd.Parameters.AddWithValue("@password", txtBoxPass_Cus.Text);
                            cmd.Parameters.AddWithValue("@licenseNo", txtBoxLicense_Cus.Text);
                            cmd.Parameters.AddWithValue("@address", txtBoxAddress_Cus.Text);
                            cmd.Parameters.AddWithValue("@age", txtBoxAge_Cus.Text);

                            cmd.Parameters.AddWithValue("@UserType", "customer");

                            // Execute the query
                            cmd.ExecuteNonQuery();
                        }

                        // Show success message
                        MessageBox.Show("Successfully Saved");
                        LoadGrid("Select * from Customer where id=@id");
                    }
                }
                catch (Exception ex)
                {
                    // Handle any errors that occur during database operations
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
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

        private void AddCustomercs_Load(object sender, EventArgs e)
        {

        }
    }
}
