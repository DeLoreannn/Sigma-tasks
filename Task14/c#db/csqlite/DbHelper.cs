using System;
using System.Data;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.Linq;

namespace csqlite
{
    static class DBHelper //extensions
    {
        public static List<Product> GetProductList(this Db db)
        {
            return db.GetList<Product>("select * from PRODUCT", Product.Create);
        }

        //sql
        public static Product GetProductByNameSql(this Db db, string name)
        {
            return db.GetList<Product>($"select * from PRODUCT where NAME like '{name}%' limit 1", Product.Create).FirstOrDefault();
        }

        //linq
        public static Product GetProductByNameLinq(this Db db, string name)
        {
            return db.GetProductList().Where(x => x.Name == name).FirstOrDefault();
        }

        //sql
        public static List<Product> GetProductListWithPriceSql(this Db db, double price)
        {
            return db.GetList<Product>($"select * from PRODUCT where PRICE like '{price}%'", Product.Create).ToList();
        }

        //linq
        public static List<Product> GetProductListWithPriceLinq(this Db db, double price)
        {
            return db.GetProductList().Where(x => x.Price == price).ToList();
        }
    }
}
