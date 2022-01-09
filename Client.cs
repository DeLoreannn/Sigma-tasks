using System;
using System.Collections.Generic;
using Shop_cousework.Interfaces;

namespace Shop_cousework
{
    class Client : User, IMakeOrder, IBasketInteraction, IViewChecks, IViewShop
    {
        private string name;
        private string surname;
        private bool vipStatus;
        private string phoneNumber;
        private List<Check> checkList;
        private Basket basket;
        public class Basket
        {
            private double totalAmount;
            private Dictionary<int, Goods> goods;

            public Dictionary<int, Goods> Goods
            {
                get { return goods; }
                set { goods = value; }
            }

            public double TotalAmount
            {
                get { return totalAmount; }
                set
                {
                    try
                    {
                        if (value < 0)
                            throw new ArgumentException("Incorrect total amount");
                        totalAmount = value;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            public Basket()
            {
                totalAmount = 0;
                goods = new Dictionary<int, Goods>();
            }

            public override string ToString()
            {
                string result = "";
                foreach (KeyValuePair<int, Goods> item in goods)
                {
                    result += item.Value.ToString() + "\n";
                }
                result += $"Total amount: {TotalAmount}";
                return result;
            }
        }

        #region Properties
        public string Name
        {
            get { return name; }
            set
            {
                try
                {
                    if (value == "")
                        throw new ArgumentException("Empty name");
                    for (int i = 0; i < value.Length; ++i)
                    {
                        if (!Char.IsLetter(value[i]))
                            throw new ArgumentException("Name should consist of only letters");
                    }
                    if (!Char.IsUpper(value[0]))
                        throw new ArgumentException("Name should start with capital letter");
                    name = value;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public string Surname
        {
            get { return surname; }
            set
            {
                try
                {
                    if (value == "")
                        throw new ArgumentException("Empty surname");
                    for (int i = 0; i < value.Length; ++i)
                    {
                        if (!Char.IsLetter(value[i]))
                            throw new ArgumentException("Surname should consist of only letters");
                    }
                    if (!Char.IsUpper(value[0]))
                        throw new ArgumentException("Surname should start with capital letter");
                    surname = value;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public bool VipStatus
        {
            get { return vipStatus; }
            set { vipStatus = value; }
        }

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                try
                {
                    if (value == "")
                        throw new ArgumentException("Empty phone number");
                    if (value.Length < 5 || value.Length > 10)
                        throw new ArgumentException("Incorrect length of phone number");
                    for (int i = 0; i < value.Length; ++i)
                    {
                        if (!Char.IsDigit(value[i]))
                            throw new ArgumentException("Phone number should consist of only numbers");
                    }
                    phoneNumber = value;
                }
                catch(ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public List<Check> CheckList
        {
            get { return checkList; }
            set { checkList = value; }
        }

        public Basket _Basket
        {
            get { return basket; }
        }

        #endregion

        public Client() : base()
        {
            name = null;
            surname = null;
            vipStatus = false;
            phoneNumber = null;
            checkList = new List<Check>();
            basket = new Basket();
            basket.Goods = new Dictionary<int, Goods>();
        }

        public Client(string login, string password, string name, string surname, bool vipStatus, string phoneNumber)
        {
            this.Login = login;
            this.Password = password;
            this.name = name;
            this.surname = surname;
            this.vipStatus = vipStatus;
            this.phoneNumber = phoneNumber;
            this.CheckList = new List<Check>();
            this.basket = new Basket();
        }

        public override User LogIn(string userData)
        {
            string[] specificUserData = userData.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            try
            {
                if (specificUserData.Length != 7)
                    throw new ArgumentException($"Incorrect number of values in tape: {userData}");
                if (specificUserData[1].Length <= 1 || specificUserData[2].Length <= 1 || specificUserData[3].Length <= 1 || specificUserData[4].Length <= 1 ||
                    specificUserData[5].Length < 4 || specificUserData[5].Length > 5)
                    throw new ArgumentException("Incorrect number of symbols");
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return new Client(specificUserData[1], specificUserData[2], specificUserData[3], specificUserData[4], bool.Parse(specificUserData[5]), specificUserData[6]);
        }

        public void AddGoodsToBasket(Goods goods)
        {
            basket.Goods.Add(goods.Identifier, goods);
            basket.TotalAmount += basket.Goods[goods.Identifier].Price;
        }

        public void RemoveGoodsFromBasket(int identifier)
        {
            basket.TotalAmount -= basket.Goods[identifier].Price;
            Shop.GetInstance().ReturnGoods(basket.Goods[identifier]);
            basket.Goods.Remove(identifier);
        }

        public bool IsBasketEmpty()
        {
            if (basket.Goods.Count == 0)
                return true;
            return false;
        }

        public bool IsGoodsByIdentifier(int identifier)
        {
            return basket.Goods.ContainsKey(identifier);
        }

        public string ViewBasket()
        {
            return basket.ToString();
        }

        public void MakeOrder(Order order)
        {

            if (vipStatus == false)
            {
                switch (order.DeliveryMethod)
                {
                    case DeliveryMethod.NewPost:
                        order.TotalAmount += 70;
                        break;
                    case DeliveryMethod.UkrPost:
                        order.TotalAmount += 30;
                        break;
                    case DeliveryMethod.Courier:
                        order.TotalAmount += 100;
                        break;
                }
            }
            checkList.Add(Shop.GetInstance().ConfirmateOrder(order, order.Goods));
            foreach (KeyValuePair<int, Goods> item in basket.Goods)
            {
                basket.Goods.Remove(item.Key);
            }
        }

        public string ViewChecks()
        {
            if (checkList.Count == 0)
            {
                return "No orders have been placed\n";
            }
            string result = "";
            for (int i = 0; i < checkList.Count; ++i)
            {
                result += checkList[i].ToString();
            }
            return result;
        }

        public string ViewShop()
        {
            return Shop.GetInstance().ViewStorageContent();
        }

        public override string ToString()
        {
            return string.Format("{0,-17}{1,-15}{2,-15}{3,-15}{4,-15}{5,-15}{6,-15}", "Client", Login, Password, Name, Surname, VipStatus, PhoneNumber);
        }
    }
}
