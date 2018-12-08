using System.Threading.Tasks;
using Refit;

namespace RefitVsRestSharp.Test.Apis
{
    public interface IResources
    {
        [Headers("x-test: header value")]
        [Get("/api/resources")]
        Task<string> Get();

        [Get("/api/resources")]
        Task<string> Get([Header("x-test")] string header);
    }
}