using LearnCosmos.Models;
using Microsoft.Azure.Cosmos;
using System;
using System.Threading.Tasks;

namespace LearnCosmos
{
    internal static class DocumentsDemo
    {
        static readonly string Database = "Families";
        static readonly string Container = "Families";

        internal static async Task Run()
        {
            await CreateDocuments();
        }


        static async Task CreateDocuments()
        {
            Console.WriteLine();
            Console.WriteLine($">>> Create Documents <<<");
            Console.WriteLine();

            var container = Shared.Client.GetContainer(Database, Container);

            dynamic document1 = new
            {
                id = Guid.NewGuid(),
                name = "New Customer 1",
                address = new
                {
                    addressType = "Main Office",
                    city = "Chicago",
                    state = "IL",
                    zipCode = "11229"
                },
                kids = new[] {
                    "Paul",
                    "Jenny"
                }
            };

            await container.CreateItemAsync(document1, new PartitionKey("11229"));
            Console.WriteLine($"Created new document {document1.id} from dynamic object");

            var customer = new Customer
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Customer 1",
                Address = new Address
                {
                    AddressType = "Main Office",
                    City = "New York",
                    State = "New York",
                    ZipCode = "40987"
                },
                Kids = new string[]
                {
                    "Bob",
                    "Paul"
                }
            };

            await container.CreateItemAsync(customer, new PartitionKey("40987"));
            Console.WriteLine($"Created new document {customer.Id} from typed object");
        }
    }
}
