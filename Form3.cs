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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CarBazar
{
    public partial class Form3 : Form
    {
    


        public Form3()
        {
            InitializeComponent();
            //dataGridView1.Hide();
            
        }
     





        private void label2_Click(object sender, EventArgs e)
        {

        }



        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void addAdminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            

        }

        private void addCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void addCarsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void showAlllCarsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void adminDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void customerDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void customerDetailsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            
        }

        private void adminDetailsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            
        }

        private void carDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void addCustomerToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //dataGridView1.Hide();
            new AddCustomercs().Show();
        }

        private void addAdminToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //dataGridView1.Hide();
            this.Hide();
            new AddAdmincs().Show();
        }

        private void addCarsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            
        }

        private void showAlllCarsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //dataGridView1.Show();
            SqlConnection con = new SqlConnection("Data Source=LAPTOP-J7JBFG4A\\SQLEXPRESS;Initial Catalog=CarBazar;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Car", con);
            DataTable dt = new DataTable();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            //dataGridView1.DataSource = dt.DefaultView;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new AdminProfile().Show();
        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new AddAdmincs().Show();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Form5().Show();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
         
            this.Hide();
            new AddCustomercs().Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Hide();
            new AddCars().Show();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Form6().Show();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Form7().Show();
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            // Show a confirmation message box for logout
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Check the user's response
            if (result == DialogResult.Yes)
            {
                this.Close(); 
                new Form1().Show(); 
            }
            // Do nothing if user selects "No"
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox10_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            new AdminDashBoard().Show();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Hide();
            new FeedBack().Show();  
        }
    }
}
