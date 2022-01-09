using System;
using Shop_cousework.Interfaces;
using Shop_cousework.AdditionalClasses;

namespace Shop_cousework.Representatives
{
    static class UserPart
    {
        static private User LogInUser(ILogIn user, string userData)
        {
            return user.LogIn(userData);
        }

        static public User LogIn()
        {
            string login, password;

            while (true)
            {
                Console.WriteLine("Enter your login:");
                login = Console.ReadLine();
                if (login == "" || !SearchFunctions.SearchUserByLogin(UserBase.GetInstance().ToString(), login))
                    Console.WriteLine("Incorrect login. Please, try again.");
                else
                    break;
            }

            while (true)
            {
                Console.WriteLine("Enter your password:");
                password = Console.ReadLine();
                if (password == "" || !SearchFunctions.SearchUserByPassword(UserBase.GetInstance().ToString(), login, password))
                    Console.WriteLine("Incorrect password. Please, try again.");
                else
                    break;
            }

            string[] userData = SearchFunctions.SearchUserByLoginAndRetUserData(UserBase.GetInstance().ToString(), login).Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string userTape = "";
            for (int i = 0; i < userData.Length; ++i)
            {
                if (i == (userData.Length - 1))
                    userTape += userData[i];
                else
                    userTape += userData[i] + " ";
            }

            try
            {
                switch (userData[0])
                {
                    case "Client":
                        return LogInUser(new Client(), userTape);
                    case "Moderator":
                        return LogInUser(new Moderator(), userTape);
                    case "Administrator":
                        return LogInUser(new Administrator(), userTape);
                    default:
                        throw new ArgumentException("Undeclared type of user");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        static public void ViewShop(IViewShop user)
        {
            Console.Clear();
            Console.WriteLine(user.ViewShop());
        }
    }
}
