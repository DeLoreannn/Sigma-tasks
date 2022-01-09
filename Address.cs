using System;

namespace Shop_cousework
{
    enum Country { Ukraine, Poland, Sweden, Germany, France }
    enum Town { Kyiv, Warsaw, Stockholm, Berlin, Paris }
    class Address
    {
        private Country country;
        private Town town;
        private int postOfficeNumber;

        public Country Country
        {
            get { return country; }
            set { country = value; }
        }

        public Town Town
        {
            get { return town; }
            set { town = value; }
        }

        public int PostOfficeNumber
        {
            get { return postOfficeNumber; }
            set 
            {
                try
                {
                    if (value < 0)
                        throw new ArgumentException("Incorrect post office number");
                    postOfficeNumber = value;
                }
                catch(ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public Address()
        {
            country = Country.Ukraine;
            town = Town.Kyiv;
            postOfficeNumber = 0;
        }

        public Address(Country country, Town town, int postOfficeNumber)
        {
            this.Country = country;
            this.Town = town;
            this.PostOfficeNumber = postOfficeNumber;
        }

        public override string ToString()
        {
            return $"Country: {country}, town: {town}, post office number: {postOfficeNumber}";
        }
    }
}
