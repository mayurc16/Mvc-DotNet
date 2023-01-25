using Microsoft.Data.SqlClient;
using System.Data;

namespace Model_Binding.Models
{
    public class Department
    {
        public int DeptNo { get; set; }
        public string DeptName { get; set; }

        public static List<Department> GetAllDepartment()
        {
            List<Department> list = new List<Department>();
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=JkJan23;Integrated Security=True;";
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Departments";

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Department dept = new Department { DeptNo = Convert.ToInt32(dr[0]), DeptName = Convert.ToString(dr[1]) };
                    list.Add(dept);
                }
                dr.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { cn.Close(); }
            return list;
        }
    }
}
