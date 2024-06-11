using System;
using System.Collections.Generic;
using TieredStorageECEN180;

public class PredictiveCache
{
    private readonly int capacity;
    private readonly Dictionary<int, int> cache;
    private readonly Queue<int> queue;

    public PredictiveCache(int capacity)
    {
        this.capacity = capacity;
        this.cache = new Dictionary<int, int>();
        this.queue = new Queue<int>();
    }

    public bool Get(int key)
    {
        return cache.ContainsKey(key);
    }

    public void Put(int key, int value)
    {
        if (!cache.ContainsKey(key))
        {
            if (cache.Count >= capacity)
            {
                int removeKey = queue.Dequeue();
                cache.Remove(removeKey);
            }
            cache[key] = value;
            queue.Enqueue(key);

            // Predict next key and prefetch it
            var data = new MLModel2.ModelInput();
            data.Current_Object_ID = key;
            int nextKey = Convert.ToInt32(MLModel2.Predict(data).Next_Object_ID);
            if (!cache.ContainsKey(nextKey) && cache.Count < capacity)
            {
                cache[nextKey] = nextKey;
                queue.Enqueue(nextKey);
            }
        }
    }
}
