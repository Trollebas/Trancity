namespace Common
{
//    using Microsoft.DirectX;
//    using Microsoft.DirectX.DirectInput;
    using SlimDX;
    using SlimDX.DirectInput;
    using System;
    using System.Windows.Forms;
    using System.Collections;
	using System.Collections.Generic;

    public class MyDirectInput
    {
        public static bool alt_f4;
        public const int AxisRange = 0x400;
        public static Guid[] DeviceGuids;
        public static string[] DeviceNames;
        public static FilteredJoystickState[] Joystick_FilteredStates;
        public static JoystickState[] Joystick_States;
        public static Joystick[] JoystickDevices;
        public static FilteredKeyboardState Key_State;
//        public static Device Keyboard_Device = null;
		public static Keyboard Keyboard_Device = null;
        public static byte[] last_buttons = new byte[5];
//        public static Device Mouse_Device = null;
		public static Mouse Mouse_Device = null;
        public static MouseState Mouse_State;
        private static DirectInput dinput;

        public static bool Acquire()
        {
            try
            {
                Keyboard_Device.Acquire();
                Mouse_Device.Acquire();
                for (int i = 0; i < JoystickDevices.Length; i++)
                {
                    JoystickDevices[i].Acquire();
                }
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
//        	DeviceInstance[] devices = dinput.GetDevices(DeviceClass.Keyboard, DeviceEnumerationFlags.AttachedOnly);
//        	DeviceInstance[] list2 = dinput.GetDevices(DeviceClass.GameController, DeviceEnumerationFlags.AttachedOnly);
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
//                if (instance2.InstanceGuid == SystemGuid.Keyboard)
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
            for (int i = 0; i < JoystickDevices.Length; i++)
            {
                if (JoystickDevices[i] != null)
                {
                    JoystickDevices[i].Unacquire();
                    JoystickDevices[i].Dispose();
                    JoystickDevices[i] = null;
                }
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
                Mouse_Device = new Mouse(dinput);;//new Device(SystemGuid.Mouse);
                JoystickDevices = new Joystick[DeviceGuids.Length - 1];
                Joystick_FilteredStates = new FilteredJoystickState[JoystickDevices.Length];
                for (int i = 0; i < JoystickDevices.Length; i++)
                {
                    JoystickDevices[i] = new Joystick(dinput, DeviceGuids[i]);
//                    JoystickDevices[i].SetDataFormat(DeviceDataFormat.Joystick);
                    Joystick_FilteredStates[i] = new FilteredJoystickState(JoystickDevices[i], 200, -1);
                }
                Joystick_States = new JoystickState[JoystickDevices.Length];
            /*}
            catch (DirectInputException)//InputException)
            {
                Free();
                return false;
            }
            try
            {*/
                CooperativeLevel flags = CooperativeLevel.Foreground | CooperativeLevel.Exclusive;
                CooperativeLevel flags2 = CooperativeLevel.Foreground | CooperativeLevel.Nonexclusive;
                Keyboard_Device.SetCooperativeLevel(control, keyboard_exclusive ? flags : flags2);
                Mouse_Device.SetCooperativeLevel(control, mouse_exclusive ? flags : flags2);
                for (int j = 0; j < JoystickDevices.Length; j++)
                {
                    JoystickDevices[j].SetCooperativeLevel(control, flags);
                    JoystickDevices[j].Properties.SetRange(-1024, 0x400);//(ParameterHow.ByDevice, 0, new InputRange(-1024, 0x400));
                    JoystickDevices[j].Properties.DeadZone = 500;//.SetDeadZone(ParameterHow.ByDevice, 0, 500);
                }
            }
            catch (DirectInputException e)//(InputException exception)
            {
            	Logging.WriteExc(e);
                /*if (exception is UnsupportedException)
                {*/
                    Free();
                    return false;
                /*}*/
            }
            Acquire();
            return true;
        }

        public static bool Process()
        {
            try
            {
                Key_State.Refresh();
                Mouse_State = Mouse_Device.GetCurrentState();//.CurrentMouseState;
                for (int i = 0; i < JoystickDevices.Length; i++)
                {
                    Joystick_FilteredStates[i].Refresh();
                    Joystick_States[i] = Joystick_FilteredStates[i].InputState;
                }
            }
            catch //(DirectInputException)//(DirectXException)
            {
                try
                {
                    Acquire();
                    Key_State.Refresh();
                    Mouse_State = Mouse_Device.GetCurrentState();//.CurrentMouseState;
                    for (int j = 0; j < JoystickDevices.Length; j++)
                    {
                        Joystick_FilteredStates[j].Refresh();
                        Joystick_States[j] = Joystick_FilteredStates[j].InputState;
                    }
                }
                catch
                {
                    return false;
                }
            }
            if ((Key_State.InputState.IsPressed(Key.LeftAlt) && Key_State.InputState.IsPressed(Key.F4)) || (alt_f4))
            {
                alt_f4 = true;
                Application.Exit();
                return false;
            }
            return true;
        }
    }
}

