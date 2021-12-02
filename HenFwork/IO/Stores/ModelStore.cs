// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenBstractions.Graphics;
using System.Collections.Generic;

namespace HenFwork.IO.Stores
{
    public class ModelStore : ResourceStore<Model>
    {
        private readonly Dictionary<string, Model> models = new();

        protected override Model GetInternal(string assetName) => models[assetName];

        protected override void LoadInternal(string assetName) => models.Add(assetName, new Model(assetName));

        protected override void UnloadInternal(string assetName) => models.Remove(assetName);
    }
}