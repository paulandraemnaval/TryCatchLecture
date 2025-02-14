using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryCatchLecture
{
    [FirestoreData]
    internal class User
    {
        [FirestoreProperty]
        public string Email { get; set; }
        [FirestoreProperty]
        public string Password { get; set; }

        public User(string email, string pass) {
            if(email == "" || email == null || pass == "" || pass == null)
            {
                throw new ArgumentException("please enter a valid Email");
            }
            
            Email = email;
            Password = pass;
        }



        public User() { }
    }
}
