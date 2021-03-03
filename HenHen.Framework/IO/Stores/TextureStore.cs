using Raylib_cs;
using System.Collections.Generic;

namespace HenHen.Framework.IO.Stores
{
    public class TextureStore : ResourceStore<Texture2D>
    {
        private readonly Dictionary<string, Texture2D> textures = new();

        public override void Load(string assetName) => Raylib.LoadTexture(assetName);

        protected override Texture2D GetInternal(string assetName) => textures[assetName];

        protected override void UnloadInternal(string assetName) => Raylib.UnloadTexture(GetInternal(assetName));
    }
}