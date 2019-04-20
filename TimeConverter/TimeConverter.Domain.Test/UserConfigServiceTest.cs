using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TimeConverter.Domain.Dto;
using TimeConverter.Domain.Interfaces.Repositories;
using TimeConverter.Domain.Interfaces.Services;

namespace TimeConverter.Domain.Test
{
    [TestClass]
    public class UserConfigServiceTest
    {
        // Define elements to be used with Moq object 
        private Mock<IUserConfigService> _serviceMock;
        private IUserConfigService _serviceMockObject;

        // Private references used accross the testing methods
        private UserConfiguration _usrConfiguration;

        [TestInitialize]
        public void Initialize()
        {
            // Data to use as retun values for Moq object
            _usrConfiguration = new UserConfiguration();
            _usrConfiguration.SetConfigItem("Color", "Orange");
            _usrConfiguration.SetConfigItem("Theme", "black");

            // Define Mock repository 
            _serviceMock = new Mock<IUserConfigService>();

            // set up the mock repository method calls and the expected results
            _serviceMock.Setup(r => r.LoadUserConfiguration())
                .Returns(_usrConfiguration);
            _serviceMock.Setup(r => r.SaveUserConfiguration())
                .Returns(true);
            _serviceMock.Setup(r => r.GetConfigItemValue(It.IsAny<string>()))
                .Returns("Orange");
            _serviceMock.Setup(r => r.SetConfigItem(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);

            // Convert Mock repository into a Mock object 
            _serviceMockObject = _serviceMock.Object;
        }

        [TestCleanup]
        public void CleanUp()
        {
            // nothing
        }

        [TestMethod]
        public void SaveUserConfigurationTest()
        {
            // Arrange
            // All set at initialization

            // Act
            bool success = _serviceMockObject.SaveUserConfiguration();

            // Assert
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void LoadUserConfigurationTest()
        {
            // Arrange
            // All set at initialization

            // Act
            UserConfiguration userConfig = _serviceMockObject.LoadUserConfiguration();

            // Assert
            Assert.AreEqual(_usrConfiguration, userConfig);
        }

        [TestMethod]
        public void GetConfigItemValueTest()
        {
            // Arrange
            string configKey = "Color";

            // Act
            string configValue = _serviceMockObject.GetConfigItemValue(configKey);

            // Assert
            Assert.AreEqual("Orange", configValue);

        }

        [TestMethod]
        public void SetConfigItem()
        {
            // Arrange
            string configKey = "Color";
            string configValue = "Orange";

            // Act 
            bool succes = _serviceMockObject.SetConfigItem(configKey, configValue);

            // Assert
            Assert.IsTrue(succes);

        }


    }
}
