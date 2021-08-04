using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ADOPayrollService
{
    public class EmployeeRepository
    {
        //Connecting to Database
        public static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=payroll_service;Integrated Security=True";
        //passing the string to sqlconnection to make connection 
        SqlConnection sqlconnection = new SqlConnection(connectionString);
        //GetAllEmployee method
        public void GetAllEmployee()
        {
            try
            {
                //Creating object for employeemodel and access the fields
                EmployeeModel employeeModel = new EmployeeModel();
                //Retrieve query
                string query = @"select * from employee_payroll";

                SqlCommand sqlCommand = new SqlCommand(query, sqlconnection);
                //Open the connection
                this.sqlconnection.Open();
                //ExecuteReader: Returns data as rows.
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        employeeModel.empId = Convert.ToInt32(reader["empId"]);
                        employeeModel.name = reader["name"].ToString();
                        employeeModel.BasicPay = Convert.ToDouble(reader["Basic Pay"]);
                        employeeModel.startDate = reader.GetDateTime(3);
                        employeeModel.emailId = reader["emailId"].ToString();
                        employeeModel.Gender = reader["Gender"].ToString();
                        employeeModel.Department = reader["Department"].ToString();
                        employeeModel.PhoneNumber = Convert.ToDouble(reader["PhoneNumber"]);
                        employeeModel.Address = reader["Address"].ToString();
                        employeeModel.Deductions = Convert.ToDouble(reader["Deductions"]);
                        employeeModel.TaxablePay = Convert.ToDouble(reader["TaxablePay"]);
                        employeeModel.IncomeTax = Convert.ToDouble(reader["IncomeTax"]);
                        employeeModel.NetPay = Convert.ToDouble(reader["NetPay"]);
                        Console.WriteLine("{0} {1} {2}  {3} {4} {5}  {6}  {7} {8} {9} {10} {11} {12}", employeeModel.empId, employeeModel.name, employeeModel.BasicPay, employeeModel.startDate, employeeModel.emailId, employeeModel.Gender, employeeModel.Department, employeeModel.PhoneNumber, employeeModel.Address, employeeModel.Deductions, employeeModel.TaxablePay, employeeModel.IncomeTax, employeeModel.NetPay);
                        Console.WriteLine("\n");
                    }

                }
                else
                {
                    Console.WriteLine("No data found");
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                this.sqlconnection.Close();
            }
        }

        /// <summary>
        /// UC3-Update the salary data using query for particular data
        /// </summary>
        /// <param name="model"></param>
        public void UpdateSalary(EmployeeModel model)
        {
            try
            {
                sqlconnection.Open();
                string query = @"update employee_payroll set Basic Pay = 3000000 where name='Karthick'";
                SqlCommand command = new SqlCommand(query, sqlconnection);

                int result = command.ExecuteNonQuery();
                if (result != 0)
                {
                    Console.WriteLine("Salary Updated Successfully ");
                }
                else
                {
                    Console.WriteLine("Unsuccessfull");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlconnection.Close();

            }

        }

        /// <summary>
        /// UC5-Retrieve employee details based on date range
        /// </summary>
        /// <returns></returns>
        public int RetrieveDataBasedOnDateRange()
        {
            //string nameList = null;
            int count = 0;
            try
            {
                using (sqlconnection)
                {
                    //query execution
                    string query = @"select * from employee_payroll where startDate between('2021-05-01') and getdate()";
                    //passing the query and connection
                    SqlCommand command = new SqlCommand(query, this.sqlconnection);
                    //open sql connection
                    sqlconnection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            //Display the details 
                            DisplayEmployeeDetails(reader);

                            //nameList += reader["name"].ToString() + " ";
                            count++;
                        }
                    }
                    //close reader
                    reader.Close();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlconnection.Close();
            }
            return count;

        }
        /// <summary>
        ///DisplayEmployeeDetails method 
        /// </summary>
        /// <param name="reader"></param>
        public void DisplayEmployeeDetails(SqlDataReader reader)
        {
            EmployeeModel model = new EmployeeModel();
            //Read data SqlDataReader and store 

            model.empId = Convert.ToInt32(reader["empId"]);
            model.name = reader["name"].ToString();
            model.BasicPay = Convert.ToDouble(reader["BasicPay"]);
            model.startDate = reader.GetDateTime(3);
            model.emailId = reader["emailId"].ToString();
            model.Gender = reader["Gender"].ToString();
            model.Department = reader["Department"].ToString();
            model.PhoneNumber = Convert.ToDouble(reader["PhoneNumber"]);
            model.Address = reader["Address"].ToString();
            model.Deductions = Convert.ToDouble(reader["Deductions"]);
            model.TaxablePay = Convert.ToDouble(reader["TaxablePay"]);
            model.IncomeTax = Convert.ToDouble(reader["IncomeTax"]);
            model.NetPay = Convert.ToDouble(reader["NetPay"]);
            Console.WriteLine("{0} {1} {2}  {3} {4} {5}  {6}  {7} {8} {9} {10} {11} {12}", model.empId, model.name, model.BasicPay, model.startDate, model.emailId, model.Gender, model.Department, model.PhoneNumber, model.Address, model.Deductions, model.TaxablePay, model.IncomeTax, model.NetPay);
            Console.WriteLine("\n");

        }
    }
}
