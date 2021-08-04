using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ADOPayrollService;

namespace PayrollValidation
{
    [TestClass]
    public class UnitTest1
    {
        EmployeeRepository employeeRepository;
        EmployeeModel model;
        [TestInitialize]
        public void SetUp()
        {
            employeeRepository = new EmployeeRepository();
            model = new EmployeeModel();
        }
        /// <summary>
        /// UC3-update the salary query 
        /// </summary>
        [TestMethod]
        [TestCategory("UpdateQuery")]
        public void TestUpdateQuery()
        {
            EmployeeModel employeeModel = new EmployeeModel();
            int expected = 1;
            int actual = employeeRepository.UpdateSalary(employeeModel);
            Assert.AreEqual(actual, expected);
        }

        /// <summary>
        /// UC5-Reeturn the count of details between the date range
        /// </summary>
        [TestMethod]
        public void TestMethodForDateRange()
        {
            int expected = 4;
            var actual = employeeRepository.RetrieveDataBasedOnDateRange();
            Assert.AreEqual(actual, expected);
        }

        /// <summary>
        /// UC6-Aggregate Functions
        /// </summary>
        [TestMethod]
        [TestCategory("Using SQL Query for Male")]
        public void TestMethodForAggregateFunction_GroupByFemale()
        {
            string expected = "M 240000 55000 70000 60000 4";
            string Gender = "M";
            string actual = employeeRepository.PerformAggregateFunctions(Gender);
            Assert.AreEqual(actual, expected);
        }
        [TestMethod]
        [TestCategory("Using SQL Query for Female")]
        public void TestMethodForAggregateFunction_GroupByMale()
        {
            string expected = "F 30050000 50000 30000000 15025000 2";
            string Gender = "F";
            string actual = employeeRepository.PerformAggregateFunctions(Gender);
            Assert.AreEqual(actual, expected);
        }
    }
}
