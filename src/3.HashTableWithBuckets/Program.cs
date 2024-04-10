// This is a simple implementation of a hash table using open addressing.
// This implementation isn't complete and it's not efficient.
// It's just a simple example to show how a hash table works.

var hashtable = new HashTable<int, Profile>(5);

hashtable.Add(3, new("Leo", "leo@email.fk"));
//hashtable.Add(3, new("Lia", "lia@email.fk"));
hashtable.Add(4, new("Eva", "eva@email.fk"));
hashtable.Add(0, new("Bob", "bob@email.fk"));
hashtable.Add(1, new("Sue", "sue@email.fk"));
hashtable.Add(2, new("Ana", "ana@email.fk"));
hashtable.Add(5, new("Max", "max@email.fk"));
hashtable.Add(6, new("Jon", "jon@email.fk"));

Console.WriteLine(hashtable.Get(0));
Console.WriteLine(hashtable.Get(1));
Console.WriteLine(hashtable.Get(2));
Console.WriteLine(hashtable.Get(3));
Console.WriteLine(hashtable.Get(4));

hashtable.Remove(0);
hashtable.Remove(1);
hashtable.Remove(2);
hashtable.Remove(3);
hashtable.Remove(4);
hashtable.Remove(5);
hashtable.Remove(6);


// var hashtable = new HashTable<string, Profile>(5);

// hashtable.Add("lo", new("Leo", "leo@email.fk"));
// hashtable.Add("la", new("Lia", "lia@email.fk"));
// hashtable.Add("ea", new("Eva", "eva@email.fk"));
// hashtable.Add("bb", new("Bob", "bob@email.fk"));
// hashtable.Add("se", new("Sue", "sue@email.fk"));
// hashtable.Add("aa", new("Ana", "ana@email.fk"));
// hashtable.Add("mx", new("Max", "max@email.fk"));


record Profile(string Name, string Email);

class HashTable<TKey, TValue>
{
    record HashNode(TKey Key, TValue Value)
    {
        public HashNode? Next { get; set; }
    }

    private HashNode?[] _buckets;
    private int _count;


    public HashTable(int capacity)
        => _buckets = new HashNode[(int)(capacity * 1.25)]; // 25% more space to avoid collisions

    public void Add(TKey key, TValue value)
    {
        var index = _computeHash(key);
        var node = new HashNode(key, value);

        if(_count == _buckets.Length)
        {
            throw new InvalidOperationException("Hashtable is full");
        }

        if(_buckets[index] is null)
        {
            _buckets[index] = node;
        }
        else
        {
            var current = _buckets[index];
            while(current.Next is not null)
            {
                current = current.Next;
            }

            if(current.Key.Equals(key))
            {
                throw new InvalidOperationException("Key already exists");
            }

            current.Next = node;
        }

        _count++;
    }

    public TValue Get(TKey key)
    {
        var index = _computeHash(key);
        var current = _buckets[index];

        while(current is not null)
        {
            if(current.Key.Equals(key))
            {
                return current.Value;
            }

            current = current.Next;
        }

        throw new KeyNotFoundException();
    }

    public void Remove(TKey key)
    {
        var index = _computeHash(key);
        var current = _buckets[index];

        if(current is null)
        {
            throw new KeyNotFoundException();
        }


        HashNode? previous = null;
        while(current is not null)
        {
            if(current.Key.Equals(key))
            {
                if(previous is null)
                {
                    _buckets[index] = current.Next;
                }
                else
                {
                    previous.Next = current.Next;
                }
                _count--;
                return;
            }

            previous = current;
            current = current.Next;
        }
    }

    private int _computeHash(TKey key)
    {
        var hash = key.GetHashCode();
        hash &= 0x7FFFFFFF; // Remove sinal -> 0x7FFFFFFF = 1111111111111111111111111111111 = int.MaxValue
        hash %= _buckets.Length;
        return hash;
    }
}
