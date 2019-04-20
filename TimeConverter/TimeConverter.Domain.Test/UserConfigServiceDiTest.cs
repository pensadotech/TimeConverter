using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TimeConverter.Domain.Dto;
using TimeConverter.Domain.Interfaces.Repositories;
using TimeConverter.Domain.Interfaces.Services;
using TimeConverter.Domain.Services;
using Unity;
using Unity.Injection;

namespace TimeConverter.Domain.Test
{
    [TestClass]
    public class UserConfigServiceDiTest
    {
        // Define elements to be used with Moq object 
        private Mock<IUserConfigRepository> _repositoryMock;
        private IUserConfigRepository _repositoryMockObject;

        // Service object
        private IUserConfigService _userConfigService; 

        // Private references used accross the testing methods
        private UserConfiguration _usrConfiguration;

        // DI container
        private IUnityContainer _container;

        [TestInitialize]
        public void Initialize()
        {
            // Data to use as retun values for Moq object
            _usrConfiguration = new UserConfiguration();
            _usrConfiguration.SetConfigItem("color", "Orange");
            _usrConfiguration.SetConfigItem("theme", "black");

            // Define Mock repository 
            _repositoryMock = new Mock<IUserConfigRepository>();

            // set up the mock repository method calls and the expected results
            _repositoryMock.Setup(r => r.LoadUserConfiguration(It.IsAny<string>()))
                .Returns(_usrConfiguration);
            _repositoryMock.Setup(r => r.SaveUserConfiguration(It.IsAny<string>(), It.IsAny<UserConfiguration>()))
                .Returns(true);

            // Convert Mock repository into a Mock object ( to be use with service)
            _repositoryMockObject = _repositoryMock.Object;

            // Define container and register ITimeConverterRepository
            _container = new UnityContainer();

            // Register in container the rpository and the service. For the service
            // help teh container understand that a ITimeConverterRepository will be passed
            // as parameter for the constructor
            _container.RegisterInstance<IUserConfigRepository>(_repositoryMockObject);
            _container.RegisterType<IUserConfigService, UserConfigService>(
                new InjectionConstructor(It.IsAny<string>(),_repositoryMockObject));

            // Instantiate a ITimeConverterService 
            _userConfigService = _container.Resolve<IUserConfigService>();
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
            bool success = _userConfigService.SaveUserConfiguration();

            // Assert
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void LoadUserConfigurationTest()
        {
            // Arrange
            // All set at initialization

            // Act
            bool sucess = _userConfigService.LoadUserConfiguration();

            // Assert
            Assert.IsTrue(sucess);
        }

        [TestMethod]
        public void GetAndSetConfigItemValueTest()
        {    
            // NOTE: The Moq repository object does no have the functinality
            //       to set or get a config item. However, the DI container
            //       can resolve the functionality. Therefore, in this test
            //       method, both functions are tested, adding first the item
            //       and then getting it. 

            // Arrange
            string configKey = "Color";
            string configValue = "Orange";

            // Act
            bool success = _userConfigService.SetConfigItem(configKey, configValue);
            // Assert
            Assert.IsTrue(success);

            // Act
            string configValue2 = _userConfigService.GetConfigItemValue(configKey);
            // Assert
            Assert.AreEqual(configValue, configValue2);

        }
       

    }
}
