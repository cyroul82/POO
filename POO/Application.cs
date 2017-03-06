using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmailAdresse;
using System.Xml.Linq;

namespace TPCSharp
{
    public class Application
    {
        private static Dictionary<Int32, Commercial> dictionnaryCommercial = new Dictionary<Int32, Commercial>();
        private static Dictionary<Int32, Technicien> listTechnicien = new Dictionary<Int32, Technicien>();

        public static void Main(string[] args)
        {
            LoadFileList();

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
                Console.WriteLine("7:  Modifier un salarié");
                Console.WriteLine("8 : Clear all lists !!!");
                Console.WriteLine("9 : Enregistrer les salariés");
                Console.WriteLine("");

                try
                {
                    Console.Write("Entrez votre Choix : ");
                    Int32 choice =CheckInt(Console.ReadLine(), "Choix incorrect !");
                    Console.WriteLine("");
                    switch (choice)
                    {
                        case 1:
                            AddSalarie();
                            break;
                        case 2:
                            DisplayListSalarie();
                            break;
                        case 3:
                            findSalarieByMatricule();                            
                            break;
                        case 4:
                            GetNombreSalarie();
                            break;
                        case 5:
                            DeleteSalarieByMatricule();
                            break;
                        case 6:
                            SaveToFile();
                            b = false;
                            break;
                        case 7:
                            ModifierSalarie();
                            break;
                        case 8:
                            ClearAllList();
                            break;
                        case 9:
                            SaveToFile();
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

        public static void Start()
        {
            dictionnaryCommercial = new Dictionary<Int32, Commercial>();
            listTechnicien = new Dictionary<Int32, Technicien>();
            LoadFileList();
        }

        /// <summary>
        /// Load from the fil list.xml into the technicien List and the Commercial List
        /// </summary>
        private static void LoadFileList()
        {
            try
            {
                XDocument doc = XDocument.Load("C:\\Users\\Public\\Documents\\list.xml");

                dictionnaryCommercial = doc.Root.Elements("Commercial")
                    .Select(x =>
                                            new Commercial(
                                                    (String)x.Element("Name"),
                                                    (Int32)x.Attribute("Type"),
                                                    (Int32)x.Element("Matricule"),
                                                    (Int32)x.Element("Categorie"),
                                                    (Int32)x.Element("Service"),
                                                    (String)x.Element("Email"),
                                                    (Double)x.Element("Salaire"),
                                                    (Double)x.Element("CA"),
                                                    (Int32)x.Element("Commission")

                                                )
                            ).ToDictionary(c => c.Matricule);

                listTechnicien = doc.Root.Elements("Technicien")
                    .Select(x =>
                                            new Technicien(
                                                    (String)x.Element("Name"),
                                                    (Int32)x.Attribute("Type"),
                                                    (Int32)x.Element("Matricule"),
                                                    (Int32)x.Element("Categorie"),
                                                    (Int32)x.Element("Service"),
                                                    (String)x.Element("Email"),
                                                    (Double)x.Element("Salaire")
                                                )
                            ).ToDictionary(t => t.Matricule);

               
            }
            catch (Exception e)
            {
                Console.WriteLine("Error file : " + e.Message);
            }
        }

        private static Int32 IsMatriculeValid()
        {
            Int32 mat = -1;
            do
            {
                Console.Write("Entrez le matricule : ");
                mat = CheckInt(Console.ReadLine(), "Mauvais numéro de matricule");
            } while (mat == -1);
            return mat;
        }

        /// <summary>
        /// Check whether the matricule already exists
        /// <para>Return the matricule entered or -1 if not !</para>
        /// </summary>
        /// <param name="matricule"></param>
        /// <returns></returns>
        private static Int32 DoesMatriculeExist()
        {
            Int32 mat = -1;
            do
            {
                Console.Write("Entrez le matricule : ");
                mat = CheckInt(Console.ReadLine(), "Mauvais numéro de matricule");

                foreach (KeyValuePair<Int32, Commercial> c in dictionnaryCommercial)
                {
                    if (c.Key == mat)
                    {
                        Console.WriteLine("Le matricule {0} existe déjà ! ", mat);
                        mat = -1;
                    }
                }

                foreach (KeyValuePair<Int32, Technicien> t in listTechnicien)
                {
                    if (t.Key == mat)
                    {
                        Console.WriteLine("Le matricule {0} existe déjà !", mat);
                        mat = -1;
                    }
                }
            }
            while (mat == -1);
            return mat;
        }


        /// <summary>
        /// Change settings of an employee
        /// </summary>
        private static void ModifierSalarie()
        {
            Console.Write("Entrez un matricule : ");
            Int32 matr = CheckInt(Console.ReadLine(), "Mauvais numéro de matricule");

            Salarie s = GetSalarieByMatricule<Salarie>(matr);
            if ( s != null)
            {

                if (s.Type == (Int32)Salarie.Salaries.Commercial)
                {
                    Commercial c = GetSalarieByMatricule<Commercial>(matr);
                    DisplaySalarie(c);
                    Console.WriteLine("1 : Changer Nom");
                    Console.WriteLine("2 : Changer Matricule");
                    Console.WriteLine("3 : Changer Email");
                    Console.WriteLine("4 : Changer Salaire");
                    Console.WriteLine("5 : Changer Chiffre d'Affaire");
                    Console.WriteLine("6 : Changer Commission");

                    Int32 choice = -1;
                    do
                    {
                        Console.Write("Choix : ");
                        choice = CheckInt(Console.ReadLine(), "Erreur choix !");
                        switch (choice)
                        {
                            case 1:
                                Console.Write("Nouveau Nom : ");
                                c.Name = Console.ReadLine();
                                break;
                            case 2:
                                Console.Write("Matricule : ");
                                c.Matricule = DoesMatriculeExist();
                                break;
                            case 3:
                                Console.Write("Email : ");
                                c.Email = GetEmail();
                                break;
                            case 4:
                                Console.Write("Salaire : ");
                                c.Salaire = CheckDouble(Console.ReadLine(), "Saisie salaire incorrect");
                                break;
                            case 5:
                                Console.Write("Chiffre d'Affaire : ");
                                c.ChiffreAffaire = CheckDouble(Console.ReadLine(), "Saisie Chiffre d'affaire érronée");
                                break;
                            case 6:
                                Console.Write("Commission : ");
                                c.Commission = GetCommission();
                                break;
                            default:
                                choice = -1;
                                break;
                        }
                    }
                    while (choice <= 1 && choice >= 4);
                }
                if (s.Type == (Int32)Salarie.Salaries.Technicien)
                {
                    Technicien t = GetSalarieByMatricule<Technicien>(matr);
                    DisplaySalarie(t);
                    Console.WriteLine("1 : Changer Nom");
                    Console.WriteLine("2 : Changer Matricule");
                    Console.WriteLine("3 : Changer Email");
                    Console.WriteLine("4 : Changer Salaire");

                    Int32 choice = -1;
                    do
                    {
                        Console.Write("Choix : ");
                        choice = CheckInt(Console.ReadLine(), "Erreur choix !");
                        switch (choice)
                        {
                            case 1:
                                Console.Write("Nouveau Nom : ");
                                t.Name = Console.ReadLine();
                                break;
                            case 2:
                                Console.Write("Matricule :");
                                t.Matricule = DoesMatriculeExist();
                                break;
                            case 3:
                                Console.Write("Email : ");
                                t.Email = Console.ReadLine();
                                break;
                            case 4:
                                Console.Write("Salaire : ");
                                t.Salaire = CheckDouble(Console.ReadLine(), "Saisie salaire incorrect");
                                break;
                            default:
                                choice = -1;
                                break;
                        }
                    }
                    while (choice <= 1 && choice >= 4);
                }
            }

        }

        /// <summary>
        /// Get the commission from the console
        /// 
        /// </summary>
        /// <returns></returns>
        private static Int32 GetCommission()
        {
            Boolean flag = false;
            Int32 com;
            do
            {
                Console.Write("Commission : ");
                com = CheckInt(Console.ReadLine(), "Erreur saisie Commission");
                if(com >= 0 && com <= 100)
                {
                    flag = true;
                }
                else
                {
                    Console.WriteLine("La commission doit être comprise entre 0 et 100 ");
                    flag = false;
                }

            }
            while (!flag);

            return com;
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
                                  new XElement("Matricule", comm.Key),
                                  new XElement("Name", comm.Value.Name),
                                  new XElement("Categorie", comm.Value.Categorie),
                                  new XElement("Service", comm.Value.Service),
                                  new XElement("Salaire", comm.Value.Salaire),
                                  new XElement("Email", comm.Value.Email),
                                  new XElement("CA", comm.Value.ChiffreAffaire),
                                  new XElement("Commission", comm.Value.Commission)
                                  )
                                  );

                //Save Technicien
                xEle.Add(
                    from tech in listTechnicien
                    select new XElement("Technicien",
                                new XAttribute("Type", tech.Value.Type),
                                  new XElement("Matricule", tech.Value.Matricule),
                                  new XElement("Name", tech.Value.Name),
                                  new XElement("Categorie", tech.Value.Categorie),
                                  new XElement("Service", tech.Value.Service),
                                  new XElement("Salaire", tech.Value.Salaire),
                                  new XElement("Email", tech.Value.Email)
                                  )
                                  );

                xEle.Save("C:\\Users\\Public\\Documents\\list.xml");
                Console.WriteLine("Les salariés sont sauvegardés dans list.xml ! \n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving into file list.xml : " + ex.Message);
            }
        }

        /// <summary>
        /// Retourne le nombre de salariés actuels dans la liste
        /// </summary>
        /// <returns>Int32</returns>
        private static Int32 GetNombreSalarie()
        {
            Int32 total = dictionnaryCommercial.Count + listTechnicien.Count;
            Console.WriteLine("Nombre de salariés enregistrés :" + total );
            Console.WriteLine("Nombre Personnes : " + Personne.Count);
            return total;
        }

        private static void AddSalarie()
        {
            Console.WriteLine("1. Ajouter un commercial");
            Console.WriteLine("2. Ajouter un Technicien");
            Int32 choiceSal = -1;
            do
            {
                Console.Write("Entrez votre Choix : ");
                choiceSal = CheckInt(Console.ReadLine(), "Choix Incorrect");
            }
            while (choiceSal != 1 && choiceSal != 2);

            Int32 matr = DoesMatriculeExist();

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

        /// <summary>
        /// Add an employee
        /// </summary>
        /// <param name="typeSalarie"></param>
        /// <param name="matr"></param>
        public static void AddEmployee(Int32 typeSalarie, Int32 matr)
        {
            Console.Write("Nom : ");

            String name = Console.ReadLine();

            Console.WriteLine("Catégorie : ");
            Console.WriteLine("1. RH");
            Console.WriteLine("2. Compta");
            Console.WriteLine("3. Admin");
            Console.WriteLine("4. Info");

            Int32 cat = -1;
            do {
                Console.Write("Choix catégorie : ");
                switch (CheckInt(Console.ReadLine(), "Mauvais choix de catégorie"))
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
                        Console.WriteLine("Cette catégorie n'existe pas !!!");
                        cat = -1;
                        break;
                }
            }
            while (cat == -1);

            int serv = -1;
            Console.Write("Service : ");
            serv = CheckInt(Console.ReadLine(), "Mauvais choix de service");

            Double sal = 0;
            Console.Write("Salaire(€): ");
            sal = CheckDouble(Console.ReadLine(), "Salaire faux, seulement des chiffres");

            String email = GetEmail();

            if (typeSalarie == (Int32)Salarie.Salaries.Commercial)
            {
                Console.Write("ChiffreAffaire: ");
                Double ca = CheckDouble(Console.ReadLine(), "CA doit contenir seulement des chiffres !");

                Int32 com = GetCommission();
                
                try
                {
                    Commercial commercial = new Commercial(name, (Int32)Salarie.Salaries.Commercial, matr, cat, serv, email, sal, ca, com);
                    dictionnaryCommercial.Add(commercial.Matricule, commercial);
                    Console.WriteLine("");
                    Console.WriteLine("Ajouté ! ");
                    Console.WriteLine("");
                }
                catch(SalaireSalarieException se)
                {
                    Console.WriteLine(se.ToString());
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("ArgumentNullException ");
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Argument Exception");
                }
                

            }
            else if (typeSalarie == (Int32)Salarie.Salaries.Technicien)
            {
                try
                {
                    Technicien technicien = new Technicien(name, (Int32)Salarie.Salaries.Technicien, matr, cat, serv, email, sal);
                    listTechnicien.Add(technicien.Matricule, technicien);
                    Console.WriteLine("");
                    Console.WriteLine("Ajouté ! ");
                    Console.WriteLine("");
                }
                catch (SalaireSalarieException se)
                {
                    Console.WriteLine(se.ToString());
                }
                

                
            }

            
        }

        /// <summary>
        /// Check whether the email is correct and returns it
        /// </summary>
        /// <returns></returns>
        private static String GetEmail()
        {
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

            return em;
        }

        /// <summary>
        /// Affiche la liste total des salariés présent dans la liste
        /// </summary>
        private static void DisplayListSalarie()
        {
            Console.WriteLine("1 : Afficher les commerciaux");
            Console.WriteLine("2 : Afficher les techniciens");
            Console.WriteLine("3 : Afficher tous les salariés" );

            Int32 choice = -1;
            do
            {
                Console.Write("Choix : ");
                choice = CheckInt(Console.ReadLine(), "Entrer une valeur entre 1 et 3 !");
                if (choice < 1 || choice > 3)
                {
                    Console.WriteLine("Entrer une valeur entre 1 et 3 !");
                    choice = -1;
                }

            }
            while (choice == -1);

            switch (choice)
            {
                case 1:
                    DisplayCommercialList();
                    break;
                case 2:
                    DisplayTechnicienList();
                    break;
                case 3:
                    DisplayCommercialList();
                    DisplayTechnicienList();
                    break;
            }

            
            
        }

        /// <summary>
        /// Display the commercial list
        /// </summary>
        private static void DisplayCommercialList()
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
                    Console.WriteLine("LISTE DES COMMERCIAUX");
                    DisplaySalarie(kpv);
                }
            }
        }

        /// <summary>
        /// Display the Technician List
        /// </summary>
        private static void DisplayTechnicienList()
        {
            if (listTechnicien.Count == 0)
            {
                Console.WriteLine("Pas de Technicien !! ");
                Console.WriteLine("");
            }
            else
            {
                foreach (KeyValuePair<Int32, Technicien> tech in listTechnicien)
                {
                    Console.WriteLine("LISTE DES TECHNICIENS");
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
        private static Int32 CheckInt(String serv, String message) 
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
                    PrintErrorMessage(message);
                    serv = Console.ReadLine();
                }
                catch (OverflowException)
                {
                    PrintErrorMessage(message);
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
        private static Double CheckDouble(String sal, String message)
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
        private static void findSalarieByMatricule()
        {
            Console.Write("Matricule : ");
            Int32 mat = CheckInt(Console.ReadLine(), "Mauvais numéro de matricule");

            Salarie s = null;
            
            foreach(KeyValuePair<Int32, Commercial> kvp in dictionnaryCommercial)
            {   
                if(kvp.Key == mat)
                {
                    s = kvp.Value;
                }
            }
            foreach(KeyValuePair<Int32, Technicien>  tech in listTechnicien)
            {
                if(tech.Key == mat)
                {
                    s = tech.Value;
                }
            }


            if(s!= null)
            {
                DisplaySalarie(s);
            }
            else
            {
                Console.WriteLine("Aucun salarié avec ce matricule");
                Console.WriteLine();
            }
        }

        public static void findSalarieByMatricule(Int32 mat)
        {
            

            Salarie s = null;

            foreach (KeyValuePair<Int32, Commercial> kvp in dictionnaryCommercial)
            {
                if (kvp.Key == mat)
                {
                    s = kvp.Value;
                }
            }
            foreach (KeyValuePair<Int32, Technicien> tech in listTechnicien)
            {
                if (tech.Key == mat)
                {
                    s = tech.Value;
                }
            }


            if (s != null)
            {
                DisplaySalarie(s);
            }
            
        }

        public static T GetSalarieByMatricule<T>(Int32 mat) where T:Salarie
        {
            foreach (KeyValuePair<Int32, Commercial> kvp in dictionnaryCommercial)
            {
                if (kvp.Key == mat)
                {
                    
                    return (T)Convert.ChangeType(kvp.Value, typeof(Commercial));
                }
            }
            foreach (KeyValuePair<Int32, Technicien> tech in listTechnicien)
            {
                if (tech.Key == mat)
                {
                    return (T)Convert.ChangeType(tech.Value, typeof(Technicien));
                }
            }
            return null;
        }


        /// <summary>
        /// Affiche une erreur générique
        /// </summary>
        private static void PrintErrorMessage()
        {
            Console.WriteLine("Erreur dans la saisie : ");
            Console.Write("Entrer une nouvelle valeur : ");
        }

        private static void PrintErrorMessage(String message)
        {
            Console.WriteLine("Erreur dans la saisie : " + message);
            Console.Write("Entrer une nouvelle valeur : ");
        }

        /// <summary>
        /// Supprime un salarié avec le matricule
        /// 
        /// </summary>
        /// <param name="mat">Matricule</param>
        private static void DeleteSalarieByMatricule()
        {
            Int32 mat = IsMatriculeValid();
            Boolean isDeleted = false;
            for (int i=0; i <= dictionnaryCommercial.Count-1; i++)
            {
                KeyValuePair<Int32, Commercial> kvp = dictionnaryCommercial.ElementAt(i);
                if(kvp.Key == mat)
                {
                    dictionnaryCommercial.Remove(kvp.Key);
                    Console.WriteLine("Salarié : " + kvp.Value.Name + " a été supprimé de la liste salarié !");
                    Personne.Count--;
                    isDeleted = true;
                }
            }
            for (int i=0; i<=listTechnicien.Count-1; i++)
            {
                KeyValuePair<Int32, Technicien> kvp = listTechnicien.ElementAt(i);
                if (kvp.Key == mat)
                {
                    listTechnicien.Remove(kvp.Key);
                    Console.WriteLine("Salarié : " + kvp.Value.Name + " a été supprimé de la liste salarié !");
                    Personne.Count--;
                }
            }
            if (!isDeleted)
            {
                Console.WriteLine("Le salarié avec ce matricule n'a pas pu être trouvé dans la liste salarié \n et par conséquent non supprimé ! \n Merci de vérifier son Matricule");
            }


         

        }
    }
}
