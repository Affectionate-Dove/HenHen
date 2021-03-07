// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Graphics2d;

namespace HenHen.Framework.Extensions
{
    public static class RaylibColorExtensions
    {
        public static Raylib_cs.Color ToRaylibColor(this ColorInfo c) => new(c.r, c.g, c.b, c.a);
    }
}