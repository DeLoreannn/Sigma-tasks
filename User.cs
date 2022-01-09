using System;
using Shop_cousework.Interfaces;

namespace Shop_cousework
{
    abstract class User : ILogIn
    {
        private string login;

        private string password;

        public string Login
        {
            get { return login; }
            set
            {
                try
                {
                    if (value == "")
                        throw new ArgumentException("Empty login");
                    for(int i = 0; i < value.Length; ++i)
                    {
                        if (!Char.IsLetterOrDigit(value[i]) && value[i] != '_' && value[i] != '.' && value[i] != '#')
                            throw new ArgumentException($"Login can not contain these symbols: {value[i]}");
                    }
                    login = value;
                }
                catch(ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                try
                {
                    if (value == "")
                        throw new ArgumentException("Empty password");
                    for (int i = 0; i < value.Length; ++i)
                    {
                        if (!Char.IsLetterOrDigit(value[i]) && value[i] != '_' && value[i] != '.' && value[i] != '#')
                            throw new ArgumentException($"Password can not contain these symbols: {value[i]}");
                    }
                    password = value;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public User()
        {
            login = null;
            password = null;
        }

        public User(string login, string password)
        {
            this.login = login;
            this.password = password;
        }

        public abstract User LogIn(string userData);
    }
}
