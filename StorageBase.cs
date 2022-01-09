using System;
using System.Collections.Generic;
using System.IO;

namespace Shop_cousework
{
    enum Type { Product = 0, Technique = 1, Goods = 2 }
    class StorageBase
    {
        private static StorageBase instance;

        private Dictionary<Type, Storage> storages;

        public Dictionary<Type, Storage> Storages
        {
            get { return storages; }
        }

        private StorageBase()
        {
            storages = new Dictionary<Type, Storage>();
        }

        public static StorageBase GetInstance()
        {
            if (instance == null)
            {
                instance = new StorageBase();
            }
            return instance;
        }

        public void ReadFromFile(string path)
        {
            try
            {
                if (!File.Exists(path))
                    throw new FileNotFoundException("File is not found");

                StreamReader file = new StreamReader(path);

                string text = file.ReadToEnd();
                if (text == "" || text == null)
                    throw new ArgumentException("File is empty");

                storages.Add(Type.Product, new Storage());
                storages.Add(Type.Technique, new Storage());
                storages.Add(Type.Goods, new Storage());

                string[] lines = text.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < lines.Length; ++i)
                {
                    string[] split = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (split.Length < 6 || split.Length > 9)
                        throw new ArgumentException($"Incorrect number of values in line {lines[i]}");
                    string goodsData = "";
                    for (int j = 1; j < split.Length; ++j)
                    {
                        if (j == (split.Length - 1))
                            goodsData += split[j];
                        else
                            goodsData += split[j] + " ";
                    }
                    if (split[0] == "Product")
                    {
                        storages[Type.Product].Goods.Add(Product.Parse(goodsData));
                    }
                    else if (split[0] == "Meat")
                    {
                        storages[Type.Product].Goods.Add(Meat.Parse(goodsData));
                    }
                    else if (split[0] == "Dairy_products")
                    {
                        storages[Type.Product].Goods.Add(Dairy_products.Parse(goodsData));
                    }
                    else if (split[0] == "Technique")
                    {
                        storages[Type.Technique].Goods.Add(Technique.Parse(goodsData));
                    }
                    else if (split[0] == "Goods")
                    {
                        storages[Type.Goods].Goods.Add(Goods.Parse(goodsData));
                    }
                    else
                    {
                        throw new ArgumentException($"Incorrect type of goods in line {lines[i]}");
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
