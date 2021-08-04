using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOPayrollService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("welcome to payroll service");

            //Creating a object for employeerepository
            EmployeeRepository repository = new EmployeeRepository();
            //Calling the method
            repository.GetAllEmployee();
        }
    }
}
