using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain = TimeConverter.Domain;

namespace TimeConverter.DataAccess.Test
{
    /// <summary>
    /// Data Access layer Unit Testing. 
    /// This will test will exercise the capability to convert a configuraiton object
    /// int an XML file using a ConfigurationRepository that implements 
    /// the Domain IUserConfigRepository. This repository has only to funtions: Load and Save
    /// </summary>
    [TestClass]
    public class UserConfigRepositoryTest
    {
        private Domain.Dto.UserConfiguration _userConfig; 

        [TestInitialize]
        public void Initialize()
        {
            // Create a new Domain configuration object for the test 
            _userConfig = new Domain.Dto.UserConfiguration
            {
                ConfigFilename = @"UsrConfig.Repo.xml",
                SavedDateTime = DateTime.Now
            };

            // Add two variables to the config object
            _userConfig.SetConfigItem("Color", "Orange");
            _userConfig.SetConfigItem("Theme", "Dark");

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
            // Create a new data-access repository, which implements the Domain IUserConfigRepository
            Domain.Interfaces.Repositories.IUserConfigRepository userConfigRepo = new ConfigurationRepository();

            // Act
            userConfigRepo.SaveUserConfiguration(_userConfig.ConfigFilename, _userConfig);

            // Assert
            // Verify that file was created
            bool doesfilExit = File.Exists(_userConfig.ConfigFilename);
            // Validate that file exists
            Assert.IsTrue(doesfilExit);
            
        }

        [TestMethod]
        public void LoadUserConfigurationTest()
        {
            // Arrange
            // Create a new data-access repository, which implements the Domain IUserConfigRepository
            Domain.Interfaces.Repositories.IUserConfigRepository userConfigRepo = new ConfigurationRepository();

            // Act
            userConfigRepo.SaveUserConfiguration(_userConfig.ConfigFilename, _userConfig);

            // Assert
            // Verify that file was created
            bool doesfilExit = File.Exists(_userConfig.ConfigFilename);
            // Validate that file exists
            Assert.IsTrue(doesfilExit);

            // Act: Load file
            Domain.Dto.UserConfiguration userConfi2 = userConfigRepo.LoadUserConfiguration(_userConfig.ConfigFilename);

            // Assert:
            // Validate that returning oopbjet is not null
            Assert.IsNotNull(userConfi2);
            // Validate that the configuration was loaded properly
            Assert.AreEqual(_userConfig.ConfigFilename, userConfi2.ConfigFilename);
            Assert.AreEqual(2, _userConfig.ConfigItems.Count);
            
        }


    }
}
