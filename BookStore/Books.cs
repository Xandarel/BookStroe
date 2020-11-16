using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore
{
    enum BookType
    {
        paperBook,
        eBook,
        audioBook
    }

    interface IBook
    {
        string Title { get; }
        BookType Type { get; }
        decimal Price { get; }
        string Author { get; }
        int ID { get; }

    }

    class Book : IBook
    {
        private static int booksID = 0;
        public string Title { get; private set; }
        public BookType Type { get; private set; }
        public decimal Price { get; private set; }
        public string Author { get; private set; }
        public int ID { get; private set; }
        public Book(string name, BookType type, decimal price, string author)
        {
            ID = booksID;
            booksID++;
            Title = name;
            Type = type;
            Price = price;
            Author = author;
        }
    }
}
