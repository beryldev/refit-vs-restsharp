using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Refit;
using RestSharp;
using Xunit;

namespace RefitVsRestSharp.Test
{
    public class UploadFile
    {
        private const string FileContent = "File Content";
        
        [Fact]
        public async Task DoWithRestSharp()
        {
            var client = new RestClient("http://localhost:5000");
            var request = new RestRequest("files/upload", Method.POST);
            byte[] bytes = Encoding.UTF8.GetBytes(FileContent);
            request.AddFile("file", bytes, "filename");

            IRestResponse<string> response = await client.ExecuteTaskAsync<string>(request);

            string responseContent = response.Data;
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(FileContent, responseContent);
        }
        //// https://github.com/restsharp/RestSharp/issues/1209
        //// https://github.com/restsharp/RestSharp/commit/25beb00bf611d58dabdb3e0235d88ff8185fe18b

        [Fact]
        public async Task DoWithRefit()
        {
            var api = RestService.For<IUploadFile>("http://localhost:5000");

            var stream = new MemoryStream(Encoding.UTF8.GetBytes(FileContent));
            HttpResponseMessage response = await api.Upload(new StreamPart(stream, "filename.txt"));

            string responseContent = await response.Content.ReadAsStringAsync();
            Assert.Equal(FileContent, responseContent);
        }
    }
}