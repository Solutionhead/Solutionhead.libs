namespace EntityParser
{
    public interface IEntityObjectGraphForeignKeyConstrainer
    {
        TObject ConstrainForeignKeys<TObject>(TObject @object);
    }
}
