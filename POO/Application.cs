using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPCSharp
{
    class Application
    {

        private static List<Salarie> listSalarie = new List<Salarie>();

        static void Main(string[] args)
        {
            Boolean b = true;
            while (b)
            {

                Console.WriteLine("Bonjour {0}", Environment.UserName);
                Console.WriteLine("Que faire : ?");
                Console.WriteLine("1 : Ajouter un salarié");
                Console.WriteLine("2 : Afficher la liste des salariés");
                Console.WriteLine("3 : Recherche un salarié par matricule");
                Console.WriteLine("4 : Quitter");

                try
                {
                    Int32 choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            AddSalarie();
                            break;
                        case 2:
                            DisplayListSalarie();
                            break;
                        case 3:
                            Boolean bb = true;
                            
                            while (bb)
                            {
                                try
                                {
                                    Console.Write("Entrez un matricule : ");
                                    findSalarieByMatricule(Convert.ToInt32(Console.ReadLine()));
                                    bb = false;
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("Erreur dans la saisie : ");
                                    Console.WriteLine("Entrer une nouvelle valeur : ");
                                }
                                catch (OverflowException)
                                {
                                    Console.WriteLine("Erreur dans la saisie : ");
                                    Console.WriteLine("Entrer une nouvelle valeur : ");
                                }
                            }
                            
                            break;
                        case 4:
                            b = false;
                            break;
                    }

                }
                catch (FormatException)
                {
                    Console.WriteLine("Erreur saisie !");

                }
                catch (OverflowException)
                {
                    Console.WriteLine("Erreur saisie");
                }
            }
        }

        private static void AddSalarie()
        {
            Salarie salarie = new Salarie();

            Console.Write("Nom du Salarié : ");
            salarie.Name = Console.ReadLine();

            Console.Write("Categorie : ");

            salarie.Categorie = CheckCategory(Console.ReadLine());

            Console.Write("Service : ");
            salarie.Service = CheckService(Console.ReadLine());

            Console.Write("Salaire(€): ");
            salarie.Salaire = CheckSalaire(Console.ReadLine());

            listSalarie.Add(salarie);
        }

        private static void DisplayListSalarie()
        {
            if (listSalarie.Count == 0)
            {
                Console.WriteLine("Pas de salarié !! ");
            }
            else
            {
                foreach (Salarie sal in listSalarie)
                {
                    DisplaySalarie(sal);
                }
            }

        }

        private static void DisplaySalarie(Salarie s)
        {
            
            Console.WriteLine("Name : {0}\n\tMatricule : {1}\n\tCategorie : {2}\n\tService : {3}\n\tSalaire: {4}", s.Name, s.Matricule, s.Categorie, s.Service, s.Salaire);
        }

        private static int CheckCategory(String cat) 
        {
            bool flag = true;
            int c = -1;
            while (flag)
            {
                try
                {
                    c = Convert.ToInt32(cat);
                    flag = false;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Erreur dans la saisie : " );
                    Console.WriteLine("Entrer une nouvelle valeur : ");
                    cat = Console.ReadLine();
                }
                catch (OverflowException)
                {
                    Console.WriteLine("La saisie est incorrect !! ");
                    Console.WriteLine("Entrer une nouvelle valeur : ");
                    cat = Console.ReadLine();
                }
            }

            return c;

        }

        private static int CheckService(String serv)
        {
            bool flag = true;
            int c = -1;
            while (flag)
            {
                try
                {
                    c = Convert.ToInt32(serv);
                    flag = false;

                }
                catch (FormatException)
                {
                    Console.WriteLine("Erreur dans la saisie : ");
                    Console.WriteLine("Entrer une nouvelle valeur : ");
                    serv = Console.ReadLine();
                }
                catch (OverflowException)
                {
                    Console.WriteLine("La saisie est incorrect !! ");
                    Console.WriteLine("Entrer une nouvelle valeur : ");
                    serv = Console.ReadLine();
                }
            }

            return c;
        }

        private static Double CheckSalaire(String sal)
        {
            bool flag = true;
            double c = -1;
            while (flag)
            {
                try
                {
                    c = Convert.ToDouble(sal);
                    flag = false;

                }
                catch (FormatException)
                {
                    Console.WriteLine("Erreur dans la saisie : ");
                    Console.WriteLine("Entrer une nouvelle valeur : ");
                    sal = Console.ReadLine();
                }
                catch (OverflowException)
                {
                    Console.WriteLine("La saisie est incorrect !! ");
                    Console.WriteLine("Entrer une nouvelle valeur : ");
                    sal = Console.ReadLine();
                }
            }

            return c;
        }

        private static void findSalarieByMatricule(Int32 mat)
        {
            Salarie s = null;
            foreach(Salarie sal in listSalarie)
            {   
                if(sal.Matricule == mat)
                {
                    s = sal;
                }
            }
            if(s!= null)
            {
                DisplaySalarie(s);
            }
            else
            {
                Console.WriteLine("Aucun salarié avec ce matricule");
            }
        }
    }
}
