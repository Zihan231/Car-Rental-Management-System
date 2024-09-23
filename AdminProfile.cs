using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarBazar
{
    public partial class AdminProfile : Form
    {
        public AdminProfile()
        {
            InitializeComponent();




            SqlConnection con = new SqlConnection("Data Source=LAPTOP-J7JBFG4A\\SQLEXPRESS;Initial Catalog=CarBazar;Integrated Security=True");

            try
            {
                // Open the connection
                con.Open();

                // Define the SQL query to fetch data based on the logged-in admin's ID
                SqlCommand cmd = new SqlCommand("SELECT name, id, phone, salary FROM Admin WHERE id = @id", con);
                cmd.Parameters.AddWithValue("@id", Form1.LoginID);

                // Execute the command and retrieve the data
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())  // Check if any data was returned
                    {
                        // Assign the data to the respective labels
                        name.Text = reader["name"].ToString();
                        id.Text = reader["id"].ToString();
                        phn.Text = reader["phone"].ToString();
                        salary.Text = reader["salary"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("No data found for the given Admin ID.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Close the connection
                con.Close();
            }

        }

        private void AdminProfile_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            new Form3().Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            new EditAdminProfile().Show();
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {

        }
    }
}
