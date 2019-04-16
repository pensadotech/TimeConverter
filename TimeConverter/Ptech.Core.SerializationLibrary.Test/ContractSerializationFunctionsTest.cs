using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ptech.Core.SerializationLibrary;
using Ptech.Core.SerializationLibrary.Test.Dto;

namespace Ptech.Core.SerializationLibrary.Test
{
    [TestClass]
    public class ContractSerializationFunctionsTest
    {
        // Private memebers
        private UserConfiguration usrConfig;

        [TestInitialize]
        public void InitializeTest()
        {
            // Set example userg configuration
            usrConfig = new UserConfiguration();
            usrConfig.ConfigName = @"UsrConfig.Contract.xml";
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
        public void SaveAndLoadObjectByContract()
        {
            // Arrange
            string cfgFilenameXml = usrConfig.ConfigName;

            // Act: Write file 
            DataContractSerializationFunctions.SaveObjectByContract<UserConfiguration>(cfgFilenameXml, usrConfig);

            // Assert: 
            // Does file exists?
            bool doesfilExit = File.Exists(cfgFilenameXml);
            // Validate that file exists
            Assert.IsTrue(doesfilExit);

            // Act: Read File
            var usrConfig2 = (UserConfiguration)DataContractSerializationFunctions.LoadObjectByContract<UserConfiguration>(cfgFilenameXml);

            // Assert:
            // Validate that returning oopbjet is not null
            Assert.IsNotNull(usrConfig2);
            // Validate that the configuration was loaded properly
            Assert.AreEqual(usrConfig.ConfigName, usrConfig2.ConfigName);
            Assert.AreEqual(3, usrConfig2.UserConfigVariables.Count);

        }
    }
}
