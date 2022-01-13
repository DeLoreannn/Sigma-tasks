using System;
using System.Collections.Generic;

namespace Task18
{
    class Program
    {
        static void Main(string[] args)
        {
            Product[] products =
            {
                new Product("Surprise", 80, 120),
                new Product("Snikers", 30, 110),
                new Product("Bounty", 70, 210),
                new Product("Mars", 40, 180)
            };

            QuickSortRecursion.QuickSort(products, "name", 0, products.Length - 1, SortOrder.Descending, (x) => x.Name.Length < 8);
            foreach (Product product in products)
            {
                Console.WriteLine(product);
            }

            Console.WriteLine();

            QuickSortIteration.QuickSort(products, "weight", 0, products.Length - 1, SortOrder.Ascending);
            foreach (Product product in products)
            {
                Console.WriteLine(product);
            }

            Console.WriteLine();

            HeapSort.Sort(products, "price", 0, products.Length, SortOrder.Ascending);
            foreach (Product product in products)
            {
                Console.WriteLine(product);
            }
        }
    }
}
