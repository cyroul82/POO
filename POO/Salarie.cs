using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPCSharp
{
    public class Salarie : Personne, IRemunerable, IComparable
    {

        public int Categorie { get; set; }

        public int Matricule { get; private set; }

        public int Service { get; set; }

        public Double Salaire { get; set; }

        public String Email { get; set; }


        public enum Categories
        {
            RH,
            Compta,
            Admin,
            Info
        }

        public Salarie(String name)
        {
            base.Name = name;
            Matricule = Count;
            Personne.Count++;
           
        }

        public Salarie(int catg, int serv, string nomSal, double sal, String email)
        {
            Categorie = catg;
            Service = serv;
            base.Name = nomSal;
            Salaire = sal;
            Matricule = Count;
            Email = email;
            Personne.Count++;
        }


        public override bool Equals(object obj)
        {
            if (obj is Salarie)
            {
                Salarie s = (Salarie)obj;
                if (s.Matricule == this.Matricule && s.Name.ToUpper() == this.Name.ToUpper())
                {
                    return true;
                }
                else return false;
            }
            else return false;
        }

        public override String ToString()
        {
            return ("Salarié :\nName : " + Name + "\n\tMatricule : " + Matricule + "\n\tCategorie : "
                        + Categorie + "\n\tService : " + Service + "\n\tSalaire: " + Salaire + "\n\tEmail : " + Email);
        }

        public virtual Double  CalculerSalaire()
        {
            return Salaire;
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
