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
    public partial class ReportSubmission : Form
    {
        public ReportSubmission()
        {
            InitializeComponent();

            LoadGrid($"Select * from Booking where Cus_id = {Form1.LoginID}");
        }

        private void ReportSubmission_Load(object sender, EventArgs e)
        {

        }

        public void LoadGrid(string Query)
        {
            SqlConnection Con = new SqlConnection("Data Source=LAPTOP-J7JBFG4A\\SQLEXPRESS;Initial Catalog=CarBazar;Integrated Security=True");
            Con.Open();
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            Con.Close();
            dataGridView1.DataSource = dt.DefaultView;
        }

        private void FindBtn_Click(object sender, EventArgs e)
        {
            int bookingID;
            
            if (!string.IsNullOrWhiteSpace(txtBookingID.Text) && int.TryParse(txtBookingID.Text, out bookingID))
            {
                LoadGrid($"Select * from Booking where B_id = {bookingID} and Cus_id = {Form1.LoginID}");
            }
            else
            {
                MessageBox.Show("Please enter a valid Booking ID.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            new Form2().Show();
        }

        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtReportTitle.Text) ||
                string.IsNullOrWhiteSpace(txtDescription.Text) ||
                string.IsNullOrWhiteSpace(txtBookingID.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection Con = new SqlConnection("Data Source=LAPTOP-J7JBFG4A\\SQLEXPRESS;Initial Catalog=CarBazar;Integrated Security=True"))
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into Report (Title,Description,Booking_id,submission_Date, Cus_ID) Values (@Title,@Description,@Booking_id,@submission_Date,@Cus_ID)", Con);
                    cmd.Parameters.AddWithValue("@Title", txtReportTitle.Text);
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                    cmd.Parameters.AddWithValue("@Booking_id", txtBookingID.Text);
                    cmd.Parameters.AddWithValue("@Cus_ID", Form1.LoginID);
                    cmd.Parameters.AddWithValue("@submission_Date", DateTime.Today);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Report submitted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
