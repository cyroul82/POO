using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPCSharp
{
    class SalaireSalarieException : SalarieException
    {
        public SalaireSalarieException(Salarie sal) : base(sal)
        {
        }
        public override string ToString()
        {
            return "\nLe Salaire ne peut être que positif\n";
        }
    }
}
