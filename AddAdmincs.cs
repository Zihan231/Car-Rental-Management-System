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
    public partial class AddAdmincs : Form
    {
        public AddAdmincs()
        {
            InitializeComponent();
        }

        public bool isEmpty()
        {
            

            if (txtBoxName.Text == string.Empty)
            {
                MessageBox.Show("Name required !!!");
                return false;
            }
            if (txtBoxID.Text == string.Empty)
            {
                MessageBox.Show("ID required !!!");
                return false;
            }
             if (txtBoxPhn.Text == string.Empty)
            {
                MessageBox.Show("Phone required !!!");
                return false;
            }
            if (txtBoxSalary.Text == string.Empty)
            {
                MessageBox.Show("Salary required !!!");
                return false;
            }
             if (txtBoxPass.Text == string.Empty)
            {
                MessageBox.Show("Password required !!!");
                return false;
            }


            return true;
        }
        public void LoadGrid(string Q)
        {
            SqlConnection con = new SqlConnection("Data Source=LAPTOP-J7JBFG4A\\SQLEXPRESS;Initial Catalog=CarBazar;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand(Q, con);
            cmd.Parameters.AddWithValue("@id", int.Parse(txtBoxID.Text));
            DataTable dt = new DataTable();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            dataGridView1.DataSource = dt.DefaultView;
        }

        private void SaveAdmin_Click(object sender, EventArgs e)
        {
            if (isEmpty())
            {
                try
                {
                    using (SqlConnection con = new SqlConnection("Data Source=LAPTOP-J7JBFG4A\\SQLEXPRESS;Initial Catalog=CarBazar;Integrated Security=True"))
                    {
                        con.Open();

                        using (SqlCommand cmd = new SqlCommand("insert into Admin (id, name, phone, password, salary, UserType) values (@id, @name, @phone, @password, @salary, @UserType)", con))
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
                            con.Close();
                        }

                        // Show success message
                        MessageBox.Show("Successfully Saved");
                        LoadGrid("select * from Admin Where id=@id");

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
            this.Close();
            new Form3().Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=LAPTOP-J7JBFG4A\\SQLEXPRESS;Initial Catalog=CarBazar;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from Admin", con);
            DataTable dt = new DataTable();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            dataGridView1.DataSource = dt.DefaultView;
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
