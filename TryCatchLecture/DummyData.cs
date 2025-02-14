using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace TryCatchLecture
{
    [FirestoreData]
    internal class DummyData
    {
        [FirestoreProperty]
        public int Number1 { get; set; }
        [FirestoreProperty]
        public int Number2 { get; set; }
        [FirestoreProperty]
        public string Description { get; set; }

        public DummyData() {
            Description = null!;
        }

        public DummyData(int num1, int num2, string description)
        {
            Number1 = num1;
            Number2 = num2;
            Description = description;
        }

        public void PrintAttribs()
        {
            Console.WriteLine("Dummy data attributes:");
            Console.WriteLine($"Number1: {Number1}");
            Console.WriteLine($"Number2: {Number2}");
            Console.WriteLine($"Description: {Description}");
        }
    }
}
