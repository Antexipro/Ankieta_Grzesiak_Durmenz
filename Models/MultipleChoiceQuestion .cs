using System;
using System.Collections.Generic;

namespace Ankieta_Grzesiak_Durmenz.Models
{
    internal class MultipleChoiceQuestion : Question
    {
        public MultipleChoiceQuestion(string content, List<string> answers)
            : base(content, answers)
        {
        }

        private static List<int> CreateVotes(int count)
        {
            List<int> votes = new List<int>();

            for (int i = 0; i < count; i++)
                votes.Add(0);

            return votes;
        }

        public override void Answer()
        {
            Display();

            Console.Write("Podaj numery odpowiedzi (np. 1,3): ");
            string inputValues = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(inputValues))
                return;

            string[] selectedValues = inputValues.Split(',');

            foreach (string item in selectedValues)
            {
                int nr;

                if (int.TryParse(item.Trim(), out nr))
                {
                    if (nr >= 1 && nr <= Answers.Count)
                        Votes[nr - 1]++;
                }
            }
        }
    }
}