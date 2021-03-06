﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TimeConverter.Domain.Interfaces.Repositories;
using TimeConverter.Domain.Interfaces.Services;
using TimeConverter.Domain.Services;
using Unity;
using Unity.Injection;

namespace TimeConverter.Domain.Test
{
    /// <summary>
    /// Domain unit test
    /// This program test the Time converter Service using a Moq object and Unity DI container
    /// Also uses ITimeConverterRepository and implements ITimeConverterService.  
    /// </summary>
    [TestClass]
    public class TimeConverterServiceDiTest
    {
        // Define elements to be used with Moq object 
        private Mock<ITimeConverterRepository> _repositoryMock;
        private ITimeConverterRepository _repositoryMockObject;

        // define service base on interface
        private ITimeConverterService _timeConvService;
        
        // DI container
        private IUnityContainer _container;

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

            // set up the mock repository method calls and the fixed results
            _repositoryMock.Setup(r => r.ConvertDateTimeObjToSeconds(It.IsAny<DateTime>()))
                .Returns(_refSeconds);
            _repositoryMock.Setup(r => r.ConvertSecondsToDateTimeObj(It.IsAny<double>()))
                .Returns(_refDateTime);
            _repositoryMock.Setup(r => r.ConvertString24HrTimeToSeconds(It.IsAny<string>()))
                .Returns(_refSeconds);

            // Convert Mock repository into a Mock object ( to be use with service)
            _repositoryMockObject = _repositoryMock.Object;

            // Define container and register ITimeConverterRepository
            _container = new UnityContainer();

            // Register in container the rpository and the service. For teh service
            // help teh container understand that a ITimeConverterRepository will be passed
            // as parameter for the constructor
            _container.RegisterInstance<ITimeConverterRepository>(_repositoryMockObject);
            _container.RegisterType<ITimeConverterService, TimeConverterService>(
                new InjectionConstructor(_repositoryMockObject));

            // Instantiate a ITimeConverterService 
            _timeConvService = _container.Resolve<ITimeConverterService>();
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
            double resultSecs = _timeConvService.ConvertDateTimeObjToSeconds(targetDateTime);

            // Assert
            Assert.AreEqual(_refSeconds, resultSecs);
        }

        [TestMethod]
        public void ConvertSecondsToCurrentDateTimeTest()
        {
            // Arrange
            // All set at initialization

            // Act
            DateTime resultDateTime = _timeConvService.ConvertSecondsToDateTimeObj(_refSeconds);

            // Assert 
            Assert.AreEqual(_refDateTime, resultDateTime);
        }

        [TestMethod]
        public void ConvertString24HrTimeToSeconds()
        {
            // Arrange
            string target24Time = "12:23:34.235";

            // Act
            double resultSecs = _timeConvService.ConvertString24HrTimeToSeconds(target24Time);

            // Assert
            Assert.AreEqual(_refSeconds, resultSecs);
        }



    }
}
