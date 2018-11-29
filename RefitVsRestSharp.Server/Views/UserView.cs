using System.Collections.Generic;

namespace RefitVsRestSharp.Server.Views
{
    public class UserView
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> Roles { get; set; }    //// RestSharp deserialization fails when array
    }
}