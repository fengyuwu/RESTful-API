using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;


namespace RestfulAPI.Services.Tests
{
    [TestClass()]
    public class DataServiceTests
    {
        [TestMethod]
        public void IdentifyTop5Customers_ReturnsCorrectData()
        {
            // Arrange
            var dataService = new DataService();

            // Act
            var result = dataService.IdentifyTop5Customers() as JsonResult;

            // Assert
            Assert.IsNotNull(result);//Check for null
            Assert.IsTrue(result.Value is IEnumerable<object>); // Check for collection of objects

            var top5Customers = result.Value as IEnumerable<object>;
            Assert.AreEqual(5, top5Customers.Count()); // Check if I have 5 customers return

         

            Assert.IsNotNull(top5Customers.ElementAt(0).GetType().GetProperty("customer_id"));
            Assert.IsNotNull(top5Customers.ElementAt(0).GetType().GetProperty("first_name"));
            Assert.IsNotNull(top5Customers.ElementAt(0).GetType().GetProperty("last_name"));


        }

        [TestMethod]
        public void GetTotalAmountSpent_ReturnsCorrectData()
        {
            // Arrange
            var dataService = new DataService();

            // Act
            var result = dataService.GetTotalAmountSpent() as JsonResult;

            // Assert
            Assert.IsNotNull(result);//Check for null
            Assert.IsTrue(result.Value is IEnumerable<object>); // Check if the result are objects

        }

        [TestMethod]
        public void GetMergedData_ReturnsCorrectData()
        {
            // Arrange
            var dataService = new DataService();

            // Act
            var result = dataService.GetMergedData();

            // Assert
            Assert.IsNotNull(result);//Check for null
            Assert.IsTrue(result is IEnumerable<object>); 


        }

        //More Edge Cases to be added, create empty or malformed csv files and put try to parse it to ensure the methods behave correctly under different scenarios. 
        
         
    }
}
