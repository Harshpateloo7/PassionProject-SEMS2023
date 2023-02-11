using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PassionProject_SEMS2023.Models
{   
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }

        public string DepartmentName { get; set; }

        public string DepartmentRole { get; set; }


    }
}