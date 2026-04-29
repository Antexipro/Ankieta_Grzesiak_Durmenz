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
            List<Survey> surveys = new List<Survey>();
            Survey currentSurvey = null;
            bool running = true;

            while (running)
            {
                Console.Clear();
                DisplayMenu();
                string choice = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(choice))
                {
                    Console.WriteLine("Wybór nie może być pusty!");
                    Pause();
                    continue;
                }

                switch (choice)
                {
                    case "1":
                        var newSurvey = CreateSurvey();
                        surveys.Add(newSurvey);
                        currentSurvey = newSurvey;
                        Console.WriteLine("Dodano ankietę!");
                        break;

                    case "2":
                        if (surveys.Count == 0)
                        {
                            Console.WriteLine("Brak ankiet!");
                            break;
                        }

                        Console.WriteLine("\nDostępne ankiety:");

                        for (int i = 0; i < surveys.Count; i++)
                            Console.WriteLine($"{i + 1}. {surveys[i].Title}");

                        Console.Write("\nWybierz ankietę: ");

                        if (int.TryParse(Console.ReadLine(), out int index) &&
                            index >= 1 && index <= surveys.Count)
                        {
                            currentSurvey = surveys[index - 1];
                        }
                        else
                        {
                            Console.WriteLine("Zły wybór!");
                            break;
                        }

                        Console.WriteLine();
                        currentSurvey.Fill();
                        break;

                    case "3":
                        if (currentSurvey != null)
                            currentSurvey.ShowResults();
                        else
                            Console.WriteLine("Brak wybranej ankiety!");
                        break;

                    case "4":
                        if (currentSurvey == null)
                        {
                            Console.WriteLine("Brak ankiety!");
                            break;
                        }

                        Console.Write("\nPodaj nazwę pliku: ");
                        string name = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(name))
                        {
                            Console.WriteLine("Błąd nazwy!");
                            break;
                        }

                        surveyFileService.SaveToTxt(currentSurvey, $"{name}.txt");
                        break;

                    case "5":
                        running = false;
                        Console.WriteLine("Do widzenia!");
                        break;

                    default:
                        Console.WriteLine("Wybierz opcję od 1 - 5");
                        break;
                }

                Pause();
            }
        }

        private static void Pause()
        {
            Console.WriteLine("Kliknij klawisz...");
            Console.ReadKey();
        }

        private static Survey? CreateSurvey()
        {
            string title;
            while (true)
            {
                Console.Write("\nTytuł ankiety: ");
                title = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(title))
                    break;
                Console.WriteLine("Tytuł nie może być pusty!");
            }

            int days;
            while (true)
            {
                Console.Write("Ile dni ma trwać ankieta: ");
                if (int.TryParse(Console.ReadLine(), out days) && days >= 1 && days <= 365)
                    break;
                Console.WriteLine("Podaj liczbę od 1 do 365!");
            }

            string type;
            while (true)
            {
                Console.WriteLine("\n1. Jednokrotnego wyboru");
                Console.WriteLine("2. Wielokrotnego wyboru");
                type = Console.ReadLine();

                if (type == "1" || type == "2")
                    break;

                Console.WriteLine("Wybierz 1 lub 2!");
            }


            Survey survey;

            if (type == "1")
                survey = new SingleSurvey(title, DateTime.Now.AddDays(days));
            else
                survey = new MultipleSurvey(title, DateTime.Now.AddDays(days));

            int questionNumber;
            while (true)
            {
                Console.Write("\nIle pytań ma mieć ankieta?: ");
                if (int.TryParse(Console.ReadLine(), out questionNumber) && questionNumber >= 2 && questionNumber <= 100)
                    break;

                Console.WriteLine("Min. 2, max. 100!");
            }

            for (int q = 0; q < questionNumber; q++)
            {
                string content;
                while (true)
                {
                    Console.Write($"\nPytanie {q + 1}: ");
                    content = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(content))
                        break;

                    Console.WriteLine("Zawartość nie może być pusta!");
                }


                List<string> answers = new List<string>();

                int count;
                while (true)
                {
                    Console.Write("\nIle odpowiedzi?: ");
                    if (int.TryParse(Console.ReadLine(), out count) && count >= 2 && count <= 10)
                        break;

                    Console.WriteLine("Min.2 Max.10 odpowiedzi!");
                }

                for (int i = 0; i < count; i++)
                {
                    string answer;
                    while (true)
                    {
                        Console.Write($"{(char)('A' + i)}: ");
                        answer = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(answer))
                            break;
                        Console.WriteLine("Odpowiedź nie może być pusta!");
                    }
                    answers.Add(answer);
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
