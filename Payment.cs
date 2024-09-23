using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace CarBazar
{
    public partial class Payment : Form
    {
        private string paymentMethod;
        public Payment()
        {
            InitializeComponent();
            pictureBox2.Hide();
            pictureBox3.Hide();

            if (ResntCars.Nagad)
            {
                pictureBox2.Show();
                paymentMethod = "Nagad";
            }

            if (ResntCars.Bkash)
            {
                pictureBox3.Show();
                paymentMethod = "Bkash";
            }

            // Display total rent amount formatted as currency
            amount.Text = ResntCars.TotalRent.ToString("C");
        }

        public void PaymentStatus()
        {
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=LAPTOP-J7JBFG4A\\SQLEXPRESS;Initial Catalog=CarBazar;Integrated Security=True"))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE Booking SET Payment_Status = @Payment_Status WHERE B_id = @id", con))
                    {
                        cmd.Parameters.AddWithValue("@id", ResntCars.LatestBookingId);
                        cmd.Parameters.AddWithValue("@Payment_Status", "Done");
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Payment Status Updated To Done!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SetCarToOnRent()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=LAPTOP-J7JBFG4A\\SQLEXPRESS;Initial Catalog=CarBazar;Integrated Security=True"))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE Car SET status = @status, Frequency = Frequency + 1 WHERE id = @id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", ResntCars.CarID);
                        cmd.Parameters.AddWithValue("@status", "On Rent");
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Car status updated to 'On Rent'!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void UpdateBookingWithPaymentId()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=LAPTOP-J7JBFG4A\\SQLEXPRESS;Initial Catalog=CarBazar;Integrated Security=True"))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT PaymentId FROM Payment WHERE BookingId = @BookingId", conn))
                    {
                        cmd.Parameters.AddWithValue("@BookingId", ResntCars.LatestBookingId);
                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            int paymentId = Convert.ToInt32(result);
                            using (SqlCommand updateCmd = new SqlCommand("UPDATE Booking SET Payment_id = @PaymentId WHERE B_id = @BookingId", conn))
                            {
                                updateCmd.Parameters.AddWithValue("@PaymentId", paymentId);
                                updateCmd.Parameters.AddWithValue("@BookingId", ResntCars.LatestBookingId);
                                updateCmd.ExecuteNonQuery();
                            }

                            MessageBox.Show("Booking updated with Payment ID successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No payment found for this booking ID.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
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
            new ResntCars().Show();
        }
        
        
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=LAPTOP-J7JBFG4A\\SQLEXPRESS;Initial Catalog=CarBazar;Integrated Security=True"))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Payment (BookingId, Amount, PaymentDate, PaymentMethod, Payment_phn) VALUES (@BookingId, @Amount, @PaymentDate, @PaymentMethod, @Payment_phn); SELECT SCOPE_IDENTITY();", conn))
                    {
                        cmd.Parameters.AddWithValue("@BookingId", ResntCars.LatestBookingId);
                        cmd.Parameters.AddWithValue("@Amount", ResntCars.TotalRent);
                        cmd.Parameters.AddWithValue("@PaymentDate", DateTime.Today);
                        cmd.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
                        cmd.Parameters.AddWithValue("@Payment_phn", int.Parse(txtBoxPhn.Text));

                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            int paymentId = Convert.ToInt32(result);
                            MessageBox.Show($"Payment via {paymentMethod} was successfully saved with Payment ID: {paymentId}!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            PaymentStatus();
                            SetCarToOnRent();
                            UpdateBookingWithPaymentId();
                            ConfirmBtn.Hide();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while processing {paymentMethod} payment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void Payment_Load(object sender, EventArgs e)
        {
        }

    }
}
