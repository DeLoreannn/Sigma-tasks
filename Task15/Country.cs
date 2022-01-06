using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task15.Models
{
    public class Country
    {
        public Country()
        {
            Cities = new HashSet<City>();
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<City> Cities { get; set; }
    }
}
