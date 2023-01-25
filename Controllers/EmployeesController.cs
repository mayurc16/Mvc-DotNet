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
            List<Employee> list = Employee.GetAllEmployee();       
            return View(list);
        }

        // GET: EmployeesController/Details/5
        public ActionResult Details(int id)
        {
            Employee emp = Employee.GetEmployee(id);
            return View(emp);
        }

        // GET: EmployeesController/Create
        public ActionResult Create()
        {
            List<Department> list = Department.GetAllDepartment();
            ViewBag.list = list;
            return View();
        }

        // POST: EmployeesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee obj)
        {
            Employee.InsertEmployee(obj);
            return RedirectToAction(nameof(Index));
        }

        // GET: EmployeesController/Edit/5
        public ActionResult Edit(int id)
        {
            List<Department> list = Department.GetAllDepartment();
            Employee emp= Employee.GetEmployee(id);
            ViewBag.list = list;
            return View(emp);
        }

        // POST: EmployeesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( IFormCollection collection,Employee emp)
        {
            Employee.UpdateEmployee(emp);
            return RedirectToAction(nameof(Index));
        }

        // GET: EmployeesController/Delete/5
        public ActionResult Delete(int id)
        {
            Employee emp= Employee.GetEmployee(id);
            return View(emp);
        }

        // POST: EmployeesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            Employee.DeleteEmployee(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
