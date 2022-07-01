using Capi.FileProcessing.Core.Collection.Application;
using Capi.FileProcessing.Core.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
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
    public class OutgoingServices : IOutgoingServices
    {
        private readonly IConfiguration _configuration;
        public OutgoingServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<string>> Outgoing()
        {
            var fileDirectory = "";
            var fileDestination = "";
            var urls = _configuration.GetSection("URLs");
            fileDirectory = urls.GetSection("fileDirectoryOutgoing").Value;
            fileDestination = urls.GetSection("fileDestinationOutgoing").Value;
            var clientUrl = urls.GetSection("clientUrl").Value;
            DirectoryInfo dir = new DirectoryInfo(fileDirectory);
            FileInfo[] files = dir.GetFiles().OrderBy(p => p.Name).ToArray();
            List<string> convertedFile = new List<string>();          
            try
            {
                foreach (FileInfo f in files)
                {             
                    string base64 = ConvertFile(f.FullName);
                    var model = new FileSendModel() { Content = base64, FileName = Path.GetFileName(f.FullName) };
                    var token = await GetAccessToken();
                    var client = new RestClient($"{clientUrl}");
                    var request = new RestRequest(Method.POST);
                    string content = JsonConvert.SerializeObject(model);
                    request.AddHeader("Authorization", $"Bearer {token}");
                    request.AddJsonBody(content);
                    var response = await client.ExecuteAsync(request);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string successFile = Path.Combine(fileDestination, Path.GetFileName(f.FullName));
                        File.Move(f.FullName, successFile, true);
                        convertedFile.Add($"File - {f.FullName} Successfully sent");
                    }
                }
            }
            catch (Exception e)
            {
                convertedFile.Add(e.Message);
            }
            return convertedFile;
        }

        private string ConvertFile(string file)
        {
            byte[] fileBytes = File.ReadAllBytes(file);
            string base64 = Convert.ToBase64String(fileBytes);
            return base64;
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

    }
}
