using System.Collections.Generic;

namespace Shop_cousework.Interfaces
{
    interface IRepresentation
    {
        User LogIn();
        void ViewShop(IViewShop user);
        void AddGoodsToBasket(IBasketInteraction customer);
        void RemoveGoodsFromBasket(IBasketInteraction customer);
        void ViewBasket(IBasketInteraction customer);
        void MakeOrder(IMakeOrder customer, Dictionary<int, Goods> goods, double totalAmount);
        void ViewChecks(IViewChecks user);
        void CreatePromotion(ICreatingPromotion moderator);
        void DeletePromotion(ICreatingPromotion moderator);
        void ViewPromotions(ICreatingPromotion moderator);
        void AddGoodsToPromotion(IPromotionInteraction moderator);
        void RemoveGoodsFromPromotion(IPromotionInteraction moderator);
        void ViewPlacedOrders(IOrderInteraction moderator);
        void DeletePlacedOrders(IOrderInteraction moderator);
        void AddGoodsToStorage(IGoodsManagement administrator, string shopContent);
        void RemoveGoodsFromStorage(IGoodsManagement administrator, string shopContent);
        void SetClientStatus(IClientStatus administrator);
        Client SignUp(ISignUp guest);
        void EnterEmail(IEnterEmail guest);
        Goods ChooseGoods(Guest guest);

        void RepresentShop();
    }
}
