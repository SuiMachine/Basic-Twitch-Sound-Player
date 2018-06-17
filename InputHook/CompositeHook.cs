using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace LiveSplit.Model.Input
{
    public delegate void EventHandlerT<T>(object sender, T value);

#pragma warning disable CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
#pragma warning disable CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
    public class KeyOrButton
#pragma warning restore CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
#pragma warning restore CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
    {

        public Keys Key { get; protected set; }

        public KeyOrButton(Keys key)
        {
            Key = key;
        }

        public KeyOrButton(string stringRepresentation)
        {
            Key = (Keys)Enum.Parse(typeof(Keys), stringRepresentation, true);
        }

        public override string ToString()
        {
            return Key.ToString();
        }

        public static bool operator ==(KeyOrButton a, KeyOrButton b)
        {
            if ((object)a == null && (object)b == null)
                return true;
            if ((object)a == null || (object)b == null)
                return false;

            return a.Key == b.Key;
        }

        public static bool operator !=(KeyOrButton a, KeyOrButton b)
        {
            return !(a == b);
        }
    }

    public class CompositeHook
    {
        protected LowLevelKeyboardHook KeyboardHook { get; set; }

        public event KeyEventHandler KeyPressed;
        public event EventHandlerT<KeyOrButton> KeyOrButtonPressed;

        public CompositeHook()
        {
            KeyboardHook = new LowLevelKeyboardHook();
            KeyboardHook.KeyPressed += KeyboardHook_KeyPressed;
        }

        void KeyboardHook_KeyPressed(object sender, KeyEventArgs e)
        {
            KeyPressed?.Invoke(this, e);
            KeyOrButtonPressed?.Invoke(this, new KeyOrButton(e.KeyCode | e.Modifiers));
        }


        public void RegisterHotKey(Keys key)
        {
            KeyboardHook.RegisterHotKey(key);
        }

        public void RegisterHotKey(KeyOrButton keyOrButton)
        {
            RegisterHotKey(keyOrButton.Key);
        }

        public void Poll()
        {
            KeyboardHook.Poll();
        }

        public void UnregisterAllHotkeys()
        {
            KeyboardHook.UnregisterAllHotkeys();
        }
    }
}
