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
        private List<IPromo> codes;
        private decimal priceSumm;
        private decimal deliver;
        bool checkCalculation;

        public List<IBook> Books { get => books; }
        public decimal ShoppingCartPrice { get => priceSumm; }
        public decimal Deliver { get => deliver; }

        public ShoppingCart()
        {
            deliver = 200;
            codes = new List<IPromo>();
            books = new List<IBook>();
            priceSumm = 0;
            checkCalculation = false;
        }
        public decimal GetCheck() 
        {
            if (!checkCalculation)
            {
                priceSumm = 0;
                decimal deliverSumm = 0;
                foreach (var book in books)
                {
                    priceSumm += book.Price;
                    if (book.Type == BookType.paperBook)
                        deliverSumm += book.Price;
                }
                if (deliverSumm > 1000)
                    deliver = 0;
                // Куплены только электронные книги. Доставка не нужна.
                if (deliverSumm == 0)
                    deliver = 0;
                CheckEvent();
                foreach (var promo in codes)
                     priceSumm -= promo.ActivatePromo(this);
                checkCalculation = true;
            }
            if (priceSumm < 0)
                priceSumm = 0;
            return priceSumm + deliver;

        }

        public void AddBook(IBook book)
        {
            books.Add(book);
            checkCalculation = false; // При добавлении новой книги нужно пересчитывать сумму корзины
        }

        public void AddPromo(IPromo promo) => codes.Add(promo);

        public void CheckEvent()
        {
            priceSumm -= Events.FreeEBook(books);
            priceSumm -= Events.FreeEBookWithAudiobook(books);
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
