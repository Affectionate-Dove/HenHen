// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System.Collections.Generic;

namespace HenFwork.IO.Stores
{
    public abstract class ResourceStore<T>
    {
        private readonly List<string> loadedAssetsNames = new();

        public void Load(string assetName)
        {
            if (IsLoaded(assetName))
                throw new System.Exception();

            loadedAssetsNames.Add(assetName);
            LoadInternal(assetName);
        }

        public void Unload(string assetName)
        {
            if (IsLoaded(assetName))
            {
                UnloadInternal(assetName);
                loadedAssetsNames.Remove(assetName);
            }
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

        protected abstract void LoadInternal(string assetName);

        protected abstract void UnloadInternal(string assetName);

        protected abstract T GetInternal(string assetName);
    }
}