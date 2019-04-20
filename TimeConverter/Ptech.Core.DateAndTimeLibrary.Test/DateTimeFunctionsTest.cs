using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ptech.Core.DateAndTimeLibrary.Test
{
    [TestClass]
    public class DateTimeFunctionsTest
    {
        [TestClass]
        public class DateTimeFunctionTest
        {
            [TestInitialize]
            public void Initialize()
            {
                // nothing
            }

            [TestCleanup]
            public void CleanUp()
            {
                // nothing
            }

            [TestMethod]
            public void ConvertSecondsToDateTimeObjTest()
            {
                // Arrange
                double refSeconds = 44614.235;
                DateTime refDateTime = DateTime.Today.AddSeconds(refSeconds);

                // Act
                DateTime resultDateTime = DateTimeFunctions.ConvertSecondsToDateTimeObj(refSeconds);

                // Assert
                Assert.AreEqual(refDateTime, resultDateTime);
            }

            [TestMethod]
            public void ConvertString24HrTimeToSecondsTest()
            {
                // Arrange
                double refSeconds = 44614.235;
                string target24Time = "12:23:34.235";

                // Act
                double resultSecs = DateTimeFunctions.ConvertString24HrTimeToSeconds(target24Time);

                // Assert
                Assert.AreEqual(refSeconds, resultSecs);
            }

            [TestMethod]
            public void ConvertDateTimeObjToSecondsTest()
            {
                // Arrange
                double refSeconds = 44614.235;
                // use current date staring from midnight today andd add seconds 
                DateTime targetDateTime = DateTime.Today.AddSeconds(refSeconds);

                // Act
                double resultSecs = DateTimeFunctions.ConvertDateTimeObjToSeconds(targetDateTime);

                // Assert
                Assert.AreEqual(refSeconds, resultSecs);
            }
        }
    }
}
