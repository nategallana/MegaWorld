using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mega_World_Mall_Linking.Helpers
{
    public class HourCodeHelper
    {
        public static string GetHourCode(DateTime time)
        {
            int hour = time.Hour;
            int minute = time.Minute;

            if ((hour == 0 && minute >= 1) || (hour == 1 && minute <= 0))
                return "24";

            int hourCode = hour;
            if (minute >= 1)
                hourCode += 1;

            if (hourCode == 0)
                hourCode = 24;
            else if (hourCode > 24)
                hourCode = 1;

            return hourCode.ToString("D2");
        }
    }
}
