using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace protfolio.Data
{
    public interface IContext
    {
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
