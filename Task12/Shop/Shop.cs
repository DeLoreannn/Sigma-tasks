using System;
using System.IO;
using System.Collections.Generic;
using Shop_cousework.Interfaces;
using Shop_cousework.Bases;

namespace Shop_cousework
{
    class Shop
    {
        private IRepresentation representation;

        private static Shop instance;

        private Shop()
        {
            StorageBase storageBase = StorageBase.GetInstance();
            FillStorageBase(@"./input.txt");

            UserBase userBase = UserBase.GetInstance();
            ClientBase clientBase = ClientBase.GetIntsance();
            FillUserBase(@"./User base.txt");
        }

        public static Shop GetInstance()
        {
            if (instance == null)
            {
                instance = new Shop();
            }
            return instance;
        }

        public void SetRepresentation(IRepresentation representation)
        {
            this.representation = representation;
        }

        public void RegisterClient(Client client)
        {
            UserBase.GetInstance().Users.Add(client);
            ClientBase.GetIntsance().Clients.Add(client);
        }

        public void FillUserBase(string path)
        {
            try
            {
                if (!File.Exists(path))
                    throw new FileNotFoundException("File with user base is not found");

                StreamReader file = new StreamReader(path);
                if (file.ReadToEnd() == "")
                    throw new ArgumentException("File with user base is empty");
                file.BaseStream.Position = 0;

                string []content = file.ReadToEnd().Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
                string[] userData;
                for (int i = 0; i < content.Length; ++i)
                {
                    userData = content[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (userData.Length != 3 && userData.Length != 7)
                        throw new ArgumentException($"Incorrect user data in tape: {content[i]}");
                    switch (userData[0])
                    {
                        case "Client":
                            if (!bool.TryParse(userData[5], out bool status))
                                throw new ArgumentException("Invalid status of client");
                            UserBase.GetInstance().Users.Add(new Client(userData[1], userData[2], userData[3], userData[4], status, userData[6]));
                            ClientBase.GetIntsance().Clients.Add(new Client(userData[1], userData[2], userData[3], userData[4], status, userData[6]));
                            break;
                        case "Moderator":
                            UserBase.GetInstance().Users.Add(new Moderator(userData[1], userData[2]));
                            break;
                        case "Administrator":
                            UserBase.GetInstance().Users.Add(new Administrator(userData[1], userData[2]));
                            break;
                        default:
                            throw new ArgumentException("Undeclared type of user");
                    }
                }
                file.Close();
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void FillStorageBase(string path)
        {
            StorageBase.GetInstance().ReadFromFile(path);
        }

        public string ViewStorageContent()
        {
            string result = "";
            foreach (KeyValuePair<Type, Storage> item in StorageBase.GetInstance().Storages)
            {
                result += $"Storage {item.Key}" + "\n";
                result += item.Value.ToString() + "\n";
            }
            return result;
        }

        private void RemoveGoodsFromStorage(int identifier)
        {
            foreach (KeyValuePair<Type, Storage> item in StorageBase.GetInstance().Storages)
            {
                for (int i = 0; i < item.Value.Goods.Count; ++i)
                {
                    if (identifier == item.Value.Goods[i].Identifier)
                    {
                        StorageBase.GetInstance().Storages[item.Key].Goods.Remove(item.Value.Goods[i]);
                    }
                }
            }
        }

        public Goods GetGoods(int identifier)
        {
            Goods goods;
            foreach (KeyValuePair<Type, Storage> item in StorageBase.GetInstance().Storages)
            {
                for (int i = 0; i < item.Value.Goods.Count; ++i)
                {
                    if (identifier == item.Value.Goods[i].Identifier)
                    {
                        goods = item.Value.Goods[i];
                        RemoveGoodsFromStorage(identifier);
                        return goods;
                    }
                }
            }
            return null;
        }

        public void ReturnGoods(Goods goods)
        {
            if (goods is Product)
            {
                StorageBase.GetInstance().Storages[Type.Product].Goods.Add(goods);
            }
            else if (goods is Technique)
            {
                StorageBase.GetInstance().Storages[Type.Technique].Goods.Add(goods);
            }
            else if (goods is Goods)
            {
                StorageBase.GetInstance().Storages[Type.Goods].Goods.Add(goods);
            }
        }

        public Check ConfirmateOrder(Order order, Dictionary<int, Goods> goods)
        {
            foreach (KeyValuePair<int, Goods> item in goods)
            {
                RemoveGoodsFromStorage(item.Key);
            }
            OrderBase.GetInstance().Orders.Add(order);
            Check check = new Check(order.ToString());
            CheckBase.GetInstance().Checks.Add(check);
            return new Check(order.ToString());
        }

        public void ShopInteraction()
        {
            representation.RepresentShop();
        }
    }
}
