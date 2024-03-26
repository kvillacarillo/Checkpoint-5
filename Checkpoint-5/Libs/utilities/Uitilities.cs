using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Checkpoint_5.Libs.enums.Department;
using EDepartment = Checkpoint_5.Libs.enums.Department.Departments;

namespace Checkpoint_5.Libs.utilities
{
    internal class Uitilities : IUtilities
    {
        public T InputHandler<T>(String inputName, out T value) where T : IConvertible
        {
            string input;
            try
            {
                Console.WriteLine("Enter " + inputName + $" [TYPE] <{typeof(T).Name}>");
                input = Console.ReadLine() ?? "";

                if (string.IsNullOrEmpty(input))
                {
                    throw new Exception("Blank values are not allowed!");
                }

                if (typeof(T) != typeof(T))
                {
                    throw new Exception("Datatype Error!");
                }


                if (typeof(T) == typeof(string) && input.Any(char.IsDigit))
                {
                    throw new Exception("String cannot contain numbers!");
                }

                if (typeof(T) == typeof(EDepartment))
                {
                    value = (T)Enum.Parse(typeof(T), input);

                    return value;
                }

                value = (T)Convert.ChangeType(input, typeof(T));

                return value;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return InputHandler(inputName, out value);
            }
        }
    }
}
