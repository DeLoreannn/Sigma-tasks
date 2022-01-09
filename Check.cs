using System;

namespace Shop_cousework
{
    class Check
    {
        private string information;

        public string Information
        {
            get { return information; }
            set 
            {
                try
                {
                    if (value == "" || value == null)
                        throw new ArgumentException("Empty information");
                    information = value;
                }
                catch(ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public Check()
        {
            information = null;
        }

        public Check(string information)
        {
            Information = information;
        }

        public override string ToString()
        {
            return information;
        }
    }
}
