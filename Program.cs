using Ankieta_Grzesiak_Durmenz.Models;
using Ankieta_Grzesiak_Durmenz.Interfaces;
using Ankieta_Grzesiak_Durmenz.Services;
using System.Text.Json;



namespace Ankieta_Grzesiak_Durmenz
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SurveyFileService surveyFileService = new SurveyFileService();
            Survey survey = null;
            bool running = true;

            while (running != false)
            {
                DisplayMenu();
                string choice = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(choice))
                {
                    Console.WriteLine("Wybór nie może być pusty!");
                    continue;
                }

                switch (choice)
                {
                    case "1":
                        survey = CreateSurvey();
                        break;
                    case "2":
                        Console.WriteLine();
                        if (survey != null)
                            survey.Fill();
                        break;
                    case "3":
                        Console.WriteLine();
                        if (survey != null)
                            survey.ShowResults();
                        break;
                    case "4":
                        Console.Write("\nPodaj nazwę ankiety: ");
                        string name = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(name))
                        {
                            Console.WriteLine("Błąd");
                            return;
                        }
                        if (survey != null)
                            surveyFileService.SaveToTxt(survey, $"{name}.txt");
                        else
                            Console.WriteLine("Najpierw utwórz ankietę!");
                        break;
                    case "5":
                        Console.WriteLine("Do widzenia!");
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Wybierz opcję od 1 - 5");
                        break;
                }
            }
        }

        private static Survey? CreateSurvey()
        {
            Console.Write("\nTytuł ankiety: ");
            string title = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Tytuł nie może być pusty!");
            }
            Console.Write("Ile dni ma trwać ankieta: ");
            int.TryParse(Console.ReadLine(), out int days);
            if(days < 1 || days > 365)
            {
                Console.WriteLine("Zła ilość dni!");
            }
            Console.WriteLine("\n1.Jenokrotnego wyboru");
            Console.WriteLine("2.Wielkorotnego wyboru");
            string type = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(type))
            {
                Console.WriteLine("Typ nie może być pusty!");
            }

            Survey survey;

            if (type == "1")
                survey = new SingleSurvey(title, DateTime.Now.AddDays(days));
            else
                survey = new MultipleSurvey(title, DateTime.Now.AddDays(days));

            Console.Write("Ile pytań ma mieć ankieta?: ");
            int.TryParse(Console.ReadLine(), out int questionNumber);
            if(questionNumber < 2 || questionNumber > 100)
            {
                Console.WriteLine("Zła ilość pytań min.2 max.100");
            }
            for (int q = 0; q < questionNumber; q++)
            {
                Console.Write($"Pytanie {q+1}: ");
                string content = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(content))
                {
                    Console.WriteLine("Zawartość nie może być pusta!");
                }

                List<string> answers = new List<string>();

                Console.Write("Ile odpowiedzi?: ");
                int count = int.Parse(Console.ReadLine());
                if(count < 2 || count > 10)
                {
                    Console.WriteLine("Zła ilośc odpowiedzi! min.2 max.10");
                }

                for (int i = 0; i < count; i++)
                {
                    Console.Write($"{(char)('A' + i)}: ");
                    answers.Add(Console.ReadLine());
                }

                if (type == "1")
                    survey.Questions.Add(new SingleChoiceQuestion(content, answers));
                else
                    survey.Questions.Add(new MultipleChoiceQuestion(content, answers));
            }
            return survey;
        }

        private static void DisplayMenu()
        {
            Console.WriteLine("\n===== Ankiety =====");
            Console.WriteLine("1. Utwórz ankietę");
            Console.WriteLine("2. Wypełnij ankietę");
            Console.WriteLine("3. Wyniki");
            Console.WriteLine("4. Zapisz do TXT");
            Console.WriteLine("5. Wyjdź");
            Console.Write("Wybierz opcję: ");
        }
    }
}
