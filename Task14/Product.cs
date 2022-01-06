using System;
using System.Data;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.Linq;

namespace csqlite
{
    class Product
    {
        private double price;
        private double weight;
        public string Name { get; set; }
        public double Price
        {
            get
            {
                return price;
            }
            set
            {
                try
                {
                    if (value <= 0)
                        throw new ArgumentException("Incorrect value of price");
                    price = value;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public double Weight
        {
            get
            {
                return weight;
            }
            set
            {
                try
                {
                    if (value <= 0)
                        throw new ArgumentException("Incorrect value of weight");
                    weight = value;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
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
}
