using Ankieta_Grzesiak_Durmenz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace Ankieta_Grzesiak_Durmenz.Services
{
    internal class SurveyFileService
    {
        public void SaveToTxt(Survey survey, string fileName)
        {
            if (survey == null)
            {
                Console.WriteLine("Nie masz żadnej ankiety!");
                return;
            }

            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"ANKIETA: {survey.Title}");
            stringBuilder.AppendLine($"Data zakończenia: {survey.EndTime}");
            stringBuilder.AppendLine("====================================");

            foreach (var q in survey.Questions)
            {
                stringBuilder.AppendLine($"\nPytanie {q.Content}");

                for (int i = 0; i < q.Answers.Count; i++)
                {
                    stringBuilder.AppendLine($"{q.Answers[i]} - {q.Votes[i]} głosów");
                }

                stringBuilder.AppendLine("====================================");

            }

            File.WriteAllText(fileName, stringBuilder.ToString());
        }
    }
}
