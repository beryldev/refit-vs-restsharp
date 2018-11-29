using System.Threading.Tasks;
using Refit;
using RefitCompare.Server.Views;

namespace RefitCompare.Test
{
    public partial class SimpleGetRequest
    {
        public interface IUsers
        {
            [Get("/api/users/{username}")]
            Task<UserView> GetUser(string username);
        }
    }
}