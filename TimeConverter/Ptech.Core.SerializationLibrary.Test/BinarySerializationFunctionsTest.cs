using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ptech.Core.SerializationLibrary;
using Ptech.Core.SerializationLibrary.Test.Dto;

namespace Ptech.Core.SerializationLibrary.Test
{
    [TestClass]
    public class BinarySerializationFunctionsTest
    {
        // Private memebers
        private UserConfiguration usrConfig;

        [TestInitialize]
        public void InitializeTest()
        {
            // Set example userg configuration
            usrConfig = new UserConfiguration();
            usrConfig.ConfigName = "TestConfig01";
            usrConfig.SavedDateTime = DateTime.Now.ToShortDateString();
            usrConfig.AddConfigVar("GRP1", "SGRP1", "Var1", "abc123", "abc1234");
            usrConfig.AddConfigVar("GRP1", "SGRP1", "Var2", "xwz123", "xwz1234");
            usrConfig.AddConfigVar("GRP1", "SGRP1", "Var3", "rty123", "rty1234");

        }

        [TestMethod]
        public void SaveAndLoadBinObjectsTest()
        {
            // Output file
            string cfgFilename = @"UsrConfig.bin";

            // Write file 
            BinarySerializationFunctions.SaveBinObject(cfgFilename, usrConfig);

            // Does file exists?
            bool doesfilExit = File.Exists(cfgFilename);

            // Validate that file exists
            Assert.IsTrue(doesfilExit);

            // Read File
            var usrConfig2 = (UserConfiguration)BinarySerializationFunctions.LoadBinObject(cfgFilename);

            // Validate that returning oopbjet is not null
            Assert.IsNotNull(usrConfig2);

            // Validate that the configuration was loaded properly
            Assert.AreEqual("TestConfig01", usrConfig2.ConfigName);
            Assert.AreEqual(3, usrConfig2.userConfigVariables.Count);
        }
    }
}
