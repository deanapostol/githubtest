using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace WindowsFormsApp1.NewFolder1
{
    class Class1
    {
       public SqlConnection getConnection()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=DESKTOP-7T8F2Q7\\DEANAPOSTOL;Initial Catalog=working;Integrated Security=True";
            return con;
        }
    }
}
