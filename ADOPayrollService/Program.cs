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
                    model.empId = 1;
                    model.name = "Ashok";
                    model.BasicPay = 300000;
                    repository.UpdateSalaryUsingStoredProcedure(model);
                    EmployeeRepository repo = new EmployeeRepository();
                    repo.GetAllEmployee();
                    break;

                case 4:
                    EmployeeRepository repository1 = new EmployeeRepository();
                    repository1.RetrieveDataBasedOnDateRange();
                    break;

                case 5:
                    EmployeeRepository repository2 = new EmployeeRepository();
                    repository2.PerformAggregateFunctions("F");
                    break;

                case 6:
                    ERRepository eRRepository = new ERRepository();
                    eRRepository.RetrieveAllData();
                    break;
                case 7:
                    ERRepository eRRepository1 = new ERRepository();
                    eRRepository1.UpdateSalaryQuery();
                    break;
                case 8:
                    ERRepository eRRepository2 = new ERRepository();
                    eRRepository2.DataBasedOnDateRange();
                    break;
                case 9:
                    ERRepository eRRepository3 = new ERRepository();

                    eRRepository3.PerformAggregateFunctions("F");
                    break;

                case 10:
                    Transaction transaction = new Transaction();
                    transaction.InsertIntoTables();
                    break;

                case 11:
                    Transaction transaction1 = new Transaction();
                    transaction1.MaintainListforAudit(1);
                    Transaction transaction2 = new Transaction();
                    transaction2.RetrieveAllData();
                    break;
            }
        }
    }
}
