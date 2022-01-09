using System.Collections.Generic;

namespace Shop_cousework
{
    delegate void UpdateStorage(Goods goods, Type type);
    class PromotionBase
    {
        public event UpdateStorage OnUpdateStorage;

        private static PromotionBase instance;

        private Dictionary<Type, Promotion> promotions;

        public Dictionary<Type, Promotion> Promotions
        {
            get { return promotions; }
            set { promotions = value; }
        }

        private PromotionBase()
        {
            promotions = new Dictionary<Type, Promotion>();
            OnUpdateStorage += StorageEvents.UpdateStorage;
        }

        public static PromotionBase GetInstance()
        {
            if (instance == null)
            {
                instance = new PromotionBase();
            }
            return instance;
        }

        public void AddGoodsToPromotion(Goods goods, Type type, int percentage)
        {
            promotions[type].OldGoods.Add(goods.Copy());
            goods.ChangePrice(percentage);
            promotions[type].Goods.Add(goods);
            OnUpdateStorage?.Invoke(goods, type);
        }

        public void RemoveGoodsFromPromotion(Goods goods, Type type)
        {
            Goods old_goods = promotions[type].OldGoods.Find(item => item.Identifier == goods.Identifier);
            promotions[type].Goods.Remove(goods);
            OnUpdateStorage?.Invoke(old_goods, type);
        }

        public override string ToString()
        {
            string result = "Information about promotions:\n";
            foreach (KeyValuePair<Type, Promotion> item in Promotions)
            {
                result += $"Type:{item.Key}\n{item.Value.ToString()}\n";
            }
            return result;
        }
    }
}
