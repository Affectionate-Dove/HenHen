using Raylib_cs;
using System.Collections.Generic;

namespace HenHen.Framework.IO.Stores
{
    public class ModelStore : ResourceStore<Model>
    {
        private readonly Dictionary<string, Model> models = new();

        protected override Model GetInternal(string assetName) => models[assetName];

        protected override void LoadInternal(string assetName) => models.Add(assetName, Raylib.LoadModel(assetName));

        protected override void UnloadInternal(string assetName) => Raylib.UnloadModel(GetInternal(assetName));
    }
}