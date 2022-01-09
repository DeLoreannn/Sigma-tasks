using Shop_cousework.Interfaces;

namespace Shop_cousework.Creators
{
    class GoodsCreator : IAbstractCreator
    {
        public Goods CreateGoods()
        {
            return new Goods();
        }
    }
}
