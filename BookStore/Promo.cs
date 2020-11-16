using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore
{

    enum PromoType
    {
        FreeDelivery,
        MoneySale,
        SalePromoPercent,
        FreeBook
    }
    interface IPromo
    {
        PromoType PromoType { get; }
        string Code { get; }
        public IBook Book { get; }
        decimal ActivatePromo(ShoppingCart shoppingCart);
    }

    class Promo : IPromo
    {
        public PromoType PromoType { get;  private set; }
        public string Code { get; private set; }
        private decimal Sale { get; set; }
        public IBook Book { get; private set; }

        private PromoType GetRpomoType(string code)
        {
            // логика получения типа промокода из бд по коду (return заглушечный, чтобы глаза не мазолила ошибка)
            return PromoType.FreeBook;
        }
        /// <summary>
        /// Инициализация класса промокода
        /// </summary>
        /// <param name="code">промокод</param>
        /// <param name="sale">размер скидки(число или процент, в зависимости от типа скидки). Скидка в процентах должна быть указана в диапазоне от 0 до 1</param>
        /// <param name="book"> книга, связанная с промокодом. по умолчанию null так как бесплатная доставка не зависит от книги</param>
        public Promo(string code, decimal sale, IBook book=null)
        {
            Code = code;
            Sale = sale;
            PromoType = GetRpomoType(Code);
            if (PromoType == PromoType.SalePromoPercent && Sale >= 1)
                throw new ArgumentException("скидка должна быть указана в процентах от 0 до 1");
            Book = book;
        }
        public decimal CalculateSale()
        {
            return PromoType switch
            {
                PromoType.FreeDelivery => 200,
                PromoType.MoneySale => Sale,
                PromoType.SalePromoPercent => Sale,
                PromoType.FreeBook => Book.Price,
                _ => 0,
            };
        }

        public decimal ActivatePromo(ShoppingCart shoppingCart)
        {
            decimal result = 0;
            var find_element = shoppingCart.Books.Contains(Book);
            if (find_element)
            {
                switch (PromoType)
                {
                    case PromoType.FreeDelivery:
                        if (shoppingCart.Deliver != 0)
                            result += 200;
                        break;
                    case PromoType.MoneySale:
                        result += Sale;
                        break;
                    case PromoType.SalePromoPercent:
                        result += shoppingCart.ShoppingCartPrice * Sale;
                        break;
                    case PromoType.FreeBook:
                        result += Book.Price;
                        break;
                }
                return result;
            }
            else
                return result;
        }
    }
}
