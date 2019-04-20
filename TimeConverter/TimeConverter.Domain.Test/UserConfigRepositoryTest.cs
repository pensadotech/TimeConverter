using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TimeConverter.Domain.Dto;
using TimeConverter.Domain.Interfaces.Repositories;

namespace TimeConverter.Domain.Test
{
    [TestClass]
    public class UserConfigRepositoryTest
    {
        // Define elements to be used with Moq object 
        private Mock<IUserConfigRepository> _repositoryMock;
        private IUserConfigRepository _repositoryMockObject;

        // Private references used accross the testing methods
        private UserConfiguration _usrConfiguration;

        [TestInitialize]
        public void Initialize()
        {
            // Data to use as retun values for Moq object
            _usrConfiguration = new UserConfiguration();
            _usrConfiguration.SetConfigItem("color","Orange");
            _usrConfiguration.SetConfigItem("theme", "black");

            // Define Mock repository 
            _repositoryMock = new Mock<IUserConfigRepository>();

            // set up the mock repository method calls and the expected results
            _repositoryMock.Setup(r => r.LoadUserConfiguration(It.IsAny<string>()))
                .Returns(_usrConfiguration);
            _repositoryMock.Setup(r => r.SaveUserConfiguration(It.IsAny<string>(),It.IsAny<UserConfiguration>()))
                .Returns(true);

            // Convert Mock repository into a Mock object ( to be use with service)
            _repositoryMockObject = _repositoryMock.Object;
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
            string configFilename = "myConfig.xml";

            // Act
            bool success = _repositoryMockObject.SaveUserConfiguration(configFilename, _usrConfiguration);

            // Assert
            Assert.IsTrue(success);

        }

        [TestMethod]
        public void LoadUserConfigurationTest()
        {
            // Arrange
            string configFilename = "myConfig.xml";

            // Act 
            UserConfiguration usrConf2 = _repositoryMockObject.LoadUserConfiguration(configFilename);


            // Assert
            Assert.AreEqual(_usrConfiguration, usrConf2);

        }

       
    }
}
