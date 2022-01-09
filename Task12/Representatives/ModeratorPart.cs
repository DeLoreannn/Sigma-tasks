using System;
using System.Collections.Generic;
using Shop_cousework.Interfaces;

namespace Shop_cousework.Representatives
{
    static class ModeratorPart
    {
        static public void CreatePromotion(ICreatingPromotion moderator)
        {
            Console.Clear();
            Promotion promotion = new Promotion();
            Type type;
            while (true)
            {
                Console.WriteLine("Enter type of promotion (Product, Goods, Technique):");
                if (!Enum.TryParse(Console.ReadLine(), out Type addType))
                    Console.WriteLine("Incorrect type. Please, try again.");
                else
                {
                    type = addType;
                    break;
                }
            }
            moderator.CreatePromotion(promotion, type);
            Console.WriteLine(StorageBase.GetInstance().Storages[type].ToString());
            List<Goods> goods = new List<Goods>();
            int goodsNumber = 0;
            while (true)
            {
                Console.WriteLine("Enter number of goods:");
                if (!int.TryParse(Console.ReadLine(), out int addGoodsNumber))
                    Console.WriteLine("Incorrect number of goods. Please, try again.");
                else
                {
                    if (addGoodsNumber > StorageBase.GetInstance().Storages[type].Goods.Count || addGoodsNumber < 0)
                        Console.WriteLine("Incorrect number of goods. Please, try again.");
                    else
                    {
                        goodsNumber = addGoodsNumber;
                        break;
                    }
                }
            }

            bool foundGoods = false;
            for (int i = 0; i < goodsNumber;)
            {
                Console.Clear();
                Console.WriteLine(StorageBase.GetInstance().Storages[type].ToString());
                Console.WriteLine("Enter identifier:");
                if (int.TryParse(Console.ReadLine(), out int identifier))
                {
                    foundGoods = false;
                    for (int j = 0; j < StorageBase.GetInstance().Storages[type].Goods.Count; ++j)
                    {
                        if (identifier == StorageBase.GetInstance().Storages[type].Goods[j].Identifier)
                        {
                            while (true)
                            {
                                Console.WriteLine("Enter percentage to change price:");
                                if (int.TryParse(Console.ReadLine(), out int percentage))
                                {
                                    PromotionBase.GetInstance().AddGoodsToPromotion(StorageBase.GetInstance().Storages[type].Goods[j], type, percentage);
                                    foundGoods = true;
                                    break;
                                }
                                else
                                    Console.WriteLine("Incorrect percentage. Please, try again.");
                            }
                        }
                    }
                    if (foundGoods == true)
                        ++i;
                    else
                        Console.WriteLine("Incorrect identifier. Please, try again.");
                }
                else
                    Console.WriteLine("Incorrect identifier. Please, try again.");
            }
            Console.WriteLine(StorageBase.GetInstance().Storages[type].ToString());
        }

        static public void DeletePromotion(ICreatingPromotion moderator)
        {
            Console.Clear();
            bool isPromotionBaseEmpty = PromotionBase.GetInstance().Promotions.Count > 0 ? false : true;
            
            if (isPromotionBaseEmpty == true)
            {
                Console.WriteLine("Promotion base is empty.\n");
                return;
            }
            Console.WriteLine(PromotionBase.GetInstance().ToString());
            Type type;
            while (true)
            {
                Console.WriteLine("Enter type of promotion:");
                if (!Enum.TryParse(Console.ReadLine(), out Type addType))
                    Console.WriteLine("Incorrect type. Please, try again.");
                else
                {
                    type = addType;
                    break;
                }
            }
            moderator.DeletePromotion(type);
        }

        static public void ViewPromotions(ICreatingPromotion moderator)
        {
            Console.Clear();
            Console.WriteLine(moderator.ViewPromotions());
        }

        static public void AddGoodsToPromotion(IPromotionInteraction moderator)
        {
            Type type;
            if (PromotionBase.GetInstance().Promotions.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("Promotion base does not have any promotions. Firstly, create promotion.");
            }
            else
            {
                while (true)
                {
                    Console.WriteLine("Enter type of goods (Product, Goods, Technique):");
                    if (Enum.TryParse(Console.ReadLine(), out Type addType))
                    {
                        if (PromotionBase.GetInstance().Promotions.ContainsKey(addType))
                        {
                            type = addType;
                            break;
                        }
                        else
                            Console.WriteLine($"Promotion base does not have promotion with type: {addType}");
                    }
                    else
                        Console.WriteLine("Incorrect type. Please, try again.");
                }
                Console.Clear();
                Console.WriteLine(StorageBase.GetInstance().Storages[type].ToString());
                bool foundGoods = true;
                while (foundGoods)
                {
                    Console.WriteLine("Enter identifier:");
                    if (int.TryParse(Console.ReadLine(), out int identifier))
                    {
                        for (int j = 0; j < StorageBase.GetInstance().Storages[type].Goods.Count; ++j)
                        {
                            if (identifier == StorageBase.GetInstance().Storages[type].Goods[j].Identifier)
                            {
                                while (true)
                                {
                                    Console.WriteLine("Enter percentage to change price:");
                                    if (int.TryParse(Console.ReadLine(), out int percentage))
                                    {
                                        moderator.AddGoodsToPromotion(type, StorageBase.GetInstance().Storages[type].Goods[j], percentage);
                                        foundGoods = false;
                                        break;
                                    }
                                    else
                                        Console.WriteLine("Incorrect percentage. Please, try again.");
                                }
                            }
                        }
                    }
                    else
                        Console.WriteLine("Incorrect identifier. Please, try again.");
                }
            }
        }

        static public void RemoveGoodsFromPromotion(IPromotionInteraction moderator)
        {
            Console.Clear();
            Type type;
            if (PromotionBase.GetInstance().Promotions.Count == 0)
            {
                Console.WriteLine("Promotion base is empty");
                return;
            }
            while (true)
            {
                Console.WriteLine("Enter type of goods (Product, Goods, Technique):");
                if (!Enum.TryParse(Console.ReadLine(), out Type addType))
                    Console.WriteLine("Incorrect type. Please, try again.");
                else
                {
                    if (PromotionBase.GetInstance().Promotions.ContainsKey(addType))
                    {
                        type = addType;
                        break;
                    }
                    else
                        Console.WriteLine("Incorrect type. Please, try again.");
                }
            }
            Console.Clear();
            Console.WriteLine(PromotionBase.GetInstance().Promotions[type].ToString());
            bool foundGoods = true;
            while (foundGoods)
            {
                Console.WriteLine("Enter identifier:");
                if (int.TryParse(Console.ReadLine(), out int identifier))
                {
                    for (int j = 0; j < PromotionBase.GetInstance().Promotions[type].Goods.Count; ++j)
                    {
                        if (identifier == PromotionBase.GetInstance().Promotions[type].Goods[j].Identifier)
                        {
                            moderator.RemoveGoodsFromPromotion(type, PromotionBase.GetInstance().Promotions[type].Goods[j]);
                            foundGoods = false;
                            break;
                        }
                    }
                }
                else
                    Console.WriteLine("Incorrect identifier. Please, try again.");
            }
    }

        static public void ViewPlacedOrders(IOrderInteraction moderator)
        {
            Console.Clear();
            Console.WriteLine(moderator.ViewPlacedOrders());
        }

        static public void DeletePlacedOrders(IOrderInteraction moderator)
        {
            Console.Clear();
            if (OrderBase.GetInstance().Orders.Count == 0)
            {
                Console.WriteLine("Order base is empty.\n");
                return;
            }
            Console.WriteLine(OrderBase.GetInstance().ToString());
            while (true)
            {
                Console.WriteLine("Enter order number:");
                if (int.TryParse(Console.ReadLine(), out int orderNumber))
                {
                    if (orderNumber > 0 && orderNumber <= OrderBase.GetInstance().Orders.Count)
                    {
                        moderator.DeletePlacedOrder(orderNumber);
                        break;
                    }
                    else
                        Console.WriteLine("Incorrect order number. Please, try again.");
                }
                else
                    Console.WriteLine("Incorrect order number. Please, try again.");
            }
        }
    }
}
