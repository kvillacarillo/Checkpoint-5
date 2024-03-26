using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkpoint_5.Libs.shared_db.Person;
using Departments = Checkpoint_5.Libs.enums.Department.Departments;

namespace Checkpoint_5.Libs.shared_db.Employee
{
    internal class Employee : Person.Person
    {
        public double? Salary { get; set; }
        public Departments? Department { get; set; } = Departments.ANON;
    }
}
