using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPCSharp
{
    public abstract class Salarie : Personne, IRemunerable, IComparable<Salarie>
    {

        public Int32 Matricule { get; set; }

        private Int32 categorie;
        public int Categorie
        {
            get
            {
                return this.categorie;
            }
            set
            {

                this.categorie = value;
            }
        }

        public Int32 Service { get; set; }

        private Double salaire;
        public Double Salaire {
            get
            {
                return this.salaire;
            }

            set
            {
                if (value < 0)
                {
                    throw new SalaireSalarieException(this);
                }
                this.salaire = value;
            }
        }

        public String Email { get; set; }

        public Int32 Type { get; set; }

        public enum Categories
        {
            RH,
            Compta,
            Admin,
            Info
        }

        public enum Salaries
        {
            Commercial,
            Technicien
        }

        public Salarie(String name, Int32 type, Int32 matricule, Int32 catg, Int32 serv, String email, Double salaire)
        {
            base.Name = name;
            this.Matricule = matricule;
            this.Type = type;
            Categorie = catg;
            Service = serv;
            Matricule = matricule;
            Email = email;
            Salaire = salaire;

            Personne.Count++;

        }

        public Salarie(String name, Int32 type, Int32 matricule, Int32 catg, Int32 serv, String email)
        {
            base.Name = name;
            this.Matricule = matricule;
            this.Type = type;
            Categorie = catg;
            Service = serv;
            Matricule = matricule;
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

        public int CompareTo(Salarie other)
        {
            return Salaire.CompareTo(other.Salaire);
        }
    }
}
