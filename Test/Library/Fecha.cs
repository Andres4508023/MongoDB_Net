using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Library
{
    public class Fecha
    {
        public DateTime GetDateOneYearLater(DateTime Date)
        {
            DateTime dateOneYearLater = Date.AddYears(1);
            return dateOneYearLater;
        }
    }
}
