using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model_Binding.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

namespace Model_Binding.Controllers
{
    public class EmployeesController : Controller
    {
        // GET: EmployeesController
        public ActionResult Index()
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
            
            return View(list);
        }

        // GET: EmployeesController/Details/5
        public ActionResult Details(int id)
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
                cmd.Parameters.AddWithValue("@id",id);
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

            return View(emp);
        }

        // GET: EmployeesController/Create
        public ActionResult Create()
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
            catch { return Index(); }   
            finally { cn.Close(); }
            ViewBag.list = list;
            return View();
        }

        // POST: EmployeesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=JkJan23;Integrated Security=True;";           
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into Employees values(@EmpNo,@Name,@Basic,@DeptNo)";
                cmd.Parameters.AddWithValue("@EmpNo",obj.EmpNo );
                cmd.Parameters.AddWithValue("@Name", obj.Name);
                cmd.Parameters.AddWithValue("@Basic", obj.Basic);
                cmd.Parameters.AddWithValue("@DeptNo", obj.DeptNo);
                cmd.ExecuteNonQuery();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeesController/Edit/5
        public ActionResult Edit(int id)
        {
            List<Department> list = new List<Department>();
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=JkJan23;Integrated Security=True;";
            Employee emp = null;
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Employees where EmpNo=@id;select * from Departments;";
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
                dr.NextResult();
                while (dr.Read())
                {
                    Department dept = new Department { DeptNo = Convert.ToInt32(dr[0]), DeptName = Convert.ToString(dr[1]) };
                    list.Add(dept);
                }
                ViewBag.list = list;
                dr.Close();
            }
            catch { return Index(); }
            return View(emp);
        }

        // POST: EmployeesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( IFormCollection collection,Employee emp)
        {
            try
            {            
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=JkJan23;Integrated Security=True;";
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
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeesController/Delete/5
        public ActionResult Delete(int id)
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

            return View(emp);
        }

        // POST: EmployeesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=JkJan23;Integrated Security=True;";
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from Employees where EmpNo=@EmpNo";
                cmd.Parameters.AddWithValue("@EmpNo", id);
               
                cmd.ExecuteNonQuery();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
