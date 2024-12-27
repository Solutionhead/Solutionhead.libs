namespace Solutionhead.Data
{
    public interface ITableNameIdentifier
    {
        string GetTableName<TEntity>() where TEntity : class;
    }
}