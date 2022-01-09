using System;
using System.Collections.Generic;
using Shop_cousework.Interfaces;

namespace Shop_cousework.Representatives
{
    class ConsoleRepresentation : IRepresentation
    {
        #region User part
        public User LogIn()
        {
            return UserPart.LogIn();
        }

        public void ViewShop(IViewShop user)
        {
            UserPart.ViewShop(user);
        }
        #endregion

        #region Client part
        public void AddGoodsToBasket(IBasketInteraction customer)
        {
            ClientPart.AddGoodsToBasket(customer);
        }
        public void RemoveGoodsFromBasket(IBasketInteraction customer)
        {
            ClientPart.RemoveGoodsFromBasket(customer);
        }
        public void ViewBasket(IBasketInteraction customer)
        {
            ClientPart.ViewBasket(customer);
        }
        public void MakeOrder(IMakeOrder customer, Dictionary<int, Goods> goods, double totalAmount)
        {
            ClientPart.MakeOrder(customer, goods, totalAmount);
        }
        public void ViewChecks(IViewChecks user)
        {
            ClientPart.ViewChecks(user);
        }
        #endregion

        #region Moderator part
        public void CreatePromotion(ICreatingPromotion moderator)
        {
            ModeratorPart.CreatePromotion(moderator);
        }
        public void DeletePromotion(ICreatingPromotion moderator)
        {
            ModeratorPart.DeletePromotion(moderator);
        }
        public void ViewPromotions(ICreatingPromotion moderator)
        {
            ModeratorPart.ViewPromotions(moderator);
        }
        public void AddGoodsToPromotion(IPromotionInteraction moderator)
        {
            ModeratorPart.AddGoodsToPromotion(moderator);
        }
        public void RemoveGoodsFromPromotion(IPromotionInteraction moderator)
        {
            ModeratorPart.RemoveGoodsFromPromotion(moderator);
        }
        public void ViewPlacedOrders(IOrderInteraction moderator)
        {
            ModeratorPart.ViewPlacedOrders(moderator);
        }
        public void DeletePlacedOrders(IOrderInteraction moderator)
        {
            ModeratorPart.DeletePlacedOrders(moderator);
        }
        #endregion

        #region Administrator part
        public void AddGoodsToStorage(IGoodsManagement administrator, string shopContent)
        {
            AdministratorPart.AddGoodsToStorage(administrator, shopContent);
        }
        public void RemoveGoodsFromStorage(IGoodsManagement administrator, string shopContent)
        {
            AdministratorPart.RemoveGoodsFromStorage(administrator, shopContent);
        }
        public void SetClientStatus(IClientStatus administrator)
        {
            AdministratorPart.SetClientStatus(administrator);
        }
        #endregion

        #region Guest part
        public Client SignUp(ISignUp guest)
        {
            return GuestPart.SignUp(guest);
        }
        public void EnterEmail(IEnterEmail guest)
        {
            GuestPart.EnterEmail(guest);
        }
        public Goods ChooseGoods(Guest guest)
        {
            return GuestPart.ChooseGoods(guest);
        }
        #endregion

        #region Interaction with shop
        public void InteractionWithShop(Client client)
        {
            Console.Clear();
            Console.WriteLine("You have registered as a client.");
            bool exit = true;
            while (exit)
            {
                Console.WriteLine("1) View shop;\n2) Add goods to basket;\n3) Remove goods from basket;\n4) View basket;\n5) Make order;\n6) Print check;\n7) Exit;");
                if (!int.TryParse(Console.ReadLine(), out int choice))
                    Console.WriteLine("Incorrect choice. Please, try again.");
                else
                {
                    switch (choice)
                    {
                        case 1:
                            UserPart.ViewShop(client);
                            break;
                        case 2:
                            AddGoodsToBasket(client);
                            break;
                        case 3:
                            RemoveGoodsFromBasket(client);
                            break;
                        case 4:
                            ViewBasket(client);
                            break;
                        case 5:
                            if (client.IsBasketEmpty())
                            {
                                Console.Clear();
                                Console.WriteLine("Basket is empty. Add some goods to make order.\n");
                                break;
                            }
                            MakeOrder(client, client._Basket.Goods, client._Basket.TotalAmount);
                            break;
                        case 6:
                            ViewChecks(client);
                            break;
                        case 7:
                            Console.Clear();
                            exit = false;
                            break;
                        default:
                            Console.WriteLine("Incorrect choice. Please, try again.");
                            break;
                    }
                }
            }
        }

        public void InteractionWithShop(Moderator moderator)
        {
            Console.Clear();
            Console.WriteLine("You have registered as a moderator.");
            bool exit = true;
            while (exit)
            {
                Console.WriteLine("1) View shop;\n2) Create promotions;\n3) Delete promotion;\n4) View promotions;\n5) Add goods to promotion;\n6) Remove goods from promotion;\n" +
                    "7) View placed orders;\n8) Delete placed order;\n9) Exit;");
                if (!int.TryParse(Console.ReadLine(), out int choice))
                    Console.WriteLine("Incorrect choice. Please, try again.");
                else
                {
                    switch (choice)
                    {
                        case 1:
                            UserPart.ViewShop(moderator);
                            break;
                        case 2:
                            CreatePromotion(moderator);
                            break;
                        case 3:
                            DeletePromotion(moderator);
                            break;
                        case 4:
                            ViewPromotions(moderator);
                            break;
                        case 5:
                            AddGoodsToPromotion(moderator);
                            break;
                        case 6:
                            RemoveGoodsFromPromotion(moderator);
                            break;
                        case 7:
                            ViewPlacedOrders(moderator);
                            break;
                        case 8:
                            DeletePlacedOrders(moderator);
                            break;
                        case 9:
                            Console.Clear();
                            exit = false;
                            break;
                        default:
                            Console.WriteLine("Incorrect choice. Please, try again.");
                            break;
                    }
                }
            }
        }

        public void InteractionWithShop(Administrator administrator)
        {
            Console.Clear();
            Console.WriteLine("You have registered as a administrator.");
            bool exit = true;
            while (exit)
            {
                Console.WriteLine("1) View shop;\n2) Add goods to storage;\n3) Remove goods from storage;\n4) Set client status;\n5) View checks;\n6) Exit;");
                if (!int.TryParse(Console.ReadLine(), out int choice))
                    Console.WriteLine("Incorrect choice. Please, try again.");
                else
                {
                    switch (choice)
                    {
                        case 1:
                            UserPart.ViewShop(administrator);
                            break;
                        case 2:
                            AddGoodsToStorage(administrator, administrator.ViewShop());
                            break;
                        case 3:
                            RemoveGoodsFromStorage(administrator, administrator.ViewShop());
                            break;
                        case 4:
                            SetClientStatus(administrator);
                            break;
                        case 5:
                            ViewChecks(administrator);
                            break;
                        case 6:
                            Console.Clear();
                            exit = false;
                            break;
                        default:
                            Console.WriteLine("Incorrect choice. Please, try again.");
                            break;
                    }
                }
            }
        }

        public void InteractionWithShop(Guest guest)
        {
            Console.Clear();
            Console.WriteLine("You have entered as a guest.");
            bool exit = true;
            while (exit)
            {
                Console.WriteLine("1) Sign up;\n2) View shop;\n3) Enter email;\n4) Make order;\n5) Exit;");
                if (!int.TryParse(Console.ReadLine(), out int choice))
                    Console.WriteLine("Incorrect choice. Please, try again.");
                else
                {
                    switch (choice)
                    {
                        case 1:
                            Client client = SignUp(guest);
                            guest.SignUp(client);
                            InteractionWithShop(client);
                            exit = false;
                            break;
                        case 2:
                            UserPart.ViewShop(guest);
                            break;
                        case 3:
                            EnterEmail(guest);
                            break;
                        case 4:
                            if (guest.Email == null)
                                EnterEmail(guest);
                            Goods goods = ChooseGoods(guest);
                            Dictionary<int, Goods> _goods = new Dictionary<int, Goods>();
                            _goods.Add(goods.Identifier, goods);
                            MakeOrder(guest, _goods, goods.Price);
                            break;
                        case 5:
                            Console.Clear();
                            exit = false;
                            break;
                        default:
                            Console.WriteLine("Incorrect choice. Please, try again.");
                            break;
                    }
                }
            }
        }
        #endregion
        public void RepresentShop()
        {
            while (true)
            {
                Console.WriteLine("Do you have registered account? 1 - Yes, 2 - No, 3 - Exit");
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    if (choice == 1)
                    {
                        User user = UserPart.LogIn();
                        if (user is Client)
                            InteractionWithShop(user as Client);
                        else if (user is Moderator)
                            InteractionWithShop(user as Moderator);
                        else if (user is Administrator)
                            InteractionWithShop(user as Administrator);
                    }
                    else if (choice == 2)
                    {
                        Guest guest = new Guest();
                        InteractionWithShop(guest);
                    }
                    else if (choice == 3)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Incorrect choice. Please, try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect choice. Please, try again.");
                }
            }
        }
    }
}
