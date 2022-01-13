using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task18
{
    class Product : AttributeSort
    {
        private string name;
        private double price;
        private double weight;
        public Product()
        {
            name = null;
            price = 0;
            weight = 0;
        }
        public Product(string name, double price, double weight)
        {
            this.name = name;
            this.price = price;
            this.weight = weight;
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Empty name of product");
                if (value.Length <= 1)
                    throw new ArgumentException("Incorrect length of name");
                name = value;
            }
        }
        public double Price
        {
            get { return price; }
            set
            {
                if (!double.TryParse(Convert.ToString(value), out double productPrice))
                    throw new FormatException("Incorrect format of product price");
                if (value < 0)
                    throw new ArgumentException("Incorrect price of product");
                price = value;
            }
        }
        public double Weight
        {
            get { return weight; }
            set
            {
                if (!double.TryParse(Convert.ToString(value), out double productWeight))
                    throw new FormatException("Incorrect format of product weight");
                if (value < 0)
                    throw new ArgumentException("Incorrect weight of product");
                weight = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType())
                return false;
            var other = (Product)obj;
            return (other.Name == this.Name);
        }
        public override int GetHashCode()
        {
            return (int)Price ^ (int)Weight;
        }
        public override string ToString()
        {
            return $"Name: {Name}, price = {Price} grn, weight = {Weight}g";
        }

        public override int CompareTo(AttributeSort other)
        {
            try
            {
                if (this is null || other is null || !(other is Product))
                    throw new ArgumentException("Incorrect value of product");
                switch (Attribute)
                {
                    case "name":
                        return this.Name.CompareTo((other as Product).Name);
                    case "price":
                        return this.Price.CompareTo((other as Product).Price);
                    case "weight":
                        return this.Weight.CompareTo((other as Product).Weight);
                    default:
                        throw new ArgumentException("Incorrect attribute");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }
    }
}
