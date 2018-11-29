using Refit;
using RefitCompare.Server.Views;
using RestSharp;
using Xunit;

namespace RefitCompare.Test
{
    public partial class SimpleGetRequest
    {
        [Fact]
        public async void DoWithRefit()
        {
            var api = RestService.For<IUsers>("http://localhost:5000");

            UserView user = await api.GetUser("TestUser");
            
            Assert.NotNull(user);
            Assert.Equal("TestUser", user.Name);
        }

        [Fact]
        public async void DoWithRestSharp()
        {
            var client = new RestClient("http://localhost:5000");
            var request = new RestRequest("api/users/{username}", Method.GET);
            request.AddUrlSegment("username", "TestUser");

            IRestResponse<UserView> response = await client.ExecuteTaskAsync<UserView>(request);
            UserView user = response.Data;
            
            Assert.NotNull(user);
            Assert.Equal("TestUser", user.Name);
        }
    }
}