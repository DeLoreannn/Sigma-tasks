using System;
using System.Data;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.Linq;


namespace csqlite
{
    class Product
    {
        private string name;
        private double price;
        private double weight;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public double Price
        {
            get { return price; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Incorrect product price");
                price = value;
            }
        }

        public double Weight
        {
            get { return weight; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Incorrect product price");
                weight = value;
            }
        }


        public Product() { }
        public Product(string Name, double Price, double Weight)
        {
            this.Name = Name;
            this.Price = Price;
            this.Weight = Weight;
        }

        public override string ToString()
        {
            return $"Name: {Name}, price: {Price} grn, weight: {Weight} g";
        }
        public static Product Create(IDataRecord data)
        {
            return new Product(data["NAME"].ToString(), Convert.ToDouble(data["PRICE"].ToString()), Convert.ToDouble(data["WEIGHT"].ToString()));
        }
    }


    static class DBHelper //extensions
    {
        public static List<Product> GetProductList(this Db db)
        {
            return db.GetList<Product>("select * from PRODUCT", Product.Create);
        }
        //linq
        public static Product GetProductByNameLinq(this Db db, string name)
        {
            return db.GetProductList().Where(p => p.Name == name).FirstOrDefault();
        }
        //sql
        public static Product GetProductByName(this Db db, string name)
        {
            return db.GetList<Product>($"select NAME, PRICE, WEIGHT from PRODUCT where NAME like '{name}%' limit 1", Product.Create).FirstOrDefault();
        }
        //linq
    }
    class Db
    {
        public SqliteConnection connection;
        public Db() { }
        public Db(string connectionString)
        {
            try
            {
                connection = new SqliteConnection(connectionString);
                connection.Open();
            }
            catch(SqliteException ex)
            {
                Console.WriteLine($"Error Generated. Details: {ex.ToString()}");
            }
        }

        public void ExecuteSQL(string sql)
        {
            using (connection)
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = sql;
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public List<T> GetList<T>(string sql, Func<IDataRecord, T> generator)
        {
            var list = new List<T>();
            using (connection)
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(sql, connection);
                try
                {
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {

                            list.Add(generator(reader));
                        }
                    }
                }
                catch
                {
                    return null;
                }
            }

            return list;
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            Db db = new Db("Data Source=data.db");

            foreach (Product product in db.GetProductList())
            {
                Console.WriteLine(product.ToString());
            }

            Console.WriteLine(db.GetProductByName("Twix"));
        }
    }
}
