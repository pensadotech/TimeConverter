using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeConverter.Domain.Functionality;
using TimeConverter.Domain.Interfaces.Repositories;

namespace TimeConverter.Domain.Test
{
    /// <summary>
    /// Domain unit testing
    /// This program test the  functionality for time conversion using the 
    /// provide internal lirbary 
    /// </summary>
    [TestClass]
    public class TimeConvertionFunctionsTest
    {
        private TimeConvertionFunctions _timeConvrtFunct; 

        [TestInitialize]
        public void Initialize()
        {
            // Create a Time convertion repo
            _timeConvrtFunct = new TimeConvertionFunctions();
        }

        [TestCleanup]
        public void CleanUp()
        {
            // nothing
        }

        [TestMethod]
        public void ConvertDate12HrTimeToSecondsTest()
        {
            // Arrange
            double refSeconds = 44614.235;
            // use current date staring from midnight today andd add seconds 
            DateTime targetDateTime = DateTime.Today.AddSeconds(refSeconds);

            // Act
            double resultSecs = _timeConvrtFunct.ConvertDateTimeObjToSeconds(targetDateTime);

            // Assert
            Assert.AreEqual(refSeconds, resultSecs);
        }

        [TestMethod]
        public void ConvertSecondsToCurrentDateTimeTest()
        {
            // Arrange
            double refSeconds = 44614.235;
            DateTime refDateTime = DateTime.Today.AddSeconds(refSeconds);

            // Act
            DateTime resultDateTime = _timeConvrtFunct.ConvertSecondsToDateTimeObj(refSeconds);

            // Assert 
            Assert.AreEqual(refDateTime, resultDateTime);
        }

        [TestMethod]
        public void ConvertString24HrTimeToSeconds()
        {
            // Arrange
            // Arrange
            double refSeconds = 44614.235;
            string target24Time = "12:23:34.235";

            // Act
            double resultSecs = _timeConvrtFunct.ConvertString24HrTimeToSeconds(target24Time);

            // Assert
            Assert.AreEqual(refSeconds, resultSecs);
        }
    }
}
