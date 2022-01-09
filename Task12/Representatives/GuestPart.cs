using System;
using Shop_cousework.Interfaces;
using Shop_cousework.AdditionalClasses;

namespace Shop_cousework.Representatives
{
    static class GuestPart
    {
        static public Client SignUp(ISignUp guest)
        {
            Client client = new Client();
            string login, password, name, surname, vipStatus, phoneNumber;
            while (true)
            {
                Console.WriteLine("Enter your login:");
                login = Console.ReadLine();
                if (login == "")
                    Console.WriteLine("Incorrect login. Please, try again.");
                else
                {
                    if (SearchFunctions.SearchUserByLogin(UserBase.GetInstance().ToString(), login))
                        Console.WriteLine($"User with login {login} has already registered. Please, try again.");
                    else
                        break;
                }
            }

            while (true)
            {
                Console.WriteLine("Enter your password:");
                password = Console.ReadLine();
                for (int i = 0; i < password.Length; ++i)
                {
                    if (password[i] == ' ')
                        Console.WriteLine("Password can not contain spaces");
                }
                if (password == "" || password.Length > 15)
                    Console.WriteLine("Incorrect password. Please, try again.");
                else
                    break;
            }

            bool correctName = true, correctSurname = true, correctPhoneNumber = true;

            while (true)
            {
                Console.WriteLine("Enter name:");
                name = Console.ReadLine();
                correctName = true;
                if (name == "")
                    Console.WriteLine("Incorrect name. Please, try again.");
                else
                {
                    if (!Char.IsUpper(name[0]))
                        Console.WriteLine("Name should start with capital letter. Please, try again.");
                    else
                    {
                        for (int i = 0; i < name.Length; ++i)
                        {
                            if (!Char.IsLetter(name[i]))
                            {
                                Console.WriteLine("Name should consist of only letters. Please, try again.");
                                correctName = false;
                                break;
                            }
                        }
                        if (correctName == true)
                        {
                            break;
                        }
                    }
                }
            }

            while (true)
            {
                Console.WriteLine("Enter surname:");
                surname = Console.ReadLine();
                correctSurname = true;
                if (surname == "")
                    Console.WriteLine("Incorrect surname. Please, try again.");
                else
                {
                    if (!Char.IsUpper(surname[0]))
                        Console.WriteLine("Surname should start with capital letter. Please, try again.");
                    else
                    {
                        for (int i = 0; i < surname.Length; ++i)
                        {
                            if (!Char.IsLetter(surname[i]))
                            {
                                Console.WriteLine("Surname should consist of only letters. Please, try again.");
                                correctSurname = false;
                                break;
                            }
                        }
                        if (correctSurname == true)
                        {
                            break;
                        }
                    }
                }
            }

            while (true)
            {
                Console.WriteLine("Enter vip status (True of False):");
                vipStatus = Console.ReadLine();
                if (vipStatus == "")
                    Console.WriteLine("Incorrect status. Please, try again.");
                else
                {
                    if (!bool.TryParse(vipStatus, out bool addVipStatus))
                        Console.WriteLine("Status should be True or False. Please, try again.");
                    else
                    {
                        break;
                    }
                }
            }

            while (true)
            {
                Console.WriteLine("Enter phone number (length 5..10):");
                phoneNumber = Console.ReadLine();
                correctPhoneNumber = true;
                if (phoneNumber == "")
                    Console.WriteLine("Incorrect phone number. Please, try again.");
                else
                {
                    if (phoneNumber.Length < 5 || phoneNumber.Length > 10)
                        Console.WriteLine("Incorrect length of phone number. Please, try again.");
                    else
                    {
                        for (int i = 0; i < phoneNumber.Length; ++i)
                        {
                            if (!Char.IsDigit(phoneNumber[i]))
                            {
                                Console.WriteLine("Phone number should consist of only numbers. Please, try again.");
                                correctPhoneNumber = false;
                                break;
                            }
                        }
                        if (correctPhoneNumber == true)
                        {
                            break;
                        }
                    }
                }
            }

            client.Login = login;
            client.Password = password;
            client.Name = name;
            client.Surname = surname;
            client.VipStatus = bool.Parse(vipStatus);
            client.PhoneNumber = phoneNumber;
            return client;
        }

        static public void EnterEmail(IEnterEmail guest)
        {
            while (true)
            {
                Console.WriteLine("Enter email:");
                string email = Console.ReadLine();
                string[] parts = email.Split('@', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 2)
                    Console.WriteLine("Email should consists of two parts");
                else
                {
                    bool correctData = true;
                    for (int i = 0; i < parts[0].Length; ++i)
                    {
                        if (!Char.IsLetterOrDigit(parts[0][i]) && parts[0][i] != '.')
                        {
                            Console.WriteLine($"Email can not contain this symbol: {parts[0][i]}");
                            correctData = false;
                        }
                    }
                    if (correctData == true)
                    {
                        string[] addressParts = parts[1].Split('.', StringSplitOptions.RemoveEmptyEntries);
                        if (addressParts.Length != 2)
                        {
                            Console.WriteLine("Email address should consists of two parts");
                            correctData = false;
                        }
                        if (correctData == true)
                        {
                            for (int i = 0; i < addressParts.Length; ++i)
                            {
                                for (int j = 0; j < addressParts[i].Length; ++j)
                                {
                                    if (!Char.IsLetter(addressParts[i][j]))
                                    {
                                        Console.WriteLine("Email address can contain only letters");
                                        correctData = false;
                                    }
                                }
                            }
                            if (correctData == true)
                            {
                                guest.EnterEmail(email);
                                break;
                            }
                        }
                    }
                }
            }
        }

        static public Goods ChooseGoods(Guest guest)
        {
            Console.WriteLine(guest.ViewShop());
            int identifier = 0;
            while (true)
            {
                Console.WriteLine("Enter identifier:");
                if (int.TryParse(Console.ReadLine(), out int addIdentifier))
                {
                    if (SearchFunctions.SearchGoodsInShopByIdentifier(addIdentifier))
                    {
                        identifier = addIdentifier;
                        break;
                    }
                    else
                        Console.WriteLine("Incorrect identifier. Please, try again.");
                }
                else
                    Console.WriteLine("Incorrect identifier. Please, try again.");
            }
            return Shop.GetInstance().GetGoods(identifier);
        }
    }
}
