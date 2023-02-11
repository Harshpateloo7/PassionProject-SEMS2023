using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PassionProject_SEMS2023.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        public string EmployeeName { get; set; }

        public string EmployeePosition { get; set; }

        //An Employee belongs to one Department
        // A Department can have many employee
        [ForeignKey("Department")]
        public int DepartmentID { get; set; }

        public virtual Department Department { get; set; }

        // An Employee can be taken care of by many Manager

        public ICollection<Manager> Managers { get; set; }
    }

    public class EmployeeDto
    {
        public int EmployeeID { get; set; }

        public string EmployeeName { get; set; }

        public string EmployeePosition { get; set; }

        public string DepartmentName { get; set; }

    }

}