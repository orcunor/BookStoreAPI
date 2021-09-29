using AutoMapper;
using BookStoreAPI.Data;
using BookStoreAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreAPI.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context;
        private readonly IMapper mapper;

        public BookRepository(BookStoreContext context, IMapper mapper)
        {
            this._context = context;
            this.mapper = mapper;
        }

        public List<Books> GetAllBooks()
        {
            try
            {
                return BookStoreContext.books;
            }
            catch {}

            return new List<Books>();
        }

        //public async Task<List<BookModel>> GetAllBooksAsync()
        //{
        //    var records = await _context.Books.Select(x => new BookModel()
        //    {
        //        Id = x.Id,
        //        Author = x.Author,
        //        Name = x.Name,
        //        Year = x.Year
        //    }).ToListAsync();

        //    return records;
        //}

        public Books GetBookById(int bookId)
        {
            try
            {
                var book = BookStoreContext.books.Where(x => x.Id == bookId).FirstOrDefault();
                if (book != null)
                    return book;
                

                //var book = BookStoreContext.books.Where(x => x.Id == bookId).FirstOrDefault();
                //return mapper.Map<BookModel>(book);
            }
            catch {}
            
            return new Books();
        }

        //public async Task<BookModel> GetBookByIdAsync(int bookId)
        //{
        //    var record = await _context.Books.Where(x => x.Id == bookId).Select(x => new BookModel()
        //    {
        //        Id = x.Id,
        //        Author = x.Author,
        //        Name = x.Name,
        //        Year = x.Year
        //    }).FirstOrDefaultAsync();

        //    return record;
        //}

        public int AddBook(BookModel bookModel)
        {
            try
            {
                var book = new Books()
                {
                    Id = bookModel.Id,
                    Author = bookModel.Author,
                    Name = bookModel.Name,
                    Year = bookModel.Year
                };

                BookStoreContext.books.Add(book);
                return book.Id;
            }
            catch {}

            return -1;
         
        }

        public bool UpdateBook(int bookId, BookModel bookModel)
        {
            try
            {
                var updatedBook = BookStoreContext.books.Find(x => x.Id == bookId);
                if (updatedBook != null)
                {
                    updatedBook.Id = bookId;
                    updatedBook.Name = bookModel.Name;
                    updatedBook.Author = bookModel.Author;
                    updatedBook.Year = bookModel.Year;
                    return true;
                }
            }
            catch { }
            return false;
         
        }

        public bool UpdateBookPatch(int bookId, JsonPatchDocument bookModel)
        {
            try
            {
                var book = BookStoreContext.books.Find(x => x.Id == bookId);
                if (book != null)
                {
                    bookModel.ApplyTo(book);
                    return true;
                }
            }
            catch{}
            return false;
            
        }

        public bool DeleteBook(int bookId)
        {
            try
            {
                var book = BookStoreContext.books.Where(x => x.Id == bookId).FirstOrDefault();
                if (book != null)
                {
                    BookStoreContext.books.Remove(book);
                    return true;
                }
                
            }
            catch { }

            return false;

        }
    }
}
