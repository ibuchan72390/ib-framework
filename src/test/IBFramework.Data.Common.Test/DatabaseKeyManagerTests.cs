﻿using IBFramework.Core.Data;
using IBFramework.Core.IoC;
using IBFramework.Data.Common.IoC;
using IBFramework.IoC;
using IBFramework.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace IBFramework.Data.Common.Test
{
    public class DatabaseKeyManagerTests : TestBase, IDisposable
    {
        private readonly IContainer _testContainer;

        private readonly Dictionary<string, string> _sampleKeyDictionary;

        public DatabaseKeyManagerTests()
        {
            // Create a new test container each time so we can resolve a fresh singleton instance for each

            var localContainerGen = ServiceLocator.Instance.Resolve<IContainerGenerator>();
            localContainerGen.InstallCommonData();
            _testContainer = localContainerGen.GenerateContainer();

            _sampleKeyDictionary = Enumerable.Range(1, 4)
                .Select(x => $"ConnectionString{x}")
                .ToDictionary(x => $"Key{x}");
        }

        public void Dispose()
        {
            _testContainer.Dispose();
        }

        [Fact]
        public void Initializing_DatabaseKeyManager_Twice_Throws_Exception()
        {
            var keyManager = _testContainer.Resolve<IDatabaseKeyManager>();

            keyManager.Init(new Dictionary<string, string>());

            var e = Assert.Throws<Exception>(() => keyManager.Init(new Dictionary<string, string>()));

            Assert.Equal("DatabaseKeyManager already initialized!", e.Message);
        }

        [Fact]
        public void GetConnectionString_Throws_Error_When_Not_Initialized()
        {
            var keyManager = _testContainer.Resolve<IDatabaseKeyManager>();

            var e = Assert.Throws<Exception>(() => keyManager.GetConnectionString(""));

            Assert.Equal("Database Key Dictionary has not been loaded!  Make sure you've initialize the DatabaseKeyManager!",
                e.Message);
        }

        [Fact]
        public void GetConnectionString_Throws_Error_When_Dictionary_Doesnt_Contain_Key()
        {
            const string databaseKey = "test";

            var keyManager = _testContainer.Resolve<IDatabaseKeyManager>();

            keyManager.Init(new Dictionary<string, string>());

            var e = Assert.Throws<Exception>(() => keyManager.GetConnectionString(databaseKey));

            var expectedErr = $"No database connection string found for key - '{databaseKey}'";
            Assert.Equal(expectedErr, e.Message);
        }

        [Fact]
        public void GetConnectionString_Returns_Connection_String_When_Found_From_Init_Collection()
        {
            var keyManager = _testContainer.Resolve<IDatabaseKeyManager>();

            keyManager.Init(_sampleKeyDictionary);

            foreach(var key in _sampleKeyDictionary.Keys)
            {
                Assert.Equal(_sampleKeyDictionary[key], keyManager.GetConnectionString(key));
            }
        }

        [Fact]
        public void DatabaseKeyManager_Maintains_Initialization_Through_Mutliple_Resolutions()
        {
            var keyManager = _testContainer.Resolve<IDatabaseKeyManager>();

            keyManager.Init(_sampleKeyDictionary);

            foreach (var key in _sampleKeyDictionary.Keys)
            {
                Assert.Equal(_sampleKeyDictionary[key], keyManager.GetConnectionString(key));
            }
        }

        private void AssertKeyManagerContainsSampleDict(IDatabaseKeyManager keyManager)
        {
            foreach (var key in _sampleKeyDictionary.Keys)
            {
                Assert.Equal(_sampleKeyDictionary[key], keyManager.GetConnectionString(key));
            }
        }
    }
}
