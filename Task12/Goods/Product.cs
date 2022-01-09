using System;

namespace Shop_cousework
{
    class Product : Goods
    {
        private int expirationDate;

        public int ExpirationDate
        {
            get { return expirationDate; }
            set
            {
                try
                {
                    if (!int.TryParse(Convert.ToString(value), out int productExpirationDate))
                        throw new FormatException("Incorrect format of product expiration date");
                    if (value < 0)
                        throw new ArgumentException("Incorrect expiration date of product");
                    expirationDate = value;
                }
                catch(FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch(ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public Product()
        {
            Name = null;
            Price = 0;
            Weight = 0;
            expirationDate = 0;
            CreationDate = null;
        }
        public Product(int identifier, string name, double price, double weight, int expirationDate, string creationDate) : base(identifier, name, price, weight, creationDate)
        {
            this.ExpirationDate = expirationDate;
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
            if (this != null)
            {
                return $"Identifier: {Identifier}, name: {Name}, price: {Price} grn, weight: {Weight}g, expiration date: {ExpirationDate} days, creation date: {CreationDate}";
            }
            else
            {
                return "";
            }
        }
        public new Product Copy()
        {
            return (Product)this.MemberwiseClone();
        }

        public new static Product Parse(string tape)
        {
            string[] split = tape.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            try
            {
                if (split.Length != 6)
                    throw new Exception($"Incorrect number of values in tape: {tape}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {
                if (!int.TryParse(split[0], out int addIdentifier))
                    throw new FormatException("Incorrect format of product identifier");
                if (!double.TryParse(split[2], out double addPrice))
                    throw new FormatException("Incorrect format of product price");
                if (!double.TryParse(split[3], out double addWeight))
                    throw new FormatException("Incorrect format of product weight");
                if (!int.TryParse(split[4], out int addExpirationDate))
                    throw new FormatException("Incorrect format of product expiration date");
                Product product = new Product();
                product.Identifier = int.Parse(split[0]);
                product.Name = split[1];
                product.Price = double.Parse(split[2]);
                product.Weight = double.Parse(split[3]);
                product.ExpirationDate = int.Parse(split[4]);
                product.CreationDate = split[5];
                return product;
            }
            catch(FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}
