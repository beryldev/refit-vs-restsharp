using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Refit;
using RefitVsRestSharp.Test.Apis;
using RestSharp;
using Xunit;

namespace RefitVsRestSharp.Test
{
    public sealed class UploadFile
    {
        private const string FileContent = "File Content";
        
        [Fact]
        public async Task DoWithRestSharp()
        {
            var client = new RestClient("http://localhost:5000");
            var request = new RestRequest("files/upload", Method.POST);
            byte[] bytes = Encoding.UTF8.GetBytes(FileContent);
            request.AddFile("file", bytes, "filename.txt");

            IRestResponse<string> response = await client.ExecuteAsync<string>(request);

            string responseContent = response.Data;
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal($"filename.txt:{FileContent}", responseContent);
        }

        [Fact]
        public async Task DoWithRefitUsingByteArray()
        {
            var api = RestService.For<IUploadFile>("http://localhost:5000");

            byte[] bytes = Encoding.UTF8.GetBytes(FileContent);
            HttpResponseMessage response = await api.Upload(bytes);

            string responseContent = await response.Content.ReadAsStringAsync();        
            Assert.Equal($"file:{FileContent}", responseContent);
        }

        [Fact]
        public async Task DoWithRefitUsingStream()
        {
            var api = RestService.For<IUploadFile>("http://localhost:5000");
            
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(FileContent));
            HttpResponseMessage response = await api.Upload(stream);

            string responseContent = await response.Content.ReadAsStringAsync();
            Assert.Equal($"file:{FileContent}", responseContent);   
        }

        [Fact]
        public async Task DoWithRefitUsingFileInfo()
        {
            using (var fileStream = new FileStream("filename.txt", FileMode.Create))
            {
                await fileStream.WriteAsync(Encoding.UTF8.GetBytes(FileContent));
            }
 
            var api = RestService.For<IUploadFile>("http://localhost:5000");
            
            var fileInfo = new FileInfo("filename.txt");
            HttpResponseMessage response = await api.Upload(fileInfo);

            string responseContent = await response.Content.ReadAsStringAsync();
            Assert.Equal($"filename.txt:{FileContent}", responseContent);
        }

        [Fact]
        public async Task DoWithRefitUsingByteArrayPart()
        {
            var api = RestService.For<IUploadFile>("http://localhost:5000");
            
            var bytes = new ByteArrayPart(Encoding.UTF8.GetBytes(FileContent), "filename.txt");
            HttpResponseMessage response = await api.Upload(bytes);

            string responseContent = await response.Content.ReadAsStringAsync();
            Assert.Equal($"filename.txt:{FileContent}", responseContent);
        }
        
        [Fact]
        public async Task DoWithRefitUsingStreamPart()
        {
            var api = RestService.For<IUploadFile>("http://localhost:5000");

            var stream = new MemoryStream(Encoding.UTF8.GetBytes(FileContent));
            HttpResponseMessage response = await api.Upload(new StreamPart(stream, "filename.txt"));

            string responseContent = await response.Content.ReadAsStringAsync();
            Assert.Equal($"filename.txt:{FileContent}", responseContent);
        }

        [Fact]
        public async Task DoWithRefitUsingFileInfoPart()
        {
            using (var fileStream = new FileStream("filename2.txt", FileMode.Create))
            {
                await fileStream.WriteAsync(Encoding.UTF8.GetBytes(FileContent));
            }
            
            var api = RestService.For<IUploadFile>("http://localhost:5000");
            
            var fileInfo = new FileInfoPart(new FileInfo("filename2.txt"), "filename.txt");
            HttpResponseMessage response = await api.Upload(fileInfo);

            string responseContent = await response.Content.ReadAsStringAsync();
            Assert.Equal($"filename.txt:{FileContent}", responseContent);
        }

    }
}