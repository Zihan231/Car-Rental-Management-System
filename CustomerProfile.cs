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
    public partial class CustomerProfile : Form
    {
        public CustomerProfile()
        {
            InitializeComponent();
            //SQL Load

            // Define the connection string (adjust as per your actual setup)
            SqlConnection con = new SqlConnection("Data Source=LAPTOP-J7JBFG4A\\SQLEXPRESS;Initial Catalog=CarBazar;Integrated Security=True");

            try
            {
                // Open the connection
                con.Open();

                // Define the SQL query to fetch data based on the logged-in admin's ID
                SqlCommand cmd = new SqlCommand("SELECT name, id, phone, age, address, licenseNo, password FROM Customer WHERE id = @id", con);
                cmd.Parameters.AddWithValue("@id", Form1.LoginID);  // Assuming 'LoginID' holds the logged-in admin's ID

                // Execute the command and retrieve the data
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())  // Check if any data was returned
                    {
                        // Assign the data to the respective labels
                        name.Text = reader["name"].ToString();
                        id.Text = reader["id"].ToString();
                        phn.Text = reader["phone"].ToString();
                        age.Text = reader["age"].ToString();          // Added 'age'
                        address.Text = reader["address"].ToString();  // Added 'address'
                        licenseNo.Text = reader["licenseNo"].ToString();  // Added 'licenseNo'
                        pass.Text = reader["password"].ToString();    // Added 'password'
                    }
                    else
                    {
                        MessageBox.Show("No data found for the given ID.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Show error message
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Close the connection
                con.Close();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            new Form2().Show();   
        }

        private void CustomerProfile_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new EditCustomerDetails().Show();
        }
    }
}
