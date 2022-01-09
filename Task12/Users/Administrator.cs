using System;
using Shop_cousework.Interfaces;
using Shop_cousework.Bases;

namespace Shop_cousework
{
    class Administrator : User, IGoodsManagement, IClientStatus, IViewChecks, IViewShop
    {
        public event LogIntoFile OnLogToFile;
        public Administrator() : base()
        {
            OnLogToFile += StorageEvents.LogDataInFile;
        }

        public Administrator(string login, string password)
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
            return new Administrator(specificUserData[1], specificUserData[2]);
        }

        public void AddGoodsToStorage(IAbstractCreator abstractCreator, Type typeOfGoodsInStorage, Goods goods)
        {
            Goods _goods = abstractCreator.CreateGoods();
            _goods = goods;
            StorageBase.GetInstance().Storages[typeOfGoodsInStorage].AddGoodsToStorage(_goods);
        }

        public void RemoveGoodsFromStorage(int identifier)
        {
            Type type;
            Goods goods;
            foreach (var item in StorageBase.GetInstance().Storages)
            {
                for (int i = 0; i < item.Value.Goods.Count; ++i)
                {
                    if (item.Value.Goods[i].Identifier == identifier)
                    {
                        type = item.Key;
                        goods = item.Value.Goods[i];
                        OnLogToFile?.Invoke($"Deleted goods: {goods.ToString()}", @"./Deleted goods.txt");
                        StorageBase.GetInstance().Storages[type].Goods.Remove(goods);
                        break;
                    }
                }
            }
        }

        public void SetClientStatus(string login, bool status)
        {
            ClientBase.GetIntsance().Clients.Find(x => x.Login == login).VipStatus = status;
        }

        public string ViewChecks()
        {
            return CheckBase.GetInstance().ToString();
        }

        public override string ToString()
        {
            return string.Format("{0,-17}{1,-15}{2,-15}", "Administrator", Login, Password);
        }

        public string ViewShop()
        {
            return Shop.GetInstance().ViewStorageContent();
        }
    }
}
