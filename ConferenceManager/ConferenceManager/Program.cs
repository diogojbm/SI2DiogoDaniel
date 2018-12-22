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
            Console.WriteLine("ATUALIZAR CONFERÊNCIA");
            Console.WriteLine();
            Console.WriteLine("MENU DE OPÇÕES:");
            Console.WriteLine("1. ATUALIZAR DATA LIMITE DE REVISÃO");
            Console.WriteLine("2. ATUALIZAR DATA LIMITE DE SUBMISSÃƒO");
            Console.WriteLine("3. ATUALIZAR PRESIDENTE");
            Console.WriteLine();
            Console.WriteLine("SELECIONE A FUNCIONALIDADE QUE DESEJA E PRIMA A TECLA ENTER:");
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
                Console.WriteLine("ERRO NA SELEÇÃO DA OPERAÇÃO. POR FAVOR TENTE NOVAMENTE.");
            }
        }
        private void AssignJobToUser()
        {
            Console.WriteLine("ATRIBUIR PAPEL A UM UTILIZADOR REGISTADO");
            Console.WriteLine();
            Console.WriteLine("MENU DE OPÇÕES:");
            Console.WriteLine("1. ATRIBUIR PAPEL DE AUTOR");
            Console.WriteLine("2. ATRIBUIR PAPEL DE REVISOR");
            Console.WriteLine("3. ATRIBUIR PAPEL DE PRESIDENTE");
            Console.WriteLine();
            Console.WriteLine("SELECIONE A FUNCIONALIDADE QUE DESEJA E PRIMA A TECLA ENTER:");
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
                service.AssignPresident();
            }
        }

        private void ListCompatibleReviewers()
        {
            Console.WriteLine("LISTAR REVISORES COMPATÍVEIS COM REVISÃO");
            service.ListRevisers();
        }

        private void AssignReviewerToRevision()
        {
            Console.WriteLine("ATRIBUIR REVISOR A UMA REVISÃO");
            service.AssignReviser();
        }

        private void RegisterRevision()
        {
            Console.WriteLine("REGISTAR REVISÃO");
            service.RegisterRevision();
        }

        private void CalcAcceptedSubmissionsRatio()
        {
            Console.WriteLine("PERCENTAGEM DE SUBMISSÕES ACEITES");
            service.CalcAcceptedSubmissionsRatio();
        }

        private void ChangeSubmissionState()
        {
            Console.WriteLine("ALTERAR O ESTADO DE TODAS AS SUBMISSÕES");
            Console.WriteLine();
            Console.WriteLine("PREENCHA OS CAMPOS: <NOME CONFERÊNCIA> <ANO CONFERÊNCIA>");
            string[] parameters = Console.ReadLine().Split(' ');

        }

        private Function DisplayMenu()
        {
            Function function = Function.Unknown;
            try
            {
                Console.WriteLine("CONFERÊNCIA ACADÉMICA");
                Console.WriteLine();
                Console.WriteLine("1. ATUALIZAR CONFERÊNCIA");
                Console.WriteLine("2. ATRIBUIR PAPEL A UM UTILIZADOR REGISTADO");
                Console.WriteLine("3. LISTAR REVISORES COMPATÍVEIS COM REVISÃO");
                Console.WriteLine("4. ATRIBUIR REVISOR A UMA REVISÃO");
                Console.WriteLine("5. REGISTAR REVISÃO");
                Console.WriteLine("6. PERCENTAGEM DE SUBMISSÕES ACEITES");
                Console.WriteLine("7. ALTERAR O ESTADO DE TODAS AS SUBMISSÕES");
                Console.WriteLine("0. SAIR");
                Console.WriteLine();
                Console.WriteLine("SELECIONE A FUNCIONALIDADE QUE DESEJA E PRIMA A TECLA ENTER:");
                var result = Console.ReadLine();
                function = (Function)Enum.Parse(typeof(Function), result);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("ERRO NA SELEÇÃO DE OPERAÇÃO: " + ex.Message);
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
        - L) Falta só fazer. Neste é para fazer um Read e a cada lido aplicar a condição que faz mudar o seu estado
        */
        static void Main(string[] args)
        {
            App.Instance.Run();
        }
    }
}