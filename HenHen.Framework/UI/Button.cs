// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Graphics2d;
using HenHen.Framework.Input;
using HenHen.Framework.Input.UI;
using System;
using System.Numerics;

namespace HenHen.Framework.UI
{
    public class Button<TInputAction> : Container, IHasColor, IInterfaceComponent<TInputAction>, IPositionalInterfaceComponent
    {
        private readonly Rectangle background;
        private readonly SpriteText spriteText;

        public event Action<IInterfaceComponent<TInputAction>> FocusRequested;

        public ColorInfo Color { get => background.Color; set => background.Color = value; }
        public string Text { get => spriteText.Text; set => spriteText.Text = value; }
        public Action Action { get; set; }

        public virtual bool AcceptsFocus => Action is not null;

        public virtual bool AcceptsPositionalInput => Action is not null;

        public Button()
        {
            AddChild(background = new Rectangle
            {
                RelativeSizeAxes = Axes.Both
            });
            AddChild(spriteText = new SpriteText
            {
                Anchor = new Vector2(0.5f),
                Origin = new Vector2(0.5f),
                RelativeSizeAxes = Axes.Both,
                AlignMiddle = true
            });
        }

        public virtual void OnFocus()
        {
        }

        public virtual void OnFocusLost()
        {
        }

        public virtual bool OnActionPressed(TInputAction action) => false;

        public virtual void OnActionReleased(TInputAction action)
        {
        }

        public virtual bool AcceptsPositionalButton(MouseButton button) => button == MouseButton.Left;

        public virtual void OnHover()
        {
        }

        public virtual void OnHoverLost()
        {
        }

        public virtual void OnMousePress(MouseButton button)
        {
        }

        public virtual void OnMouseRelease(MouseButton button)
        {
        }

        public virtual void OnClick(MouseButton button)
        {
            FocusRequested?.Invoke(this);
            Action();
        }

        protected void RequestFocus() => FocusRequested?.Invoke(this);
    }
}