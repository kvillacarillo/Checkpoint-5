using System;
using UtitlitiesServices = Checkpoint_5.Libs.utilities.Uitilities;
using IInputHandler = Checkpoint_5.Libs.utilities.IUtilities;
using EmployeeServices = Checkpoint_5.Libs.shared_db.Employee.Service;
using IEmployee = Checkpoint_5.Libs.shared_db.Employee.IEmployee;

using Employee = Checkpoint_5.Libs.shared_db.Employee.Employee;
using EDepartment = Checkpoint_5.Libs.enums.Department;
using Newtonsoft.Json.Linq;

namespace Checkpoint5
{
 class Program
 {
        private delegate bool arrayOperations(IInputHandler inputService, IEmployee employeeService);
        public Program()
        {

        }
        static void Main(string[] args)
        {

            IInputHandler inputService = new UtitlitiesServices();
            IEmployee employeeService = new EmployeeServices();

            displayOptions(inputService, employeeService);
        }

        private static void displayOptions(IInputHandler inputService, IEmployee employeeService)
        {
            bool gracefullyCompleted = false;

            var options = new Dictionary<int, arrayOperations>
            {
                {1, listAllEmployee},
                {2, addNewEmployee},
                {3, deleteEmployee},
                {4, updateEmployee},
            };

            do
            {
                Console.WriteLine("Welcome to the Student Grades System!\r\n[1]List Employees\r\n[2]Register New Employee\r\n[3]Delete Employee\r\n[4]Update Employee\r\n[5]Exit");
                inputService.InputHandler<int>("Option", out int option);
                if (options.ContainsKey(option))
                {
                    try {
                        options[option](inputService, employeeService);
                        gracefullyCompleted = false;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        gracefullyCompleted = false;

                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid option selected!");
                    displayOptions(inputService, employeeService);
                }

                if (inputService.InputHandler<bool>("Exit?", out bool toContinue))
                {
                    //Terminate
                    gracefullyCompleted = true;
                }

            } while (!gracefullyCompleted);
        }

        private static bool listAllEmployee(IInputHandler inputService, IEmployee employeeService)
        {
            Console.WriteLine("Listing Employees . . .");
            try
            { 
                List<Employee> employees = employeeService.GetEmployees();
                string formattedTable = $"{"Employee ID",-10} {"Firstname",-20} {"Lastname",-20} {"Salary",-10} {"Department",-12}";

                Console.WriteLine("----------|--------------------|--------------------|----------|------------");
                Console.WriteLine(formattedTable);
                Console.WriteLine("----------|--------------------|--------------------|----------|------------");

                foreach (Employee employee in employees)
                {
                    string body = string.Format("{0,-10} {1,-20} {2,-20} {3,-10:F2} {4,-12}", employee.Id, employee.FirstName, employee.LastName, employee.Salary, employee.Department);
                    Console.WriteLine(body);
                }


                if (inputService.InputHandler<bool>($"Enter again?", out bool enterAgain))
                {
                    deleteEmployee(inputService, employeeService);
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return true;
        }

        private static bool addNewEmployee(IInputHandler inputService, IEmployee employeeService)
        {
            Console.WriteLine("Adding Employee . . .");
            try 
            { 
               inputService.InputHandler<string>("Firstname", out string firstName);
               inputService.InputHandler<string>("Lastname", out string lastName);
               inputService.InputHandler<double>("Salary", out double salaryValue);
               inputService.InputHandler<EDepartment.Departments>("Department", out EDepartment.Departments departmentValue);


                employeeService.AddEmployee(firstName, lastName, salaryValue, departmentValue);


                if (inputService.InputHandler<bool>($"Enter again?", out bool enterAgain))
                {
                    addNewEmployee(inputService, employeeService);
                }

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private static bool deleteEmployee(IInputHandler inputService, IEmployee employeeService)
        {
            try 
            {
                inputService.InputHandler<int>("Employee ID", out int employeeId);
                employeeService.DeleteEmployee(employeeId);

                if (inputService.InputHandler<bool>($"Enter again?", out bool enterAgain))
                {
                    deleteEmployee(inputService, employeeService);
                }

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private static bool updateEmployee(IInputHandler inputService, IEmployee employeeService)
        {
            try
            {
                inputService.InputHandler<int>("Employee ID", out int employeeId);
                inputService.InputHandler<string>("Firstname", out string firstName);
                inputService.InputHandler<string>("Lastname", out string lastName);
                inputService.InputHandler<double>("Salary", out double salaryValue);
                inputService.InputHandler<EDepartment.Departments>("Department", out EDepartment.Departments departmentValue);

                Employee employee = new Employee
                {
                    Id = employeeId,
                    FirstName = firstName,
                    LastName = lastName,
                    Salary = salaryValue,
                    Department = departmentValue
                };

                employeeService.UpdateEmployee(employeeId, employee);

                if (inputService.InputHandler<bool>($"Enter again?", out bool enterAgain))
                {
                    updateEmployee(inputService, employeeService);
                }

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            } 
        }
 }
}