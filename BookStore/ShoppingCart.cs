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
        private List<IPromo> codes;
        private decimal priceSumm;
        private decimal deliver;
        bool checkCalculation;
        private List<IEvent> events;

        public IReadOnlyList<IBook> Books { get => books; }
        public decimal ShoppingCartPrice { get => priceSumm; }
        public decimal Deliver { get => deliver; }

        public ShoppingCart(params IEvent[] @event)
        {
            deliver = 200;
            codes = new List<IPromo>();
            books = new List<IBook>();
            priceSumm = 0;
            checkCalculation = false;
            events = new List<IEvent>();
            events.AddRange(@event);

        }

        private decimal CalculateDeliver() => books.Where(x => x.Type == BookType.paperBook).Sum(x => x.Price);
        public decimal GetCheck()
        {
            if (!checkCalculation)
            {
                priceSumm = 0;
                decimal deliverSumm = CalculateDeliver();
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

        private void CheckEvent()
        {
            var bookWithPromo = new List<IBook>();
            foreach (var ce in events)
                bookWithPromo.AddRange(ce.ActivateEvent(books));
            foreach (var bwp in bookWithPromo.Distinct())
                priceSumm -= bwp.Price;

        }
    }
}
