﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ptech.Core.DateAndTimeLibrary;

namespace Ptech.Core.DateAndTimeLibrary.Test
{
    [TestClass]
    public class DateTimeFunctionsTest
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
                DateTime resultDateTime = DateTimeFunctions.ConvertSecondsToCurrentDateTime(refSeconds);

                // assert
                Assert.AreEqual(refDateTime, resultDateTime);
            }

            [TestMethod]
            public void ConvertString24HrTimeToSecondsTest()
            {
                // reference objects
                double refSeconds = 44614.235;
                string target24Time = "12:23:34.235";

                // convert
                double resultSecs = DateTimeFunctions.ConvertString24HrTimeToSeconds(target24Time);

                Assert.AreEqual(refSeconds, resultSecs);
            }

            [TestMethod]
            public void ConvertDate12HrTimeToSecondsTest()
            {
                double refSeconds = 44614.235;
                // use current date staring from midnight today andd add seconds 
                DateTime targetDateTime = DateTime.Today.AddSeconds(refSeconds);

                // Convert
                double resultSecs = DateTimeFunctions.ConvertDate12HrTimeToSeconds(targetDateTime);

                Assert.AreEqual(refSeconds, resultSecs);
            }
        }
    }
}
