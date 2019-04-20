using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptech.Core.DateAndTimeLibrary
{
    public class DateTimeFunctions
    {
        /// <summary>
        /// Convert seconds since midnight into a DateTime object for current date
        /// staring at midnight plust the provided second since midnight.
        /// Example: 44614.235
        /// </summary>
        /// <param name="secSinceMidnight"></param>
        /// <returns>DateTime</returns>
        public static DateTime ConvertSecondsToDateTimeObj(double secSinceMidnight)
        {
            DateTime dt = DateTime.Today;

            return dt.AddSeconds(secSinceMidnight);
        }

        /// <summary>
        /// Convert a string hours in 24 hrs format into second since midnight
        /// example: "12:23:34.235"
        /// </summary>
        /// <param name="targetTime24"></param>
        /// <returns>double</returns>
        public static double ConvertString24HrTimeToSeconds(string targetTime24)
        {
            // Example 12:23:34.235 
            string[] timeArray = targetTime24.Split(':');

            int secHrs = 3600 * int.Parse(timeArray[0]);
            int secMin = 60 * int.Parse(timeArray[1]);
            double seconds = double.Parse(timeArray[2]);

            double totaSecs = secHrs + secMin + seconds;

            return totaSecs;
        }

        /// <summary>
        /// Convert Datetime object into second since midnight
        /// </summary>
        /// <param name="targetDatetime"></param>
        /// <returns></returns>
        public static double ConvertDateTimeObjToSeconds(DateTime targetDatetime)
        {
            int secHrs = 3600 * targetDatetime.Hour;
            int secMin = 60 * targetDatetime.Minute;
            double seconds = targetDatetime.Second + ((double)targetDatetime.Millisecond / 1000);

            double totaSecs = secHrs + secMin + seconds;

            return totaSecs;
        }

        /// <summary>
        /// Obtain time difference in seconds
        /// </summary>
        /// <param name="fromTime"></param>
        /// <param name="toTime"></param>
        /// <returns></returns>
        public static double TimeDifferenceInSeconds(double fromTime, double toTime)
        {
            double timeDiff = toTime - fromTime;
            return timeDiff;
        }

        /// <summary>
        /// Obtain time differences in hours using two string representing hours in 24 hrs fromat
        /// </summary>
        /// <param name="fromTime"></param>
        /// <param name="toTime"></param>
        /// <returns></returns>
        public static double String24TimeDifference(string fromTime, string toTime)
        {
            // Convert FROM time to seconds (Example 12:23:34.235 )
            string[] timeArray = fromTime.Split(':');

            int secHrs = 3600 * int.Parse(timeArray[0]);
            int secMin = 60 * int.Parse(timeArray[1]);
            double seconds = double.Parse(timeArray[2]);

            double fromSecsTotal = secHrs + secMin + seconds;

            // Convert TO time to seconds (Example 12:23:34.235 )
            timeArray = toTime.Split(':');

            //double time1 = double.Parse(timeArray[2]);
            //int tehSec = int.Parse(timeArray[2]);
            //double msec = (time1 - tehSec) * 1000;

            //DateTime toTime = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,
            //    int.Parse(timeArray[0]), int.Parse(timeArray[1]), tehSec, msec);

            secHrs = 3600 * int.Parse(timeArray[0]);
            secMin = 60 * int.Parse(timeArray[1]);
            seconds = double.Parse(timeArray[2]);

            double toSecsTotal = secHrs + secMin + seconds;

            // Calculate teh difference
            double timeDiff = toSecsTotal - fromSecsTotal;

            return timeDiff;
        }
    }
}
