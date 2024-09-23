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
using System.Xml.Linq;

namespace CarBazar
{
    public partial class AdminDashBoard : Form
    {
        
        SqlConnection Con = new SqlConnection("Data Source=LAPTOP-J7JBFG4A\\SQLEXPRESS;Initial Catalog=CarBazar;Integrated Security=True");

        public AdminDashBoard()
        {
            InitializeComponent();

            loadTotalCus();
            loadTotalCar();
            loadTotalAdmin();
            loadNewCustomer();
            loadTotalRevenue();         
            loadTotalExpense();
            loadPendingPayment();



            //Hiding the Panels

            Performance.Hide();


        }

        public void LoadGrid(string Query)
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            Con.Close();
            dataGridView1.DataSource = dt.DefaultView;
        }

            public void loadTotalCus()
        {
            try
            {
                Con.Open();  // Open the database connection
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Customer", Con);
                int rowCount = (int)cmd.ExecuteScalar();
                totalCustomers.Text = rowCount.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);  // Display the error message
            }
            finally
            {
                if (Con.State == ConnectionState.Open)
                {
                    Con.Close();  // Ensure the connection is closed in the 'finally' block
                }
            }
        }
        public void loadTotalCar()
        {
            try
            {
                Con.Open();  
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Car", Con);
                int rowCount = (int)cmd.ExecuteScalar();
                totalCar.Text = rowCount.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);  
            }
            finally
            {
                if (Con.State == ConnectionState.Open)
                {
                    Con.Close();  
                }
            }
        }
        public void loadTotalAdmin()
        {
            try
            {
                Con.Open();  
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Admin", Con);
                int rowCount = (int)cmd.ExecuteScalar();
                totalAdmins.Text = rowCount.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);  
            }
            finally
            {
                if (Con.State == ConnectionState.Open)
                {
                    Con.Close();  
                }
            }
        }
        public void loadTotalRevenue()
        {
            try
            {
                Con.Open();  // Open the database connection
                SqlCommand cmd = new SqlCommand("SELECT SUM(total_Rent) FROM Booking WHERE Payment_Status = 'Done'", Con);

                object result = cmd.ExecuteScalar();  

                if (result != DBNull.Value && result != null)
                {
                    int totalRevenueValue = Convert.ToInt32(result);  
                    totalRevenue.Text = totalRevenueValue.ToString("C0");  
                }
                else
                {
                    totalRevenue.Text = "0.00";  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);  
            }
            finally
            {
                if (Con.State == ConnectionState.Open)
                {
                    Con.Close();  
                }
            }
        }
        

        public void loadPendingPayment()
        {
            try
            {
                Con.Open();  // Open the database connection
                SqlCommand cmd = new SqlCommand("SELECT SUM(total_Rent) FROM Booking WHERE Payment_Status = 'Pending' ", Con);

                object result = cmd.ExecuteScalar();  

                if (result != DBNull.Value && result != null)
                {
                    int totalPrndinPayment = Convert.ToInt32(result);
                    PendingPayment.Text = totalPrndinPayment.ToString("C0");  
                }
                else
                {
                    totalRevenue.Text = "0.00";  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);  
            }
            finally
            {
                if (Con.State == ConnectionState.Open)
                {
                    Con.Close();  
                }
            }

        }
        public void loadTotalExpense()
        {
            try
            {
                Con.Open();  // Open the database connection
                SqlCommand cmd = new SqlCommand("SELECT SUM(Salary) FROM Admin ", Con);

                object result = cmd.ExecuteScalar();  

                if (result != DBNull.Value && result != null)
                {
                    int totalAdminSal = Convert.ToInt32(result);
                    totalExpense.Text = totalAdminSal.ToString("C0");  
                }
                else
                {
                    totalRevenue.Text = "0.00";  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);  
            }
            finally
            {
                if (Con.State == ConnectionState.Open)
                {
                    Con.Close();  
                }
            }
        }

        public void loadNewCustomer()
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("SELECT Count(*) FROM Customer WHERE Join_Date >= DATEADD(DAY, -3, GETDATE())", Con);
                int totalNewCustomer = (int)cmd.ExecuteScalar();
                newCustomer.Text = totalNewCustomer.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                if (Con.State == ConnectionState.Open)
                {
                    Con.Close();
                }
            }

        }
        private int MostRentedCar;
        private int LeastRentedCar;

        public void loadMostRented()
        {
            try
            {
                Con.Open();
                string query = "SELECT id, name FROM Car WHERE Frequency = (SELECT MAX(Frequency) FROM Car);";
                SqlCommand cmd = new SqlCommand(query, Con);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    
                    MostRentedCarID.Text = reader[0].ToString();  // Car ID
                    MostRentedCarName.Text = reader[1].ToString();  // Car Name
                    MostRentedCar = int.Parse(MostRentedCarID.Text);
                }
                else
                {
                    MostRentedCarID.Text = "N/A";
                    MostRentedCarName.Text = "N/A";
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                if (Con.State == ConnectionState.Open)
                {
                    Con.Close();
                }
            }
        }
        public void loadLeastRented()
        {
            try
            {
                Con.Open();
                string query = "SELECT id, name FROM Car WHERE Frequency = (SELECT Min(Frequency) FROM Car);";
                SqlCommand cmd = new SqlCommand(query, Con);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    
                    LeastRentedCarID.Text = reader[0].ToString();  // Car ID
                    LeastRentedCarName.Text = reader[1].ToString();  // Car Name

                    LeastRentedCar = int.Parse(LeastRentedCarID.Text);
                }
                else
                {
                    LeastRentedCarID.Text = "N/A";
                    LeastRentedCarName.Text = "N/A";
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                if (Con.State == ConnectionState.Open)
                {
                    Con.Close();
                }
            }
        }

        private int MostFrequentCusIDValue;
        private int LeastFrequentCusIDValue;

        public void loadMostFrequentRenter()
        {
            try
            {
                Con.Open();
                string query = "SELECT id, name FROM Customer WHERE Renting_Frequency = (SELECT MAX(Renting_Frequency) FROM Customer);";
                SqlCommand cmd = new SqlCommand(query, Con);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                     MostFrequentCusIDValue = reader.GetInt32(0);
                    string MostFrequentCusNameValue = reader.GetString(1);

                    MostFrequentCusID.Text = MostFrequentCusIDValue.ToString();
                    MostFrequentCusName.Text = MostFrequentCusNameValue;
                }
                else
                {
                    MostFrequentCusID.Text = "N/A";
                    MostFrequentCusName.Text = "N/A";
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                if (Con.State == ConnectionState.Open)
                {
                    Con.Close();
                }
            }
        }
        public void loadLeastFrequentRenter()
        {
            try
            {
                Con.Open();
                string query = "SELECT id, name FROM Customer WHERE Renting_Frequency = (SELECT MIN(Renting_Frequency) FROM Customer);";
                SqlCommand cmd = new SqlCommand(query, Con);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                     LeastFrequentCusIDValue = reader.GetInt32(0);
                    string LeastFrequentCusNameValue = reader.GetString(1);

                    LeastFrequentCusID.Text = LeastFrequentCusIDValue.ToString();
                    LeastFrequentCusName.Text = LeastFrequentCusNameValue;
                }
                else
                {
                    MostFrequentCusID.Text = "N/A";
                    MostFrequentCusName.Text = "N/A";
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                if (Con.State == ConnectionState.Open)
                {
                    Con.Close();
                }
            }
        }



        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void amount_Click(object sender, EventArgs e)
        {

        }

        private void AdminDashBoard_Load(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            LoadGrid("Select  B_id AS BookingID, Cus_id AS CustomerID, Payment_id AS PaymentID,Car_id AS CarID,total_Rent AS [Total Rent] from Booking Where Payment_Status = 'Done' ");
        }

        private void label10_Click(object sender, EventArgs e)
        {
            LoadGrid("Select * from Admin");
        }

        private void label12_Click(object sender, EventArgs e)
        {
            LoadGrid("Select id As ID, name As Name, salary As Salary from Admin");
        }

        private void label11_Click(object sender, EventArgs e)
        {
            LoadGrid("Select B_id AS BookingID, Cus_id AS CustomerID, Payment_id AS PaymentID,Car_id AS CarID,total_Rent AS [Total Rent], Payment_Status AS [Payment Status] from Booking Where Payment_Status = 'Pending' ");
        }

        private void label9_Click(object sender, EventArgs e)
        {
            LoadGrid("Select id AS [Car ID], name As Name, rent As Rent, regNum As [Reg Number], model As [Car Model], company As Company from Car");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            new Form3().Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Performance.Hide();
            PanelTotalCalculation.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadGrid("Select id AS [Car ID], name As Name, rent As Rent, regNum As [Reg Number], model As [Car Model], status As [Car Status] from Car Where status = 'Booked' ");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadGrid("Select id AS [Car ID], name As Name, rent As Rent, regNum As [Reg Number], model As [Car Model], status As [Car Status] from Car Where status = 'On Rent' ");
        }

        private void PerformanceBtn_Click(object sender, EventArgs e)
        {
            Performance.Show();
            //Calling the Functions

            loadMostRented();
            loadLeastRented();
            loadMostFrequentRenter();
            loadLeastFrequentRenter();
        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MostFrequentCusName_Click(object sender, EventArgs e)
        {
            
;        }

        private void label21_Click(object sender, EventArgs e)
        {
            string Query = $"Select id AS ID, name AS Name,address As Address, phone As Phone,Join_Date AS [Joining Date], Renting_Frequency AS [Renting Frequency] from Customer where id = {MostFrequentCusIDValue} ";
            LoadGrid(Query);
        }

        private void MostFrequentCusID_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void label34_Click(object sender, EventArgs e)
        {

        }

        private void label31_Click(object sender, EventArgs e)
        {
            string Query = $"Select id AS ID, name AS Name,address As Address, phone As Phone,Join_Date AS [Joining Date], Renting_Frequency AS [Renting Frequency] from Customer where id = {LeastFrequentCusIDValue} ";
            LoadGrid(Query);
        }

        private void label16_Click(object sender, EventArgs e)
        {
            string Query = $"Select id As ID,name AS Name, model AS Model,rent AS Rent,regNum AS [Reg Num], Frequency from Car where id = {MostRentedCar}";
            LoadGrid(Query);
        }

        private void label24_Click(object sender, EventArgs e)
        {
            string Query = $"Select id As ID,name AS Name, model AS Model,rent AS Rent,regNum AS [Reg Num], Frequency from Car where id = {LeastRentedCar}";
            LoadGrid(Query);
        }

        private void button5_Click(object sender, EventArgs e)
        {
           
           
        }
    }
}
