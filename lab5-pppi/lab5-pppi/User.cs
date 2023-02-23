using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5_pppi
{
    internal class User
    {
        public string id { get; set; }
        public DateTime updated_at { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public string bio { get; set; }
        public string portfolio_url { get; set; }
        public string location { get; set; }
        public void printData()
        {
            Console.WriteLine($"= USER:");
            Console.WriteLine($"= \tID: {this.id}");
            Console.WriteLine($"= \tUSERNAME: {this.username}");
            Console.WriteLine($"= \tPORTFOLIO URL: {this.portfolio_url}");
        }
    }
}
