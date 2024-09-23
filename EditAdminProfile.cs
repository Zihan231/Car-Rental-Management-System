using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CarBazar
{
    public partial class EditAdminProfile : Form
    {
        public EditAdminProfile()
        {
            InitializeComponent();

            SqlConnection con = new SqlConnection("Data Source=LAPTOP-J7JBFG4A\\SQLEXPRESS;Initial Catalog=CarBazar;Integrated Security=True");

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT id FROM Admin WHERE id = @id", con);
                cmd.Parameters.AddWithValue("@id", Form1.LoginID);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        ID.Text = reader["id"].ToString();
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            new AdminProfile().Show();
        }

        private void EditAdminProfile_Load(object sender, EventArgs e)
        {
        }

        // Function to check if textboxes are empty
        private bool AreFieldsEmpty()
        {
            if (string.IsNullOrWhiteSpace(txtBoxName.Text) ||
                string.IsNullOrWhiteSpace(txtBoxPhn.Text) ||
                string.IsNullOrWhiteSpace(txtBoxSalary.Text))
            {
                MessageBox.Show("Please fill in all the fields.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            return false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Check if fields are empty
            if (AreFieldsEmpty())
                return;

            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=LAPTOP-J7JBFG4A\\SQLEXPRESS;Initial Catalog=CarBazar;Integrated Security=True"))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE Admin SET name = @name, phone = @phone, salary = @salary WHERE id = @id", con))
                    {
                        cmd.Parameters.AddWithValue("@id", Form1.LoginID);
                        cmd.Parameters.AddWithValue("@name", txtBoxName.Text);
                        cmd.Parameters.AddWithValue("@phone", txtBoxPhn.Text);
                        cmd.Parameters.AddWithValue("@salary", decimal.Parse(txtBoxSalary.Text));

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Profile Successfully Updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
