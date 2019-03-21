using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DateAndTimeLibrary;

namespace TimeConverter.Service
{
    public class TimeConverter : ITimeConverter
    {
        public DateTime ConvertSecondsToCurrentDateTime(double secSinceMidnight)
        {
            return DateAndTimeFunctions.ConvertSecondsToCurrentDateTime(secSinceMidnight);
        }

        public double ConvertString24HrTimeToSeconds(string targetTime24)
        {
            return DateAndTimeFunctions.ConvertString24HrTimeToSeconds(targetTime24);
        }

        public double ConvertDateTimeToSeconds(DateTime targetDatetime)
        {
            return DateAndTimeFunctions.ConvertDateTimeToSeconds(targetDatetime);
        }
    }
}
