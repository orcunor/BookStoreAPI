using BookStoreAPI.Data;
using BookStoreAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreAPI.Repository
{
    public interface IBookRepository
    {
        // Task<List<BookModel>> GetAllBooksAsync();
        //  Task<BookModel> GetBookByIdAsync(int bookId);
        List<Books> GetAllBooks();
        Books GetBookById(int bookId);
        int AddBook(BookModel bookModel);
        bool UpdateBook(int bookId, BookModel book);

        bool UpdateBookPatch(int bookId, JsonPatchDocument book);

        bool DeleteBook(int bookId);
    }
}
