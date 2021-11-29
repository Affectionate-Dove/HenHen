// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System.Numerics;

namespace HenHen.Framework.Graphics2d
{
    /// <summary>
    ///     Draws a provided texture.
    /// </summary>
    public class Sprite : Drawable
    {
        private Raylib_cs.Texture2D texture;
        private bool autoFillModeProportions;

        public Raylib_cs.Texture2D Texture
        {
            get => texture;
            set
            {
                texture = value;
                if (AutoFillModeProportions)
                    SetFillProportionsToTextureProportions();
            }
        }

        public ColorInfo Color { get; set; } = new ColorInfo(255, 255, 255);

        /// <summary>
        ///     Whether to automatically set
        ///     <see cref="Drawable.FillModeProportions"/>
        ///     to <see cref="Texture"/>'s proportions
        /// </summary>
        /// <remarks>
        ///     If <see langword="true"/>, sets
        ///     <see cref="Drawable.FillModeProportions"/> when
        ///     this property or <see cref="Texture"/> changes.
        /// </remarks>
        public bool AutoFillModeProportions
        {
            get => autoFillModeProportions;
            set
            {
                autoFillModeProportions = value;
                if (autoFillModeProportions)
                    SetFillProportionsToTextureProportions();
            }
        }

        protected override void OnRender()
        {
            base.OnRender();
            var rect = LayoutInfo.RenderRect;
            var sourceRec = new Raylib_cs.Rectangle(0, 0, Texture.width, Texture.height);
            var destRec = new Raylib_cs.Rectangle(rect.Left, rect.Top, rect.Width, rect.Height);
            Raylib_cs.Raylib.DrawTexturePro(Texture, sourceRec, destRec, Vector2.Zero, 0, Color);
        }

        /// <summary>
        ///     Sets <see cref="Drawable.FillModeProportions"/>
        ///     to proportions of <see cref="Texture"/>.
        /// </summary>
        private void SetFillProportionsToTextureProportions()
        {
            if (Texture.height is not 0)
                FillModeProportions = Texture.width / (float)Texture.height;
        }
    }
}