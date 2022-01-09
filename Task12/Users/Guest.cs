using System;
using Shop_cousework.Interfaces;

namespace Shop_cousework
{
    delegate void RegisterClient(Client client);
    class Guest : ISignUp, IEnterEmail, IMakeOrder, IViewShop
    {
        public event RegisterClient OnRegisterClient;
        private string email;

        public string Email
        {
            get { return email; }
            set 
            {
                try
                {
                    string[] parts = value.Split('@', StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length != 2)
                        throw new ArgumentException("Email should consists of two parts");
                    for (int i = 0; i < parts[0].Length; ++i)
                    {
                        if (!Char.IsLetterOrDigit(parts[0][i]) && parts[0][i] != '.')
                            throw new ArgumentException($"Email can not contain this symbol: {parts[0][i]}");
                    }
                    string[] addressParts = parts[1].Split('.', StringSplitOptions.RemoveEmptyEntries);
                    if (addressParts.Length != 2)
                        throw new ArgumentException("Email address should consists of two parts");
                    for (int i = 0; i < addressParts.Length; ++i)
                    {
                        for (int j = 0; j < addressParts[i].Length; ++j)
                        {
                            if (!Char.IsLetter(addressParts[i][j]))
                                throw new ArgumentException("Email address can contain only letters");
                        }
                    }
                    email = value;
                }
                catch(ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public Guest()
        {
            email = null;
        }

        public void SignUp(Client client)
        {
            OnRegisterClient += Shop.GetInstance().RegisterClient;
            OnRegisterClient?.Invoke(client);
        }

        public string ViewShop()
        {
            return Shop.GetInstance().ViewStorageContent();
        }

        public void EnterEmail(string email)
        {
            Email = email;
        }

        public void SendCheckToEmail(Check check)
        {
            Console.WriteLine(check.Information);
        }

        public void MakeOrder(Order order)
        {
            switch (order.DeliveryMethod)
            {
                case DeliveryMethod.NewPost:
                    order.TotalAmount += 70;
                    break;
                case DeliveryMethod.UkrPost:
                    order.TotalAmount += 30;
                    break;
                case DeliveryMethod.Courier:
                    order.TotalAmount += 100;
                    break;
            }
            SendCheckToEmail(Shop.GetInstance().ConfirmateOrder(order, order.Goods));
        }
    }
}
