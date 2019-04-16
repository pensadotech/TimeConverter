using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeConverter.Domain.Functionality;
using TimeConverter.Domain.Interfaces.Repositories;

namespace TimeConverter.Domain.Test
{
    [TestClass]
    public class TimeConvertionFunctionsTest
    {
        private ITimeConverterRepository _timeConvrtRepo; 

        [TestInitialize]
        public void Initialize()
        {
            // Create a Time convertion repo
            _timeConvrtRepo  = new TimeConvertionFunctions();
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
            double resultSecs = _timeConvrtRepo.ConvertDate12HrTimeToSeconds(targetDateTime);

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
            DateTime resultDateTime = _timeConvrtRepo.ConvertSecondsToCurrentDateTime(refSeconds);

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
            double resultSecs = _timeConvrtRepo.ConvertString24HrTimeToSeconds(target24Time);

            // Assert
            Assert.AreEqual(refSeconds, resultSecs);
        }
    }
}
