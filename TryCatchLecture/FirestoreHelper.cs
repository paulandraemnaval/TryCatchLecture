using Google.Cloud.Firestore;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TryCatchLecture
{
    internal static class FirestoreHelper
    {
        static string fireConfig = @"{
          ""type"": ""service_account"",
          ""project_id"": ""trycatchdemo"",
          ""private_key_id"": ""d3d482539e315eb3273edfd9e1e3a224df7ae48e"",
          ""private_key"": ""-----BEGIN PRIVATE KEY-----\nMIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQDiKj8FJv4kqaaX\nCajhKGjotrN7IF7cb3+KnEB1AoYzND/7MGN+sLRUY8WifpuBJcvPR0WLlhOZnJ63\n34Of83iiO19lu1+m1sMmNr1pfSkzFHPqPtkmd5MCWl28ot/LZ8BlSTZ8gGOwPBh9\n72T57WTNYE2C5QvNFTgArgiDyE0i9VREVRYc8NbDTUoS8xRGnUZV6AfEichmyM8k\nqufL6WmgSw/sNiYhaUZa8REIKZuasLM22giow1g/CwKNHhfZK0pATjN/v/jnN9EE\nl1KBJgNOloAPX9wcC+rtvjxxuly7+6QeQRmfgkVqadhbVD6pUcJOd1aQM7kLS9rJ\nnfYDQ1rrAgMBAAECggEAJBekgainW30iHhhnNbIX4TYcORoBXsxXCO0b8ZBlj5EB\nujm5UbksDgueDpXbMHuyUByKTKxhPwfE4d3ESP0MfgIGR1s6TeBtZDvNc82K4Evz\nSP6enjTsO66QJvpscdmCKqfJujSttAI4e7XTN3oFttYXiJFk62WSJQmg8jw/kxmm\nyw7u37I9s7ZrewPnTslADi5DtqBJX3B33VEwnLYczDeYZsWv2a86hz1kKr7TMlkQ\ndjNDa9DAF8/YjiZPd09cU71q/A6PSl/R8yCGkXNxpDQUgwTubOVtRlHvyg4oMvnE\nT2bM/L43qIljBnXd5EfqW4vLfmTuIQiKLRaQyl8agQKBgQD27KPF9U8SLpTIXGTj\nYeStOZmW0gHbRjAW1NQOUijrxqp66teYsd7L/xQBl7R73GRg80cPwIGdsF55msdE\n1Vm59PqkSS0uvu+y+/WbzRPwD4+/pYVX2RZ1D3ok1HhSjrXuLxdLzuK/BIdisDb6\nKfZdW2z+yHqjFfP7NBgLhMuTxQKBgQDqekcRIoNqGP484WO08JZK/jEFplMvlO7U\n9KCX3M1QO488qnkFjZbG1K97VPg+AFCw7XQO/fStKaIVEjhVv9VCrJr/xc83NJMj\n93g5VMnOyJyqcEe/fAskpHQdMf/tsK8DYmyZ5s1/92rboNg7ohIPnkvVxLXG1RDX\n3+p88Xsu7wKBgQCmPxTUxyCBgVMywuI6JUKtTkaWQLZ3R35BIPHU4oQimhNVxp95\ncugqOIbju5wMeIosrB6AAIBEBubUwNzA/1P123aU/Z+qBWuk+faW+zAdRJznzy1D\nxClWjyERguuvyd27i4EHzTbknMv6KeIZ8/6nRFLSB3BqNPGbg6tctf/KcQKBgEqz\nYBa+Zx2TDtQ4fjpz932208lX/uKG0Tv5H3yiNKrd/rk2Gk5BmIqJ0co5/MhL82ka\njUIFeED+pYuO/XGcJwYR1WOtEmIlFsd7nMqvD4gYc4j9Mm53x1kKJ4/xkPaZtnge\nkDjLxiaBnYKRELKW5KgjJ0fYXo0U7kPIK31YtYTzAoGANembkPzU/xGiN/DUjLCv\nSxYViOgRkn/BXExLFpXomWUWhNLuFwmzMFME3fDvRz0yoBef+tvQE7pBr2StBa/O\nKOmQgPGQkkqR/ehVxIMmP9FRuLb0qR2W0RMcqj2q6AL0jaNRTJ38nFlCv7ol6GZI\ntctIYmnBfsGyipf7K63IQho=\n-----END PRIVATE KEY-----\n"",
          ""client_email"": ""firebase-adminsdk-fbsvc@trycatchdemo.iam.gserviceaccount.com"",
          ""client_id"": ""107584722727877397065"",
          ""auth_uri"": ""https://accounts.google.com/o/oauth2/auth"",
          ""token_uri"": ""https://oauth2.googleapis.com/token"",
          ""auth_provider_x509_cert_url"": ""https://www.googleapis.com/oauth2/v1/certs"",
          ""client_x509_cert_url"": ""https://www.googleapis.com/robot/v1/metadata/x509/firebase-adminsdk-fbsvc%40trycatchdemo.iam.gserviceaccount.com"",
          ""universe_domain"": ""googleapis.com""
        }";

        static string filePath = "";
        public static FirestoreDb Database { get; private set; } = null!;

        public static void InitializeFireStore()
        {
            filePath = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(Path.GetRandomFileName())) + ".json";
            File.WriteAllText(filePath, fireConfig);
            File.SetAttributes(filePath, FileAttributes.Hidden);
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", filePath);
            Database = FirestoreDb.Create("trycatchdemo");
            File.Delete(filePath);
        }

        public static async Task UploadDummyData()
        {
            // Create dummy data
            int num1 = new Random().Next(1, 100);
            int num2 = new Random().Next(1, 100);
            string desc = "This is dummy data";
            DummyData dd = new(num1, num2, desc);

            // Upload dummy data
            CollectionReference collection = Database.Collection("DummyData");
            await collection.AddAsync(dd);

            // Print dummy data
            Console.WriteLine("Done adding dummy data");
            dd.PrintAttribs();
        }

       
        
        public static async Task CheckEmail(User userarg)
        {
            if(userarg.Email == null || userarg.Email == "")
            {
                return;
            }

            //get the data from server
            CollectionReference collection = Database.Collection("Users");
            QuerySnapshot snapshot = await collection.GetSnapshotAsync();

            //check if input email is already in database
            try
            {
                List<string> EmailList = new List<string>();
                if (EmailList.Contains(userarg.Email))
                {
                    foreach (DocumentSnapshot document in snapshot.Documents)
                    {
                        User user = document.ConvertTo<User>();
                        EmailList.Add(user.Email);
                    }
                    //check
                    if (EmailList.Contains(userarg.Email))
                    {
                        throw new EmailAlreadyExistsException(userarg.Email, "this email already exists!");
                    }
                    else
                    {
                        await collection.AddAsync(userarg);
                        Console.WriteLine("User Added");
                    };
                }
            }
            catch (EmailAlreadyExistsException ex)
            {
                Console.WriteLine($"email {ex.attemptedEmail} alreaady exists");
            }
        }
        public class EmailAlreadyExistsException(string email, string message) : Exception(message)
        {
            public string attemptedEmail { get; private set; } = email;
        }
        }
    }
}