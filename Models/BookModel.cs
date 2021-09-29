using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Models
{
    public class BookModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Please add name property")]
        public string Name { get; set; }


        public string Author { get; set; }
        public DateTime Year { get; set; }
    }
}
