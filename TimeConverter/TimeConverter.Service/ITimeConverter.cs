using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeConverter.Service
{
    public interface ITimeConverter
    {
        DateTime ConvertSecondsToCurrentDateTime(double secSinceMidnight);

        double ConvertString24HrTimeToSeconds(string targetTime24);

        double ConvertDateTimeToSeconds(DateTime targetDatetime);


    }
}
