using System.Threading.Tasks;
using Refit;
using RefitVsRestSharp.Server.Views;
using RestSharp;
using Xunit;

namespace RefitVsRestSharp.Test
{
    public class SimpleGet
    {
        [Fact]
        public async Task DoWithRestSharp()
        {
            var client = new RestClient("http://localhost:5000");
            var request = new RestRequest("api/users/{username}");
            request.AddUrlSegment("username", "TestUser");

            IRestResponse<User> response = await client.ExecuteTaskAsync<User>(request);
            User user = response.Data;
            
            AssertResult(user);
        }
        
        [Fact]
        public async Task DoWithRefit()
        {
            var api = RestService.For<IUsers>("http://localhost:5000");

            User user = await api.Get("TestUser");
            
            AssertResult(user);
        }

        private void AssertResult(User user)
        {
            Assert.NotNull(user);
            Assert.Equal("TestUser", user.Name);
        }
    }
}