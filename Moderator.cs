using System;
using Shop_cousework.Interfaces;

namespace Shop_cousework
{
    class Moderator : User, ICreatingPromotion, IPromotionInteraction, IOrderInteraction, IViewShop
    {
        public event LogIntoFile OnLogToFile;
        public Moderator() : base()
        {
            OnLogToFile += StorageEvents.LogDataInFile;
        }

        public Moderator(string login, string password)
        {
            Login = login;
            Password = password;
            OnLogToFile += StorageEvents.LogDataInFile;
        }

        public override User LogIn(string userData)
        {
            string[] specificUserData = userData.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            try
            {
                if (specificUserData.Length != 3)
                    throw new ArgumentException($"Incorrect number of values in tape: {userData}");
                if (specificUserData[1].Length <= 1 || specificUserData[2].Length <= 1)
                    throw new ArgumentException("Incorrect number of symbols");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return new Moderator(specificUserData[1], specificUserData[2]);
        }

        public void CreatePromotion(Promotion promotion, Type type)
        {
            if (!PromotionBase.GetInstance().Promotions.ContainsKey(type))
            {
                OnLogToFile?.Invoke($"Created promotion with type: {type}\n", @"./Promotions.txt");
                PromotionBase.GetInstance().Promotions.Add(type, promotion);
            }
            else
            {
                OnLogToFile?.Invoke($"Promotion base already has promotion with the same type: {type}", @"./output.txt");
            }
        }

        public void DeletePromotion(Type type)
        {
            if (PromotionBase.GetInstance().Promotions.ContainsKey(type))
            {
                OnLogToFile?.Invoke($"Deleted promotion with type: {type}\n", @"./Promotions.txt");
                if (PromotionBase.GetInstance().Promotions[type].Goods.Count > 0)
                {
                    for (int i = 0, j = 0; i < PromotionBase.GetInstance().Promotions[type].Goods.Count; ++i)
                    {
                        RemoveGoodsFromPromotion(type, PromotionBase.GetInstance().Promotions[type].Goods[j]);
                    }
                }
                PromotionBase.GetInstance().Promotions.Remove(type);
            }
            else
            {
                OnLogToFile?.Invoke($"Promotion base has not promotion with type: {type}", @"./output.txt");
            }
        }

        public void AddGoodsToPromotion(Type type, Goods goods, int percentage)
        {
            if (PromotionBase.GetInstance().Promotions.ContainsKey(type))
            {
                OnLogToFile?.Invoke($"Goods was successfully added:\n{goods.ToString()}\n", @"./Promotions.txt");
                PromotionBase.GetInstance().AddGoodsToPromotion(goods, type, percentage);
            }
            else
            {
                OnLogToFile?.Invoke($"Promotion base has not promotion with type: {type}", @"./output.txt");
            }
        }

        public void RemoveGoodsFromPromotion(Type type, Goods goods)
        {
            if (PromotionBase.GetInstance().Promotions.ContainsKey(type))
            {
                OnLogToFile?.Invoke($"Goods was successfully deleted:\n{goods.ToString()}\n", @"./Promotions.txt");
                PromotionBase.GetInstance().RemoveGoodsFromPromotion(goods, type);
            }
            else
            {
                OnLogToFile?.Invoke($"Promotion base has not promotion with type: {type}", @"./output.txt");
            }
        }

        public string ViewPromotions()
        {
            return PromotionBase.GetInstance().ToString();
        }

        public string ViewPlacedOrders()
        {
            return OrderBase.GetInstance().ToString();
        }

        public string ViewShop()
        {
            return Shop.GetInstance().ViewStorageContent();
        }

        public void DeletePlacedOrder(int orderNumber)
        {
            OnLogToFile?.Invoke($"Deleted order:\n{OrderBase.GetInstance().Orders[orderNumber - 1].ToString()}", @"./Deleted orders.txt");
            OrderBase.GetInstance().Orders.Remove(OrderBase.GetInstance().Orders[orderNumber - 1]);
        }

        public override string ToString()
        {
            return string.Format("{0,-17}{1,-15}{2,-15}", "Moderator", Login, Password);
        }
    }
}
