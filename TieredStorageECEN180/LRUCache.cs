using System;
using System.Collections.Generic;

public class LRUCache<K, V>
{
    private readonly int capacity;
    private readonly Dictionary<K, LinkedListNode<(K key, V value)>> cache;
    private readonly LinkedList<(K key, V value)> lruList;

    public LRUCache(int capacity)
    {
        this.capacity = capacity;
        this.cache = new Dictionary<K, LinkedListNode<(K key, V value)>>();
        this.lruList = new LinkedList<(K key, V value)>();
    }

    public V Get(K key, out bool found)
    {
        if (cache.TryGetValue(key, out var node))
        {
            lruList.Remove(node);
            lruList.AddFirst(node);
            found = true;
            return node.Value.value;
        }
        found = false;
        return default;
    }

    public void Put(K key, V value)
    {
        if (cache.ContainsKey(key))
        {
            var node = cache[key];
            lruList.Remove(node);
        }
        else if (cache.Count >= capacity)
        {
            var lruNode = lruList.Last;
            if (lruNode != null)
            {
                cache.Remove(lruNode.Value.key);
                lruList.RemoveLast();
            }
        }

        var newNode = new LinkedListNode<(K key, V value)>((key, value));
        lruList.AddFirst(newNode);
        cache[key] = newNode;
    }
}
