using System;
using System.Collections.Generic;

public enum CacheType
{
    LRU,
    Predictive
}

public class TieredStorage
{
    private readonly LRUCache<int, int> fastLRUCache;
    private readonly PredictiveCache fastPredictiveCache;
    private readonly int fastAccessTime;
    private readonly int slowAccessTime;
    private readonly CacheType cacheType;

    public TieredStorage(int fastCapacity, int fastAccessTime, int slowAccessTime, CacheType cacheType)
    {
        this.fastLRUCache = new LRUCache<int, int>(fastCapacity);
        this.fastPredictiveCache = new PredictiveCache(fastCapacity);
        this.fastAccessTime = fastAccessTime;
        this.slowAccessTime = slowAccessTime;
        this.cacheType = cacheType;
    }

    private int AccessDataLRU(int key)
    {
        bool found;
        int cost = 0;

        fastLRUCache.Get(key, out found);
        if (found)
        {
            cost += fastAccessTime;
        }
        else
        {
            cost += slowAccessTime;
            fastLRUCache.Put(key, key);
        }

        return cost;
    }

    private int AccessDataPredictive(int key)
    {
        int cost = 0;

        if (fastPredictiveCache.Get(key))
        {
            cost += fastAccessTime;
        }
        else
        {
            cost += slowAccessTime;
            fastPredictiveCache.Put(key, key);
        }

        return cost;
    }

    public int AccessData(int key)
    {
        switch (cacheType)
        {
            case CacheType.LRU:
                return AccessDataLRU(key);
            case CacheType.Predictive:
                return AccessDataPredictive(key);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public int CalculateTotalCost(List<int> readRequests)
    {
        int totalCost = 0;
        foreach (var key in readRequests)
        {
            totalCost += AccessData(key);
        }
        return totalCost;
    }
}
