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
        private static Dictionary<Int32,Commercial> dictionnaryCommercial = new Dictionary<Int32, Commercial>();
        private static List<Technicien> listTechnicien = new List<Technicien>();

        static void Main(string[] args)
        {
            try
            {
                XDocument doc = XDocument.Load("C:\\Users\\Public\\Documents\\list.xml");
                XElement xl = doc.Root;
                IEnumerable<XElement> ieComm = xl.Elements("List").Elements("Commercial");
                IEnumerable<XElement> ieTech = xl.Elements("List").Elements("Technicien");

                dictionnaryCommercial = ieComm
                    .Select(x =>
                                            new Commercial(
                                                    (String)x.Element("Name"),
                                                    (Int32)x.Attribute("Type"),
                                                    (Int32)x.Attribute("Matricule"),
                                                    (Int32)x.Element("Categorie"),
                                                    (Int32)x.Element("Service"),
                                                    (String)x.Element("Email")
                                                )
                            ).ToDictionary(c => c.Matricule);

                listTechnicien = ieTech
                    .Select(x =>
                                            new Technicien(
                                                    (String)x.Element("Name"),
                                                    (Int32)x.Attribute("Type"),
                                                    (Int32)x.Attribute("Matricule"),
                                                    (Int32)x.Element("Categorie"),
                                                    (Int32)x.Element("Service"),
                                                    (String)x.Element("Email")
                                                )
                            ).ToList();



            }
            catch (Exception e)
            {
                Console.WriteLine("Error file : " + e.Message);
            }

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
                Console.WriteLine("8 : Clear all lists !!!");
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
                            Console.WriteLine("Avez vous un matricule (o/n) : ");
                            Console.WriteLine("Entrez votre choix :");

                            String choiceMatricule = Console.ReadLine();
                            choiceMatricule.ToLower();

                            if(choiceMatricule == "o")
                            {
                                Console.Write("Entrez votre matricule : ");

                   
                                int matr = CheckInt(Console.ReadLine());

                                Console.WriteLine("1. Ajouter un commercial");
                                Console.WriteLine("2. Ajouter un Technicien");
                                Console.Write("Entrez votre Choix : ");

                                Int32 choiceSal = Convert.ToInt32(Console.ReadLine());
                                switch (choiceSal)
                                {
                                    case 1:
                                        AddEmployee((Int32)Salarie.Salaries.Commercial, matr);
                                        break;
                                    case 2:
                                        AddEmployee((Int32)Salarie.Salaries.Technicien, matr);
                                        break;
                                    default:
                                        break;
                                }
                            }
                            else
                            {
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
                            SaveToFile();
                            b = false;
                            break;

                        case 7:

                            break;

                        case 8:
                            ClearAllList();
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
        /// Clear the Commercial and Technician lists
        /// </summary>
        private static void ClearAllList()
        {
            listTechnicien.Clear();
            dictionnaryCommercial.Clear();
        }

        /// <summary>
        /// Save the commercial List and the Technican List into a XML file "list.xml"
        /// <para>Path of the file : "C:\Users\Public\Documents\list.xml"</para>
        /// </summary>
        private static void SaveToFile()
        {   
            try
            {
                //Save Commercial
                XElement xEle = new XElement("List");
                
                xEle.Add(
                    from comm in dictionnaryCommercial
                    select new XElement("Commercial",
                                new XAttribute("Type", comm.Value.Type),
                                new XAttribute("Matricule", comm.Key),
                                  new XElement("Name", comm.Value.Name),
                                  new XElement("Categorie", comm.Value.Categorie),
                                  new XElement("Service", comm.Value.Service),
                                  new XElement("Email", comm.Value.Email)
                                  )
                                  );
              
                //Save Technicien
                xEle.Add(
                    from tech in listTechnicien
                    select new XElement("Technicien",
                                new XAttribute("Type", tech.Type),
                                new XAttribute("Matricule", tech.Matricule),
                                  new XElement("Name", tech.Name),
                                  new XElement("Categorie", tech.Categorie),
                                  new XElement("Service", tech.Service),
                                  new XElement("Email", tech.Email)
                                  )
                                  );

                xEle.Save("C:\\Users\\Public\\Documents\\list.xml");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Retourne le nombre de salariés actuels dans la liste
        /// </summary>
        /// <returns>Int32</returns>
        private static Int32 GetNombreSalarie()
        {
            return dictionnaryCommercial.Count + listTechnicien.Count;
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
                dictionnaryCommercial.Add(commercial.Matricule, commercial);

            }
            else if (typeSalarie == (Int32)Salarie.Salaries.Technicien)
            {
                Technicien technicien = new Technicien(name, (Int32)Salarie.Salaries.Technicien);
                technicien.Categorie = cat;
                technicien.Service = serv;
                technicien.Email = em;
                technicien.Salaire = sal;


                listTechnicien.Add(technicien);
            }

            Console.ReadLine();
            Console.WriteLine("");
            Console.WriteLine("Ajouté ! ");
            Console.WriteLine("");

        }

        public static void AddEmployee(Int32 typeSalarie, Int32 matr)
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

                Commercial commercial = new Commercial(name, (Int32)Salarie.Salaries.Commercial, matr);
                commercial.Categorie = cat;
                commercial.Service = serv;
                commercial.Email = em;
                commercial.Salaire = sal;
                commercial.ChiffreAffaire = ca;
                commercial.Commission = com;

                dictionnaryCommercial.Add(commercial.Matricule, commercial);

            }
            else if (typeSalarie == (Int32)Salarie.Salaries.Technicien)
            {
                Technicien technicien = new Technicien(name, (Int32)Salarie.Salaries.Technicien, matr);
                technicien.Categorie = cat;
                technicien.Service = serv;
                technicien.Email = em;
                technicien.Salaire = sal;


                listTechnicien.Add(technicien);
            }

            Console.ReadLine();
            Console.WriteLine("");
            Console.WriteLine("Ajouté ! ");
            Console.WriteLine("");
        }

        public static bool IsMatriculeExist(Int32 matr)
        {

            Boolean flag = false;
           
            return flag;
        }


        /// <summary>
        /// Affiche la liste total des salariés présent dans la liste
        /// </summary>
        private static void DisplayListSalarie()
        {

            if (dictionnaryCommercial.Count == 0)
            {
                Console.WriteLine("Pas de Commercial !! ");
                Console.WriteLine("");
            }
            else
            {
                
                foreach (KeyValuePair<Int32, Commercial> kpv in dictionnaryCommercial)
                {
                    DisplaySalarie(kpv);
                }
            }
            if(listTechnicien.Count == 0)
            {
                Console.WriteLine("Pas de Technicien !! ");
                Console.WriteLine("");
            }
            else
            {
                foreach(Technicien tech in listTechnicien)
                {
                    DisplaySalarie(tech);
                }
            }
        }

        /// <summary>
        /// Affiche un salarié 
        /// </summary>
        /// <param name="s">Prend comme argument un salarié à afficher</param>
        private static void DisplaySalarie<T>(T t)
        {
            Console.Write(t.ToString());
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
            
            foreach(KeyValuePair<Int32, Commercial> kvp in dictionnaryCommercial)
            {   
                if(kvp.Key == mat)
                {
                    s = kvp.Value;
                }
            }
            foreach(Technicien tech in listTechnicien)
            {
                if(tech.Matricule == mat)
                {
                    s = tech;
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
            for (int i=0; i <= dictionnaryCommercial.Count-1; i++)
            {
                KeyValuePair<Int32, Commercial> kvp = dictionnaryCommercial.ElementAt(i);
                if(kvp.Key == mat)
                {
                    dictionnaryCommercial.Remove(kvp.Key);
                    Console.WriteLine("Salarié : " + kvp.Value.Name + " a été supprimé de la liste salarié !");
                    isDeleted = true;
                }
            }
            for (int i=0; i<=listTechnicien.Count-1; i++)
            {
                Technicien tech = listTechnicien.ElementAt(i);
                if (tech.Matricule == mat)
                {
                    listTechnicien.Remove(tech);
                    Console.WriteLine("Salarié : " + tech.Name + " a été supprimé de la liste salarié !");
                }
            }
            if (!isDeleted)
            {
                Console.WriteLine("Le salarié avec ce matricule n'a pas pu être trouvé dans la liste salarié \n et par conséquent non supprimé ! \n Merci de vérifier son Matricule");
            }


         

        }
    }
}
