using System.Threading;

public class Node<TKey, TValue>
{
    public TKey Key { get; set; }
    public TValue Value { get; set; }
    public Node<TKey, TValue> Prev { get; set; }
    public Node<TKey, TValue> Next { get; set; }

    public Node(TKey key, TValue value)
    {
        Key = key;
        Value = value;
    }
}

public class LRUCache<TKey, TValue>
{
    private readonly int capacity;
    private readonly Dictionary<TKey, Node<TKey, TValue>> cache;
    private Node<TKey, TValue> head;
    private Node<TKey, TValue> tail;

    public LRUCache(int input_capacity)
    {
        capacity = input_capacity;
        cache = new Dictionary<TKey, Node<TKey, TValue>>(capacity);
    }

    public TValue Get(TKey key)
    {
        if (cache.TryGetValue(key, out var node))
        {
            MoveToHead(node);
            return node.Value;
        }
        else
        {
            throw new KeyNotFoundException("The given key was not present in the cache.");
        }
    }

    public void Put(TKey key, TValue value)
    {
        if (cache.TryGetValue(key, out var node))
        {
            node.Value = value;
            MoveToHead(node);
        }
        else
        {
            var newNode = new Node<TKey, TValue>(key, value);
            if (cache.Count >= capacity)
            {
                RemoveTail();
            }
            AddToHead(newNode);
            cache[key] = newNode;
        }
    }

    private void AddToHead(Node<TKey, TValue> node)
    {
        node.Next = head;
        node.Prev = null;
        if (head != null)
        {
            head.Prev = node;
        }
        head = node;
        if (tail == null)
        {
            tail = head;
        }
    }

    private void MoveToHead(Node<TKey, TValue> node)
    {
        if (node == head) return;

        if (node.Prev != null)
        {
            node.Prev.Next = node.Next;
        }
        if (node.Next != null)
        {
            node.Next.Prev = node.Prev;
        }
        if (node == tail)
        {
            tail = node.Prev;
        }
        node.Next = head;
        node.Prev = null;
        if (head != null)
        {
            head.Prev = node;
        }
        head = node;
    }

    private void RemoveTail()
    {
        if (tail != null)
        {
            cache.Remove(tail.Key);
            if (tail.Prev != null)
            {
                tail.Prev.Next = null;
            }
            else
            {
                head = null;
            }
            tail = tail.Prev;
        }
    }

    public List<KeyValuePair<TKey, TValue>> GetCacheState()
    {
        var state = new List<KeyValuePair<TKey, TValue>>();
        var current = head;
        while (current != null)
        {
            state.Add(new KeyValuePair<TKey, TValue>(current.Key, current.Value));
            current = current.Next;
        }
        return state;
    }
}