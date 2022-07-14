using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.Utilities
    {
        public static class GetContent
    { 
            public static StringContent GetRequestContent(this object obj)
            {
                return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
            }


        }
    }



