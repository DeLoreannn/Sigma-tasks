using System.Collections.Generic;

namespace Shop_cousework.Bases
{
    class CheckBase
    {
        private static CheckBase instance;

        private List<Check> checks;

        public List<Check> Checks
        {
            get { return checks; }
            private set { checks = value; }
        }

        private CheckBase()
        {
            checks = new List<Check>();
        }

        public static CheckBase GetInstance()
        {
            if (instance == null)
            {
                instance = new CheckBase();
            }
            return instance;
        }

        public override string ToString()
        {
            string result = "Checks\n";
            for (int i = 0; i < checks.Count; ++i)
            {
                result += checks[i].ToString();
            }
            return result;
        }
    }
}
