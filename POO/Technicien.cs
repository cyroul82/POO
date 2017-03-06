using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPCSharp
{
    public class Technicien : Salarie
    {
        public Technicien(String name, Int32 type, Int32 mat, Int32 cat, Int32 serv, String email, Double salaire) : base(name, type, mat, cat, serv, email, salaire)
        {

        }

        public Technicien(String name, Int32 type, Int32 mat, Int32 cat, Int32 serv, String email) : base(name, type, mat, cat, serv, email )
        {

        }

        public override String ToString()
        {
            return ("Technicien :\nName : " + Name + "\n\tMatricule : " + Matricule + "\n\tCategorie : "
                        + Categorie + "\n\tService : " + Service + "\n\tSalaire: " + Salaire + "\n\tEmail : " + Email);
        }
    }
}
