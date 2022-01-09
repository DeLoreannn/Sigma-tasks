using System.Collections.Generic;

namespace Shop_cousework
{
    class Promotion
    {
        private List<Goods> goods;
        private List<Goods> oldGoods;

        public List<Goods> Goods
        {
            get { return goods; }
            set { goods = value; }
        }

        public List<Goods> OldGoods
        {
            get { return oldGoods; }
            set { oldGoods = value; }
        }

        public Promotion()
        {
            goods = new List<Goods>();
            oldGoods = new List<Goods>();
        }

        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < Goods.Count; ++i)
            {
                result += Goods[i].ToString() + "\n";
            }
            return result;
        }
    }
}
