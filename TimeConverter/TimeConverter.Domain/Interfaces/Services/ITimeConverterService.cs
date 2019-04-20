using System;

namespace TimeConverter.Domain.Interfaces.Services
{
    public interface ITimeConverterService
    {
        /// <summary>
        /// Convert second since midnight to Datetime using the defined repository
        /// </summary>
        /// <param name="secSinceMidnight"></param>
        /// <returns>DateTime</returns>
        DateTime ConvertSecondsToDateTimeObj(double secSinceMidnight);

        /// <summary>
        /// Convert a sring representing time in 24hrs format to seconds since midnight using the defined repository
        /// </summary>
        /// <param name="targetTime24"></param>
        /// <returns>double</returns>
        double ConvertString24HrTimeToSeconds(string targetTime24);

        /// <summary>
        /// Convert a sring representing time in 12hrs format to seconds since midnight using the defined repository
        /// </summary>
        /// <param name="targetDatetime12"></param>
        /// <returns>double</returns>
        double ConvertDateTimeObjToSeconds(DateTime targetDatetime12);
    }
}
