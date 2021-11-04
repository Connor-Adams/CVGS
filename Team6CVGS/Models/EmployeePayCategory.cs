using System;
using System.Collections.Generic;

#nullable disable

namespace Team6CVGS.Models
{
    public partial class EmployeePayCategory
    {
        public EmployeePayCategory()
        {
            Employees = new HashSet<Employee>();
        }

        public string Code { get; set; }
        public string EnglishName { get; set; }
        public string FrenchName { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
