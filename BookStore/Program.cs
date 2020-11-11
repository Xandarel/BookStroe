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
        private List<IBook> ebooks;
        private List<IBook> paperBook;
        private List<IPromo> codes;
        private decimal priceSumm;
        private decimal deliver;
        private decimal deliverSumm;
        public ShoppingCart()
        {
            deliver = 200;
            codes = new List<IPromo>();
            books = new List<IBook>();
            ebooks = new List<IBook>();
            paperBook = new List<IBook>();
            priceSumm = 0;
            deliverSumm = 0;
        }
        public decimal GetCheck() 
        {
            foreach (var book in books)
            {
                priceSumm += book.Price;
                if (book.Type == BookType.paperBook)
                    deliverSumm += book.Price;
                if (deliverSumm > 1000)
                    deliver = 0;
                // Куплены только электронные книги. Доставка не нужна.
                if (deliverSumm == 0)
                    deliver = 0;
            }
            CheckEvent();
            foreach (var promo in codes)
                ActivatePromo(promo);
            return priceSumm + deliver;
        }

        public void AddBook(IBook book)
        {
            books.Add(book);
            switch (book.Type)
            {
                case BookType.paperBook:
                    paperBook.Add(book);
                    break;
                case BookType.eBook:
                    ebooks.Add(book);
                    break;
            }
        }

        public void AddPromo(IPromo promo)
        {
            codes.Add(promo);
        }
        private void ActivatePromo(IPromo promo)
        {
            try
            {
                var find_element = books.Contains(promo.Book);
                if (find_element)
                    priceSumm -= promo.CalculateSale();
                else
                    throw new ArgumentException("Этой книги нет в вашей корзине");
            }
            catch
            {
                //если find_element выдаст ошибку - значит это промокод на доставку
                deliver = 0;
            }
        }

        public void CheckEvent()
        {
            var authorsBook = new Dictionary<string, int>();
            var eBookAuthor = new Dictionary<string, List<IBook>>();
            foreach (var paper in paperBook )
            {
                if (!authorsBook.ContainsKey(paper.Author))
                    authorsBook[paper.Author] = 1;
                else
                    authorsBook[paper.Author] += 1;
            }
            foreach (var ebook in ebooks)
            {
                if (authorsBook.ContainsKey(ebook.Author) && (authorsBook[ebook.Author] > 1))
                    if (!eBookAuthor.ContainsKey(ebook.Author))
                    {
                        eBookAuthor[ebook.Author] = new List<IBook>
                        {
                            ebook
                        };
                    }
                    else
                        eBookAuthor[ebook.Author].Add(ebook);
            }
            foreach(var author in eBookAuthor.Keys)
            {
                Console.WriteLine("Вы купили две бумажные книги автора {0}. в подарок вам полагается электронная книга этого автора", author);
                priceSumm -= eBookAuthor[author].Min(a => a.Price);
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
