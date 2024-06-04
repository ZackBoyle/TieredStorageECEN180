int cache_size = 5;

var cache1 = new LRUCache<int, string>(cache_size);

cache1.Put(1, "hi mom");
cache1.Put(2, "hello world");
cache1.Put(3, "the giant cheese");

var cache1_state = cache1.GetCacheState();

foreach (var entry in cache1_state)
{
    Console.WriteLine($"Key: {entry.Key}, Value: {entry.Value}");
}

Console.WriteLine($"Capacity: {cache1_state.Count()}/{cache_size}");