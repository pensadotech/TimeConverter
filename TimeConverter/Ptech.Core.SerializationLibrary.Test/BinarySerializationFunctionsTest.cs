﻿using System;
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
            // Set example user configuration using local DTOs
            usrConfig = new UserConfiguration();
            usrConfig.ConfigName = "TestConfig01";
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
        public void SaveAndLoadBinObjectsTest()
        {
            // Arrange
            string cfgFilename = @"UsrConfig.bin";

            // Act : Writeh file
            BinarySerializationFunctions.SaveBinObject(cfgFilename, usrConfig);

            // Assert
            bool doesfilExit = File.Exists(cfgFilename);
            // Validate that file exists
            Assert.IsTrue(doesfilExit);

            // Act : Read file
            var usrConfig2 = (UserConfiguration)BinarySerializationFunctions.LoadBinObject(cfgFilename);

            // Assert
            // Validate that returning oopbjet is not null
            Assert.IsNotNull(usrConfig2);

            // Validate that the configuration was loaded properly
            Assert.AreEqual("TestConfig01", usrConfig2.ConfigName);
            Assert.AreEqual(3, usrConfig2.UserConfigVariables.Count);
        }
    }
}
