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
    public partial class AddCars : Form
    {
        public AddCars()
        {
            InitializeComponent();
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
        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void SaveAdmin_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=LAPTOP-J7JBFG4A\\SQLEXPRESS;Initial Catalog=CarBazar;Integrated Security=True"))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Car (id, name, model, rent, regNum, Company, status, Frequency) VALUES (@id, @name, @model, @rent, @regNum, @Company, @status, @Frequency)", con))
                    {
                        cmd.Parameters.AddWithValue("@id", int.Parse(txtBoxID.Text));
                        cmd.Parameters.AddWithValue("@regNum", int.Parse(txtBoxReg.Text));
                        cmd.Parameters.AddWithValue("@name", txtBoxName.Text);
                        cmd.Parameters.AddWithValue("@model", txtBoxModel.Text);
                        cmd.Parameters.AddWithValue("@Company", txtBoxCompany.Text);
                        cmd.Parameters.AddWithValue("@rent", decimal.Parse(txtBoxRent.Text));
                        cmd.Parameters.AddWithValue("@status", "Free");
                        cmd.Parameters.AddWithValue("@Frequency", 0);

                        // Execute the query
                        cmd.ExecuteNonQuery();
                    }

                    // Show success message
                    MessageBox.Show("Car information saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadGrid("SELECT * FROM Car WHERE id=@id");
                }
            }
            catch (FormatException fe)
            {
                MessageBox.Show("Please ensure all fields are filled out correctly. Error: " + fe.Message, "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error occurred: " + sqlEx.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            SqlCommand cmd = new SqlCommand("Select * from Car", con);
            DataTable dt = new DataTable();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            dataGridView1.DataSource = dt.DefaultView;
        }

        private void label3_Click(object sender, EventArgs e)
        {

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

        private void AddCars_Load(object sender, EventArgs e)
        {

        }
    }
}
