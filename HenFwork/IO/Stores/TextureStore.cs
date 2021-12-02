// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenBstractions.Graphics;
using System.Collections.Generic;

namespace HenFwork.IO.Stores
{
    public class TextureStore : ResourceStore<Texture>
    {
        private readonly Dictionary<string, Texture> textures = new();

        protected override Texture GetInternal(string assetName) => textures[assetName];

        protected override void LoadInternal(string assetName) => textures.Add(assetName, new ImageTexture(assetName));

        protected override void UnloadInternal(string assetName) => textures.Remove(assetName);
    }
}