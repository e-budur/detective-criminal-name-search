using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detective.Index
{
    public interface IIndexer
    {

        void Index(int id, string name);
        void Complete();
    }
}
