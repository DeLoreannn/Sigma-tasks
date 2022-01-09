using System;

namespace Shop_cousework
{
    delegate void LogIntoFile(string message, string path);
    delegate Goods CorrectInput(Goods product, string mistake);
    class Goods
    {
        private int identifier;
        private string name;
        private double price;
        private double weight;
        private string creationDate;

        #region Properties
        public int Identifier
        {
            get { return identifier; }
            set
            {
                try
                {
                    if (value < 0)
                        throw new ArgumentException("Incorrect value of identifier");
                    identifier = value;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                try
                {
                    if (value == "")
                        throw new ArgumentNullException("Empty name of goods");
                    name = value;
                }
                catch (ArgumentNullException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public double Price
        {
            get { return price; }
            set
            {
                try
                {
                    if (!double.TryParse(Convert.ToString(value), out double goodsPrice))
                        throw new FormatException("Incorrect format of goods price");
                    if (value < 0)
                        throw new ArgumentException("Incorrect price of goods");
                    price = value;
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public double Weight
        {
            get { return weight; }
            set
            {
                try
                {
                    if (!double.TryParse(Convert.ToString(value), out double goodsWeight))
                        throw new FormatException("Incorrect format of product weight");
                    if (value < 0)
                        throw new ArgumentException("Incorrect weight of product");
                    weight = value;
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public string CreationDate
        {
            get { return creationDate; }
            set
            {
                try
                {
                    if (value == null)
                        creationDate = null;
                    else
                    {
                        string[] values = value.Split(".");
                        if (values.Length != 3)
                            throw new ArgumentException("Incorrect number of values in tape of creation date");
                        if (!int.TryParse(values[0], out int days) || !int.TryParse(values[1], out int months) || !int.TryParse(values[2], out int years))
                        {
                            throw new FormatException("Wrong input formats of values in tape");
                        }
                        switch (months)
                        {
                            case 4:
                            case 6:
                            case 9:
                            case 11:
                                if (int.Parse(values[0]) < 0 || int.Parse(values[0]) > 30)
                                    throw new ArgumentException($"Wrong number of days ({days}) in month ({months}) in tape");
                                break;
                            case 2:
                                if (DateTime.IsLeapYear(years))
                                    if (int.Parse(values[0]) < 0 || int.Parse(values[0]) > 29)
                                        throw new ArgumentException($"Wrong nubmer of days ({days}) in month ({months}) in leap year");
                                    else if (int.Parse(values[0]) < 0 || int.Parse(values[0]) > 28)
                                        throw new ArgumentException($"Wrong nubmer of days ({days}) in month ({months})");
                                break;
                            default:
                                if (int.Parse(values[0]) < 0 || int.Parse(values[0]) > 31)
                                    throw new ArgumentException($"Wrong number of days ({days}) in month ({months}) in tape");
                                break;
                        }
                        if ((int.Parse(values[1]) <= 0) || (int.Parse(values[1]) > 12))
                        {
                            throw new ArgumentException($"Wrong input of months ({months}) in tape");
                        }
                        if ((int.Parse(values[2]) < 1900) || (int.Parse(values[2]) > 2021))
                        {
                            throw new ArgumentException($"Wrong input of years ({years}) in tape");
                        }
                        creationDate = values[0] + "." + values[1] + "." + values[2];
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        #endregion

        public Goods()
        {
            name = null;
            price = 0;
            weight = 0;
            creationDate = null;
            identifier = 0;
        }
        public Goods(int identifier, string name, double price, double weight, string creationDate)
        {
            this.Identifier = identifier;
            this.Name = name;
            this.Price = price;
            this.Weight = weight;
            this.CreationDate = creationDate;
        }

        public new virtual bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType())
                return false;
            var other = (Goods)obj;
            return (other.Name == this.Name);
        }
        public new virtual int GetHashCode()
        {
            return (int)Price ^ (int)Weight;
        }
        public override string ToString()
        {
            if (this != null)
            {
                return $"Identifier: {Identifier}, name: {Name}, price: {Price} grn, weight: {Weight}g, creation date: {CreationDate}";
            }
            else
            {
                return "";
            }
        }
        public Goods Copy()
        {
            return (Goods)this.MemberwiseClone();
        }

        public double ChangePrice(int percentage)
        {
            double standardPercentage = 1;
            standardPercentage += (double)percentage / 100;
            Price *= standardPercentage;
            return Price;
        }

        public static Goods Parse(string tape)
        {
            string[] split = tape.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            try
            {
                if (split.Length != 5)
                    throw new Exception($"Incorrect number of values in tape: {tape}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {
                if (!int.TryParse(split[0], out int addIdentifier))
                    throw new FormatException("Incorrect format of goods identifier");
                if (!double.TryParse(split[2], out double addPrice))
                    throw new FormatException("Incorrect format of goods price");
                if (!double.TryParse(split[3], out double addWeight))
                    throw new FormatException("Incorrect format of goods weight");
                Goods goods = new Goods();
                goods.Identifier = int.Parse(split[0]);
                goods.Name = split[1];
                goods.Price = double.Parse(split[2]);
                goods.Weight = double.Parse(split[3]);
                goods.CreationDate = split[4];
                return goods;
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}
