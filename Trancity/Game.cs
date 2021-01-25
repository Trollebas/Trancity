using System;
using System.Drawing;
using Common;
using Engine;
using SlimDX.Direct3D9;
using SlimDX.DirectInput;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Trancity
{
    public class Game
    {
//        private byte[] _lastMouseButtons = new byte[5];
        private bool[] _lastMouseButtons = new bool[5];
        public bool ������� = true;
        public �����[] ������;
        public World ���;
        private int _���������PosIndex;
        private const int num = 0x400;
        public static bool fmouse = false;
        public static bool space = true;
        public bool NewControl = false;
        public MyMenu menu;
        //test:
        public static int col = 0;
        public static int row = 0;
//        public Transport[] ���������Array;// = ���.����������.ToArray(typeof(Transport));




        public void Process_Input()
        {
            if (������� && (���.����������.Count > 0))
            {
                this._���������PosIndex++;
                if (this._���������PosIndex >= ���.����������.Count)//.Length)
                {
                    this._���������PosIndex = 0;
                }
                foreach (var ��������� in ((Transport)���.����������[this._���������PosIndex]).���������_���������)
                {
                    if (���������.������ != null)
                    {
                        ���������.������.����������������.Remove(���������);
                    }
                }
                ((Transport)���.����������[_���������PosIndex]).�����������������(���);
                foreach (var ���������2 in ((Transport)���.����������[_���������PosIndex]).���������_���������)
                {
                    if (���������2.������ != null)
                    {
                        ���������2.������.����������������.Add(���������2);
                    }
                }
                foreach (Transport ��������� in ���.����������)
                {
                    if (���������.����������.��������������)
                    {
                        ���������.����������������������(���);
                    }
                }
            }
            var MouseState = MyDirectInput.Mouse_State;
            var KeyState = MyDirectInput.Key_State;
            var JStatesArray = MyDirectInput.Joystick_States;
            var FJStatesArray = MyDirectInput.Joystick_FilteredStates;
            var joystickDevices = MyDirectInput.JoystickDevices;
            var deviceGuids = MyDirectInput.DeviceGuids;
//            const int num = 0x400;
//            byte[] mouseButtons = state.GetMouseButtons();
            bool[] mouseButtons = MouseState.GetButtons();
            int x = MouseState.X;
            int y = MouseState.Y;
            int z = MouseState.Z;
            if (MyDirectInput.alt_f4) return;
            if (!�������)
            {
                menu.Refresh();
            }
            var changed = false;
            var changed_time = false;
            if (KeyState[Key.F7])
            {
                NewControl = !NewControl;
            }
            if (KeyState[Key.PageUp])
            {
                MyDirect3D.light_intency = Math.Min(MyDirect3D.light_intency + 0.1f, 1.0f);
                changed = true;
            }
            if (KeyState[Key.PageDown])
            {
                MyDirect3D.light_intency = Math.Max(MyDirect3D.light_intency - 0.1f, 0.0f);
                changed = true;
            }
            if (KeyState[Key.Tab])
                {
                    MyDirect3D.���_������ = !MyDirect3D.���_������;
                }
            /*if (KeyState[Key.F7])
            {
                changed_time = !changed_time;
            }*/
            if (changed)
            {
                int color = (int)(MyDirect3D.light_color * MyDirect3D.light_intency);
                for (int i = 0; i < 11; i += 2)
                {
                    var light = MyDirect3D.device.GetLight(i);
                    light.Diffuse = Color.FromArgb(color, color, color);
                    MyDirect3D.device.SetLight(i, light);
                }
            }
            if (changed_time)
            {
                
            }
            if (KeyState[Key.Escape])
            {
                ������� = !�������;
            }
            for (var i = 0; i < joystickDevices.Length; i++)
            {
                if (FJStatesArray[i][9])
                {
                    ������� = !�������;
                }
            }
            if (!NewControl)
            {
            if (�������)
            {
                foreach (var ����� in ������)
                {
                    if (�����.����������������� != null)
                    {
                        DoublePoint point5 = �����.�����������������.position - �����.cameraPosition.XZPoint;
                        if (point5.Modulus > 200.0)
                        {
                            �����.�����������������.���������� = ����������.��������������;
                            �����.����������������� = null;
                            �����.�������������� = null;
                        }
                    }
                    int current_joystick = -1;
                    for (int k = 0; k < joystickDevices.Length; k++)
                    {
                        if (�����.inputGuid == deviceGuids[k])
                        {
                            current_joystick = k;
                            break;
                        }
                    }
                    if (current_joystick == -1)//(�����.inputGuid == MyDirectInput.Keyboard_Device.Information.InstanceGuid)//SystemGuid.Keyboard)
                    {
//                        byte[] mouseButtons = state.GetMouseButtons();
//                        int x = state.X;
//                        int y = state.Y;
//                        int z = state.Z;
//                        if ((mouseButtons[1] != 0) && (this._lastMouseButtons[1] == 0))
                        if ((mouseButtons[1]) && (!this._lastMouseButtons[1]))
                        {
                            this.�����������(�����);
                        }
                        if (KeyState[Key.F9])
                        {
                            IVector _�������� = null;
                            IControlledObject _������ = null;
                            �����.�������������� = _��������;
                        if ((�����.����������������� != null) && (�����.����������������� != _������))
                            {
                            �����.�����������������.���������� = ����������.��������������;
                            }
                            �����.����������������� = _������;
                        }
                        if (!MyDirect3D.���_������)
                        {
//                            if (mouseButtons[0] == 0)
                            if (!mouseButtons[0])
                            {
                                �����.cameraRotationChange.x -= 0.001 * x;
                                �����.cameraRotationChange.y -= 0.001 * y;
                            }
                            else
                            {
                                DoublePoint point = new DoublePoint(�����.cameraPositionChange.x, �����.cameraPositionChange.z) / new DoublePoint(�����.cameraRotation.x);
                                point.x -= 0.1 * y;
                                point.y -= 0.1 * x;
                                �����.cameraPositionChange.x = (point * new DoublePoint(�����.cameraRotation.x)).x;
                                �����.cameraPositionChange.z = (point * new DoublePoint(�����.cameraRotation.x)).y;
                            }
                            �����.cameraPositionChange.y += 0.001 * z;
                        }
                        else
                        {                            
                            MyDirect3D.������� += 0.001 * z;
                            if (MyDirect3D.������� <= 2.5) MyDirect3D.������� = 2.5;
//                            if (mouseButtons[0] != 0)
                            if (mouseButtons[0])
                            {
                                DoublePoint point = new DoublePoint(�����.cameraPositionChange.x, �����.cameraPositionChange.z);
                                point.x += 0.01 * x;
                                point.y -= 0.01 * y;
                                �����.cameraPositionChange.x = point.x;
                                �����.cameraPositionChange.z = point.y;
                            }
                        }
                        this._lastMouseButtons = mouseButtons;
                    }
                    else
                    {
                        FilteredJoystickState Current_FJState = FJStatesArray[current_joystick];
                        JoystickState Current_JState = JStatesArray[current_joystick];
                        if (Current_FJState[8])
                        {
                            this.�����������(�����);
                        }
                        double num8 = (0.05 * Current_JState.X) / ((double) num);
                        double num9 = (0.02 * Current_JState.Y) / ((double) num);
                        double num10 = (0.05 * Current_JState.Z) / ((double) num);
                        switch (Current_JState.GetPointOfViewControllers()[0])//.GetPointOfView()[0])
                        {
                            case 0:
                                num10 = 0.04;
                                break;

                            case 0x4650:
                                num10 = -0.04;
                                break;

                            default:
                                num10 = 0.0;
                                break;
                        }
                        if (((�����.����������������� != null) && (�����.����������������� is ������������_���������)) && �����.�����������������.����������.������)
                        {
                            if (!Current_FJState[4, false])
                            {
                                int num12 = 6;
                                if (((Transport) �����.�����������������).�������_���������� is �������_����������.����������)
                                {
                                    num12 = 10;
                                }
                                if (Current_FJState[num12, false])
                                {
                                    �����.cameraRotationChange.x -= num8;
                                    �����.cameraRotationChange.y -= num9;
                                }
                            }
                            else
                            {
                                DoublePoint point2 = new DoublePoint(�����.cameraPositionChange.x, �����.cameraPositionChange.z) / new DoublePoint(�����.cameraRotation.x);
                                point2.x -= 10.0 * num9;
                                point2.y -= 10.0 * num8;
                                �����.cameraPositionChange.x = (point2 * new DoublePoint(�����.cameraRotation.x)).x;
                                �����.cameraPositionChange.z = (point2 * new DoublePoint(�����.cameraRotation.x)).y;
                                �����.cameraPositionChange.y += num10;
                            }
                        }
                        else if (!Current_FJState[4, false])
                        {
                            �����.cameraRotationChange.x -= num8;
                            �����.cameraRotationChange.y -= num9;
                        }
                        else
                        {
                            DoublePoint point3 = new DoublePoint(�����.cameraPositionChange.x, �����.cameraPositionChange.z) / new DoublePoint(�����.cameraRotation.x);
                            point3.x -= 10.0 * num9;
                            point3.y -= 10.0 * num8;
                            �����.cameraPositionChange.x = (point3 * new DoublePoint(�����.cameraRotation.x)).x;
                            �����.cameraPositionChange.z = (point3 * new DoublePoint(�����.cameraRotation.x)).y;
                            �����.cameraPositionChange.y += num10;
                        }
                    }
                    /*var _y = ���.GetHeight(�����.cameraPosition.xz_point);
                    if (�����.cameraPosition.y - 0.01 < _y)
                    {
                        �����.cameraPosition.y = _y + 0.01;
                    	�����.cameraPositionChange.y = 0;
                    }*/
					if (�����.����������������� == null) continue;
					var _transport = (Transport) �����.�����������������;
					if (current_joystick == -1)//(�����.inputGuid == MyDirectInput.Keyboard_Device.Information.InstanceGuid)//SystemGuid.Keyboard)
					{
						if (KeyState[Key.A])
		                {// TODO: ��������� ������ ��������� ��� ����� ����������
		                	_transport.����������.�������������� = !_transport.����������.��������������;
		                	//_transport.currentStop = _transport.nextStop = null;
		                	//_transport.nextStop = _transport.currentStop = null;
		                    _transport.stopIndex = 0;
		                }
		                if (KeyState[Key.M])
		                {
		                	_transport.����������.������ = !_transport.����������.������;
		                	//_transport.currentStop = _transport.nextStop = null;
                            //_transport.nextStop = _transport.currentStop = null;
                            _transport.stopIndex = 0;
		                }
		                if (_transport.����������.������)
		                {
		                	if (KeyState[Key.Y])
		                    {
		                    	_transport.������� = !_transport.�������;
		                    }
		                	if (KeyState[Key.S])
		                    {
		                    	if (!_transport.�����_��������_�������)
		                        {
		                        	_transport.��������������������(false);
		                        }
		                        else if (!_transport.�����_��������_�������)
		                        {
		                        	_transport.��������������������(true);
		                        }
		                    }
		                	if (KeyState[Key.D])
		                    {
		                    	if (!_transport.�����_�������)
		                        {
		                        	_transport.������������(false);
		                        }
		                        else if (!_transport.�����_�������)
		                        {
		                        	_transport.������������(true);
		                        }
		                    }
		                    if (KeyState[Key.D1])
		                    {
		                        if (!_transport.������������(0))
		                        {
		                        	_transport.������������(0, false);
		                        }
		                        else if (!_transport.������������(0))
		                        {
		                        	_transport.������������(0, true);
		                    	}
		                    }
		                    if (KeyState[Key.D2])
		                    {
		                        if (!_transport.������������(1))
		                        {
		                        	_transport.������������(1, false);
		                        }
		                        else if (!_transport.������������(0))
		                        {
		                        	_transport.������������(1, true);
		                    	}
		                    }
		                    if (KeyState[Key.D3])
		                    {
		                        if (!_transport.������������(2))
		                        {
		                        	_transport.������������(2, false);
		                        }
		                        else if (!_transport.������������(0))
		                        {
		                        	_transport.������������(2, true);
		                    	}
		                    }
		                    if (KeyState[Key.D4])
		                    {
		                        if (!_transport.������������(3))
		                        {
		                        	_transport.������������(3, false);
		                        }
		                        else if (!_transport.������������(0))
		                        {
		                        	_transport.������������(3, true);
		                    	}
		                    }
		                    if (KeyState[Key.D5])
		                    {
		                        if (!_transport.������������(4))
		                        {
		                        	_transport.������������(4, false);
		                        }
		                        else if (!_transport.������������(0))
		                        {
		                        	_transport.������������(4, true);
		                    	}
		                    }
		                    if (KeyState[Key.B])
	                        {
	                        	_transport.stand_brake = !_transport.stand_brake;
	                        }
		                    /*if (state2[Key.E])
	                        {
	                            if (transport.���������_������������)
	                            {
	                            	transport.���������_������������ = false;
	                            }
	                            else
	                            {
	                            	transport.���������_������������ = true;
	                            	transport.���������_�������� = 0;
	                        	}
	                        }*/
	                        if (KeyState[Key.F])
	                        {
	                        	_transport.��������_���� = !_transport.��������_����;
	                        }
		                }
		                if (�����.�������������� != null)
		                {
		                	if (KeyState[Key.C])//KeyState.InputState.IsPressed(Key)
			                {
			                	_transport.SetCamera(0, �����);
			                }
			                if (KeyState[Key.F2])
			                {
			                	_transport.SetCamera(1, �����);
			                }
			                 if (KeyState[Key.F3])
			                {
			                	_transport.SetCamera(2, �����);
			                }
			                if (KeyState[Key.F4])
			                {
			                	_transport.SetCamera(3, �����);
			                }
		                }
	                }
                    if (�����.����������������� is �������)
                    {
	                    ������� ������� = (�������) �����.�����������������;
	                    if (current_joystick == -1)//(�����.inputGuid == MyDirectInput.Keyboard_Device.Information.InstanceGuid)//SystemGuid.Keyboard)
	                    {
	                        if (�������.����������.������)
	                        {
	                            if (KeyState[Key.T])
	                            {
	                                if (�������.�����������.������)
	                                {
	                                	�������.�����������.�����������(���.�����������������2);
	                                	if (�������.�����������.������ != null)
	                                	{
	                                    	�������.�����������.����������� = true;
	                                	}
	                                }
	                                else if (�������.�����������.������)
	                                {
	                                    �������.�����������.����������� = false;
	                                }
	                            }
	                            if (KeyState[Key.D6])
	                            {
	                                if (!�������.������������(5))
	                                {
	                                    �������.������������(5, false);
	                                }
	                                else if (!�������.������������(5))
	                                {
	                                    �������.������������(5, true);
	                                }
	                            }
	                            if (KeyState[Key.D7])
	                            {
	                                if (!�������.������������(6))
	                                {
	                                    �������.������������(6, false);
	                                }
	                                else if (!�������.������������(6))
	                                {
	                                    �������.������������(6, true);
	                                }
	                            }
	                            if (KeyState[Key.D8])
	                            {
	                                if (!�������.������������(7))
	                                {
	                                    �������.������������(7, false);
	                                }
	                                else if (!�������.������������(7))
	                                {
	                                    �������.������������(7, true);
	                                }
	                            }
	                            if (KeyState[Key.D9])
	                            {
	                                if (!�������.������������(8))
	                                {
	                                    �������.������������(8, false);
	                                }
	                                else if (!�������.������������(8))
	                                {
	                                    �������.������������(8, true);
	                                }
	                            }
	                            if (KeyState[Key.D0])
	                            {
	                                if (!�������.������������(9))
	                                {
	                                    �������.������������(9, false);
	                                }
	                                else if (!�������.������������(9))
	                                {
	                                    �������.������������(9, true);
	                                }
	                            }
	                            if (�������.�������_���������� is �������_����������.����_�������)
	                            {
	                            	�������_����������.����_������� �������2 = (�������_����������.����_�������) �������.�������_����������;
	                            	if ((KeyState[Key.Backspace] && (�������.�������� == 0.0)) && (�������2.�������_����������� == 0))
	                            	{
	                            	    �������2.�������_��������� = -�������2.�������_���������;
	                            	}
	                            	 //if (KeyState[Key.LeftAlt] && (�������2.�������_����������� != 0))
                                   //{
                                         //  �������2.�������_����������� = 0;
                                         //  �������2.��������������_������ += 0.05;
                                           // }
	                            	if (KeyState[Key.DownArrow])
	                            	{
	                            	    if (�������2.�������_����������� > �������2.�������_min)
	                            	    {
	                            	        �������2.�������_�����������--;
	                            	    }
	                            	}
	                            	else if (KeyState[Key.UpArrow] && (�������2.�������_����������� < �������2.�������_max))
	                            	{
	                                	�������2.�������_�����������++;
	                            	}
	                            }
	                            if (!KeyState[Key.RightControl])
	                            {
	                            	if (KeyState[Key.LeftArrow])
	                                {
	                                	if (�������.���������_�������� >= 0)
	                                    {
	                                        �������.���������_�������� = -1;
	                                        �������.���������_������������ = false;
	                                    }
	                                    else
	                                    {
	                                        �������.���������_�������� = 0;
	                                    }
	                                }
	                            	if (KeyState[Key.RightArrow])
	                                {
	                            	       if (�������.���������_�������� <= 0)
	                                    {
	                                        �������.���������_�������� = 1;
	                                        �������.���������_������������ = false;
	                                    }
	                                    else
	                                    {
	                                        �������.���������_�������� = 0;
	                                    }
	                                }
	                            }
	                            else if (((�������.�������� == 0.0) && �������.�����_��������_�������) && ((�������.��������_���.�������_�����.���������_������.Length > 1) && (�������.��������_���.����������_����������_��_������ > (�������.��������_���.�������_�����.����� - 8.0))))
	                            {
	                                if (KeyState[Key.LeftArrow])
	                                {
	                                    �������.��������_���.�������_�����.���������_����� = 0;
	                                }
	                                if (KeyState[Key.RightArrow])
	                                {
	                                    �������.��������_���.�������_�����.���������_����� = 1;
	                                }
	                            }
	                            if (KeyState[Key.Q])
	                            {
	                                �������.���������_������������ = !�������.���������_������������;
	                            }
	                        }
	                    }
	                    else
	                    {
		                    FilteredJoystickState state5 = FJStatesArray[current_joystick];
		                    JoystickState state6 = JStatesArray[current_joystick];
		                    int num16 = state6.GetPointOfViewControllers()[0];//.GetPointOfView()[0];
		                    if (num16 >= 0)
		                    {
		                        num16 = (int) Math.Round((double) ((num16 * 1.0) / 4500.0));
		                    }
		                    bool flag1 = state5[6];
		                    if (state5[5])
		                    {
		                        �������.����������.�������������� = !�������.����������.��������������;
		                        �������.����������.������ = !�������.����������.������;
		                    }
		                    if (!�������.����������.������)
		                    {
		                        goto Label_0E1A;
		                    }
		                    if (state5[11])
		                    {
		                        �������.������� = !�������.�������;
		                    }
		                    if (state5[2])
		                    {
		                        if (�������.�����������.������)
		                        {
		                            �������.�����������.�����������(���.�����������������2);
	                                if (�������.�����������.������ != null)
	                                {
	                                   	�������.�����������.����������� = true;
	                                }
		                        }
		                        else if (�������.�����������.������)
		                        {
		                            �������.�����������.����������� = false;
		                        }
		                    }
		                    if (state5[0])
		                    {
		                        if (!�������.�����_��������_�������)
		                        {
		                            �������.��������������������(false);
		                        }
		                        else if (!�������.�����_��������_�������)
		                        {
		                            �������.��������������������(true);
		                        }
		                    }
		                    if (state5[1])
		                    {
		                        if (!�������.�����_�������)
		                        {
		                            �������.������������(false);
		                        }
		                        else if (!�������.�����_�������)
		                        {
		                            �������.������������(true);
		                        }
		                    }
		                    if (�������.�������_���������� is �������_����������.����_�������)
		                    {
		                    	var �������2 = (�������_����������.����_�������) �������.�������_����������;
		                    	switch (((-5 * state6.RotationZ) / num))
		                    	{
		                        	case -5:
		                            	�������2.�������_����������� = -5;
		                            	goto Label_0D93;
		
		                        	case -4:
		                            	if (�������2.�������_����������� > -4)
		                            	{
		                            	    �������2.�������_����������� = -4;
		                            	}
		                            	goto Label_0D93;
		
		                        	case -3:
		                            	if (�������2.�������_����������� > -3)
		                            	{
		                            	    �������2.�������_����������� = -3;
		                            	}
		                            	goto Label_0D93;
		
		                        	case -2:
		                            	if (�������2.�������_����������� > -2)
		                            	{
		                            	    �������2.�������_����������� = -2;
		                            	}
		                            	goto Label_0D93;
		
		                        	case -1:
		                            	if (�������2.�������_����������� > -1)
		                            	{
		                            	    �������2.�������_����������� = -1;
		                            	}
		                            	goto Label_0D93;
		
		                        	case 0:
		                            	if (state6.RotationZ <= 0)
		                            	{
		                            	    break;
		                            	}
		                            	if (�������2.�������_����������� > 0)
		                            	{
		                            	    �������2.�������_����������� = 0;
		                            	}
		                            	goto Label_0D93;
		
		                        	case 1:
		                            	if (�������2.�������_����������� < 1)
		                            	{
		                            	    �������2.�������_����������� = 1;
		                            	}
		                            	goto Label_0D93;
		
		                        	case 2:
		                            	if (�������2.�������_����������� < 1)
		                            	{
		                            	    �������2.�������_����������� = 1;
		                            	}
		                            	goto Label_0D93;
		
		                        	case 3:
		                            	if (�������2.�������_����������� < 2)
		                            	{
		                            	    �������2.�������_����������� = 2;
		                            	}
		                            	goto Label_0D93;
		
		                        	case 4:
		                            	if (�������2.�������_����������� < 3)
		                            	{
		                                	�������2.�������_����������� = 3;
		                            	}
		                            	goto Label_0D93;
		
		                   	     	case 5:
		                   	         	�������2.�������_����������� = 4;
		                    	        goto Label_0D93;
		
		                    	    default:
		                    	        goto Label_0D93;
		                    	}
		                    	if ((state6.RotationZ < 0) && (�������2.�������_����������� < 0))
		                    	{
		                    	    �������2.�������_����������� = 0;
		                    	}
		                    	Label_0D93:
		                    	if ((state5[7] && (�������.�������� == 0.0)) && (�������2.�������_����������� == 0))
		                    	{
		                    	    �������2.�������_��������� = -�������2.�������_���������;
		                    	}
		                    }
		                    switch (num16)
		                    {
		                        case 0:
		                            �������.���������_�������� = 0;
		                            �������.���������_������������ = false;
		                            break;
		
		                        case 2:
		                            �������.���������_�������� = 1;
		                            �������.���������_������������ = false;
		                            break;
		
		                        case 4:
		                            �������.���������_�������� = 0;
		                            �������.���������_������������ = true;
		                            break;
		
		                        case 6:
		                            �������.���������_�������� = -1;
		                            �������.���������_������������ = false;
		                            goto Label_0E1A;
		                    }
	                    }
                    }
                    Label_0E1A:;
                    if (�����.����������������� is ����������)
                    {
                        ���������� ���������� = (����������) �����.�����������������;
                        if (current_joystick == -1)//(�����.inputGuid == SystemGuid.Keyboard)
                        {
                            if (����������.����������.������)
                            {
                                if (����������.������.Length == 2)
                                {
	                                if (KeyState[Key.T])
	                                {
	                                    if (����������.������[0].������� && ����������.������[1].�������)
	                                    {
	                                        ����������.������[0].�����������(this.���.�����������������);
	                                        if (����������.������[0].������ != null)
	                                        {
	                                            ����������.������[0].����������� = true;
	                                        }
	                                        ����������.������[1].�����������(this.���.�����������������);
	                                        if (����������.������[1].������ != null)
	                                        {
	                                            ����������.������[1].����������� = true;
	                                        }
	                                    }
	                                    else
	                                    {
	                                        ����������.������[0].����������� = false;
	                                        ����������.������[1].����������� = false;
	                                    }
	                                }
                                }
                                if (����������.�������_���������� is �������_����������.����_����������)
                                {
                                    �������_����������.����_���������� ����������2 = (�������_����������.����_����������) ����������.�������_����������;
                                    if ((KeyState[Key.Backspace] && (����������.�������� == 0.0)) && (����������2.�������_����������� == 0))
                                    {
                                        ����������2.�������_��������� = -����������2.�������_���������;
                                    }
                                    if (KeyState[Key.LeftAlt] && (����������2.�������_����������� != 0)&& (����������.�������� >= 0))
                                    {
                                           ����������2.�������_����������� = 0;
                                           ����������2.��������������_������ = 0.0;
                                            }
                                   
                                    if (KeyState.IsDirtyPressed(Key.DownArrow))//[Key.DownArrow])
                                    {
                                        if ((����������2.��������������_������ > 0.0) && (����������2.��������������_������ < 1.0))
                                        {
                                            ����������2.��������������_������ += 0.05;
                                        }
                                    }
                                    else if (KeyState.IsDirtyPressed(Key.UpArrow) && (����������2.��������������_������ > 0.0))
                                    {
                                        ����������2.��������������_������ -= 0.05;
                                        if (����������2.��������������_������ < 0.0)
                                        {
                                            ����������2.��������������_������ = 0.0;
                                        }
                                    }
                                    if (KeyState[Key.DownArrow])//[Key.DownArrow] [Key.UpArrow]
                                    {
                                        if (����������2.�������_����������� > ����������2.�������_min)
                                        {
                                            ����������2.�������_�����������--;
                                        }
                                        else if (����������2.��������������_������ == 0.0)
                                        {
                                            ����������2.��������������_������ = 0.05;
                                        }
                                    }
                                    else if ((KeyState[Key.UpArrow] && (����������2.�������_����������� < ����������2.�������_max)) && (����������2.��������������_������ == 0.0))
                                    {
                                        ����������2.�������_�����������++;
                                    }
                                    if ((KeyState[Key.O]) && (����������.�� != null))
                                    {
                                    	����������.��.������� = !����������.��.�������;
                                    }
                                }
                                if (����������.�������_���������� is �������_����������.��_����)
                                {
                                    �������_����������.��_���� ���� = (�������_����������.��_����) ����������.�������_����������;
                                    if ((KeyState[Key.Z] && (����.����� > 0)) && (((����.�������_����� != "R") && (����.�������_����� != "N")) || ((����.���������_������� == -1.0) && (����������.�������� == 0.0))))
                                    {
                                        ����.�����--;
                                    }
                                    if ((KeyState[Key.X] && (����.����� < (����.������.Length - 1))) && (((����.�������_����� != "P") && (����.�������_����� != "N")) || ((����.���������_������� == -1.0) && (����������.�������� == 0.0))))
                                    {
                                        ����.�����++;
                                    }
                                    if (KeyState[Key.DownArrow])
                                    {
                                        if (����.���������_������� == 0.0)
                                        {
                                            ����.���������_������� = (-World.������������� * 5.0) / 3.0;
                                        }
                                    }
                                    else if (KeyState[Key.UpArrow] && (����.���������_������� == 0.0))
                                    {
                                        ����.���������_������� = (World.������������� * 5.0) / 3.0;
                                    }
                                    if (KeyState.IsDirtyPressed(Key.DownArrow))
                                    {
                                        if (����.���������_������� < 0.0)
                                        {
                                            ����.���������_������� -= (World.������������� * 5.0) / 3.0;
                                            if (����.���������_������� < -1.0)
                                            {
                                                ����.���������_������� = -1.0;
                                            }
                                        }
                                        if (����.���������_������� > 0.0)
                                        {
                                            ����.���������_������� -= (World.������������� * 5.0) / 3.0;
                                            if (����.���������_������� < 0.0)
                                            {
                                                ����.���������_������� = 0.0;
                                                KeyState.keyticks[0xd0] = 1;
                                            }
                                        }
                                    }
                                    
                                    else if (KeyState.IsDirtyPressed(Key.UpArrow))
                                    {
                                        if (����.���������_������� > 0.0)
                                        {
                                            ����.���������_������� += (World.������������� * 5.0) / 3.0;
                                            if (����.���������_������� > 1.0)
                                            {
                                                ����.���������_������� = 1.0;
                                            }
                                        }
                                        if (����.���������_������� < 0.0)
                                        {
                                            ����.���������_������� += (World.������������� * 5.0) / 3.0;
                                            if (����.���������_������� > 0.0)
                                            {
                                                ����.���������_������� = 0.0;
                                                KeyState.keyticks[200] = 1;
                                            }
                                        }
                                    }
                                }
                                if (����������.�������_���������� is �������_����������.��_����1)
                                {
                                    �������_����������.��_����1 ���� = (�������_����������.��_����1) ����������.�������_����������;
                                    if (KeyState[Key.Z])
                                    {
                                        ����.��������--;
                                    }
                                    if (KeyState[Key.X])
                                    {
                                        ����.��������++;
                                    }
                                    if (KeyState[Key.LeftAlt])
                                    {
                                        ����.���������_������� = 0.0;
                                    }
                                    /*if ((KeyState[Key.Z] && (����.����� > 0)) && (((����.�������_����� != "R") && (����.�������_����� != "N")) || ((����.���������_������� == -1.0) && (����������.�������� == 0.0))))
                                    {
                                        ����.�����--;
                                    }
                                    if ((KeyState[Key.X] && (����.����� < (����.������.Length - 1))) && (((����.�������_����� != "P") && (����.�������_����� != "N")) || ((����.���������_������� == -1.0) && (����������.�������� == 0.0))))
                                    {
                                        ����.�����++;
                                    }*/
                                    if (KeyState[Key.DownArrow])
                                    {
                                        if (����.���������_������� == 0.0)
                                        {
                                            ����.���������_������� = (-World.������������� * 5.0) / 3.0;
                                        }
                                    }
                                    else if (KeyState[Key.UpArrow] && (����.���������_������� == 0.0))
                                    {
                                        ����.���������_������� = (World.������������� * 5.0) / 3.0;
                                    }
                                    if (KeyState.IsDirtyPressed(Key.DownArrow))
                                    {
                                        if (����.���������_������� < 0.0)
                                        {
                                            ����.���������_������� -= (World.������������� * 5.0) / 3.0;
                                            if (����.���������_������� < -1.0)
                                            {
                                                ����.���������_������� = -1.0;
                                            }
                                        }
                                        if (����.���������_������� > 0.0)
                                        {
                                            ����.���������_������� -= (World.������������� * 5.0) / 3.0;
                                            if (����.���������_������� < 0.0)
                                            {
                                                ����.���������_������� = 0.0;
                                                KeyState.keyticks[0xd0] = 1;
                                            }
                                        }
                                    }
                                    
                                    else if (KeyState.IsDirtyPressed(Key.UpArrow))
                                    {
                                        if (����.���������_������� > 0.0)
                                        {
                                            ����.���������_������� += (World.������������� * 5.0) / 3.0;
                                            if (����.���������_������� > 1.0)
                                            {
                                                ����.���������_������� = 1.0;
                                            }
                                        }
                                        if (����.���������_������� < 0.0)
                                        {
                                            ����.���������_������� += (World.������������� * 5.0) / 3.0;
                                            if (����.���������_������� > 0.0)
                                            {
                                                ����.���������_������� = 0.0;
                                                KeyState.keyticks[200] = 1;
                                            }
                                        }
                                    }
                                }
                                if (!fmouse)
                                {
                                    if (KeyState.IsDirtyPressed(Key.LeftArrow))
	                                {
	                                    ����������.����������� -= 0.3 * World.�������������;
	                                }
                                    if (KeyState.IsDirtyPressed(Key.RightArrow))
	                                {
	                                    ����������.����������� += 0.3 * World.�������������;
	                                }
                                	if (KeyState.IsDirtyPressed(Key.Space) && space)
                                    {
                                	    
                                	   if (����������.����������� < 0)
                                	    {
                                	       ����������.����������� -= 0.3 * World.�������������;
                                	       if (Math.Abs(����������.�����������) > 0.001)
                            {
                                ����������.����������� = 0.0;
                            }
                                	   }
                                	   if (����������.����������� > 0)
                                	    {
                                	       ����������.����������� += 0.3 * World.�������������;
                                	       if (Math.Abs(����������.�����������) > 0.001)
                                	           
                            {
                                ����������.����������� = 0.0;
                            }
                                	   }
                                	   /*
                                	       if (����������.����������� != 0.0)
                                            
                {   
                                	          /* var num6 =
                    if (num6 < -Math.PI)
                    {
                        num6 += Math.PI * 2.0;
                    }
                    if (num6 > Math.PI)
                    {
                        num6 -= Math.PI * 2.0;
                    }
                                       int num7 = Math.Sign((double)(����������.�����������));
                    if (num7 > 0)
                    {
                        if (Game.fmouse)
                        {
                            ����������.����������� += num7 * World.�������������;
                            if (Math.Abs(����������.�����������) < 0.001)
                            {
                                ����������.����������� = 0.0;
                            }
                        }
                        }*/
                                	   
                                	
                     
                                    }
                                }
                                else if (!mouseButtons[0])
                                {
	                                ����������.����������� += x * 0.001;
                                }
                                if (KeyState[Key.Q])
                                {
                                    if (����������.���������_�������� >= 0)
                                    {
                                        ����������.���������_�������� = -1;
                                        ����������.���������_������������ = false;
                                    }
                                    else
                                    {
                                        ����������.���������_�������� = 0;
                                    }
                                }
                                if (KeyState[Key.W])
                                {
                                    if (����������.���������_�������� <= 0)
                                    {
                                        ����������.���������_�������� = 1;
                                        ����������.���������_������������ = false;
                                    }
                                    else
                                    {
                                        ����������.���������_�������� = 0;
                                    }
                                }
                                if (KeyState[Key.E])
                                {
                                    if (����������.���������_������������)
                                    {
                                        ����������.���������_������������ = false;
                                    }
                                    else
                                    {
                                        ����������.���������_������������ = true;
                                        ����������.���������_�������� = 0;
                                    }
                                }
                            }
                        }
                        else
                        {
                                FilteredJoystickState state7 = FJStatesArray[current_joystick];
                                JoystickState state8 = JStatesArray[current_joystick];
                                int num20 = state8.GetPointOfViewControllers()[0];//.GetPointOfView()[0];
                                if (num20 >= 0)
                                {
                                    num20 = (int) Math.Round((double) ((num20 * 1.0) / 4500.0));
                                }
                                if (state7[5])
                                {
                                    ����������.����������.�������������� = !����������.����������.��������������;
                                    ����������.����������.������ = !����������.����������.������;
                                }
                                if (state7[3])
                                {
                                    ����������.��������_���� = !����������.��������_����;
                                }
                                if (����������.����������.������)
                                {
                                    if (state7[11])
                                    {
                                        ����������.������� = !����������.�������;
                                    }
                                    if (state7[2] && (����������.������.Length > 1))
                                    {
                                        if (����������.������[0].������� && ����������.������[1].�������)
                                        {
                                            ����������.������[0].�����������(this.���.�����������������);
                                            if (����������.������[0].������ != null)
                                            {
                                                ����������.������[0].����������� = true;
                                            }
                                            ����������.������[1].�����������(this.���.�����������������);
                                            if (����������.������[1].������ != null)
                                            {
                                                ����������.������[1].����������� = true;
                                            }
                                        }
                                        else
                                        {
                                            ����������.������[0].����������� = false;
                                            ����������.������[1].����������� = false;
                                        }
                                    }
                                    if (state7[0])
                                    {
                                        if (!����������.�����_��������_�������)
                                        {
                                            ����������.��������������������(false);
                                        }
                                        else if (!����������.�����_��������_�������)
                                        {
                                            ����������.��������������������(true);
                                        }
                                    }
                                    if (state7[1])
                                    {
                                        if (!����������.�����_�������)
                                        {
                                            ����������.������������(false);
                                        }
                                        else if (!����������.�����_�������)
                                        {
                                            ����������.������������(true);
                                        }
                                    }
                                    if (!state7[4, false] && !state7[6, false])
                                    {
                                        ����������.����������� += ((0.5 * World.�������������) * state8.X) / ((double) num);
                                    }
                                    double num21 = (-1.0 * state8.RotationZ) / ((double) num);
                                    if (����������.�������_���������� is �������_����������.����_����������)
                                    {
                                        �������_����������.����_���������� ����������3 = (�������_����������.����_����������) ����������.�������_����������;
                                        if (num21 >= -0.6)
                                        {
                                            ����������3.�������_����������� = (int) (4.0 * num21);
                                            ����������3.��������������_������ = 0.0;
                                        }
                                        else
                                        {
                                            ����������3.�������_����������� = -2;
                                            ����������3.��������������_������ = -(num21 + 0.6) / 0.4;
                                        }
                                        if ((state7[7] && (����������.�������� == 0.0)) && (����������3.�������_����������� == 0))
                                        {
                                            ����������3.�������_��������� = -����������3.�������_���������;
                                        }
                                    }
                                    if (����������.�������_���������� is �������_����������.��_����)
                                    {
                                        �������_����������.��_���� ����2 = (�������_����������.��_����) ����������.�������_����������;
                                        ����2.���������_������� = num21;
                                        if ((state7[6] && (����2.����� > 0)) && (((����2.�������_����� != "R") && (����2.�������_����� != "N")) || ((����2.���������_������� == -1.0) && (����������.�������� == 0.0))))
                                        {
                                            ����2.�����--;
                                        }
                                        if ((state7[7] && (����2.����� < (����2.������.Length - 1))) && (((����2.�������_����� != "P") && (����2.�������_����� != "N")) || ((����2.���������_������� == -1.0) && (����������.�������� == 0.0))))
                                        {
                                            ����2.�����++;
                                        }
                                    }
                                    if (����������.�������_���������� is �������_����������.��_����1)
                                    {
                                        �������_����������.��_����1 ����2 = (�������_����������.��_����1) ����������.�������_����������;
                                        ����2.���������_������� = num21;
                                        if ((state7[6] && (����2.��������_������� > 0)) && (((����2.�������_�������� != "R") && (����2.�������_�������� != "N")) || ((����2.���������_������� == 0.0) && (����������.�������� == 0.0))))
                                        {
                                            ����2.��������_�������--;
                                        }
                                        if ((state7[7]) || ((����2.���������_������� == 0.0) && (����������.�������� == 0.0)))
                                        {
                                            ����2.��������_�������++;
                                        }
                                    }
                                    switch (num20)
                                    {
                                        case 0:
                                            ����������.���������_�������� = 0;
                                            ����������.���������_������������ = false;
                                            break;

                                        case 2:
                                            ����������.���������_�������� = 1;
                                            ����������.���������_������������ = false;
                                            break;

                                        case 4:
                                            ����������.���������_�������� = 0;
                                            ����������.���������_������������ = true;
                                            break;

                                        case 6:
                                            ����������.���������_�������� = -1;
                                            ����������.���������_������������ = false;
                                            break;
                                    }
                                }
//                            }
                        }
                    }
                }
            }
            }
            if (NewControl) {
                if (�������)
            {
                foreach (var ����� in ������)
                {
                    if (�����.����������������� != null)
                    {
                        DoublePoint point5 = �����.�����������������.position - �����.cameraPosition.XZPoint;
                        if (point5.Modulus > 200.0)
                        {
                            �����.�����������������.���������� = ����������.��������������;
                            �����.����������������� = null;
                            �����.�������������� = null;
                        }
                    }
                    int current_joystick = -1;
                    for (int k = 0; k < joystickDevices.Length; k++)
                    {
                        if (�����.inputGuid == deviceGuids[k])
                        {
                            current_joystick = k;
                            break;
                        }
                    }
                    if (current_joystick == -1)//(�����.inputGuid == MyDirectInput.Keyboard_Device.Information.InstanceGuid)//SystemGuid.Keyboard)
                    {
//                        byte[] mouseButtons = state.GetMouseButtons();
//                        int x = state.X;
//                        int y = state.Y;
//                        int z = state.Z;
//                        if ((mouseButtons[1] != 0) && (this._lastMouseButtons[1] == 0))
                        if ((mouseButtons[1]) && (!this._lastMouseButtons[1]))
                        {
                            this.�����������(�����);
                        }
                        if (!MyDirect3D.���_������)
                        {
//                            if (mouseButtons[0] == 0)
                            if (!mouseButtons[0])
                            {
                                �����.cameraRotationChange.x -= 0.001 * x;
                                �����.cameraRotationChange.y -= 0.001 * y;
                            }
                            else
                            {
                                DoublePoint point = new DoublePoint(�����.cameraPositionChange.x, �����.cameraPositionChange.z) / new DoublePoint(�����.cameraRotation.x);
                                point.x -= 0.1 * y;
                                point.y -= 0.1 * x;
                                �����.cameraPositionChange.x = (point * new DoublePoint(�����.cameraRotation.x)).x;
                                �����.cameraPositionChange.z = (point * new DoublePoint(�����.cameraRotation.x)).y;
                            }
                            �����.cameraPositionChange.y += 0.001 * z;
                        }
                        else
                        {                            
                            MyDirect3D.������� += 0.001 * z;
                            if (MyDirect3D.������� <= 2.5) MyDirect3D.������� = 2.5;
//                            if (mouseButtons[0] != 0)
                            if (mouseButtons[0])
                            {
                                DoublePoint point = new DoublePoint(�����.cameraPositionChange.x, �����.cameraPositionChange.z);
                                point.x += 0.01 * x;
                                point.y -= 0.01 * y;
                                �����.cameraPositionChange.x = point.x;
                                �����.cameraPositionChange.z = point.y;
                            }
                        }
                        this._lastMouseButtons = mouseButtons;
                    }
                    else
                    {
                        FilteredJoystickState Current_FJState = FJStatesArray[current_joystick];
                        JoystickState Current_JState = JStatesArray[current_joystick];
                        if (Current_FJState[8])
                        {
                            this.�����������(�����);
                        }
                        double num8 = (0.05 * Current_JState.X) / ((double) num);
                        double num9 = (0.02 * Current_JState.Y) / ((double) num);
                        double num10 = (0.05 * Current_JState.Z) / ((double) num);
                        switch (Current_JState.GetPointOfViewControllers()[0])//.GetPointOfView()[0])
                        {
                            case 0:
                                num10 = 0.04;
                                break;

                            case 0x4650:
                                num10 = -0.04;
                                break;

                            default:
                                num10 = 0.0;
                                break;
                        }
                        if (((�����.����������������� != null) && (�����.����������������� is ������������_���������)) && �����.�����������������.����������.������)
                        {
                            if (!Current_FJState[4, false])
                            {
                                int num12 = 6;
                                if (((Transport) �����.�����������������).�������_���������� is �������_����������.����������)
                                {
                                    num12 = 10;
                                }
                                if (Current_FJState[num12, false])
                                {
                                    �����.cameraRotationChange.x -= num8;
                                    �����.cameraRotationChange.y -= num9;
                                }
                            }
                            else
                            {
                                DoublePoint point2 = new DoublePoint(�����.cameraPositionChange.x, �����.cameraPositionChange.z) / new DoublePoint(�����.cameraRotation.x);
                                point2.x -= 10.0 * num9;
                                point2.y -= 10.0 * num8;
                                �����.cameraPositionChange.x = (point2 * new DoublePoint(�����.cameraRotation.x)).x;
                                �����.cameraPositionChange.z = (point2 * new DoublePoint(�����.cameraRotation.x)).y;
                                �����.cameraPositionChange.y += num10;
                            }
                        }
                        else if (!Current_FJState[4, false])
                        {
                            �����.cameraRotationChange.x -= num8;
                            �����.cameraRotationChange.y -= num9;
                        }
                        else
                        {
                            DoublePoint point3 = new DoublePoint(�����.cameraPositionChange.x, �����.cameraPositionChange.z) / new DoublePoint(�����.cameraRotation.x);
                            point3.x -= 10.0 * num9;
                            point3.y -= 10.0 * num8;
                            �����.cameraPositionChange.x = (point3 * new DoublePoint(�����.cameraRotation.x)).x;
                            �����.cameraPositionChange.z = (point3 * new DoublePoint(�����.cameraRotation.x)).y;
                            �����.cameraPositionChange.y += num10;
                        }
                    }
                    /*var _y = ���.GetHeight(�����.cameraPosition.xz_point);
                    if (�����.cameraPosition.y - 0.01 < _y)
                    {
                        �����.cameraPosition.y = _y + 0.01;
                        �����.cameraPositionChange.y = 0;
                    }*/
                    if (�����.����������������� == null) continue;
                    var _transport = (Transport) �����.�����������������;
                    if (current_joystick == -1)//(�����.inputGuid == MyDirectInput.Keyboard_Device.Information.InstanceGuid)//SystemGuid.Keyboard)
                    {
                        if (KeyState[Key.L])
                        {// TODO: ��������� ������ ��������� ��� ����� ����������
                            _transport.����������.�������������� = !_transport.����������.��������������;
                            //_transport.currentStop = _transport.nextStop = null;
                            //_transport.nextStop = _transport.currentStop = null;
                            _transport.stopIndex = 0;
                        }
                        if (KeyState[Key.M])
                        {
                            _transport.����������.������ = !_transport.����������.������;
                            //_transport.currentStop = _transport.nextStop = null;
                            //_transport.nextStop = _transport.currentStop = null;
                            _transport.stopIndex = 0;
                        }
                        if (_transport.����������.������)
                        {
                            if (KeyState[Key.Y])
                            {
                                _transport.������� = !_transport.�������;
                            }
                            if (KeyState[Key.G])
                            {
                                if (!_transport.�����_��������_�������)
                                {
                                    _transport.��������������������(false);
                                }
                                else if (!_transport.�����_��������_�������)
                                {
                                    _transport.��������������������(true);
                                }
                            }
                            if (KeyState[Key.H])
                            {
                                if (!_transport.�����_�������)
                                {
                                    _transport.������������(false);
                                }
                                else if (!_transport.�����_�������)
                                {
                                    _transport.������������(true);
                                }
                            }
                            if (KeyState[Key.D1])
                            {
                                if (!_transport.������������(0))
                                {
                                    _transport.������������(0, false);
                                }
                                else if (!_transport.������������(0))
                                {
                                    _transport.������������(0, true);
                                }
                            }
                            if (KeyState[Key.D2])
                            {
                                if (!_transport.������������(1))
                                {
                                    _transport.������������(1, false);
                                }
                                else if (!_transport.������������(0))
                                {
                                    _transport.������������(1, true);
                                }
                            }
                            if (KeyState[Key.D3])
                            {
                                if (!_transport.������������(2))
                                {
                                    _transport.������������(2, false);
                                }
                                else if (!_transport.������������(0))
                                {
                                    _transport.������������(2, true);
                                }
                            }
                            if (KeyState[Key.D4])
                            {
                                if (!_transport.������������(3))
                                {
                                    _transport.������������(3, false);
                                }
                                else if (!_transport.������������(0))
                                {
                                    _transport.������������(3, true);
                                }
                            }
                            if (KeyState[Key.D5])
                            {
                                if (!_transport.������������(4))
                                {
                                    _transport.������������(4, false);
                                }
                                else if (!_transport.������������(0))
                                {
                                    _transport.������������(4, true);
                                }
                            }
                            if (KeyState[Key.V])
                            {
                                _transport.stand_brake = !_transport.stand_brake;
                            }
                            /*if (state2[Key.E])
                            {
                                if (transport.���������_������������)
                                {
                                    transport.���������_������������ = false;
                                }
                                else
                                {
                                    transport.���������_������������ = true;
                                    transport.���������_�������� = 0;
                                }
                            }*/
                            if (KeyState[Key.F])
                            {
                                _transport.��������_���� = !_transport.��������_����;
                            }
                        }
                        if (�����.�������������� != null)
                        {
                            if (KeyState[Key.C])//KeyState.InputState.IsPressed(Key)
                            {
                                _transport.SetCamera(0, �����);
                            }
                            if (KeyState[Key.F2])
                            {
                                _transport.SetCamera(1, �����);
                            }
                             if (KeyState[Key.F3])
                            {
                                _transport.SetCamera(2, �����);
                            }
                            if (KeyState[Key.F4])
                            {
                                _transport.SetCamera(3, �����);
                            }
                        }
                    }
                    if (�����.����������������� is �������)
                    {
                        ������� ������� = (�������) �����.�����������������;
                        if (current_joystick == -1)//(�����.inputGuid == MyDirectInput.Keyboard_Device.Information.InstanceGuid)//SystemGuid.Keyboard)
                        {
                            if (�������.����������.������)
                            {
                                if (KeyState[Key.T])
                                {
                                    if (�������.�����������.������)
                                    {
                                        �������.�����������.�����������(���.�����������������2);
                                        if (�������.�����������.������ != null)
                                        {
                                            �������.�����������.����������� = true;
                                        }
                                    }
                                    else if (�������.�����������.������)
                                    {
                                        �������.�����������.����������� = false;
                                    }
                                }
                                if (KeyState[Key.D6])
                                {
                                    if (!�������.������������(5))
                                    {
                                        �������.������������(5, false);
                                    }
                                    else if (!�������.������������(5))
                                    {
                                        �������.������������(5, true);
                                    }
                                }
                                if (KeyState[Key.D7])
                                {
                                    if (!�������.������������(6))
                                    {
                                        �������.������������(6, false);
                                    }
                                    else if (!�������.������������(6))
                                    {
                                        �������.������������(6, true);
                                    }
                                }
                                if (KeyState[Key.D8])
                                {
                                    if (!�������.������������(7))
                                    {
                                        �������.������������(7, false);
                                    }
                                    else if (!�������.������������(7))
                                    {
                                        �������.������������(7, true);
                                    }
                                }
                                if (KeyState[Key.D9])
                                {
                                    if (!�������.������������(8))
                                    {
                                        �������.������������(8, false);
                                    }
                                    else if (!�������.������������(8))
                                    {
                                        �������.������������(8, true);
                                    }
                                }
                                if (KeyState[Key.D0])
                                {
                                    if (!�������.������������(9))
                                    {
                                        �������.������������(9, false);
                                    }
                                    else if (!�������.������������(9))
                                    {
                                        �������.������������(9, true);
                                    }
                                }
                                if (�������.�������_���������� is �������_����������.����_�������)
                                {
                                    �������_����������.����_������� �������2 = (�������_����������.����_�������) �������.�������_����������;
                                    if ((KeyState[Key.Backspace] && (�������.�������� == 0.0)) && (�������2.�������_����������� == 0))
                                    {
                                        �������2.�������_��������� = -�������2.�������_���������;
                                    }
                                     //if (KeyState[Key.LeftAlt] && (�������2.�������_����������� != 0))
                                   //{
                                         //  �������2.�������_����������� = 0;
                                         //  �������2.��������������_������ += 0.05;
                                           // }
                                    if (KeyState[Key.S])
                                    {
                                        if (�������2.�������_����������� > �������2.�������_min)
                                        {
                                            �������2.�������_�����������--;
                                        }
                                    }
                                    else if (KeyState[Key.W] && (�������2.�������_����������� < �������2.�������_max))
                                    {
                                        �������2.�������_�����������++;
                                    }
                                }
                                if (!KeyState[Key.RightControl])
                                {
                                    if (KeyState[Key.Q])
                                    {
                                        if (�������.���������_�������� >= 0)
                                        {
                                            �������.���������_�������� = -1;
                                            �������.���������_������������ = false;
                                        }
                                        else
                                        {
                                            �������.���������_�������� = 0;
                                        }
                                    }
                                    if (KeyState[Key.E])
                                    {
                                           if (�������.���������_�������� <= 0)
                                        {
                                            �������.���������_�������� = 1;
                                            �������.���������_������������ = false;
                                        }
                                        else
                                        {
                                            �������.���������_�������� = 0;
                                        }
                                    }
                                }
                                else if (((�������.�������� == 0.0) && �������.�����_��������_�������) && ((�������.��������_���.�������_�����.���������_������.Length > 1) && (�������.��������_���.����������_����������_��_������ > (�������.��������_���.�������_�����.����� - 8.0))))
                                {
                                    if (KeyState[Key.A])
                                    {
                                        �������.��������_���.�������_�����.���������_����� = 0;
                                    }
                                    if (KeyState[Key.D])
                                    {
                                        �������.��������_���.�������_�����.���������_����� = 1;
                                    }
                                }
                                if (KeyState[Key.O])
                                {
                                    �������.���������_������������ = !�������.���������_������������;
                                }
                            }
                        }
                        else
                        {
                            FilteredJoystickState state5 = FJStatesArray[current_joystick];
                            JoystickState state6 = JStatesArray[current_joystick];
                            int num16 = state6.GetPointOfViewControllers()[0];//.GetPointOfView()[0];
                            if (num16 >= 0)
                            {
                                num16 = (int) Math.Round((double) ((num16 * 1.0) / 4500.0));
                            }
                            bool flag1 = state5[6];
                            if (state5[5])
                            {
                                �������.����������.�������������� = !�������.����������.��������������;
                                �������.����������.������ = !�������.����������.������;
                            }
                            if (!�������.����������.������)
                            {
                                goto Label_0E1A;
                            }
                            if (state5[11])
                            {
                                �������.������� = !�������.�������;
                            }
                            if (state5[2])
                            {
                                if (�������.�����������.������)
                                {
                                    �������.�����������.�����������(���.�����������������2);
                                    if (�������.�����������.������ != null)
                                    {
                                           �������.�����������.����������� = true;
                                    }
                                }
                                else if (�������.�����������.������)
                                {
                                    �������.�����������.����������� = false;
                                }
                            }
                            if (state5[0])
                            {
                                if (!�������.�����_��������_�������)
                                {
                                    �������.��������������������(false);
                                }
                                else if (!�������.�����_��������_�������)
                                {
                                    �������.��������������������(true);
                                }
                            }
                            if (state5[1])
                            {
                                if (!�������.�����_�������)
                                {
                                    �������.������������(false);
                                }
                                else if (!�������.�����_�������)
                                {
                                    �������.������������(true);
                                }
                            }
                            if (�������.�������_���������� is �������_����������.����_�������)
                            {
                                var �������2 = (�������_����������.����_�������) �������.�������_����������;
                                switch (((-5 * state6.RotationZ) / num))
                                {
                                    case -5:
                                        �������2.�������_����������� = -5;
                                        goto Label_0D93;
        
                                    case -4:
                                        if (�������2.�������_����������� > -4)
                                        {
                                            �������2.�������_����������� = -4;
                                        }
                                        goto Label_0D93;
        
                                    case -3:
                                        if (�������2.�������_����������� > -3)
                                        {
                                            �������2.�������_����������� = -3;
                                        }
                                        goto Label_0D93;
        
                                    case -2:
                                        if (�������2.�������_����������� > -2)
                                        {
                                            �������2.�������_����������� = -2;
                                        }
                                        goto Label_0D93;
        
                                    case -1:
                                        if (�������2.�������_����������� > -1)
                                        {
                                            �������2.�������_����������� = -1;
                                        }
                                        goto Label_0D93;
        
                                    case 0:
                                        if (state6.RotationZ <= 0)
                                        {
                                            break;
                                        }
                                        if (�������2.�������_����������� > 0)
                                        {
                                            �������2.�������_����������� = 0;
                                        }
                                        goto Label_0D93;
        
                                    case 1:
                                        if (�������2.�������_����������� < 1)
                                        {
                                            �������2.�������_����������� = 1;
                                        }
                                        goto Label_0D93;
        
                                    case 2:
                                        if (�������2.�������_����������� < 1)
                                        {
                                            �������2.�������_����������� = 1;
                                        }
                                        goto Label_0D93;
        
                                    case 3:
                                        if (�������2.�������_����������� < 2)
                                        {
                                            �������2.�������_����������� = 2;
                                        }
                                        goto Label_0D93;
        
                                    case 4:
                                        if (�������2.�������_����������� < 3)
                                        {
                                            �������2.�������_����������� = 3;
                                        }
                                        goto Label_0D93;
        
                                        case 5:
                                            �������2.�������_����������� = 4;
                                        goto Label_0D93;
        
                                    default:
                                        goto Label_0D93;
                                }
                                if ((state6.RotationZ < 0) && (�������2.�������_����������� < 0))
                                {
                                    �������2.�������_����������� = 0;
                                }
                                Label_0D93:
                                if ((state5[7] && (�������.�������� == 0.0)) && (�������2.�������_����������� == 0))
                                {
                                    �������2.�������_��������� = -�������2.�������_���������;
                                }
                            }
                            switch (num16)
                            {
                                case 0:
                                    �������.���������_�������� = 0;
                                    �������.���������_������������ = false;
                                    break;
        
                                case 2:
                                    �������.���������_�������� = 1;
                                    �������.���������_������������ = false;
                                    break;
        
                                case 4:
                                    �������.���������_�������� = 0;
                                    �������.���������_������������ = true;
                                    break;
        
                                case 6:
                                    �������.���������_�������� = -1;
                                    �������.���������_������������ = false;
                                    goto Label_0E1A;
                            }
                        }
                    }
                    Label_0E1A:;
                    if (�����.����������������� is ����������)
                    {
                        ���������� ���������� = (����������) �����.�����������������;
                        if (current_joystick == -1)//(�����.inputGuid == SystemGuid.Keyboard)
                        {
                            if (����������.����������.������)
                            {
                                if (����������.������.Length == 2)
                                {
                                    if (KeyState[Key.T])
                                    {
                                        if (����������.������[0].������� && ����������.������[1].�������)
                                        {
                                            ����������.������[0].�����������(this.���.�����������������);
                                            if (����������.������[0].������ != null)
                                            {
                                                ����������.������[0].����������� = true;
                                            }
                                            ����������.������[1].�����������(this.���.�����������������);
                                            if (����������.������[1].������ != null)
                                            {
                                                ����������.������[1].����������� = true;
                                            }
                                        }
                                        else
                                        {
                                            ����������.������[0].����������� = false;
                                            ����������.������[1].����������� = false;
                                        }
                                    }
                                }
                                if (����������.�������_���������� is �������_����������.����_����������)
                                {
                                    �������_����������.����_���������� ����������2 = (�������_����������.����_����������) ����������.�������_����������;
                                    if ((KeyState[Key.Backspace] && (����������.�������� == 0.0)) && (����������2.�������_����������� == 0))
                                    {
                                        ����������2.�������_��������� = -����������2.�������_���������;
                                    }
                                    if (KeyState[Key.LeftAlt] && (����������2.�������_����������� != 0)&& (����������.�������� >= 0))
                                    {
                                           ����������2.�������_����������� = 0;
                                           ����������2.��������������_������ = 0.0;
                                            }
                                   
                                    if (KeyState.IsDirtyPressed(Key.S))//[Key.DownArrow])
                                    {
                                        if ((����������2.��������������_������ > 0.0) && (����������2.��������������_������ < 1.0))
                                        {
                                            ����������2.��������������_������ += 0.05;
                                        }
                                    }
                                    else if (KeyState.IsDirtyPressed(Key.W) && (����������2.��������������_������ > 0.0))
                                    {
                                        ����������2.��������������_������ -= 0.05;
                                        if (����������2.��������������_������ < 0.0)
                                        {
                                            ����������2.��������������_������ = 0.0;
                                        }
                                    }
                                    if (KeyState[Key.S])//[Key.DownArrow] [Key.UpArrow]
                                    {
                                        if (����������2.�������_����������� > ����������2.�������_min)
                                        {
                                            ����������2.�������_�����������--;
                                        }
                                        else if (����������2.��������������_������ == 0.0)
                                        {
                                            ����������2.��������������_������ = 0.05;
                                        }
                                    }
                                    else if ((KeyState[Key.W] && (����������2.�������_����������� < ����������2.�������_max)) && (����������2.��������������_������ == 0.0))
                                    {
                                        ����������2.�������_�����������++;
                                    }
                                    if ((KeyState[Key.P]) && (����������.�� != null))
                                    {
                                        ����������.��.������� = !����������.��.�������;
                                    }
                                }
                                if (����������.�������_���������� is �������_����������.��_����)
                                {
                                    �������_����������.��_���� ���� = (�������_����������.��_����) ����������.�������_����������;
                                    if ((KeyState[Key.Z] && (����.����� > 0)) && (((����.�������_����� != "R") && (����.�������_����� != "N")) || ((����.���������_������� == -1.0) && (����������.�������� == 0.0))))
                                    {
                                        ����.�����--;
                                    }
                                    if ((KeyState[Key.X] && (����.����� < (����.������.Length - 1))) && (((����.�������_����� != "P") && (����.�������_����� != "N")) || ((����.���������_������� == -1.0) && (����������.�������� == 0.0))))
                                    {
                                        ����.�����++;
                                    }
                                    if (KeyState[Key.S])
                                    {
                                        if (����.���������_������� == 0.0)
                                        {
                                            ����.���������_������� = (-World.������������� * 5.0) / 3.0;
                                        }
                                    }
                                    else if (KeyState[Key.W] && (����.���������_������� == 0.0))
                                    {
                                        ����.���������_������� = (World.������������� * 5.0) / 3.0;
                                    }
                                    if (KeyState.IsDirtyPressed(Key.S))
                                    {
                                        if (����.���������_������� < 0.0)
                                        {
                                            ����.���������_������� -= (World.������������� * 5.0) / 3.0;
                                            if (����.���������_������� < -1.0)
                                            {
                                                ����.���������_������� = -1.0;
                                            }
                                        }
                                        if (����.���������_������� > 0.0)
                                        {
                                            ����.���������_������� -= (World.������������� * 5.0) / 3.0;
                                            if (����.���������_������� < 0.0)
                                            {
                                                ����.���������_������� = 0.0;
                                                KeyState.keyticks[0xd0] = 1;
                                            }
                                        }
                                    }
                                    
                                    else if (KeyState.IsDirtyPressed(Key.W))
                                    {
                                        if (����.���������_������� > 0.0)
                                        {
                                            ����.���������_������� += (World.������������� * 5.0) / 3.0;
                                            if (����.���������_������� > 1.0)
                                            {
                                                ����.���������_������� = 1.0;
                                            }
                                        }
                                        if (����.���������_������� < 0.0)
                                        {
                                            ����.���������_������� += (World.������������� * 5.0) / 3.0;
                                            if (����.���������_������� > 0.0)
                                            {
                                                ����.���������_������� = 0.0;
                                                KeyState.keyticks[200] = 1;
                                            }
                                        }
                                    }
                                }
                                if (����������.�������_���������� is �������_����������.��_����1)
                                {
                                    �������_����������.��_����1 ���� = (�������_����������.��_����1) ����������.�������_����������;
                                    if (KeyState[Key.Z])
                                    {
                                        ����.��������--;
                                    }
                                    if (KeyState[Key.X])
                                    {
                                        ����.��������++;
                                    }
                                    if (KeyState[Key.LeftAlt])
                                    {
                                        ����.�������� = 0;
                                    }
                                    /*if ((KeyState[Key.Z] && (����.����� > 0)) && (((����.�������_����� != "R") && (����.�������_����� != "N")) || ((����.���������_������� == -1.0) && (����������.�������� == 0.0))))
                                    {
                                        ����.�����--;
                                    }
                                    if ((KeyState[Key.X] && (����.����� < (����.������.Length - 1))) && (((����.�������_����� != "P") && (����.�������_����� != "N")) || ((����.���������_������� == -1.0) && (����������.�������� == 0.0))))
                                    {
                                        ����.�����++;
                                    }*/
                                    if (KeyState[Key.S])
                                    {
                                        if (����.���������_������� == 0.0)
                                        {
                                            ����.���������_������� = (-World.������������� * 5.0) / 3.0;
                                        }
                                    }
                                    else if (KeyState[Key.W] && (����.���������_������� == 0.0))
                                    {
                                        ����.���������_������� = (World.������������� * 5.0) / 3.0;
                                    }
                                    if (KeyState.IsDirtyPressed(Key.S))
                                    {
                                        if (����.���������_������� < 0.0)
                                        {
                                            ����.���������_������� -= (World.������������� * 5.0) / 3.0;
                                            if (����.���������_������� < -1.0)
                                            {
                                                ����.���������_������� = -1.0;
                                            }
                                        }
                                        if (����.���������_������� > 0.0)
                                        {
                                            ����.���������_������� -= (World.������������� * 5.0) / 3.0;
                                            if (����.���������_������� < 0.0)
                                            {
                                                ����.���������_������� = 0.0;
                                                KeyState.keyticks[0xd0] = 1;
                                            }
                                        }
                                    }
                                    
                                    else if (KeyState.IsDirtyPressed(Key.W))
                                    {
                                        if (����.���������_������� > 0.0)
                                        {
                                            ����.���������_������� += (World.������������� * 5.0) / 3.0;
                                            if (����.���������_������� > 1.0)
                                            {
                                                ����.���������_������� = 1.0;
                                            }
                                        }
                                        if (����.���������_������� < 0.0)
                                        {
                                            ����.���������_������� += (World.������������� * 5.0) / 3.0;
                                            if (����.���������_������� > 0.0)
                                            {
                                                ����.���������_������� = 0.0;
                                                KeyState.keyticks[200] = 1;
                                            }
                                        }
                                    }
                                }
                                if (!fmouse)
                                {
                                    if (KeyState.IsDirtyPressed(Key.A))
                                    {
                                        ����������.����������� -= 0.3 * World.�������������;
                                    }
                                    if (KeyState.IsDirtyPressed(Key.D))
                                    {
                                        ����������.����������� += 0.3 * World.�������������;
                                    }
                                    if (KeyState.IsDirtyPressed(Key.Space) && space)
                                    {
                                        
                                       if (����������.����������� < 0)
                                        {
                                           ����������.����������� -= 0.3 * World.�������������;
                                           if (Math.Abs(����������.�����������) > 0.001)
                            {
                                ����������.����������� = 0.0;
                            }
                                       }
                                       if (����������.����������� > 0)
                                        {
                                           ����������.����������� += 0.3 * World.�������������;
                                           if (Math.Abs(����������.�����������) > 0.001)
                                               
                            {
                                ����������.����������� = 0.0;
                            }
                                       }
                                       /*
                                           if (����������.����������� != 0.0)
                                            
                {   
                                              /* var num6 =
                    if (num6 < -Math.PI)
                    {
                        num6 += Math.PI * 2.0;
                    }
                    if (num6 > Math.PI)
                    {
                        num6 -= Math.PI * 2.0;
                    }
                                       int num7 = Math.Sign((double)(����������.�����������));
                    if (num7 > 0)
                    {
                        if (Game.fmouse)
                        {
                            ����������.����������� += num7 * World.�������������;
                            if (Math.Abs(����������.�����������) < 0.001)
                            {
                                ����������.����������� = 0.0;
                            }
                        }
                        }*/
                                       
                                    
                     
                                    }
                                }
                                else if (!mouseButtons[0])
                                {
                                    ����������.����������� += x * 0.001;
                                }
                                if (KeyState[Key.Q])
                                {
                                    if (����������.���������_�������� >= 0)
                                    {
                                        ����������.���������_�������� = -1;
                                        ����������.���������_������������ = false;
                                    }
                                    else
                                    {
                                        ����������.���������_�������� = 0;
                                    }
                                }
                                if (KeyState[Key.E])
                                {
                                    if (����������.���������_�������� <= 0)
                                    {
                                        ����������.���������_�������� = 1;
                                        ����������.���������_������������ = false;
                                    }
                                    else
                                    {
                                        ����������.���������_�������� = 0;
                                    }
                                }
                                if (KeyState[Key.O])
                                {
                                    if (����������.���������_������������)
                                    {
                                        ����������.���������_������������ = false;
                                    }
                                    else
                                    {
                                        ����������.���������_������������ = true;
                                        ����������.���������_�������� = 0;
                                    }
                                }
                            }
                        }
                        else
                        {
                                FilteredJoystickState state7 = FJStatesArray[current_joystick];
                                JoystickState state8 = JStatesArray[current_joystick];
                                int num20 = state8.GetPointOfViewControllers()[0];//.GetPointOfView()[0];
                                if (num20 >= 0)
                                {
                                    num20 = (int) Math.Round((double) ((num20 * 1.0) / 4500.0));
                                }
                                if (state7[5])
                                {
                                    ����������.����������.�������������� = !����������.����������.��������������;
                                    ����������.����������.������ = !����������.����������.������;
                                }
                                if (state7[3])
                                {
                                    ����������.��������_���� = !����������.��������_����;
                                }
                                if (����������.����������.������)
                                {
                                    if (state7[11])
                                    {
                                        ����������.������� = !����������.�������;
                                    }
                                    if (state7[2] && (����������.������.Length > 1))
                                    {
                                        if (����������.������[0].������� && ����������.������[1].�������)
                                        {
                                            ����������.������[0].�����������(this.���.�����������������);
                                            if (����������.������[0].������ != null)
                                            {
                                                ����������.������[0].����������� = true;
                                            }
                                            ����������.������[1].�����������(this.���.�����������������);
                                            if (����������.������[1].������ != null)
                                            {
                                                ����������.������[1].����������� = true;
                                            }
                                        }
                                        else
                                        {
                                            ����������.������[0].����������� = false;
                                            ����������.������[1].����������� = false;
                                        }
                                    }
                                    if (state7[0])
                                    {
                                        if (!����������.�����_��������_�������)
                                        {
                                            ����������.��������������������(false);
                                        }
                                        else if (!����������.�����_��������_�������)
                                        {
                                            ����������.��������������������(true);
                                        }
                                    }
                                    if (state7[1])
                                    {
                                        if (!����������.�����_�������)
                                        {
                                            ����������.������������(false);
                                        }
                                        else if (!����������.�����_�������)
                                        {
                                            ����������.������������(true);
                                        }
                                    }
                                    if (!state7[4, false] && !state7[6, false])
                                    {
                                        ����������.����������� += ((0.5 * World.�������������) * state8.X) / ((double) num);
                                    }
                                    double num21 = (-1.0 * state8.RotationZ) / ((double) num);
                                    if (����������.�������_���������� is �������_����������.����_����������)
                                    {
                                        �������_����������.����_���������� ����������3 = (�������_����������.����_����������) ����������.�������_����������;
                                        if (num21 >= -0.6)
                                        {
                                            ����������3.�������_����������� = (int) (4.0 * num21);
                                            ����������3.��������������_������ = 0.0;
                                        }
                                        else
                                        {
                                            ����������3.�������_����������� = -2;
                                            ����������3.��������������_������ = -(num21 + 0.6) / 0.4;
                                        }
                                        if ((state7[7] && (����������.�������� == 0.0)) && (����������3.�������_����������� == 0))
                                        {
                                            ����������3.�������_��������� = -����������3.�������_���������;
                                        }
                                    }
                                    if (����������.�������_���������� is �������_����������.��_����)
                                    {
                                        �������_����������.��_���� ����2 = (�������_����������.��_����) ����������.�������_����������;
                                        ����2.���������_������� = num21;
                                        if ((state7[6] && (����2.����� > 0)) && (((����2.�������_����� != "R") && (����2.�������_����� != "N")) || ((����2.���������_������� == -1.0) && (����������.�������� == 0.0))))
                                        {
                                            ����2.�����--;
                                        }
                                        if ((state7[7] && (����2.����� < (����2.������.Length - 1))) && (((����2.�������_����� != "P") && (����2.�������_����� != "N")) || ((����2.���������_������� == -1.0) && (����������.�������� == 0.0))))
                                        {
                                            ����2.�����++;
                                        }
                                    }
                                    if (����������.�������_���������� is �������_����������.��_����1)
                                    {
                                        �������_����������.��_����1 ����2 = (�������_����������.��_����1) ����������.�������_����������;
                                        ����2.���������_������� = num21;
                                        if ((state7[6] && (����2.��������_������� > 0)) && (((����2.�������_�������� != "R") && (����2.�������_�������� != "N")) || ((����2.���������_������� == 0.0) && (����������.�������� == 0.0))))
                                        {
                                            ����2.��������_�������--;
                                        }
                                        if ((state7[7]) || ((����2.���������_������� == 0.0) && (����������.�������� == 0.0)))
                                        {
                                            ����2.��������_�������++;
                                        }
                                    }
                                    switch (num20)
                                    {
                                        case 0:
                                            ����������.���������_�������� = 0;
                                            ����������.���������_������������ = false;
                                            break;

                                        case 2:
                                            ����������.���������_�������� = 1;
                                            ����������.���������_������������ = false;
                                            break;

                                        case 4:
                                            ����������.���������_�������� = 0;
                                            ����������.���������_������������ = true;
                                            break;

                                        case 6:
                                            ����������.���������_�������� = -1;
                                            ����������.���������_������������ = false;
                                            break;
                                    }
                                }
//                            }
                        }
                    }
                }
            }
            }
            if (KeyState[Key.F1])
            {
            	MainForm.debug = !MainForm.debug;
            }
            if (KeyState[Key.F5])
            {
                //MainForm.IsKeyLocked = MainForm.IsKeyLocked;
                //MainForm.IsMnemonic = MainForm.IsMnemonic;
            }
            if (KeyState[Key.F10])
               {
               var now = DateTime.Now;
                var path = Application.StartupPath + @"\Screenshots\";
                var screenshot = string.Format(@"{0}\Trancity {1:00}-{2:00}-{3} {4:00}-{5:00}-{6:00}-{7:000}.jpg", path, now.Day, now.Month, now.Year, now.Hour, now.Minute, now.Second, now.Millisecond);
                var surface = MyDirect3D.device.GetRenderTarget(0);
                Surface.ToFile(surface, screenshot, ImageFileFormat.Jpg);
               // Surface.ToStream(surface, ImageFileFormat.Jpg);
                surface.Dispose(); 
            }
        }

        public void Render()
        {
            if (MyDirect3D.device == null) return;
            if (MainForm.in_editor) goto Label_new;
            if (MyDirect3D._newDevice.IsDeviceLost) return;
            MyDirect3D._newDevice.BeginScene();
            MyDirect3D.ResetViewports(������.Length);
            MyDirect3D.SetViewport(-1);
            MyDirect3D.device.Clear(ClearFlags.ZBuffer | ClearFlags.Target, 0, 1f, 0);
            if (!�������)
            {
            	menu.Draw();
            	MyDirect3D._newDevice.EndScene();
            	return;
            }
            Label_new:
            for (var i = 0; i < ������.Length; i++)
            {
                MyDirect3D.SetViewport(i);
                MyDirect3D.device.Clear(ClearFlags.ZBuffer | ClearFlags.Target, 0xb4ff, 1f, 0);
                //
                ������[i].cameraPosition.Add(ref ������[i].cameraPositionChange);
                ������[i].cameraPositionChange.Divide(3.0);
                ������[i].cameraRotation.Add(ref ������[i].cameraRotationChange);
                ������[i].cameraRotationChange.Divide(3.0);
                //� ����� ������ ����������? ����������� �������� ������
                if (Math.Abs(������[i].cameraRotation.x) > Math.PI)
                	������[i].cameraRotation.x -= 2.0 * Math.PI * Math.Sign(������[i].cameraRotation.x);
                if (Math.Abs(������[i].cameraRotation.y) > (Math.PI / 2.0))
                	������[i].cameraRotation.y = (Math.PI / 2.0) * Math.Sign(������[i].cameraRotation.y);
                //
                MyDirect3D.SetCameraPos(������[i].cameraPosition, ������[i].cameraRotation);
                //
                col = (int)Math.Floor(������[i].cameraPosition.x / (double)Ground.grid_size);
                row = (int)Math.Floor(������[i].cameraPosition.z / (double)Ground.grid_size);
                //
                MyDirect3D.ComputeFrustum();
                ���.RenderMeshes2();
				���.RenderMeshes();
				MeshObject.RenderList();
				MyDirect3D.Alpha = true;
				���.RenderMeshesA();
				MeshObject.RenderListA();
				MyDirect3D.Alpha = false;
                if (������[i].����������������� != null)
                {
                	var _transport = (Transport) ������[i].�����������������;
                	var speed_str = (_transport.�������� * 3.6).ToString("###0.00");
	                var control_str = "";
	                if (_transport.����������.��������������)
	                {
	                    control_str = _transport.����������.������ ? Localization.current_.ctrl_s : Localization.current_.ctrl_a;
	                }
	                else
	                {
	                    control_str = _transport.����������.������ ? Localization.current_.ctrl_m : "-";
	                }
	                if (MainForm.debug)
	                {
	                    var str111 = "\nCS: " + ((_transport.currentStop != null) ? _transport.currentStop.�������� : "")
	                    	+ "\nNS: " + ((_transport.nextStop != null) ? _transport.nextStop.�������� : "")
	                    	+ "\nSI: " + _transport.stopIndex
	                        + "\n\nX: " + _transport.����������3D.x.ToString("#0.0")
	                        + "\nY: " + _transport.����������3D.y.ToString("#0.0")
	                        + "\nZ: " + _transport.����������3D.z.ToString("#0.0")
	                        + "\nrY: " + (_transport.direction * 180.0 / Math.PI).ToString("#0.0")
	                    	+ "\nrZ: " + (_transport.�����������Y * 180.0 / Math.PI).ToString("#0.0");
	                    Common.MyGUI.default_font.DrawString(null, str111, (int) (420 + MyDirect3D.device.Viewport.X), (int) (15 + MyDirect3D.device.Viewport.Y), Color.Black);
	                }
	                if (_transport is �������)//(������[i].����������������� is �������)
	                {
	                    var ������� = (�������) _transport;//������[i].�����������������;
	                    var str = "-";
	                    if (�������.�������_���������� is �������_����������.����_�������)
	                    {
		                    var �������2 = (�������_����������.����_�������) �������.�������_����������;
		                    switch (�������2.�������_�����������)
		                    {
		                        case -5:
		                            str = Localization.current_.bp;
		                            break;
		
		                        case -4:
		                            str = Localization.current_.b4;
		                            break;
		
		                        case -3:
		                            str = Localization.current_.b3;
		                            break;
		
		                        case -2:
		                            str = Localization.current_.b2;
		                            break;
		
		                        case -1:
		                            str = Localization.current_.b1;
		                            break;
		
		                        case 0:
		                            str = "0";
		                            break;
		
		                        case 1:
		                            str = Localization.current_.m;
		                            break;
		
		                        case 2:
		                            str = Localization.current_.x1;
		                            break;
		
		                        case 3:
		                            str = Localization.current_.x2;
		                            break;
		
		                        case 4:
		                            str = Localization.current_.x3;
		                            break;
		                    }
		                    var str2 = (�������2.�������_��������� == 1) ? Localization.current_.forward : (�������2.�������_��������� == -1) ? Localization.current_.back : "0";
		                    str = str + "\n" + Localization.current_.reverse + ": " + str2;
	                    }
	                    str = str + "\n" + ((�������.�����������.������) ? Localization.current_.tk_on : Localization.current_.tk_off)
	                    	 + "\n" + Localization.current_.parking_brake + " " + (�������.stand_brake ? Localization.current_.enable : Localization.current_.disable);
	                    var str5 = �������.�������.number;
	                    
	                   
	                    if (�������.�_����)
	                    {
	                        str5 = str5 + " (" + Localization.current_.route_in_park + ")";
	                    }
	                    if (�������.����� != null)
	                    {
//	                        var str15 = str5;
	                        str5 = str5 + "\n" + Localization.current_.order + ": " + �������.�����.�������.number + "/" + �������.�����.�����;
	                        if (�������.���� != null)
	                        {
                                    
	                            
	                            if (���.time < �������.����.�����_�����������)
	                            {
	                                str5 = str5 + "\n" + Localization.current_.departure_time + ": " + �������.����.str_�����_�����������;
	                            }
	                            str5 = str5 + "\n" + Localization.current_.arrival_time + ": " + �������.����.str_�����_��������;
	                            if (((�������.����_index < (�������.����.pathes.Length - 1)) && (�������.��������_���.�������_�����.���������_������.Length > 1)) && ((�������.����_index > 0) || (�������.��������_���.�������_����� == �������.����.pathes[0])))
	                            {
	                                var ������ = �������.����.pathes[�������.����_index + 1];
	                                var str6 = Localization.current_.nr_pryamo;
	                                if (������.������)
	                                {
	                                    if (������.���������������0 > 0.0)
	                                    {
	                                        str6 = Localization.current_.nr_right;
	                                    }
	                                    else if (������.���������������0 < 0.0)
	                                    {
	                                        str6 = Localization.current_.nr_left;
	                                    }
	                                }
	                                str5 = str5 + "\n" + Localization.current_.nr + ": " + str6;
	                                
	                            }
	                        }
	                    }
	                    if (�������.nextStop != null)
                        {
                            //str17 = "\n" + Localization.current_.stop  + ((_transport.nextStop != null) ? _transport.nextStop.�������� : "");
                           var str17 = "\n" + Localization.current_.stop  + ((_transport.nextStop != null) ? _transport.nextStop.�������� : "");
                        }
	                    Common.MyGUI.default_font.DrawString(null, Localization.current_.tram_control + ": " + control_str + "\n" + Localization.current_.ctrl_pos + ": " + str + "\n" + Localization.current_.speed + ": " + speed_str + " " + Localization.current_.speed_km +  "\n" + Localization.current_.route + ": " + str5, (int) (15 + MyDirect3D.device.Viewport.X), (int) (15 + MyDirect3D.device.Viewport.Y), Color.Black);
	                }
	                if (_transport is ����������)// (������[i].����������������� is ����������)
	                {
	                    var ���������� = (����������) _transport;//������[i].�����������������;
	                    var str7 = "-";
	                    var str8 = "���������� ���";
	                    if (����������.�������_���������� is �������_����������.����_����������)
	                    {
	                        str8 = Localization.current_.trol_control;
	                        str7 = "\n" + Localization.current_.ctrl_pos + ": ";
	                        var ����������2 = (�������_����������.����_����������) ����������.�������_����������;
	                        switch (����������2.�������_�����������)
	                        {
	                            case -2:
	                                str7 = str7 + Localization.current_.b2;
	                                break;
	
	                            case -1:
	                                str7 = str7 + Localization.current_.b1;
	                                break;
	
	                            case 0:
	                                str7 = str7 + "0";
	                                break;
	
	                            case 1:
	                                str7 = str7 + Localization.current_.m;
	                                break;
	
	                            case 2:
	                                str7 = str7 + Localization.current_.x1;
	                                break;
	
	                            case 3:
	                                str7 = str7 + Localization.current_.x2;
	                                break;
	
	                            case 4:
	                                str7 = str7 + Localization.current_.x3;
	                                break;
	                        }
	                        str7 = str7 + "\n" + Localization.current_.air_brake + ": " + ((����������2.��������������_������ * 100.0)).ToString("0") + "%";
	                        var str9 = (����������2.�������_��������� == 1) ? Localization.current_.forward : (����������2.�������_��������� == -1) ? Localization.current_.back : "0";
	                        str7 = str7 + "\n" + Localization.current_.reverse + ": " + str9;
	                        str7 = str7 + "\n" + ((����������.������_�������) ? Localization.current_.st_on : Localization.current_.st_off);
	                        str7 = str7 + "\n" + Localization.current_.trol + " " + ((����������.�������) ? Localization.current_.enable : Localization.current_.disable);
	                        if (����������.�� != null)
	                        {
	                            //var str99 = (����������2.���������_�� == 1) ? Localization.current_.enable : (����������2.���������_�� == 0) ? Localization.current_.disable : "0";
                                str7 = str7 + "\n" + Localization.current_.ax + " " + ((����������.��.�������) ? Localization.current_.enable : Localization.current_.disable);
	                           // str7 = str7 + "\n" + ((����������.��.�������) ? Localization.current_.enable : Localization.current_.disable);
	                            //var str99 = (����������2.���������_�� == 1)  ? Localization.current_.enable : (����������2.���������_�� == 0)  ? Localization.current_.disable : "0";
	                        	//str7 = str7 + "\n" + Localization.current_.ax + " " + str99;
	                        	str7 = str7 + "\n" + Localization.current_.ax_power + ": " + (����������.��.�������_������� / ����������.��.������_�������).ToString("##0%");
	                        }
	                    }
	                    else if (����������.�������_���������� is �������_����������.��_����)
	                    {
	                        str8 = Localization.current_.bus_control;
	                        var ���� = (�������_����������.��_����) ����������.�������_����������;
	                        str7 = (("\n" + Localization.current_.gmod + ": " + ����.�������_�����) + "\n" + Localization.current_.cur_pos + ": " + ����.�������_��������) + "\n" + Localization.current_.pedal_pos + ": ";
	                        if (����.���������_������� > 0.0)
	                        {
	                            str7 = str7 + Localization.current_.gas + " ";
	                        }
	                        if (����.���������_������� < 0.0)
	                        {
	                            str7 = str7 + Localization.current_.brake + " ";
	                        }
	                        str7 = str7 + ((Math.Abs(����.���������_�������) * 100.0)).ToString("0") + "%"
	                        	+ "\n" + Localization.current_.engine + " " + (����������.������� ? Localization.current_.enable : Localization.current_.disable);
	                    }
	                    else if (����������.�������_���������� is �������_����������.��_����1)
                        {
                            str8 = Localization.current_.auto_control;
                            var ���� = (�������_����������.��_����1) ����������.�������_����������;
                            str7 = ("\n" + Localization.current_.cur_pos + ": " + ����.�������_��������) + "\n" + Localization.current_.pedal_pos + ": ";
                            if (����.���������_������� > 0.0)
                            {
                                str7 = str7 + Localization.current_.gas + " ";
                            }
                            if (����.���������_������� < 0.0)
                            {
                                str7 = str7 + Localization.current_.brake + " ";
                            }
                            str7 = str7 + ((Math.Abs(����.���������_�������) * 100.0)).ToString("0") + "%"
                                + "\n" + Localization.current_.engine + " " + (����������.������� ? Localization.current_.enable : Localization.current_.disable);
                        }
	                    str7 = str7 + "\n" + Localization.current_.parking_brake + " " + (����������.stand_brake ? Localization.current_.enable : Localization.current_.disable);
	                    if (����������.����������� > 0.0)
	                    {
	                        str7 = str7 + "\n" + Localization.current_.sterling + ": " + (((����������.����������� * 180.0) / 3.1415926535897931)).ToString("0") + "\x00b0 " + Localization.current_.ster_r;
	                    }
	                    else if (����������.����������� < 0.0)
	                    {
	                        str7 = str7 + "\n" + Localization.current_.sterling + ": " + (((-����������.����������� * 180.0) / 3.1415926535897931)).ToString("0") + "\x00b0 " + Localization.current_.ster_l;
	                    }
	                    else
	                    {
	                        str7 = str7 + "\n" + Localization.current_.sterling + ": " + Localization.current_.nr_pryamo;
	                    }
	                    var str12 = ����������.�������.number;
	                    
	                    if (����������.�_����)
	                    {
	                        str12 = str12 + " (" + Localization.current_.route_in_park + ")";
	                    }
	                    if (����������.����� != null)
	                    {
	                        var str16 = str12;
	                        str12 = str16 + "\n" + Localization.current_.order + ": " + ����������.�����.�������.number + "/" + ����������.�����.�����;
	                        if (����������.���� != null)
	                        {
	                            if (���.time < ����������.����.�����_�����������)
	                            {
	                                str12 = str12 + "\n" + Localization.current_.departure_time + ": " + ����������.����.str_�����_�����������;
	                            }
	                            str12 = str12 + "\n" + Localization.current_.arrival_time + ": " + ����������.����.str_�����_��������;
	                            if ((((����������.����_index < (����������.����.pathes.Length - 1)) && (����������.���������.������ != null)) && (����������.���������.������.���������������.Length > 1)) && ((����������.����_index > 0) || (����������.���������.������ == ����������.����.pathes[0])))
	                            {
	                                var ������2 = ����������.����.pathes[����������.����_index + 1];
	                                var str13 = Localization.current_.nr_pryamo;
	                                if (������2.������)
	                                {
	                                    if (������2.���������������0 > 0.0)
	                                    {
	                                        str13 = Localization.current_.nr_right;
	                                    }
	                                    else if (������2.���������������0 < 0.0)
	                                    {
	                                        str13 = Localization.current_.nr_left;
	                                    }
	                                }
	                                str12 = str12 + "\n" + Localization.current_.nr + ": " + str13;
	                                
	                            }
	                        }
	                    }
	                    if (����������.nextStop != null)
	                    {
	                        var str17 = "\n" + Localization.current_.stop  + ((_transport.nextStop != null) ? _transport.nextStop.�������� : "");
	                    }
	                    Common.MyGUI.default_font.DrawString(null, str8 + ": " + control_str + str7 + "\n" + Localization.current_.speed + ": " + speed_str + " " + Localization.current_.speed_km + "\n" + Localization.current_.route + ": " + str12, 15 + MyDirect3D.device.Viewport.X, 15 + MyDirect3D.device.Viewport.Y, Color.Black);
	                }
                }
                if (((MyDirect3D.device.Viewport.X + MyDirect3D.device.Viewport.Width) == MyDirect3D.Window_Width) &&
                    (MyDirect3D.device.Viewport.Y == 0))// continue;
                {
                	Common.MyGUI.default_font.DrawString(null, ConvertTime.TimeFromSeconds(���.time % 86400.0), MyDirect3D.Window_Width - 105/*0x69*/, 15, Color.Black);
//                	MyGUI.default_font.DrawString(null, "�����������".PadLeft(27), MyDirect3D.Window_Width - 398, 15, Color.Black);
                }
                if (!MainForm.debug) continue;
                var _str = "\ndTmax: " + World.dtmax.ToString("#0.000") + "\nFPS: " + MyDirect3D._newDevice.FPS.ToString("#00")
                    + "\nNewControl: " + ((NewControl) ? Localization.current_.enable : Localization.current_.disable)
                	+ "\nX: " + MyDirect3D.Camera_Position.x.ToString("#0.0")
	                + "\nY: " + MyDirect3D.Camera_Position.y.ToString("#0.0")
	                + "\nZ: " + MyDirect3D.Camera_Position.z.ToString("#0.0")
	                + "\nrY: " + MyDirect3D.Camera_Rotation.x.ToString("#0.000")
	                + "\nrZ: " + MyDirect3D.Camera_Rotation.y.ToString("#0.000");
                Common.MyGUI.default_font.DrawString(null, _str, new Rectangle(MyDirect3D.Window_Width - 160, 15, 160, 500), DrawTextFormat.Right, Color.Black);
            }
            MyDirect3D._newDevice.EndScene();
        }
        
        public void RenderMain()
        {
            for (var i = 0; i < ������.Length; i++)
            {
            	������[i].cameraPosition.Add(ref ������[i].cameraPositionChange);// += ������[i].cameraPositionChange;
                ������[i].cameraPositionChange.Divide(3.0);// = ������[i].cameraPositionChange / 3.0;
                ������[i].cameraRotation.Add(ref ������[i].cameraRotationChange);// += ������[i].cameraRotationChange;
                if (������[i].cameraRotation.x > Math.PI) ������[i].cameraRotation.x -= Math.PI * 2.0;
                else if (������[i].cameraRotation.x < -Math.PI) ������[i].cameraRotation.x += Math.PI * 2.0;
                ������[i].cameraRotation.y = Math.Min(Math.Max(������[i].cameraRotation.y, -(Math.PI / 2.0)), (Math.PI / 2.0));
                ������[i].cameraRotationChange.Divide(3.0);// = ������[i].cameraRotationChange / 3.0;
                ������[i].excameraPosition = ������[i].cameraPosition;
                ������[i].excameraRotation = ������[i].cameraRotation;
                MyDirect3D.SetCameraPos(������[i].cameraPosition, ������[i].cameraRotation);
                //
                col = (int)Math.Floor(������[i].cameraPosition.x / (double)Ground.grid_size);
                row = (int)Math.Floor(������[i].cameraPosition.z / (double)Ground.grid_size);
                //
                MyDirect3D.ComputeFrustum();
				���.RenderMeshes();
				string whole_info = "";
                if (������[i].����������������� != null)
                {
                	var _transport = (Transport) ������[i].�����������������;
                	var speed_str = (_transport.�������� * 3.6).ToString("###0.00");
	                var control_str = "";
	                if (_transport.����������.��������������)
	                {
	                    control_str = _transport.����������.������ ? Localization.current_.ctrl_s : Localization.current_.ctrl_a;
	                }
	                else
	                {
	                    control_str = _transport.����������.������ ? Localization.current_.ctrl_m : "-";
	                }
	                if (MainForm.debug)
	                {
	                	MyGUI.stringlist[4 + i] = "\nCS: " + ((_transport.currentStop != null) ? _transport.currentStop.�������� : "")
	                    	+ "\nNS: " + ((_transport.nextStop != null) ? _transport.nextStop.�������� : "")
	                    	+ "\nSI: " + _transport.stopIndex
	                        + "\n\nX: " + _transport.����������3D.x.ToString("#0.0")
	                        + "\nY: " + _transport.����������3D.y.ToString("#0.0")
	                        + "\nZ: " + _transport.����������3D.z.ToString("#0.0")
	                        + "\nrY: " + (_transport.direction * 180.0 / Math.PI).ToString("#0.0")
	                    	+ "\nrZ: " + (_transport.�����������Y * 180.0 / Math.PI).ToString("#0.0");
	                }
	                if (_transport is �������)//(������[i].����������������� is �������)
	                {
	                    var ������� = (�������) _transport;//������[i].�����������������;
	                    var str = "-";
	                    if (�������.�������_���������� is �������_����������.����_�������)
	                    {
		                    var �������2 = (�������_����������.����_�������) �������.�������_����������;
		                    switch (�������2.�������_�����������)
		                    {
		                        case -5:
		                            str = "��";
		                            break;
		
		                        case -4:
		                            str = "�4";
		                            break;
		
		                        case -3:
		                            str = "�3";
		                            break;
		
		                        case -2:
		                            str = "�2";
		                            break;
		
		                        case -1:
		                            str = "�1";
		                            break;
		
		                        case 0:
		                            str = "0";
		                            break;
		
		                        case 1:
		                            str = "�";
		                            break;
		
		                        case 2:
		                            str = "�1";
		                            break;
		
		                        case 3:
		                            str = "�2";
		                            break;
		
		                        case 4:
		                            str = "�3";
		                            break;
		                    }
		                    var str2 = (�������2.�������_��������� == 1) ? Localization.current_.forward : (�������2.�������_��������� == -1) ? Localization.current_.back : "0";
		                    str = str + "\n" + Localization.current_.reverse + ": " + str2;
	                    }
	                    str = str + "\n" + ((�������.�����������.������) ? Localization.current_.tk_on : Localization.current_.tk_off)
	                    	 + "\n" + Localization.current_.parking_brake + " " + (�������.stand_brake ? Localization.current_.enable : Localization.current_.disable);
	                    var str5 = �������.�������.number;
	                    var str7 = �������.nextStop;
	                    if (�������.�_����)
	                    {
	                        str5 = str5 + " (" + Localization.current_.route_in_park + ")";
	                    }
	                    if (�������.����� != null)
	                    {
	                        str5 = str5 + "\n" + Localization.current_.order + ": " + �������.�����.�������.number + "/" + �������.�����.�����;
	                        if (�������.���� != null)
	                        {
	                            if (���.time < �������.����.�����_�����������)
	                            {
	                                str5 = str5 + "\n" + Localization.current_.departure_time + ": " + �������.����.str_�����_�����������;
	                            }
	                            str5 = str5 + "\n" + Localization.current_.arrival_time + ": " + �������.����.str_�����_��������;
	                            if (((�������.����_index < (�������.����.pathes.Length - 1)) && (�������.��������_���.�������_�����.���������_������.Length > 1)) && ((�������.����_index > 0) || (�������.��������_���.�������_����� == �������.����.pathes[0])))
	                            {
	                                var ������ = �������.����.pathes[�������.����_index + 1];
	                                var str6 = Localization.current_.nr_pryamo;
	                                if (������.������)
	                                {
	                                    if (������.���������������0 > 0.0)
	                                    {
	                                        str6 = Localization.current_.nr_right;
	                                    }
	                                    else if (������.���������������0 < 0.0)
	                                    {
	                                        str6 = Localization.current_.nr_left;
	                                    }
	                                }
	                                str5 = str5 + "\n" + Localization.current_.nr + ": " + str6;
	                                
	                            }
	                        }
	                    }
	                    //str7 = "\nNS: "(�������.nextStop.��������);
	                    whole_info = Localization.current_.tram_control + ": " + control_str + "\n" + Localization.current_.ctrl_pos + ": " + str + "\n" + Localization.current_.speed + ": " + speed_str + " " + Localization.current_.speed_km +  "\n" + Localization.current_.route + ": " + str5 + str7;
	                }
	                if (_transport is ����������)
	                {
	                    var ���������� = (����������) _transport;
	                    var str7 = "-";
	                    var str8 = "���������� ���";
	                    if (����������.�������_���������� is �������_����������.����_����������)
	                    {
	                        str8 = Localization.current_.trol_control;
	                        str7 = "\n" + Localization.current_.ctrl_pos + ": ";
	                        var �������������_�� = (�������_����������.����_����������) ����������.�������_����������;
	                        switch (�������������_��.�������_�����������)
	                        {
	                            case -2:
	                                str7 = str7 + "�2";
	                                break;
	
	                            case -1:
	                                str7 = str7 + "�1";
	                                break;
	
	                            case 0:
	                                str7 = str7 + "0";
	                                break;
	
	                            case 1:
	                                str7 = str7 + "�";
	                                break;
	
	                            case 2:
	                                str7 = str7 + "�1";
	                                break;
	
	                            case 3:
	                                str7 = str7 + "�2";
	                                break;
	
	                            case 4:
	                                str7 = str7 + "�3";
	                                break;
	                        }
	                        str7 = str7 + "\n" + Localization.current_.air_brake + ": " + ((�������������_��.��������������_������ * 100.0)).ToString("0") + "%";
	                        var str9 = (�������������_��.�������_��������� == 1) ? Localization.current_.forward : (�������������_��.�������_��������� == -1) ? Localization.current_.back : "0";
	                        str7 = str7 + "\n" + Localization.current_.reverse + ": " + str9;
	                        str7 = str7 + "\n" + ((����������.������_�������) ? Localization.current_.st_on : Localization.current_.st_off);
	                        str7 = str7 + "\n" + Localization.current_.trol + " " + ((����������.�������) ? Localization.current_.enable : Localization.current_.disable);
	                        /*if (����������.�� != null)
	                        {
	                        	str7 = str7 + "\n" + Localization.current_.ax + " " + (����������.��.������� ? Localization.current_.enable : Localization.current_.disable) + "\n" + Localization.current_.ax_power + ": " + (����������.��.�������_������� / ����������.��.������_�������).ToString("##0%");
	                        }*/
                            if (����������.�� != null) {
	                            //var str99 = (�������������_��.���������_�� == 1) ? Localization.current_.enable : (�������������_��.���������_�� == 0) ? Localization.current_.disable : "0";
	                            str7 = str7 + "\n" + Localization.current_.ax + ((����������.��.�������) ? Localization.current_.enable : Localization.current_.disable);
                                
                                //str7 = str7 + "\n" + Localization.current_.ax + " " + str99;
                                str7 = str7 + "\n" + Localization.current_.ax_power + ": " + (����������.��.�������_������� / ����������.��.������_�������).ToString("##0%");
                            }
	                        
	                    }
	                    else if (����������.�������_���������� is �������_����������.��_����)
	                    {
	                        str8 = Localization.current_.bus_control;
	                        var ����������_�� = (�������_����������.��_����) ����������.�������_����������;
	                        str7 = (("\n" + Localization.current_.gmod + ": " + ����������_��.�������_�����) + "\n" + Localization.current_.cur_pos + ": " + ����������_��.�������_��������) + "\n" + Localization.current_.pedal_pos + ": ";
	                        if (����������_��.���������_������� > 0.0)
	                        {
	                            str7 = str7 + Localization.current_.gas + " ";
	                        }
	                        if (����������_��.���������_������� < 0.0)
	                        {
	                            str7 = str7 + Localization.current_.brake + " ";
	                        }
	                        str7 = str7 + ((Math.Abs(����������_��.���������_�������) * 100.0)).ToString("0") + "%"
	                        	+ "\n" + Localization.current_.engine + " " + (����������.������� ? Localization.current_.enable : Localization.current_.disable);
	                    }
	                    else if (����������.�������_���������� is �������_����������.��_����1)
                        {
                            str8 = Localization.current_.bus_control;
                            var ����������_�� = (�������_����������.��_����) ����������.�������_����������;
                            str7 = (("\n" + Localization.current_.gmod + ": " + ����������_��.�������_�����) + "\n" + Localization.current_.cur_pos + ": " + ����������_��.�������_��������) + "\n" + Localization.current_.pedal_pos + ": ";
                            if (����������_��.���������_������� > 0.0)
                            {
                                str7 = str7 + Localization.current_.gas + " ";
                            }
                            if (����������_��.���������_������� < 0.0)
                            {
                                str7 = str7 + Localization.current_.brake + " ";
                            }
                            str7 = str7 + ((Math.Abs(����������_��.���������_�������) * 100.0)).ToString("0") + "%"
                                + "\n" + Localization.current_.engine + " " + (����������.������� ? Localization.current_.enable : Localization.current_.disable);
                        }
	                    str7 = str7 + "\n" + Localization.current_.parking_brake + " " + (����������.stand_brake ? Localization.current_.enable : Localization.current_.disable);
	                    if (����������.����������� > 0.0)
	                    {
	                        str7 = str7 + "\n" + Localization.current_.sterling + ": " + (((����������.����������� * 180.0) / 3.1415926535897931)).ToString("0") + "\x00b0 " + Localization.current_.ster_r;
	                    }
	                    else if (����������.����������� < 0.0)
	                    {
	                        str7 = str7 + "\n" + Localization.current_.sterling + ": " + (((-����������.����������� * 180.0) / 3.1415926535897931)).ToString("0") + "\x00b0 " + Localization.current_.ster_l;
	                    }
	                    else
	                    {
	                        str7 = str7 + "\n" + Localization.current_.sterling + ": " + Localization.current_.nr_pryamo;
	                    }
	                    var str12 = ����������.�������.number;
	                    var str17 = ����������.nextStop;
	                    if (����������.�_����)
	                    {
	                        str12 = str12 + " (" + Localization.current_.route_in_park + ")";
	                    }
	                    if (����������.����� != null)
	                    {
	                        var str16 = str12;
	                        str12 = str16 + "\n" + Localization.current_.order + ": " + ����������.�����.�������.number + "/" + ����������.�����.�����;
	                        if (����������.���� != null)
	                        {
	                            if (���.time < ����������.����.�����_�����������)
	                            {
	                                str12 = str12 + "\n" + Localization.current_.departure_time + ": " + ����������.����.str_�����_�����������;
	                            }
	                            str12 = str12 + "\n" + Localization.current_.arrival_time + ": " + ����������.����.str_�����_��������;
	                            if ((((����������.����_index < (����������.����.pathes.Length - 1)) && (����������.���������.������ != null)) && (����������.���������.������.���������������.Length > 1)) && ((����������.����_index > 0) || (����������.���������.������ == ����������.����.pathes[0])))
	                            {
	                                var ������2 = ����������.����.pathes[����������.����_index + 1];
	                                var str13 = Localization.current_.nr_pryamo;
	                                if (������2.������)
	                                {
	                                    if (������2.���������������0 > 0.0)
	                                    {
	                                        str13 = Localization.current_.nr_right;
	                                    }
	                                    else if (������2.���������������0 < 0.0)
	                                    {
	                                        str13 = Localization.current_.nr_left;
	                                    }
	                                    
	                                }
	                                str12 = str12 + "\n" + Localization.current_.nr + ": " + str13;
	                            }
	                        }
	                    }
	                    //str17 = "\nNS: "(����������.nextStop.��������);
	                    whole_info = str8 + ": " + control_str + str7 + "\n" + Localization.current_.speed + ": " + speed_str + " " + Localization.current_.speed_km + "\n" 
	                        + Localization.current_.route + ": " + str12;
	                    
	                }
                }
                Common.MyGUI.stringlist[i] = whole_info;
                Common.MyGUI.stringlist[i + 8] = "\ndTmax: " + World.dtmax.ToString("#0.000") + "\nFPS: " + MyDirect3D._newDevice.FPS.ToString("#00")
                    + "\nNewControl: " + ((NewControl) ? Localization.current_.enable : Localization.current_.disable)
                	+ "\nX: " + MyDirect3D.Camera_Position.x.ToString("#0.0")
	                + "\nY: " + MyDirect3D.Camera_Position.y.ToString("#0.0")
	                + "\nZ: " + MyDirect3D.Camera_Position.z.ToString("#0.0")
	                + "\nrY: " + MyDirect3D.Camera_Rotation.x.ToString("#0.000")
	                + "\nrZ: " + MyDirect3D.Camera_Rotation.y.ToString("#0.000");
            }
            Common.MyGUI.stringlist[12] = ConvertTime.TimeFromSeconds(���.time % 86400.0);
        }
        
        public void RenderThread()
        {
        	if (MyDirect3D.device == null) return;
            if (MainForm.in_editor) goto Label_new;
            MyDirect3D.device.BeginScene();
            MyDirect3D.ResetViewports(������.Length);
            MyDirect3D.SetViewport(-1);
            MyDirect3D.device.Clear(ClearFlags.ZBuffer | ClearFlags.Target, 0, 1f, 0);
            if (!�������)
            {
            	menu.Draw();
            	MyDirect3D.device.EndScene();
            	MyDirect3D.device.Present();
            	return;
            }
            Label_new:
        	for (var i = 0; i < ������.Length; i++)
            {
        		MyDirect3D.SetViewport(i);
                MyDirect3D.device.Clear(ClearFlags.ZBuffer | ClearFlags.Target, 0xb4ff, 1f, 0);
                MyDirect3D.SetCameraPos(������[i].excameraPosition, ������[i].excameraRotation);
                ���.RenderMeshes2();
				MeshObject.RenderList();
				MyDirect3D.Alpha = true;
				���.RenderMeshesA();
				MeshObject.RenderListA();
				MyDirect3D.Alpha = false;
        		if (!string.IsNullOrEmpty(Common.MyGUI.stringlist[i])) Common.MyGUI.default_font.DrawString(null, Common.MyGUI.stringlist[i], 15 + MyDirect3D.device.Viewport.X, 15 + MyDirect3D.device.Viewport.Y, Color.Black);
        		if (((MyDirect3D.device.Viewport.X + MyDirect3D.device.Viewport.Width) == MyDirect3D.Window_Width) &&
                    (MyDirect3D.device.Viewport.Y == 0))// continue;
                {
                	if (!string.IsNullOrEmpty(Common.MyGUI.stringlist[12])) Common.MyGUI.default_font.DrawString(null, Common.MyGUI.stringlist[12], MyDirect3D.Window_Width - 105/*0x69*/, 15, Color.Black);
                }
        		if (!MainForm.debug) continue;
        		if (!string.IsNullOrEmpty(Common.MyGUI.stringlist[i + 8])) Common.MyGUI.default_font.DrawString(null, Common.MyGUI.stringlist[i + 8], new Rectangle(MyDirect3D.Window_Width - 160, 15, 160, 500), DrawTextFormat.Right, Color.Black);
        		if (!string.IsNullOrEmpty(Common.MyGUI.stringlist[i + 4]))
        		{
        			Common.MyGUI.default_font.DrawString(null, Common.MyGUI.stringlist[i + 4], (int) (420 + MyDirect3D.device.Viewport.X), (int) (15 + MyDirect3D.device.Viewport.Y), Color.Black);
            		Common.MyGUI.stringlist[i + 4] = string.Empty;
        		}
        	}
            MyFeatures.MakeScreenshot(false);
        	MyDirect3D.device.EndScene();
        	MyDirect3D.device.Present();
        }

        private void �����������(����� �����)
        {
            if (�����.�������������� != null)
            {
                �����.�������������� = null;
            }
            else
            {
                double num = 200.0;
                IVector _�������� = null;
                IControlledObject _������ = null;
                foreach (Transport ��������� in this.���.����������)
                {
                    DoublePoint point = ���������.position - �����.cameraPosition.XZPoint;
                    double num2 = point.Modulus;
                    if (num2 < num)
                    {
                        num = num2;
                        _�������� = ���������;
                        _������ = ���������;
                    }
                }
                �����.�������������� = _��������;
                if ((�����.����������������� != null) && (�����.����������������� != _������))
                {
                    �����.�����������������.���������� = ����������.��������������;
                }
                �����.����������������� = _������;
            }
        }

        public void ���������(string filename)
        {
        }
    }
}