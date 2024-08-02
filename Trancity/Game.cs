using System;
using System.Drawing;
using Common;
//using Microsoft.DirectX.Direct3D;
//using Microsoft.DirectX.DirectInput;
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
        public bool активна = true;
        public Игрок[] игроки;
        public World мир;
        private int _транспортPosIndex;
        private const int num = 0x400;
        public static bool fmouse = false;
//        public ПсевдоЛокализация loc = new ПсевдоЛокализация();
        public MyMenu menu;
        //test:
        public static int col;
        public static int row;
//        public Transport[] транспортArray;// = мир.транспорты.ToArray(typeof(Transport));

        public void Process_Input()
        {
        	if (активна && (мир.транспорты.Count > 0))// мир.транспорты.Count > 0))
            {
//                var транспортArray = (Transport[]) мир.транспорты.ToArray(typeof(Transport));
                this._транспортPosIndex++;
                if (this._транспортPosIndex >= мир.транспорты.Count)//.Length)
                {
                    this._транспортPosIndex = 0;
                }
                foreach (var положение in ((Transport)мир.транспорты[this._транспортPosIndex]).найденные_положения)
                {
                    if (положение.Дорога != null)
                    {
                        положение.Дорога.занятыеПоложения.Remove(положение);
                    }
                }
                ((Transport)мир.транспорты[_транспортPosIndex]).НайтиВсеПоложения(мир);
                foreach (var положение2 in ((Transport)мир.транспорты[_транспортPosIndex]).найденные_положения)
                {
                    if (положение2.Дорога != null)
                    {
                        положение2.Дорога.занятыеПоложения.Add(положение2);
                    }
                }
                foreach (Transport транспорт in мир.транспорты)
                {
                    if (транспорт.управление.автоматическое)
                    {
                        транспорт.АвтоматическиУправлять(мир);
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
            if (!активна)
            {
            	menu.Refresh();
            }
            var changed = false;
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
            if (KeyState[Key.Escape])
            {
                активна = !активна;
            }
            for (var i = 0; i < joystickDevices.Length; i++)
            {
                if (FJStatesArray[i][9])
                {
                    активна = !активна;
                }
            }
            if (активна)
            {
            	if (KeyState[Key.Tab])
            	{
                	MyDirect3D.вид_сверху = !MyDirect3D.вид_сверху;
            	}
                foreach (var игрок in игроки)
                {
                    /*if (игрок.объектПривязки != null)
                    {
                        var point4 = игрок.объектПривязки.position - игрок.cameraPosition.xz_point;
                        if (point4.модуль > 200.0)
                        {
                            игрок.объектПривязки = null;
                        }
                    }*/
                    if (игрок.управляемыйОбъект != null)
                    {
                        DoublePoint point5 = игрок.управляемыйОбъект.position - игрок.cameraPosition.xz_point;
                        if (point5.модуль > 200.0)
                        {
                            игрок.управляемыйОбъект.управление = Управление.Автоматическое;
                            игрок.управляемыйОбъект = null;
                            игрок.объектПривязки = null;
                        }
                    }
                    int current_joystick = -1;
                    for (int k = 0; k < joystickDevices.Length; k++)
                    {
                        if (игрок.inputGuid == deviceGuids[k])
                        {
                            current_joystick = k;
                            break;
                        }
                    }
                    if (current_joystick == -1)//(игрок.inputGuid == MyDirectInput.Keyboard_Device.Information.InstanceGuid)//SystemGuid.Keyboard)
                    {
//                        byte[] mouseButtons = state.GetMouseButtons();
//                        int x = state.X;
//                        int y = state.Y;
//                        int z = state.Z;
//                        if ((mouseButtons[1] != 0) && (this._lastMouseButtons[1] == 0))
                        if ((mouseButtons[1]) && (!this._lastMouseButtons[1]))
                        {
                            this.Привязывать(игрок);
                        }
                        if (!MyDirect3D.вид_сверху)
                        {
//	                        if (mouseButtons[0] == 0)
	                        if (!mouseButtons[0])
	                        {
	                            игрок.cameraRotationChange.x -= 0.001 * x;
	                            игрок.cameraRotationChange.y -= 0.001 * y;
	                        }
	                        else
	                        {
	                            DoublePoint point = new DoublePoint(игрок.cameraPositionChange.x, игрок.cameraPositionChange.z) / new DoublePoint(игрок.cameraRotation.x);
	                            point.x -= 0.01 * y;
	                            point.y -= 0.01 * x;
	                            игрок.cameraPositionChange.x = (point * new DoublePoint(игрок.cameraRotation.x)).x;
	                            игрок.cameraPositionChange.z = (point * new DoublePoint(игрок.cameraRotation.x)).y;
	                        }
	                        игрок.cameraPositionChange.y += 0.001 * z;
                        }
                        else
                        {                        	
                        	MyDirect3D.масштаб += 0.001 * z;
                        	if (MyDirect3D.масштаб <= 2.5) MyDirect3D.масштаб = 2.5;
//                        	if (mouseButtons[0] != 0)
                        	if (mouseButtons[0])
                        	{
                        		DoublePoint point = new DoublePoint(игрок.cameraPositionChange.x, игрок.cameraPositionChange.z);
	                            point.x += 0.01 * x;
	                            point.y -= 0.01 * y;
                        		игрок.cameraPositionChange.x = point.x;
                        		игрок.cameraPositionChange.z = point.y;
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
                            this.Привязывать(игрок);
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
                        if (((игрок.управляемыйОбъект != null) && (игрок.управляемыйОбъект is Безрельсовый_Транспорт)) && игрок.управляемыйОбъект.управление.ручное)
                        {
                            if (!Current_FJState[4, false])
                            {
                                int num12 = 6;
                                if (((Transport) игрок.управляемыйОбъект).система_управления is Система_управления.Автобусная)
                                {
                                    num12 = 10;
                                }
                                if (Current_FJState[num12, false])
                                {
                                    игрок.cameraRotationChange.x -= num8;
                                    игрок.cameraRotationChange.y -= num9;
                                }
                            }
                            else
                            {
                                DoublePoint point2 = new DoublePoint(игрок.cameraPositionChange.x, игрок.cameraPositionChange.z) / new DoublePoint(игрок.cameraRotation.x);
                                point2.x -= 10.0 * num9;
                                point2.y -= 10.0 * num8;
                                игрок.cameraPositionChange.x = (point2 * new DoublePoint(игрок.cameraRotation.x)).x;
                                игрок.cameraPositionChange.z = (point2 * new DoublePoint(игрок.cameraRotation.x)).y;
                                игрок.cameraPositionChange.y += num10;
                            }
                        }
                        else if (!Current_FJState[4, false])
                        {
                            игрок.cameraRotationChange.x -= num8;
                            игрок.cameraRotationChange.y -= num9;
                        }
                        else
                        {
                            DoublePoint point3 = new DoublePoint(игрок.cameraPositionChange.x, игрок.cameraPositionChange.z) / new DoublePoint(игрок.cameraRotation.x);
                            point3.x -= 10.0 * num9;
                            point3.y -= 10.0 * num8;
                            игрок.cameraPositionChange.x = (point3 * new DoublePoint(игрок.cameraRotation.x)).x;
                            игрок.cameraPositionChange.z = (point3 * new DoublePoint(игрок.cameraRotation.x)).y;
                            игрок.cameraPositionChange.y += num10;
                        }
                    }
                    /*var _y = мир.GetHeight(игрок.cameraPosition.xz_point);
                    if (игрок.cameraPosition.y - 0.01 < _y)
                    {
                        игрок.cameraPosition.y = _y + 0.01;
                    	игрок.cameraPositionChange.y = 0;
                    }*/
					if (игрок.управляемыйОбъект == null) continue;
					var _transport = (Transport) игрок.управляемыйОбъект;
					if (current_joystick == -1)//(игрок.inputGuid == MyDirectInput.Keyboard_Device.Information.InstanceGuid)//SystemGuid.Keyboard)
					{
						if (KeyState[Key.A])
		                {// TODO: пофиксить список остановок при смене управления
		                	_transport.управление.автоматическое = !_transport.управление.автоматическое;
		                    _transport.currentStop = _transport.nextStop = null;
		                    _transport.stopIndex = 0;
		                }
		                if (KeyState[Key.M])
		                {
		                	_transport.управление.ручное = !_transport.управление.ручное;
		                }
		                if (_transport.управление.ручное)
		                {
		                	if (KeyState[Key.Y])
		                    {
		                    	_transport.включен = !_transport.включен;
		                    }
		                	if (KeyState[Key.S])
		                    {
		                    	if (!_transport.двери_водителя_закрыты)
		                        {
		                        	_transport.ОткрытьДвериВодителя(false);
		                        }
		                        else if (!_transport.двери_водителя_открыты)
		                        {
		                        	_transport.ОткрытьДвериВодителя(true);
		                        }
		                    }
		                	if (KeyState[Key.D])
		                    {
		                    	if (!_transport.двери_закрыты)
		                        {
		                        	_transport.ОткрытьДвери(false);
		                        }
		                        else if (!_transport.двери_открыты)
		                        {
		                        	_transport.ОткрытьДвери(true);
		                        }
		                    }
		                    if (KeyState[Key.D1])
		                    {
		                        if (!_transport.ДверьЗакрыта(0))
		                        {
		                        	_transport.ОткрытьДвери(0, false);
		                        }
		                        else if (!_transport.ДверьОткрыта(0))
		                        {
		                        	_transport.ОткрытьДвери(0, true);
		                    	}
		                    }
		                    if (KeyState[Key.D2])
		                    {
		                        if (!_transport.ДверьЗакрыта(1))
		                        {
		                        	_transport.ОткрытьДвери(1, false);
		                        }
		                        else if (!_transport.ДверьОткрыта(0))
		                        {
		                        	_transport.ОткрытьДвери(1, true);
		                    	}
		                    }
		                    if (KeyState[Key.D3])
		                    {
		                        if (!_transport.ДверьЗакрыта(2))
		                        {
		                        	_transport.ОткрытьДвери(2, false);
		                        }
		                        else if (!_transport.ДверьОткрыта(0))
		                        {
		                        	_transport.ОткрытьДвери(2, true);
		                    	}
		                    }
		                    if (KeyState[Key.D4])
		                    {
		                        if (!_transport.ДверьЗакрыта(3))
		                        {
		                        	_transport.ОткрытьДвери(3, false);
		                        }
		                        else if (!_transport.ДверьОткрыта(0))
		                        {
		                        	_transport.ОткрытьДвери(3, true);
		                    	}
		                    }
		                    if (KeyState[Key.D5])
		                    {
		                        if (!_transport.ДверьЗакрыта(4))
		                        {
		                        	_transport.ОткрытьДвери(4, false);
		                        }
		                        else if (!_transport.ДверьОткрыта(0))
		                        {
		                        	_transport.ОткрытьДвери(4, true);
		                    	}
		                    }
		                    if (KeyState[Key.B])
	                        {
	                        	_transport.stand_brake = !_transport.stand_brake;
	                        }
		                    /*if (state2[Key.E])
	                        {
	                            if (transport.аварийная_сигнализация)
	                            {
	                            	transport.аварийная_сигнализация = false;
	                            }
	                            else
	                            {
	                            	transport.аварийная_сигнализация = true;
	                            	transport.указатель_поворота = 0;
	                        	}
	                        }*/
	                        if (KeyState[Key.F])
	                        {
	                        	_transport.включены_фары = !_transport.включены_фары;
	                        }
		                }
		                if (игрок.объектПривязки != null)
		                {
		                	if (KeyState[Key.C])//KeyState.InputState.IsPressed(Key)
			                {
			                	_transport.SetCamera(0, игрок);
			                }
			                if (KeyState[Key.F2])
			                {
			                	_transport.SetCamera(1, игрок);
			                }
			                 if (KeyState[Key.F3])
			                {
			                	_transport.SetCamera(2, игрок);
			                }
			                if (KeyState[Key.F4])
			                {
			                	_transport.SetCamera(3, игрок);
			                }
		                }
	                }
                    if (игрок.управляемыйОбъект is Трамвай)
                    {
	                    Трамвай трамвай = (Трамвай) игрок.управляемыйОбъект;
	                    if (current_joystick == -1)//(игрок.inputGuid == MyDirectInput.Keyboard_Device.Information.InstanceGuid)//SystemGuid.Keyboard)
	                    {
	                        if (трамвай.управление.ручное)
	                        {
	                            if (KeyState[Key.T])
	                            {
	                                if (трамвай.токоприёмник.опущен)
	                                {
	                                	трамвай.токоприёмник.НайтиПровод(мир.контактныеПровода2);
	                                	if (трамвай.токоприёмник.Провод != null)
	                                	{
	                                    	трамвай.токоприёмник.поднимается = true;
	                                	}
	                                }
	                                else if (трамвай.токоприёмник.поднят)
	                                {
	                                    трамвай.токоприёмник.поднимается = false;
	                                }
	                            }
	                            if (KeyState[Key.D6])
	                            {
	                                if (!трамвай.ДверьЗакрыта(5))
	                                {
	                                    трамвай.ОткрытьДвери(5, false);
	                                }
	                                else if (!трамвай.ДверьОткрыта(5))
	                                {
	                                    трамвай.ОткрытьДвери(5, true);
	                                }
	                            }
	                            if (KeyState[Key.D7])
	                            {
	                                if (!трамвай.ДверьЗакрыта(6))
	                                {
	                                    трамвай.ОткрытьДвери(6, false);
	                                }
	                                else if (!трамвай.ДверьОткрыта(6))
	                                {
	                                    трамвай.ОткрытьДвери(6, true);
	                                }
	                            }
	                            if (KeyState[Key.D8])
	                            {
	                                if (!трамвай.ДверьЗакрыта(7))
	                                {
	                                    трамвай.ОткрытьДвери(7, false);
	                                }
	                                else if (!трамвай.ДверьОткрыта(7))
	                                {
	                                    трамвай.ОткрытьДвери(7, true);
	                                }
	                            }
	                            if (KeyState[Key.D9])
	                            {
	                                if (!трамвай.ДверьЗакрыта(8))
	                                {
	                                    трамвай.ОткрытьДвери(8, false);
	                                }
	                                else if (!трамвай.ДверьОткрыта(8))
	                                {
	                                    трамвай.ОткрытьДвери(8, true);
	                                }
	                            }
	                            if (KeyState[Key.D0])
	                            {
	                                if (!трамвай.ДверьЗакрыта(9))
	                                {
	                                    трамвай.ОткрытьДвери(9, false);
	                                }
	                                else if (!трамвай.ДверьОткрыта(9))
	                                {
	                                    трамвай.ОткрытьДвери(9, true);
	                                }
	                            }
	                            if (трамвай.система_управления is Система_управления.РКСУ_Трамвай)
	                            {
	                            	Система_управления.РКСУ_Трамвай трамвай2 = (Система_управления.РКСУ_Трамвай) трамвай.система_управления;
	                            	if ((KeyState[Key.Backspace] && (трамвай.скорость == 0.0)) && (трамвай2.позиция_контроллера == 0))
	                            	{
	                            	    трамвай2.позиция_реверсора = -трамвай2.позиция_реверсора;
	                            	}
	                            	if (KeyState[Key.DownArrow])
	                            	{
	                            	    if (трамвай2.позиция_контроллера > трамвай2.позиция_min)
	                            	    {
	                            	        трамвай2.позиция_контроллера--;
	                            	    }
	                            	}
	                            	else if (KeyState[Key.UpArrow] && (трамвай2.позиция_контроллера < трамвай2.позиция_max))
	                            	{
	                                	трамвай2.позиция_контроллера++;
	                            	}
	                            }
	                            if (!KeyState[Key.RightControl])
	                            {
	                            	if (KeyState[Key.LeftArrow])
	                                {
	                                	if (трамвай.указатель_поворота >= 0)
	                                    {
	                                        трамвай.указатель_поворота = -1;
	                                        трамвай.аварийная_сигнализация = false;
	                                    }
	                                    else
	                                    {
	                                        трамвай.указатель_поворота = 0;
	                                    }
	                                }
	                            	if (KeyState[Key.RightArrow])
	                                {
	                            	       if (трамвай.указатель_поворота <= 0)
	                                    {
	                                        трамвай.указатель_поворота = 1;
	                                        трамвай.аварийная_сигнализация = false;
	                                    }
	                                    else
	                                    {
	                                        трамвай.указатель_поворота = 0;
	                                    }
	                                }
	                            }
	                            else if (((трамвай.скорость == 0.0) && трамвай.двери_водителя_открыты) && ((трамвай.передняя_ось.текущий_рельс.следующие_рельсы.Length > 1) && (трамвай.передняя_ось.пройденное_расстояние_по_рельсу > (трамвай.передняя_ось.текущий_рельс.Длина - 8.0))))
	                            {
	                                if (KeyState[Key.LeftArrow])
	                                {
	                                    трамвай.передняя_ось.текущий_рельс.следующий_рельс = 0;
	                                }
	                                if (KeyState[Key.RightArrow])
	                                {
	                                    трамвай.передняя_ось.текущий_рельс.следующий_рельс = 1;
	                                }
	                            }
	                            if (KeyState[Key.Q])
	                            {
	                                трамвай.аварийная_сигнализация = !трамвай.аварийная_сигнализация;
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
		                        трамвай.управление.автоматическое = !трамвай.управление.автоматическое;
		                        трамвай.управление.ручное = !трамвай.управление.ручное;
		                    }
		                    if (!трамвай.управление.ручное)
		                    {
		                        goto Label_0E1A;
		                    }
		                    if (state5[11])
		                    {
		                        трамвай.включен = !трамвай.включен;
		                    }
		                    if (state5[2])
		                    {
		                        if (трамвай.токоприёмник.опущен)
		                        {
		                            трамвай.токоприёмник.НайтиПровод(мир.контактныеПровода2);
	                                if (трамвай.токоприёмник.Провод != null)
	                                {
	                                   	трамвай.токоприёмник.поднимается = true;
	                                }
		                        }
		                        else if (трамвай.токоприёмник.поднят)
		                        {
		                            трамвай.токоприёмник.поднимается = false;
		                        }
		                    }
		                    if (state5[0])
		                    {
		                        if (!трамвай.двери_водителя_закрыты)
		                        {
		                            трамвай.ОткрытьДвериВодителя(false);
		                        }
		                        else if (!трамвай.двери_водителя_открыты)
		                        {
		                            трамвай.ОткрытьДвериВодителя(true);
		                        }
		                    }
		                    if (state5[1])
		                    {
		                        if (!трамвай.двери_закрыты)
		                        {
		                            трамвай.ОткрытьДвери(false);
		                        }
		                        else if (!трамвай.двери_открыты)
		                        {
		                            трамвай.ОткрытьДвери(true);
		                        }
		                    }
		                    if (трамвай.система_управления is Система_управления.РКСУ_Трамвай)
		                    {
		                    	var трамвай2 = (Система_управления.РКСУ_Трамвай) трамвай.система_управления;
		                    	switch (((-5 * state6.RotationZ) / num))
		                    	{
		                        	case -5:
		                            	трамвай2.позиция_контроллера = -5;
		                            	goto Label_0D93;
		
		                        	case -4:
		                            	if (трамвай2.позиция_контроллера > -4)
		                            	{
		                            	    трамвай2.позиция_контроллера = -4;
		                            	}
		                            	goto Label_0D93;
		
		                        	case -3:
		                            	if (трамвай2.позиция_контроллера > -3)
		                            	{
		                            	    трамвай2.позиция_контроллера = -3;
		                            	}
		                            	goto Label_0D93;
		
		                        	case -2:
		                            	if (трамвай2.позиция_контроллера > -2)
		                            	{
		                            	    трамвай2.позиция_контроллера = -2;
		                            	}
		                            	goto Label_0D93;
		
		                        	case -1:
		                            	if (трамвай2.позиция_контроллера > -1)
		                            	{
		                            	    трамвай2.позиция_контроллера = -1;
		                            	}
		                            	goto Label_0D93;
		
		                        	case 0:
		                            	if (state6.RotationZ <= 0)
		                            	{
		                            	    break;
		                            	}
		                            	if (трамвай2.позиция_контроллера > 0)
		                            	{
		                            	    трамвай2.позиция_контроллера = 0;
		                            	}
		                            	goto Label_0D93;
		
		                        	case 1:
		                            	if (трамвай2.позиция_контроллера < 1)
		                            	{
		                            	    трамвай2.позиция_контроллера = 1;
		                            	}
		                            	goto Label_0D93;
		
		                        	case 2:
		                            	if (трамвай2.позиция_контроллера < 1)
		                            	{
		                            	    трамвай2.позиция_контроллера = 1;
		                            	}
		                            	goto Label_0D93;
		
		                        	case 3:
		                            	if (трамвай2.позиция_контроллера < 2)
		                            	{
		                            	    трамвай2.позиция_контроллера = 2;
		                            	}
		                            	goto Label_0D93;
		
		                        	case 4:
		                            	if (трамвай2.позиция_контроллера < 3)
		                            	{
		                                	трамвай2.позиция_контроллера = 3;
		                            	}
		                            	goto Label_0D93;
		
		                   	     	case 5:
		                   	         	трамвай2.позиция_контроллера = 4;
		                    	        goto Label_0D93;
		
		                    	    default:
		                    	        goto Label_0D93;
		                    	}
		                    	if ((state6.RotationZ < 0) && (трамвай2.позиция_контроллера < 0))
		                    	{
		                    	    трамвай2.позиция_контроллера = 0;
		                    	}
		                    	Label_0D93:
		                    	if ((state5[7] && (трамвай.скорость == 0.0)) && (трамвай2.позиция_контроллера == 0))
		                    	{
		                    	    трамвай2.позиция_реверсора = -трамвай2.позиция_реверсора;
		                    	}
		                    }
		                    switch (num16)
		                    {
		                        case 0:
		                            трамвай.указатель_поворота = 0;
		                            трамвай.аварийная_сигнализация = false;
		                            break;
		
		                        case 2:
		                            трамвай.указатель_поворота = 1;
		                            трамвай.аварийная_сигнализация = false;
		                            break;
		
		                        case 4:
		                            трамвай.указатель_поворота = 0;
		                            трамвай.аварийная_сигнализация = true;
		                            break;
		
		                        case 6:
		                            трамвай.указатель_поворота = -1;
		                            трамвай.аварийная_сигнализация = false;
		                            goto Label_0E1A;
		                    }
	                    }
                    }
                    Label_0E1A:;
                    if (игрок.управляемыйОбъект is Троллейбус)
                    {
                        Троллейбус троллейбус = (Троллейбус) игрок.управляемыйОбъект;
                        if (current_joystick == -1)//(игрок.inputGuid == SystemGuid.Keyboard)
                        {
                            if (троллейбус.управление.ручное)
                            {
                                if (троллейбус.штанги.Length == 2)
                                {
	                                if (KeyState[Key.T])
	                                {
	                                    if (троллейбус.штанги[0].Опущена && троллейбус.штанги[1].Опущена)
	                                    {
	                                        троллейбус.штанги[0].НайтиПровод(this.мир.контактныеПровода);
	                                        if (троллейбус.штанги[0].Провод != null)
	                                        {
	                                            троллейбус.штанги[0].поднимается = true;
	                                        }
	                                        троллейбус.штанги[1].НайтиПровод(this.мир.контактныеПровода);
	                                        if (троллейбус.штанги[1].Провод != null)
	                                        {
	                                            троллейбус.штанги[1].поднимается = true;
	                                        }
	                                    }
	                                    else
	                                    {
	                                        троллейбус.штанги[0].поднимается = false;
	                                        троллейбус.штанги[1].поднимается = false;
	                                    }
	                                }
                                }
                                if (троллейбус.система_управления is Система_управления.РКСУ_Троллейбус)
                                {
                                    Система_управления.РКСУ_Троллейбус троллейбус2 = (Система_управления.РКСУ_Троллейбус) троллейбус.система_управления;
                                    if ((KeyState[Key.Backspace] && (троллейбус.скорость == 0.0)) && (троллейбус2.позиция_контроллера == 0))
                                    {
                                        троллейбус2.позиция_реверсора = -троллейбус2.позиция_реверсора;
                                    }
                                    if (KeyState.InputState.IsPressed(Key.DownArrow))//[Key.DownArrow])
                                    {
                                        if ((троллейбус2.пневматический_тормоз > 0.0) && (троллейбус2.пневматический_тормоз < 1.0))
                                        {
                                            троллейбус2.пневматический_тормоз += 0.05;
                                        }
                                    }
                                    else if (KeyState.InputState.IsPressed(Key.UpArrow) && (троллейбус2.пневматический_тормоз > 0.0))
                                    {
                                        троллейбус2.пневматический_тормоз -= 0.05;
                                        if (троллейбус2.пневматический_тормоз < 0.0)
                                        {
                                            троллейбус2.пневматический_тормоз = 0.0;
                                        }
                                    }
                                    if (KeyState[Key.DownArrow])//[Key.DownArrow] [Key.UpArrow]
                                    {
                                        if (троллейбус2.позиция_контроллера > троллейбус2.позиция_min)
                                        {
                                            троллейбус2.позиция_контроллера--;
                                        }
                                        else if (троллейбус2.пневматический_тормоз == 0.0)
                                        {
                                            троллейбус2.пневматический_тормоз = 0.05;
                                        }
                                    }
                                    else if ((KeyState[Key.UpArrow] && (троллейбус2.позиция_контроллера < троллейбус2.позиция_max)) && (троллейбус2.пневматический_тормоз == 0.0))
                                    {
                                        троллейбус2.позиция_контроллера++;
                                    }
                                    
                                    if ((KeyState[Key.O]) && (троллейбус.ах != null))
                                    {
                                    	троллейбус.ах.включён = !троллейбус.ах.включён;
                                    }
                                }
                                if (троллейбус.система_управления is Система_управления.КП_Авто)
                                {
                                    Система_управления.КП_Авто авто = (Система_управления.КП_Авто) троллейбус.система_управления;
                                    if ((KeyState[Key.Z] && (авто.режим > 0)) && (((авто.текущий_режим != "R") && (авто.текущий_режим != "N")) || ((авто.положение_педалей == -1.0) && (троллейбус.скорость == 0.0))))
                                    {
                                        авто.режим--;
                                    }
                                    if ((KeyState[Key.X] && (авто.режим < (авто.режимы.Length - 1))) && (((авто.текущий_режим != "P") && (авто.текущий_режим != "N")) || ((авто.положение_педалей == -1.0) && (троллейбус.скорость == 0.0))))
                                    {
                                        авто.режим++;
                                    }
                                    if (KeyState[Key.DownArrow])
                                    {
                                        if (авто.положение_педалей == 0.0)
                                        {
                                            авто.положение_педалей = (-World.прошлоВремени * 5.0) / 3.0;
                                        }
                                    }
                                    else if (KeyState[Key.UpArrow] && (авто.положение_педалей == 0.0))
                                    {
                                        авто.положение_педалей = (World.прошлоВремени * 5.0) / 3.0;
                                    }
                                    if (KeyState.InputState.IsPressed(Key.DownArrow))
                                    {
                                        if (авто.положение_педалей < 0.0)
                                        {
                                            авто.положение_педалей -= (World.прошлоВремени * 5.0) / 3.0;
                                            if (авто.положение_педалей < -1.0)
                                            {
                                                авто.положение_педалей = -1.0;
                                            }
                                        }
                                        if (авто.положение_педалей > 0.0)
                                        {
                                            авто.положение_педалей -= (World.прошлоВремени * 5.0) / 3.0;
                                            if (авто.положение_педалей < 0.0)
                                            {
                                                авто.положение_педалей = 0.0;
                                                KeyState.keyticks[0xd0] = 1;
                                            }
                                        }
                                    }
                                    else if (KeyState.InputState.IsPressed(Key.UpArrow))
                                    {
                                        if (авто.положение_педалей > 0.0)
                                        {
                                            авто.положение_педалей += (World.прошлоВремени * 5.0) / 3.0;
                                            if (авто.положение_педалей > 1.0)
                                            {
                                                авто.положение_педалей = 1.0;
                                            }
                                        }
                                        if (авто.положение_педалей < 0.0)
                                        {
                                            авто.положение_педалей += (World.прошлоВремени * 5.0) / 3.0;
                                            if (авто.положение_педалей > 0.0)
                                            {
                                                авто.положение_педалей = 0.0;
                                                KeyState.keyticks[200] = 1;
                                            }
                                        }
                                    }
                                }
                                if (!fmouse)
                                {
                                	if (KeyState.InputState.IsPressed(Key.LeftArrow))
	                                {
	                                    троллейбус.поворотРуля -= 0.3 * World.прошлоВремени;
	                                }
                                	if (KeyState.InputState.IsPressed(Key.RightArrow))
	                                {
	                                    троллейбус.поворотРуля += 0.3 * World.прошлоВремени;
	                                }
                                }
                                else if (!mouseButtons[0])
                                {
	                                троллейбус.поворотРуля += x * 0.001;
                                }
                                if (KeyState[Key.Q])
                                {
                                    if (троллейбус.указатель_поворота >= 0)
                                    {
                                        троллейбус.указатель_поворота = -1;
                                        троллейбус.аварийная_сигнализация = false;
                                    }
                                    else
                                    {
                                        троллейбус.указатель_поворота = 0;
                                    }
                                }
                                if (KeyState[Key.W])
                                {
                                    if (троллейбус.указатель_поворота <= 0)
                                    {
                                        троллейбус.указатель_поворота = 1;
                                        троллейбус.аварийная_сигнализация = false;
                                    }
                                    else
                                    {
                                        троллейбус.указатель_поворота = 0;
                                    }
                                }
                                if (KeyState[Key.E])
                                {
                                    if (троллейбус.аварийная_сигнализация)
                                    {
                                        троллейбус.аварийная_сигнализация = false;
                                    }
                                    else
                                    {
                                        троллейбус.аварийная_сигнализация = true;
                                        троллейбус.указатель_поворота = 0;
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
                                    троллейбус.управление.автоматическое = !троллейбус.управление.автоматическое;
                                    троллейбус.управление.ручное = !троллейбус.управление.ручное;
                                }
                                if (state7[3])
                                {
                                    троллейбус.включены_фары = !троллейбус.включены_фары;
                                }
                                if (троллейбус.управление.ручное)
                                {
                                    if (state7[11])
                                    {
                                        троллейбус.включен = !троллейбус.включен;
                                    }
                                    if (state7[2] && (троллейбус.штанги.Length > 1))
                                    {
                                        if (троллейбус.штанги[0].Опущена && троллейбус.штанги[1].Опущена)
                                        {
                                            троллейбус.штанги[0].НайтиПровод(this.мир.контактныеПровода);
                                            if (троллейбус.штанги[0].Провод != null)
                                            {
                                                троллейбус.штанги[0].поднимается = true;
                                            }
                                            троллейбус.штанги[1].НайтиПровод(this.мир.контактныеПровода);
                                            if (троллейбус.штанги[1].Провод != null)
                                            {
                                                троллейбус.штанги[1].поднимается = true;
                                            }
                                        }
                                        else
                                        {
                                            троллейбус.штанги[0].поднимается = false;
                                            троллейбус.штанги[1].поднимается = false;
                                        }
                                    }
                                    if (state7[0])
                                    {
                                        if (!троллейбус.двери_водителя_закрыты)
                                        {
                                            троллейбус.ОткрытьДвериВодителя(false);
                                        }
                                        else if (!троллейбус.двери_водителя_открыты)
                                        {
                                            троллейбус.ОткрытьДвериВодителя(true);
                                        }
                                    }
                                    if (state7[1])
                                    {
                                        if (!троллейбус.двери_закрыты)
                                        {
                                            троллейбус.ОткрытьДвери(false);
                                        }
                                        else if (!троллейбус.двери_открыты)
                                        {
                                            троллейбус.ОткрытьДвери(true);
                                        }
                                    }
                                    if (!state7[4, false] && !state7[6, false])
                                    {
                                        троллейбус.поворотРуля += ((0.5 * World.прошлоВремени) * state8.X) / ((double) num);
                                    }
                                    double num21 = (-1.0 * state8.RotationZ) / ((double) num);
                                    if (троллейбус.система_управления is Система_управления.РКСУ_Троллейбус)
                                    {
                                        Система_управления.РКСУ_Троллейбус троллейбус3 = (Система_управления.РКСУ_Троллейбус) троллейбус.система_управления;
                                        if (num21 >= -0.6)
                                        {
                                            троллейбус3.позиция_контроллера = (int) (4.0 * num21);
                                            троллейбус3.пневматический_тормоз = 0.0;
                                        }
                                        else
                                        {
                                            троллейбус3.позиция_контроллера = -2;
                                            троллейбус3.пневматический_тормоз = -(num21 + 0.6) / 0.4;
                                        }
                                        if ((state7[7] && (троллейбус.скорость == 0.0)) && (троллейбус3.позиция_контроллера == 0))
                                        {
                                            троллейбус3.позиция_реверсора = -троллейбус3.позиция_реверсора;
                                        }
                                    }
                                    if (троллейбус.система_управления is Система_управления.КП_Авто)
                                    {
                                        Система_управления.КП_Авто авто2 = (Система_управления.КП_Авто) троллейбус.система_управления;
                                        авто2.положение_педалей = num21;
                                        if ((state7[6] && (авто2.режим > 0)) && (((авто2.текущий_режим != "R") && (авто2.текущий_режим != "N")) || ((авто2.положение_педалей == -1.0) && (троллейбус.скорость == 0.0))))
                                        {
                                            авто2.режим--;
                                        }
                                        if ((state7[7] && (авто2.режим < (авто2.режимы.Length - 1))) && (((авто2.текущий_режим != "P") && (авто2.текущий_режим != "N")) || ((авто2.положение_педалей == -1.0) && (троллейбус.скорость == 0.0))))
                                        {
                                            авто2.режим++;
                                        }
                                    }
                                    switch (num20)
                                    {
                                        case 0:
                                            троллейбус.указатель_поворота = 0;
                                            троллейбус.аварийная_сигнализация = false;
                                            break;

                                        case 2:
                                            троллейбус.указатель_поворота = 1;
                                            троллейбус.аварийная_сигнализация = false;
                                            break;

                                        case 4:
                                            троллейбус.указатель_поворота = 0;
                                            троллейбус.аварийная_сигнализация = true;
                                            break;

                                        case 6:
                                            троллейбус.указатель_поворота = -1;
                                            троллейбус.аварийная_сигнализация = false;
                                            break;
                                    }
                                }
//                            }
                        }
                    }
                }
            }
            if (KeyState[Key.F1])
            {
            	MainForm.debug = !MainForm.debug;
            }
            if (KeyState[Key.F10])
            {
            	var now = DateTime.Now;
            	var path = Application.StartupPath + @"\Screenshots\";
            	var screenshot = string.Format(@"{0}\Trancity {1:00}-{2:00}-{3} {4:00}-{5:00}-{6:00}-{7:000}.bmp", path, now.Day, now.Month, now.Year, now.Hour, now.Minute, now.Second, now.Millisecond);
//				var surface = Surface.CreateOffscreenPlain(MyDirect3D.device, Screen.PrimaryScreen.Bounds.Width,
//                Screen.PrimaryScreen.Bounds.Height,
//                Format.A8R8G8B8,
//                Pool.SystemMemory);
            	/*var surface = MyDirect3D.device.CreateOffscreenPlainSurface(
                Screen.PrimaryScreen.Bounds.Width,
                Screen.PrimaryScreen.Bounds.Height,
                Format.A8R8G8B8,
                Pool.SystemMemory);*/
//            	var swapchain = MyDirect3D.device.GetSwapChain(0);
//            	swapchain.GetFrontBufferData(surface);
//				MyDirect3D.device.GetFrontBufferData(0, surface);
//            	var surface = swapchain.GetBackBuffer(0);
            	var surface = MyDirect3D.device.GetRenderTarget(0);
//            	MyDirect3D.device.GetFrontBufferData(0, surface);
            	Surface.ToFile(surface, screenshot, ImageFileFormat.Bmp);
//            	SurfaceLoader.Save(screenshot, ImageFileFormat.Png, surface);
            	surface.Dispose();
            }
        }

        public void Render()
        {
            if (MyDirect3D.device == null) return;
            if (MainForm.in_editor) goto Label_new;
            MyDirect3D.device.BeginScene();
            MyDirect3D.ResetViewports(игроки.Length);
            MyDirect3D.SetViewport(-1);
            MyDirect3D.device.Clear(ClearFlags.ZBuffer | ClearFlags.Target, 0, 1f, 0);
            if (!активна)
            {
            	menu.Draw();
            	goto Label_rend;
            }
            Label_new:
            for (var i = 0; i < игроки.Length; i++)
            {
                MyDirect3D.SetViewport(i);
                MyDirect3D.device.Clear(ClearFlags.ZBuffer | ClearFlags.Target, 0xb4ff, 1f, 0);
//                var игрок1 = игроки[i];
//                игрок1.cameraPosition += игроки[i].cameraPositionChange;
//                var игрок2 = игроки[i];
//                игрок2.cameraPositionChange = игрок2.cameraPositionChange / 3.0;
//                var игрок3 = игроки[i];
//                игрок3.cameraRotation += игроки[i].cameraRotationChange;
//                var игрок4 = игроки[i];
//                игрок4.cameraRotationChange = игрок4.cameraRotationChange / 3.0;
                игроки[i].cameraPosition += игроки[i].cameraPositionChange;
                игроки[i].cameraPositionChange = игроки[i].cameraPositionChange / 3.0;
                игроки[i].cameraRotation += игроки[i].cameraRotationChange;
                if (игроки[i].cameraRotation.x > Math.PI) игроки[i].cameraRotation.x -= Math.PI * 2.0;
                else if (игроки[i].cameraRotation.x < -Math.PI) игроки[i].cameraRotation.x += Math.PI * 2.0;
                игроки[i].cameraRotation.y = Math.Min(Math.Max(игроки[i].cameraRotation.y, -Math.PI / 2.0), Math.PI / 2.0);
                игроки[i].cameraRotationChange = игроки[i].cameraRotationChange / 3.0;
                MyDirect3D.SetCameraPos(игроки[i].cameraPosition, игроки[i].cameraRotation);
                //
                col = (int)Math.Floor(игроки[i].cameraPosition.x / (double)Ground.grid_size);
                row = (int)Math.Floor(игроки[i].cameraPosition.z / (double)Ground.grid_size);
                //
                MyDirect3D.ComputeFrustum();
//                lock (MeshObject.timer/*.renderobj*/)
//                {
//                	MeshObject.timer.Refresh();
					мир.RenderMeshes();
					MeshObject.RenderList();
					MyDirect3D.Alpha = true;
					мир.RenderMeshesA();
					MeshObject.RenderListA();
					MyDirect3D.Alpha = false;
	                /*мир.RenderMeshes();
	                MeshObject.RenderList();
	                MyDirect3D.Alpha = true;
	                мир.RenderMeshes();
	                MeshObject.RenderList();
	                MyDirect3D.Alpha = false;*/
//                }
                if (игроки[i].управляемыйОбъект != null)
                {
                	var _transport = (Transport) игроки[i].управляемыйОбъект;
                	var speed_str = (_transport.скорость * 3.6).ToString("###0.00");
	                var control_str = "";
	                if (_transport.управление.автоматическое)
	                {
	                    control_str = _transport.управление.ручное ? Localization.current_.ctrl_s : Localization.current_.ctrl_a;
	                }
	                else
	                {
	                    control_str = _transport.управление.ручное ? Localization.current_.ctrl_m : "-";
	                }
	                if (MainForm.debug)
	                {
	                    var str111 = "\nCS: " + ((_transport.currentStop != null) ? _transport.currentStop.название : "")
	                    	+ "\nNS: " + ((_transport.nextStop != null) ? _transport.nextStop.название : "")
	                    	+ "\nSI: " + _transport.stopIndex
	                        + "\n\nX: " + _transport.Координаты3D.x.ToString("#0.0")
	                        + "\nY: " + _transport.Координаты3D.y.ToString("#0.0")
	                        + "\nZ: " + _transport.Координаты3D.z.ToString("#0.0")
	                        + "\nrY: " + (_transport.direction * 180.0 / Math.PI).ToString("#0.0")
	                    	+ "\nrZ: " + (_transport.НаправлениеY * 180.0 / Math.PI).ToString("#0.0");
	                    MyGUI.default_font.DrawString(null, str111, (int) (420 + MyDirect3D.device.Viewport.X), (int) (15 + MyDirect3D.device.Viewport.Y), Color.Black);
	                }
	                if (_transport is Трамвай)//(игроки[i].управляемыйОбъект is Трамвай)
	                {
	                    var трамвай = (Трамвай) _transport;//игроки[i].управляемыйОбъект;
	                    var str = "-";
	                    if (трамвай.система_управления is Система_управления.РКСУ_Трамвай)
	                    {
		                    var трамвай2 = (Система_управления.РКСУ_Трамвай) трамвай.система_управления;
		                    switch (трамвай2.позиция_контроллера)
		                    {
		                        case -5:
		                            str = "ТР";
		                            break;
		
		                        case -4:
		                            str = "Т4";
		                            break;
		
		                        case -3:
		                            str = "Т3";
		                            break;
		
		                        case -2:
		                            str = "Т2";
		                            break;
		
		                        case -1:
		                            str = "Т1";
		                            break;
		
		                        case 0:
		                            str = "0";
		                            break;
		
		                        case 1:
		                            str = "М";
		                            break;
		
		                        case 2:
		                            str = "Х1";
		                            break;
		
		                        case 3:
		                            str = "Х2";
		                            break;
		
		                        case 4:
		                            str = "Х3";
		                            break;
		                    }
		                    var str2 = (трамвай2.позиция_реверсора == 1) ? Localization.current_.forward : (трамвай2.позиция_реверсора == -1) ? Localization.current_.back : "0";
		                    /*if (трамвай2.позиция_реверсора == 1)
		                    {
		                        str2 = Localization.current_.forward;
		                    }
		                    if (трамвай2.позиция_реверсора == -1)
		                    {
		                        str2 = Localization.current_.back;
		                    }*/
		                    str = str + "\n" + Localization.current_.reverse + ": " + str2;
	                    }
	                    str = str + "\n" + ((трамвай.токоприёмник.поднят) ? Localization.current_.tk_on : Localization.current_.tk_off)
	                    	 + "\n" + Localization.current_.parking_brake + " " + (трамвай.stand_brake ? Localization.current_.enable : Localization.current_.disable);
	                    var str5 = трамвай.маршрут.number;
	                    if (трамвай.в_парк)
	                    {
	                        str5 = str5 + " (" + Localization.current_.route_in_park + ")";
	                    }
	                    if (трамвай.наряд != null)
	                    {
//	                        var str15 = str5;
	                        str5 = str5 + "\n" + Localization.current_.order + ": " + трамвай.наряд.маршрут.number + "/" + трамвай.наряд.номер;
	                        if (трамвай.рейс != null)
	                        {
	                            if (мир.time < трамвай.рейс.время_отправления)
	                            {
	                                str5 = str5 + "\n" + Localization.current_.departure_time + ": " + трамвай.рейс.str_время_отправления;
	                            }
	                            str5 = str5 + "\n" + Localization.current_.arrival_time + ": " + трамвай.рейс.str_время_прибытия;
	                            if (((трамвай.рейс_index < (трамвай.рейс.pathes.Length - 1)) && (трамвай.передняя_ось.текущий_рельс.следующие_рельсы.Length > 1)) && ((трамвай.рейс_index > 0) || (трамвай.передняя_ось.текущий_рельс == трамвай.рейс.pathes[0])))
	                            {
	                                var дорога = трамвай.рейс.pathes[трамвай.рейс_index + 1];
	                                var str6 = Localization.current_.nr_pryamo;
	                                if (дорога.кривая)
	                                {
	                                    if (дорога.СтепеньПоворота0 > 0.0)
	                                    {
	                                        str6 = Localization.current_.nr_right;
	                                    }
	                                    else if (дорога.СтепеньПоворота0 < 0.0)
	                                    {
	                                        str6 = Localization.current_.nr_left;
	                                    }
	                                }
	                                str5 = str5 + "\n" + Localization.current_.nr + ": " + str6;
	                            }
	                        }
	                    }
	                    MyGUI.default_font.DrawString(null, Localization.current_.tram_control + ": " + control_str + "\n" + Localization.current_.ctrl_pos + ": " + str + "\n" + Localization.current_.speed + ": " + speed_str + " " + Localization.current_.speed_km +  "\n" + Localization.current_.route + ": " + str5, (int) (15 + MyDirect3D.device.Viewport.X), (int) (15 + MyDirect3D.device.Viewport.Y), Color.Black);
	                }
	                if (_transport is Троллейбус)// (игроки[i].управляемыйОбъект is Троллейбус)
	                {
	                    var троллейбус = (Троллейбус) _transport;//игроки[i].управляемыйОбъект;
	                    var str7 = "-";
	                    var str8 = "неизвестно чем";
	                    if (троллейбус.система_управления is Система_управления.РКСУ_Троллейбус)
	                    {
	                        str8 = Localization.current_.trol_control;
	                        str7 = "\n" + Localization.current_.ctrl_pos + ": ";
	                        var троллейбус2 = (Система_управления.РКСУ_Троллейбус) троллейбус.система_управления;
	                        switch (троллейбус2.позиция_контроллера)
	                        {
	                            case -2:
	                                str7 = str7 + "Т2";
	                                break;
	
	                            case -1:
	                                str7 = str7 + "Т1";
	                                break;
	
	                            case 0:
	                                str7 = str7 + "0";
	                                break;
	
	                            case 1:
	                                str7 = str7 + "М";
	                                break;
	
	                            case 2:
	                                str7 = str7 + "Х1";
	                                break;
	
	                            case 3:
	                                str7 = str7 + "Х2";
	                                break;
	
	                            case 4:
	                                str7 = str7 + "Х3";
	                                break;
	                        }
	                        str7 = str7 + "\n" + Localization.current_.air_brake + ": " + ((троллейбус2.пневматический_тормоз * 100.0)).ToString("0") + "%";
	                        var str9 = (троллейбус2.позиция_реверсора == 1) ? Localization.current_.forward : (троллейбус2.позиция_реверсора == -1) ? Localization.current_.back : "0";
	                        str7 = str7 + "\n" + Localization.current_.reverse + ": " + str9;
	                        str7 = str7 + "\n" + ((троллейбус.штанги_подняты) ? Localization.current_.st_on : Localization.current_.st_off);
	                        str7 = str7 + "\n" + Localization.current_.trol + " " + ((троллейбус.включен) ? Localization.current_.enable : Localization.current_.disable);
	                        if (троллейбус.ах != null)
	                        {
	                        	str7 = str7 + "\n" + Localization.current_.ax + " " + (троллейбус.ах.включён ? Localization.current_.enable : Localization.current_.disable) + "\n" + Localization.current_.ax_power + ": " + (троллейбус.ах.текущая_ёмкость / троллейбус.ах.полная_ёмкость).ToString("##0%");
	                        }
	                    }
	                    else if (троллейбус.система_управления is Система_управления.КП_Авто)
	                    {
	                        str8 = Localization.current_.bus_control;
	                        var авто = (Система_управления.КП_Авто) троллейбус.система_управления;
	                        str7 = (("\n" + Localization.current_.gmod + ": " + авто.текущий_режим) + "\n" + Localization.current_.cur_pos + ": " + авто.текущая_передача) + "\n" + Localization.current_.pedal_pos + ": ";
	                        if (авто.положение_педалей > 0.0)
	                        {
	                            str7 = str7 + Localization.current_.gas + " ";
	                        }
	                        if (авто.положение_педалей < 0.0)
	                        {
	                            str7 = str7 + Localization.current_.brake + " ";
	                        }
	                        str7 = str7 + ((Math.Abs(авто.положение_педалей) * 100.0)).ToString("0") + "%"
	                        	+ "\n" + Localization.current_.engine + " " + (троллейбус.включен ? Localization.current_.enable : Localization.current_.disable);
	                    }
	                    str7 = str7 + "\n" + Localization.current_.parking_brake + " " + (троллейбус.stand_brake ? Localization.current_.enable : Localization.current_.disable);
	                    if (троллейбус.поворотРуля > 0.0)
	                    {
	                        str7 = str7 + "\n" + Localization.current_.sterling + ": " + (((троллейбус.поворотРуля * 180.0) / 3.1415926535897931)).ToString("0") + "\x00b0 " + Localization.current_.ster_r;
	                    }
	                    else if (троллейбус.поворотРуля < 0.0)
	                    {
	                        str7 = str7 + "\n" + Localization.current_.sterling + ": " + (((-троллейбус.поворотРуля * 180.0) / 3.1415926535897931)).ToString("0") + "\x00b0 " + Localization.current_.ster_l;
	                    }
	                    else
	                    {
	                        str7 = str7 + "\n" + Localization.current_.sterling + ": " + Localization.current_.nr_pryamo;
	                    }
	                    var str12 = троллейбус.маршрут.number;
	                    if (троллейбус.в_парк)
	                    {
	                        str12 = str12 + " (" + Localization.current_.route_in_park + ")";
	                    }
	                    if (троллейбус.наряд != null)
	                    {
	                        var str16 = str12;
	                        str12 = str16 + "\n" + Localization.current_.order + ": " + троллейбус.наряд.маршрут.number + "/" + троллейбус.наряд.номер;
	                        if (троллейбус.рейс != null)
	                        {
	                            if (мир.time < троллейбус.рейс.время_отправления)
	                            {
	                                str12 = str12 + "\n" + Localization.current_.departure_time + ": " + троллейбус.рейс.str_время_отправления;
	                            }
	                            str12 = str12 + "\n" + Localization.current_.arrival_time + ": " + троллейбус.рейс.str_время_прибытия;
	                            if ((((троллейбус.рейс_index < (троллейбус.рейс.pathes.Length - 1)) && (троллейбус.положение.Дорога != null)) && (троллейбус.положение.Дорога.следующиеДороги.Length > 1)) && ((троллейбус.рейс_index > 0) || (троллейбус.положение.Дорога == троллейбус.рейс.pathes[0])))
	                            {
	                                var дорога2 = троллейбус.рейс.pathes[троллейбус.рейс_index + 1];
	                                var str13 = Localization.current_.nr_pryamo;
	                                if (дорога2.кривая)
	                                {
	                                    if (дорога2.СтепеньПоворота0 > 0.0)
	                                    {
	                                        str13 = Localization.current_.nr_right;
	                                    }
	                                    else if (дорога2.СтепеньПоворота0 < 0.0)
	                                    {
	                                        str13 = Localization.current_.nr_left;
	                                    }
	                                }
	                                str12 = str12 + "\n" + Localization.current_.nr + ": " + str13;
	                            }
	                        }
	                    }
	                    MyGUI.default_font.DrawString(null, str8 + ": " + control_str + str7 + "\n" + Localization.current_.speed + ": " + speed_str + " " + Localization.current_.speed_km + "\n" + Localization.current_.route + ": " + str12, 15 + MyDirect3D.device.Viewport.X, 15 + MyDirect3D.device.Viewport.Y, Color.Black);
	                }
                }
                if (((MyDirect3D.device.Viewport.X + MyDirect3D.device.Viewport.Width) == MyDirect3D.Window_Width) &&
                    (MyDirect3D.device.Viewport.Y == 0))// continue;
                {
                	MyGUI.default_font.DrawString(null, ConvertTime.TimeFromSeconds(мир.time % 86400.0), MyDirect3D.Window_Width - 105/*0x69*/, 15, Color.Black);
//                	MyGUI.default_font.DrawString(null, "Понедельник".PadLeft(27), MyDirect3D.Window_Width - 398, 15, Color.Black);
                }
                if (!MainForm.debug) continue;
                var _str = "\ndTmax: " + World.dtmax.ToString("#0.000") + "\nFPS: " + World.fps.ToString("#0.0")
                	+ "\nX: " + MyDirect3D.Camera_Position.x.ToString("#0.0")
	                + "\nY: " + MyDirect3D.Camera_Position.y.ToString("#0.0")
	                + "\nZ: " + MyDirect3D.Camera_Position.z.ToString("#0.0")
	                + "\nrY: " + MyDirect3D.Camera_Rotation.x.ToString("#0.000")
	                + "\nrZ: " + MyDirect3D.Camera_Rotation.y.ToString("#0.000");
                MyGUI.default_font.DrawString(null, _str, new Rectangle(MyDirect3D.Window_Width - 160, 15, 160, 500), DrawTextFormat.Right, Color.Black);
//                MyGUI.default_font.DrawString(null, _str, MyDirect3D.Window_Width - 160, 15, Color.Black);
            }
            Label_rend:
            MyDirect3D.device.EndScene();
            MyDirect3D.device.Present();
        }

        private void Привязывать(Игрок игрок)
        {
            if (игрок.объектПривязки != null)
            {
                игрок.объектПривязки = null;
            }
            else
            {
                double num = 200.0;
                IVector _привязки = null;
                IControlledObject _объект = null;
                foreach (Transport транспорт in this.мир.транспорты)
                {
                    DoublePoint point = транспорт.position - игрок.cameraPosition.xz_point;
                    double num2 = point.модуль;
                    if (num2 < num)
                    {
                        num = num2;
                        _привязки = транспорт;
                        _объект = транспорт;
                    }
                }
                игрок.объектПривязки = _привязки;
                if ((игрок.управляемыйОбъект != null) && (игрок.управляемыйОбъект != _объект))
                {
                    игрок.управляемыйОбъект.управление = Управление.Автоматическое;
                }
                игрок.управляемыйОбъект = _объект;
            }
        }

        public void Сохранить(string filename)
        {
        }
        
        /*public void LoadLocalization()
        {
        	this.loc = Localization.localization;
        }*/
    }
}