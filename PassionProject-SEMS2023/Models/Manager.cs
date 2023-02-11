using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PassionProject_SEMS2023.Models
{
    public class Manager
    {
        [Key]
        public int ManagerID { get; set; }

        public string ManagerName { get; set; }

        public string ManagerBranch { get; set; }

        public string ManagerPosition { get; set; }

        // A Manager can take care of many employee

        public ICollection<Employee> Employees { get; set; }


    }
}