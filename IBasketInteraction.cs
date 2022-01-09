namespace Shop_cousework.Interfaces
{
    interface IBasketInteraction
    {
        void AddGoodsToBasket(Goods goods);
        void RemoveGoodsFromBasket(int identifier);

        bool IsBasketEmpty();

        bool IsGoodsByIdentifier(int identifier);

        string ViewBasket();
    }
}
