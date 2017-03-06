using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPCSharp
{
    public class Commercial : Salarie
    {
        public Double ChiffreAffaire { get; set; }
        public Int32 Commission { get; set; }

        public Commercial(String name, Int32 type, Int32 mat, Int32 cat, Int32 serv, String email, Double salaire, Double ca, Int32 com) : base(name, type, mat, cat, serv, email, salaire)
        {
            ChiffreAffaire = ca;
            Commission = com;
        }
        public Commercial(String name, Int32 type, Int32 mat, Int32 cat, Int32 serv, String email, Double ca, Int32 com) : base(name, type, mat, cat, serv, email)
        {
            ChiffreAffaire = ca;
            Commission = com;
        }

        public override Double CalculerSalaire()
        {
            return ((ChiffreAffaire * Commission) / 100 + Salaire);
        }

        public override string ToString()
        {
            return ("Commercial :\nName : " + Name + "\n\tMatricule : " + Matricule + "\n\tCategorie : "
                        + Categorie + "\n\tService : " + Service + "\n\tSalaire: " + Salaire + "\n\tEmail : " + Email
                        + "\n\tChiffre d'affaire : " + ChiffreAffaire + "\n\tCommission : " + Commission + "\n\tSalaire Total : "  + CalculerSalaire());
        }


    }
}
