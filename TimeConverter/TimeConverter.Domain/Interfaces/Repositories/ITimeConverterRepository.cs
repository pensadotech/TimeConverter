using System;

namespace TimeConverter.Domain.Interfaces.Repositories
{
    public interface ITimeConverterRepository
    {
        /// <summary>
        /// Convert Datetime object into second since midnight
        /// </summary>
        /// <param name="targetDatetime12"></param>
        /// <returns>double</returns>
        double ConvertDateTimeObjToSeconds(DateTime targetDatetime12);

        /// <summary>
        /// Convert seconds since midnight into a DateTime object for current date
        /// staring at midnight plust the provided second since midnight.
        /// Example: 44614.235
        /// </summary>
        /// <param name="secSinceMidnight"></param>
        /// <returns>DateTime</returns>
        DateTime ConvertSecondsToDateTimeObj(double secSinceMidnight);

        /// <summary>
        /// Convert a string hours in 24 hrs format into second since midnight
        /// example: "12:23:34.235"
        /// </summary>
        /// <param name="targetTime24"></param>
        /// <returns>double</returns>
        double ConvertString24HrTimeToSeconds(string targetTime24);

        

    }
}
