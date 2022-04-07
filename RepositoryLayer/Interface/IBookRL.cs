namespace RepoLayer.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using CommonLayer.Models;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    ///  Interface class
    /// </summary>
    public interface IBookRL
    {
        
        public AddBookModel AddBook(AddBookModel book);

        
        public BookModel UpdateBook(BookModel book);

        
        public bool DeleteBook(int bookId);

        
        public BookModel GetBookByBookId(int bookId);

        
        public List<BookModel> GetAllBooks();
    }
}