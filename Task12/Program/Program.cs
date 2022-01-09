using Shop_cousework.Representatives;

namespace Shop_cousework
{
    class Program
    {
        static void Main(string[] args)
        {
            Shop shop = Shop.GetInstance();
            shop.SetRepresentation(new ConsoleRepresentation());
            shop.ShopInteraction();
        }
    }
}
