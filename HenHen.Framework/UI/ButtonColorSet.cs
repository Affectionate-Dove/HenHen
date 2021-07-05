// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Graphics2d;
using System;
using System.Collections.Generic;

namespace HenHen.Framework.UI
{
    public struct ButtonColorSet
    {
        public ColorInfo? fill;
        public ColorInfo? border;
        public ColorInfo? text;

        public ButtonColorSet(ColorInfo? fill, ColorInfo? border, ColorInfo? text)
        {
            this.fill = fill;
            this.border = border;
            this.text = text;
        }

        public static implicit operator (ColorInfo? fill, ColorInfo? border, ColorInfo? text)(ButtonColorSet value) => (value.fill, value.border, value.text);

        public static implicit operator ButtonColorSet((ColorInfo? fill, ColorInfo? border, ColorInfo? text) value) => new(value.fill, value.border, value.text);

        public override bool Equals(object obj) => obj is ButtonColorSet other && EqualityComparer<ColorInfo?>.Default.Equals(fill, other.fill) && EqualityComparer<ColorInfo?>.Default.Equals(border, other.border) && EqualityComparer<ColorInfo?>.Default.Equals(text, other.text);

        public override int GetHashCode() => HashCode.Combine(fill, border, text);

        public void Deconstruct(out ColorInfo? fill, out ColorInfo? border, out ColorInfo? text)
        {
            fill = this.fill;
            border = this.border;
            text = this.text;
        }

        public static bool operator ==(ButtonColorSet left, ButtonColorSet right) => left.Equals(right);

        public static bool operator !=(ButtonColorSet left, ButtonColorSet right) => !(left == right);
    }
}