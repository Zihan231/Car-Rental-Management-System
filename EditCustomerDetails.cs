using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CarBazar
{
    public partial class EditCustomerDetails : Form
    {
        public EditCustomerDetails()
        {
            InitializeComponent();
        }

        private void EditCustomerDetails_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=LAPTOP-J7JBFG4A\\SQLEXPRESS;Initial Catalog=CarBazar;Integrated Security=True");

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT id FROM Customer WHERE id = @id", con);
                cmd.Parameters.AddWithValue("@id", Form1.LoginID);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        id.Text = reader["id"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("No data found for the given Admin ID.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private bool CheckIfFieldsAreEmpty()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtPhn.Text) ||
                string.IsNullOrWhiteSpace(txtPass.Text) ||
                string.IsNullOrWhiteSpace(txtLicenseNo.Text) ||
                string.IsNullOrWhiteSpace(txtAddress.Text) ||
                string.IsNullOrWhiteSpace(txtAge.Text))
            {
                MessageBox.Show("Please fill in all the fields.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!CheckIfFieldsAreEmpty())
                return;

            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=LAPTOP-J7JBFG4A\\SQLEXPRESS;Initial Catalog=CarBazar;Integrated Security=True"))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE Customer SET name = @name, age = @age, address = @address, phone = @phone, licenseNo = @licenseNo, password = @password, UserType = @UserType WHERE id = @id", con);

                    cmd.Parameters.AddWithValue("@id", Form1.LoginID);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@phone", txtPhn.Text);
                    cmd.Parameters.AddWithValue("@password", txtPass.Text);
                    cmd.Parameters.AddWithValue("@licenseNo", txtLicenseNo.Text);
                    cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@age", txtAge.Text);
                    cmd.Parameters.AddWithValue("@UserType", "customer");

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Successfully Updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            new Form2().Show();
        }
    }
}
