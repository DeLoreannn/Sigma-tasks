using System.Collections.Generic;

namespace Shop_cousework
{
    class UserBase
    {
        private static UserBase instance;

        private List<User> users;

        public List<User> Users
        {
            get { return users; }
        }

        private UserBase()
        {
            users = new List<User>();
        }

        public static UserBase GetInstance()
        {
            if (instance == null)
            {
                instance = new UserBase();
            }
            return instance;
        }
        
        public override string ToString()
        {
            string result = string.Format("{0,-17}{1,-15}{2,-15}{3,-15}{4,-15}{5,-15}{6,-15}", "Type of user", "Login", "Password", "Name", "Surname", "Vip status", "Phone number") + "\n";
            for (int i = 0; i < Users.Count; ++i)
            {
                result += Users[i].ToString() + "\n";
            }
            return result;
        }
    }
}
