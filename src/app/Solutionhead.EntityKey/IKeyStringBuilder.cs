namespace Solutionhead.EntityKey
{
    public interface IKeyStringBuilder<in TKey> where TKey : class
    {
        string BuildKeyValue(TKey key);
    }
}