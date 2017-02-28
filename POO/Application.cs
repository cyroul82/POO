using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmailAdresse;
using System.Xml.Linq;

namespace TPCSharp
{
    class Application
    {

        private static List<Salarie> listSalarie = new List<Salarie>();
        private static List<Commercial> listCommercial = new List<Commercial>();
        private static List<Technicien> listTechnicien = new List<Technicien>();

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
                            Console.WriteLine("2. Ajouter un Technicien");
                            Console.Write("Entrez votre Choix : ");
                            Int32 choiceSal = Convert.ToInt32(Console.ReadLine());
                            switch (choiceSal)
                            {
                                case 1:
                                    AddEmployee((Int32)Salarie.Salaries.Commercial);
                                    break;
                                case 2:
                                    AddEmployee((Int32)Salarie.Salaries.Technicien);
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

        /// <summary>
        /// Retourne le nombre de salariés actuels dans la liste
        /// </summary>
        /// <returns>Int32</returns>
        private static Int32 GetNombreSalarie()
        {
            return listSalarie.Count;
        }

        public static void AddEmployee(Int32 typeSalarie)
        {
            Console.Write("Nom : ");
            
            String name = Console.ReadLine();

            Console.WriteLine("Catégorie : ");
            Console.WriteLine("1. RH");
            Console.WriteLine("2. Compta");
            Console.WriteLine("3. Admin");
            Console.WriteLine("4. Info");

            int cat = -1;
            switch (CheckInt(Console.ReadLine()))
            {
                case 1:
                    cat = (Int32)Salarie.Categories.RH;
                    break;
                case 2:
                    cat = (Int32)Salarie.Categories.Compta;
                    break;
                case 3:
                    cat = (Int32)Salarie.Categories.Admin;
                    break;
                case 4:
                    cat = (Int32)Salarie.Categories.Info;
                    break;
                default:

                    break;
            }

            int serv = -1;
            Console.Write("Service : ");
            serv = CheckInt(Console.ReadLine());

            Double sal = 0;
            Console.Write("Salaire(€): ");
            sal = CheckDouble(Console.ReadLine());

            Boolean flag = true;
            String em = null;
            while (flag)
            {
                Console.Write("Email @ : ");
                String email = Console.ReadLine();

                if (Program.checkEmail(email))
                {
                    em = email;
                    flag = false;
                }
            }


            if (typeSalarie == (Int32)Salarie.Salaries.Commercial)
            {
                Double ca = 0;
                Console.Write("ChiffreAffaire: ");
                ca = CheckDouble(Console.ReadLine());

                int com = -1;
                Console.Write("Commission (%): ");
                com = CheckInt(Console.ReadLine());

                Commercial commercial = new Commercial(name, (Int32)Salarie.Salaries.Commercial);
                commercial.Categorie = cat;
                commercial.Service = serv;
                commercial.Email = em;
                commercial.Salaire = sal;
                commercial.ChiffreAffaire = ca;
                commercial.Commission = com;
                listCommercial.Add(commercial);
                listSalarie.Add(commercial);

                try
                {
                    var xEle = new XElement("Commercials",
                        from comm in listCommercial
                        select new XElement("Commercial",
                                    new XAttribute("Matricule", comm.Matricule),
                                      new XElement("Name", comm.Name),
                                      new XElement("Categorie", comm.Categorie),
                                      new XElement("Service", comm.Service),
                                      new XElement("Email", comm.Email)

                                      ));



                    xEle.Save("C:\\Users\\Public\\Documents\\commercial.xml");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }


            }
            else if (typeSalarie == (Int32)Salarie.Salaries.Technicien)
            {
                Technicien technicien = new Technicien(name, (Int32)Salarie.Salaries.Technicien);
                technicien.Categorie = cat;
                technicien.Service = serv;
                technicien.Email = em;
                technicien.Salaire = sal;


                listTechnicien.Add(technicien);
                listSalarie.Add(technicien);
                try
                {
                    var xEle = new XElement("Techniciens",
                        from tech in listTechnicien
                        select new XElement("Technicien",
                                    new XAttribute("Matricule", tech.Matricule),
                                      new XElement("Name", tech.Name),
                                      new XElement("Categorie", tech.Categorie),
                                      new XElement("Service", tech.Service),
                                      new XElement("Email", tech.Email)

                                      ));



                    xEle.Save("C:\\Users\\Public\\Documents\\technicien.xml");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.ReadLine();
            Console.WriteLine("");
            Console.WriteLine("Ajouté ! ");
            Console.WriteLine("");



            try
            {
                var xEle = new XElement("Salariés",
                    from sala in listSalarie
                    select new XElement("Salarie",
                                new XAttribute("Type", sala.Type),
                                new XAttribute("Matricule", sala.Matricule),
                                  new XElement("Name", sala.Name),
                                  new XElement("Categorie", sala.Categorie),
                                  new XElement("Service", sala.Service),
                                  new XElement("Email", sala.Email)

                                  ));



                xEle.Save("C:\\Users\\Public\\Documents\\salariés.xml");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }


        /// <summary>
        /// Affiche la liste total des salariés présent dans la liste
        /// </summary>
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

        /// <summary>
        /// Affiche un salarié 
        /// </summary>
        /// <param name="s">Prend comme argument un salarié à afficher</param>
        private static void DisplaySalarie(Salarie s)
        {
            Console.Write(s.ToString());
            Console.WriteLine("");

        }
        

        /// <summary>
        /// Boucle pour vérifier si le paramètre (String) est convertible en Int32
        /// 
        /// </summary>
        /// <param name="serv"</param>
        /// <returns></returns>
        private static Int32 CheckInt(String serv) 
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

        /// <summary>
        /// Boucle pour vérifier si le paramètre (String) est convertible en Int32
        /// 
        /// </summary>
        /// <param name="serv"</param>
        /// <returns></returns>
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

        /// <summary>
        /// Recherche un salarié dans la liste par matricule
        /// </summary>
        /// <param name="mat">Matricule Salarié</param>
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

        /// <summary>
        /// Affiche une erreur générique
        /// </summary>
        private static void PrintErrorMessage()
        {
            Console.WriteLine("Erreur dans la saisie : ");
            Console.WriteLine("Entrer une nouvelle valeur :\n ");
        }

        /// <summary>
        /// Supprime un salarié avec le matricule
        /// 
        /// </summary>
        /// <param name="mat">Matricule</param>
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
