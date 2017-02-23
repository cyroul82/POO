using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmailAdresse;


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
                Console.WriteLine("");
                Console.WriteLine("Que faire : ?");
                Console.WriteLine("1 : Ajouter un salarié");
                Console.WriteLine("2 : Afficher la liste des salariés");
                Console.WriteLine("3 : Recherche un salarié par matricule");
                Console.WriteLine("4 : Nombre de Salarié");
                Console.WriteLine("5 : Supprimer un salarié par matricule");
                Console.WriteLine("6 : Quitter");
                
                

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
                                    PrintErrorMessage();
                                }
                                catch (OverflowException)
                                {
                                    PrintErrorMessage();
                                }
                            }
                            
                            break;
                        case 4:
                            Console.WriteLine("Nombre de salariés enregistrés :" + Salarie.Count);
                            break;
                        case 5:
                            Boolean bbb = true;

                            while (bbb)
                            {
                                try
                                {
                                    Console.Write("Entrez un matricule : ");
                                    DeleteSalarieByMatricule(Convert.ToInt32(Console.ReadLine()));
                                    bbb = false;
                                }
                                catch (FormatException)
                                {
                                    PrintErrorMessage();
                                }
                                catch (OverflowException)
                                {
                                    PrintErrorMessage();
                                }
                            }
                            
                            break;
                        case 6:
                            b = false;
                            break;
                        default:
                            break;
                    }

                }
                catch (FormatException)
                {
                    PrintErrorMessage();

                }
                catch (OverflowException)
                {
                    PrintErrorMessage();
                }
            }
        }

        public static void AddSalarie()
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

            Boolean flag = true;
            while (flag)
            {
                Console.WriteLine("Email @ : ");
                if (Program.checkEmail(Console.ReadLine())) flag = false;
            }
            

            listSalarie.Add(salarie);
            Console.WriteLine("");
            Console.WriteLine("Le salarié à été ajouté ! ");
            Console.WriteLine("");
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
            CenterText("Name : "+ s.Name + "\n\tMatricule : " + s.Matricule + "\n\tCategorie : " 
                        + s.Categorie + "\n\tService : " + s.Service +"\n\tSalaire: " + s.Salaire);
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
                    PrintErrorMessage();
                    cat = Console.ReadLine();
                }
                catch (OverflowException)
                {
                    PrintErrorMessage(); ;
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
                    PrintErrorMessage();
                    serv = Console.ReadLine();
                }
                catch (OverflowException)
                {
                    PrintErrorMessage();
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
                    PrintErrorMessage();
                    sal = Console.ReadLine();
                }
                catch (OverflowException)
                {
                    PrintErrorMessage();
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

        private static void CenterText(String text)
        {
            int space = (Console.WindowWidth - text.Length) / 2;
            Console.SetCursorPosition(space, Console.CursorTop);
            Console.WriteLine(text);
        }

        private static void PrintErrorMessage()
        {
            Console.WriteLine("Erreur dans la saisie : ");
            Console.WriteLine("Entrer une nouvelle valeur :\n ");
        }

        private static void DeleteSalarieByMatricule(Int32 mat)
        {
            Boolean isDeleted = false;
            for (int i=0; i <= listSalarie.Count-1; i++)
            {
                Salarie s = listSalarie.ElementAt(i);
                if(s.Matricule == mat)
                {
                    listSalarie.Remove(s);
                    Console.WriteLine("Salarié : " + s.Name + " a été supprimé !");
                    isDeleted = true;
                }
            }

            if (!isDeleted)
            {
                Console.WriteLine("Le salarié avec ce matricule n'a pas pu être trouvé \n et par conséquent non supprimé ! \n Merci de vérifier son Matricule");
            }

            
        }
    }
}
