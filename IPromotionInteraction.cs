namespace Shop_cousework.Interfaces
{
    interface IPromotionInteraction
    {
        void AddGoodsToPromotion(Type type, Goods goods, int percentage);

        void RemoveGoodsFromPromotion(Type type, Goods goods);
    }
}
