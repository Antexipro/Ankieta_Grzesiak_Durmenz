using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ankieta_Grzesiak_Durmenz.Models
{
    internal class SingleSurvey : Survey
    {
        public SingleSurvey(string title, DateTime endTime) : base(title, endTime)
        {
        }
    }
}
