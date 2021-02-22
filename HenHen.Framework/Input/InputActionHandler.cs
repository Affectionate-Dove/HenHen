using System.Collections.Generic;

namespace HenHen.Framework.Input
{
    public abstract class InputActionHandler<TInputAction>
    {
        /// <summary>
        /// Keys that have to be pressed together
        /// to trigger an action.
        /// </summary>
        private readonly Dictionary<TInputAction, List<KeyboardKey>> actionKeyBindings = new();

        /// <summary>
        /// Keys that will be monitored for presses,
        /// because they can trigger an action.
        /// </summary>
        private readonly Dictionary<KeyboardKey, List<TInputAction>> keysToMonitor;

        /// <summary>
        /// A list of input actions for which
        /// keybindings were pressed but not yet released.
        /// </summary>
        private readonly List<TInputAction> activeInputActions = new();

        public virtual InputManager InputManager { get; set; }

        /// <summary>
        /// Keys that have to be pressed together
        /// to trigger an action.
        /// </summary>
        public IReadOnlyDictionary<TInputAction, List<KeyboardKey>> ActionKeyBindings => actionKeyBindings;

        /// <summary>
        /// A list of input actions for which
        /// keybindings were pressed but not yet released.
        /// </summary>
        public IReadOnlyList<TInputAction> ActiveInputActions => activeInputActions;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputManager"></param>
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
                            OnActionPress(possibleAction);
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
                    OnActionRelease(activeAction);
                else
                    i++;
            }
        }

        /// <summary>
        /// Whether all keys in an action keybinding are pressed.
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
        /// Triggered when all keys in a keybinding
        /// for a given InputAction were pressed.
        /// </summary>
        /// <param name="inputAction">The action that was triggered.</param>
        public virtual void OnActionPress(TInputAction inputAction)
        {
        }

        /// <summary>
        /// Triggered when at least one key in a keybinding
        /// for a given InputAction was released.
        /// </summary>
        /// <param name="inputAction">The action that was triggered.</param>
        public virtual void OnActionRelease(TInputAction inputAction)
        {
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
