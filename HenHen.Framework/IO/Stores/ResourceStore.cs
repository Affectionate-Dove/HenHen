using System.Collections.Generic;

namespace HenHen.Framework.IO.Stores
{
    public abstract class ResourceStore<T>
    {
        private readonly List<string> loadedAssetsNames = new();

        public abstract void Load(string assetName);

        public void Unload(string assetName)
        {
            if (IsLoaded(assetName))
                UnloadInternal(assetName);
            else
                throw new System.Exception();
        }

        public IEnumerable<string> LoadedAssetsNames() => loadedAssetsNames;

        public bool IsLoaded(string assetName) => loadedAssetsNames.Contains(assetName);

        public T Get(string assetName)
        {
            if (IsLoaded(assetName))
                return GetInternal(assetName);
            else
                throw new System.Exception();
        }

        protected abstract void UnloadInternal(string assetName);

        protected abstract T GetInternal(string assetName);
    }
}