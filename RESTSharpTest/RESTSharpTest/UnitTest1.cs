using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace RESTSharpTest
{
    public class Employee
    {
        public int id { get; set; }
        public string name { get; set; }
        public int salary { get; set; }
    }
    public class UnitTest1
    {
        RestClient client;
        [TestInitialize]
        public void Setup()
        {
            client = new RestClient("http://localhost:4000");
        }

        private IRestResponse GetEmployeeList()
        {
            // Request the client by giving resource url and method to perform
            IRestRequest request = new RestRequest("/employees", Method.GET);

            // Executing the request using client and saving the result in IrestResponse.
            IRestResponse response = client.Execute(request);
            return response;
        }

        [TestMethod]
        public void OnCallingAPI_RetrieveAllElementsFromJSONServer()
        {
            // Get the response
            IRestResponse response = GetEmployeeList();

            // Get all the elements into a list
            List<Employee> employeeList = JsonConvert.DeserializeObject<List<Employee>>(response.Content);

            // Assert
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            Assert.AreEqual(7, employeeList.Count);

            // Print all employees
            foreach (var item in employeeList)
            {
                Console.WriteLine("id: " + item.id + "\tName: " + item.name + "\tSalary: " + item.salary);
            }
        }
        [TestMethod]
        public void givenEmployee_OnPost_ShouldReturnAddedEmployee()
        {
            // Request the client by giving resource url and method to perform
            RestRequest request = new RestRequest("/employees", Method.POST);

            JObject jObjectbody = new JObject();
            jObjectbody.Add("name", "Natasha Romanuff");
            jObjectbody.Add("Salary", "85000");
            request.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);

            // Executing the request using client and saving the result in IrestResponse.
            IRestResponse response = client.Execute(request);
            Employee employeeList = JsonConvert.DeserializeObject<Employee>(response.Content);

            // Assert
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.Created);
            Assert.AreEqual("Natasha Romanuff", employeeList.name);
            Assert.AreEqual(85000, employeeList.salary);
        }
        [TestMethod]
        public void givenEmployee_OnPost_ShouldReturnAddedEmployee()
        {
            // Request the client by giving resource url and method to perform
            RestRequest request = new RestRequest("/employees", Method.POST);

            JObject jObjectbody = new JObject();
            jObjectbody.Add("name", "Natasha Romanuff");
            jObjectbody.Add("Salary", "60000");
            request.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);

            // Executing the request using client and saving the result in IrestResponse.
            IRestResponse response = client.Execute(request);
            Employee employeeList = JsonConvert.DeserializeObject<Employee>(response.Content);

            // Assert
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.Created);
            Assert.AreEqual("Natasha Romanuff", employeeList.name);
            Assert.AreEqual(85000, employeeList.salary);
        }
    }
}
