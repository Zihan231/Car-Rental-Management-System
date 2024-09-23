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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        private void ShowBtn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=LAPTOP-J7JBFG4A\\SQLEXPRESS;Initial Catalog=CarBazar;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Car", con);
            DataTable dt = new DataTable();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            dataGridView1.DataSource = dt.DefaultView;
        }
        //Check Validity
        private bool IsFormValid()
        {
            // Check if any of the text boxes are empty
            if (string.IsNullOrWhiteSpace(txtBoxID_Car.Text) ||
                string.IsNullOrWhiteSpace(txtBoxName_Car.Text) ||
                string.IsNullOrWhiteSpace(txtBoxModel_Car.Text) ||
                string.IsNullOrWhiteSpace(txtBoxRent_Car.Text) ||
                string.IsNullOrWhiteSpace(txtBoxReg_Car.Text) ||
                string.IsNullOrWhiteSpace(txtBoxCompany_Car.Text))
            {
                // Show an error message and return false to indicate the form is invalid
                MessageBox.Show("Please fill out all required fields before submitting.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return false;
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            new Form3().Show();
        }

        private void UpdateAdmin_Click(object sender, EventArgs e)
        {
            if (IsFormValid()) {
                try
                {
                    using (SqlConnection con = new SqlConnection("Data Source=LAPTOP-J7JBFG4A\\SQLEXPRESS;Initial Catalog=CarBazar;Integrated Security=True"))
                    {
                        con.Open();

                        using (SqlCommand cmd = new SqlCommand("UPDATE Car SET name=@name, model=@model, rent=@rent, regNum=@regNum, company=@company WHERE id=@id", con))
                        {
                            // Add parameters with proper data types
                            cmd.Parameters.AddWithValue("@id", int.Parse(txtBoxID_Car.Text));
                            cmd.Parameters.AddWithValue("@name", txtBoxName_Car.Text);
                            cmd.Parameters.AddWithValue("@model", txtBoxModel_Car.Text);
                            cmd.Parameters.AddWithValue("@rent", txtBoxRent_Car.Text);
                            cmd.Parameters.AddWithValue("@regNum", txtBoxReg_Car.Text);
                            cmd.Parameters.AddWithValue("@company", txtBoxCompany_Car.Text);

                            // Execute the query
                            cmd.ExecuteNonQuery();
                        }

                        // Show success message
                        MessageBox.Show("The car details have been successfully updated.", "Update Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
                catch (Exception ex)
                {
                    // Handle any errors that occur during database operations
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtBoxID_Car.Text)) // Check if the ID textbox is NOT empty
            {
                
                if (int.TryParse(txtBoxID_Car.Text, out int carId))
                {
                    try
                    {
                        using (SqlConnection con = new SqlConnection("Data Source=LAPTOP-J7JBFG4A\\SQLEXPRESS;Initial Catalog=CarBazar;Integrated Security=True"))
                        {
                            con.Open();
                            SqlCommand cmd = new SqlCommand("select * from Car where id=@id", con);
                            cmd.Parameters.AddWithValue("@id", carId); // Use the parsed carId

                            DataTable dt = new DataTable();
                            SqlDataReader sdr = cmd.ExecuteReader();
                            dt.Load(sdr);

                            // Check if the DataTable is empty
                            if (dt.Rows.Count == 0)
                            {
                                MessageBox.Show("No car found with the provided ID.", "No Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        MessageBox.Show("Error: " + ex.Message); // Error handling
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid numeric ID.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid ID. The ID field cannot be left empty.", "ID Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);

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
    }
}
