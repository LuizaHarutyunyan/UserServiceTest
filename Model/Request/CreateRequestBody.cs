using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserServiceTest.Model.Request
{
    internal class CreateRequestBody
    {
        [JsonProperty("FirstName")]
        public string firstName { get; set; }

        [JsonProperty("LastName")]
        public string lastName { get; set; }
    }
}
