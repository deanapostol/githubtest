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
            if (MessageBox.Show("are you sure you want to insert?", "insert record", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                sqlcon.Open();

                cmd = new SqlCommand("INSERT INTO [dbo].[PurchaseMain] ([PurchaseID],[PurchaseDate],[Vendorkey]) VALUES ('" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "')", sqlcon);
                cmd.ExecuteNonQuery();
                MessageBox.Show("successfull");
                sqlcon.Close();
                BindData();
            }
            else
            {
               BindData();
            }
            

          

        }
        void BindData()
        {
            SqlCommand command = new SqlCommand("select * from [dbo].[PurchaseMain]", sqlcon);
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


                    {  sqlcon.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM [dbo].[PurchaseMain] WHERE [Purchasekey] = '" + int.Parse(textBox1.Text) + "'", sqlcon);
                    cmd.ExecuteNonQuery();
                    sqlcon.Close();
                    MessageBox.Show("success");
                    BindData(); }
                    else {
                        BindData();
                    }
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

                List<string> conditions = new List<string>();

                
                if (!string.IsNullOrEmpty(textBox1.Text))
                {
                   
                    conditions.Add("[Purchasekey] = '" + int.Parse(textBox1.Text) + "'");
                }




                if (!string.IsNullOrEmpty(textBox2.Text))
                {
                    string searchText = textBox2.Text;

                    searchText = searchText.Replace("'", "''");
                    conditions.Add("[PurchaseID] LIKE '%" + searchText + "%'");
                }
              

                if (!string.IsNullOrEmpty(textBox3.Text))
                {
                    if (DateTime.TryParse(textBox3.Text, out DateTime selectedDate))
                    {
                        conditions.Add("[PurchaseDate] = '" + selectedDate.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                    }
                   
                }


                    if (!string.IsNullOrEmpty(textBox4.Text))
                    {
                        conditions.Add("[Vendorkey] = '" + int.Parse(textBox4.Text) + "'");
                    }


                    string whereClause = string.Join(" OR ", conditions);


                    SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[PurchaseMain] WHERE " + whereClause, sqlcon);


                    SqlDataAdapter sd = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    sd.Fill(dt);


                    dataGridView1.DataSource = dt;



                
            }
            catch (Exception ex)
            {
                MessageBox.Show("EROOR            " + ex);
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
                {
                    sqlcon.Open();
                    SqlCommand command = new SqlCommand("UPDATE [dbo].[PurchaseMain] SET [PurchaseID] = '" + textBox2.Text + "',[PurchaseDate] = '" + textBox3.Text + "', [Vendorkey] = '" + textBox4.Text + "' WHERE [Purchasekey] = " + int.Parse(textBox1.Text), sqlcon);
                    command.ExecuteNonQuery();
                    sqlcon.Close();
                    MessageBox.Show("success");
                    BindData();
                }
                else
                {
                    MessageBox.Show("error");
                }
            }
        }
    }
}
