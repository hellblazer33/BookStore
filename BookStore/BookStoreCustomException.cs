using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore
{
    public class BookStoreCustomException : Exception
    {
       public enum ExceptionType
        {
            EMPTY_PARAMETER
        }

        private readonly ExceptionType type;
        public BookStoreCustomException(ExceptionType Type,string message) : base(message)
        {
            this.type = Type;
        }
    }
}
