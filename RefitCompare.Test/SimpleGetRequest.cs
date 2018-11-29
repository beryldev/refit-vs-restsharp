using Refit;
using RefitCompare.Server.Views;
using RestSharp;
using Xunit;

namespace RefitCompare.Test
{
    public partial class SimpleGetRequest
    {
        [Fact]
        public async void DoByRefit()
        {
            var api = RestService.For<IUsers>("http://localhost:5000");

            UserView user = await api.GetUser("testuser");
            
            Assert.NotNull(user);
            Assert.Equal("Test user", user.Name);
        }

        [Fact]
        public void DoByRestSharp()
        {
            var client = new RestClient("http://localhost:5000");
            var request = new RestRequest("api/users/{username}", Method.GET);
            request.AddUrlSegment("username", "testuser");

            client.ExecuteAsync<UserView>(request, response =>
            {
                UserView user = response.Data;
            
                Assert.NotNull(user);
                Assert.Equal("Test user", user.Name);
            });
            
        }
    }
}