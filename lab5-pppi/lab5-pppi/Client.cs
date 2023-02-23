using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5_pppi
{
    internal class Client
    {
        public string client_id { get; set; }
        public string client_secret { get; set;}
        public string redirect_uri { get; set;}
        public string code { get; set;}
        public string grant_type { get; set;}
        public Client(string client_id, string client_secret)
        {
            this.client_id = client_id;
            this.client_secret = client_secret;
            this.grant_type = "authorization_code";
        }
    }
}
