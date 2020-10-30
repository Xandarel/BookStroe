using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore
{
    class ShoppingCart
    {
        private List<IBook> books;
        private decimal priceSumm;
        private decimal deliver;
        public ShoppingCart()
        {
            deliver = 200;
            books = new List<IBook>();
            priceSumm = 0;
        }
        public decimal GetCheck() => priceSumm + deliver;

        public void AddBook(IBook book)
        {
            books.Add(book);
            priceSumm += book.Price;
            if (priceSumm > 1000)
                deliver = 0;
        }

        public void AddPromo(IPromo promo, IBook book)
        {
            var find_element = books.Contains(book);
            if (find_element)
            {
                var PromoSumm = promo.CalculateSale(book);
                if (Enum.IsDefined(typeof(FreeDelivery), promo.Code))
                    deliver -= PromoSumm;
                else
                    priceSumm -= PromoSumm;
            }
            else
                throw new ArgumentException("Этой книги нет в вашей корзине");
        }

        public void CheckEvent()
        {
            var authorsBook = new Dictionary<Author, int>();
            foreach(var book in books)
            {
                if (book.Type != BookType.paperBook)
                    continue;
                if (!authorsBook.ContainsKey(book.Author))
                {
                    authorsBook[book.Author] = 1;
                }
                else
                    authorsBook[book.Author] += 1;
            }
            foreach(var author in authorsBook.Keys)
            {
                if (authorsBook[author] >= 2)
                    Console.WriteLine("Вы купили две бумажные книги автора {0}. в подарок вам полагается электронная книга этого автора", author);
            }
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
