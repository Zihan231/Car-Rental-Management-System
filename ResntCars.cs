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

    
    public partial class ResntCars : Form
    {

        public static int CarID;   // for passing the id to next window
        //public static double rent; // for passing the id to next window


        private string PaymentMethod;
        public static DateTime selectedDate;
        public static double TotalRent; // Variable to store the total rent


        public ResntCars()
        {
            InitializeComponent();
            PaymentPanel.Hide();

            dateTimePicker1.MinDate = DateTime.Today; //To select the date from today

            dateTimePicker1.ValueChanged += new EventHandler(DateChanged); // Add the ValueChanged event handler for date changes
            LoadGridView();

        }

        public void LoadGridView()
        {
            SqlConnection con = new SqlConnection("Data Source=LAPTOP-J7JBFG4A\\SQLEXPRESS;Initial Catalog=CarBazar;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Car where status=@status", con);
            cmd.Parameters.AddWithValue("@status", "Free");
            DataTable dt = new DataTable();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            dataGridView1.DataSource = dt.DefaultView;
        }
        private void DateChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtBoxID_Car.Text) && int.TryParse(txtBoxID_Car.Text, out int carId))
            {
                // Calculate the number of rental days based on the newly selected date
                int rentalDays = (dateTimePicker1.Value.Date - DateTime.Today).Days + 1; // Ensure today is included

                // Recalculate the total rent when the date changes
                CalculateTotalRent(carId, rentalDays);
            }
            else
            {
                label6.Text = "Select a valid car to calculate rent.";
            }
        }


        private void CalculateTotalRent(int carId, int rentalDays)
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=LAPTOP-J7JBFG4A\\SQLEXPRESS;Initial Catalog=CarBazar;Integrated Security=True");
                con.Open();

                // Retrieve the rent per day for the selected car
                SqlCommand cmd = new SqlCommand("SELECT Rent FROM Car WHERE id=@id", con);
                cmd.Parameters.AddWithValue("@id", carId);
                var result = cmd.ExecuteScalar();
                con.Close();

                if (result != null)
                {
                    double rentPerDay = Convert.ToDouble(result); // Assuming Rent is a numeric field in the database
                    TotalRent = rentPerDay * rentalDays; // Reset and calculate total rent based on the current selection

                    // Display total rent in label6
                    label6.Text = $"{TotalRent:C}"; // Display as currency (C)
                    
                }
                else
                {
                    MessageBox.Show("No car found with the provided ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public bool IsValidity()
        {
            if (txtBoxID_Car.Text == string.Empty)
            {
                MessageBox.Show("Car ID cannot be empty.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (PaymentMethod == null)
            {
                MessageBox.Show("Please select a payment method.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ResntCars_Load(object sender, EventArgs e)
        {
           
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            new Form2().Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.PaymentPanel.Hide();
            this.ConfirmPanal.Show();

            if (!string.IsNullOrWhiteSpace(txtBoxID_Car.Text)) // Check if the ID textbox is NOT empty
            {
                if (int.TryParse(txtBoxID_Car.Text, out int carId))
                {
                    try
                    {
                        using (SqlConnection con = new SqlConnection("Data Source=LAPTOP-J7JBFG4A\\SQLEXPRESS;Initial Catalog=CarBazar;Integrated Security=True"))
                        {
                            con.Open();
                            SqlCommand cmd = new SqlCommand("select * from Car where id=@id and status=@status", con);
                            cmd.Parameters.AddWithValue("@id", carId);
                            cmd.Parameters.AddWithValue("@status", "Free");

                            DataTable dt = new DataTable();
                            SqlDataReader sdr = cmd.ExecuteReader();
                            dt.Load(sdr);

                            // Check if the DataTable is empty
                            if (dt.Rows.Count == 0)
                            {
                                MessageBox.Show("No car found with the provided ID.", "No Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                // Reset the previous rent when a new car is selected
                                TotalRent = 0;

                                // Bind the result to the DataGridView
                                dataGridView1.DataSource = dt.DefaultView;

                                // Store the current selected car ID

                                // Recalculate the rent for the new car with the selected date
                                int rentalDays = (dateTimePicker1.Value.Date - DateTime.Today).Days + 1; // Ensure today is included
                                CalculateTotalRent(carId, rentalDays);
                            }

                            con.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message); // Error handling
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid numeric ID.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid ID. The ID field cannot be left empty.", "ID Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }




        public static bool Nagad = false;
        public static bool Bkash = false;


        private void button2_Click(object sender, EventArgs e)
        {
            if (IsValidity() && (radioButton1.Checked == true || radioButton2.Checked == true))
            {   
                CarID = int.Parse(txtBoxID_Car.Text);

                if (PaymentMethod == "Nagad")
                {
                    Nagad = true;

                    this.Close();
                    new Payment().Show();
                }
                else if (PaymentMethod == "Bkash")
                {
                    Bkash = true;

                    this.Close();
                    new Payment().Show();
                }
            }
            else
            {
                MessageBox.Show("Please select a Payment Method before confirming the booking.", "No Car Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {
            PaymentMethod = "Nagad";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            PaymentMethod = "Bkash";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            PaymentMethod = "Nagad";
            radioButton1.Checked= true;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            PaymentMethod = "Bkash";
            radioButton2.Checked = true;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }


        public static int LatestBookingId { get; private set; }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtBoxID_Car.Text == string.Empty)
            {
                MessageBox.Show("Please select a car before confirming the booking.", "No Car Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int carId = int.Parse(txtBoxID_Car.Text);
                DateTime selectedEndDate = dateTimePicker1.Value.Date;
                int rentalDays = (selectedEndDate - DateTime.Today).Days + 1;

                CalculateTotalRent(carId, rentalDays);

                using (SqlConnection con = new SqlConnection("Data Source=LAPTOP-J7JBFG4A\\SQLEXPRESS;Initial Catalog=CarBazar;Integrated Security=True"))
                {
                    con.Open();

                    // Insert booking and retrieve the new Booking ID
                    SqlCommand cmd = new SqlCommand("INSERT INTO Booking (Cus_id, Car_id, Start_time, End_time, total_Rent, Payment_Status) " +
                                                    "VALUES (@Cus_id, @Car_id, @Start_time, @end_time, @total_Rent, @Payment_Status); SELECT SCOPE_IDENTITY();", con);

                    cmd.Parameters.AddWithValue("@Cus_id", Form1.LoginID);
                    cmd.Parameters.AddWithValue("@Car_id", carId);
                    cmd.Parameters.AddWithValue("@Start_time", DateTime.Today);
                    cmd.Parameters.AddWithValue("@end_time", selectedEndDate);
                    cmd.Parameters.AddWithValue("@total_Rent", TotalRent);
                    cmd.Parameters.AddWithValue("@Payment_Status", "Pending");

                    // Use ExecuteScalar to get the latest Booking ID (SCOPE_IDENTITY)
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        LatestBookingId = Convert.ToInt32(result); // Store the latest Booking ID
                        MessageBox.Show($"Booking ID: {LatestBookingId}");
                    }

                    // Remove the second ExecuteNonQuery call to avoid duplicate inserts
                    // Now handle updating car status after booking
                    try
                    {
                        using (SqlCommand updateStatusCmd = new SqlCommand("UPDATE Car SET Status = @Status WHERE id = @Car_id", con))
                        {
                            updateStatusCmd.Parameters.AddWithValue("@Status", "Booked");
                            updateStatusCmd.Parameters.AddWithValue("@Car_id", carId);

                            int statusUpdated = updateStatusCmd.ExecuteNonQuery();

                            if (statusUpdated > 0)
                            {
                                MessageBox.Show("Booking confirmed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadGridView();
                                this.ConfirmPanal.Hide();
                                this.PaymentPanel.Show();
                            }
                            else
                            {
                                MessageBox.Show("Failed to update car status. Please check the car ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred while updating car status: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }





        // Dummy function for customer ID (replace with actual logic)
        private int GetCurrentCustomerId()
        {
            
            return Form1.LoginID; // Replace with actual customer ID retrieval logic
        }

        private void ShowCars_Click(object sender, EventArgs e)
        {
            LoadGridView();
        }
    }
}
