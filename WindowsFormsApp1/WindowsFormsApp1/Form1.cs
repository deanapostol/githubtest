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
using WindowsFormsApp1.NewFolder1;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Class1 contest = new Class1();
        SqlConnection sqlcon = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
       



        public Form1()
        {
            InitializeComponent();
            sqlcon = contest.getConnection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if  (MessageBox.Show("are you sure you want to insert?", "insert record", MessageBoxButtons.YesNo) == DialogResult.Yes)
                sqlcon.Open();

                cmd = new SqlCommand("INSERT INTO [dbo].[1234] ([1],[2],[3],[4]) VALUES ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "')", sqlcon);
                cmd.ExecuteNonQuery();
                MessageBox.Show("successfull");
                sqlcon.Close();
                BindData(); 
            

          

        }
        void BindData()
        {
            SqlCommand command = new SqlCommand("select * from [dbo].[1234]",sqlcon);
            SqlDataAdapter sd = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            BindData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            {
                if (textBox1.Text != "")
                {
                    if (MessageBox.Show("are you sure you want to delete?", "delete record", MessageBoxButtons.YesNo) == DialogResult.Yes)


                        sqlcon.Open();
                    SqlCommand cmd1 = new SqlCommand("DELETE FROM [dbo].[1234] WHERE [1] = '" + int.Parse(textBox1.Text) + "'", sqlcon);
                    cmd1.ExecuteNonQuery();
                    sqlcon.Close();
                    MessageBox.Show("success");
                    BindData();
                }
                else
                {
                    MessageBox.Show("not possible");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // Create a list to store the conditions for each column
                List<string> conditions = new List<string>();

                // Check if textBox1.Text contains a value, and if so, add it to the conditions
                if (!string.IsNullOrEmpty(textBox1.Text))
                {
                    conditions.Add("[1] = '" + int.Parse(textBox1.Text) + "'");
                }

                // Check if textBox2.Text contains a value, and if so, add it to the conditions
                if (!string.IsNullOrEmpty(textBox2.Text))
                {
                    conditions.Add("[2] = '" + int.Parse(textBox2.Text) + "'");
                }

                // Check if textBox3.Text contains a value, and if so, add it to the conditions
                if (!string.IsNullOrEmpty(textBox3.Text))
                {
                    conditions.Add("[3] = '" + int.Parse(textBox3.Text) + "'");
                }

                // Check if textBox4.Text contains a value, and if so, add it to the conditions
                if (!string.IsNullOrEmpty(textBox4.Text))
                {
                    conditions.Add("[4] = '" + int.Parse(textBox4.Text) + "'");
                }

                // Construct the WHERE clause with the conditions
                string whereClause = string.Join(" OR ", conditions);

                // Create the SQL command with the dynamic WHERE clause
                SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[1234] WHERE " + whereClause, sqlcon);

                // Create a data adapter and fill a data table
                SqlDataAdapter sd = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                sd.Fill(dt);

                // Set the DataGridView's data source
                dataGridView1.DataSource = dt;



            }
            catch (Exception ex)
            {
                MessageBox.Show("This doesnt exist in the table" + ex);
            }
            finally
            {
                sqlcon.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (MessageBox.Show("are you sure you want to update?", "update record", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    sqlcon.Open();
                SqlCommand command = new SqlCommand("UPDATE [dbo].[1234] SET [2] = '" + textBox2.Text + "', [3] = '" + textBox3.Text + "', [4] = '" + textBox4.Text + "' WHERE [1] = " + int.Parse(textBox1.Text), sqlcon);
                command.ExecuteNonQuery();
                sqlcon.Close();
                MessageBox.Show("success");
                BindData();
            }
        }
    }
}
