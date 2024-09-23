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
    public partial class Bookings : Form
    {
        private string PaymentMethodFromBookings;
        private int Carid = 0;
        private int totalRent = 0;

        public Bookings()
        {
            InitializeComponent();
            PaymentPanel.Hide();
            LoadGridView($"Select B_id As [Booking Id],Start_time As [Start Time], End_time As [Ending Time], Car_id As [Car ID],total_Rent AS [Total Rent], Payment_Status AS [Payment Status] from Booking where Cus_id = {Form1.LoginID} AND Payment_Status = 'Pending' ;");
            PaymentPanel.Hide();
        }

        public void LoadGridView(string Q)
        {
            SqlConnection con = new SqlConnection("Data Source=LAPTOP-J7JBFG4A\\SQLEXPRESS;Initial Catalog=CarBazar;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand(Q, con);
            DataTable dt = new DataTable();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            dataGridView1.DataSource = dt.DefaultView;
        }

        private bool ValidatePaymentInputs()
        {
            if (string.IsNullOrWhiteSpace(txtBoxBookingID.Text))
            {
                MessageBox.Show("Please enter your Booking ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!radioButton1.Checked && !radioButton2.Checked)
            {
                MessageBox.Show("Please select a payment method.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }


        private void Bookings_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBoxBookingID.Text))
            {
                MessageBox.Show("Please enter your Booking ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtBoxBookingID.Text, out int bookingID))
            {
                MessageBox.Show("Booking ID must be a valid integer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string query = $"SELECT B_id AS [Booking Id], Start_time AS [Start Time], End_time AS [Ending Time], Car_id AS [Car ID], total_Rent AS [Total Rent], Payment_Status AS [Payment Status] FROM Booking WHERE Cus_id = {Form1.LoginID} AND Payment_Status = 'Pending' AND B_id = {bookingID};";

                LoadGridView(query);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            new Form2().Show();
        }

        private static bool Bkash = false;
        private static bool Nagad = false;


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            PaymentMethodFromBookings = "Nagad";
            radioButton1.Checked = true;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            PaymentMethodFromBookings = "Bkash";
            radioButton2.Checked = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            PaymentMethodFromBookings = "Nagad";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            PaymentMethodFromBookings = "Bkash";
        }
        public void GetCarID()
        {
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=LAPTOP-J7JBFG4A\\SQLEXPRESS;Initial Catalog=CarBazar;Integrated Security=True"))
                {
                    con.Open();
                    SqlCommand command = new SqlCommand("Select  Car_id from Booking where B_id = @B_id and Cus_id = @Cus_id", con);
                    command.Parameters.AddWithValue("@B_id", int.Parse(txtBoxBookingID.Text));
                    command.Parameters.AddWithValue("@Cus_id", Form1.LoginID);
                    
                    var result = command.ExecuteScalar();
                    if (result != null)
                    {
                        Carid = Convert.ToInt32(result);
                        

                        MessageBox.Show($"{Carid}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=LAPTOP-J7JBFG4A\\SQLEXPRESS;Initial Catalog=CarBazar;Integrated Security=True"))
                {
                    con.Open();
                    SqlCommand command = new SqlCommand("Select total_Rent, Car_id from Booking where B_id = @B_id and Cus_id = @Cus_id", con);
                    command.Parameters.AddWithValue("@B_id", int.Parse(txtBoxBookingID.Text));
                    command.Parameters.AddWithValue("@Cus_id", Form1.LoginID);
                    
                    var result = command.ExecuteScalar();
                    if (result != null)
                    {
                        totalRent = Convert.ToInt32(result);
                        amount.Text = totalRent.ToString("C");
                        GetCarID();                        
                        SelectPanel.Hide();
                        PaymentPanel.Show();

                        // MessageBox.Show($"{totalRent}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            PaymentPanel.Hide();
            SelectPanel.Show();
            LoadGridView($"Select B_id As [Booking Id],Start_time As [Start Time], End_time As [Ending Time], Car_id As [Car ID],total_Rent AS [Total Rent], Payment_Status AS [Payment Status] from Booking where Cus_id = {Form1.LoginID} AND Payment_Status = 'Pending' ;");

        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(txtBoxPhn.Text))
            {
                MessageBox.Show("Please enter your phone number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!radioButton1.Checked && !radioButton2.Checked)
            {
                MessageBox.Show("Please select a payment method.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            
            PaymentMethodFromBookings = radioButton1.Checked ? "Nagad" : "Bkash";

            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=LAPTOP-J7JBFG4A\\SQLEXPRESS;Initial Catalog=CarBazar;Integrated Security=True"))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Payment (BookingId, Amount, PaymentDate, PaymentMethod, Payment_phn) VALUES (@BookingId, @Amount, @PaymentDate, @PaymentMethod, @Payment_phn); SELECT SCOPE_IDENTITY();", conn))
                    {
                        cmd.Parameters.AddWithValue("@BookingId", int.Parse(txtBoxBookingID.Text));
                        cmd.Parameters.AddWithValue("@Amount", totalRent);
                        cmd.Parameters.AddWithValue("@PaymentDate", DateTime.Today);
                        cmd.Parameters.AddWithValue("@PaymentMethod", PaymentMethodFromBookings);
                        cmd.Parameters.AddWithValue("@Payment_phn", int.Parse(txtBoxPhn.Text));

                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            int paymentId = Convert.ToInt32(result);
                            MessageBox.Show($"Payment via {PaymentMethodFromBookings} was successfully saved with Payment ID: {paymentId}!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            PaymentStatus();
                            SetCarToOnRent();
                            UpdateBookingWithPaymentId();
                            SelectPanel.Show();
                            PaymentPanel.Hide();
                            LoadGridView($"Select B_id As [Booking Id], Start_time As [Start Time], End_time As [Ending Time], Car_id As [Car ID], total_Rent AS [Total Rent], Payment_Status AS [Payment Status] from Booking where Cus_id = {Form1.LoginID} AND Payment_Status = 'Pending';");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while processing {PaymentMethodFromBookings} payment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                        cmd.Parameters.AddWithValue("@id", int.Parse(txtBoxBookingID.Text));
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
                        cmd.Parameters.AddWithValue("@id", Carid);
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
                        cmd.Parameters.AddWithValue("@BookingId", int.Parse(txtBoxBookingID.Text));
                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            int paymentId = Convert.ToInt32(result);
                            using (SqlCommand updateCmd = new SqlCommand("UPDATE Booking SET Payment_id = @PaymentId WHERE B_id = @BookingId", conn))
                            {
                                updateCmd.Parameters.AddWithValue("@PaymentId", paymentId);
                                updateCmd.Parameters.AddWithValue("@BookingId", int.Parse(txtBoxBookingID.Text));
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

    }



}

