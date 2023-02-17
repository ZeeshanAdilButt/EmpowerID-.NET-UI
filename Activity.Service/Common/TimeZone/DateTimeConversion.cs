using System;
using System.Collections.Generic;
using System.Text;

namespace WaitLess.Service.Common.TimeZone
{
    public static class DateTimeConversion
    {
        public static string Convert12(TimeSpan timeSpan)
        {
            var hours = timeSpan.Hours;
            var minutes = timeSpan.Minutes;
            var amPmDesignator = "AM";
            if (hours == 0)
                hours = 12;
            else if (hours == 12)
                amPmDesignator = "PM";
            else if (hours > 12)
            {
                hours -= 12;
                amPmDesignator = "PM";
            }
            return String.Format("{0}:{1:00} {2}", hours, minutes, amPmDesignator);
        }

        public static string FormatDate(DateTime? datetime)
        {
            if (datetime is null)
            {
                return "";
            }

            return datetime.Value.ToString("MM/dd/yyyy");
        }
    }
}
