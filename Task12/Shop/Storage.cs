using System;
using System.Collections.Generic;
using System.Collections;

namespace Shop_cousework
{
    delegate void LogExpiredProducts(List<Goods> list, string path);
    class Storage : IEnumerable
    {
        public event LogIntoFile OnLogIncorrectInfoToFile;
        public event LogExpiredProducts OnLogExpiredProducts;
        private List<Goods> goods;

        public List<Goods> Goods
        {
            get { return goods; }
            set { goods = value; }
        }

        public Storage()
        {
            OnLogExpiredProducts += StorageEvents.LogExpiredProducts;
            OnLogIncorrectInfoToFile += StorageEvents.LogDataInFile;
            goods = new List<Goods>();
        }
        public Storage(List<Goods> goods)
        {
            this.goods = goods;
        }

        public void ChangePrice(int percentage)
        {
            for (int i = 0; i < goods.Count; ++i)
            {
                goods[i].ChangePrice(percentage);
            }
        }
        public Goods this[int i]
        {
            get 
            {
                try
                {
                    if (i < 0 || i >= goods.Count)
                        throw new ArgumentException("Index out of range");
                    return goods[i];
                }
                catch(ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return goods[i]; 
            }
            set 
            {
                try
                {
                    if (i < 0 || i >= goods.Count)
                        throw new ArgumentException("Index out of range");
                    goods[i] = value;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void AddGoodsToStorage(Goods goods)
        {
            Goods.Add(goods);
        }

        public void ExcludeGoods(int identifier)
        {
            bool foundProduct = false;
            for (int i = 0; i < goods.Count; ++i)
            {
                if (identifier == goods[i].Identifier)
                {
                    foundProduct = true;
                    goods.Remove(goods[i]);
                }
            }
            if (foundProduct == false)
            {
                OnLogIncorrectInfoToFile?.Invoke($"There is no goods to exclude with this identifier: {identifier}", @"./output.txt");
            }
        }

        public List<Goods> SearchGoods(string attribute)
        {
            string[] allGoodsData = new string[goods.Count];
            List<Goods> foundGoods = new List<Goods>();
            for (int i = 0; i < goods.Count; ++i)
            {
                allGoodsData[i] = goods[i].ToString();
            }
            string[] goodsData;
            for (int i = 0; i < allGoodsData.Length; ++i)
            {
                goodsData = allGoodsData[i].Split(' ');
                for (int j = 0; j < goodsData.Length; ++j)
                {
                    if (attribute == goodsData[j])
                    {
                        foundGoods.Add(goods[i]);
                    }
                }
            }
            if (foundGoods.Count > 0)
            {
                return foundGoods;
            }
            else
            {
                OnLogIncorrectInfoToFile?.Invoke($"No goods was found for this attribute: {attribute}", @"./output.txt");
                return null;
            }

        }

        public override string ToString()
        {
            string information = "";
            DateTime now = DateTime.Now;
            DateTime expirationDate;
            List<Goods> expiredProducts = new List<Goods>();
            for (int i = 0; i < goods.Count;)
            {
                if (goods[i] is Product)
                {
                    expirationDate = DateTime.Parse(goods[i].CreationDate);
                    expirationDate = expirationDate.AddDays((goods[i] as Product).ExpirationDate);
                    if ((expirationDate.CompareTo(now)) < 0)
                    {
                        expiredProducts.Add(goods[i]);
                        this.ExcludeGoods(goods[i].Identifier);
                    }
                    else
                    {
                        information += goods[i].ToString() + "\n";
                        ++i;
                    }
                }
                else
                {
                    information += goods[i].ToString() + "\n";
                    ++i;
                }
            }
            OnLogExpiredProducts?.Invoke(expiredProducts, @"./expired products.txt");
            return information;
        }

        public IEnumerator GetEnumerator()
        {
            return goods.GetEnumerator();
        }
    }
}
