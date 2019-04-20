using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ptech.Core.SerializationLibrary.Test.Dto;

namespace Ptech.Core.SerializationLibrary.Test
{
    [TestClass]
    public class XmlSerializationFunctionsTest
    {
        // Private memebers
        private UserConfiguration usrConfig;

        [TestInitialize]
        public void InitializeTest()
        {
            // Set example user configuration with local DTOs
            usrConfig = new UserConfiguration();
            usrConfig.ConfigName = @"UsrConfig1.xml";
            usrConfig.SavedDateTime = DateTime.Now;
            usrConfig.AddConfigVar("GRP1", "SGRP1", "Var1", "abc123", "abc1234");
            usrConfig.AddConfigVar("GRP1", "SGRP1", "Var2", "xwz123", "xwz1234");
            usrConfig.AddConfigVar("GRP1", "SGRP1", "Var3", "rty123", "rty1234");
        }

        [TestCleanup]
        public void CleanUp()
        {
            // nothing
        }

        [TestMethod]
        public void SaveAndLoadXmlGenericObjectTest()
        {
            // Arrange
            // Output file
            string cfgFilenameXml = usrConfig.ConfigName;
            
            // Act: Save into file using XML serializaiton
            XmlSerializationFunctions.SaveXmlGenericObject<UserConfiguration>(cfgFilenameXml, usrConfig);
            
            // Assert
            // Does file exists?
            bool doesfilExit = File.Exists(cfgFilenameXml);
            // Validate that file exists
            Assert.IsTrue(doesfilExit);

            // Act: Read configurations from the file and cast object to corresponding type
            var usrConfig2 = (UserConfiguration)XmlSerializationFunctions.LoadXmlGenericObject<UserConfiguration>(cfgFilenameXml);

            // Assert
            // Validate that returning objet is not null
            Assert.IsNotNull(usrConfig2);
            // Validate that the configuration was loaded properly
            Assert.AreEqual(usrConfig.ConfigName, usrConfig2.ConfigName);
            Assert.AreEqual(3, usrConfig2.UserConfigVariables.Count);
        }

        [TestMethod]
        public void SaveAndLoadXmlObjectTest()
        {
            // Arrange
            // Output file
            string cfgFilenameXml = usrConfig.ConfigName;

            // Act: Save into file using XML serializaiton
            XmlSerializationFunctions.SaveXmlObject(cfgFilenameXml, usrConfig, 
                new Type[] { typeof(UserConfiguration),typeof(UserConfigVariable) } );

            // Assert
            // Does file exists?
            bool doesfilExit = File.Exists(cfgFilenameXml);
            // Validate that file exists
            Assert.IsTrue(doesfilExit);

            // Act: Read configurations from the file and cast object to corresponding type
            var usrConfig2 = new UserConfiguration();
            usrConfig2 = (UserConfiguration)XmlSerializationFunctions.LoadXmlObject(cfgFilenameXml, usrConfig2,
                new Type[] { typeof(UserConfiguration), typeof(UserConfigVariable) });

            // Assert
            // Validate that returning oopbjet is not null
            Assert.IsNotNull(usrConfig2);
            // Validate that the configuration was loaded properly
            Assert.AreEqual(usrConfig.ConfigName, usrConfig2.ConfigName);
            Assert.AreEqual(3, usrConfig2.UserConfigVariables.Count);
        }
    }
}
