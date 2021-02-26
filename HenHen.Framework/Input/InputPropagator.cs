using System.Collections.Generic;

namespace HenHen.Framework.Input
{
    public class InputPropagator<TInputAction>
    {
        private IInputListener<TInputAction> lastListener;

        public List<IInputListener<TInputAction>> Listeners { get; } = new();

        public void OnActionPressed(TInputAction action)
        {
            for (int i = Listeners.Count - 1; i >= 0; i--)
            {
                if (Listeners[i].OnActionPressed(action))
                {
                    lastListener = Listeners[i];
                    break;
                }
            }
        }

        public void OnActionReleased(TInputAction action)
        {
            lastListener.OnActionReleased(action);
        }
    }
}
