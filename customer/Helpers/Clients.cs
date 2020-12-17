using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CustomerServices.Helpers
{
    public class Clients
    {

        public HttpClient AccountDetails()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44338");
            return client;
        }

    
    }
}
