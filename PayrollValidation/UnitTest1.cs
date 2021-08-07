using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ADOPayrollService;
using System.Collections.Generic;


namespace PayrollValidation
{
    [TestClass]
    public class UnitTest1
    {
        EmployeeRepository employeeRepository;
        EmployeeModel model;
        Transaction transaction;
        
        [TestInitialize]
        public void SetUp()
        {
            employeeRepository = new EmployeeRepository();
            model = new EmployeeModel();
            transaction = new Transaction();
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
        /// UC4-Update the query using stored procedure
        /// </summary>
        [TestMethod]
        public void TestUpdateQueryUsingStoredProcedure()
        {
            int expected = 1;
            model.empId = 1;
            model.name = "Ashok";
            model.BasicPay = 30000000;
            int actual = employeeRepository.UpdateSalaryUsingStoredProcedure(model);
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

        /// <summary>
        /// Using ER Diagram Relationship -Retrieve the data using inner join
        /// </summary>

        [TestMethod]
        public void TestMethodForRetrieveDataUsingInnerJoin()
        {
            ERRepository eRRepository = new ERRepository();
            int expected = 4;
            var actual = eRRepository.RetrieveAllData();
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        /// Update the basicpay column and returns the data
        /// </summary>
        [TestMethod]
        public void TestMethodForUpdateQueryUsingErDiagramRelationShip()
        {
            ERRepository eRRepository = new ERRepository();
            int expected = 1;
            int actual = eRRepository.UpdateSalaryQuery();
            Assert.AreEqual(actual, expected);
        }
        /// <summary>
        /// Retruns the emploee details between date range
        /// </summary>
        [TestMethod]
        public void TestMethodForDateRangeUsingERDiagramRelationShip()
        {
            ERRepository eRRepository = new ERRepository();
            int expected = 2;
            int actual = eRRepository.DataBasedOnDateRange();
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        /// Performing aggreagte functions(sum,count,ab=vg,min,max)
        /// </summary>
        [TestMethod]
        public void TestMethodForPerformingAggregateFnUsingERDiagramRelationship()
        {
            ERRepository eRRepository = new ERRepository();

            string expected = "F 3135000 60000 3000000 1045000";
            string Gender = "F";
            string actual = eRRepository.PerformAggregateFunctions(Gender);
            Assert.AreEqual(expected, actual);
        }

        //------------------------Transcation Operation------------
        /// <summary>
        /// Insert the values in multiple tables(Using transcation)
        /// </summary>
        [TestMethod]
        public void TestMethodForInsertTheValuesInMultipleTables()
        {
            int expected = 1;
            int actual = transaction.InsertIntoTables();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Cascading Delete
        /// </summary>
        [TestMethod]
        public void TestMethodForCascadingDelete()
        {
            int expected = 1;
            int actual = transaction.DeleteUsingCasadeDelete();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethodAddingNewColumnInEmployeeTable()
        {
            string expected = "IsActive Column Added";
            var actual = transaction.AddIsActiveColumn();
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        /// Maintain the list and set the IsActive for 0(non-employees)
        /// </summary>
        [DataTestMethod]
        public void TestMethodForAuditPurpose()
        {
            int expected = 1;
            Transaction transaction = new Transaction();
            int actual = transaction.MaintainListforAudit(1);
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]

        public void TestMethodForWithoutUsingThread()
        {
            bool expected = true;
            bool actual = transaction.ImplementingWithoutUsingThread();
            Assert.AreEqual(expected, actual);
        }
    }
}
