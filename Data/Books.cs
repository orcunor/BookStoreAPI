using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreAPI.Data
{
    public class Books
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public DateTime Year { get; set; }
    }
}
