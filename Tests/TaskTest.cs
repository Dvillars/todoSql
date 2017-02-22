using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ToDoList
{
    public class ToDoTest : IDisposable
    {
        public ToDoTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=todo_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void Test_DatabaseEmptyAtFirst()
        {
            //Arrange, Act
            int result = Task.GetAll().Count;

            //Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void Test_EqualOverrideTrueForSameDescription()
        {
            //Arrange, Act
            Task firstTask = new Task("Mow the lawn", 1, "04/22/2017");
            Task secondTask = new Task("Mow the lawn", 1, "04/22/2017");

            firstTask.Save();
            secondTask.Save();

            //Assert
            Assert.NotEqual(firstTask, secondTask);
        }

        [Fact]
        public void Test_Save()
        {
            //Arrange
            Task testTask = new Task("Mow the lawn", 1, "05/15/2020");
            testTask.Save();

            //Act
            List<Task> result = Task.GetAll();
            List<Task> testList = new List<Task>{testTask};

            //Assert
            Assert.Equal(testList, result);
        }

        [Fact]
        public void Test_SaveAssignsIdToObject()
        {
            //Arrange
            Task testTask = new Task("Mow the carpet", 1, "05/10/2210");

            testTask.Save();

            //Act
            Task savedTask = Task.GetAll()[0];

            int result = savedTask.GetId();
            int testId = testTask.GetId();

            //Assert
            Assert.Equal(testId, result);
        }

        [Fact]
        public void Test_FindFindsTaskInDatabase()
        {
            //Arrange
            Task testTask = new Task("Mow the lawn", 1, "10/01/2018");
            testTask.Save();

            //Act
            Task foundTask = Task.Find(testTask.GetId());

            //Assert
            Assert.Equal(testTask, foundTask);
        }

        [Fact]
        public void Test_SaveCompleteBy()
        {
            //Arrange
            Task testTask = new Task("Mow the lawn", 1, "09/29/2020");
            testTask.Save();

            //Act
            List<Task> result = Task.GetAll();
            List<Task> testList = new List<Task>{testTask};

            //Assert
            Assert.Equal(testList, result);
        }

        public void Dispose()
        {
            Task.DeleteAll();
            Category.DeleteAll();
        }
    }
}
