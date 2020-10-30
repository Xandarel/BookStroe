using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore
{
    enum FreeBook
    {
        DRAGON,
        MOTHER
    }
    enum SalePromoPercent
    {
        Tomat,
        Trone
    }

    enum MoneySale
    {
        Zikkurat,
        TOMAT12
    }

    enum FreeDelivery
    {
        ORK,
        KOROVAN
    }
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
        decimal CalculateSale(IBook book);
    }

    class Promo : IPromo
    {
        public PromoType PromoType { get;  private set; }
        public string Code { get; private set; }
        private decimal Sale { get; set; }
        public Promo(SalePromoPercent code, decimal sale)
        {
            Code = code.ToString();
            Sale = sale;
            PromoType = PromoType.SalePromoPercent;
        }
        public Promo(MoneySale code, decimal sale)
        {
            Code = code.ToString();
            Sale = sale;
            PromoType = PromoType.MoneySale;
        }
        public Promo(FreeBook code, decimal sale)
        {
            Code = code.ToString();
            Sale = sale;
            PromoType = PromoType.FreeBook;
        }
        public Promo(FreeDelivery code, decimal sale)
        {
            Code = code.ToString();
            Sale = sale;
            PromoType = PromoType.FreeDelivery;
        }
        public decimal CalculateSale(IBook book)
        {
            switch ((int)PromoType)
            {
                case 0:
                    return 200;
                case 1:
                    return Sale;
                case 2:
                    return book.Price * Sale;
                case 3:
                    return book.Price;
                default:
                    return 0;
            }
        }
    }
}
