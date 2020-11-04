using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BookStore
{
    class ShoppingCart
    {
        private List<IBook> books;
        private decimal priceSumm;
        private decimal deliver;
        private decimal deliverSumm;
        public ShoppingCart()
        {
            deliver = 200;
            books = new List<IBook>();
            priceSumm = 0;
            deliverSumm = 0;
        }
        public decimal GetCheck() => priceSumm + deliver;

        public void AddBook(IBook book)
        {
            books.Add(book);
            priceSumm += book.Price;
            if (book.Type == BookType.paperBook)
                deliverSumm += book.Price;
            if (deliverSumm > 1000)
                deliver = 0;
        }

        public void AddPromo(IPromo promo)
        {
            var find_element = books.Contains(promo.Book);
            if (find_element)
            {
                var PromoSumm = promo.CalculateSale();
                if (promo.PromoType == PromoType.FreeDelivery)
                    deliver = 0;
                else
                    priceSumm -= PromoSumm;
            }
            else
                throw new ArgumentException("Этой книги нет в вашей корзине");
        }

        public void CheckEvent()
        {
            var authorsBook = new Dictionary<string, int>();
            var eBookAuthor = new Dictionary<string, List<IBook>>();
            foreach(var book in books)
            {
                if (book.Type != BookType.paperBook)
                {
                    if (authorsBook[book.Author] > 1)
                    {
                        if (!eBookAuthor.ContainsKey(book.Author))
                        {
                            eBookAuthor[book.Author] = new List<IBook>
                            {
                                book
                            };
                        }
                        else
                            eBookAuthor[book.Author].Add(book);
                    }

                }
                else
                {
                    if (!authorsBook.ContainsKey(book.Author))
                    {
                        authorsBook[book.Author] = 1;
                    }
                    else
                        authorsBook[book.Author] += 1;
                }
            }
            foreach(var author in authorsBook.Keys)
            {
                if (authorsBook[author] >= 2)
                {
                    Console.WriteLine("Вы купили две бумажные книги автора {0}. в подарок вам полагается электронная книга этого автора", author);
                    priceSumm -= eBookAuthor[author].Min(a => a.Price);
                }
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
