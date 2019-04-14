using System;
using TimeConverter.Domain.Interfaces.Repositories;
using TimeLibrary = Ptech.Core.DateAndTimeLibrary;

namespace TimeConverter.Domain.Functionality
{
    public class TimeConvertionFunctions : ITimeConverterRepository
    {

        public double ConvertDate12HrTimeToSeconds(DateTime targetDatetime12)
        {
            return TimeLibrary.DateTimeFunctions.ConvertDate12HrTimeToSeconds(targetDatetime12);
        }

        public DateTime ConvertSecondsToCurrentDateTime(double secSinceMidnight)
        {
            return TimeLibrary.DateTimeFunctions.ConvertSecondsToCurrentDateTime(secSinceMidnight);
        }

        public double ConvertString24HrTimeToSeconds(string targetTime24)
        {
            return TimeLibrary.DateTimeFunctions.ConvertString24HrTimeToSeconds(targetTime24);
        }
    }
}
