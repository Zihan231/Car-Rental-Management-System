using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarBazar
{
    public partial class Form2 : Form
    {

        public Form2()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new CustomerProfile().Show();
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Confirm Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
                new Form1().Show();
            }
        }


        private void label10_Click(object sender, EventArgs e)
        {
            this.Close();
            new Form1().Show();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            this.Hide();
            new CustomerProfile().Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
            new EditCustomerDetails().Show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
            new EditCustomerDetails().Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
            new ResntCars().Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new ResntCars().Show();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.Hide();
            new ReportSubmission().Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Hide();
            new ReturnCar().Show();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Close();
            new Bookings().Show();
        }
    }
}
