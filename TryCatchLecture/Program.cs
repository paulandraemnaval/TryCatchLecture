using Grpc.Core;
using System.ComponentModel.Design;
using TryCatchLecture;

namespace CustomExceptionDemo
{
    class Program
    {
        static void Main()
        {
            ShoppingCart sc = new("1234");

            sc.AddItem("item1");

            try
            {
                sc.RemoveItem("item2");
            }catch(ShoppingCart.ItemDoesNotExistException ex)
            {
                Console.WriteLine($"item {ex.AttemptedItem} does not exist");
            }
        }
    }
}