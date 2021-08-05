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
    }
}
