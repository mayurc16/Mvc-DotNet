using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection.Metadata.Ecma335;

namespace Model_Binding.Models
{
    public class Employee
    {
        public int EmpNo { get; set; }
        public string Name { get; set; }
        public decimal Basic { get; set; }
        public int DeptNo { get; set; }

        public static List<Employee> GetAllEmployee()
        {
            List<Employee> list = new List<Employee>();
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=JkJan23;Integrated Security=True;";
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Employees";

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Employee emp = new Employee
                    {
                        EmpNo = Convert.ToInt32(dr[0]),
                        Name = Convert.ToString(dr[1]),
                        Basic = Convert.ToDecimal(dr[2]),
                        DeptNo = Convert.ToInt32(dr[3]),
                    };
                    list.Add(emp);
                }
                dr.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { cn.Close(); }
            return list;
        }
        public static Employee GetEmployee(int id) 
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=JkJan23;Integrated Security=True;";
            Employee emp = null;
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Employees where EmpNo=@id";
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    emp = new Employee
                    {
                        EmpNo = Convert.ToInt32(dr[0]),
                        Name = Convert.ToString(dr[1]),
                        Basic = Convert.ToDecimal(dr[2]),
                        DeptNo = Convert.ToInt32(dr[3]),
                    };
                }
                dr.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { cn.Close(); }
            return emp;
        }

        public static void InsertEmployee(Employee obj)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=JkJan23;Integrated Security=True;";
            try
            { 
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into Employees values(@EmpNo,@Name,@Basic,@DeptNo)";
                cmd.Parameters.AddWithValue("@EmpNo", obj.EmpNo);
                cmd.Parameters.AddWithValue("@Name", obj.Name);
                cmd.Parameters.AddWithValue("@Basic", obj.Basic);
                cmd.Parameters.AddWithValue("@DeptNo", obj.DeptNo);
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex) { Console.WriteLine(ex.Message); }
            finally{ cn.Close(); }
        }
        public static void UpdateEmployee(Employee emp)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=JkJan23;Integrated Security=True;";
            try
            {              
                cn.Open();
                SqlCommand cmd1 = new SqlCommand();
                cmd1.Connection = cn;
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "update Employees set Name=@Name,Basic=@Basic,DeptNo=@DeptNo where EmpNo=@EmpNo";
                cmd1.Parameters.AddWithValue("@EmpNo", emp.EmpNo);
                cmd1.Parameters.AddWithValue("@Name", emp.Name);
                cmd1.Parameters.AddWithValue("@Basic", emp.Basic);
                cmd1.Parameters.AddWithValue("@DeptNo", emp.DeptNo);
                cmd1.ExecuteNonQuery();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { cn.Close(); }
         }
        public static void DeleteEmployee(int id) 
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=JkJan23;Integrated Security=True;";
            try
            {       
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from Employees where EmpNo=@EmpNo";
                cmd.Parameters.AddWithValue("@EmpNo", id);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { cn.Close(); }
        }
    }
}
/*
 ->Build the project whenever you change something

 Scaffolding means the code that is generated by visual studio

 */