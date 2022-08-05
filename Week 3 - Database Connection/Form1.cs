using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Week_3___Database_Connection
{
    public partial class Form1 : Form
    {

        static SqlConnection sqlConnection = new SqlConnection(@"Data Source=LAAM;Initial Catalog=ESEMKA_MART;Integrated Security=True");

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadDgv();
            loadCbRole();
        }

        private void loadCbRole()
        {
            cbRole.Items.Clear();

            openConection();

            string query = "SELECT * FROM Role";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConnection);

            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            cbRole.DataSource = dt;
            cbRole.ValueMember = "Id";
            cbRole.DisplayMember = "Name";
        }

        private void loadDgv()
        {
            dgv.Rows.Clear();

            openConection();

            String query = "SELECT * FROM Customer";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConnection);

            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                dgv.Rows.Add(dr["Id"], dr["Name"], dr["Email"], dr["PhoneNumber"]);
            }
        }

        private void openConection()
        {
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                openConection();

                SqlCommand cmd = new SqlCommand($"INSERT INTO Customer VALUES ('{txtID.Text}', '{txtName.Text}', '{txtEmal.Text}', '{txtPhoneNumber.Text}')", sqlConnection);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Insert Success!");

                loadDgv();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                openConection();

                SqlCommand cmd = new SqlCommand(
                    $"UPDATE Customer SET Name = '{txtName.Text}', Email = '{txtEmal.Text}', PhoneNumber = '{txtPhoneNumber.Text}' WHERE Id = '{txtID.Text}'",
                    sqlConnection
                );
                cmd.ExecuteNonQuery();
                MessageBox.Show("Update Success!");

                loadDgv();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                openConection();

                SqlCommand cmd = new SqlCommand($"DELETE Customer WHERE Id = '{txtID.Text}'", sqlConnection);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Delete Success!");

                loadDgv();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

