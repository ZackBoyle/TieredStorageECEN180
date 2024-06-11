using TieredStorageECEN180;

// Define the capacities and access times
int fastCapacity = 5;
int fastAccessTime = 1; // 1 unit of time
int slowAccessTime = 5; // 5 units of time

// Define a series of read requests
List<int> readRequests = new List<int> { 28, 48, 38, 53, 83, 88, 22, 46, 27, 97, 55, 76, 45, 67, 80, 
    8, 92, 19, 3, 17, 16, 21, 97, 38, 76, 32, 8, 85, 95, 66, 48, 74, 92, 51, 11, 48 };
Console.WriteLine("Read Requests: ");
foreach (var readRequest in readRequests)
{
    Console.Write($"{readRequest} ");
}
Console.WriteLine();

// Create the tiered storage with LRU Cache
TieredStorage tieredStorageLRU = new TieredStorage(fastCapacity, fastAccessTime, slowAccessTime, CacheType.LRU);
int totalCostLRU = tieredStorageLRU.CalculateTotalCost(readRequests);
Console.WriteLine($"Total cost using LRU cache: {totalCostLRU}");

// Create the tiered storage with Predictive Cache
TieredStorage tieredStoragePredictive = new TieredStorage(fastCapacity, fastAccessTime, slowAccessTime, CacheType.Predictive);
int totalCostPredictive = tieredStoragePredictive.CalculateTotalCost(readRequests);
Console.WriteLine($"Total cost using Predictive cache: {totalCostPredictive}");