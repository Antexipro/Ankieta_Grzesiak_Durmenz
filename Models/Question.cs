using Ankieta_Grzesiak_Durmenz.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ankieta_Grzesiak_Durmenz.Models
{
    internal class Question : IQuestion
    {
        public string Content { get; set; }
        public List<string> Answers { get; set; }
        public List<int> Votes { get; set; }

        public Question(string content, List<string> answers)
        {
            Content = content;
            Answers = answers;

            Votes = new List<int>();

            for (int i = 0; i < answers.Count; i++)
                Votes.Add(0);
        }

        public virtual void Answer() { }

        public virtual void Display()
        {
            Console.WriteLine(Content);

            for (int i = 0; i < Answers.Count; i++)
                Console.WriteLine($"{i + 1}. {Answers[i]}");
        }

        public virtual void ShowStats()
        {
            int totalVotes = Votes.Sum();

            Console.WriteLine($"\nStatystyki: {Content}");

            for (int i = 0; i < Answers.Count; i++)
            {
                double percent = totalVotes == 0 ? 0: (double)Votes[i] / totalVotes * 100;

                Console.WriteLine($"{Answers[i]} - {Votes[i]} głosów ({percent}%)");
            }
        }
    }
}
