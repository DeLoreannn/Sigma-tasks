using System.Collections.Generic;

namespace Shop_cousework
{
    class OrderBase
    {
        private static OrderBase instance;

        private List<Order> orders;

        public List<Order> Orders
        {
            get { return orders; }
            set { orders = value; }
        }

        private OrderBase()
        {
            orders = new List<Order>();
        }

        public static OrderBase GetInstance()
        {
            if (instance == null)
            {
                instance = new OrderBase();
            }
            return instance;
        }

        public override string ToString()
        {
            string result = "Information about placed orders:\n";
            for (int i = 0; i < Orders.Count; ++i)
            {
                result += $"{i + 1}) {Orders[i].ToString()}" + "\n";
            }
            return result;
        }
    }
}
