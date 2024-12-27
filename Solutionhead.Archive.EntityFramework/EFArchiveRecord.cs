using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Solutionhead.Archive.EntityFramework
{
    class EFArchiveRecord
    {
        [Key]
        string primaryKey;
        [Key]
        string objectType;
        [Key]
        DateTime datetimeArchived;
        string XMLdata;
    }
}
