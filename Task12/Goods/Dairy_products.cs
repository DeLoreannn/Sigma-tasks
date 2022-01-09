using System;

namespace Shop_cousework
{
    class Dairy_products : Product
    {
        public Dairy_products() : base()
        {
        }
        public Dairy_products(int identifier, string name, double price, double weight, int expirationDate, string creationDate) : base(identifier, name, price, weight, expirationDate, creationDate)
        {
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType())
                return false;
            var other = (Dairy_products)obj;
            return (other.Name == this.Name) && (other.Price == this.Price) && (other.Weight == this.Weight) && (other.ExpirationDate == this.ExpirationDate);
        }
        public override int GetHashCode()
        {
            return (int)Price ^ (int)Weight;
        }
        public override string ToString()
        {
            return $"Identifier: {Identifier}, name: {Name}, price: {Price} grn, weight: {Weight}g, expiration date: {ExpirationDate} days, creation date: {CreationDate}";
        }
        public new Dairy_products Copy()
        {
            return (Dairy_products)this.MemberwiseClone();
        }

        public new static Dairy_products Parse(string tape)
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
                Dairy_products product = new Dairy_products();
                product.Identifier = int.Parse(split[0]);
                product.Name = split[1];
                product.Price = double.Parse(split[2]);
                product.Weight = double.Parse(split[3]);
                product.ExpirationDate = int.Parse(split[4]);
                product.CreationDate = split[5];
                return product;
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}
