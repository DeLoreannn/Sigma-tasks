namespace Shop_cousework.Interfaces
{
    interface ICreatingPromotion
    {
        void CreatePromotion(Promotion promotion, Type type);

        void DeletePromotion(Type type);

        string ViewPromotions();
    }
}
