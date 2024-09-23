using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CarBazar
{
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();
        }

        public void ClearValue()
        {
            txtBoxID_Cus.Clear();
            txtBoxName_Cus.Clear();
            txtBoxPhone_Cus.Clear();
            txtBoxPass_Cus.Clear();
            txtBoxLicense_Cus.Clear();
            txtBoxAddress_Cus.Clear();
            txtBoxAge_Cus.Clear();
        }

        // Check if inputs are empty
        public bool isEmpty()
        {
            if (txtBoxName_Cus.Text == string.Empty)
            {
                MessageBox.Show("Name is required!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtBoxID_Cus.Text == string.Empty)
            {
                MessageBox.Show("ID is required!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtBoxPhone_Cus.Text == string.Empty)
            {
                MessageBox.Show("Phone number is required!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtBoxAge_Cus.Text == string.Empty)
            {
                MessageBox.Show("Age is required!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtBoxPass_Cus.Text == string.Empty)
            {
                MessageBox.Show("Password is required!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtBoxLicense_Cus.Text == string.Empty)
            {
                MessageBox.Show("License is required!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtBoxAddress_Cus.Text == string.Empty)
            {
                MessageBox.Show("Address is required!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        // Check if inputs are in correct data types
        public bool isDataTypeValid()
        {
            int id, age;
            long phone;

            // Validate ID
            if (!int.TryParse(txtBoxID_Cus.Text, out id))
            {
                MessageBox.Show("ID must be a valid integer!", "Data Type Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Validate Phone Number
            if (!long.TryParse(txtBoxPhone_Cus.Text, out phone))
            {
                MessageBox.Show("Phone number must be numeric!", "Data Type Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Validate Age
            if (!int.TryParse(txtBoxAge_Cus.Text, out age))
            {
                MessageBox.Show("Age must be a valid integer!", "Data Type Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Optional: Add more validation (e.g., password length, name format, etc.)
            if (txtBoxPass_Cus.Text.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters long!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (isEmpty() && isDataTypeValid())
            {
                try
                {
                    using (SqlConnection con = new SqlConnection("Data Source=LAPTOP-J7JBFG4A\\SQLEXPRESS;Initial Catalog=CarBazar;Integrated Security=True"))
                    {
                        con.Open();

                        using (SqlCommand cmd = new SqlCommand("INSERT INTO Customer (id, name, age, address, phone, licenseNo, password, UserType) VALUES (@id, @name, @age, @address, @phone, @licenseNo, @password, @UserType)", con))
                        {
                            // Add parameters with proper data types
                            cmd.Parameters.AddWithValue("@id", int.Parse(txtBoxID_Cus.Text));
                            cmd.Parameters.AddWithValue("@name", txtBoxName_Cus.Text);
                            cmd.Parameters.AddWithValue("@phone", txtBoxPhone_Cus.Text);
                            cmd.Parameters.AddWithValue("@password", txtBoxPass_Cus.Text);
                            cmd.Parameters.AddWithValue("@licenseNo", txtBoxLicense_Cus.Text);
                            cmd.Parameters.AddWithValue("@address", txtBoxAddress_Cus.Text);
                            cmd.Parameters.AddWithValue("@age", int.Parse(txtBoxAge_Cus.Text));
                            cmd.Parameters.AddWithValue("@UserType", "customer");

                            // Execute the query
                            cmd.ExecuteNonQuery();
                        }

                        // Show success message
                        MessageBox.Show("Successfully Registered!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Redirect to login form
                        this.Close();
                        new Form1().Show();
                    }
                }
                catch (Exception ex)
                {
                    // Handle any errors that occur during database operations
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            new Form1().Show();
        }

        private void SaveCus_Click(object sender, EventArgs e)
        {
            // Clear all textboxes
            ClearValue();

            // Show a message indicating that the form has been reset
            MessageBox.Show("Form cleared successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
