using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPCSharp
{
     public abstract class Personne
    {
        public String Name { get; set; }

        public static int Count { get; set; } = 0;

    }
}
