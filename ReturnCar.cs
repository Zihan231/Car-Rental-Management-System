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
    public partial class ReturnCar : Form
    {
        public ReturnCar()
        {
            InitializeComponent();
            LoadGrid($"SELECT b.B_id AS BookingId, c.name AS CarName, c.id AS CarId, cus.name AS CustomerName, cus.id AS CustomerId, b.Start_time AS StartDate, b.End_time AS EndDate, b.total_Rent AS Rent, p.PaymentId AS PaymentId FROM Booking b INNER JOIN Car c ON b.Car_id = c.id INNER JOIN Customer cus ON b.Cus_id = cus.id INNER JOIN Payment p ON b.Payment_id = p.PaymentId WHERE c.status = 'ON Rent' AND b.Cus_id = {Form1.LoginID};");
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

        private void ReturnCar_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            new Form2().Show();
        }

        private void FindBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCarID.Text) || !int.TryParse(txtCarID.Text, out _))
            {
                MessageBox.Show("Please enter a valid Car ID (integer).", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LoadGrid($"SELECT b.B_id AS BookingId, c.name AS CarName, c.id AS CarId, cus.name AS CustomerName, cus.id AS CustomerId, b.Start_time AS StartDate, b.End_time AS EndDate, b.total_Rent AS Rent, p.PaymentId AS PaymentId FROM Booking b INNER JOIN Car c ON b.Car_id = c.id INNER JOIN Customer cus ON b.Cus_id = cus.id INNER JOIN Payment p ON b.Payment_id = p.PaymentId WHERE c.status = 'ON Rent' AND c.id = {txtCarID.Text} AND b.Cus_id = {Form1.LoginID};");
        }



        public void SetCarToFree(string carId)
        {
            if (string.IsNullOrWhiteSpace(carId) || !int.TryParse(carId, out _))
            {
                MessageBox.Show("Please enter a valid Car ID (integer).", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=LAPTOP-J7JBFG4A\\SQLEXPRESS;Initial Catalog=CarBazar;Integrated Security=True"))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE Car SET status = 'Free' WHERE id = @CarId", conn))
                    {
                        cmd.Parameters.AddWithValue("@CarId", carId);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Car Returned Successfully !!!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No car found with the specified ID.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void button2_Click(object sender, EventArgs e)
        {
            SetCarToFree(txtCarID.Text);
;        }
    }
}
