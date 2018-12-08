using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Refit;
using RefitVsRestSharp.Server.Views;
using RefitVsRestSharp.Test.Apis;
using RestSharp;
using Xunit;

namespace RefitVsRestSharp.Test
{
    public class GetWithParameters
    {
        [Fact]
        public async Task DoWithRestSharp()
        {
            var client = new RestClient("http://localhost:5000");
            var request = new RestRequest("api/users");
            request.AddParameter("role", "RoleA");

            IRestResponse<IEnumerable<User>> response = await client.ExecuteTaskAsync<IEnumerable<User>>(request);
            IEnumerable<User> users = response.Data;
            
            AssertResult(users);
        }
        
        [Fact]
        public async Task DoWithRefit()
        {
            var api = RestService.For<IUsers>("http://localhost:5000");
            
            IEnumerable<User> users = await api.Search("RoleA");
            
            AssertResult(users);
        }

        private void AssertResult(IEnumerable<User> result)
        {
            User user = result.FirstOrDefault();
            Assert.NotNull(user);
            Assert.Equal("username", user.Name);
        }
    }
}