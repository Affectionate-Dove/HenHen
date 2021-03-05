using HenHen.Framework.IO.Stores;

namespace HenHen.Framework.Tests.IO.Stores
{
    public class TestStore : ResourceStore<string>
    {
        protected override string GetInternal(string assetName) => assetName;

        protected override void LoadInternal(string assetName)
        {
        }

        protected override void UnloadInternal(string assetName)
        {
        }
    }
}