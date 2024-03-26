using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Departments = Checkpoint_5.Libs.enums.Department.Departments;

namespace Checkpoint_5.Libs.shared_db.Employee
{
    internal interface IEmployee
    {
        public void AddEmployee(string fname, string lname, double salary, Departments department);
        public void UpdateEmployee(int employeeId, Employee employee);
        public void DeleteEmployee(int employeeId);
        public Employee GetEmployee(int employeeId);
        public List<Employee> GetEmployees();
        public int GetEmployeeCount();
    }
}
