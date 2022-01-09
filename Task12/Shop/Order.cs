using System;
using System.Collections.Generic;

namespace Shop_cousework
{
    enum DeliveryMethod { NewPost, UkrPost, Courier, SelfPickup}
    class Order
    {
        private Dictionary<int, Goods> goods;

        private double totalAmount;

        private Address address;

        private DeliveryMethod deliveryMethod;

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
                        throw new ArgumentException("Incorrect total amount of goods");
                    totalAmount = value;
                }
                catch(ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public Address Address
        {
            get { return address; }
            set { address = value; }
        }

        public DeliveryMethod DeliveryMethod
        {
            get { return deliveryMethod; }
            set { deliveryMethod = value; }
        }

        public Order()
        {
            goods = new Dictionary<int, Goods>();
            totalAmount = 0;
            address = null;
            deliveryMethod = DeliveryMethod.NewPost;
        }

        public override string ToString()
        {
            string result = "Information about goods:\n";
            foreach (KeyValuePair<int, Goods> item in goods)
            {
                result += item.Value.ToString() + "\n";
            }
            result += "Address: " + address.ToString() + "\n";
            result += "Delivery method: " + deliveryMethod + "\n";
            result += "Total amount: " + totalAmount + "\n\n";
            return result;
        }
    }
}
