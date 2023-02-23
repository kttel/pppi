using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5_pppi
{
    internal class Photo
    {
        public string id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string description { get; set; }
        public string alt_description { get; set; }
        public Url urls { get; set; }
        public int likes { get; set; }
        public User user { get; set; }
    }
}
