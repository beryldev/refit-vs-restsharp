using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Refit;
using RefitVsRestSharp.Server.Views;
using RefitVsRestSharp.Test.Apis;
using RestSharp;
using Xunit;

namespace RefitVsRestSharp.Test
{
    public class PostWithBody
    {
        private const string UserName = "TestUser";
        private const string Description = "Description";
        
        [Fact]
        public async Task DoWithRestSharp()
        {
            var client = new RestClient("http://localhost:5000");
            var request = new RestRequest("api/users", Method.POST);
            request.AddJsonBody(new User {Name = UserName, Description = Description});

            IRestResponse<string> response = await client.ExecuteAsync<string>(request);
            
            AssertJsonResult(response.Data);
        }

        [Fact]
        public async Task DoWithRefitUsingSerializedObject()
        {
            var api = RestService.For<IUsers>("http://localhost:5000");

            HttpResponseMessage response = await api.Create(new User {Name = UserName, Description = Description});
                
            AssertJsonResult(await response.Content.ReadAsStringAsync());
        }
        
        [Fact]
        public async Task DoWithRefitUsingString()
        {
            var api = RestService.For<IUsers>("http://localhost:5000");

            HttpResponseMessage response = await api.Create("{\"Name\":\"TestUser\",\"Description\":\"Description\",\"Roles\":null}");
                
            AssertJsonResult(await response.Content.ReadAsStringAsync());
        }
        
        [Fact]
        public async Task DoWithRefitUsingStream()
        {
            var api = RestService.For<IUsers>("http://localhost:5000");

            byte[] bytes = Encoding.UTF8.GetBytes("{\"Name\":\"TestUser\",\"Description\":\"Description\",\"Roles\":null}");

            HttpResponseMessage response = await api.Create(new MemoryStream(bytes));
                
            AssertJsonResult(await response.Content.ReadAsStringAsync());
        }
        
        [Fact]
        public async Task DoWithRefitUsingFormSubmit()
        {
            var api = RestService.For<IUsers>("http://localhost:5000");

            HttpResponseMessage response = await api.CreateByFromSubmit(new User {Name = UserName, Description = Description});
                
            AssertJsonResult(await response.Content.ReadAsStringAsync());
        }

        private void AssertJsonResult(string data)
        {
            var expected = "{\"Name\":\"TestUser\",\"Description\":\"Description\",\"Roles\":null}";
            Assert.Equal(expected, data, true);
        }
        
    }
}