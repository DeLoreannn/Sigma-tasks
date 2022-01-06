using System;
using System.Data;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.Linq;


namespace csqlite
{
    class Program
    {
        static void Main(string[] args)
        {
            Db db = new Db("Data Source=data.db");

            foreach (var product in db.GetProductList())
            {
                Console.WriteLine(product);
            }

            Console.WriteLine(db.GetProductByNameSql("Bounty"));

            foreach (var product in db.GetProductListWithPriceLinq(30))
            {
                Console.WriteLine(product);
            }
        }
    }
}
