// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using NUnit.Framework;

namespace HenHen.Framework.Tests.IO.Stores
{
    public class ResourceStoreTests
    {
        private readonly TestStore testStore = new();

        [Test]
        public void LoadTest()
        {
            var assetName = "some asset name";
            testStore.Load(assetName);
            Assert.IsTrue(testStore.IsLoaded(assetName));
        }

        [Test]
        public void UnloadTest()
        {
            var assetName = "unloading test";
            testStore.Load(assetName);
            Assert.IsTrue(testStore.IsLoaded(assetName));
            testStore.Unload(assetName);
            Assert.IsFalse(testStore.IsLoaded(assetName));
        }

        [Test]
        public void GetTest()
        {
            var assetName = "get test";
            testStore.Load(assetName);
            Assert.AreEqual(assetName, testStore.Get(assetName));
            testStore.Unload(assetName);
            Assert.Throws<System.Exception>(() => testStore.Get(assetName));
        }
    }
}