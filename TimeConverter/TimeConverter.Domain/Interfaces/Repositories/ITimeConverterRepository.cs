using System;

namespace TimeConverter.Domain.Interfaces.Repositories
{
    public interface ITimeConverterRepository
    {
        /// <summary>
        /// Convert second since midnight to Datetime
        /// </summary>
        /// <param name="secSinceMidnight"></param>
        /// <returns>DateTime</returns>
        DateTime ConvertSecondsToCurrentDateTime(double secSinceMidnight);

        /// <summary>
        /// Convert a sring representing time in 24hrs format to seconds since midnight
        /// </summary>
        /// <param name="targetTime24"></param>
        /// <returns>double</returns>
        double ConvertString24HrTimeToSeconds(string targetTime24);

        /// <summary>
        /// Convert a sring representing time in 12hrs format to seconds since midnight
        /// </summary>
        /// <param name="targetDatetime12"></param>
        /// <returns>double</returns>
        double ConvertDate12HrTimeToSeconds(DateTime targetDatetime12);

    }
}
