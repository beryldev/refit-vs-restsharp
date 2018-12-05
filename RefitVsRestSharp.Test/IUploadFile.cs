using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;

namespace RefitVsRestSharp.Test
{
    interface IUploadFile
    {
        [Multipart]
        [Post("/files/upload")]
        Task<HttpResponseMessage> Upload(byte[] file);

        [Multipart]
        [Post("/files/upload")]
        Task<HttpResponseMessage> Upload(Stream file);

        [Multipart]
        [Post("/files/upload")]
        Task<HttpResponseMessage> Upload(FileInfo file);

        [Multipart]
        [Post("/files/upload")]
        Task<HttpResponseMessage> Upload(ByteArrayPart file);
        
        [Multipart]
        [Post("/files/upload")]
        Task<HttpResponseMessage> Upload(StreamPart file);

        [Multipart]
        [Post("/files/upload")]
        Task<HttpResponseMessage> Upload(FileInfoPart file);
    }
}