using ChatAppWithSignalR.Client.Services.Authenticate;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppWithSignalR.Client.Services
{
    public class ServiceProvider
    {
        private static ServiceProvider _instance;
        //private string _serverRootUrl = "https://10.0.2.2:7093";
        public string _accessToken = "";
        private ServiceProvider() { }

        public static ServiceProvider GetInstance()
        {
            if (_instance == null)
                _instance = new ServiceProvider();

            return _instance;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request)
        {
            var devSslHelper = new DevHttpsConnectionHelper(sslPort: 7093);
            using (HttpClient client = devSslHelper.HttpClient)
            {
                client.Timeout = TimeSpan.FromSeconds(10);
                var httpRequestMessage = new HttpRequestMessage();
                httpRequestMessage.Method = HttpMethod.Post;
                httpRequestMessage.RequestUri = new Uri(devSslHelper.DevServerRootUrl + "/Authenticate/Authenticate");

                if (request != null)
                {
                    string jsonContent = JsonConvert.SerializeObject(request);
                    var httpContent = new StringContent(jsonContent, encoding: Encoding.UTF8, "application/json"); ;
                    httpRequestMessage.Content = httpContent;
                }

                try
                {
                    var response = await client.SendAsync(httpRequestMessage);
                    var responseContent = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<AuthenticateResponse>(responseContent);
                    result.StatusCode = (int)response.StatusCode;

                    if (result.StatusCode == 200)
                    {
                        _accessToken = result.Token;
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    var result = new AuthenticateResponse
                    {
                        StatusCode = 500,
                        StatusMessage = ex.Message
                    };
                    return result;
                }
            }
        }

        public async Task<TResponse> CallWebApi<TRequest, TResponse>(
            string apiUrl, HttpMethod httpMethod, TRequest request) where TResponse :BaseResponse
        {
            var devSslHelper = new DevHttpsConnectionHelper(sslPort: 7093);
            using (HttpClient client = devSslHelper.HttpClient)
            {
                client.Timeout = TimeSpan.FromSeconds(10);
                var httpRequestMessage = new HttpRequestMessage();
                httpRequestMessage.Method = HttpMethod.Post;
                httpRequestMessage.RequestUri = new Uri(devSslHelper.DevServerRootUrl + apiUrl);
                httpRequestMessage.Headers.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer" , _accessToken);

                if (request != null)
                {
                    string jsonContent = JsonConvert.SerializeObject(request);
                    var httpContent = new StringContent(jsonContent, encoding: Encoding.UTF8, "application/json"); ;
                    httpRequestMessage.Content = httpContent;
                }

                try
                {
                    var response = await client.SendAsync(httpRequestMessage);
                    var responseContent = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<TResponse>(responseContent);
                    result.StatusCode = (int)response.StatusCode;

                    return result;
                }
                catch (Exception ex)
                {
                    var result = Activator.CreateInstance<TResponse>();
                    result.StatusCode = 500;
                    result.StatusMessage = ex.Message;
                    return result;
                }
            }
        }
    }
}
