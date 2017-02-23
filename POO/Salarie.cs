using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPCSharp
{
    class Salarie
    {

        private static int count = 0;

        private int matricule, categorie, service;
        private string name;
        private double salaire;

        public enum Categories
        {
            RH,
            Compta,
            Admin,
            Info
        }

        public Salarie()
        {
            matricule = count;
            count++;
        }

        public Salarie(int catg, int serv, string nomSal, double sal)
        {
            this.categorie = catg;
            this.Service = serv;
            this.Name = nomSal;
            this.Salaire = sal;
            count++;
        }

        public static int GetCount
        {
            get
            {
                return count;
            }
        }

        public int Categorie
        {
            get
            {
                return categorie;
            }

            set
            {
                categorie = value;
            }
        }

        public int Matricule
        {
            get
            {
                return matricule;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public double Salaire
        {
            get
            {
                return salaire;
            }

            set
            {
                salaire = value;
            }
        }

        public int Service
        {
            get
            {
                return service;
            }

            set
            {
                service = value;
            }
        }

        public string CalculerSalaire()
        {
            return "le salaire de " + Name + " est de " +  Salaire;
        }

    }
}
