using System;

namespace Engine.Input
{
    /// <summary>
    /// Оригинальный класс из Трансити, переделанный
    /// </summary>
    public class FilteredKeyboardState
    {
        public KeyboardDevice device;
        private bool[] key_pressed;
        private int[] keyticks;
        private int lasttick;
        public const int Max_Available_Keys = 0x100;
        private int[] repeat_tick;

        public FilteredKeyboardState(KeyboardDevice keyboard, int ArrowTicks, int OtherTicks)
        {
            this.device = keyboard;
            this.keyticks = new int[Max_Available_Keys];
            this.key_pressed = new bool[Max_Available_Keys];
            this.lasttick = Environment.TickCount;
            this.repeat_tick = GenerateDefaultRepeatTicks(ArrowTicks, OtherTicks);
        }

        private static int[] GenerateDefaultRepeatTicks(int ArrowTicks, int OtherTicks)
        {
            int[] numArray = new int[Max_Available_Keys];
            for (int i = 0; i < Max_Available_Keys; i++)
            {
                numArray[i] = OtherTicks;
            }
            numArray[0x4d/*(int)Key.LeftAlt*/] = 0;
            numArray[0x4b/*(int)Key.LeftControl*/] = 0;
            numArray[0x4e/*(int)Key.LeftShift*/] = 0;
            numArray[0x77/*(int)Key.RightAlt*/] = 0;
            numArray[0x74/*(int)Key.RightControl*/] = 0;
            numArray[120/*(int)Key.RightShift*/] = 0;
            numArray[0x84/*(int)Key.UpArrow*/] = ArrowTicks;
            numArray[50/*(int)Key.DownArrow*/] = ArrowTicks;
            numArray[0x4c/*(int)Key.LeftArrow*/] = ArrowTicks;
            numArray[0x76/*(int)Key.RightArrow*/] = ArrowTicks;
            return numArray;
        }

        public void Refresh()
        {
            device.RefreshState();
            int num = Environment.TickCount - this.lasttick;
            this.lasttick = Environment.TickCount;
            for (int i = 0; i < Max_Available_Keys; i++)
            {
                if (device.IsKeyPressed(i))
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
                else
                {
                    this.key_pressed[i] = false;
                    this.keyticks[i] = 0;
                }
            }
        }

        public bool this[/*Key*/int key]
        {
            get
            {
                return this.key_pressed[(int)key];
            }
            set
            {
                this.key_pressed[(int)key] = value;
            }
        }
    }
}