using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Refit;
using RefitVsRestSharp.Test.Apis;
using RestSharp;
using Xunit;

namespace RefitVsRestSharp.Test
{
    public sealed class HeadersUsage
    {
        [Fact]
        public async Task DoWithRestSharp()
        {
            var client = new RestClient("http://localhost:5000");
            var request = new RestRequest("api/resources");
            request.AddHeader("x-test", "header value");

            IRestResponse<string> response = await client.ExecuteAsync<string>(request);

            string responseContent = response.Data;
            Assert.Equal("x-test:header value", responseContent);
        }

        [Fact]
        public async Task DoWithRefitUsingStaticHeader()
        {
            var api = RestService.For<IResources>("http://localhost:5000");

            string response = await api.Get();
            
            Assert.Equal("x-test:header value", response);
        }

        [Fact]
        public async Task DoWithRefitUsingDynamicHeader()
        {
            var api = RestService.For<IResources>("http://localhost:5000");

            string response = await api.Get("header value");
            
            Assert.Equal("x-test:header value", response);
        }

        [Fact]
        public async Task DoWithRefitUsingHttpClientHandler()
        {
            Task<string> GetHeaderValue() => Task.FromResult("other header value");
            var handler = new CustomHttpClientHandler(GetHeaderValue);
            var httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri("http://localhost:5000")
            };

            var api = RestService.For<IResources>(httpClient);

            string response = await api.Get();
            
            Assert.Equal("x-test:other header value", response);      
        }

        
        class CustomHttpClientHandler : HttpClientHandler
        {
            private readonly Func<Task<string>> _getHeaderValue;

            public CustomHttpClientHandler(Func<Task<string>> headerValue)
            {
                _getHeaderValue = headerValue ?? throw new ArgumentNullException(nameof(headerValue));
            }

            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                request.Headers.Remove("x-test");
                request.Headers.Add("x-test", await _getHeaderValue());
                
                return await base.SendAsync(request, cancellationToken);
            }
        }
    }
}