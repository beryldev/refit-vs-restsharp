using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;
using RefitVsRestSharp.Server.Views;

namespace RefitVsRestSharp.Test.Apis
{
    interface IUsers
    {
        [Get("/api/users/{username}")]
        Task<User> Get(string username);
        
        [Get("/api/users")]
        Task<IEnumerable<User>> Search(string role);

        
        [Post("/api/users")]
        Task<HttpResponseMessage> Create([Body] User user);

        [Post("/api/users")]
        Task<HttpResponseMessage> Create([Body]string data);
        
        [Post("/api/users")]
        Task<HttpResponseMessage> Create([Body]Stream data);

        [Post("/users/new")]
        Task<HttpResponseMessage> CreateByFromSubmit([Body(BodySerializationMethod.UrlEncoded)] User user);
    }
}