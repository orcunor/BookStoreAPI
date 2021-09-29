using BookStoreAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreAPI.Data
{
    public class BookStoreContext : IdentityDbContext<ApplicationUser>
    {
        public static List<Books> books = new List<Books>()
        {
           new Books(){Id = 1, Author = "Orçun", Name = "Orçun's Book", Year = DateTime.Now.AddYears(-5) },
           new Books(){Id = 2, Author = "Deniz", Name = "Deniz's Book", Year = DateTime.Now.AddYears(-7) },
           new Books(){Id = 3, Author = "Ahmet", Name = "Ahmet's Book", Year = DateTime.Now.AddYears(-3) },
           new Books(){Id = 4, Author = "Ersen", Name = "Ersen's Book", Year = DateTime.Now.AddYears(-2) },
           new Books(){Id = 5, Author = "Pınar", Name = "Pınar's Book", Year = DateTime.Now.AddYears(-15) }
        };

        public BookStoreContext(DbContextOptions<BookStoreContext> options)
            :base(options)
        {

        }
        public DbSet<Books> Books { get; set; }
    }
}
