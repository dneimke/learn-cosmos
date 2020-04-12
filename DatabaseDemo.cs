using Microsoft.Azure.Cosmos;
using System;
using System.Threading.Tasks;

namespace LearnCosmos
{
    public static class DatabaseDemo
    {
        static readonly string Database = "MyNewDatabase";

        internal static async Task Run()
        {
            await ListDatabases();
            await CreateDatabase();
            await ListDatabases();
            await DeleteDatabase();

        }

        static async Task ListDatabases()
        {
            Console.WriteLine();
            Console.WriteLine(">>> List Databases <<<");

            var iterator = Shared.Client.GetDatabaseQueryIterator<DatabaseProperties>();
            var databases = await iterator.ReadNextAsync();

            var count = 0;
            foreach (var db in databases)
            {
                count++;
                Console.WriteLine($"  Database Id: {db.Id}; Modified: {db.LastModified}.");
            }

            Console.WriteLine();
            Console.WriteLine($"Total databases: {count}");
        }

        static async Task CreateDatabase()
        {
            Console.WriteLine();
            Console.WriteLine(">>> Create Database <<<");

            var result = await Shared.Client.CreateDatabaseIfNotExistsAsync(Database);
            var db = result.Resource;

            Console.WriteLine($"  Database Id: {db.Id}; Modified: {db.LastModified}.");
        }

        static async Task DeleteDatabase()
        {
            Console.WriteLine();
            Console.WriteLine(">>> Delete Database <<<");

            await Shared.Client.GetDatabase(Database).DeleteAsync();
        }
    }
}
