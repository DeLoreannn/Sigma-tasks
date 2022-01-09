using System;
using System.Collections.Generic;
using System.IO;

namespace Shop_cousework
{
    class StorageEvents
    {
        public static void LogDataInFile(string message, string path)
        {
            try
            {
                if (!File.Exists(path))
                    throw new FileNotFoundException("File is not found");
                StreamWriter file = new StreamWriter(path, append: true);
                string line = DateTime.Now + " " + message;
                file.WriteLine(line);
                file.Close();
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private static Goods CorrectProductType()
        {
            string type;
            while (true)
            {
                Console.WriteLine("Enter type of goods:");
                type = Console.ReadLine();
                if (type == "Product")
                {
                    return new Product();
                }
                else if (type == "Meat")
                {
                    return new Meat();
                }
                else if (type == "Dairy_products")
                {
                    return new Dairy_products();
                }
                else if (type == "Technique")
                {
                    return new Technique();
                }
                else if (type == "Goods")
                {
                    return new Goods();
                }
            }
        }

        private static void CorrectGoodsIdentifier(Goods goods)
        {
            string identifierStr;
            while (true)
            {
                Console.WriteLine("Enter identifier of goods:");
                identifierStr = Console.ReadLine();
                if (int.TryParse(identifierStr, out int identifier) && identifier > 0)
                {
                    goods.Identifier = identifier;
                    break;
                }
            }
        }

        private static void CorrectGoodsName(Goods goods)
        {
            string name;
            while (true)
            {
                Console.WriteLine("Enter name of goods:");
                name = Console.ReadLine();
                if (name != "")
                {
                    goods.Name = name;
                    break;
                }
            }
        }
        
        private static void CorrectGoodsPrice(Goods goods)
        {
            string priceStr;
            while (true)
            {
                Console.WriteLine("Enter price of goods:");
                priceStr = Console.ReadLine();
                if (double.TryParse(priceStr, out double price) && price > 0)
                {
                    goods.Price = price;
                    break;
                }
            }
        }

        private static void CorrectGoodsWeight(Goods goods)
        {
            string weightStr;
            while (true)
            {
                Console.WriteLine("Enter weight of goods:");
                weightStr = Console.ReadLine();
                if (double.TryParse(weightStr, out double weight) && weight > 0)
                {
                    goods.Weight = weight;
                    break;
                }
            }
        }

        private static void CorrectProductExpirationDate(Goods goods)
        {
            string expirationDateStr;
            while (true)
            {
                Console.WriteLine("Enter expiration date of products:");
                expirationDateStr = Console.ReadLine();
                if (int.TryParse(expirationDateStr, out int expirationDate) && expirationDate > 0)
                {
                    (goods as Product).ExpirationDate = expirationDate;
                    break;
                }
            }
        }

        private static void CorrectGoodsCreationDate(Goods goods)
        {
            string creationDateStr;
            string[] values;
            while (true)
            {
                Console.WriteLine("Enter creation date of goods");
                creationDateStr = Console.ReadLine();
                values = creationDateStr.Split('.');
                if (values.Length == 3)
                {
                    if (int.TryParse(values[0], out int days) && int.TryParse(values[1], out int months) && int.TryParse(values[2], out int years))
                    {
                        if (months == 4 || months == 6 || months == 9 || months == 11)
                        {
                            if (days > 0 && days <= 30)
                            {
                                if (years > 1900 && years <= 2021)
                                {
                                    goods.CreationDate = creationDateStr;
                                    break;
                                }
                            }
                        }
                        else if (months == 2)
                        {
                            if (DateTime.IsLeapYear(years))
                            {
                                if (days > 0 && days <= 29)
                                {
                                    if (years > 1900 && years <= 2021)
                                    {
                                        goods.CreationDate = creationDateStr;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                if (days > 0 && days <= 28)
                                {
                                    if (years > 1900 && years <= 2021)
                                    {
                                        goods.CreationDate = creationDateStr;
                                        break;
                                    }
                                }
                            }
                        }
                        else if (months == 1 || months == 3 || months == 5 || months == 7 || months == 8 || months == 10 || months == 12)
                        {
                            if (days > 0 && days <= 31)
                            {
                                if (years > 1900 && years <= 2021)
                                {
                                    goods.CreationDate = creationDateStr;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void CorrectCategoryOfMeat(Goods goods)
        {
            string meatCategoryStr;
            Meat meat = goods as Meat;
            bool correctCategory = false;
            while (true)
            {
                Console.WriteLine("Enter category of meat:");
                meatCategoryStr = Console.ReadLine();
                switch (meatCategoryStr)
                {
                    case "Highest Grade":
                        meat.CategoryOfMeat = Category.HighestGrade;
                        correctCategory = true;
                        break;
                    case "First Sort":
                        meat.CategoryOfMeat = Category.FirstSort;
                        correctCategory = true;
                        break;
                    case "Second Sort":
                        meat.CategoryOfMeat = Category.SecondSort;
                        correctCategory = true;
                        break;
                }
                if (correctCategory == true)
                {
                    goods = meat;
                    break;
                }
            }
        }

        private static void CorrectKindOfMeat(Goods goods)
        {
            string kindOfMeatStr;
            Meat meat = goods as Meat;
            bool correctKind = false;
            while (true)
            {
                Console.WriteLine("Enter kind of meat:");
                kindOfMeatStr = Console.ReadLine();
                switch (kindOfMeatStr)
                {
                    case "Mutton":
                        meat.KindOfMeat = Kind.Mutton;
                        correctKind = true;
                        break;
                    case "Veal":
                        meat.KindOfMeat = Kind.Veal;
                        correctKind = true;
                        break;
                    case "Pork":
                        meat.KindOfMeat = Kind.Pork;
                        correctKind = true;
                        break;
                    case "Chicken":
                        meat.KindOfMeat = Kind.Chicken;
                        correctKind = true;
                        break;
                }
                if (correctKind == true)
                {
                    goods = meat;
                    break;
                }
            }
        }

        private static void CorrectTechniqueBatteryCapacity(Goods goods)
        {
            string batteryCapacityStr;
            while (true)
            {
                Console.WriteLine("Enter battery capacity of technique:");
                batteryCapacityStr = Console.ReadLine();
                if (int.TryParse(batteryCapacityStr, out int batteryCapacity) && batteryCapacity > 0)
                {
                    (goods as Technique).BatteryCapacity = batteryCapacity;
                    break;
                }
            }
        }

        private static void CorrectTechniqueWarrantyPeriod(Goods goods)
        {
            string warrantyPeriodStr;
            while (true)
            {
                Console.WriteLine("Enter warranty period of technique:");
                warrantyPeriodStr = Console.ReadLine();
                if (int.TryParse(warrantyPeriodStr, out int warrantyPeriod) && warrantyPeriod > 0)
                {
                    (goods as Technique).WarrantyPeriod = warrantyPeriod;
                    break;
                }
            }
        }
        public static Goods CorrectProductData(Goods goods, string mistake)
        {
            if (mistake == "type")
            {
                goods = CorrectProductType();
                return goods;
            }
            else if (mistake == "identifier")
            {
                CorrectGoodsIdentifier(goods);
                return goods;
            }
            else if (mistake == "name")
            {
                CorrectGoodsName(goods);
                return goods;
            }
            else if (mistake == "price")
            {
                CorrectGoodsPrice(goods);
                return goods;
            }
            else if (mistake == "weight")
            {
                CorrectGoodsWeight(goods);
                return goods;
            }
            else if (mistake == "expiration date")
            {
                CorrectProductExpirationDate(goods);
                return goods;
            }
            else if (mistake == "creation date")
            {
                CorrectGoodsCreationDate(goods);
                return goods;
            }
            else if (mistake == "meat category")
            {
                CorrectCategoryOfMeat(goods);
                return goods;
            }
            else if (mistake == "kind of meat")
            {
                CorrectKindOfMeat(goods);
                return goods;
            }
            else if (mistake == "battery capacity")
            {
                CorrectTechniqueBatteryCapacity(goods);
                return goods;
            }
            else if (mistake == "warranty period")
            {
                CorrectTechniqueWarrantyPeriod(goods);
                return goods;
            }
            return goods;
        }
        public static void LogExpiredProducts(List<Goods> list, string path)
        {
            try
            {
                if (!File.Exists(path))
                    throw new FileNotFoundException("File is not found");
                StreamWriter file = new StreamWriter(path, append: true);
                for (int i = 0; i < list.Count; ++i)
                {
                    file.WriteLine(list[i].ToString());
                }
                file.Close();
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void UpdateStorage(Goods goods, Type type)
        {
            for (int i = 0; i < StorageBase.GetInstance().Storages[type].Goods.Count; ++i)
            {
                if (StorageBase.GetInstance().Storages[type].Goods[i].Identifier == goods.Identifier)
                {
                    StorageBase.GetInstance().Storages[type].Goods[i].Price = goods.Price;
                }
            }
        }
    }
}
