using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore
{
    interface IPromo
    {
        string Code { get; }
        decimal ActivatePromo(ShoppingCart shoppingCart);
    }

    class FreeDelivery : IPromo
    {
        public string Code { get; private set; }
        private decimal Sale { get; set; }
        public FreeDelivery( string code, decimal sale=200)
        {
            Code = code;
            Sale = sale;
        }

        public decimal ActivatePromo(ShoppingCart shoppingCart) => shoppingCart.Deliver != 0 ? Sale : 0;
    }

    class MoneySale : IPromo
    {
        public string Code { get; private set; }
        private decimal Sale { get; set; }
        public MoneySale(string code, decimal sale)
        {
            Code = code;
            Sale = sale;
        }

        public decimal ActivatePromo(ShoppingCart shoppingCart) => Sale;

    }

    class SalePromoPercent : IPromo
    {
        public string Code { get; private set; }
        private decimal Sale { get; set; }
        public SalePromoPercent(string code, decimal sale)
        {
            Code = code;
            if (Sale >= 1)
                throw new ArgumentException("скидка должна быть указана в процентах от 0 до 1");
            Sale = sale;
        }
        public decimal ActivatePromo(ShoppingCart shoppingCart) => shoppingCart.ShoppingCartPrice * Sale;
    }

    class FreeBook : IPromo
    {
        public string Code { get; private set; }
        private IBook Book;
        public FreeBook(string code, IBook book)
        {
            Code = code;
            Book = book;
        }
        public decimal ActivatePromo(ShoppingCart shoppingCart) => Book.Price;
    }
}
