using Newtonsoft.Json;
using RestSharp;

namespace ClassLibrary1
{
   public class RestAPIHelper
   {
       private RestClient _client;
       private RestRequest request;
        public RestClient SetUrl(string baseUrl)
        {
            _client=new RestClient(baseUrl);
            return _client;
        }

       public RestRequest CreateGetRequest(string endPoint)
       {
           request=new RestRequest(endPoint,Method.GET);
           request.AddParameter("Accept","application/json");
           return request;
       }

       public RestRequest CreateDeleteRequest(string endPoint,string productId)
       {
           var resource = endPoint;
           request = new RestRequest(resource, Method.DELETE);
           request.AddParameter("productID", productId, ParameterType.UrlSegment);
           return request;
       }

       public RestRequest CreatePOSTRequest(string endpoint, Product product)
       {
           var resource = endpoint;
           request=new RestRequest(resource,Method.POST);
           var jsonToSend = JsonConvert.SerializeObject(product);
           request.AddParameter("application/json;charset;UFT-8", jsonToSend, ParameterType.RequestBody);
           return request;
       }

        public IRestResponse Execute()
       {
           var response=_client.Execute(request);
           return response;
       }

    }
   
}
