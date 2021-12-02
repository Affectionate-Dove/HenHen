// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenBstractions.Graphics;
using HenFwork.Graphics2d;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace HenFwork.UI
{
    public class SpriteText : Drawable, IHasColor
    {
        private string text;
        private List<string> displayedText;

        public static Font DefaultFont { get; set; } = Font.DefaultFont;

        public string Text
        {
            get => text;
            set
            {
                if (text == value)
                    return;

                text = value;
                LayoutValid = false;
            }
        }

        public Font Font { get; set; } = DefaultFont;

        public ColorInfo Color { get; set; } = new ColorInfo(255, 255, 255);

        public float FontSize { get; set; } = 16;

        public float LetterSpacing { get; set; } = 0.1f;

        public float LineSpacing { get; set; } = 0;

        public Axes AutoSizeAxes { get; set; } = Axes.Both;

        public bool AutoSizingExceedsAvailableSpace { get; set; }

        /// <remarks>
        ///     A value of (0, 0) represents alignment to the top left,
        ///     a value of (0.5f, 0) represents alignment to the top center,
        ///     and a value of (1, 1) represents alignment to the bottom right.
        /// </remarks>
        public Vector2 TextAlignment { get; set; }

        protected override void OnRender()
        {
            base.OnRender();
            if (displayedText is null)
                return;

            var r = LayoutInfo.RenderRect;
            var fullTextSize = MeasureTextSize(displayedText);
            var fullTextTop = r.Top + (r.Height * TextAlignment.Y) - (fullTextSize.Y * TextAlignment.Y);

            for (var i = 0; i < displayedText.Count; i++)
            {
                var line = displayedText[i];
                var lineSize = MeasureTextSize(line);

                var left = r.Left + (r.Width * TextAlignment.X) - (lineSize.X * TextAlignment.X);
                var top = fullTextTop + (i * FontSize) + ((i - 1) * LineSpacing);

                Drawing.DrawText(Font, displayedText[i], new Vector2(left, top), FontSize, LetterSpacing, Color);
            }
        }

        protected override Vector2 ComputeRenderSize()
        {
            if (Text is null)
                return base.ComputeRenderSize();

            float width, height;
            {
                var baseRenderSize = base.ComputeRenderSize();
                width = baseRenderSize.X;
                height = baseRenderSize.Y;
            }

            displayedText = Text.Split('\n').ToList();
            var textSize = MeasureTextSize(displayedText);

            if (AutoSizeAxes.HasFlag(Axes.X))
            {
                var maxWidth = textSize.X;
                if (!(AutoSizingExceedsAvailableSpace || Parent.ContainerLayoutInfo.ExpandableSizeAxes.HasFlag(Axes.X)))
                    maxWidth = Parent.ContainerLayoutInfo.ChildrenRenderArea.Width;

                displayedText = GetWrappedTextLinesFromLines(displayedText, maxWidth);
                textSize = MeasureTextSize(displayedText);
                width = MathF.Min(MathF.Max(width, textSize.X), maxWidth);
            }
            if (AutoSizeAxes.HasFlag(Axes.Y))
            {
                var maxHeight = textSize.Y;
                if (!(AutoSizingExceedsAvailableSpace || Parent.ContainerLayoutInfo.ExpandableSizeAxes.HasFlag(Axes.Y)))
                    maxHeight = Parent.ContainerLayoutInfo.ChildrenRenderArea.Height;

                height = MathF.Min(MathF.Max(height, textSize.Y), maxHeight);
            }

            return new(width, height);
        }

        private List<string> GetWrappedTextLinesFromLines(in List<string> lines, float maxWidth)
        {
            var _lines = new List<string>();
            foreach (var line in lines)
                _lines.AddRange(GetWrappedTextLinesFromText(line, maxWidth));
            return _lines;
        }

        private List<string> GetWrappedTextLinesFromText(in string t, float maxWidth)
        {
            var remainingWords = new Queue<string>(t.Split(' '));
            List<string> lines = new();

            var currentLine = remainingWords.Dequeue();
            while (remainingWords.Count > 0)
            {
                var nextWord = remainingWords.Dequeue();
                var currentLineWithNextWord = currentLine + " " + nextWord;
                if (MeasureTextWidth(currentLineWithNextWord) > maxWidth)
                {
                    lines.Add(currentLine);
                    currentLine = nextWord;
                }
                else
                    currentLine = currentLineWithNextWord;
            }
            lines.Add(currentLine);

            return lines;
        }

        private float MeasureTextWidth(string text) => Drawing.MeasureText(Font, text, FontSize, LetterSpacing).X + 1;

        private Vector2 MeasureTextSize(List<string> lines)
        {
            if (lines.Count == 0)
                return Vector2.Zero;

            var width = lines.Max(line => MeasureTextWidth(line));
            var height = (lines.Count * FontSize) + ((lines.Count - 1) * LineSpacing);
            return new(width, height);
        }

        private Vector2 MeasureTextSize(string line) => new(MeasureTextWidth(line), FontSize);
    }
}