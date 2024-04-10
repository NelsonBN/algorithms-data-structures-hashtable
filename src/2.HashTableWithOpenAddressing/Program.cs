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
//hashtable.Add(6, new("Jon", "jon@email.fk"));

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
    record Item(TKey Key, TValue Value);

    private Item?[] _items;
    private int _count;

    public HashTable(int capacity)
    {
        _count = 0;
        _items = new Item[(int)(capacity * 1.25)]; // 25% more space to avoid collisions
    }

    public void Add(TKey key, TValue value)
    {
        var index = _computeHash(key);


        if(_count == _items.Length)
        {
            throw new InvalidOperationException("Hashtable is full");
        }

        while(_items[index] is not null)
        {
            if(_items[index].Key.Equals(key))
            {
                throw new ArgumentException("Key already exists");
            }

            index++;
            index = index < 0 ? -index : index;
            index %= _items.Length;
        }

        _items[index] = new(key, value);
        _count++;
    }

    public TValue Get(TKey key)
    {
        var index = _computeHash(key);


        while(_items[index] is not null)
        {
            if(_items[index].Key.Equals(key))
            {
                return _items[index].Value;
            }

            index++;
            index = index < 0 ? -index : index;
            index %= _items.Length;
        }

        throw new KeyNotFoundException();
    }

    public void Remove(TKey key)
    {
        var index = _computeHash(key);

        while(_items[index] is not null)
        {
            if(_items[index].Key.Equals(key))
            {
                _items[index] = null;
                _count--;
                return;
            }
            else
            {
                index++;
                index = index < 0 ? -index : index;
                index %= _items.Length;
            }
        }

        throw new KeyNotFoundException();
    }

    private int _computeHash(TKey key)
    {
        var hash = key.GetHashCode();
        hash = hash < 0 ? -hash : hash;
        return hash % _items.Length;
    }
}
