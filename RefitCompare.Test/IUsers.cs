using System.Threading.Tasks;
using Refit;
using RefitCompare.Server.Views;

namespace RefitCompare.Test
{
    interface IUsers
    {
        [Get("/api/users/{username}")]
        Task<UserView> GetUser(string username);
    }
}