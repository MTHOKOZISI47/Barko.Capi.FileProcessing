using Capi.FileProcessing.Core.Collection.Application;
using Capi.FileProcessing.Core.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capi.FileProcessing.Application
{
    public class IncomingServices : IIncomingServices
    {
       private readonly IConfiguration _configuration;
               
        public IncomingServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> GetAccessToken()
        {
            var urls = _configuration.GetSection("BarkoIdp");
            var _idpUrl = urls.GetSection("IdpURL").Value;
            var _clientId = urls.GetSection("ClientId").Value;
            var _clientSecrete = urls.GetSection("ClientSecret").Value;
            var client = new RestClient($"{_idpUrl}");         
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("client_id", $"{_clientId}");
            request.AddParameter("client_secret", $"{_clientSecrete}");
            request.AddParameter("grant_type", "client_credentials");
            IRestResponse response = await client.ExecuteAsync(request);
            JObject obj = JObject.Parse(response.Content);
            return obj.Value<string>("access_token");
        }
              
        public void Incoming(FileSendModel model)
        {       
            if(model!=null)
            {
                string fileDir = Bootstrapper.Folder;
                byte[] bytes = Convert.FromBase64String(model.Content);
                string filePath = Path.Combine(fileDir, model.FileName);
                System.IO.File.WriteAllBytes(filePath, bytes);
            }    
          
        }

        private string ConvertFile(string file)
        {
            byte[] fileBytes = File.ReadAllBytes(file);
            string base64 = Convert.ToBase64String(fileBytes);
            return base64;
        }
    }
}
