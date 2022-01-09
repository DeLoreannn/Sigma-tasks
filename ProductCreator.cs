using Shop_cousework.Interfaces;

namespace Shop_cousework.Creators
{
    class ProductCreator : IAbstractCreator
    {
        public Goods CreateGoods()
        {
            return new Product();
        }
    }
}
