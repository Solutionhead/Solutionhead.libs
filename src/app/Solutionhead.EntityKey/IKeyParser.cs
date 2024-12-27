namespace Solutionhead.EntityKey
{
    public interface IKeyParser<TKey> where TKey : class
    {
        TKey Parse(string keyValue);
        bool TryParse(string keyValue, out TKey key);
        string GetParseFailMessage(string inputValue = null);
        TKey Default { get; }
    }
}