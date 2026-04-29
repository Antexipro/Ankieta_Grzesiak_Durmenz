using System;
using System.Collections.Generic;

namespace Ankieta_Grzesiak_Durmenz.Models
{
    internal class SingleChoiceQuestion : Question
    {
        public SingleChoiceQuestion(string content, List<string> answers)
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

            Console.Write("Wybierz 1 odpowiedź: ");
            int.TryParse(Console.ReadLine(), out int choice);

            if (choice >= 1 && choice <= Answers.Count)
                Votes[choice - 1]++;
        }
    }
}