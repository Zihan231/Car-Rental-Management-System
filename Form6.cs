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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        //Checking validity
        private bool isEmpty()
        {
            // Check if any of the textboxes are empty or contain only whitespace
            if (string.IsNullOrWhiteSpace(txtBoxID.Text) ||
                string.IsNullOrWhiteSpace(txtBoxName.Text) ||
                string.IsNullOrWhiteSpace(txtBoxPhn.Text) ||
                string.IsNullOrWhiteSpace(txtBoxPass.Text) ||
                string.IsNullOrWhiteSpace(txtBoxSalary.Text))
            {
                // Display an error message to the user
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // Return true if any textbox is empty
            }

            return true; // Return false if all textboxes are filled
        }


        private void Form6_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=LAPTOP-J7JBFG4A\\SQLEXPRESS;Initial Catalog=CarBazar;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Admin", con);
            DataTable dt = new DataTable();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            dataGridView1.DataSource = dt.DefaultView;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtBoxPhn_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (isEmpty())
            {
                try
                {
                    using (SqlConnection con = new SqlConnection("Data Source=LAPTOP-J7JBFG4A\\SQLEXPRESS;Initial Catalog=CarBazar;Integrated Security=True"))
                    {
                        con.Open();

                        using (SqlCommand cmd = new SqlCommand("UPDATE Admin SET name = @name, phone = @phone, password = @password, salary = @salary, UserType = @UserType WHERE id = @id", con))
                        {
                            // Add parameters with proper data types
                            cmd.Parameters.AddWithValue("@id", int.Parse(txtBoxID.Text));
                            cmd.Parameters.AddWithValue("@name", txtBoxName.Text);
                            cmd.Parameters.AddWithValue("@phone", txtBoxPhn.Text);
                            cmd.Parameters.AddWithValue("@password", txtBoxPass.Text);
                            cmd.Parameters.AddWithValue("@salary", decimal.Parse(txtBoxSalary.Text)); // Assuming salary is a decimal
                            cmd.Parameters.AddWithValue("@UserType", "admin");

                            // Execute the query
                            cmd.ExecuteNonQuery();
                        }

                        // Show success message
                        MessageBox.Show("Successfully Updated !!!");
                    }
                }
                catch (Exception ex)
                {
                    // Handle any errors that occur during database operations
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtBoxID.Text))
            {
                // Try parsing the ID to ensure it's a valid integer
                if (int.TryParse(txtBoxID.Text, out int adminId))
                {
                    try
                    {
                        using (SqlConnection con = new SqlConnection("Data Source=LAPTOP-J7JBFG4A\\SQLEXPRESS;Initial Catalog=CarBazar;Integrated Security=True"))
                        {
                            con.Open();
                            SqlCommand cmd = new SqlCommand("select * from Admin where id=@id", con);
                            cmd.Parameters.AddWithValue("@id", adminId);

                            DataTable dt = new DataTable();
                            SqlDataReader sdr = cmd.ExecuteReader();
                            dt.Load(sdr);

                            // Check if the DataTable is empty
                            if (dt.Rows.Count == 0)
                            {
                                MessageBox.Show("No admin found with the provided ID.", "No Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                // Bind the result to the DataGridView
                                dataGridView1.DataSource = dt.DefaultView;
                            }

                            con.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        // Display the error message in case something goes wrong
                        MessageBox.Show("Error fetching data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // If the input isn't a valid integer, notify the user
                    MessageBox.Show("Please enter a valid numeric ID.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                // If the text box is empty, notify the user
                MessageBox.Show("ID field cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            new Form3().Show();
        }

        private void button5_Click(object sender, EventArgs e)
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

    }
}
