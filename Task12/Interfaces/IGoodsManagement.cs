namespace Shop_cousework.Interfaces
{
    interface IGoodsManagement
    {
        void AddGoodsToStorage(IAbstractCreator abstractCreator, Type typeOfGoodsInStorage, Goods goods);

        void RemoveGoodsFromStorage(int identifier);
    }
}
