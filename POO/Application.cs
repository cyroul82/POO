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
                Console.WriteLine("");

                try
                {
                    Console.WriteLine("");
                    Console.Write("Entrez votre Choix : ");
                    Int32 choice = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("");
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("1. Ajouter un commercial");
                            Console.WriteLine("2. Ajouter un salarié classique");
                            Console.Write("Entrez votre Choix : ");
                            Int32 choiceSal = Convert.ToInt32(Console.ReadLine());
                            switch (choiceSal)
                            {
                                case 1:
                                    AddCommercial();
                                    break;
                                case 2:
                                    AddSalarie();
                                    break;
                                default:
                                    break;
                            }
                            
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
                            Console.WriteLine("Nombre de salariés enregistrés :" + GetNombreSalarie());
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

        
        private static Int32 GetNombreSalarie()
        {
            return listSalarie.Count;
        }

        public static void AddCommercial()
        {

            Console.Write("Nom du Commercial : ");
            String name = Console.ReadLine();

            Commercial commercial = new Commercial(name);
            Console.WriteLine("Catégorie : ");
            Console.WriteLine("1. RH");
            Console.WriteLine("2. Compta");
            Console.WriteLine("3. Admin");
            Console.WriteLine("4. Info");

            switch (CheckInt(Console.ReadLine()))
            {
                case 1:
                    commercial.Categorie = (Int32)Salarie.Categories.RH;
                    break;
                case 2:
                    commercial.Categorie = (Int32)Salarie.Categories.Compta;
                    break;
                case 3:
                    commercial.Categorie = (Int32)Salarie.Categories.Admin;
                    break;
                case 4:
                    commercial.Categorie = (Int32)Salarie.Categories.Info;
                    break;
                default:
                    
                    break;
            }
           
            Console.Write("Service : ");
            commercial.Service = CheckInt(Console.ReadLine());

            Console.Write("Salaire(€): ");
            commercial.Salaire = CheckDouble(Console.ReadLine());

            Boolean flag = true;
            while (flag)
            {
                Console.Write("Email @ : ");
                String email = Console.ReadLine();
                if (Program.checkEmail(email))
                {
                    commercial.Email = email;
                    flag = false;
                }
            }

            Console.Write("ChiffreAffaire: ");
            commercial.ChiffreAffaire = CheckDouble(Console.ReadLine());

            Console.Write("Commission (%): ");
            commercial.Commission = CheckInt(Console.ReadLine());


            listSalarie.Add(commercial);
            Console.WriteLine("");
            Console.WriteLine("Le commercial à été ajouté ! ");
            Console.WriteLine("");
        }

        
        public static void AddSalarie()
        {
            
            Console.Write("Nom du Salarié : ");
            String name = Console.ReadLine();

            Salarie salarie = new Salarie(name);
            Console.WriteLine("Catégorie : ");
            Console.WriteLine("1. RH");
            Console.WriteLine("2. Compta");
            Console.WriteLine("3. Admin");
            Console.WriteLine("4. Info");

            switch (CheckInt(Console.ReadLine()))
            {
                case 1:
                    salarie.Categorie = (Int32)Salarie.Categories.RH;
                    break;
                case 2:
                    salarie.Categorie = (Int32)Salarie.Categories.Compta;
                    break;
                case 3:
                    salarie.Categorie = (Int32)Salarie.Categories.Admin;
                    break;
                case 4:
                    salarie.Categorie = (Int32)Salarie.Categories.Info;
                    break;
                default:

                    break;
            }

            Console.Write("Service : ");
            salarie.Service = CheckInt(Console.ReadLine());

            Console.Write("Salaire(€): ");
            salarie.Salaire = CheckDouble(Console.ReadLine());

            Boolean flag = true;
            while (flag)
            {
                Console.Write("Email @ : ");
                String email = Console.ReadLine();
                if (Program.checkEmail(email))
                {
                    salarie.Email = email;
                    flag = false;
                }
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
                Console.WriteLine("");
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
            Console.Write(s.ToString());
            Console.WriteLine("");

        }
        

        private static int CheckInt(String serv) 
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

        private static Double CheckDouble(String sal)
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
