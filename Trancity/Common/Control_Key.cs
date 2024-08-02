/*namespace Common
{
//    using Microsoft.DirectX.DirectInput;
    using SlimDX.DirectInput;
    using System;
//    using System.Reflection;

    public class Control_Key
    {
        public int button;
        public bool joystick;
        private FilteredJoystickState joystick_state;
        public Key key;
        private FilteredKeyboardState keyboard_state;

        public Control_Key()
        {
            this.joystick = false;
            this.keyboard_state = MyDirectInput.Key_State;
            this.key = (Key) 0;
        }

        public Control_Key(Key key) : this(MyDirectInput.Key_State, key)
        {
        }

        public Control_Key(FilteredJoystickState joystick_state, int button)
        {
            this.joystick = true;
            this.joystick_state = joystick_state;
            this.button = button;
        }

        public Control_Key(FilteredKeyboardState keyboard_state, Key key)
        {
            this.joystick = false;
            this.keyboard_state = keyboard_state;
            this.key = key;
        }

        public bool this[bool filter]
        {
            get
            {
                if (!this.joystick)
                {
                    if (!filter)
                    {
                    	return this.keyboard_state.InputState.IsPressed(this.key);//[this.key];
                    }
                    return this.keyboard_state[this.key];
                }
                if (this.button >= 0)
                {
                    return this.joystick_state[this.button, filter];
                }
                switch (this.button)
                {
                    case -4:
                        if (this.joystick_state.Arrow_State < 0x57e4)
                        {
                            return false;
                        }
                        return (this.joystick_state.Arrow_State <= 0x7b0c);

                    case -3:
                        if (this.joystick_state.Arrow_State < 0x34bc)
                        {
                            return false;
                        }
                        return (this.joystick_state.Arrow_State <= 0x57e4);

                    case -2:
                        if (this.joystick_state.Arrow_State < 0x1194)
                        {
                            return false;
                        }
                        return (this.joystick_state.Arrow_State <= 0x34bc);

                    case -1:
                        return (((this.joystick_state.Arrow_State >= 0) && (this.joystick_state.Arrow_State <= 0x1194)) || (this.joystick_state.Arrow_State >= 0x7b0c));
                }
                return false;
            }
        }
    }
}
*/
