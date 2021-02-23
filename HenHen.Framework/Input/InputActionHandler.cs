using System.Collections.Generic;

namespace HenHen.Framework.Input
{
    /// <summary>
    /// Abstracts away inputs from keyboard keys to <see cref="TInputAction"/>s.
    /// </summary>
    /// <remarks>
    /// For example, a <see cref="TInputAction"/> enum
    /// may have a GoForward action, which is binded to key "W".
    /// This class manages monitoring the key "W" and translates
    /// it into the GoForward action, so that even if the key binding changes,
    /// the action remains the same.
    /// </remarks>
    public abstract class InputActionHandler<TInputAction>
    {
        /// <summary>
        /// For each <see cref="TInputAction"/>, a list of
        /// keyboard keys that have to be pressed together
        /// to trigger that <see cref="TInputAction"/>.
        /// </summary>
        private readonly Dictionary<TInputAction, List<KeyboardKey>> actionKeyBindings = new();

        /// <summary>
        /// It may be expensive to check all keyboard keys
        /// for input at every game update, so this list stores
        /// only the keys that can trigger a <see cref="TInputAction"/>.
        /// 
        /// </summary>
        private readonly Dictionary<KeyboardKey, List<TInputAction>> keysToMonitor = new();

        /// <summary>
        /// A list of <see cref="TInputAction"/>s that were
        /// pressed but not yet released.
        /// </summary>
        private readonly List<TInputAction> activeInputActions = new();

        public virtual InputManager InputManager { get; set; }

        /// <summary>
        /// For each <see cref="TInputAction"/>, a list of
        /// keyboard keys that have to be pressed together
        /// to trigger that <see cref="TInputAction"/>.
        /// </summary>
        public IReadOnlyDictionary<TInputAction, List<KeyboardKey>> ActionKeyBindings => actionKeyBindings;

        /// <summary>
        /// A list of <see cref="TInputAction"/>s that were
        /// pressed but not yet released.
        /// </summary>
        public IReadOnlyList<TInputAction> ActiveInputActions => activeInputActions;

        public InputActionHandler(InputManager inputManager)
        {
            SetKeyBindings(CreateDefaultKeybindings());
            InputManager = inputManager;
        }

        public void SetKeyBindings(IReadOnlyDictionary<TInputAction, List<KeyboardKey>> keyBindings)
        {
            actionKeyBindings.Clear();
            foreach (var keyValue in keyBindings)
                actionKeyBindings.Add(keyValue.Key, keyValue.Value);
            SetKeysToMonitor();
        }

        public abstract Dictionary<TInputAction, List<KeyboardKey>> CreateDefaultKeybindings();

        public void Update()
        {
            HandleMonitoredKeysPresses();
            HandleActiveActionsReleases();
        }

        private void HandleMonitoredKeysPresses()
        {
            foreach (var (monitoredKey, possibleActions) in keysToMonitor)
            {
                if (InputManager.IsKeyPressed(monitoredKey))
                {
                    foreach (var possibleAction in possibleActions)
                    {
                        if (AreActionKeysAllPressed(possibleAction))
                        {
                            activeInputActions.Add(possibleAction);
                            OnActionPress(possibleAction);
                        }
                    }
                }
            }
        }

        private void HandleActiveActionsReleases()
        {
            TInputAction activeAction;
            var i = 0;
            while (i < activeInputActions.Count)
            {
                activeAction = activeInputActions[i];
                if (!AreActionKeysAllPressed(activeAction))
                {
                    activeInputActions.RemoveAt(i);
                    OnActionRelease(activeAction);
                }
                else
                    i++;
            }
        }

        /// <summary>
        /// Whether all keys for a <see cref="TInputAction"/>
        /// keybinding are pressed.
        /// </summary>
        public bool AreActionKeysAllPressed(TInputAction action)
        {
            foreach (var key in ActionKeyBindings[action])
            {
                if (!InputManager.IsKeyDown(key))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Called when all keys in a keybinding
        /// for a given <see cref="TInputAction"/> were pressed.
        /// </summary>
        /// <param name="inputAction">The action that was triggered.</param>
        protected void OnActionPress(TInputAction inputAction)
        {
            // TODO: here call an InputPropagator
        }

        /// <summary>
        /// Triggered when at least one key in a keybinding
        /// for a pressed <see cref="TInputAction"/> was released.
        /// </summary>
        /// <param name="inputAction">The action that was triggered.</param>
        protected void OnActionRelease(TInputAction inputAction)
        {
            // TODO: here call an InputPropagator
        }

        /// <summary>
        /// Generates contents of <see cref="keysToMonitor"/>
        /// based on <see cref="actionKeyBindings"/>.
        /// </summary>
        private void SetKeysToMonitor()
        {
            keysToMonitor.Clear();
            foreach (var (action, keys) in actionKeyBindings)
            {
                foreach (var key in keys)
                {
                    if (!keysToMonitor.ContainsKey(key))
                        keysToMonitor.Add(key, new List<TInputAction>());
                    keysToMonitor[key].Add(action);
                }
            }
        }
    }
}
