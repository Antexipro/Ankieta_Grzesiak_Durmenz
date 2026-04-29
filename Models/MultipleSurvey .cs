using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ankieta_Grzesiak_Durmenz.Models
{
    internal class MultipleSurvey : Survey
    {
        public MultipleSurvey(string title, DateTime endTime) : base(title, endTime)
        {
        }
    }
}
