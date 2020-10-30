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
        eBook
    }

    interface IBook
    {
        BookTitle Title { get; }
        BookType Type { get; }
        decimal Price { get; }
        Author Author { get; }
        int ID { get; }

    }
    enum Author
    {
        GeorgeRaymondRichardMartin,
        JohnRonaldReuelTolkien
    }

    enum BookTitle
    {
        GameOfTrones,
        TheSunAlsoRises,
        MartinEden
    }
    class Book : IBook
    {
        private static int booksID = 0;
        public BookTitle Title { get; private set; }
        public BookType Type { get; private set; }
        public decimal Price { get; private set; }
        public Author Author { get; private set; }
        public int ID { get; private set; }
        public Book(BookTitle name, BookType type, decimal price, Author author)
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
