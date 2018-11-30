using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;

namespace ClassLibrary1
{

    [TestFixture]
    public class Class1
    {
        public RestClient client;


        public RestRequest request;
        private RestAPIHelper helper = new RestAPIHelper();

        [SetUp]
        public void Setup()
        {
            client = helper.SetUrl("http://sampletestapi.getsandbox.com");
        }

        [Test]
        public void CallGetAPI()
        {
            request=helper.CreateGetRequest("getallproducts");
            var response=helper.Execute();
            //Assert.AreEqual(HttpStatusCode.OK,response.StatusCode);
            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);

            IList<Product> products=JsonConvert.DeserializeObject<List<Product>>(response.Content);

            foreach (var product in products)
            {
                Console.WriteLine(product.isSalable);
                Console.WriteLine(product.productID);
                Console.WriteLine(product.productName);
            }
        }

        [TestCase("1")]
        public void CallDeleteSingleProductAPI(string productId)
        {
            request = helper.CreateDeleteRequest("/deletesingleproduct/{productID}", productId);
            var response = helper.Execute();
            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);

        
        }


        [Test]
        public void CallPostMethod()
        {
            var product = new Product()
            {
                isSalable = "yes",
                productID = "7888",
                productName = "T-shirt"
            };
            request = helper.CreatePOSTRequest("/products",product);
            var response = helper.Execute();
            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);
        }


        [TestCase("1")]
        public void CallDeleteAPI(string productId)
        {
            request = helper.CreateDeleteRequest("/deletesingleproduct/{productID}", productId);
            var response = helper.Execute();
            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);
        }
    }
}
