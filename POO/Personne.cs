using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPCSharp
{
    public abstract class Personne
    {
        public abstract String Name { get; }

        protected static int Count { get; set; } = 0;

    }
}
