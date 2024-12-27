using System;

namespace Solutionhead.TestFoundations
{
    public interface IIntegrationTestHelper<out TContext>
        where TContext : class, IDisposable
    {
        TContext Context { get; }
        IObjectInstantiator ObjectInstantiator { get; }

        T CreateObjectGraph<T>(params Action<T>[] initializations)
            where T : class;

        T ConstrainForeignKeys<T>(T @object)
            where T : class;

        T CreateObjectGraphAndInsertIntoDatabase<T>(params Action<T>[] initializations)
            where T : class;

        T CreateObjectGraphAndInsertIntoDatabase<T>(bool saveChangesToContext, params Action<T>[] initializations)
            where T : class;

        T InsertObjectGraphIntoDatabase<T>(T @object, bool saveChangesToContext = true)
            where T : class;

        void DisposeOfContext();
        TContext ResetContext();
        void SaveChangesToContext();
    }
}