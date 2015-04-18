using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace mAxCommerce.Core
{
    public class HttpRestClient
    {
        private readonly HttpClient httpClient;

        public HttpRestClient(string baseAddress)
        {
            var handler = new HttpClientHandler();
            if (handler.SupportsAutomaticDecompression)
            {
                handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            }
            this.httpClient = new HttpClient(handler);
            this.httpClient.BaseAddress = new Uri(baseAddress);
        }

        public async Task<TResult> Get<TResult>(string requestUri)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            return await this.SendRequest<TResult>(request);
        }

        public async Task<TResult> Post<TContent, TResult>(string requestUri, TContent contentObject)
        {
            string contentString = JsonConvert.SerializeObject(contentObject);
            Debug.WriteLine("HttpRequestMessage content string:");
            Debug.WriteLine(contentString);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, requestUri);
            request.Content = new StringContent(contentString, Encoding.UTF8, "application/json");
            return await this.SendRequest<TResult>(request);
        }

        private async Task<TResult> SendRequest<TResult>(HttpRequestMessage request)
        {
            HttpResponseMessage response = await this.httpClient.SendAsync(request);
            string responseContent = await this.ReadHttpResponseContentAsStringAsync(response);
            Debug.WriteLine("HttpResponseMessage content string:");
            Debug.WriteLine(responseContent);
            return JsonConvert.DeserializeObject<TResult>(responseContent);
        }

        private async Task<string> ReadHttpResponseContentAsStringAsync(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                Debug.WriteLine("Failed HttpRequestMessage:");
                Debug.WriteLine(response.RequestMessage);
                Debug.WriteLine("Failed HttpResponseMessage:");
                Debug.WriteLine(response);
                throw new HttpRequestException("Unsuccessful response status code.");
            }
        }
    }
}
