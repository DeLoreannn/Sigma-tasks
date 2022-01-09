using System;

namespace Shop_cousework
{
    class Technique : Goods
    {
        private int batteryCapacity;
        private int warrantyPeriod;

        public int BatteryCapacity
        {
            get { return batteryCapacity; }
            set
            {
                try
                {
                    if (value < 0)
                        throw new ArgumentException("Incorrect battery capacity");
                    batteryCapacity = value;
                }
                catch(ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public int WarrantyPeriod
        {
            get { return warrantyPeriod; }
            set
            {
                try
                {
                    if (value < 0)
                        throw new ArgumentException("Incorrect warranty period");
                    warrantyPeriod = value;
                }
                catch(ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public Technique(): base()
        {
            batteryCapacity = 0;
            warrantyPeriod = 0;
        }

        public Technique(int identifier, string name, double price, double weight, string creationDate, int batteryCapacity, int warrantyPeriod): base(identifier, name, price, weight, creationDate)
        {
            BatteryCapacity = batteryCapacity;
            WarrantyPeriod = warrantyPeriod;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType())
                return false;
            var other = (Technique)obj;
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
                return $"Identifier: {Identifier}, name: {Name}, price: {Price} grn, weight: {Weight}g, creation date: {CreationDate}, battery capacity: {BatteryCapacity} mA, warranty period: {WarrantyPeriod} days";
            }
            else
            {
                return "";
            }
        }
        public new Technique Copy()
        {
            return (Technique)this.MemberwiseClone();
        }

        public new static Technique Parse(string tape)
        {
            string[] split = tape.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            try
            {
                if (split.Length != 7)
                    throw new Exception($"Incorrect number of values in tape: {tape}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {
                if (!int.TryParse(split[0], out int addIdentifier))
                    throw new FormatException("Incorrect format of technique identifier");
                if (!double.TryParse(split[2], out double addPrice))
                    throw new FormatException("Incorrect format of technique price");
                if (!double.TryParse(split[3], out double addWeight))
                    throw new FormatException("Incorrect format of technique weight");
                if (!int.TryParse(split[5], out int addBatteryCapacity))
                    throw new FormatException("Incorrect format of technique battery capacity");
                if (!int.TryParse(split[6], out int addWarrantyPeriod))
                    throw new FormatException("Incorrect format of technique warranty period");
                Technique technique = new Technique();
                technique.Identifier = int.Parse(split[0]);
                technique.Name = split[1];
                technique.Price = double.Parse(split[2]);
                technique.Weight = double.Parse(split[3]);
                technique.CreationDate = split[4];
                technique.BatteryCapacity = int.Parse(split[5]);
                technique.WarrantyPeriod = int.Parse(split[6]);
                return technique;
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}
