namespace BusinessLayer.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using CommonLayer.Models;
    using Microsoft.AspNetCore.Http;

    
    public interface IBookBL
    {
        
        public AddBookModel AddBook(AddBookModel book);

       
        public BookModel UpdateBook(BookModel book);

        
        public bool DeleteBook(int bookId);

      
        public BookModel GetBookByBookId(int bookId);

      
        public List<BookModel> GetAllBooks();
    }
}