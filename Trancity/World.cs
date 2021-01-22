using System;
using System.Collections.Generic;
using System.Xml;
using System.Windows.Forms;
using System.Diagnostics;
using Common;
using SlimDX;
using Engine;

namespace Trancity
{
    public class World
    {
        public string filename;
        public MyList list������ = new MyList(new[] { typeof(Road), typeof(�����) });
        public double time;
        public Ground ����� = new Ground();
        public ����������_������[] ����������������� = new ����������_������[0];
        public ����������_����������_������[] �����������������2 = new ����������_����������_������[0];
        public Route[] �������� = new Route[0];
        public List<Stop> ��������� = new List<Stop>();
        public ����[] ����� = new[] { new ����("����") };
        public static double �������������;
        public �����������_�������[] ������������������ = new �����������_�������[0];
        public ����������_�������[] ����������������� = new ����������_�������[0];
        public double ��������������;
        public MyList ���������� = new MyList(new Type[0]);
        public List<������> ������� = new List<������>();
        public double time_speed = 1000.0;
//        public SimpleTimer simple_timer = new SimpleTimer(0.175);
        public SkyBox skybox = new SkyBox();
        public static double dtmax = 0.0;

        public void Create_Meshes()
        {
			Common.MyGUI.status_string = Localization.current_.load_models;//"�������� �������...";
            Common.MyGUI.Splash();
            for (var i = 0; i < ���������.Length; i++)
            {
                if ((Environment.TickCount - MainForm.ticklast) > 50)
                {
                    var num2 = ((Common.MyGUI.load_max / 20) * i) / ���������.Length;//MyDirect3D.
                    if (num2 > Common.MyGUI.load_status)
                    {
                        Common.MyGUI.load_status = num2;
                        Common.MyGUI.Splash();
                    }
                }
                ���������[i].CreateMesh();
//                ���������[i].Test();
                if (���������[i] is �����)
                {
                	if ((!MainForm.in_editor) && (((�����)���������[i]).���������_������.Length <= 1)) continue;
                    ((�����)���������[i]).����������_�������.CreateMesh();
                }
            }
            for (var j = 0; j < �����������������.Length; j++)
            {
                if ((Environment.TickCount - MainForm.ticklast) > 50)
                {
                    var num4 = (((Common.MyGUI.load_max / 20) * j) / �����������������.Length) + (Common.MyGUI.load_max / 20);
                    if (num4 > Common.MyGUI.load_status)
                    {
                        Common.MyGUI.load_status = num4;
                        Common.MyGUI.Splash();
                    }
                }
                �����������������[j].CreateMesh();
            }
            for (var j = 0; j < �����������������2.Length; j++)
            {
                if ((Environment.TickCount - MainForm.ticklast) > 50)
                {
                    var num5 = (((Common.MyGUI.load_max / 20) * j) / �����������������2.Length) + (Common.MyGUI.load_max / 20);
                    if (num5 > Common.MyGUI.load_status)
                    {
                        Common.MyGUI.load_status = num5;
                        Common.MyGUI.Splash();
                    }
                }
                �����������������2[j].CreateMesh();
            }
            for (var k = 0; k < ����������.Count; k++)
            {
                var num6 = ((((Common.MyGUI.load_max * 0x12) / 20) * k) / ����������.Count) + (Common.MyGUI.load_max / 20);
                if (num6 > Common.MyGUI.load_status)
                {
                    Common.MyGUI.load_status = num6;
                    Common.MyGUI.Splash();
                }
                ((Transport)this.����������[k]).CreateMesh(this);
            }
            Common.MyGUI.load_status = Common.MyGUI.load_max;
            Common.MyGUI.Splash();
            foreach (var ������� in �����������������)
            {
                �������.CreateMesh();
            }
            foreach (var _�������2 in this.������������������)
            {
                _�������2.CreateMesh();
            }
            this.�����.CreateMesh();
            if ((SkyBox.draw) && (!MainForm.in_editor))
            {
            	Common.MyGUI.status_string = Localization.current_.load_shaders;//"�������� ��������...";
            	Common.MyGUI.Splash();
            	this.skybox.CreateMesh();
            }
            Common.MyGUI.load_status = 0;
            Common.MyGUI.status_string = Localization.current_.load_objects;//"�������� ��������...";
            Common.MyGUI.Splash();
            int obj_count = �������.Count > 1 ? �������.Count - 1 : �������.Count;
            for (var z = 0; z < �������.Count; z++)
            {
            	if ((Environment.TickCount - MainForm.ticklast) > 50)
                {
                    int num21 = (Common.MyGUI.load_max * z) / (obj_count);
                    if (num21 > Common.MyGUI.load_status)
                    {
                        Common.MyGUI.load_status = num21;
                        Common.MyGUI.Splash();
                    }
                }
                �������[z].CreateMesh();
            }
            Common.MyGUI.load_status = Common.MyGUI.load_max;
            Common.MyGUI.Splash();
            Common.MyGUI.load_status = 0;
            Common.MyGUI.status_string = Localization.current_.load_stops;//"�������� ���������...";
            Common.MyGUI.Splash();
            int stops_count = this.���������.Count > 1 ? this.���������.Count - 1 : this.���������.Count;
            for (int m = 0; m < this.���������.Count; m++)
            {
                if ((Environment.TickCount - MainForm.ticklast) > 50)
                {
                    int num8 = (Common.MyGUI.load_max * m) / (stops_count);
                    if (num8 > Common.MyGUI.load_status)
                    {
                        Common.MyGUI.load_status = num8;
                        Common.MyGUI.Splash();
                    }
                }
                this.���������[m].CreateMesh();
                this.���������[m].����������������();
                if (!MainForm.in_editor) this.���������[m].ComputeMatrix();
            }
            Common.MyGUI.load_status = Common.MyGUI.load_max;
            Common.MyGUI.Splash();
        }
        
        public void CreateSound()
        {
            Common.MyGUI.load_status = 0;
            Common.MyGUI.status_string = Localization.current_.load_sounds;
            Common.MyGUI.Splash();
            int trs_count = this.����������.Count > 1 ? this.����������.Count - 1 : this.����������.Count;
            for (int m = 0; m < this.����������.Count; m++)
            {
                if ((Environment.TickCount - MainForm.ticklast) > 50)
                {
                    int num8 = (Common.MyGUI.load_max * m) / (trs_count);
                    if (num8 > Common.MyGUI.load_status)
                    {
                        Common.MyGUI.load_status = num8;
                        Common.MyGUI.Splash();
                    }
                }
                ((Transport)this.����������[m]).CreateSoundBuffers();
            }
            Common.MyGUI.load_status = Common.MyGUI.load_max;
            Common.MyGUI.Splash();
        }

        public void RenderMeshes()
        {
            foreach (var ������ in ������)
            {
//            	������.Render();
            	if (������.���������_������.Length <= 1) continue;
                ������.����������_�������.Render();
            }
            foreach (var ������ in �����������������)
            {
                ������.Render();
            }
            foreach (var ������2 in �����������������2)
            {
                ������2.Render();
            }
            foreach (Transport ��������� in ����������)
            {
                ���������.Render();
            }
            foreach (var ������� in �����������������)
            {
                �������.Render();
            }
            foreach (var �������2 in ������������������)
            {
                �������2.Render();
            }
            foreach (var ��������� in ���������)
            {
            	���������.CheckCondition();
            	���������.Render();
            }
            foreach (var ������ in �������)
            {
            	������.CheckCondition();
            	������.Render();
            }
//            �����.IsNear = true;
//            �����.Render();
        }
        
        public void RenderMeshes2()
        {
        	skybox.Render();
        	�����.Render();
        }
        
        public void RenderMeshesA()
        {
            foreach (var ������ in ���������)
            {
            	������.Render();
            	/*if ((!(������ is �����)) || (((�����)������).���������_������.Length <= 1)) continue;
                ((�����)������).����������_�������.Render();*/
            }
        }

        public void UpdateSound(�����[] ������, bool ����_�������)
        {
//        	var pnt = DoublePoint.Zero;
//        	var pnt2 = Double3DPoint.Zero;
        	MyXAudio2.Device.UpdateListner(ref /*pnt2/**/������[0].cameraPosition/**/,
        	                               ref /*pnt/**/������[0].cameraRotation/**/);
            foreach (Transport ��������� in this.����������)
            {
                ���������.UpdateSound(������, ����_�������);
            }
        }

        public void �����������������(MainForm.���������������� ���������, Game ����)
        {
            if (���� == null)
            {
                ���� = new Game();
            }
            var list = ���������;
            for (var i = 0; i < list.Count; i++)
            {
                var index = list[i].�����.Length - 1;
                if ((index >= 0) && (list[i].�����[index].�����_�������� >= time)) continue;
                list.RemoveAt(i);
                i--;
            }
            var tr_by_parks = new List<Transport>[�����.Length];
            for (int i = 0; i < �����.Length; i++)
            {
            	tr_by_parks[i] = new List<Transport>();
            }
            ����.������ = new �����[���������.�����������������];
            for (var j = 0; j < ���������.�����������������; j++)
            {
                Transport ��������� = null;
                Road ������;
                var ������ = ���������.������[j];
                ����.������[j] = new �����();
                ����.������[j].cameraPositionChange = Double3DPoint.Zero;
                ����.������[j].cameraRotationChange = DoublePoint.Zero;
                ����.������[j].inputGuid = ������.inputGuid;
                ����.������[j].������������������ = ���������.������������������;
//                var ���������� = TypeOfTransport.Tramway;
//                var ����������� = new ����������������();
                ���������������� ����������� = null;
                int ���������� = -1;
                foreach (var �������2 in ������.�������)
                {
                	if (������.��������������� != �������2.name) continue;
                	���������� = TypeOfTransport.Tramway;
                	����������� = �������2;
                }
                foreach (var �����������2 in ������.�����������)
                {
                    if (������.��������������� != �����������2.name) continue;
                    ���������� = TypeOfTransport.Trolleybus;
                    ����������� = �����������2;
                }
                foreach (var �����������3 in ������.��������)
                {
                    if (������.��������������� != �����������3.name) continue;
                    ���������� = TypeOfTransport.Bus;
                    ����������� = �����������3;
                }
                if ((����������� == null) || (���������� == -1))
                {
					Logger.Log("World", Localization.current_.nulltran + ������.��������������� + Localization.current_.forplayer + ������.���);
                	continue;
                }
                var ���������� = ���������.������������������������ ? ����������.�������������� : ����������.������;
                var ������� = new Route(����������, "-");
                if ((������.������� > 0) && (��������.Length > 0))
                {
                    if ((������.������� > 1) && ((������.������� - 2) < ��������.Length))
                    {
                    	������� = ��������[(������.������� - 2)];
                    }
                    else
                    {
                    	try
                    	{
                    		List<Route> current_routes = new List<Route>();
	                    	foreach (var route in ��������)
	                    	{
	                    		if (route.typeOfTransport != ����������) continue;
	                    		current_routes.Add(route);
	                    	}
	                    	������� = current_routes[Cheats._random.Next(current_routes.Count)];
                    	}
                    	catch
                    	{
                    		Logger.Log("World", Localization.current_.nullrandom + ������.���);
                    	}
                    }
                }
                var ���� = �����[Cheats._random.Next(�����.Length)];
                Order item = new Order(����, �������, "-", ������.���������������);
                if ((������.����� > 1) && ((������.����� - 2) < �������.orders.Length))
                {
                    item = �������.orders[������.����� - 2];
                }
                else if (������.����� == 1)
                {
                	try
                	{
                		item = �������.orders[Cheats._random.Next(�������.orders.Length)];
                	}
                	catch
                	{
                		Logger.Log("World", Localization.current_.nullorder + ������.���);
                	}
                }
                if (���������� != TypeOfTransport.Tramway)
                {
                	try
                	{
                		������ = ������[Cheats._random.Next(������.Length)];
                	}
                	catch
                	{
                		������ = new Road(0.0, 0.0, 20.0, 0.0, 0.0, true, 1.0, 1.0);
                		������.��������������� = new[] { ������ };
                    	������.���������������� = new[] { ������ };
                    	������.�������������� = new[] { ������ };
                	}
                }
                else
                {
                	try
                	{
                		������ = ������[Cheats._random.Next(������.Length)];
                	}
                	catch
                	{
                		������ = new �����(0.0, 0.0, 20.0, 0.0, 0.0, true);
                    	������.��������������� = new[] { ������ };
                    	������.���������������� = new[] { ������ };
                    	������.�������������� = new[] { ������ };
                	}
                }
                var num4 = Cheats._random.NextDouble() * ������.�����;
                Trip ���� = null;
                bool from_park = false;
                if (item != null)
                {
                    list.Remove(item);
                    ���� = item.����;
                    �����_���������_������(item, ref ����, ref ������, ref num4, ref from_park);
                }
                else if (�������.trips.Count > 0)
                {
                    ���� = �������.trips[0];
                }
                ����.������[j].cameraPosition = new Double3DPoint(0.0, 2.0, 0.0);
                ����.������[j].cameraRotation = new DoublePoint(0.0, -0.1);
                switch (����������)
                {
                	case TypeOfTransport.Tramway:
	                	{
	                		if (!(������ is �����))
	                        {
	                        	Logger.Log("World", Localization.current_.tramofrali + ������.���);
	                        	break;
	                        }
	                        ��������� = new �������.��������������(�����������, (�����)������, num4, ����������, ����, �������, item);//, true);
	                    }
                		break;
                    case TypeOfTransport.Trolleybus:
                    case TypeOfTransport.Bus:
	                    {
	                        var point = new Double3DPoint
	                            {
	                                XZPoint = ������.���������������(num4, 0.0),
	                                y = ������.�����������(num4)
	                            };
	                        var point2 = new DoublePoint(������.����������������(num4), ������.����������������Y(num4));
	                        ��������� = new ����������.�����������������(�����������, point, point2, ����������, ����, �������, item);
	                        ���������.SetPosition(������, num4, 0.0, Double3DPoint.Zero, DoublePoint.Zero, this);
	                    }
                		break;
                }
                ����.������[j].����������������� = ���������;
                ����.������[j].�������������� = ���������;
                switch (����������)
                {
           case TypeOfTransport.Tramway:
                        {
                            if (!(������ is �����))
                            {
                                Logger.Log("World", Localization.current_.tramofrali + ������.���);
                                break;
                            }
                            ��������� = new �������.��������������(�����������, (�����)������, num4, ����������, ����, �������, item);//, true);
                        }
                        break;
                        }
                ���������.���� = ����;
                if (��������� is ����������)
                {
                    foreach (var ������ in ((����������)���������).������)
                    {
                        ������.�����������(�����������������);
                        if (������.������ != null)
                        {
                        	������.����������� = true;
                        	������.��������(false);//, 0.0f);
                            ������.���� = ������.����Normal;
                        }
                    }
                }
                else
                {
                	var pant = ((�������)���������).�����������;
                	((�������)���������).��������(this, new �����[0]);//����������� ������� ��������
                	pant.�����������(�����������������2);
                	if (pant.������ != null)
                	{
	                	pant.����������� = true;
	                	pant.������ = pant.�������_������_max;
                	}
                }
//                var point4 = ���������.position + new DoublePoint(���������.direction) * 9.5;//9.5
//                ����.������[j].cameraPosition = new Double3DPoint(point4.x, 2.5, point4.y);
				if (!���������.SetCamera(0, ����.������[j]))
				{
                	����.������[j].cameraRotation = new DoublePoint(���������.direction, ���������.�����������Y - 0.1);
                	����.������[j].cameraPosition = Double3DPoint.Multiply(new Double3DPoint(8.0, 2.5, 0.0), ���������.����������3D, ����.������[j].cameraRotation);
                }
                ����������.Add(���������);
                if (from_park)
                {
                	for (int i = 0; i < �����.Length; i++)
		            {
                		if (���� != �����[i]) continue;
		            	tr_by_parks[i].Add(���������);
		            }
                }
            }
            for (var k = 0; k < list.Count; k++)
            {
                Road ������2 = null;
                var distance = 0.0;
                Trip ����2 = null;
                bool from_park = false;
                �����_���������_������(list[k], ref ����2, ref ������2, ref distance, ref from_park);
                if ((����2 == null) || (������2 == null)) continue;
                Transport ���������2 = null;
                switch (list[k].�������.typeOfTransport)
                {
                    case TypeOfTransport.Tramway:
                		{
                			if (������.�������.Count == 0) continue;
                			���������������� ����4 = null;
                			if (list[k].transport == "" || list[k].transport == Localization.current_.random)
                            {
                                ����4 = ������.�������[Cheats._random.Next(0, ������.�������.Count)];
                            }
                			else
                			{
                                ����4 = null;
                                foreach (var ������� in ������.�������)
                                {
                                    if (�������.name == list[k].transport)
                                    {
                                        ����4 = �������;
                                        break;//������.�������[i];
                                    }
                                }
                                if (����4 == null)
                                {
                                    ����4 = ������.�������[Cheats._random.Next(0, ������.�������.Count)];
                                }
                			}
                			if (!(������2 is �����))
                			{
								Logger.Log("World", Localization.current_.tramofraliroute + list[k].����� + Localization.current_.tramofralirouteend);
                				break;
                			}
                			���������2 = new �������.��������������(����4, (�����)������2, distance, ����������.��������������, list[k].����, list[k].�������, list[k]);
                		}
                		break;
                    case TypeOfTransport.Trolleybus:
                        {
                			if (������.�����������.Count == 0) continue;
                            var point5 = new Double3DPoint
                                             {
                                                 XZPoint = ������2.���������������(distance, 0.0),
                                                 y = ������2.�����������(distance)
                                             };
                            var point6 = new DoublePoint(������2.����������������(distance), ������2.����������������Y(distance));
                            ���������������� �����������4 = null;
                            if (list[k].transport == "" || list[k].transport == Localization.current_.random)
                            {
                                �����������4 = ������.�����������[Cheats._random.Next(0, ������.�����������.Count)];
                            }
                            else
                            {
                                �����������4 = null;
                                foreach (var ���������� in ������.�����������)
                                {
                                    if (����������.name == list[k].transport)
                                    {
                                        �����������4 = ����������;
                                        break;//������.�����������[i];
                                    }
                                }
                                if (�����������4 == null)
                                {
                                    �����������4 = ������.�����������[Cheats._random.Next(0, ������.�����������.Count)];
                                }
                            }
                            ���������2 = new ����������.�����������������(�����������4, point5, point6, ����������.��������������, list[k].����, list[k].�������, list[k]);
                            ���������2.SetPosition(������2, distance, 0.0, Double3DPoint.Zero, DoublePoint.Zero, this);
//                            ((����������.�����������������)���������2).�����������������(this);
                        }
                        break;
                    default:
                        {
                            if (list[k].�������.typeOfTransport != TypeOfTransport.Bus)
                            {
                                throw new Exception(Localization.current_.trannull);
                            }
                            if (������.��������.Count == 0) continue;
                            var point7 = new Double3DPoint
                                             {
                                                 XZPoint = ������2.���������������(distance, 0.0),
                                                 y = ������2.�����������(distance)
                                             };
                            var point8 = new DoublePoint(������2.����������������(distance), ������2.����������������Y(distance));
                            ���������������� ������� = null;
                            if (list[k].transport == "" || list[k].transport == Localization.current_.random)
                            {
                                ������� = ������.��������[Cheats._random.Next(0, ������.��������.Count)];
                            }
                            else
                            {
                                ������� = null;
                                foreach (var �������������� in ������.��������)
                                {
                                    if (��������������.name == list[k].transport)
                                    {
                                        ������� = ��������������;
                                        break;//������.��������[i];
                                    }
                                }
                                if (������� == null)
                                {
                                    ������� = ������.��������[Cheats._random.Next(0, ������.��������.Count)];
                                }
                            }
                            ���������2 = new ����������.�����������������(�������, point7, point8, ����������.��������������, list[k].����, list[k].�������, list[k]);
                            ���������2.SetPosition(������2, distance, 0.0, Double3DPoint.Zero, DoublePoint.Zero, this);
//                            ((����������.�����������������)���������2).�����������������(this);
                        }
                        break;
                }
                if (���������2 == null) continue;
                ���������2.����� = list[k];
                ���������2.���� = ����2;
                if (���������2 is ����������)
                {
                    foreach (var ������ in ((����������)���������2).������)
                    {
                        ������.�����������(�����������������);
                        if (������.������ != null)
                        {
                        	������.����������� = true;
                        	������.��������(false);//, 0.0f);
                            ������.���� = ������.����Normal;
                        }
                    }
                }
                else
                {
                	var pant = ((�������)���������2).�����������;
                	((�������)���������2).��������(this, new �����[0]);
                	pant.�����������(�����������������2);
                	if (pant.������ != null)
                	{
	                	pant.����������� = true;
	                	pant.������ = pant.�������_������_max;
                	}
                }
                ����������.Add(���������2);
                if (from_park)
                {
                	for (int i = 0; i < �����.Length; i++)
		            {
                		if (���������2.���� != �����[i]) continue;
		            	tr_by_parks[i].Add(���������2);
		            }
                }
            }
//            ����.���������Array = (Transport[]) ����������.ToArray(typeof(Transport));
			//������� �� ������ � ����������� �� ������� ������:
			for (int i = 0; i < �����.Length; i++)
			{
				if (�����[i].����_�������.Length == 0) continue;
				int pathpos = 0;
				double dist = �����[i].����_�������[0].�����;
				while (tr_by_parks[i].Count > 0)
				{
					Transport transport = tr_by_parks[i][0];
					foreach (var trs in tr_by_parks[i])
					{
						if (transport.����.�����_����������� > trs.����.�����_�����������) transport = trs;
					}
					while (((dist - transport.length0) < -1.0) && (pathpos < �����[i].����_�������.Length - 1))
					{
						pathpos++;
						dist = �����[i].����_�������[pathpos].�����;
					}
					transport.SetPosition(�����[i].����_�������[pathpos], dist - transport.length0 + 1.0, 0.0, Double3DPoint.Zero, DoublePoint.Zero, this);
					dist -= transport.length0 + transport.length1;
					//TODO: ������������ ������:
					for (int j = 0; j < ����.������.Length; j++)
					{
						if (����.������[j].����������������� != transport) continue;
						if (!transport.SetCamera(0, ����.������[j]))
						{
	                		����.������[j].cameraRotation = new DoublePoint(transport.direction, transport.�����������Y - 0.1);
	                		����.������[j].cameraPosition = Double3DPoint.Multiply(new Double3DPoint(8.0, 2.5, 0.0), transport.����������3D, ����.������[j].cameraRotation);
						}
					}
					if (transport is ����������)
	                {
	                    foreach (var ������ in ((����������)transport).������)
	                    {
	                        ������.�����������(�����������������);
	                        if (������.������ != null)
	                        {
	                        	������.����������� = true;
	                        	������.��������(false);//, 0.0f);
	                            ������.���� = ������.����Normal;
	                        }
	                    }
	                }
	                else
	                {
	                	var pant = ((�������)transport).�����������;
	                	((�������)transport).��������(this, new �����[0]);
	                	pant.�����������(�����������������2);
	                	if (pant.������ != null)
	                	{
		                	pant.����������� = true;
		                	pant.������ = pant.�������_������_max;
	                	}
	                }
					tr_by_parks[i].Remove(transport);
				}
			}
        }

        public void ��������������(string filename)
        {
        	if (string.IsNullOrEmpty(filename)) return;
        	Logger.Log("LoadCity", string.Format("Loading {0}", filename));
        	XmlDocument document = Engine.Xml.TryOpenDocument(filename);
            var element = document["City"];
            list������.Clear();
            if (element == null) return;
            var element2 = element["Rails"];
            if (element2 != null)
            {
            	for (var i = 0; i < element2.ChildNodes.Count; i++)
            	{
            		XmlElement element3 = element2["rail" + i];
            		list������.Add(new �����(Engine.Xml.GetDouble(element3["x0"]), Engine.Xml.GetDouble(element3["y0"]), Engine.Xml.GetDouble(element3["x1"]), Engine.Xml.GetDouble(element3["y1"]), Engine.Xml.GetDouble(element3["angle0"]), Engine.Xml.GetDouble(element3["angle1"])));
            		if ((element3["height0"] != null) && (element3["height1"] != null))
            		{
            			������[i].������[0] = Engine.Xml.GetDouble(element3["height0"]);
            			������[i].������[1] = Engine.Xml.GetDouble(element3["height1"]);
            		}
            		this.������[i].����������_����������_�������� = Engine.Xml.GetDouble(element3["d_strel"]);
            		this.������[i].������ = Engine.Xml.GetDouble(element3["iskriv"]) != 0.0;
            		this.������[i].name = Engine.Xml.GetString(element3["name"], "Rails");
            	}
            }
            XmlElement element4 = element["Roads"];
            if (element4 != null)
            {
                for (int j = 0; j < element4.ChildNodes.Count; j++)
                {
                	XmlElement element5 = element4["road" + j.ToString()];
                    this.list������.Add(new Road(Engine.Xml.GetDouble(element5["x0"]), Engine.Xml.GetDouble(element5["y0"]), Engine.Xml.GetDouble(element5["x1"]), Engine.Xml.GetDouble(element5["y1"]), Engine.Xml.GetDouble(element5["angle0"]), Engine.Xml.GetDouble(element5["angle1"]), Engine.Xml.GetDouble(element5["wide0"]), Engine.Xml.GetDouble(element5["wide1"])));
                    if ((element5["height0"] != null) && (element5["height1"] != null))
                    {
                        this.������[j].������[0] = Engine.Xml.GetDouble(element5["height0"]);
                        this.������[j].������[1] = Engine.Xml.GetDouble(element5["height1"]);
                    }
                    this.������[j].������ = Engine.Xml.GetDouble(element5["iskriv"]) != 0.0;
                    this.������[j].name = Engine.Xml.GetString(element5["name"], "Road");
                }
            }
            foreach (Road ������ in this.���������)
            {
                ������.�����������������������(this.���������);
            }
            //TODO: �������� � �������� MatricesCount �������
            foreach (Road ������ in this.���������)
            {
//                ������.ComputeMatrix();
                ������.CreateBoundingSphere();
            }
            XmlElement element6 = element["Trolleybus_lines"];
            if (element6 != null)
            {
                this.����������������� = new ����������_������[element6.ChildNodes.Count];
                for (int k = 0; k < element6.ChildNodes.Count; k++)
                {
                	XmlElement element7 = element6["line" + k.ToString()];
                    this.�����������������[k] = new ����������_������(Engine.Xml.GetDouble(element7["x0"]), Engine.Xml.GetDouble(element7["y0"]), Engine.Xml.GetDouble(element7["x1"]), Engine.Xml.GetDouble(element7["y1"]), Engine.Xml.GetDouble(element7["right"]) != 0.0);
                    if ((element7["height0"] != null) && (element7["height1"] != null))
                    {
                        this.�����������������[k].������[0] = Engine.Xml.GetDouble(element7["height0"]);
                        this.�����������������[k].������[1] = Engine.Xml.GetDouble(element7["height1"]);
                    }
                    this.�����������������[k].������������ = Engine.Xml.GetDouble(element7["no_contact"]) != 0.0;
                }
            }
            foreach (����������_������ _������ in this.�����������������)
            {
                _������.UpdateNextWires(this.�����������������);
                _������.ComputeMatrix();
            }
            XmlElement element6_1 = element["Tramway_lines"];
            if (element6_1 != null)
            {
                this.�����������������2 = new ����������_����������_������[element6_1.ChildNodes.Count];
                for (int k = 0; k < element6_1.ChildNodes.Count; k++)
                {
                	XmlElement element7 = element6_1["line" + k.ToString()];
                    this.�����������������2[k] = new ����������_����������_������(Engine.Xml.GetDouble(element7["x0"]), Engine.Xml.GetDouble(element7["y0"]), Engine.Xml.GetDouble(element7["x1"]), Engine.Xml.GetDouble(element7["y1"]));
                    if ((element7["height0"] != null) && (element7["height1"] != null))
                    {
                        this.�����������������2[k].������[0] = Engine.Xml.GetDouble(element7["height0"]);
                        this.�����������������2[k].������[1] = Engine.Xml.GetDouble(element7["height1"]);
                    }
                    this.�����������������2[k].������������ = Engine.Xml.GetDouble(element7["no_contact"]) != 0.0;
                }
            }
            foreach (����������_����������_������ _������2 in this.�����������������2)
            {
                _������2.UpdateNextWires(this.�����������������2);
                _������2.ComputeMatrix();
            }
            XmlElement element8 = element["Parks"];
            if (element8 != null)
            {
                this.����� = new ����[element8.ChildNodes.Count];
                for (int m = 0; m < element8.ChildNodes.Count; m++)
                {
                    XmlElement element9 = element8["park" + m.ToString()];
                    this.�����[m] = new ����(element9["name"].InnerText);
                    int index = (int)Engine.Xml.GetDouble(element9["in"]);
                    if (index >= 0)
                    {
                        this.�����[m].����� = this.���������[index];
                    }
                    int num6 = (int)Engine.Xml.GetDouble(element9["out"]);
                    if (num6 >= 0)
                    {
                        this.�����[m].����� = this.���������[num6];
                    }
                    XmlElement element10 = element9["park_rails"];
                    this.�����[m].����_������� = new Road[element10.ChildNodes.Count];
                    for (int n = 0; n < element10.ChildNodes.Count; n++)
	                {
	                    this.�����[m].����_�������[n] = this.���������[(int)Engine.Xml.GetDouble(element10["park_rail" + n.ToString()])];
	                }
                }
            }
            var element23 = element["Stops"];
            if (element23 != null)
            {
                ��������� = new List<Stop>();
                var flag = false;
                for (var num17 = 0; num17 < element23.ChildNodes.Count; num17++)
                {
                    var element24 = element23[string.Format("stop{0}", num17)];
                    var name = element24["name"].InnerText;
                    try
                    {
                    	this.���������.Add(new Stop(Engine.Xml.GetString(element24["model"], "Stop (4 routes)"), new TypeOfTransport(TypeOfTransport.Tramway), this.���������[(int)Engine.Xml.GetDouble(element24["rail"])], Engine.Xml.GetDouble(element24["distance"])));
                    }
                    catch
                    {
                    	throw new IndexOutOfRangeException("�� ������� ��������� ���������[" + num17 + "] " + name);
                    }
                    XmlElement type = element24["type"];
                    if (type != null)
                    {
                        if (type.InnerText == "0" || type.InnerText == "1" || type.InnerText == "2")
                        {
                        	if (!flag)
                        	{
                        		Logger.Log("LoadCity", "������� ������ ����� (������ 0.6.2)");
                        		flag = true;
                        	}
                            ���������[num17].typeOfTransport[(int)Engine.Xml.GetDouble(element24["type"])] = true;
                        }
                        else
                        {
                            XmlNodeList nodes = type.ChildNodes;
                            ���������[num17].typeOfTransport[TypeOfTransport.Tramway] = nodes[TypeOfTransport.Tramway].InnerText == "True" ? true : false;
                            ���������[num17].typeOfTransport[TypeOfTransport.Trolleybus] = nodes[TypeOfTransport.Trolleybus].InnerText == "True" ? true : false;
                            ���������[num17].typeOfTransport[TypeOfTransport.Bus] = nodes[TypeOfTransport.Bus].InnerText == "True" ? true : false;
                        }
                    }
                    this.���������[num17].�������� = name;//element24["name"].InnerText;
                    XmlElement element25 = element24["stop_path"];
                    this.���������[num17].��������� = new Road[element25.ChildNodes.Count];
                    for (int num18 = 0; num18 < element25.ChildNodes.Count; num18++)
	                {
	                    this.���������[num17].���������[num18] = this.���������[(int)Engine.Xml.GetDouble(element25["stop_rail" + num18.ToString()])];
	                }
//                    this.���������[num17].����������������(this.��������);
                    this.���������[num17].UpdatePosition(this);
                }
            }
            var element11 = element["Routes"];
            if (element11 != null)
            {
                �������� = new Route[element11.ChildNodes.Count];
                for (var num8 = 0; num8 < element11.ChildNodes.Count; num8++)
                {
                    XmlElement element13 = null;
                    var element12 = element11[string.Format("route{0}", num8)];
                    if (element12 != null)
                    {
                        ��������[num8] = new Route(TypeOfTransport.Tramway, element12["name"].InnerText);
                        if (element12["type"] != null)
                        {
                            ��������[num8].typeOfTransport = ((int)Engine.Xml.GetDouble(element12["type"]));
                        }
                        element13 = element12["route_runs"];
                    }
                    if (element13 != null)
                        for (var num9 = 0; num9 < element13.ChildNodes.Count; num9++)
                        {
                            var element14 = element13[string.Format("run{0}", num9)];
                            if (element14 == null) continue;
                            var item = new Trip
                                           {
                                               �����_�������� = Engine.Xml.GetDouble(element14["time"])
                                           };
                            var element15 = element14["run_rails"];
                            if (element15 != null)
                            {
                                item.pathes = new Road[element15.ChildNodes.Count];
                                for (var num10 = 0; num10 < element15.ChildNodes.Count; num10++)
                                {
                                    item.pathes[num10] = ���������[(int)Engine.Xml.GetDouble(element15["run_rail" + num10])];
                                }
                            }
                            var tripStopsElement = element14["Stops"];
                            if (tripStopsElement != null)
                            {
                                item.tripStopList = new List<TripStop>();
                                int parse = 0;
                                for (int i = 0; i < tripStopsElement.ChildNodes.Count; i++)
                                {
                                    XmlNode node = tripStopsElement.ChildNodes[i];
                                    parse = int.Parse(node.Name.Substring(4));
                                    if (parse < 0) continue;
                                    Stop stop = ���������[parse];
                                    item.tripStopList.Add(new TripStop(stop, (node.InnerText == "��") || (node.InnerText == "1")));
                                }
                            }
                            else
                            {
                                item.InitTripStopList(��������[num8]);
                            }
                            ��������[num8].trips.Add(item);
                        }
                    if (element12 != null)
                    {
                        var element16 = element12["park_runs"];
                        if (element16 != null)
                            for (var num11 = 0; num11 < element16.ChildNodes.Count; num11++)
                            {
                                var element17 = element16["run" + num11];
                                if (element17 == null) continue;
                                var ����2 = new Trip
                                                {
                                                    inPark = Engine.Xml.GetDouble(element17["to_park"]) != 0.0,
                                                    inParkIndex = (int)Engine.Xml.GetDouble(element17["to_park_index"]),
                                                    �����_�������� = Engine.Xml.GetDouble(element17["time"])
                                                };
                                var element18 = element17["run_rails"];
                                if (element18 != null)
                                {
                                    ����2.pathes = new Road[element18.ChildNodes.Count];
                                    for (var num12 = 0; num12 < element18.ChildNodes.Count; num12++)
                                    {
                                        ����2.pathes[num12] = ���������[(int)Engine.Xml.GetDouble(element18["run_rail" + num12])];
                                    }
                                }
                                var tripStopsElement = element17["Stops"];
                                if (tripStopsElement != null)
                                {
                                    ����2.tripStopList = new List<TripStop>();
                                    int parse = 0;
                                    for (int i = 0; i < tripStopsElement.ChildNodes.Count; i++)
                                    {
                                        XmlNode node = tripStopsElement.ChildNodes[i];
                                        parse = int.Parse(node.Name.Substring(4));
                                    	if (parse < 0) continue;
                                        Stop stop = ���������[parse];
                                        ����2.tripStopList.Add(new TripStop(stop, (node.InnerText == "��") || (node.InnerText == "1")));
                                    }
                                }
                                else
                                {
                                    ����2.InitTripStopList(��������[num8]);
                                }
                                ��������[num8].parkTrips.Add(����2);
                            }
                    }
                    if (element12 == null) continue;
                    var element19 = element12["Narads"];
                    if (element19 == null) continue;
                    ��������[num8].orders = new Order[element19.ChildNodes.Count];
                    XmlElement element21 = null;
                    for (var num13 = 0; num13 < element19.ChildNodes.Count; num13++)
                    {
                        var element20 = element19[string.Format("narad{0}", num13)];
                        if (element20 != null)
                        {
                            ��������[num8].orders[num13] = new Order(�����[(int)Engine.Xml.GetDouble(element20["park"])],
                                                                     ��������[num8], element20["name"].InnerText,
                                                                     Engine.Xml.GetString(element20["transport"]))
                            {
                                ��������� =
                                    Engine.Xml.GetDouble(element20["po_rabochim"]) != 0.0,
                                ���������� =
                                    Engine.Xml.GetDouble(element20["po_vihodnim"]) != 0.0
                            };
                            element21 = element20["runs"];
                        }
                        if (element21 == null) continue;
                        ��������[num8].orders[num13].����� = new Trip[element21.ChildNodes.Count];
                        for (var num14 = 0; num14 < element21.ChildNodes.Count; num14++)
                        {
                            var element22 = element21[string.Format("run{0}", num14)];
                            if (element22 == null) continue;
                            var num15 = (int)Engine.Xml.GetDouble(element22["index"]);
                            var num16 = Engine.Xml.GetDouble(element22["time"]);
                            ��������[num8].orders[num13].�����[num14] = Engine.Xml.GetDouble(element22["park"]) == 0.0 ? ��������[num8].trips[num15].Clone(num16) : ��������[num8].parkTrips[num15].Clone(num16);
                        }
                    }
                }
                if (element23 != null)
                {
	                for (var num171 = 0; num171 < element23.ChildNodes.Count; num171++)
	                {
	                	this.���������[num171].����������������(this.��������);
	                }
                }
            }

            XmlElement element26 = element["Signals"];
            if (element26 != null)
            {
            	Logger.Log("LoadCity", "Old signals construction found!");
                this.����������������� = new ����������_�������[element26.ChildNodes.Count];
                for (int num19 = 0; num19 < element26.ChildNodes.Count; num19++)
                {
                    XmlElement element27 = element26["signal" + num19.ToString()];
                    this.�����������������[num19] = new ����������_�������((int)Engine.Xml.GetDouble(element27["bound"]), (int)Engine.Xml.GetDouble(element27["status"]));
                    XmlElement element28 = element27["elements"];
                    for (int num20 = 0; num20 < element28.ChildNodes.Count; num20++)
                    {
                        XmlElement element29 = element28["element" + num20.ToString()];
                        Road ������2 = this.���������[(int)Engine.Xml.GetDouble(element29["rail"])];
                        double num21 = Engine.Xml.GetDouble(element29["distance"]);
                        string innerText = element29["type"].InnerText;
                        if (innerText != null)
                        {
                        	switch (innerText)
                        	{
                        		case "�������":
                        			new ����������_�������.�������(this.�����������������[num19], ������2, num21, Engine.Xml.GetDouble(element29["minus"]) != 0.0);
                        			break;
                        		
                        		case "������":
                        			{
//                        				new ����������_�������.������(this.�����������������[num19], ������2, num21, "Signal");
                        				var signal = new Visual_Signal(this.�����������������[num19], Engine.Xml.GetString(element29["model"], "Signal"));
                        				signal.��������� = new ���������();
                        				signal.road = ������2;
                        				signal.���������.���������� = num21;
                        				signal.���������.���������� = Engine.Xml.GetDouble(element29["place"], -1.4 - (������2.�����������(num21) / 2.0));
                        				signal.���������.������ = Engine.Xml.GetDouble(element29["height"]);
                        				signal.CreateBoundingSphere();
                        				break;
                        			}
                        	}
                        }
                    }
                }
            }
            else
            {
            	// TODO: ����� �������� ��������
            	var sinals_element = element["Signal_systems"];
            	this.����������������� = new ����������_�������[sinals_element.ChildNodes.Count];
                for (int num19 = 0; num19 < sinals_element.ChildNodes.Count; num19++)
                {
                    XmlElement element27 = sinals_element["signal_system" + num19.ToString()];
                    this.�����������������[num19] = new ����������_�������((int)Engine.Xml.GetDouble(element27["bound"]), (int)Engine.Xml.GetDouble(element27["status"]));
                    var items_element = element27["signals"];
                    for (int i = 0; i < items_element.ChildNodes.Count; i++)
                    {
                    	var temp_signal = items_element["signal" + i.ToString()];
                    	string name = Engine.Xml.GetString(temp_signal["model"]);
                    	var road = this.���������[(int)Engine.Xml.GetDouble(temp_signal["path"])];
                    	var dist = Engine.Xml.GetDouble(temp_signal["distance"]);
                    	var shift = Engine.Xml.GetDouble(temp_signal["place"]);
                    	var signal = new Visual_Signal(this.�����������������[num19], name);
                    	signal.��������� = new ���������();
                    	signal.road = road;
                    	signal.���������.���������� = dist;
                    	signal.���������.���������� = shift;
                    	signal.���������.������ = Engine.Xml.GetDouble(temp_signal["height"]);
                    	signal.CreateBoundingSphere();
                    }
                    XmlElement element28 = element27["contacts"];
                    for (int num20 = 0; num20 < element28.ChildNodes.Count; num20++)
                    {
                        XmlElement element29 = element28["element" + num20.ToString()];
                        new ����������_�������.�������(this.�����������������[num19], this.���������[(int)Engine.Xml.GetDouble(element29["rail"])], Engine.Xml.GetDouble(element29["distance"]), Engine.Xml.GetDouble(element29["minus"]) != 0.0);
                    }
                }
            }
            XmlElement element30 = element["Svetofor_systems"];
            if (element30 != null)
            {
                this.������������������ = new �����������_�������[element30.ChildNodes.Count];
                for (int num22 = 0; num22 < element30.ChildNodes.Count; num22++)
                {
                    XmlElement element31 = element30["svetofor_system" + num22.ToString()];
                    this.������������������[num22] = new �����������_�������();
                    this.������������������[num22].������_������ = Engine.Xml.GetDouble(element31["begin"]);
                    this.������������������[num22].���������_������ = Engine.Xml.GetDouble(element31["end"]);
                    this.������������������[num22].���� = Engine.Xml.GetDouble(element31["cycle"]);
                    this.������������������[num22].�����_������������_��_������ = Engine.Xml.GetDouble(element31["time_to_green"]);
                    this.������������������[num22].�����_������� = Engine.Xml.GetDouble(element31["time_of_green"]);
                    XmlElement element32 = element31["svetofors"];
                    for (int num23 = 0; num23 < element32.ChildNodes.Count; num23++)
                    {
                        XmlElement element33 = element32["svetofor" + num23.ToString()];
                        bool flag = Engine.Xml.GetDouble(element33["arrow"]) != 0.0;
                        this.������������������[num22].���������.Add(new ��������(Engine.Xml.GetString(element33["model"], flag ? "Tr. light (arrow)" : "Tr. light")));
                        this.������������������[num22].���������[num23].���������.������ = this.���������[(int)Engine.Xml.GetDouble(element33["rail"])];
                        this.������������������[num22].���������[num23].���������.���������� = Engine.Xml.GetDouble(element33["distance"]);
                        this.������������������[num22].���������[num23].���������.���������� = Engine.Xml.GetDouble(element33["place"]);
                        this.������������������[num22].���������[num23].���������.������ = Engine.Xml.GetDouble(element33["height"]);
//                        this.������������������[num22].���������[num23].������� = flag;
                        this.������������������[num22].���������[num23].������_������� = /*(��������.�������)*/((int)Engine.Xml.GetDouble(element33["arrow_green"]));
                        this.������������������[num22].���������[num23].�����_������� = /*(��������.�������)*/((int)Engine.Xml.GetDouble(element33["arrow_yellow"]));
                        this.������������������[num22].���������[num23].�������_������� = /*(��������.�������)*/((int)Engine.Xml.GetDouble(element33["arrow_red"]));
                        this.������������������[num22].���������[num23].CreateBoundingSphere();
                    }
                    XmlElement element34 = element31["svetofor_signals"];
                    for (int num24 = 0; num24 < element34.ChildNodes.Count; num24++)
                    {
                        XmlElement element35 = element34["svetofor_signal" + num24.ToString()];
                        Road ������3 = this.���������[(int)Engine.Xml.GetDouble(element35["rail"])];
                        double num25 = Engine.Xml.GetDouble(element35["distance"]);
                        this.������������������[num22].�����������_�������.Add(new �����������_������(������3, num25));
                    }
                }
            }
            XmlElement objects = element["Objects"];
            if (objects != null)
            {
                for (int i = 0; i < objects.ChildNodes.Count; i++)
                {
                    XmlElement obj = objects["object" + i.ToString()];
                    �������.Add(new ������(obj["filename"].InnerText, Engine.Xml.GetDouble(obj["x0"]), Engine.Xml.GetDouble(obj["y0"]), Engine.Xml.GetDouble(obj["angle0"]), Engine.Xml.GetDouble(obj["height0"])));
                }
            }
            Logger.Log("LoadCity", "Success!");
            this.filename = filename;
        }
        
        public void LoadCitySimple(string filename)
        {
        	if (filename == string.Empty) return;
        	Logger.Log("LoadCitySimple", string.Format("Loading {0}", filename));
            XmlDocument document = Engine.Xml.TryOpenDocument(filename);
            var element = document["City"];
            list������.Clear();
            if (element == null) return;
            var element2 = element["Rails"];
            if (element2 != null)
            {
            	for (var i = 0; i < element2.ChildNodes.Count; i++)
            	{
            		list������.Add(new �����(0.0, 0.0, 0.0, 0.0, 0.0, 0.0));
            	}
            }
            XmlElement element4 = element["Roads"];
            if (element4 != null)
            {
                for (int j = 0; j < element4.ChildNodes.Count; j++)
                {
                    this.list������.Add(new Road(0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0));
                }
            }
            XmlElement element8 = element["Parks"];
            if (element8 != null)
            {
                this.����� = new ����[element8.ChildNodes.Count];
                for (int m = 0; m < element8.ChildNodes.Count; m++)
                {
                    XmlElement element9 = element8["park" + m.ToString()];
                    this.�����[m] = new ����(element9["name"].InnerText);
                    var index = (int)Engine.Xml.GetDouble(element9["out"]);
                    if (index >= 0) this.�����[m].����� = this.���������[index];
                }
            }
            var element11 = element["Routes"];
            if (element11 != null)
            {
                �������� = new Route[element11.ChildNodes.Count];
                for (var num8 = 0; num8 < element11.ChildNodes.Count; num8++)
                {
                    XmlElement element13 = null;
                    var element12 = element11[string.Format("route{0}", num8)];
                    if (element12 != null)
                    {
                        ��������[num8] = new Route(TypeOfTransport.Tramway, element12["name"].InnerText);
                        if (element12["type"] != null)
                        {
                            ��������[num8].typeOfTransport = ((int)Engine.Xml.GetDouble(element12["type"]));
                        }
                        element13 = element12["route_runs"];
                    }
                    if (element13 != null)
                    {
                        for (var num9 = 0; num9 < element13.ChildNodes.Count; num9++)
                        {
                            var element14 = element13[string.Format("run{0}", num9)];
                            if (element14 == null) continue;
                            var item = new Trip
                                           {
                                               �����_�������� = Engine.Xml.GetDouble(element14["time"])
                                           };
                            var element15 = element14["run_rails"];
                            if (element15 != null)
                            {
                                item.pathes = new Road[Math.Min(element15.ChildNodes.Count, 1)];
                                for (var num10 = 0; num10 < item.pathes.Length; num10++)
                                {
                                    item.pathes[num10] = ���������[(int)Engine.Xml.GetDouble(element15["run_rail" + num10])];
                                }
                            }
                            ��������[num8].trips.Add(item);
                        }
                	}
                    if (element12 != null)
                    {
                        var element16 = element12["park_runs"];
                        if (element16 != null)
                            for (var num11 = 0; num11 < element16.ChildNodes.Count; num11++)
                            {
                                var element17 = element16["run" + num11];
                                if (element17 == null) continue;
                                var ����2 = new Trip
                                                {
                                                    inPark = Engine.Xml.GetDouble(element17["to_park"]) != 0.0,
                                                    inParkIndex = (int)Engine.Xml.GetDouble(element17["to_park_index"]),
                                                    �����_�������� = Engine.Xml.GetDouble(element17["time"])
                                                };
                                var element18 = element17["run_rails"];
                                if (element18 != null)
                                {
                                    ����2.pathes = new Road[Math.Min(element18.ChildNodes.Count, 1)];
                                    for (var num12 = 0; num12 < ����2.pathes.Length; num12++)
                                    {
                                        ����2.pathes[num12] = ���������[(int)Engine.Xml.GetDouble(element18["run_rail" + num12])];
                                    }
                                }
                                ��������[num8].parkTrips.Add(����2);
                            }
                    }
                    if (element12 == null) continue;
                    var element19 = element12["Narads"];
                    if (element19 == null) continue;
                    ��������[num8].orders = new Order[element19.ChildNodes.Count];
                    XmlElement element21 = null;
                    for (var num13 = 0; num13 < element19.ChildNodes.Count; num13++)
                    {
                        var element20 = element19[string.Format("narad{0}", num13)];
                        if (element20 != null)
                        {
                            ��������[num8].orders[num13] = new Order(�����[(int)Engine.Xml.GetDouble(element20["park"])],
                                                                     ��������[num8], element20["name"].InnerText,
                                                                     Engine.Xml.GetString(element20["transport"]))
                            {
                                ��������� =
                                    Engine.Xml.GetDouble(element20["po_rabochim"]) != 0.0,
                                ���������� =
                                    Engine.Xml.GetDouble(element20["po_vihodnim"]) != 0.0
                            };
                            element21 = element20["runs"];
                        }
                        if (element21 == null) continue;
                        ��������[num8].orders[num13].����� = new Trip[element21.ChildNodes.Count];
                        for (var num14 = 0; num14 < element21.ChildNodes.Count; num14++)
                        {
                            var element22 = element21[string.Format("run{0}", num14)];
                            if (element22 == null) continue;
                            var num15 = (int)Engine.Xml.GetDouble(element22["index"]);
                            var num16 = Engine.Xml.GetDouble(element22["time"]);
                            ��������[num8].orders[num13].�����[num14] = Engine.Xml.GetDouble(element22["park"]) == 0.0 ? ��������[num8].trips[num15].Clone(num16) : ��������[num8].parkTrips[num15].Clone(num16);
                        }
                    }
                }
            }
            Logger.Log("LoadCitySimple", "Success!");
            this.filename = filename;
        }

        public ��������� �����_���������_���������(DoublePoint pos)
        {
            return this.�����_���������_���������(pos, this.���������);
        }

        public ��������� �����_���������_���������(DoublePoint pos, Road[] ������_������)
        {
            List<���������> list = new List<���������>();
            List<double> list2 = new List<double>();
            DoublePoint point = new DoublePoint();
            DoublePoint point2 = new DoublePoint();
            foreach (Road ������ in ������_������)
            {
                if (������.������)
                {
//                    point = pos - ������.���������.�����0;
                    pos.CopyTo(ref point);
                    point.Subtract(ref ������.���������.�����0);
//                    point2 = ������.�����[0] - ������.���������.�����0;
                    ������.�����[0].CopyTo(ref point2);
                    point2.Subtract(ref ������.���������.�����0);
                    double num = ((Math.Sign(������.���������.����0) * (point.Angle - point2.Angle)) + 12.566370614359173) % 6.2831853071795862;
                    if (num < Math.Abs(������.���������.����0))
                    {
                        list.Add(new ���������(������, (������.���������.�����0 * num) / Math.Abs(������.���������.����0), -Math.Sign(������.���������.����0) * (point.Modulus - ������.����������������)));
                        list2.Add(Math.Abs((double)(point.Modulus - ������.����������������)));
                    }
//                    point = pos - ������.���������.�����1;
                    pos.CopyTo(ref point);
                    point.Subtract(ref ������.���������.�����1);
//                    point2 = ������.���������.��������� - ������.���������.�����1;
                    ������.���������.���������.CopyTo(ref point2);
                    point2.Subtract(ref ������.���������.�����1);
                    num = ((Math.Sign(������.���������.����1) * (point.Angle - point2.Angle)) + 12.566370614359173) % 6.2831853071795862;
                    if (num < Math.Abs(������.���������.����1))
                    {
                        list.Add(new ���������(������, ������.���������.�����0 + ((������.���������.�����1 * num) / Math.Abs(������.���������.����1)), -Math.Sign(������.���������.����1) * (point.Modulus - ������.����������������)));
                        list2.Add(Math.Abs((double)(point.Modulus - ������.����������������)));
                    }
                }
                else
                {
//                    point = pos - ������.�����[0];
                    pos.CopyTo(ref point);
                    point.Subtract(ref ������.�����[0]);
                    point.Angle -= ������.�����������[0];
//                    point2 = ������.�����[1] - ������.�����[0];
                    ������.�����[1].CopyTo(ref point2);
                    point2.Subtract(ref ������.�����[0]);
                    point2.Angle -= ������.�����������[0];
                    if ((point.x >= 0.0) && (point.x < point2.x))
                    {
                        point.y -= (point2.y * point.x) / point2.x;
                        point.x *= ������.����� / point2.x;
                        list.Add(new ���������(������, point.x, point.y));
                        list2.Add(Math.Abs(point.y));
                    }
                }
            }
            double num2 = 4.0;
            ��������� ��������� = new ���������();
            for (int i = 0; i < list.Count; i++)
            {
                if (list2[i] < num2)
                {
                    num2 = list2[i];
                    ��������� = list[i];
                }
            }
            return ���������;
        }

        public ���������[] �����_���_���������(params Double3DPoint[] pos)
        {
        	Double3DPoint point = new Double3DPoint();
        	DoublePoint point2 = new DoublePoint();
            List<���������> list = new List<���������>();
            double num = 0.0;
            for (int i = 0; i < pos.Length; i++)
            {
                for (int j = i + 1; j < pos.Length; j++)
                {
                	pos[i].CopyTo(ref point);
                	pos[i].Subtract(ref pos[j]);
                    num = Math.Max(num, point.Modulus);
                }
            }
            foreach (Road ������ in this.���������)
            {
                double num4 = ������.����� + Math.Max(������.������[0], ������.������[1]);
                pos[0].XZPoint.CopyTo(ref point2);
                point2.Subtract(ref ������.�����[0]);
                if (point2.Modulus <= (num4 + num))
                {
                    pos[0].XZPoint.CopyTo(ref point2);
                	point2.Subtract(ref ������.�����[1]);
                    if (point2.Modulus <= (num4 + num))
                    {
                        for (int k = 0; k < pos.Length; k++)
                        {
                            ��������� item = this.�����_���������(pos[k], ������);
                            if (item.������ != null)
                            {
                                list.Add(item);
                            }
                        }
                    }
                }
            }
            return list.ToArray();
        }

        public int �����_������(Road ������)
        {
            return this.list������.IndexOf(������);
        }

        public int �����_������(���� ����)
        {
            for (int i = 0; i < this.�����.Length; i++)
            {
                if (���� == this.�����[i])
                {
                    return i;
                }
            }
            return -1;
        }

        public int �����_������(Trip ����, Route �������, ref bool ��������)
        {
            for (int i = 0; i < �������.trips.Count; i++)
            {
                if (����.pathes == �������.trips[i].pathes)
                {
                    �������� = false;
                    return i;
                }
            }
            for (int j = 0; j < �������.parkTrips.Count; j++)
            {
                if (����.pathes == �������.parkTrips[j].pathes)
                {
                    �������� = true;
                    return j;
                }
            }
            return -1;
        }

        public int �����_������_���_����������(Road ������)
        {
            List<Road> list = new List<Road>(this.������);
            list.AddRange(this.������);
            return list.IndexOf(������);
        }

        public ��������� �����_���������(Double3DPoint pos, Road ������)
        {
        	DoublePoint point = new DoublePoint();
        	DoublePoint point2 = new DoublePoint();
        	DoublePoint posXZ = pos.XZPoint;
            double num = ������.����� + (Math.Max(������.������[0], ������.������[1]) / 2.0);
//            DoublePoint point5 = pos.xz_point - ������.�����[0];
//            if (point5.������ <= num)
//            posXZ.CopyTo(ref point);
//            point.Subtract(ref ������.�����[0]);
            if (/*point.Modulus*/DoublePoint.Distance(ref posXZ, ref ������.�����[0]) <= num)
            {
//                DoublePoint point6 = pos.xz_point - ������.�����[1];
//                if (point6.������ <= num)
//                posXZ.CopyTo(ref point);
//            	point.Subtract(ref ������.�����[1]);
                if (/*point.Modulus*/DoublePoint.Distance(ref posXZ, ref ������.�����[1]) <= num)
                {
                    if (������.������)
                    {
//                        DoublePoint point = pos.xz_point - ������.���������.�����0;
//                        DoublePoint point2 = ������.�����[0] - ������.���������.�����0;
                        posXZ.CopyTo(ref point);
			            point.Subtract(ref ������.���������.�����0);
			            ������.�����[0].CopyTo(ref point2);
			            point2.Subtract(ref ������.���������.�����0);
                        double num2 = ((Math.Sign(������.���������.����0) * (point.Angle - point2.Angle)) + 12.566370614359173) % 6.2831853071795862;
                        if (num2 < Math.Abs(������.���������.����0))
                        {
                            double num3 = (������.���������.�����0 * num2) / Math.Abs(������.���������.����0);
                            double num4 = pos.y - ������.�����������(num3);
                            if (((Math.Abs((double)(point.Modulus - ������.����������������)) < (������.�����������(num3) / 2.0)) && (num4 >= -1.0)) && (num4 < 5.0))
                            {
                                return new ���������(������, num3, -Math.Sign(������.���������.����0) * (point.Modulus - ������.����������������), num4);
                            }
                        }
//                        point = pos.xz_point - ������.���������.�����1;
//                        point2 = ������.���������.��������� - ������.���������.�����1;
                        posXZ.CopyTo(ref point);
			            point.Subtract(ref ������.���������.�����1);
			            ������.���������.���������.CopyTo(ref point2);
			            point2.Subtract(ref ������.���������.�����1);
                        num2 = ((Math.Sign(������.���������.����1) * (point.Angle - point2.Angle)) + 12.566370614359173) % 6.2831853071795862;
                        if (num2 < Math.Abs(������.���������.����1))
                        {
                            double num5 = ������.���������.�����0 + ((������.���������.�����1 * num2) / Math.Abs(������.���������.����1));
                            double num6 = pos.y - ������.�����������(num5);
                            if (((Math.Abs((double)(point.Modulus - ������.����������������)) < (������.�����������(num5) / 2.0)) && (num6 >= -1.0)) && (num6 < 5.0))
                            {
                                return new ���������(������, num5, -Math.Sign(������.���������.����1) * (point.Modulus - ������.����������������), num6);
                            }
                        }
                    }
                    else
                    {
                        /*DoublePoint point3 = pos.xz_point - ������.�����[0];
                        point3.���� -= ������.�����������[0];
                        DoublePoint point4 = ������.�����[1] - ������.�����[0];
                        point4.���� -= ������.�����������[0];
                        if ((point3.x >= 0.0) && (point3.x < point4.x))
                        {
                            point3.y -= (point4.y * point3.x) / point4.x;
                            point3.x *= ������.����� / point4.x;
                            double num7 = pos.y - ������.�����������(point3.x);
                            if (((Math.Abs(point3.y) < (������.�����������(point3.x) / 2.0)) && (num7 >= -1.0)) && (num7 < 5.0))
                            {
                                return new ���������(������, point3.x, point3.y, num7);
                            }
                        }*/
                        posXZ.CopyTo(ref point);
            			point.Subtract(ref ������.�����[0]);
            			point.Angle -= ������.�����������[0];
            			������.�����[1].CopyTo(ref point2);
            			point2.Subtract(ref ������.�����[0]);
            			point2.Angle -= ������.�����������[0];
            			if ((point.x >= 0.0) && (point.x < point2.x))
                        {
                            point.y -= (point2.y * point.x) / point2.x;
                            point.x *= ������.����� / point2.x;
                            double num7 = pos.y - ������.�����������(point.x);
                            if (((Math.Abs(point.y) < (������.�����������(point.x) / 2.0)) && (num7 >= -1.0)) && (num7 < 5.0))
                            {
                                return new ���������(������, point.x, point.y, num7);
                            }
                        }
                    }
                    return new ���������();
                }
            }
            return new ���������();
        }

        public ��������� �����_���������(DoublePoint pos, Road ������)
        {
            return this.�����_���������(new Double3DPoint(pos.x, 0.0, pos.y), ������);
        }

        public void �����_���������_������(Order �����, ref Trip ����, ref Road ������, ref double ����������_��_������, ref bool from_park)
        {
            for (int i = 0; i < �����.�����.Length; i++)
            {
                if (this.time < �����.�����[i].�����_��������)
                {
                    ���� = �����.�����[i];
                    if (this.time < �����.�����[i].�����_�����������)
                    {
                        if (�����.�����[i].������_����������� == �����.����.�����)
                        {
                            from_park = true;
                            ������ = �����.����.�����;//�����.����.����_�������[index];
                            ����������_��_������ = Cheats._random.NextDouble() * Math.Min(Math.Abs((������.����� - 20.0)), ������.�����);
                            return;
                        }
                        ������ = �����.�����[i].������_�����������;
                        ����������_��_������ = Cheats._random.NextDouble() * (������.����� * 0.4);//10.0;
                        return;
                    }
                    double num1 = ����.�����_����;
                    double num4 = (num1 * (this.time - �����.�����[i].�����_�����������)) / (�����.�����[i].�����_�������� - �����.�����[i].�����_�����������);
                    foreach (Road ������2 in ����.pathes)
                    {
                        if (num4 < ������2.�����)
                        {
                            ������ = ������2;
                            ����������_��_������ = num4;
                            if (((������2 is �����) && (((�����)������2).���������_������.Length > 1)) && (����������_��_������ > (������2.����� - ((�����)������2).����������_����������_��������)))
                            {
                            	����������_��_������ -= ((�����)������2).����������_����������_�������� + (Cheats._random.NextDouble() * 5.0);
                                return;
                            }
                            return;
                        }
                        num4 -= ������2.�����;
                    }
                    return;
                }
                if (i == (�����.�����.Length - 1))
                {
                    ���� = �����.�����[i];
                    ������ = �����.�����[i].������_��������;
                    ����������_��_������ = Cheats._random.NextDouble() * (������.����� * 0.4);//10.0;
                }
            }
        }

        public void ��������(�����[] ������)
        {
            this.��������_�����();
            this.time += �������������;
            if (this.time >= 97200.0)
            {
                this.time -= 86400.0;
            }
            foreach (�����������_������� _������� in this.������������������)
            {
                _�������.��������(this);
            }
            foreach (Transport ��������� in this.����������)
            {
                ���������.��������(this, ������);
            }
        }

        public void ��������_�����()
        {
        	var ftime = ((double)Environment.TickCount) / time_speed;
            if (this.�������������� == 0.0)
            {
                this.�������������� = ftime;//((double)Environment.TickCount) / time_speed;//0x3e8;
            }
            ������������� = ftime - this.��������������;//(((double)Environment.TickCount) / time_speed) - this.��������������;
            dtmax = Math.Max(dtmax, �������������);
            if (MainForm.in_editor) ������������� = Math.Min(�������������, 0.25);
            this.�������������� = ftime;//((double)Environment.TickCount) / time_speed;//1000
//            MeshObject.timer.Refresh();
        }

        public void ���������_�����(string filename)
        {
        	Logger.Log("SaveCity", "Trying to save current city...");
        	Stopwatch stopwatch = new Stopwatch();
        	stopwatch.Start();
            XmlDocument parent = new XmlDocument();
            XmlElement element = Engine.Xml.AddElement(parent, "City");
            XmlElement element2 = Engine.Xml.AddElement(parent, element, "Rails");
            for (int i = 0; i < this.������.Length; i++)
            {
                XmlElement element3 = Engine.Xml.AddElement(parent, element2, "rail" + i.ToString());
                Engine.Xml.AddElement(parent, element3, "x0", this.������[i].�����[0].x);
                Engine.Xml.AddElement(parent, element3, "y0", this.������[i].�����[0].y);
                Engine.Xml.AddElement(parent, element3, "x1", this.������[i].�����[1].x);
                Engine.Xml.AddElement(parent, element3, "y1", this.������[i].�����[1].y);
                Engine.Xml.AddElement(parent, element3, "angle0", this.������[i].�����������[0]);
                Engine.Xml.AddElement(parent, element3, "angle1", this.������[i].�����������[1]);
                Engine.Xml.AddElement(parent, element3, "height0", this.������[i].������[0]);
                Engine.Xml.AddElement(parent, element3, "height1", this.������[i].������[1]);
                Engine.Xml.AddElement(parent, element3, "d_strel", this.������[i].����������_����������_��������);
                Engine.Xml.AddElement(parent, element3, "iskriv", this.������[i].������ ? ((double)1) : ((double)0));
                Engine.Xml.AddElement(parent, element3, "name", this.������[i].name);
            }
            XmlElement element4 = Engine.Xml.AddElement(parent, element, "Roads");
            for (int j = 0; j < this.������.Length; j++)
            {
                XmlElement element5 = Engine.Xml.AddElement(parent, element4, "road" + j.ToString());
                Engine.Xml.AddElement(parent, element5, "x0", this.������[j].�����[0].x);
                Engine.Xml.AddElement(parent, element5, "y0", this.������[j].�����[0].y);
                Engine.Xml.AddElement(parent, element5, "x1", this.������[j].�����[1].x);
                Engine.Xml.AddElement(parent, element5, "y1", this.������[j].�����[1].y);
                Engine.Xml.AddElement(parent, element5, "angle0", this.������[j].�����������[0]);
                Engine.Xml.AddElement(parent, element5, "angle1", this.������[j].�����������[1]);
                Engine.Xml.AddElement(parent, element5, "wide0", this.������[j].������[0]);
                Engine.Xml.AddElement(parent, element5, "wide1", this.������[j].������[1]);
                Engine.Xml.AddElement(parent, element5, "height0", this.������[j].������[0]);
                Engine.Xml.AddElement(parent, element5, "height1", this.������[j].������[1]);
                Engine.Xml.AddElement(parent, element5, "iskriv", this.������[j].������ ? ((double)1) : ((double)0));
                Engine.Xml.AddElement(parent, element5, "name", this.������[j].name);
            }
            XmlElement element6 = Engine.Xml.AddElement(parent, element, "Trolleybus_lines");
            for (int k = 0; k < this.�����������������.Length; k++)
            {
                XmlElement element7 = Engine.Xml.AddElement(parent, element6, "line" + k.ToString());
                Engine.Xml.AddElement(parent, element7, "x0", this.�����������������[k].������.x);
                Engine.Xml.AddElement(parent, element7, "y0", this.�����������������[k].������.y);
                Engine.Xml.AddElement(parent, element7, "x1", this.�����������������[k].�����.x);
                Engine.Xml.AddElement(parent, element7, "y1", this.�����������������[k].�����.y);
                Engine.Xml.AddElement(parent, element7, "height0", this.�����������������[k].������[0]);
                Engine.Xml.AddElement(parent, element7, "height1", this.�����������������[k].������[1]);
                Engine.Xml.AddElement(parent, element7, "right", this.�����������������[k].������ ? ((double)1) : ((double)0));
                Engine.Xml.AddElement(parent, element7, "no_contact", this.�����������������[k].������������ ? ((double)1) : ((double)0));
            }
            XmlElement element6_1 = Engine.Xml.AddElement(parent, element, "Tramway_lines");
            for (int k = 0; k < this.�����������������2.Length; k++)
            {
                XmlElement element7 = Engine.Xml.AddElement(parent, element6_1, "line" + k.ToString());
                Engine.Xml.AddElement(parent, element7, "x0", this.�����������������2[k].������.x);
                Engine.Xml.AddElement(parent, element7, "y0", this.�����������������2[k].������.y);
                Engine.Xml.AddElement(parent, element7, "x1", this.�����������������2[k].�����.x);
                Engine.Xml.AddElement(parent, element7, "y1", this.�����������������2[k].�����.y);
                Engine.Xml.AddElement(parent, element7, "height0", this.�����������������2[k].������[0]);
                Engine.Xml.AddElement(parent, element7, "height1", this.�����������������2[k].������[1]);
                Engine.Xml.AddElement(parent, element7, "no_contact", this.�����������������2[k].������������ ? ((double)1) : ((double)0));
            }
            XmlElement element8 = Engine.Xml.AddElement(parent, element, "Parks");
            for (int m = 0; m < this.�����.Length; m++)
            {
                XmlElement element9 = Engine.Xml.AddElement(parent, element8, "park" + m.ToString());
                Engine.Xml.AddElement(parent, element9, "name", this.�����[m].��������);
                Engine.Xml.AddElement(parent, element9, "in", (double)this.�����_������_���_����������(this.�����[m].�����));
                Engine.Xml.AddElement(parent, element9, "out", (double)this.�����_������_���_����������(this.�����[m].�����));
                XmlElement element10 = Engine.Xml.AddElement(parent, element9, "park_rails");
                int index = 0;
                for (int num6 = 0; index < this.�����[m].����_�������.Length; num6++)
                {
                    int num7 = this.�����_������_���_����������(this.�����[m].����_�������[index]);
                    if (num7 < 0)
                    {
                        num6--;
                    }
                    else
                    {
                        Engine.Xml.AddElement(parent, element10, "park_rail" + num6.ToString(), (double)num7);
                    }
                    index++;
                }
            }
            XmlElement element11 = Engine.Xml.AddElement(parent, element, "Routes");
            for (int n = 0; n < this.��������.Length; n++)
            {
                XmlElement element12 = Engine.Xml.AddElement(parent, element11, "route" + n.ToString());
                Engine.Xml.AddElement(parent, element12, "name", this.��������[n].number);
                Engine.Xml.AddElement(parent, element12, "type", (double)this.��������[n].typeOfTransport);
                XmlElement element13 = Engine.Xml.AddElement(parent, element12, "route_runs");
                for (int num9 = 0; num9 < this.��������[n].trips.Count; num9++)
                {
                    XmlElement element14 = Engine.Xml.AddElement(parent, element13, "run" + num9.ToString());
                    Engine.Xml.AddElement(parent, element14, "time", this.��������[n].trips[num9].�����_��������);
                    XmlElement element15 = Engine.Xml.AddElement(parent, element14, "run_rails");
                    for (int num10 = 0; num10 < this.��������[n].trips[num9].pathes.Length; num10++)
                    {
                        int num11 = this.�����_������_���_����������(this.��������[n].trips[num9].pathes[num10]);
                        if (num11 < 0)
                        {
                            throw new IndexOutOfRangeException("������� " + this.��������[n].number + " (���� " + num9.ToString() + ") �������� �� ��������������� ����!");
                        }
                        Engine.Xml.AddElement(parent, element15, "run_rail" + num10.ToString(), (double)num11);
                    }
                    XmlElement stops = Engine.Xml.AddElement(parent, element14, "Stops");
                    if (��������[n].trips[num9].tripStopList != null && ��������[n].trips[num9].tripStopList.Count > 0)
                    {
                        for (int i = 0; i < ��������[n].trips[num9].tripStopList.Count; i++)
                        {
                            TripStop tripStop = ��������[n].trips[num9].tripStopList[i];
                            if (!tripStop.stop.typeOfTransport[��������[n].typeOfTransport]) continue;
                            int stops_ind = ���������.IndexOf(tripStop.stop);
                            if (stops_ind < 0)
                            {
                            	continue;//throw new IndexOutOfRangeException("���� �� ��������� ������� ��������� �������� " + this.��������[n].number + " (����� " + num9.ToString() + ") �� ����������!");
                            }
                            Engine.Xml.AddElement(parent, stops, "Stop" + stops_ind, tripStop.flag ? ((double)1) : ((double)0));//"��" : "���");
                        }
                    }
                }
                XmlElement element16 = Engine.Xml.AddElement(parent, element12, "park_runs");
                for (int num12 = 0; num12 < this.��������[n].parkTrips.Count; num12++)
                {
                    XmlElement element17 = Engine.Xml.AddElement(parent, element16, "run" + num12.ToString());
                    Engine.Xml.AddElement(parent, element17, "to_park", this.��������[n].parkTrips[num12].inPark ? ((double)1) : ((double)0));
                    Engine.Xml.AddElement(parent, element17, "to_park_index", (double)this.��������[n].parkTrips[num12].inParkIndex);
                    Engine.Xml.AddElement(parent, element17, "time", this.��������[n].parkTrips[num12].�����_��������);
                    XmlElement element18 = Engine.Xml.AddElement(parent, element17, "run_rails");
                    for (int num13 = 0; num13 < this.��������[n].parkTrips[num12].pathes.Length; num13++)
                    {
                        int num14 = this.�����_������_���_����������(this.��������[n].parkTrips[num12].pathes[num13]);
                        if (num14 < 0)
                        {
                            throw new IndexOutOfRangeException("������� " + this.��������[n].number + " (�������� ���� " + num12.ToString() + ") �������� �� ��������������� ����!");
                        }
                        Engine.Xml.AddElement(parent, element18, "run_rail" + num13.ToString(), (double)num14);
                    }
                    XmlElement stops = Engine.Xml.AddElement(parent, element17, "Stops");
                    if (��������[n].parkTrips[num12].tripStopList != null && ��������[n].parkTrips[num12].tripStopList.Count > 0)
                    {
                        for (int i = 0; i < ��������[n].parkTrips[num12].tripStopList.Count; i++)
                        {
                            TripStop tripStop = ��������[n].parkTrips[num12].tripStopList[i];
                            if (!tripStop.stop.typeOfTransport[��������[n].typeOfTransport]) continue;
                            int stops_ind = ���������.IndexOf(tripStop.stop);
                            if (stops_ind < 0)
                            {
                            	continue;//throw new IndexOutOfRangeException("���� �� ��������� ������� ��������� �������� " + this.��������[n].number + " (��������� ����� " + num12.ToString() + ") �� ����������!");
                            }
                            Engine.Xml.AddElement(parent, stops, "Stop" + stops_ind, tripStop.flag ? ((double)1) : ((double)0));//"��" : "���");
                        }
                    }
                }
                XmlElement element19 = Engine.Xml.AddElement(parent, element12, "Narads");
                for (int num15 = 0; num15 < this.��������[n].orders.Length; num15++)
                {
                    XmlElement element20 = Engine.Xml.AddElement(parent, element19, "narad" + num15.ToString());
                    Engine.Xml.AddElement(parent, element20, "name", this.��������[n].orders[num15].�����);
                    int num16 = this.�����_������(this.��������[n].orders[num15].����);
                    if (num16 < 0)
                    {
                        throw new IndexOutOfRangeException("� ������ " + this.��������[n].number + "/" + this.��������[n].orders[num15].����� + " �� ������ ����!");
                    }
                    Engine.Xml.AddElement(parent, element20, "park", (double)num16);
                    Engine.Xml.AddElement(parent, element20, "transport", this.��������[n].orders[num15].transport);
                    Engine.Xml.AddElement(parent, element20, "po_rabochim", this.��������[n].orders[num15].��������� ? ((double)1) : ((double)0));
                    Engine.Xml.AddElement(parent, element20, "po_vihodnim", this.��������[n].orders[num15].���������� ? ((double)1) : ((double)0));
                    XmlElement element21 = Engine.Xml.AddElement(parent, element20, "runs");
                    for (int num17 = 0; num17 < this.��������[n].orders[num15].�����.Length; num17++)
                    {
                        Trip ���� = this.��������[n].orders[num15].�����[num17];
                        XmlElement element22 = Engine.Xml.AddElement(parent, element21, "run" + num17.ToString());
                        bool flag = false;
                        int num18 = this.�����_������(����, this.��������[n], ref flag);
                        if (num18 < 0)
                        {
                            num18 = 0;
                        }
                        Engine.Xml.AddElement(parent, element22, "park", flag ? ((double)1) : ((double)0));
                        Engine.Xml.AddElement(parent, element22, "index", (double)num18);
                        Engine.Xml.AddElement(parent, element22, "time", this.��������[n].orders[num15].�����[num17].�����_�����������);
                    }
                }
            }
            XmlElement element23 = Engine.Xml.AddElement(parent, element, "Stops");
            for (int num19 = 0; num19 < this.���������.Count; num19++)
            {
                XmlElement element24 = Engine.Xml.AddElement(parent, element23, "stop" + num19.ToString());
                Engine.Xml.AddElement(parent, element24, "name", this.���������[num19].��������);
                Engine.Xml.AddElement(parent, element24, "model", this.���������[num19].name);
                XmlElement element240 = Engine.Xml.AddElement(parent, element24, "type");
                Engine.Xml.AddElement(parent, element240, "Tramway", ���������[num19].typeOfTransport[TypeOfTransport.Tramway].ToString());
                Engine.Xml.AddElement(parent, element240, "Trolleybus", ���������[num19].typeOfTransport[TypeOfTransport.Trolleybus].ToString());
                Engine.Xml.AddElement(parent, element240, "Bus", ���������[num19].typeOfTransport[TypeOfTransport.Bus].ToString());
                //Engine.Xml.AddElement(element24, "type", (double) this.���������[num19].typeOfTransport);
                int num20 = this.�����_������_���_����������(this.���������[num19].road);
                if (num20 < 0)
                {
                    throw new IndexOutOfRangeException("��������� \"" + this.���������[num19].�������� + "\" ��������� �� �������������� ����!");
                }
                Engine.Xml.AddElement(parent, element24, "rail", (double)num20);
                Engine.Xml.AddElement(parent, element24, "distance", this.���������[num19].distance);
                XmlElement element25 = Engine.Xml.AddElement(parent, element24, "stop_path");
                for (int num21 = 0; num21 < this.���������[num19].���������.Length; num21++)
                {
                    int num22 = this.�����_������_���_����������(this.���������[num19].���������[num21]);
                    if (num22 < 0)
                    {
                        break;
                    }
                    Engine.Xml.AddElement(parent, element25, "stop_rail" + num21.ToString(), (double)num22);
                }
            }
            XmlElement element26 = Engine.Xml.AddElement(parent, element, "Signals");
            for (int num23 = 0; num23 < this.�����������������.Length; num23++)
            {
                XmlElement element27 = Engine.Xml.AddElement(parent, element26, "signal" + num23.ToString());
                Engine.Xml.AddElement(parent, element27, "status", (double)this.�����������������[num23].���������);
                Engine.Xml.AddElement(parent, element27, "bound", (double)this.�����������������[num23].�������_������������);
                XmlElement element28 = Engine.Xml.AddElement(parent, element27, "elements");
                for (int i = 0; i < this.�����������������[num23].vsignals.Count; i++)
                {
                	XmlElement element29 = Engine.Xml.AddElement(parent, element28, "element" + i.ToString());
                    Engine.Xml.AddElement(parent, element29, "type", "������");
                    Engine.Xml.AddElement(parent, element29, "model", this.�����������������[num23].vsignals[i].name);
                    int num25 = this.�����_������_���_����������(this.�����������������[num23].vsignals[i].���������.������);
                    if (num25 < 0)
                    {
                        throw new IndexOutOfRangeException("������ �" + i.ToString() + " ���������� ������� �" + num23.ToString() + " ��������� �� �������������� ����!");
                    }
                    Engine.Xml.AddElement(parent, element29, "rail", (double)num25);
                    Engine.Xml.AddElement(parent, element29, "distance", this.�����������������[num23].vsignals[i].���������.����������);
                    Engine.Xml.AddElement(parent, element29, "place", this.�����������������[num23].vsignals[i].���������.����������);
                    Engine.Xml.AddElement(parent, element29, "height", this.�����������������[num23].vsignals[i].���������.������);
                }
                for (int num24 = 0; num24 < this.�����������������[num23].��������.Count; num24++)
                {
                	XmlElement element29 = Engine.Xml.AddElement(parent, element28, "element" + (num24 + this.�����������������[num23].vsignals.Count).ToString());
                    Engine.Xml.AddElement(parent, element29, "type", this.�����������������[num23].��������[num24].GetType().Name);
                    int num25 = this.�����_������_���_����������(this.�����������������[num23].��������[num24].������);
                    if (num25 < 0)
                    {
                        throw new IndexOutOfRangeException("������� �" + num24.ToString() + " ���������� ������� �" + num23.ToString() + " ��������� �� �������������� ����!");
                    }
                    Engine.Xml.AddElement(parent, element29, "rail", (double)num25);
                    Engine.Xml.AddElement(parent, element29, "distance", this.�����������������[num23].��������[num24].����������);
//                    if (this.�����������������[num23].��������[num24] is ����������_�������.�������)
//                    {
//                        ����������_�������.������� ������� = (����������_�������.�������)this.�����������������[num23].��������[num24];
                        Engine.Xml.AddElement(parent, element29, "minus", /*�������*/this.�����������������[num23].��������[num24].����� ? ((double)1) : ((double)0));
//                    }
                }
            }
            XmlElement element30 = Engine.Xml.AddElement(parent, element, "Svetofor_systems");
            for (int num26 = 0; num26 < this.������������������.Length; num26++)
            {
                XmlElement element31 = Engine.Xml.AddElement(parent, element30, "svetofor_system" + num26.ToString());
                Engine.Xml.AddElement(parent, element31, "begin", this.������������������[num26].������_������);
                Engine.Xml.AddElement(parent, element31, "end", this.������������������[num26].���������_������);
                Engine.Xml.AddElement(parent, element31, "cycle", this.������������������[num26].����);
                Engine.Xml.AddElement(parent, element31, "time_to_green", this.������������������[num26].�����_������������_��_������);
                Engine.Xml.AddElement(parent, element31, "time_of_green", this.������������������[num26].�����_�������);
                XmlElement element32 = Engine.Xml.AddElement(parent, element31, "svetofors");
                for (int num27 = 0; num27 < this.������������������[num26].���������.Count; num27++)
                {
                    XmlElement element33 = Engine.Xml.AddElement(parent, element32, "svetofor" + num27.ToString());
                    int num28 = this.�����_������_���_����������(this.������������������[num26].���������[num27].���������.������);
                    if (num28 < 0)
                    {
                        throw new IndexOutOfRangeException("�������� �" + num27.ToString() + " ����������� ������� �" + num26.ToString() + " ��������� �� �������������� ����!");
                    }
                    Engine.Xml.AddElement(parent, element33, "model", this.������������������[num26].���������[num27].name);
                    Engine.Xml.AddElement(parent, element33, "rail", (double)num28);
                    Engine.Xml.AddElement(parent, element33, "distance", this.������������������[num26].���������[num27].���������.����������);
                    Engine.Xml.AddElement(parent, element33, "place", this.������������������[num26].���������[num27].���������.����������);
                    Engine.Xml.AddElement(parent, element33, "height", this.������������������[num26].���������[num27].���������.������);
//                    Engine.Xml.AddElement(element33, "arrow", this.������������������[num26].���������[num27].������� ? ((double)1) : ((double)0));
                    Engine.Xml.AddElement(parent, element33, "arrow_green", (double)this.������������������[num26].���������[num27].������_�������);
                    Engine.Xml.AddElement(parent, element33, "arrow_yellow", (double)this.������������������[num26].���������[num27].�����_�������);
                    Engine.Xml.AddElement(parent, element33, "arrow_red", (double)this.������������������[num26].���������[num27].�������_�������);
                }
                XmlElement element34 = Engine.Xml.AddElement(parent, element31, "svetofor_signals");
                for (int num29 = 0; num29 < this.������������������[num26].�����������_�������.Count; num29++)
                {
                    XmlElement element35 = Engine.Xml.AddElement(parent, element34, "svetofor_signal" + num29.ToString());
                    int num30 = this.�����_������_���_����������(this.������������������[num26].�����������_�������[num29].������);
                    if (num30 < 0)
                    {
                        throw new IndexOutOfRangeException("������ �" + num29.ToString() + " ����������� ������� �" + num26.ToString() + " ��������� �� �������������� ����!");
                    }
                    Engine.Xml.AddElement(parent, element35, "rail", (double)num30);
                    Engine.Xml.AddElement(parent, element35, "distance", this.������������������[num26].�����������_�������[num29].����������);
                }
            }
            XmlElement objects = Engine.Xml.AddElement(parent, element, "Objects");
            for (int i = 0; i < �������.Count; i++)
            {
                XmlElement obj = Engine.Xml.AddElement(parent, objects, "object" + i.ToString());
                Engine.Xml.AddElement(parent, obj, "filename", this.�������[i].filename);
                Engine.Xml.AddElement(parent, obj, "x0", this.�������[i].x0);
                Engine.Xml.AddElement(parent, obj, "y0", this.�������[i].y0);
                Engine.Xml.AddElement(parent, obj, "angle0", this.�������[i].angle0);
                Engine.Xml.AddElement(parent, obj, "height0", this.�������[i].height0);
            }
            parent.Save(filename);
            stopwatch.Stop();
            Logger.Log("SaveCity", "City saved to file " + filename);
            Logger.Log("SaveCity", "Elapsed time : " + stopwatch.Elapsed.ToString());
        }
        
        public double GetHeight(DoublePoint pos)
        {
        	return �����.GetHeight(pos);
        }

        public Road[] ���������
        {
            get
            {
                return (Road[])this.list������.ToArray(typeof(Road));
            }
        }

        private List<Order> ���������
        {
            get
            {
                var list = new List<Order>();
                foreach (Route ������� in this.��������)
                {
                    list.AddRange(�������.orders);
                }
                return list;
            }
        }

        public Road[] ������
        {
            get
            {
                return list������.Get_array<Road>();
            }
        }

        public �����[] ������
        {
            get
            {
                return list������.Get_array<�����>();
            }
        }
    }
}