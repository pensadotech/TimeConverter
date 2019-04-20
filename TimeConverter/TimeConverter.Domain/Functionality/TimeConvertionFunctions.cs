using System;
using TimeConverter.Domain.Interfaces.Repositories;
using TimeLibrary = Ptech.Core.DateAndTimeLibrary;

namespace TimeConverter.Domain.Functionality
{
    /// <summary>
    /// Internal Time Conversion functionality that implements the ITimeConverterRepository
    /// this is implemented through the TimeConverterService, and this services
    /// can receive a differen version of thsi functionality as long it implements ITimeConverterRepository.
    /// </summary>
    public class TimeConvertionFunctions : ITimeConverterRepository
    {
        /// <summary>
        /// Convert Datetime object into second since midnight
        /// </summary>
        /// <param name="targetDatetime12"></param>
        /// <returns></returns>
        public double ConvertDateTimeObjToSeconds(DateTime targetDatetime12)
        {
            return TimeLibrary.DateTimeFunctions.ConvertDateTimeObjToSeconds(targetDatetime12);
        }

        /// <summary>
        /// Convert seconds since midnight into a DateTime object for current date
        /// staring at midnight plust the provided second since midnight.
        /// Example: 44614.235
        /// </summary>
        /// <param name="secSinceMidnight"></param>
        /// <returns></returns>
        public DateTime ConvertSecondsToDateTimeObj(double secSinceMidnight)
        {
            return TimeLibrary.DateTimeFunctions.ConvertSecondsToDateTimeObj(secSinceMidnight);
        }

        /// <summary>
        /// Convert a string hours in 24 hrs format into second since midnight
        /// example: "12:23:34.235"
        /// </summary>
        /// <param name="targetTime24"></param>
        /// <returns></returns>
        public double ConvertString24HrTimeToSeconds(string targetTime24)
        {
            return TimeLibrary.DateTimeFunctions.ConvertString24HrTimeToSeconds(targetTime24);
        }
    }
}
