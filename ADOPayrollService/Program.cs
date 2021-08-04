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
            EmployeeModel model = new EmployeeModel();

            int option = Convert.ToInt32(Console.ReadLine());
            switch (option)
            {
                case 1:
                    repository.GetAllEmployee();
                    break;
                case 2:
                    repository.UpdateSalary(model);
                    repository.GetAllEmployee();
                    break;
            }
        }
    }
}
