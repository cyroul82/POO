using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPCSharp
{
    class Salarie
    {

        //private static int Count = 0;

        public static int Count { get; private set; } = 0;

        public int Categorie { get; set; }

        public int Matricule { get; private set; }

        public int Service { get; set; }

        public String Name { get; set; }

        public Double Salaire { get; set; }

        public String Email { get; set; }

        public enum Categories
        {
            RH,
            Compta,
            Admin,
            Info
        }

        public Salarie()
        {
            Matricule = Count;
            Count++;
           
        }

        public Salarie(int catg, int serv, string nomSal, double sal, String email)
        {
            Categorie = catg;
            Service = serv;
            Name = nomSal;
            Salaire = sal;
            Matricule = Count;
            Email = email;
            Count++;
        }


        

        public string CalculerSalaire()
        {
            return "le salaire de " + Name + " est de " +  Salaire;
        }

    }
}
