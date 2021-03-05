using NUnit.Framework;

namespace HenHen.Framework.Tests.IO.Stores
{
    public class ResourceStoreTests
    {
        private readonly TestStore testStore = new();

        [Test]
        public void LoadTest()
        {
            string assetName = "some asset name";
            testStore.Load(assetName);
            Assert.IsTrue(testStore.IsLoaded(assetName));
        }

        [Test]
        public void UnloadTest()
        {
            string assetName = "unloading test";
            testStore.Load(assetName);
            Assert.IsTrue(testStore.IsLoaded(assetName));
            testStore.Unload(assetName);
            Assert.IsFalse(testStore.IsLoaded(assetName));
        }

        [Test]
        public void GetTest()
        {
            string assetName = "get test";
            testStore.Load(assetName);
            Assert.AreEqual(assetName, testStore.Get(assetName));
            testStore.Unload(assetName);
            Assert.Throws<System.Exception>(() => testStore.Get(assetName));
        }
    }
}