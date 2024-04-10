// This is a simple implementation of a set using a hash table.
// This implementation isn't complete and it's not efficient.
// It's just a simple example to show how a hash table works.

var hashtable = new Set(5);

hashtable.Add("Leo"); // 76  101  111  =  288  %  5  ->  3
hashtable.Add("Eva"); // 69  118  97   =  284  %  5  ->  4
hashtable.Add("Bob"); // 66  111  98   =  275  %  5  ->  0
hashtable.Add("Sue"); // 83  117  101  =  301  %  5  ->  1
hashtable.Add("Ana"); // 65  110  97   =  272  %  5  ->  2
//hashtable.Add("Lia"); // 76  105  97   =  278  %  5  ->  3

Console.WriteLine(hashtable.Get("Leo")); // Leo
Console.WriteLine(hashtable.Get("Eva")); // Eva
Console.WriteLine(hashtable.Get("Bob")); // Bob
Console.WriteLine(hashtable.Get("Sue")); // Sue
Console.WriteLine(hashtable.Get("Ana")); // Ana
//Console.WriteLine(hashtable.Get("Lia")); // Lia

hashtable.Remove("Leo");
hashtable.Remove("Eva");
hashtable.Remove("Bob");
hashtable.Remove("Sue");
hashtable.Remove("Ana");
//hashtable.Remove("Lia");

class Set
{
    private string?[] _items;

    public Set(int capacity)
        => _items = new string[capacity];

    public void Add(string value)
    {
        var index = _computeHash(value);

        if (_items[index] is not null)
        {
            throw new InvalidOperationException();
        }

        _items[index] = value;
    }

    public string Get(string value)
    {
        var index = _computeHash(value);
        if (_items[index] is not null)
        {
            if (_items[index] == value)
            {
                return _items[index];
            }
        }

        throw new KeyNotFoundException();
    }

    public void Remove(string value)
    {
        var index = _computeHash(value);
        if (_items[index] is null)
        {
            throw new KeyNotFoundException();
        }

        if (_items[index] != value)
        {
            throw new KeyNotFoundException();
        }

        _items[index] = null;
    }

    private int _computeHash(string value)
    { // This is an ingenuous way to get a hash code. Never use this in production.
        var sum = 0;
        foreach(var c in value)
        {
            sum += c;
        }

        return sum % _items.Length;
    }
}
