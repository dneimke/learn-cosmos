using Microsoft.Azure.Cosmos;
using System;
using System.Threading.Tasks;

namespace LearnCosmos
{
    public static class ContainersDemo
    {
        static readonly string Database = "Families";

        internal static async Task Run()
        {
            await ListContainers();

            await CreateContainer("Container1");
            await CreateContainer("Container2", 800, "/other");
            
            await ListContainers();
            
            await DeleteContainer("Container1");
            await DeleteContainer("Container2");
            
            await ListContainers();
        }

        static async Task ListContainers()
        {
            Console.WriteLine();
            Console.WriteLine($">>> List Containers in {Database} Database <<<");

            var database = Shared.Client.GetDatabase(Database);
            var iterator = database.GetContainerQueryIterator<ContainerProperties>();
            var containers = await iterator.ReadNextAsync();

            var count = 0;
            foreach (var container in containers)
            {
                count++;
                await ViewContainer(container);
            }

            Console.WriteLine();
            Console.WriteLine($"Total containers in {Database} Database: {count}");
        }

        static async Task ViewContainer(ContainerProperties props)
        {
            Console.WriteLine($"     Container ID: {props.Id}");
            Console.WriteLine($"    Last Modified: {props.LastModified}");
            Console.WriteLine($"    Partition Key: {props.PartitionKeyPath}");

            var container = Shared.Client.GetContainer(Database, props.Id);
            var throughput = await container.ReadThroughputAsync();

            Console.WriteLine($"       Throughput: {throughput}");
        }

        static async Task CreateContainer(string containerId, int throughput = 400, string partitionKey = "/partitionKey")
        {
            Console.WriteLine();
            Console.WriteLine($">>> Create Container {containerId} in {Database} Database <<<");

            var database = Shared.Client.GetDatabase(Database);

            var props = new ContainerProperties(containerId, partitionKey);
            var result = await database.CreateContainerAsync(props, throughput);
            var container = result.Resource;

            Console.WriteLine($"  Container Id: {container.Id}; Modified: {container.LastModified}.");
        }

        static async Task DeleteContainer(string containerId)
        {
            Console.WriteLine();
            Console.WriteLine(">>> Delete Container <<<");

            var container = Shared.Client.GetContainer(Database, containerId);
            await container.DeleteContainerAsync();

            Console.WriteLine($"Deleted continer {containerId} from {Database} Database");
        }
    }
}
