using System.Net.Http;
using System.Threading.Tasks;
using Refit;

namespace RefitVsRestSharp.Test
{
    interface IUploadFile
    {
        [Multipart]
        [Post("/files/upload")]
        Task<HttpResponseMessage> Upload(StreamPart file);
    }
}