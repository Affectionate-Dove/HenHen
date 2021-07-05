// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Graphics2d;
using HenHen.Framework.Input;
using HenHen.Framework.Input.UI;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace HenHen.Framework.UI
{
    public class Button<TInputAction> : Container, IHasColor, IInterfaceComponent<TInputAction>, IPositionalInterfaceComponent
    {
        private readonly Rectangle background;
        private readonly SpriteText spriteText;
        private ButtonState state;
        private Action action;
        private ButtonColorSet disabledColors = new(new(50, 50, 50), null, Raylib_cs.Color.GRAY);
        private ButtonColorSet enabledColors = new(new(80, 80, 80), null, Raylib_cs.Color.WHITE);
        private ButtonColorSet hoveredColors = new(new(110, 110, 110), null, Raylib_cs.Color.WHITE);
        private ButtonColorSet focusedColors = new(null, new(200, 200, 200), Raylib_cs.Color.WHITE);
        private ButtonColorSet pressedColors = new(new(60, 60, 60), null, Raylib_cs.Color.WHITE);

        public event Action<IInterfaceComponent<TInputAction>> FocusRequested;

        public HashSet<TInputAction> AcceptedActions { get; } = new();

        public ColorInfo Color
        {
            get => background.Color;
            protected set => background.Color = value;
        }

        public ColorInfo BorderColor
        {
            get => background.BorderColor;
            protected set => background.BorderColor = value;
        }

        public string Text { get => spriteText.Text; set => spriteText.Text = value; }

        public Action Action
        {
            get => action;
            set
            {
                action = value;
                if (value is not null)
                    State |= ButtonState.Enabled;
                else
                    State ^= ButtonState.Enabled;
            }
        }

        public int BorderThickness
        {
            get => background.BorderThickness;
            set => background.BorderThickness = value;
        }

        public ButtonColorSet DisabledColors
        {
            get => disabledColors;
            set
            {
                disabledColors = value;
                OnStateChanged();
            }
        }

        public ButtonColorSet EnabledColors
        {
            get => enabledColors;
            set
            {
                enabledColors = value;
                OnStateChanged();
            }
        }

        public ButtonColorSet HoveredColors
        {
            get => hoveredColors;
            set
            {
                hoveredColors = value;
                OnStateChanged();
            }
        }

        public ButtonColorSet FocusedColors
        {
            get => focusedColors;
            set
            {
                focusedColors = value;
                OnStateChanged();
            }
        }

        public ButtonColorSet PressedColors
        {
            get => pressedColors;
            set
            {
                pressedColors = value;
                OnStateChanged();
            }
        }

        public virtual bool AcceptsFocus => Action is not null;

        public virtual bool AcceptsPositionalInput => Action is not null;

        protected ButtonState State
        {
            get => state;
            set
            {
                state = value;
                OnStateChanged();
            }
        }

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
            BorderThickness = 2;
            OnStateChanged();
        }

        public virtual bool AcceptsPositionalButton(MouseButton button) => button == MouseButton.Left;

        public virtual void OnFocus() => State |= ButtonState.Focused;

        public virtual void OnFocusLost() => State &= ~(ButtonState.Focused | ButtonState.Pressed);

        public virtual bool OnActionPressed(TInputAction action)
        {
            if (!AcceptedActions.Contains(action))
                return false;
            State |= ButtonState.Pressed;
            return true;
        }

        public virtual void OnActionReleased(TInputAction action)
        {
            State &= ~ButtonState.Pressed;
            Action?.Invoke();
        }

        public virtual void OnHover() => State |= ButtonState.Hovered;

        public virtual void OnHoverLost() => State &= ~ButtonState.Hovered;

        public virtual void OnMousePress(MouseButton button)
        {
            FocusRequested?.Invoke(this);
            State |= ButtonState.Pressed;
        }

        public virtual void OnMouseRelease(MouseButton button) => State &= ~ButtonState.Pressed;

        public virtual void OnClick(MouseButton button)
        {
            FocusRequested?.Invoke(this);
            Action?.Invoke();
        }

        protected virtual void OnStateChanged()
        {
            var resultingColorSet = new ButtonColorSet();

            if (state.HasFlag(ButtonState.Pressed))
                TrySetColors(PressedColors, ref resultingColorSet);
            if (state.HasFlag(ButtonState.Focused))
                TrySetColors(FocusedColors, ref resultingColorSet);
            if (state.HasFlag(ButtonState.Hovered))
                TrySetColors(HoveredColors, ref resultingColorSet);
            if (state.HasFlag(ButtonState.Enabled))
                TrySetColors(EnabledColors, ref resultingColorSet);

            TrySetColors(DisabledColors, ref resultingColorSet);

            Color = resultingColorSet.fill.GetValueOrDefault();
            BorderColor = resultingColorSet.border.GetValueOrDefault();
            spriteText.Color = resultingColorSet.text.GetValueOrDefault();
        }

        protected void RequestFocus() => FocusRequested?.Invoke(this);

        private static void TrySetColors(ButtonColorSet colorSet, ref ButtonColorSet destination)
        {
            destination.fill ??= colorSet.fill;
            destination.border ??= colorSet.border;
            destination.text ??= colorSet.text;
        }

        [Flags]
        protected enum ButtonState
        {
            Disabled = 0,
            Enabled = 1 << 0,
            Hovered = 1 << 1,
            Focused = 1 << 2,
            Pressed = 1 << 3
        }
    }
}