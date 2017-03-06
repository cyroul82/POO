using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPCSharp
{
    public class SalarieException : Exception
    {
        public SalarieException(Salarie sal) : base (sal.ToString())
        {

        }




        
    }
}
