namespace Common
{
    using SlimDX.DirectInput;
    using System;

    public class FilteredKeyboardState
    {
        public Keyboard device;
        private KeyboardState InputState;
        public bool[] key_pressed;
        public int[] keyticks;
        public int lasttick;
        public const int Max_Available_Keys = 0x100;
        public int[] repeat_tick;

        public FilteredKeyboardState(Keyboard inputDevice, int ArrowTicks, int OtherTicks)
        {
            this.device = inputDevice;
            this.keyticks = new int[Max_Available_Keys];
            this.key_pressed = new bool[Max_Available_Keys];
            this.lasttick = Environment.TickCount;
            this.repeat_tick = GenerateDefaultRepeatTicks(ArrowTicks, OtherTicks);
        }

        public static int[] GenerateDefaultRepeatTicks(int ArrowTicks, int OtherTicks)
        {
            int[] numArray = new int[Max_Available_Keys];
            for (int i = 0; i < Max_Available_Keys; i++)
            {
                numArray[i] = OtherTicks;
            }
			numArray[(int)Key.LeftAlt] = 0;
			numArray[(int)Key.LeftControl] = 0;
			numArray[(int)Key.LeftShift] = 0;
			numArray[(int)Key.RightAlt] = 0;
			numArray[(int)Key.RightControl] = 0;
			numArray[(int)Key.RightShift] = 0;
            numArray[(int)Key.UpArrow] = ArrowTicks;
            numArray[(int)Key.DownArrow] = ArrowTicks;
            numArray[(int)Key.LeftArrow] = ArrowTicks;
            numArray[(int)Key.RightArrow] = ArrowTicks;
            return numArray;
        }

        public void Refresh()
        {
        	this.InputState = this.device.GetCurrentState();//.GetCurrentKeyboardState();
            int num = Environment.TickCount - this.lasttick;
            this.lasttick = Environment.TickCount;
            for (int i = 0; i < Max_Available_Keys; i++)
            {
            	if (this.InputState.IsPressed((Key) i))//[(Key) i])
                {
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
                else// if (this.InputState.IsReleased((Key) i))
                {
                    this.key_pressed[i] = false;
                    this.keyticks[i] = 0;
                }
            }
        }

        public bool this[Key key]
        {
            get
            {
                return this.key_pressed[(int) key];
            }
            set
            {
                this.key_pressed[(int) key] = value;
            }
        }
        
        ///
        
        public bool IsDirtyPressed(Key key)
        {
        	return this.InputState.IsPressed(key);
        }
    }
}

