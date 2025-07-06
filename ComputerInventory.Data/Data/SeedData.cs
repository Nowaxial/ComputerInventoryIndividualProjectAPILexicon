using Bogus;
using ComputerInventory.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComputerInventory.Data.Data
{
    public static class SeedData
    {
        public static async Task Initialize(ComputerInventoryContext db)
        {
            if (await db.Inventories.AnyAsync())
            {
                return; // Database has been seeded
            }

            try
            {
                var inventories = GenerateInventories(5);
                db.AddRange(inventories);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private static List<Inventory> GenerateInventories(int nrOfInventories)
        {
            var faker = new Faker<Inventory>("sv").Rules((f, c) =>
            {
                // Slumpa antal användare
                int userCount = 5;
                var users = GenerateUsers(userCount);

                // Slumpa en användare för att bestämma datornamn
                var selectedUser = users.ElementAt(f.Random.Int(0, users.Count - 1));

                // Bestäm prefix beroende på användarens position
                string computerPrefix = selectedUser.Position switch
                {
                    "Developer" => "DEV",
                    "Admin" => "ADMIN",
                    "Manager" => "MGR",
                    "Office" => "OFF",
                    _ => "WORKSTATION"
                };

                c.Address = f.Address.StreetAddress();
                c.City = f.Address.City();
                c.Users = users;
                c.Name = f.Company.CompanyName();
            });

            return faker.Generate(nrOfInventories);
        }

        private static ICollection<User> GenerateUsers(int nrOfUsers)
        {
            string[] positions = { "Developer", "Office", "Manager", "Admin" };

            var faker = new Faker<User>("sv").Rules((f, e) =>
            {
                e.Name = f.Person.FullName;
                e.Email = f.Person.Email;
                e.Position = f.PickRandom(positions);

                //string computerPrefix = e.Position switch
                //{
                //    "Developer" => "DEV",
                //    "Admin" => "ADMIN",
                //    "Manager" => "MGR",
                //    "Office" => "OFF",
                //    _ => "WORKSTATION"
                //};


                //e.MAC = f.Internet.Mac();
                //e.IP = f.Internet.IpAddress().ToString();
                e.ComputerName = GenerateComputerName(e.Position, e.Name);
            });

            return faker.Generate(nrOfUsers);
        }

        // Hjälpmetod för att generera slumpmässiga datornamn med prefix
        //private static string GenerateComputerName(string prefix)
        //{
        //    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        //    var random = new Random();
        //    var suffix = new char[7];

        //    for (int i = 0; i < suffix.Length; i++)
        //    {
        //        suffix[i] = chars[random.Next(chars.Length)];
        //    }

        //    return $"{prefix}-{new string(suffix)}";
        //}

        //Jag vill få  denna att generera datornamn baserat på position och namn men namnet ska innehålla mer än 3 tecken

        private static string GenerateComputerName(string position, string name)
        {
            string prefix = position switch
            {
                "Developer" => "DEV",
                "Admin" => "ADMIN",
                "Manager" => "MGR",
                "Office" => "OFF",
                _ => "WORKSTATION"
            };

            var nameParts = name.Split(' ');
            string firstName = nameParts.Length > 0 && nameParts[0].Length >= 3
                ? nameParts[0].Substring(0, 3)
                : "Usr";
            string lastName = nameParts.Length > 1 && nameParts[1].Length >= 3
                ? nameParts[1].Substring(0, 3)
                : "Usr";

            return $"{prefix}-{firstName}{lastName}";
        }
    }
}