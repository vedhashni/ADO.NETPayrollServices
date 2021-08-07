using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ADOPayrollService
{
    public class Transaction
    {
        public static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Payroll_Services;Integrated Security=True";
        SqlConnection SqlConnection = new SqlConnection(connectionString);
        //Transaction Query
        public int InsertIntoTables()
        {
            int flag = 0;
            using (SqlConnection)
            {
                //Opening the transation
                SqlConnection.Open();
                //Begin the transaction 
                SqlTransaction sqlTransaction = SqlConnection.BeginTransaction();
                //Create a Command
                SqlCommand sqlCommand = SqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;
                try
                {
                    //Insert data into Table
                    sqlCommand.CommandText = "Insert into Employee1(Company_Id, EmployeeName, EmployeePhoneNum, EmployeeAddress, StartDate, Gender)values(1,'Revathi','897845732','Adam Street','2021-05-01','F')";
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.CommandText = "Insert into PayRollCalculate1 (Employee_Id,BasicPay) values('5','90000')";
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.CommandText = "update PayRollCalculate1 set Deductions = (BasicPay *20)/100 where Employee_Id = '5'";
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.CommandText = "update PayRollCalculate1 set TaxablePay = (BasicPay - Deductions) where Employee_Id = '5'";
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.CommandText = "update PayRollCalculate1 set IncomeTax = (TaxablePay * 10) / 100 where Employee_Id = '5'";
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.CommandText = "update PayRollCalculate1 set NetPay = (BasicPay - IncomeTax) where Employee_Id = '5'";
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.CommandText = "Insert into EmployeeDept values('3','5')";
                    sqlCommand.ExecuteNonQuery();
                    //Commit the transaction If successful 
                    sqlTransaction.Commit();
                    Console.WriteLine("Updated Successfully!");
                    flag = 1;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //Rollback if not inserted properly
                    sqlTransaction.Rollback();
                    flag = 0;

                }
            }
            return flag;
        }

        /// <summary>
        /// Cascading Delete Operation
        /// </summary>
        /// <returns></returns>
        public int DeleteUsingCasadeDelete()
        {
            int result = 0;
            using (SqlConnection)
            {
                SqlConnection.Open();
                //Begin SQL transaction
                SqlTransaction sqlTransaction = SqlConnection.BeginTransaction();
                SqlCommand sqlCommand = SqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;
                try
                {
                    //Delete the corresponding value which has employee id 4
                    sqlCommand.CommandText = "Delete from Employee1 where EmployeeID='4'";
                    sqlCommand.ExecuteNonQuery();
                    result++;
                    //Commit the transcation
                    sqlTransaction.Commit();
                    Console.WriteLine("Deleted Successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //Rollback to the point before exception
                    sqlTransaction.Rollback();
                }
            }
            return result;
        }

        /// <summary>
        /// Adding the IsActive column in employee1 table
        /// </summary>
        /// <returns></returns>
        public string AddIsActiveColumn()
        {
            string result = null;
            using (SqlConnection)
            {
                SqlConnection.Open();
                //Begins SQL transaction
                SqlTransaction sqlTransaction = SqlConnection.BeginTransaction();
                SqlCommand sqlCommand = SqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;
                try
                {
                    //Add column IsActive in Employee1
                    sqlCommand.CommandText = "Alter table Employee1 add IsActive int NOT NULL default 1";
                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                    result = "IsActive Column Added";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //Rollback to the point before exception
                    sqlTransaction.Rollback();
                    result = "Column not Updated";
                }
            }
            SqlConnection.Close();
            return result;
        }
        public int MaintainListforAudit(int IDValue)
        {
            int res = 0;
            SqlConnection.Open();
            using (SqlConnection)
            {
                //Begin sql transaction
                SqlTransaction sqlTransaction = SqlConnection.BeginTransaction();
                SqlCommand sqlCommand = SqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;
                try
                {
                    sqlCommand.CommandText = @"Update Employee1 set IsActive = 0 where EmployeeID = '" + IDValue + "'";
                    sqlCommand.ExecuteNonQuery();
                    res++;
                    sqlTransaction.Commit();
                    Console.WriteLine("Updated Successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //Rollback to the last point
                    sqlTransaction.Rollback();
                }
            }
            SqlConnection.Close();
            return res;
        }
        /// <summary>
        ///Retrieve the data from table
        /// </summary>
        public void RetrieveAllData()
        {
            //Open Connection
            SqlConnection.Open();

            try
            {
                string query = @"Select CompanyID,CompanyName,EmployeeID,EmployeeName,EmployeeAddress,IsActive,EmployeePhoneNum,StartDate,Gender,BasicPay,TaxablePay,IncomeTax,NetPay,Deductions,DepartmentId,DepartName
from Company inner join Employee1 on Company.CompanyID=Employee1.Company_Id and Employee1.IsActive=1
inner join PayRollCalculate on PayRollCalculate.Employee_Id=Employee1.EmployeeId
inner join EmployeeDept on EmployeeDept.Employee_Id=Employee1.EmployeeID
inner join DepartmentTable on DepartmentTable.DepartmentId=EmployeeDept.Dept_Id";
                SqlCommand sqlCommand = new SqlCommand(query, SqlConnection);
                DisplayEmployeeDetails(sqlCommand);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //Close Connection
            SqlConnection.Close();
        }
        //Display the employee details
        public void DisplayEmployeeDetails(SqlCommand sqlCommand)
        {
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            //Creating the list to add data
            List<EmployeeModel> employeeList = new List<EmployeeModel>();

            if (sqlDataReader.HasRows)
            {
                //Read each row
                while (sqlDataReader.Read())
                {
                    //Creating object for employeemodel
                    EmployeeModel model = new EmployeeModel();

                    model.empId = Convert.ToInt32(sqlDataReader["EmployeeID"]);
                    model.CompanyID = Convert.ToInt32(sqlDataReader["CompanyID"]);
                    model.name = sqlDataReader["EmployeeName"].ToString();
                    model.CompanyName = sqlDataReader["CompanyName"].ToString();
                    model.BasicPay = Convert.ToDouble(sqlDataReader["BasicPay"]);
                    model.Deductions = Convert.ToDouble(sqlDataReader["Deductions"]);
                    model.IncomeTax = Convert.ToDouble(sqlDataReader["IncomeTax"]);
                    model.TaxablePay = Convert.ToDouble(sqlDataReader["TaxablePay"]);
                    model.NetPay = Convert.ToDouble(sqlDataReader["NetPay"]);
                    model.Gender = Convert.ToString(sqlDataReader["Gender"]);
                    model.PhoneNumber = Convert.ToInt64(sqlDataReader["EmployeePhoneNum"]);
                    model.Department = sqlDataReader["DepartName"].ToString();
                    model.Address = sqlDataReader["EmployeeAddress"].ToString();
                    model.startDate = Convert.ToDateTime(sqlDataReader["StartDate"]);
                    model.IsActive = Convert.ToInt32(sqlDataReader["IsActive"]);
                    //Display Data
                    Console.WriteLine("\nCompany ID: {0} \t Company Name: {1} \nEmployee ID: {2} \t Employee Name: {3} \nBasic Pay: {4} \t Deduction: {5} \t Income Tax: {6} \t Taxable Pay: {7} \t NetPay: {8} \nGender: {9} \t PhoneNumber: {10} \t Department: {11} \t Address: {12} \t Start Date: {13} \t IsActive: {14}", model.CompanyID, model.CompanyName, model.empId, model.name, model.BasicPay, model.Deductions, model.IncomeTax, model.TaxablePay, model.NetPay, model.Gender, model.PhoneNumber, model.Department, model.Address, model.startDate, model.IsActive);
                    employeeList.Add(model);
                }
                //Close sqlDataReader Connection
                sqlDataReader.Close();
            }
        }
    }
}
