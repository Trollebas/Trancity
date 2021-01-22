namespace Common
{
	using SlimDX.DirectInput;
    using System;

    public class FilteredJoystickState
    {
        public bool Arrow_Pressed;
        public int Arrow_Repeat_Ticks;
        public int Arrow_State = -1;
        public int Arrow_Tick;
        public Joystick device;//Device device;
        public JoystickState InputState;
        public bool[] key_pressed;
        public bool[] key_pressed_unfiltered;
        public int[] keyticks;
        public int lasttick;
        public const int Max_Available_Buttons = 0x10;
        public int[] repeat_tick;

        public FilteredJoystickState(Joystick inputDevice, int ArrowTicks, int OtherTicks)
        {
            this.device = inputDevice;
            this.keyticks = new int[0x10];
            this.key_pressed = new bool[0x10];
            this.key_pressed_unfiltered = new bool[0x10];
            this.lasttick = Environment.TickCount;
            this.repeat_tick = GenerateDefaultRepeatTicks(OtherTicks);
            this.Arrow_Tick = ArrowTicks;
            if (this.repeat_tick.Length < 0x10)
            {
                throw new Exception("The repeat tick array lenth must be at least " + 0x10 + "!");
            }
        }

        public static int[] GenerateDefaultRepeatTicks(int OtherTicks)
        {
            int[] numArray = new int[0x10];
            for (int i = 0; i < 0x10; i++)
            {
                numArray[i] = OtherTicks;
            }
            return numArray;
        }

        public void Refresh()
        {
            this.device.Poll();
            this.InputState = this.device.GetCurrentState();
            int num = Environment.TickCount - this.lasttick;
            this.lasttick = Environment.TickCount;
            for (int i = 0; i < 0x10; i++)
            {
            	if ((this.InputState.GetButtons().Length > i) && (this.InputState.GetButtons()[i]))
                {
                    this.key_pressed_unfiltered[i] = true;
                    if (!this.key_pressed[i] && (this.keyticks[i] == 0))
                    {
                        this.key_pressed[i] = true;
                        this.keyticks[i] = 1;
                    }
                    else
                    {
                        this.keyticks[i] += num;
                        if ((this.keyticks[i] >= this.repeat_tick[i]) && (this.repeat_tick[i] != -1))
                        {
                            this.key_pressed[i] = true;
                            this.keyticks[i] = 1;
                        }
                        else
                        {
                            this.key_pressed[i] = false;
                        }
                    }
                }
                else
                {
                    this.key_pressed_unfiltered[i] = false;
                    this.key_pressed[i] = false;
                    this.keyticks[i] = 0;
                }
            }
            int num3 = this.InputState.GetPointOfViewControllers()[0];
            if (num3 != -1)
            {
                if ((!this.Arrow_Pressed && (this.Arrow_Tick == 0)) || (num3 != this.Arrow_State))
                {
                    this.Arrow_Pressed = true;
                    this.Arrow_Tick = 1;
                }
                else
                {
                    this.Arrow_Tick += num;
                    if ((this.Arrow_Tick >= this.Arrow_Repeat_Ticks) && (this.Arrow_Repeat_Ticks != -1))
                    {
                        this.Arrow_Pressed = true;
                        this.Arrow_Tick = 1;
                    }
                    else
                    {
                        this.Arrow_Pressed = false;
                    }
                }
            }
            else
            {
                this.Arrow_Pressed = false;
                this.Arrow_Tick = 0;
            }
            this.Arrow_State = num3;
        }

        public bool this[int button]
        {
            get
            {
                return this[button, true];
            }
            set
            {
                this.key_pressed[button] = value;
            }
        }

        public bool this[int button, bool filter]
        {
            get
            {
                if (filter)
                {
                    return this.key_pressed[button];
                }
                return this.key_pressed_unfiltered[button];
            }
            set
            {
                this.key_pressed[button] = value;
            }
        }
    }
}

