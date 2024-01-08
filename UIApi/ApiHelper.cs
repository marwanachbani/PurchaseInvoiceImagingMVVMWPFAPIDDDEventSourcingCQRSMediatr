using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;

using System.Net.Http.Headers;
using System.Net.PeerToPeer;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using UIApi.Models;
using UIApi.Responses;

namespace UIApi
{
    public  class ApiHelper 
    {
        private  readonly HttpClient _httpClient = new HttpClient();
        
        

        public  async Task<ObservableCollection<All>> GelAllForPeperPusher(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var endpoint = "http://localhost:5179/api/ScannerInvoice";
            
            var response = await _httpClient.GetAsync(endpoint);
            if(response.IsSuccessStatusCode == true )
            {
                var list = await response.Content.ReadAsAsync<ObservableCollection<All>>();
                return list;
            }
            return null;
                
        }
        public  async Task<byte[]> GelImage(string token , string id)
        {
            var endpoint = "http://localhost:5179/api/ScannerInvoice/getimagedata?id="+id;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            
            if(Guid.TryParse(id, out Guid result)==true)
            {
                var response = await _httpClient.GetAsync(endpoint);
                if (response.IsSuccessStatusCode == true)
                {
                    return await response.Content.ReadAsAsync<byte[]>();
                }
            }
            
            return null;
            
        }

        public  async Task<CommandResponse> Login(string username, string password)
        {
            var endpoint = "http://localhost:5179/api/account/login/";
       
            var date = new Login(username,password);
            var jsonContent =  Newtonsoft.Json.JsonConvert.SerializeObject(date);
            StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(endpoint, content);
           
            string responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CommandResponse>(responseContent);
        }
        public  async Task<CommandResponse>CreateInvoiceContent(string token,DateTime date,string committeBy)
        {
            var endpoint = "http://localhost:5179/api/Scannerinvoice/createcontent";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var data = new AddInvoiceContent(date,committeBy);
            var jsoncontent = JsonConvert.SerializeObject(data);
            StringContent content = new StringContent(jsoncontent, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(endpoint, content);

            string responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CommandResponse>(responseContent);
        }
        public  async Task<CommandResponse>EditInvoiceContent(string token , string content , DateTime date , string committedBy)
        {
            var endpoint = "http://localhost:5179/api/Scannerinvoice/modifyinvoicecontent";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var data = new EditInvoiceContent(content,date,committedBy);
            var jsoncontent = JsonConvert.SerializeObject(data);
            StringContent contentht = new StringContent(jsoncontent, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(endpoint, contentht);
            
            string responseContent = await response.Content.ReadAsStringAsync();
            
            return JsonConvert.DeserializeObject<CommandResponse>(responseContent);
        }
        public  async Task<CommandResponse> DeleteInvoiceContent(string token, string date, string committeBy)
        {
            var endpoint = "http://localhost:5179/api/Scannerinvoice/deleteinvoicecontent";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var data = new DeleteInvoiceContent(date, committeBy);
            var jsoncontent = JsonConvert.SerializeObject(data);
            StringContent content = new StringContent(jsoncontent, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(endpoint, content);

            string responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CommandResponse>(responseContent);
        }
        public  async Task<CommandResponse> AddInvoice(string token, byte[]image ,  string date, string committeBy)
        {
            var endpoint = "http://localhost:5179/api/Scannerinvoice/addinvoice";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var data = new AddInvoice(image,date, committeBy);
            var jsoncontent = JsonConvert.SerializeObject(data);
            StringContent content = new StringContent(jsoncontent, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(endpoint, content);

            string responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CommandResponse>(responseContent);
        }
        public async Task<CommandResponse> ReAddInvoice(Guid id , string token, byte[] image, string committeBy)
        {
            var endpoint = "http://localhost:5179/api/Scannerinvoice/readdinvoice";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var data = new ReAddInvoice(id, image, committeBy);
            var jsoncontent = JsonConvert.SerializeObject(data);
            StringContent content = new StringContent(jsoncontent, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(endpoint, content);

            string responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CommandResponse>(responseContent);
        }
        public async Task<CommandResponse> DeleteInvoice(Guid id, string token, string committeBy)
        {
            var endpoint = "http://localhost:5179/api/Scannerinvoice/deleteInvoice";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var data = new DeleteInvoice(id,  committeBy);
            var jsoncontent = JsonConvert.SerializeObject(data);
            StringContent content = new StringContent(jsoncontent, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(endpoint, content);

            string responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CommandResponse>(responseContent);
        }
        public async Task<CommandResponse> AddSupplier( Guid invoiveid, string invoicenum, string supplier, string street, string city, string state, string country, string zipcode , string committedby ,string token)
        {
            var endpoint = "http://localhost:5179/api/Scannerinvoice/AddSupplier";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var data = new AddSup(invoiveid,invoicenum,supplier,street,city,state,country,zipcode,committedby);
            var jsoncontent = JsonConvert.SerializeObject(data);
            StringContent content = new StringContent(jsoncontent, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(endpoint, content);

            string responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CommandResponse>(responseContent);
        }
        public async Task<CommandResponse>AddItems(Guid invoiceid,ObservableCollection<AddItems>items , string token , string user)
        {
            var endpoint = "http://localhost:5179/api/Scannerinvoice/AddItems";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var data = new AddItem(invoiceid,user,items);
            var jsoncontent = JsonConvert.SerializeObject(data);
            StringContent content = new StringContent(jsoncontent, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(endpoint, content);

            string responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CommandResponse>(responseContent);
        }
        public async Task<CommandResponse>AddCalculation(Guid id , decimal subT, int taxR , decimal taxAmnt , int redcR ,decimal redcAmnt , decimal total ,string token , string user )
        {
            var endpoint = "http://localhost:5179/api/Scannerinvoice/AddCalc";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var data = new AddCalc(id,subT, taxR,taxAmnt,redcR,redcAmnt,total,user);
            var jsoncontent = JsonConvert.SerializeObject(data);
            StringContent content = new StringContent(jsoncontent, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(endpoint, content);

            string responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CommandResponse>(responseContent);
        }
    }
    
}
