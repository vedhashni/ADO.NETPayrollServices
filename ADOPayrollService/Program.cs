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

                case 3:
                    EmployeeRepository repository1 = new EmployeeRepository();
                    repository1.RetrieveDataBasedOnDateRange();
                    break;

                case 4:
                    EmployeeRepository repository2 = new EmployeeRepository();
                    repository2.PerformAggregateFunctions("F");
                    break;

                case 7:
                    ERRepository eRRepository = new ERRepository();
                    eRRepository.RetrieveAllData();
                    break;
                case 8:
                    ERRepository eRRepository1 = new ERRepository();
                    eRRepository1.UpdateSalaryQuery();
                    break;
                case 9:
                    ERRepository eRRepository2 = new ERRepository();
                    eRRepository2.DataBasedOnDateRange();
                    break;
                case 10:
                    ERRepository eRRepository3 = new ERRepository();

                    eRRepository3.PerformAggregateFunctions("F");
                    break;

                case 11:
                    Transaction transaction = new Transaction();
                    transaction.InsertIntoTables();
                    break;

                case 12:
                    Transaction transaction1 = new Transaction();
                    transaction1.MaintainListforAudit(1);
                    Transaction transaction2 = new Transaction();
                    transaction2.RetrieveAllData();
                    break;
            }
        }
    }
}
