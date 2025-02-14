using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TryCatchLecture
{
    internal class ShoppingCart(string userID)
    {
        private Dictionary<string, int> _items = new();
        public string userId { get; private set; } = userID;
        private string[] promoCodes = { "promo1", "promo2" };

        public void AddItem(string item)
        {
            if (item == null || item == "")
            {
                Console.WriteLine("Please enter an item name");
                return;
            }

            if (_items.ContainsKey(item))
            {
                _items[item]++;
            }
            else
            {
                _items.Add(item, 1);
            }
        }

        public void RemoveItem(string item)
        {
            if (item == null || item == "")
            {
                Console.WriteLine("Please enter an item name");
                return;
            }
            if (_items.ContainsKey(item))
            {
                _items[item]--;
                if (_items[item] == 0)
                {
                    _items.Remove(item);
                }
            }
            else
            {
                throw new ItemDoesNotExistException(item, $"Item {item} does not exist in the shopping cart");
            }
        }

        public void ApplyPromoCode(string PromoCode)
        {
            if(PromoCode == null || PromoCode == "")
            {
                Console.WriteLine("please enter a promocode");
                return;
            }

            for(int i = 0; i <  promoCodes.Length; i++)
            {
                if (promoCodes[i] == PromoCode)
                {
                    Console.WriteLine("Promocode successfully applied!");
                }
            }

            throw new PromocodeNotFoundException(PromoCode, "promocode not found");
        }

        public void Checkout(bool hasInternet)
        {
            //vaildation logic
            if(_items.Count == 0)
            {
                Console.WriteLine("shopping cart is empty!");
                return;
            }

            Console.WriteLine("checking out...");

            //validation logic
            if(!hasInternet)
            {
                throw new CheckoutException(userId, "Please check your internet connection");
            }
            Console.WriteLine("checked out successfully");
        }

        //do not modify this method
        public void ViewCart()
        {
            Console.WriteLine("\nItems in cart:");
            Console.WriteLine("Item\t|Quantity");
            Console.WriteLine("------------------");
            foreach (var item in _items)
            {
                Console.WriteLine($"{item.Key}\t|{item.Value}");
            }
        }


        //implement custom exceptions as needed...

        public class ItemDoesNotExistException(string attemptedItem, string message) : Exception(message)
        {
            public string AttemptedItem { get; private set; } = attemptedItem;
        }

        public class CheckoutException(string userID, string message) : Exception(message)
        {
            public string UserID { get; private set; } = userID;
        }

        public class PromocodeNotFoundException(string promocode, string message) : Exception(message)
        {
            public string Promocode { get; private set; } = promocode;
        }
    }
}

