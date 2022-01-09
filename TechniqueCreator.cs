using Shop_cousework.Interfaces;

namespace Shop_cousework.Creators
{
    class TechniqueCreator : IAbstractCreator
    {
        public Goods CreateGoods()
        {
            return new Technique();
        }
    }
}
