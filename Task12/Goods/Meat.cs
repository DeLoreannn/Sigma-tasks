using System;

namespace Shop_cousework
{
    enum Category { HighestGrade, FirstSort, SecondSort };
    enum Kind { Mutton, Veal, Pork, Chicken };
    class Meat : Product
    {
        private Category categoryOfMeat;
        public Category CategoryOfMeat
        {
            get { return categoryOfMeat; }
            set
            {
                categoryOfMeat = value;
            }
        }

        private Kind kindOfMeat;
        public Kind KindOfMeat
        {
            get { return kindOfMeat; }
            set
            {
                string kindCategoryString = Convert.ToString(value);
                if (!Enum.TryParse(kindCategoryString, true, out Kind meatKind))
                    throw new Exception("Incorrect kind of meat");
                kindOfMeat = value;
            }
        }

        public Meat() : base()
        {
            categoryOfMeat = Category.FirstSort;
            kindOfMeat = Kind.Chicken;
        }
        public Meat(int identifier, string name, double price, double weight, Kind kindOfMeat, Category categoryOfMeat, int expirationDate, string creationDate) : base(identifier, name, price, weight, expirationDate, creationDate)
        {
            this.kindOfMeat = kindOfMeat;
            this.categoryOfMeat = categoryOfMeat;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType())
                return false;
            var other = (Meat)obj;
            return (other.Name == this.Name) && (other.Price == this.Price) && (other.Weight == this.Weight) && (other.KindOfMeat == this.KindOfMeat) && (other.CategoryOfMeat == this.CategoryOfMeat);
        }
        public override int GetHashCode()
        {
            return (int)Price ^ (int)Weight;
        }
        public override string ToString()
        {
            return $"Identifier: {Identifier}, name: {Name}, price: {Price} grn, weight: {Weight}g, expiration date: {ExpirationDate} days, creation date: {CreationDate}, category: {CategoryOfMeat}, kind of meat: {KindOfMeat}";
        }
        public new Meat Copy()
        {
            return (Meat)this.MemberwiseClone();
        }

        public new static Meat Parse(string tape)
        {
            string[] split = tape.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            try
            {
                if (split.Length != 8)
                    throw new Exception($"Incorrect number of values in tape: {tape}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {
                Meat meat = new Meat();
                if (!int.TryParse(split[0], out int addIdentifier))
                    throw new FormatException("Incorrect format of meat identifier");
                if (!double.TryParse(split[2], out double addPrice))
                    throw new FormatException("Incorrect format of meat price");
                if (!double.TryParse(split[3], out double addWeight))
                    throw new FormatException("Incorrect format of meat weight");
                if (!int.TryParse(split[4], out int addExpirationDate))
                    throw new FormatException("Incorrect format of meat expiration date");
                meat.Identifier = int.Parse(split[0]);
                meat.Name = split[1];
                meat.Price = double.Parse(split[2]);
                meat.Weight = double.Parse(split[3]);
                meat.ExpirationDate = int.Parse(split[4]);
                meat.CreationDate = split[5];
                if (!Enum.TryParse(split[6], true, out Category meatCategory))
                    throw new Exception("Incorrect category of meat");
                switch (split[6])
                {
                    case "HighestGrade":
                        meat.CategoryOfMeat = Category.HighestGrade;
                        break;
                    case "FirstSort":
                        meat.CategoryOfMeat = Category.FirstSort;
                        break;
                    case "SecondSort":
                        meat.CategoryOfMeat = Category.SecondSort;
                        break;
                    default:
                        throw new ArgumentException("Incorrect category of meat");
                }
                if (!Enum.TryParse(split[7], true, out Kind meatKind))
                    throw new Exception("Incorrect kind of meat");
                switch (split[7])
                {
                    case "Mutton":
                        meat.KindOfMeat = Kind.Mutton;
                        break;
                    case "Veal":
                        meat.KindOfMeat = Kind.Veal;
                        break;
                    case "Pork":
                        meat.KindOfMeat = Kind.Pork;
                        break;
                    case "Chicken":
                        meat.KindOfMeat = Kind.Chicken;
                        break;
                    default:
                        throw new ArgumentException("Incorrect kind of meat");
                }
                return meat;
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}
