using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TimeConverter.Domain.Interfaces.Repositories;

namespace TimeConverter.Domain.Test
{
    /// <summary>
    /// Domain unit testing
    /// This program test the functionality for a ITimeConverterRepository 
    /// using the Moq object 
    /// </summary>
    [TestClass]
    public class TimeConverterRepositoryTest
    {
        // Define elements to be used with Moq object 
        private Mock<ITimeConverterRepository> _repositoryMock;
        private ITimeConverterRepository _repositoryMockObject;

        // Private references used accross the testing methods
        private double _refSeconds;
        private DateTime _refDateTime;

        [TestInitialize]
        public void Initialize()
        {
            // Predetermined values that the Mock object will return
            _refSeconds = 44614.235;
            _refDateTime = DateTime.Today.AddSeconds(_refSeconds);  // use current date staring from midnight today andd add seconds 

            // Define Mock repository 
            _repositoryMock = new Mock<ITimeConverterRepository>();

            // set up the mock repository method calls and the expexted results
            _repositoryMock.Setup(r => r.ConvertDate12HrTimeToSeconds(It.IsAny<DateTime>()))
                .Returns(_refSeconds);
            _repositoryMock.Setup(r => r.ConvertSecondsToCurrentDateTime(It.IsAny<double>()))
                .Returns(_refDateTime);
            _repositoryMock.Setup(r => r.ConvertString24HrTimeToSeconds(It.IsAny<string>()))
                .Returns(_refSeconds);

            // Convert Mock repository into a Mock object ( to be use with service)
            _repositoryMockObject = _repositoryMock.Object;
          
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
            DateTime targetDateTime = DateTime.Today.AddSeconds(_refSeconds);

            // Act
            double resultSecs = _repositoryMockObject.ConvertDate12HrTimeToSeconds(targetDateTime);

            // Assert
            Assert.AreEqual(_refSeconds, resultSecs);
        }

        [TestMethod]
        public void ConvertSecondsToCurrentDateTimeTest()
        {
            // Arrange
            // All set at initialization

            // Act
            DateTime resultDateTime = _repositoryMockObject.ConvertSecondsToCurrentDateTime(_refSeconds);

            // Assert 
            Assert.AreEqual(_refDateTime, resultDateTime);
        }

        [TestMethod]
        public void ConvertString24HrTimeToSeconds()
        {
            // Arrange
            string target24Time = "12:23:34.235";

            // Act
            double resultSecs = _repositoryMockObject.ConvertString24HrTimeToSeconds(target24Time);

            // Assert
            Assert.AreEqual(_refSeconds, resultSecs);
        }

    }
}
