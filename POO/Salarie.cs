﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPCSharp
{
    public abstract class Salarie : Personne, IRemunerable, IComparable<Salarie>
    {

        public int Categorie { get; set; }

        public int Matricule { get; private set; }

        public int Service { get; set; }

        public Double Salaire { get; set; }

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

        public String s = "salut";

        public Salarie(String name, Int32 type)
        {
            base.Name = name;
            Matricule = Count;
            this.Type = type;
            Personne.Count++;

           

        }

        public Salarie(String name, Int32 type, Int32 matricule)
        {
            base.Name = name;
            this.Matricule = matricule;
            this.Type = type;
           

        }

        public Salarie(String name, Int32 type, Int32 matricule, int catg, int serv, String email)
        {
            base.Name = name;
            this.Matricule = matricule;
            this.Type = type;
            Categorie = catg;
            Service = serv;
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

        public int CompareTo(Salarie other)
        {
            return Salaire.CompareTo(other.Salaire);
        }
    }
}
