using System;
using DateAndTimeLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DateAndTimeTest
{
    [TestClass]
    public class DateAndTimeTest
    {
        [TestClass]
        public class UnitTest1
        {
            [TestMethod]
            public void ConvertSecondsToCurrentDateTimeTest()
            {
                // reference objects
                double refSeconds = 44614.235;
                DateTime refDateTime = DateTime.Today.AddSeconds(refSeconds);

                // convert 
                DateTime resultDateTime = DateAndTimeFunctions.ConvertSecondsToCurrentDateTime(refSeconds);

                Assert.AreEqual(refDateTime, resultDateTime);
            }

            [TestMethod]
            public void ConvertString24HrTimeToSecondsTest()
            {
                // reference objects
                double refSeconds = 44614.235;
                string target24Time = "12:23:34.235";

                // convert
                double resultSecs = DateAndTimeFunctions.ConvertString24HrTimeToSeconds(target24Time);

                Assert.AreEqual(refSeconds, resultSecs);
            }

            [TestMethod]
            public void ConvertDateTimeToSecondsTest()
            {
                double refSeconds = 44614.235;
                DateTime targetDateTime = DateTime.Today.AddSeconds(refSeconds);

                // Convert
                double resultSecs = DateAndTimeFunctions.ConvertDateTimeToSeconds(targetDateTime);

                Assert.AreEqual(refSeconds, resultSecs);

            }
        }
    }
}
