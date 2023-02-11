using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using PassionProject_SEMS2023.Models;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using System.Diagnostics;

namespace PassionProject_SEMS2023.Controllers
{
    public class EmployeeDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/EmployeeData/ListEmployees
        [HttpGet]
        public IEnumerable<EmployeeDto> ListEmployees()
        {
            List<Employee> Employees = db.Employees.ToList();
            List<EmployeeDto> EmployeeDtos = new List<EmployeeDto>();

            Employees.ForEach(e => EmployeeDtos.Add(new EmployeeDto()
            {
                EmployeeID = e.EmployeeID,
                EmployeeName = e.EmployeeName,
                EmployeePosition = e.EmployeePosition,
                DepartmentName = e.Department.DepartmentName
            }));
            return EmployeeDtos;
        }

        // GET: api/EmployeeData/FindEmployee/5
        [ResponseType(typeof(Employee))]
        [HttpGet]
        public IHttpActionResult FindEmployee(int id)
        {
            Employee Employee = db.Employees.Find(id);
            EmployeeDto EmployeeDto = new EmployeeDto()
            {
                EmployeeID = Employee.EmployeeID,
                EmployeeName = Employee.EmployeeName,
                EmployeePosition = Employee.EmployeePosition,
                DepartmentName = Employee.Department.DepartmentName
            };
            if (Employee == null)
            {
                return NotFound();
            }

            return Ok(EmployeeDto);
        }

        // POST: api/EmployeeData/UpdateEmployee/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateEmployee(int id, Employee employee)
        {
            Debug.WriteLine("I have reach the update Employee method");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model State is invalid");
                return BadRequest(ModelState);
            }

            if (id != employee.EmployeeID)
            {
                Debug.WriteLine("ID mismatch");
                Debug.WriteLine("GET paramter" +id);
                Debug.WriteLine("POST paramter" +employee.EmployeeID);
                Debug.WriteLine("POST paramter" + employee.EmployeeName);
                Debug.WriteLine("POST paramter" + employee.EmployeePosition);
                Debug.WriteLine("POST paramter" + employee.EmployeePosition);


                return BadRequest();
            }

            db.Entry(employee).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    Debug.WriteLine("Employee not found");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            Debug.WriteLine("None of the condition trigger");
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/EmployeeData/AddEmployee
        [ResponseType(typeof(Employee))]
        [HttpPost]
        public IHttpActionResult AddEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Employees.Add(employee);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = employee.EmployeeID }, employee);
        }

        // POST: api/EmployeeData/DeleteEmployee/5
        [ResponseType(typeof(Employee))]
        [HttpPost]
        public IHttpActionResult DeleteEmployee(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            db.Employees.Remove(employee);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeExists(int id)
        {
            return db.Employees.Count(e => e.EmployeeID == id) > 0;
        }
    }
}