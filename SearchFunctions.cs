using System;
using System.Collections.Generic;

namespace Shop_cousework.AdditionalClasses
{
    class SearchFunctions
    {
        public static bool SearchUserByLogin(string userData, string login)
        {
            string[] userDataTapes = userData.Split("\n", StringSplitOptions.RemoveEmptyEntries);
            string[][] userDataParts = new string[userDataTapes.Length][];
            for (int i = 0; i < userDataTapes.Length; ++i)
            {
                userDataParts[i] = userDataTapes[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            }
            for (int i = 0; i < userDataParts.Length; ++i)
            {
                if (login == userDataParts[i][1])
                    return true;
            }
            return false;
        }

        public static bool SearchUserByPassword(string userData, string login, string password)
        {
            string[] userDataTapes = userData.Split("\n", StringSplitOptions.RemoveEmptyEntries);
            string[][] userDataParts = new string[userDataTapes.Length][];
            for (int i = 0; i < userDataTapes.Length; ++i)
            {
                userDataParts[i] = userDataTapes[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            }
            for (int i = 0; i < userDataParts.Length; ++i)
            {
                if (login == userDataParts[i][1])
                    if (password == userDataParts[i][2])
                        return true;
            }
            return false;
        }

        public static string SearchUserByLoginAndRetUserData(string userData, string login)
        {
            string[] userDataTapes = userData.Split("\n", StringSplitOptions.RemoveEmptyEntries);
            string[][] userDataParts = new string[userDataTapes.Length][];
            for (int i = 0; i < userDataTapes.Length; ++i)
            {
                userDataParts[i] = userDataTapes[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            }
            for (int i = 0; i < userDataParts.Length; ++i)
            {
                if (login == userDataParts[i][1])
                    return userDataTapes[i];
            }
            return null;
        }

        public static bool SearchGoodsInShopByIdentifier(int identifier)
        {
            foreach (KeyValuePair<Type, Storage> item in StorageBase.GetInstance().Storages)
            {
                for (int i = 0; i < item.Value.Goods.Count; ++i)
                {
                    if (identifier == item.Value.Goods[i].Identifier)
                        return true;
                }
            }
            return false;
        }
    }
}
