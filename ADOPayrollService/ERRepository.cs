using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ADOPayrollService
{
    public class ERRepository
    {
        //Giving path
        public static string connection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Payroll_Services;Integrated Security=True";
        //Connecting to sql server
        SqlConnection sqlConnection = new SqlConnection(connection);

        //Create Object for EmployeeData Repository
        EmployeeModel employeeModel = new EmployeeModel();

        /// <summary>
        /// ER DIAGRAM -Retrieve the data using join
        /// </summary>
        /// <returns></returns>
        public int RetrieveAllData()
        {
            int count = 0;
            try
            {
                //Open Connection
                sqlConnection.Open();
                //Inner join query
                string query = @"Select CompanyID,CompanyName,EmployeeID,EmployeeName,EmployeeAddress,EmployeePhoneNum,StartDate,Gender,BasicPay,TaxablePay,IncomeTax,NetPay,Deductions,DepartmentId,DepartName from Company_Table
         inner join Employee on Company_Table.CompanyID = Employee.Company_Id
         inner join PayRollCalculate on PayRollCalculate.Employee_Id = Employee.EmployeeId
         inner join EmployeeDept on EmployeeDept.Employee_Id = Employee.EmployeeID
          inner join DepartmentTable on DepartmentTable.DepartmentId = EmployeeDept.Dept_Id";
                //Passing the query and connection
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                SqlDataReader reader = sqlCommand.ExecuteReader();
                //Check if rows
                if (reader.HasRows)
                {
                    //Read each row
                    while (reader.Read())
                    {
                        //Read data SqlDataReader and store 
                        DisplayEmployeeDetails(reader);
                        count++;
                    }
                    //Close SQLDataReader Connection
                    reader.Close();
                }
                //Close Connection
                sqlConnection.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return count;
        }
        /// <summary>
        /// Update the basic pay using inner join
        /// </summary>
        /// <returns></returns>
        public int UpdateSalaryQuery()
        {
            //Open Connection
            sqlConnection.Open();
            string query = @"update PayrollCalculate set BasicPay = 3000000 where Employee_Id = (Select EmployeeID from Employee where EmployeeName = 'Priyadarshini')";

            //Pass query to TSql
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            int result = sqlCommand.ExecuteNonQuery();
            SqlDataReader dataReader = sqlCommand.ExecuteReader();
            if (result != 0)
            {
                Console.WriteLine("Salary Updated Successfully!");
            }
            else
            {
                Console.WriteLine("Not Updated!");
            }
            //Close Connection
            sqlConnection.Close();
            return result;
        }
        /// <summary>
        /// Print the employee details using inner join
        /// </summary>
        /// <returns></returns>
        public int DataBasedOnDateRange()
        {
            int count = 0;
            try
            {
                using (sqlConnection)
                {
                    //query execution
                    string query = @"select CompanyID,CompanyName,EmployeeID,EmployeeName,StartDate,BasicPay from Company
            inner join Employee on Company.CompanyID=Employee.Company_Id and StartDate between Cast('2020-01-01' as Date) and GetDate()
            inner join PayRollCalculate on Employee.EmployeeID=PayRollCalculate.Employee_Id;";
                    SqlCommand command = new SqlCommand(query, this.sqlConnection);
                    //open sql connection
                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = command.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            EmployeeModel model = new EmployeeModel();

                            model.empId = Convert.ToInt32(sqlDataReader["EmployeeID"]);
                            model.CompanyID = Convert.ToInt32(sqlDataReader["CompanyID"]);
                            model.name = sqlDataReader["EmployeeName"].ToString();
                            model.CompanyName = sqlDataReader["CompanyName"].ToString();
                            model.BasicPay = Convert.ToDouble(sqlDataReader["BasicPay"]);
                            model.startDate = sqlDataReader.GetDateTime(4);

                            Console.WriteLine("EmployeeId :{0}\t CompanyId :{1}\t EmployeeName:{2}\t CompanyName:{3}\t BasicPay:{4},StartDate:{5}", model.empId, model.CompanyID, model.name, model.CompanyName, model.BasicPay, model.startDate);
                            Console.WriteLine("\n");
                            count++;
                        }
                    }
                    //close reader
                    sqlDataReader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
            //returns the count of employee in the list between the given range
            return count;
        }
        public string PerformAggregateFunctions(string Gender)
        {
            string result = null;
            try
            {
                string query1 =@"select sum(BasicPay) as TotalSalary,min(BasicPay) as MinSalary,max(BasicPay) as MaxSalary,Round(avg(BasicPay),0) as AvgSalary,Gender,Count(*) from Employee inner join PayRollCalculate on Employee.EmployeeID=PayRollCalculate.Employee_Id where Gender =" + "'" + Gender + "'" + " group by Gender";

                SqlCommand sqlCommand = new SqlCommand(query1, this.sqlConnection);
                //Sends params to procedure
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.HasRows)
                {

                    while (sqlDataReader.Read())
                    {
                        Console.WriteLine("Total Salary {0}", sqlDataReader[0]);
                        Console.WriteLine("Average Salary {0}", sqlDataReader[1]);
                        Console.WriteLine("Minimum Salary {0}", sqlDataReader[2]);
                        Console.WriteLine(" Maximum Salary {0}", sqlDataReader[3]);
                        Console.WriteLine("No of employess {0}", sqlDataReader[4]);
                        //Console.WriteLine("Grouped By Gender {0}", sqlDataReader[5]);
                        result += sqlDataReader[4] + " " + sqlDataReader[0] + " " + sqlDataReader[1] + " " + sqlDataReader[2] + " " + sqlDataReader[3];
                    }
                }
                else
                {
                    result = "empty";
                }
                sqlDataReader.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
            return result;

        }
        public void DisplayEmployeeDetails(SqlDataReader reader)
        {
            //Creating object for Employeemodel which has fields
            EmployeeModel model = new EmployeeModel();


            model.empId = Convert.ToInt32(reader["EmployeeID"]);
            model.name = reader["EmployeeName"].ToString();
            model.BasicPay = Convert.ToDouble(reader["BasicPay"]);
            model.startDate = reader.GetDateTime(6);
            //model.emailId = reader["emailId"].ToString();
            model.Gender = reader["Gender"].ToString();
            model.Department = reader["DepartName"].ToString();
            model.PhoneNumber = Convert.ToDouble(reader["EmployeePhoneNum"]);
            model.Address = reader["EmployeeAddress"].ToString();
            model.Deductions = Convert.ToDouble(reader["Deductions"]);
            model.TaxablePay = Convert.ToDouble(reader["TaxablePay"]);
            model.IncomeTax = Convert.ToDouble(reader["IncomeTax"]);
            model.NetPay = Convert.ToDouble(reader["NetPay"]);
            model.CompanyID = Convert.ToInt32(reader["CompanyID"]);
            model.CompanyName = reader["CompanyName"].ToString();
            Console.WriteLine("\nCompany ID: {0} \t Company Name: {1} \nEmployee ID: {2} \t Employee Name: {3} \nBasic Pay: {4} \t Deduction: {5} \t Income Tax: {6} \t Taxable Pay: {7} \t NetPay: {8} \nGender: {9} \t PhoneNumber: {10} \t Department: {11} \t Address: {12} \t Start Date: {13}", model.CompanyID, model.CompanyName, model.empId, model.name, model.BasicPay, model.Deductions, model.IncomeTax, model.TaxablePay, model.NetPay, model.Gender, model.PhoneNumber, model.Department, model.Address, model.startDate);
        }
    }
}
