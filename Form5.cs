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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
 
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=LAPTOP-J7JBFG4A\\SQLEXPRESS;Initial Catalog=CarBazar;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Customer", con);
            DataTable dt = new DataTable();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            dataGridView1.DataSource = dt.DefaultView;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
            new Form3().Show();
        }
        //Checking Validity
        private bool isEmpty()
        {
            if (string.IsNullOrWhiteSpace(txtBoxID_Cus.Text) ||
                string.IsNullOrWhiteSpace(txtBoxName_Cus.Text) ||
                string.IsNullOrWhiteSpace(txtBoxPhone_Cus.Text) ||
                string.IsNullOrWhiteSpace(txtBoxPass_Cus.Text) ||
                string.IsNullOrWhiteSpace(txtBoxLicense_Cus.Text) ||
                string.IsNullOrWhiteSpace(txtBoxAddress_Cus.Text) ||
                string.IsNullOrWhiteSpace(txtBoxAge_Cus.Text))
            {
                MessageBox.Show("Please fill in all the required fields.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false; // One or more textboxes are empty
            }

            return true; // All textboxes are filled
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Check if the ID textbox is empty
            if (string.IsNullOrWhiteSpace(txtBoxID_Cus.Text))
            {
                MessageBox.Show("Please enter a Customer ID.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit the method if the ID is empty
            }

            // Check if the ID is a valid integer
            if (!int.TryParse(txtBoxID_Cus.Text, out int customerId))
            {
                MessageBox.Show("Please enter a valid numeric Customer ID.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit the method if the ID is not a valid integer
            }

            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=LAPTOP-J7JBFG4A\\SQLEXPRESS;Initial Catalog=CarBazar;Integrated Security=True"))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select * from Customer where id=@id", con);
                    cmd.Parameters.AddWithValue("@id", customerId);
                    DataTable dt = new DataTable();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    dt.Load(sdr);

                    // Check if the DataTable is empty
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No customer found with the provided ID.", "No Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        dataGridView1.DataSource = dt.DefaultView;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void button7_Click(object sender, EventArgs e)
        {
            if (isEmpty())
            {
                try
                {
                    using (SqlConnection con = new SqlConnection("Data Source=LAPTOP-J7JBFG4A\\SQLEXPRESS;Initial Catalog=CarBazar;Integrated Security=True"))
                    {
                        con.Open();

                        using (SqlCommand cmd = new SqlCommand("UPDATE Customer SET name = @name, age = @age, address = @address, phone = @phone, licenseNo = @licenseNo, password = @password, UserType = @UserType WHERE id = @id", con))
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

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
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
