using System;
using System.Threading.Tasks;

namespace LearnCosmos
{
    class Program
    {
        static void Main(string[] args)
        {
            QueryForDocuments().Wait();

            Console.WriteLine("DB - Databases");
            Console.WriteLine("CO - Containers");
            Console.WriteLine("DO - Documents");
            Console.WriteLine("QU - Queries");
            Console.WriteLine("Enter an operation to perform.");

            var input = Console.ReadLine();

            switch (input)
            {
                case "DB":
                    DatabaseDemo.Run().Wait();
                    break;
                case "CO":
                    ContainersDemo.Run().Wait();
                    break;
                case "DO":
                    DocumentsDemo.Run().Wait();
                    break;
                case "QU":
                    QueriesDemo.Run().Wait();
                    break;
                default:
                    break;
            }

            Console.ReadKey();
        }



        static async Task QueryForDocuments()
        {
            var container = Shared.Client.GetContainer("Families", "Families");
            var sql = "SELECT * FROM c WHERE ARRAY_LENGTH(c.kids) > 1";
            var iterator = container.GetItemQueryIterator<dynamic>(sql);

            var page = await iterator.ReadNextAsync();

            foreach(var doc in page)
            {
                Console.WriteLine($"Family {doc.id} has {doc.kids.Count} children.");
            }
        }


    }
}
