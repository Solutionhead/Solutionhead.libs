using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using EntityParser;

namespace Solutionhead.TestFoundations
{
    public abstract class IntegrationTestHelperBase<TContext> : IIntegrationTestHelper<TContext>
        where TContext : class, IDisposable
    {
        public TContext Context { get; protected set; }
        public IObjectInstantiator ObjectInstantiator { get; protected set; }
        protected IEntityObjectGraphForeignKeyConstrainer ForeignKeyConstrainer { get; set; }

        public abstract TContext ResetContext();

        public T CreateObjectGraph<T>(params Action<T>[] initializations)
            where T : class
        {
            return ObjectInstantiator.InstantiateObject(initializations);
        }

        public T ConstrainForeignKeys<T>(T @object)
            where T : class
        {
            return ForeignKeyConstrainer.ConstrainForeignKeys(@object);
        }

        public T CreateObjectGraphAndInsertIntoDatabase<T>(params Action<T>[] initializations)
            where T : class
        {
            return InsertObjectGraphIntoDatabase(CreateObjectGraph(initializations));
        }

        public T CreateObjectGraphAndInsertIntoDatabase<T>(bool saveChangesToContext, params Action<T>[] initializations)
            where T : class
        {
            return InsertObjectGraphIntoDatabase(CreateObjectGraph(initializations), saveChangesToContext);
        }

        public T InsertObjectGraphIntoDatabase<T>(T @object, bool saveChangesToContext = true)
            where T : class
        {
            ForeignKeyConstrainer.ConstrainForeignKeys(@object);
            AddObjectToContext(@object);

            if(saveChangesToContext)
            {
                SaveChangesToContext();
            }

            return @object;
        }

        public void DropAndRecreateDatabase()
        {
            ResetContext();
            DropAndRecreateContext();
        }

        public void DisposeOfContext()
        {
            if(Context != null)
            {
                Context.Dispose();
                Context = null;
            }
        }

        public abstract void SaveChangesToContext();
        
        protected abstract void DropAndRecreateContext();
        protected abstract void AddObjectToContext<T>(T @object) where T : class;
    }

    public abstract class DbContextIntegrationTestHelper<TContext> : IntegrationTestHelperBase<TContext>
        where TContext : DbContext
    {
        public override void SaveChangesToContext()
        {
            Context.SaveChanges();
        }

        protected override void AddObjectToContext<T>(T @object)
        {
            Context.Set(@object.GetType()).Add(@object);
        }
    }

    public abstract class ObjectContextIntegrationTestHelper<TContext> : IntegrationTestHelperBase<TContext>
        where TContext : ObjectContext
    {
        public override void SaveChangesToContext()
        {
            Context.SaveChanges();
        }

        protected override void AddObjectToContext<T>(T @object)
        {
            Context.CreateObjectSet<T>().AddObject(@object);
        }
    }
}
