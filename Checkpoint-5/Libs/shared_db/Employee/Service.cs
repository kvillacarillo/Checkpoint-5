using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkpoint_5.Libs.shared_db.Employee;
using Newtonsoft.Json;
using Departments = Checkpoint_5.Libs.enums.Department.Departments;

namespace Checkpoint_5.Libs.shared_db.Employee
{
    internal class Service : IEmployee, IDisposable
    {
        const string filePath = "Employee.txt";
        public Service() {
        }

        public void AddEmployee(string fname, string lname, double salary, Departments department)
        {
            try {
                int id = GetEmployeeCount() + 1;
                Employee employee = new Employee
                {
                    Id = id,
                    FirstName = fname,
                    LastName = lname,
                    Salary = salary,
                    Department = department
                };

                string dataLine = $"{employee.Id},{employee.FirstName},{employee.LastName},{employee.Salary},{employee.Department}";
                using (StreamWriter writer = File.AppendText(filePath))
                {
                    writer.WriteLine(dataLine);
                }
                Console.WriteLine("Employee added successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);
            }
        }

        public void UpdateEmployee(int employeeId, Employee employee)
        {
            try
            {
                List<Employee> employees = GetEmployees();
                bool found = false;

                using (StreamWriter writer = new StreamWriter(filePath, false))
                {
                    foreach (Employee emp in employees)
                    {
                        if (emp.Id == employeeId)
                        {
                            writer.WriteLine($"{emp.Id},{employee.FirstName},{employee.LastName},{employee.Salary}, {employee.Department}");
                            found = true;
                        }
                        else
                        {
                            writer.WriteLine($"{emp.Id},{emp.FirstName},{emp.LastName},{emp.Salary}, {emp.Department}");
                        }
                    }
                }

                if (!found)
                {
                    Console.WriteLine("Employee not found for update.");
                }
                else
                {
                    Console.WriteLine("Employee updated successfully!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);
            }
           
        }

        public void DeleteEmployee(int employeeId)
        {
            try
            {
                List<Employee> employees = GetEmployees();
                List<Employee> newEmployees = new List<Employee>();

                foreach (Employee emp in employees)
                {
                    if (emp.Id != employeeId)
                    {
                        newEmployees.Add(emp);
                    }
                }

                using (StreamWriter writer = new StreamWriter(filePath, false))
                {
                    foreach (Employee emp in newEmployees)
                    {
                        writer.WriteLine($"{emp.Id},{emp.FirstName},{emp.LastName},{emp.Salary}, {emp.Department}");
                    }
                }
                if (employees.Count == newEmployees.Count)
                {
                    Console.WriteLine("Employee not found for deletion.");
                }
                else
                {
                    Console.WriteLine("Employee deleted successfully!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);
            }
          
        }

        public Employee GetEmployee(int employeeId)
        {
            try 
            {
                List<Employee> employees = GetEmployees();
                Employee employee = employees.Find(emp => emp.Id == employeeId) ?? new Employee();

                if (employee == null)
                {
                    Console.WriteLine("Employee not found.");

                    return employee ?? new Employee();
                }

                Console.WriteLine($"Fetched employee {employee}");

                return employee;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);
            }
          

        }

        public List<Employee> GetEmployees()
        {
            try {
                List<Employee> employees = new List<Employee>();
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("No data found.");
                    return employees;
                }

                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] data = line.Split(',');
                    Console.WriteLine(line);
                    Employee employee = new Employee
                    {
                        Id = (int)Convert.ToInt64(data[0]),
                        FirstName = data[1].Trim(),
                        LastName = data[2].Trim(),
                        Salary = Convert.ToDouble(data[3]),
                        Department = (Departments)Enum.Parse(typeof(Departments), data[4].Trim())
                    };
                    employees.Add(employee);
                }

                return employees;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);
            }
           
        }

        public int GetEmployeeCount()
        {
            List<Employee> employees = GetEmployees();
            return employees.Count;
        }

        public void Dispose()
        {
            Console.WriteLine("Disposing resources...");
        }
    }
}
