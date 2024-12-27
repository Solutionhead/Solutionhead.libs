using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Solutionhead.Data
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
