using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceManager
{
    class App
    {
        private AppService service;
        private enum Function
        {
            Unknown = -1,
            Exit,
            UpdateConference,
            AssignJobToUser,
            ListCompatibleReviewers,
            AssignReviewerToRevision,
            RegisterRevision,
            AcceptedSubmissionsPercentage,
            ChangeSubmissionState
        }

        private App()
        {
            service = new AppService();
            mainOptions = new Dictionary<Function, MainMenuOptions>();
            mainOptions.Add(Function.UpdateConference, UpdateConference);
            mainOptions.Add(Function.AssignJobToUser, AssignJobToUser);
            mainOptions.Add(Function.ListCompatibleReviewers, ListCompatibleReviewers);
            mainOptions.Add(Function.AssignReviewerToRevision, AssignReviewerToRevision);
            mainOptions.Add(Function.RegisterRevision, RegisterRevision);
            mainOptions.Add(Function.AcceptedSubmissionsPercentage, CalcAcceptedSubmissionsRatio);
            mainOptions.Add(Function.ChangeSubmissionState, ChangeSubmissionState);
        }

        private static App __instance;
        public static App Instance
        {
            get
            {
                if (__instance == null)
                    __instance = new App();
                return __instance;
            }
            private set { }
        }

        private delegate void MainMenuOptions();
        private System.Collections.Generic.Dictionary<Function, MainMenuOptions> mainOptions;

        private void UpdateConference()
        {
            Console.WriteLine("UPDATE CONFERENCE");
            Console.WriteLine();
            Console.WriteLine("OPTIONS MENU:");
            Console.WriteLine("1. UPDATE REVISION LIMIT DATE");
            Console.WriteLine("2. UPDATE SUBMISSION LIMIT DATE");
            Console.WriteLine("3. UPDATE PRESIDENT");
            Console.WriteLine();
            Console.WriteLine("SELECT THE FUNCTIONALITY YOU WANT AND PRESS ENTER KEY:");
            var op = Console.ReadLine().ToString();

            if (op == "1")
            {
                service.UpdateRevisionLimitDate();
            }

            else if (op == "2")
            {
                service.UpdateSubmissionLimitDate();
            }
            else if (op == "3")
            {
                service.UpdateConferencePresident();
            }
            else
            {
                Console.WriteLine("OPERATION SELECTION ERROR. PLEASE TRY AGAIN.");
            }
        }
        private void AssignJobToUser()
        {
            Console.WriteLine("ASSIGN ROLE TO A REGISTERED USER");
            Console.WriteLine();
            Console.WriteLine("OPTIONS MENU:");
            Console.WriteLine("1. ASSIGN AUTHOR ROLE");
            Console.WriteLine("2. ASSIGN REVISER ROLE DE REVISOR");
            Console.WriteLine("3. ASSIGN PRESIDENT ROLE");
            Console.WriteLine();
            Console.WriteLine("SELECT THE FUNCTIONALITY YOU WANT AND PRESS ENTER KEY:");
            var op = Console.ReadLine().ToString();

            if (op == "1")
            {
                service.AssignAuthorRole();
            }
            else if (op == "2")
            {
                service.AssignReviserRole();
            }
            else if (op == "3")
            {
                service.AssignPresidentRole();
            }
        }

        private void ListCompatibleReviewers()
        {
            Console.WriteLine("LIST COMPATIBLE REVISERS WITH A SPECIFIC REVISION");
            service.ListRevisers();
        }

        private void AssignReviewerToRevision()
        {
            Console.WriteLine("ASSIGN REVISER TO A REVISION");
            service.AssignReviser();
        }

        private void RegisterRevision()
        {
            Console.WriteLine("REGISTER REVISION");
            service.RegisterRevision();
        }

        private void CalcAcceptedSubmissionsRatio()
        {
            Console.WriteLine("ACCEPTED SUBMISSIONS RATIO");
            service.CalcAcceptedSubmissionsRatio();
        }

        private void ChangeSubmissionState()
        {
            Console.WriteLine("CHANGE ALL THE SUBMISSIONS' STATE");
            Console.WriteLine();
            Console.WriteLine("FILL THE FIELDS: <CONFERENCE NAME> <CONFERENCE YEAR>");
            string[] parameters = Console.ReadLine().Split(' ');

        }

        private Function DisplayMenu()
        {
            Function function = Function.Unknown;
            try
            {
                Console.WriteLine("CONFERENCE MANAGER");
                Console.WriteLine();
                Console.WriteLine("1. UPDATE CONFERENCE");
                Console.WriteLine("2. ASSIGN A ROLE TO A REGISTERED USER");
                Console.WriteLine("3. LIST COMPATIBLE REVISERS WITH A REVISION");
                Console.WriteLine("4. ASSIGN A REVISION TO A REVISER");
                Console.WriteLine("5. REGISTER REVISION");
                Console.WriteLine("6. ACCEPTED SUBMISSIONS RATIO");
                Console.WriteLine("7. CHANGE ALL THE SUBMISSIONS' STATE");
                Console.WriteLine("0. EXIT");
                Console.WriteLine();
                Console.WriteLine("SELECT THE FUNCTIONALITY YOU WANT AND PRESS ENTER KEY:");
                var result = Console.ReadLine();
                function = (Function)Enum.Parse(typeof(Function), result);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("OPERATION SELECTION ERROR " + ex.Message);
            }

            return function;
        }

        public void Run()
        {
            Function userInput = Function.Unknown;
            do
            {
                Console.Clear();
                userInput = DisplayMenu();
                Console.Clear();
                try
                {
                    mainOptions[userInput]();
                    Console.ReadKey();
                }
                catch (KeyNotFoundException ex)
                {
                }

            } while (userInput != Function.Exit);
        }
    }

    class Program
    {   /*
        - H) O resultado é um select a uma tabela temporária. Como é que uso mapeamento para retornar a tabela?
        - L) Falta só fazer. Neste é para fazer um Read e a cada lido aplicar a condição que faz mudar o seu estado
        */
        static void Main(string[] args)
        {
            App.Instance.Run();
        }
    }
}