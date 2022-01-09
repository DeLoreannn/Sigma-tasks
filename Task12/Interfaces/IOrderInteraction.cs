namespace Shop_cousework.Interfaces
{
    interface IOrderInteraction
    {
        string ViewPlacedOrders();

        void DeletePlacedOrder(int orderNumber);
    }
}
