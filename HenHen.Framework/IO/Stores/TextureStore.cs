using Raylib_cs;
using System.Collections.Generic;

namespace HenHen.Framework.IO.Stores
{
    public class TextureStore : ResourceStore<Texture2D>
    {
        private readonly Dictionary<string, Texture2D> textures = new();

        protected override Texture2D GetInternal(string assetName) => textures[assetName];

        protected override void LoadInternal(string assetName) => textures.Add(assetName, Raylib.LoadTexture(assetName));

        protected override void UnloadInternal(string assetName) => Raylib.UnloadTexture(GetInternal(assetName));
    }
}