using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5_pppi
{
    internal class Url
    {
        public string raw { get; set; }
        public string full { get; set; }
        public string regular { get; set; }
        public string small { get; set; }
        public string thumb { get; set; }
        public void printData()
        {
            Console.WriteLine($"= URLS:");
            Console.WriteLine($"= \tRAW: {this.raw}");
            Console.WriteLine($"= \tFULL: {this.full}");
            Console.WriteLine($"= \tREGULAR: {this.regular}");
            Console.WriteLine($"= \tSMALL: {this.small}");
            Console.WriteLine($"= \tTHUMB: {this.thumb}");
        }
    }
}
