using System;
using Shop_cousework.Interfaces;
using Shop_cousework.AdditionalClasses;
using Shop_cousework.Creators;

namespace Shop_cousework.Representatives
{
    static class AdministratorPart
    {
        static public event LogIntoFile OnLogIncorrectInfoToFile;
        static public event CorrectInput OnCorrectDataProduct;

        static AdministratorPart()
        {
            OnLogIncorrectInfoToFile += StorageEvents.LogDataInFile;
            OnCorrectDataProduct += StorageEvents.CorrectProductData;
        }

        #region Enter goods data
        static private void EnterIdentifier(Goods goods)
        {
            Console.WriteLine("Enter identifier of goods:");
            string identifierStr = Console.ReadLine();
            if (!int.TryParse(identifierStr, out int identifier))
            {
                OnLogIncorrectInfoToFile?.Invoke($"Incorrect format of goods identifier: {identifierStr}", @"./output.txt");
                OnCorrectDataProduct?.Invoke(goods, "identifier");
            }
            else if (identifier <= 0)
            {
                OnLogIncorrectInfoToFile?.Invoke($"Incorrect goods identifier: {identifierStr}", @"./output.txt");
                OnCorrectDataProduct?.Invoke(goods, "identifier");
            }
            else
            {
                goods.Identifier = identifier;
            }
        }

        static private void EnterName(Goods goods)
        {
            Console.WriteLine("Enter name of goods:");
            string nameStr = Console.ReadLine();
            if (nameStr == "")
            {
                OnLogIncorrectInfoToFile?.Invoke($"Incorrect name of goods: {goods.Name}", @"./output.txt");
                OnCorrectDataProduct?.Invoke(goods, "name");
            }
            else
            {
                goods.Name = nameStr;
            }
        }

        static private void EnterPrice(Goods goods)
        {
            Console.WriteLine("Enter price of goods:");
            string priceStr = Console.ReadLine();
            if (!double.TryParse(priceStr, out double price))
            {
                OnLogIncorrectInfoToFile?.Invoke($"Incorrect format of goods price: {priceStr}", @"./output.txt");
                OnCorrectDataProduct?.Invoke(goods, "price");
            }
            else if (price <= 0)
            {
                OnLogIncorrectInfoToFile?.Invoke($"Incorrect goods price: {priceStr}", @"./output.txt");
                OnCorrectDataProduct?.Invoke(goods, "price");
            }
            else
            {
                goods.Price = price;
            }
        }

        static private void EnterWeight(Goods goods)
        {
            Console.WriteLine("Enter weight of goods:");
            string weightStr = Console.ReadLine();
            if (!double.TryParse(weightStr, out double weight))
            {
                OnLogIncorrectInfoToFile?.Invoke($"Incorrect format of goods weight: {weightStr}", @"./output.txt");
                OnCorrectDataProduct?.Invoke(goods, "weight");
            }
            else if (weight <= 0)
            {
                OnLogIncorrectInfoToFile?.Invoke($"Incorrect goods weight: {weightStr}", @"./output.txt");
                OnCorrectDataProduct?.Invoke(goods, "weight");
            }
            else
            {
                goods.Weight = weight;
            }
        }

        static private void EnterCreationDate(Goods goods)
        {
            Console.WriteLine("Enter creation date of goods:");
            string creationDateStr = Console.ReadLine();
            string[] values = creationDateStr.Split(".");
            bool creationDateIsChanged = false;
            if (values.Length != 3)
            {
                OnLogIncorrectInfoToFile?.Invoke($"Incorrect number of values in creation date: {creationDateStr}", @"./output.txt");
                OnCorrectDataProduct?.Invoke(goods, "creation date");
                creationDateIsChanged = true;
            }
            else
            {
                if (!int.TryParse(values[0], out int days) && (!creationDateIsChanged))
                {
                    OnLogIncorrectInfoToFile?.Invoke($"Incorrect format of days in creation date: {creationDateStr}", @"./output.txt");
                    OnCorrectDataProduct?.Invoke(goods, "creation date");
                    creationDateIsChanged = true;
                }
                if (!int.TryParse(values[1], out int months) && (!creationDateIsChanged))
                {
                    OnLogIncorrectInfoToFile?.Invoke($"Incorrect format of months in creation date: {creationDateStr}", @"./output.txt");
                    OnCorrectDataProduct?.Invoke(goods, "creation date");
                    creationDateIsChanged = true;
                }
                if (!int.TryParse(values[2], out int years) && (!creationDateIsChanged))
                {
                    OnLogIncorrectInfoToFile?.Invoke($"Incorrect format of years in creation date: {creationDateStr}", @"./output.txt");
                    OnCorrectDataProduct?.Invoke(goods, "creation date");
                    creationDateIsChanged = true;
                }

                switch (months)
                {
                    case 4:
                    case 6:
                    case 9:
                    case 11:
                        if ((int.Parse(values[0]) < 0 || int.Parse(values[0]) > 30) && (!creationDateIsChanged))
                        {
                            OnLogIncorrectInfoToFile?.Invoke($"Incorrect number of days in creation date: {creationDateStr}", @"./output.txt");
                            OnCorrectDataProduct?.Invoke(goods, "creation date");
                            creationDateIsChanged = true;
                        }
                        break;
                    case 2:
                        if (DateTime.IsLeapYear(years))
                            if ((int.Parse(values[0]) < 0 || int.Parse(values[0]) > 29) && (!creationDateIsChanged))
                            {
                                OnLogIncorrectInfoToFile?.Invoke($"Incorrect number of days in creation date: {creationDateStr}", @"./output.txt");
                                OnCorrectDataProduct?.Invoke(goods, "creation date");
                                creationDateIsChanged = true;
                            }
                            else if ((int.Parse(values[0]) < 0 || int.Parse(values[0]) > 28) && (!creationDateIsChanged))
                            {
                                OnLogIncorrectInfoToFile?.Invoke($"Incorrect number of days in creation date: {creationDateStr}", @"./output.txt");
                                OnCorrectDataProduct?.Invoke(goods, "creation date");
                                creationDateIsChanged = true;
                            }
                        break;
                    case 1:
                    case 3:
                    case 5:
                    case 7:
                    case 8:
                    case 10:
                    case 12:
                        if ((int.Parse(values[0]) < 0 || int.Parse(values[0]) > 31) && (!creationDateIsChanged))
                        {
                            OnLogIncorrectInfoToFile?.Invoke($"Incorrect number of days in creation date: {creationDateStr}", @"./output.txt");
                            OnCorrectDataProduct?.Invoke(goods, "creation date");
                            creationDateIsChanged = true;
                        }
                        break;
                }
                if (((months <= 0) || (months > 12)) && (!creationDateIsChanged))
                {
                    OnLogIncorrectInfoToFile?.Invoke($"Incorrect number of months in creation date: {creationDateStr}", @"./output.txt");
                    OnCorrectDataProduct?.Invoke(goods, "creation date");
                    creationDateIsChanged = true;
                }
                if (((years < 1900) || (years > 2021)) && (!creationDateIsChanged))
                {
                    OnLogIncorrectInfoToFile?.Invoke($"Incorrect number of years in creation date: {creationDateStr}", @"./output.txt");
                    OnCorrectDataProduct?.Invoke(goods, "creation date");
                    creationDateIsChanged = true;
                }
                if (!creationDateIsChanged)
                {
                    goods.CreationDate = values[0] + "." + values[1] + "." + values[2];
                }
            }
        }

        static private void EnterExpirationDate(Goods goods)
        {
            Console.WriteLine("Enter expiration date of product:");
            string expirationDateStr = Console.ReadLine();
            if (!int.TryParse(expirationDateStr, out int expirationDate))
            {
                OnLogIncorrectInfoToFile?.Invoke($"Incorrect format of expiration date of product: {expirationDateStr}", @"./output.txt");
                OnCorrectDataProduct?.Invoke(goods, "expiration date");
            }
            else if (expirationDate <= 0)
            {
                OnLogIncorrectInfoToFile?.Invoke($"Incorrect expiration date of product: {expirationDateStr}", @"./output.txt");
                OnCorrectDataProduct?.Invoke(goods, "expiration date");
            }
            else
            {
                (goods as Product).ExpirationDate = expirationDate;
            }
        }

        static private void EnterCategoryOfMeat(Goods goods)
        {
            Meat meatProduct = goods as Meat;
            Console.WriteLine("Enter category of meat:");
            string categoryOfMeatStr = Console.ReadLine();
            switch (categoryOfMeatStr)
            {
                case "Highest Grade":
                    meatProduct.CategoryOfMeat = Category.HighestGrade;
                    break;
                case "First Sort":
                    meatProduct.CategoryOfMeat = Category.FirstSort;
                    break;
                case "Second Sort":
                    meatProduct.CategoryOfMeat = Category.SecondSort;
                    break;
                default:
                    OnLogIncorrectInfoToFile?.Invoke($"Incorrect meat category: {categoryOfMeatStr}", @"./output.txt");
                    OnCorrectDataProduct?.Invoke(meatProduct, "meat category");
                    break;
            }
            goods = meatProduct;
        }

        static private void EnterKindOfMeat(Goods goods)
        {
            Meat meatProduct = goods as Meat;
            Console.WriteLine("Enter kind of meat");
            string kindOfMeatStr = Console.ReadLine();
            switch (kindOfMeatStr)
            {
                case "Mutton":
                    meatProduct.KindOfMeat = Kind.Mutton;
                    break;
                case "Veal":
                    meatProduct.KindOfMeat = Kind.Veal;
                    break;
                case "Pork":
                    meatProduct.KindOfMeat = Kind.Pork;
                    break;
                case "Chicken":
                    meatProduct.KindOfMeat = Kind.Chicken;
                    break;
                default:
                    OnLogIncorrectInfoToFile?.Invoke($"Incorrect kind of meat: {kindOfMeatStr}", @"./output.txt");
                    OnCorrectDataProduct?.Invoke(meatProduct, "kind of meat");
                    break;
            }
            goods = meatProduct;
        }

        static private void EnterBatteryCapacity(Goods goods)
        {
            Console.WriteLine("Enter battery capacity of technique:");
            string batterCapacityStr = Console.ReadLine();
            if (!int.TryParse(batterCapacityStr, out int batteryCapacity))
            {
                OnLogIncorrectInfoToFile?.Invoke($"Incorrect format of technique battery capacity: {batterCapacityStr}", @"./output.txt");
                OnCorrectDataProduct?.Invoke(goods, "battery capacity");
            }
            else if (batteryCapacity <= 0)
            {
                OnLogIncorrectInfoToFile?.Invoke($"Incorrect goods battery capacity: {batterCapacityStr}", @"./output.txt");
                OnCorrectDataProduct?.Invoke(goods, "battery capacity");
            }
            else
            {
                (goods as Technique).BatteryCapacity = batteryCapacity;
            }
        }

        static private void EnterWarrantyPeriod(Goods goods)
        {
            Console.WriteLine("Enter warranty period of technique:");
            string warrantyPeriodStr = Console.ReadLine();
            if (!int.TryParse(warrantyPeriodStr, out int warrantyPeriod))
            {
                OnLogIncorrectInfoToFile?.Invoke($"Incorrect format of technique warranty period: {warrantyPeriodStr}", @"./output.txt");
                OnCorrectDataProduct?.Invoke(goods, "warranty period");
            }
            else if (warrantyPeriod <= 0)
            {
                OnLogIncorrectInfoToFile?.Invoke($"Incorrect goods warranty period: {warrantyPeriodStr}", @"./output.txt");
                OnCorrectDataProduct?.Invoke(goods, "warranty period");
            }
            else
            {
                (goods as Technique).WarrantyPeriod = warrantyPeriod;
            }
        }
        #endregion

        static public void AddGoodsToStorage(IGoodsManagement administrator, string shopContent)
        {
            Console.Clear();
            Console.WriteLine(shopContent);
            while (true)
            {
                Console.WriteLine("Enter fabric of goods: 1 - Product creator, 2 - Goods creator, 3 - Technique creator");
                if (!int.TryParse(Console.ReadLine(), out int fabric))
                    Console.WriteLine("Incorrect type. Please, try again.");
                else
                {
                    if (fabric == 1)
                    {
                        Goods goods;
                        while (true)
                        {
                            Console.WriteLine("Enter type of product (1 - Product, 2 - Meat, 3 - Dairy products):");
                            if (int.TryParse(Console.ReadLine(), out int addType))
                            {
                                if (addType == 1)
                                {
                                    goods = new Product();
                                    EnterIdentifier(goods);
                                    EnterName(goods);
                                    EnterPrice(goods);
                                    EnterWeight(goods);
                                    EnterCreationDate(goods);
                                    EnterExpirationDate(goods);
                                    break;
                                }
                                else if (addType == 2)
                                {
                                    goods = new Meat();
                                    EnterIdentifier(goods);
                                    EnterName(goods);
                                    EnterPrice(goods);
                                    EnterWeight(goods);
                                    EnterCreationDate(goods);
                                    EnterExpirationDate(goods);
                                    EnterCategoryOfMeat(goods);
                                    EnterKindOfMeat(goods);
                                    break;
                                }
                                else if (addType == 3)
                                {
                                    goods = new Dairy_products();
                                    EnterIdentifier(goods);
                                    EnterName(goods);
                                    EnterPrice(goods);
                                    EnterWeight(goods);
                                    EnterCreationDate(goods);
                                    EnterExpirationDate(goods);
                                    break;
                                }
                                else
                                    Console.WriteLine("Incorrect type. Please, try again.");
                            }
                            else
                                Console.WriteLine("Incorrect type. Please, try again.");
                        }

                        administrator.AddGoodsToStorage(new ProductCreator(), Type.Product, goods);
                        break;
                    }
                    else if (fabric == 2)
                    {
                        Goods goods = new Goods();
                        EnterIdentifier(goods);
                        EnterName(goods);
                        EnterPrice(goods);
                        EnterWeight(goods);
                        EnterCreationDate(goods);
                        administrator.AddGoodsToStorage(new GoodsCreator(), Type.Goods, goods);
                        break;
                    }
                    else if (fabric == 3)
                    {
                        Goods goods = new Technique();
                        EnterIdentifier(goods);
                        EnterName(goods);
                        EnterPrice(goods);
                        EnterWeight(goods);
                        EnterCreationDate(goods);
                        EnterBatteryCapacity(goods);
                        EnterWarrantyPeriod(goods);
                        administrator.AddGoodsToStorage(new TechniqueCreator(), Type.Technique, goods);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Incorrect type. Please, try again.");
                    }
                }
            }
            Console.Clear();
        }

        static public void RemoveGoodsFromStorage(IGoodsManagement administrator, string shopContent)
        {
            Console.Clear();
            int identifier = 0;
            bool isStorageBaseEmpty = true;
            foreach(var item in StorageBase.GetInstance().Storages)
            {
                if (item.Value.Goods.Count > 0)
                    isStorageBaseEmpty = false;
            }
            if (isStorageBaseEmpty == true)
            {
                Console.WriteLine("Storage base is empty.\n");
                return;
            }

            Console.WriteLine(shopContent);

            while (true)
            {
                Console.WriteLine("Enter identifier of goods:");
                if (!int.TryParse(Console.ReadLine(), out int addIdentifier))
                    Console.WriteLine("Incorrect identifier. Please, try again.");
                else
                {
                    if (SearchFunctions.SearchGoodsInShopByIdentifier(addIdentifier))
                    {
                        identifier = addIdentifier;
                        break;
                    }
                    else
                        Console.WriteLine("Incorrect identifier. Please, try again.");
                }
            }
            administrator.RemoveGoodsFromStorage(identifier);
        }

        static public void SetClientStatus(IClientStatus administrator)
        {
            Console.Clear();
            Console.WriteLine(ClientBase.GetIntsance().ToString());
            string login = "";
            bool choice = true;
            while (choice)
            {
                Console.WriteLine("Enter login of client:");
                login = Console.ReadLine();
                if (SearchFunctions.SearchUserByLogin(ClientBase.GetIntsance().ToString(), login))
                {
                    while (choice)
                    {
                        Console.WriteLine("Set status of client:");
                        if (bool.TryParse(Console.ReadLine(), out bool status))
                        {
                            administrator.SetClientStatus(login, status);
                            choice = false;
                        }
                        else
                        {
                            Console.WriteLine("Incorrect status of client. Please, try again.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect login of client. Please, try again.");
                }
            }
            Console.WriteLine(ClientBase.GetIntsance().ToString());
        }
    }
}
