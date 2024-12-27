using Solutionhead.Core;
using Solutionhead.Commands;

namespace Solutionhead.Data
{
    public interface ISqlCommandFactory
    {
        SqlCommand BuildCommand(string sqlCommand);
    }
}