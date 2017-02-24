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

        public Commercial (String name) : base (name){
            ChiffreAffaire = 0;
            Commission = 0;
            Salarie.Count++;
         }

        public Commercial(String name, Double chiffreAffaire, Int32 commission) : base(name)
        {
            this.ChiffreAffaire = chiffreAffaire;
            this.Commission = commission;
            Salarie.Count++;
        }



        public override Double CalculerSalaire()
        {
            return  ((ChiffreAffaire * Commission) / 100 + Salaire);
           
       }

        public override string ToString()
        {
            return ("Commercial :\nName : " + Name + "\n\tMatricule : " + Matricule + "\n\tCategorie : "
                        + Categorie + "\n\tService : " + Service + "\n\tSalaire: " + Salaire + "\n\tEmail : " + Email
                        + "\n\tChiffre d'affaire : " + ChiffreAffaire + "\n\tCommission : " + Commission + "\n\tSalaire Total : " + CalculerSalaire());
        }


    }
}
