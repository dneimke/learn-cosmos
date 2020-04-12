using LearnCosmos.Models;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace LearnCosmos
{
    internal static class QueriesDemo
    {
        static readonly string Database = "Families";
        static readonly string Container = "Families";

        internal static async Task Run()
        {
            await QueryDocuments();
        }

        static async Task QueryDocuments()
        {
            Console.Clear();
            Console.WriteLine($">>> Query Documents <<<");
            Console.WriteLine();

            var container = Shared.Client.GetContainer(Database, Container);
            var sql = "SELECT * FROM c WHERE STARTSWITH(c.name, 'New Customer') = true";

            var iterator1 = container.GetItemQueryIterator<dynamic>(sql);
            var docs1 = await iterator1.ReadNextAsync();
            var count = 0;
            foreach (var doc in docs1)
            {
                Console.WriteLine($"  ({++count}) Id: {doc.id}; Name: {doc.name}");

                Customer customer = JsonConvert.DeserializeObject<Customer>(doc.ToString());
                Console.WriteLine($"  City: {customer.Address.City}");
            }

            Console.WriteLine($"Retrieved {count} new documents as dynamic");
            Console.WriteLine();

            var iterator2 = container.GetItemQueryIterator<Customer>(sql);
            var docs2 = await iterator2.ReadNextAsync();
            count = 0;
            foreach (var doc in docs2)
            {
                Console.WriteLine($"  ({++count}) Id: {doc.Id}; Name: {doc.Name}");
                Console.WriteLine($"  City: {doc.Address.City}");
            }

            Console.WriteLine($"Retrieved {count} new documents as typed");
            Console.WriteLine();
        }
    }
}
