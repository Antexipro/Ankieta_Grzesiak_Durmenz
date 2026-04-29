using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ankieta_Grzesiak_Durmenz.Models
{
    internal class Survey
    {

        public string Title { get; set; }
        public DateTime EndTime { get; set; }
        public List<Question> Questions { get; set;}
        public Survey(string title, DateTime endTime)
        {
            Title = title;
            EndTime = endTime;
            Questions = new List<Question>();
        }
        public bool IsAtcive()
        {
            return DateTime.Now <= EndTime;
        }
        public virtual void Fill()
        {
            if (!IsAtcive())
            {
                Console.WriteLine("Ankieta zakończona!");
                return;
            }

            foreach(var question in Questions)
            {
                question.Answer();
            }
        }
        public virtual void ShowResults()
        {
            foreach (var q in Questions)
                q.ShowStats();
        }
    }
}
