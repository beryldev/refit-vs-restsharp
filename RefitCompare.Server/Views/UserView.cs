using System.Collections.Generic;

namespace RefitCompare.Server.Views
{
    public class UserView
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> Roles { get; set; }    //// RestSharp deserialization fails when array
    }
}