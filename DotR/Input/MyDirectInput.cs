#if false
using SlimDX;
using SlimDX.DirectInput;
using System;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;

namespace Engine
{
    public class MyDirectInput
    {
        public static bool alt_f4;
        public const int AxisRange = 0x400;
        public static Guid[] DeviceGuids;
        public static string[] DeviceNames;
        public static FilteredKeyboardState Key_State;
		public static Keyboard Keyboard_Device = null;
        public static byte[] last_buttons = new byte[5];
		public static Mouse Mouse_Device = null;
        public static MouseState Mouse_State;
        private static DirectInput dinput;

        public static bool Acquire()
        {
            try
            {
                Keyboard_Device.Acquire();
                Mouse_Device.Acquire();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void EnumerateDevices()
        {
        	dinput = new DirectInput();
        	var devices = new List<DeviceInstance>(dinput.GetDevices(DeviceClass.Keyboard, DeviceEnumerationFlags.AttachedOnly));
        	var list2 = new List<DeviceInstance>(dinput.GetDevices(DeviceClass.GameController, DeviceEnumerationFlags.AttachedOnly));
            DeviceGuids = new Guid[list2.Count + 1];
            DeviceNames = new string[list2.Count + 1];
            int index = 0;
            foreach (DeviceInstance instance in list2)
            {
                DeviceGuids[index] = instance.InstanceGuid;
                DeviceNames[index] = instance.InstanceName;
                index++;
            }
            foreach (DeviceInstance instance2 in devices)
            {
                if (instance2.Type == DeviceType.Keyboard)
                {
                    DeviceGuids[index] = instance2.InstanceGuid;
                    DeviceNames[index] = instance2.InstanceName;
                    index++;
                    break;
                }
            }
        }

        public static void Free()
        {
            if (Keyboard_Device != null)
            {
                Keyboard_Device.Unacquire();
                Keyboard_Device.Dispose();
                Keyboard_Device = null;
            }
            if (Mouse_Device != null)
            {
                Mouse_Device.Unacquire();
                Mouse_Device.Dispose();
                Mouse_Device = null;
            }
        }

        public static bool Initialize(Control control)
        {
            return Initialize(control, false, false);//true all
        }

        public static bool Initialize(Control control, bool keyboard_exclusive, bool mouse_exclusive)
        {
            try
            {
            	Keyboard_Device = new Keyboard(dinput);//new Device(SystemGuid.Keyboard);
                if (Key_State == null)
                {
                    Key_State = new FilteredKeyboardState(Keyboard_Device, 200, -1);
                }
                else
                {
                    Key_State.device = Keyboard_Device;
                }
                Mouse_Device = new Mouse(dinput);
                CooperativeLevel flags = CooperativeLevel.Foreground | CooperativeLevel.Exclusive;
                CooperativeLevel flags2 = CooperativeLevel.Foreground | CooperativeLevel.Nonexclusive;
                Keyboard_Device.SetCooperativeLevel(control, keyboard_exclusive ? flags : flags2);
                Mouse_Device.SetCooperativeLevel(control, mouse_exclusive ? flags : flags2);
            }
            catch (DirectInputException e)
            {
            	Logger.LogException(e);
                Free();
                return false;
            }
            Acquire();
            return true;
        }

        public static bool Process()
        {
            try
            {
                Key_State.Refresh();
                Mouse_State = Mouse_Device.GetCurrentState();
            }
            catch
            {
                try
                {
                    Acquire();
                    Key_State.Refresh();
                    Mouse_State = Mouse_Device.GetCurrentState();
                }
                catch
                {
                    return false;
                }
            }
            if (Key_State.InputState.IsPressed(Key.Escape) || alt_f4)
            {
                alt_f4 = true;
                Application.Exit();
                return false;
            }
            return true;
        }
    }
}

#endif