﻿using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.DataAccess;
using com.WanderingTurtle.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace EmployeeLogicTests
{   
    /// <summary>
    /// Tony Noel
    /// Created: 2015/03/27
    /// A complete testing of all employee logic methods in the Employee Manager.
    /// The test creates a dummy employee record and performs the various methods in the manager using this record.
    /// </summary>
    /// <remarks>
    /// Tony Noel
    /// Updated: 2015/04/10
    /// </remarks>
    [TestClass]
    public class EmployeeTests
    {   //Test values
        private string FirstName = "Test";
        private string LastName = "Passman";
        private string Password = "passman_1";
        private RoleData Level = (RoleData)2;
        private bool Active = true;

        private Employee testEmp;
        private EmployeeManager myManager;

        [TestInitialize]
        public void EmployeeManagerTestSetup()
        {
            myManager = new EmployeeManager();
            testEmp = new Employee(FirstName, LastName, Password, (int)Level, Active);
            addEmp();
        }

        #region Additional test attributes


        #endregion Additional test attributes
        public void addEmp()
        {
            ResultsEdit result = myManager.AddNewEmployee(testEmp);
        }
        [TestMethod]
        public void EmployeeManagerAddEmployee()
        {
            //Adds fake employee to Data base
            bool worked = false;
            ResultsEdit result = myManager.AddNewEmployee(testEmp);
            if (result == ResultsEdit.Success)
            {
                worked = true;
            }
            Assert.IsTrue(worked);
        }
        [TestMethod]
        public void EmployeeManagerFetchEmpList()
        {
            List<Employee> myList = new List<Employee>();
            myList = myManager.FetchListEmployees();
            bool worked = false;
            //Tests that a list greater than one is being returned.
            if (myList.Count > 1)
            {
                worked = true;
            }
            Assert.IsTrue(worked);
        }
        [TestMethod]
        public void EmployeeManagerGetEmpLogin()
        {
            //Grabs the fake emp id 
            int ID = TestCleanupAccessor.getTestEmp();
            //Gets the entire Employee Record by login info
            Employee orig = myManager.GetEmployeeLogin(ID, Password);
            //Asserts that the record returned matches the one in setup.
            Assert.AreEqual("Test", orig.FirstName);
        }
        [TestMethod]
        public void EmployeeManagerEditEmployee()
        {   //Grabs the fake emp id 
            int ID = TestCleanupAccessor.getTestEmp();
            //Gets the entire Employee Record
            Employee orig = EmployeeAccessor.GetEmployee(ID);
            //Creates a new employee object with the original properties, update the active property to false.
            Employee newEmp = new Employee(orig.FirstName, orig.LastName, orig.Password, (int)orig.Level, false);
            //calls to manager.
            ResultsEdit result = myManager.EditCurrentEmployee(orig, newEmp);
            //Asserts that the update went through
            Assert.AreEqual(ResultsEdit.Success, result);
        }
        [TestMethod]
        public void EmployeeManagerSearchEmployee()
        {
            //Uses the fake first name to find the employee
            List<Employee> emps = myManager.SearchEmployee("Test");
            //Asserts that the employee returned in the list will match the test setup employee
            Assert.AreEqual("Test", emps[emps.Count - 1].FirstName);
        }
        [TestCleanup]
        public void EmployeeManagerDeleteRecord()
        {
            TestCleanupAccessor.testEmp(testEmp);
        }
    }
}