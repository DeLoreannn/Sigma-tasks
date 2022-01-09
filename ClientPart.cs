using System;
using System.Collections.Generic;
using Shop_cousework.Interfaces;
using Shop_cousework.AdditionalClasses;

namespace Shop_cousework.Representatives
{
    static class ClientPart
    {
        static public void AddGoodsToBasket(IBasketInteraction customer)
        {
            Console.Clear();
            Console.WriteLine($"{Shop.GetInstance().ViewStorageContent()}");
            while (true)
            {
                Console.WriteLine("Enter identifier of goods:");
                if (int.TryParse(Console.ReadLine(), out int identifier))
                {
                    if (SearchFunctions.SearchGoodsInShopByIdentifier(identifier))
                    {
                        Goods goods = Shop.GetInstance().GetGoods(identifier);
                        customer.AddGoodsToBasket(goods);
                        break;
                    }
                    else
                        Console.WriteLine("Incorrect identifier of goods. Please, try again.");
                }
                else
                    Console.WriteLine("Incorrect identifier of goods. Please, try again.");
            }
            Console.Clear();
        }

        static public void RemoveGoodsFromBasket(IBasketInteraction customer)
        {
            Console.Clear();
            if (customer.IsBasketEmpty())
                Console.WriteLine("Basket is empty. Add some goods to it.\n");
            else
            {
                Console.WriteLine($"Basket content:\n{customer.ViewBasket()}");
                while (true)
                {
                    Console.WriteLine("Enter identifier of goods:");
                    if (int.TryParse(Console.ReadLine(), out int identifier))
                    {
                        if (customer.IsGoodsByIdentifier(identifier))
                        {
                            customer.RemoveGoodsFromBasket(identifier);
                            break;
                        }
                        else
                            Console.WriteLine("Incorrect identifier. Please, try again.");
                    }
                    else
                        Console.WriteLine("Incorrect identifier. Please, try again.");
                }
            }
        }

        static public void ViewBasket(IBasketInteraction customer)
        {
            Console.Clear();
            Console.WriteLine("Basket content:");
            Console.WriteLine(customer.ViewBasket());
            Console.WriteLine();
        }

        static public void MakeOrder(IMakeOrder customer, Dictionary<int, Goods> goods, double totalAmount)
        {
            Console.Clear();
            Country country;
            Town town;
            int postOfficeNumber;
            while (true)
            {
                Console.WriteLine("Enter country for delivery (Ukraine, Poland, Sweden, Germany, France):");
                if (Enum.TryParse(Console.ReadLine(), out Country addCountry))
                {
                    country = addCountry;
                    break;
                }
                Console.WriteLine("Incorrect country. Please, try again.");
            }
            while (true)
            {
                Console.WriteLine("Enter town for delivery (Kyiv, Warsaw, Stockholm, Berlin, Paris):");
                if (Enum.TryParse(Console.ReadLine(), out Town addTown))
                {
                    town = addTown;
                    break;
                }
                Console.WriteLine("Incorrect town. Please, try again.");
            }
            while (true)
            {
                Console.WriteLine("Enter post office number:");
                if (int.TryParse(Console.ReadLine(), out int addPostOfficeNumber))
                {
                    if (addPostOfficeNumber > 0)
                    {
                        postOfficeNumber = addPostOfficeNumber;
                        break;
                    }
                    else
                        Console.WriteLine("Incorrect post office number. Please, try again.");
                }
                else
                    Console.WriteLine("Incorrect post office number. Please, try again.");
            }
            Address address = new Address(country, town, postOfficeNumber);
            DeliveryMethod deliveryMethod;
            while (true)
            {
                Console.WriteLine("Enter delivery method (NewPost, UkrPost, Courier, SelfPickup):");
                if (Enum.TryParse(Console.ReadLine(), out DeliveryMethod addDeliveryMethod))
                {
                    deliveryMethod = addDeliveryMethod;
                    break;
                }
                Console.WriteLine("Incorrect delivery method. Please, try again.");
            }

            Order order = new Order();
            order.Goods = goods;
            order.TotalAmount = totalAmount;
            order.Address = address;
            order.DeliveryMethod = deliveryMethod;

            customer.MakeOrder(order);
        }

        static public void ViewChecks(IViewChecks user)
        {
            Console.Clear();
            Console.WriteLine(user.ViewChecks());
        }
    }
}
