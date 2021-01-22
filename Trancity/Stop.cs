using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Common;
using SlimDX.Direct3D9;

namespace Trancity
{
    public class Stop : BaseStop, IComparable<Stop>
    {
        private Route[] _��������;
        public string ��������;
        public static bool �������������������;
        private ����������[] _����������;
        public Road[] ���������;

        public Stop(string mname, TypeOfTransport �������������, Road ������, double ����������) : base(mname)
        {
            ��������� = new Road[0];
            �������� = "";
            _�������� = new Route[0];
            _���������� = new ����������[0];
            typeOfTransport = �������������;
            base.road = ������;
            base.distance = ����������;
        }

        public Stop(string mname, TypeOfTransport �������������, string ��������, Road ������, double ����������) : this(mname, �������������, ������, ����������)
        {
            this.�������� = ��������;
        }

        public Stop(string mname, TypeOfTransport �������������, Road ������, double ����������, bool ���������) : this(mname, �������������, ������, ����������)
        {
            base.serviceStop = ���������;
        }

        private void DrawCenteredString(Graphics graphics, string s, System.Drawing.Font font, Brush brush, float x, float y)
        {
            graphics.DrawString(s, font, brush, x - (0.5f * graphics.MeasureString(s, font).Width), y);
        }

        public void ����������������()
        {
        	var str = model.FindStringArg("tram_tex", string.Empty);//"Plate.PNG";
            var str2 = "";//str;            
            if (typeOfTransport[TypeOfTransport.Trolleybus])
            {
                str2 = model.FindStringArg("trol_tex", string.Empty);//"PlateTr.png";
            }
            if (typeOfTransport[TypeOfTransport.Bus])
            {
                str2 = model.FindStringArg("bus_tex", string.Empty);//"PlateB.png";
            }
            if ((typeOfTransport[TypeOfTransport.Trolleybus]) && (typeOfTransport[TypeOfTransport.Bus]))
            {
                str2 = model.FindStringArg("trol_bus_tex", string.Empty);//"PlateTrB.png";
            }
            if (string.IsNullOrEmpty(str)) return;
            if (string.IsNullOrEmpty(str2)) str2 = str;
            if (�������������������)
            {
                if (str2 != str)
                {
                    for (var i = 0; i < _meshTextures.Length; i++)
                    {
                    	if ((string.IsNullOrEmpty(_meshTextureFilenames[i])) || (_meshTextureFilenames[i].ToLower() != str.ToLower()))
                    		continue;
                        LoadTexture(i, base.meshDir + str2);
                    }
                }
            }
            else
            {
                var listArray = new List<Route>[Math.Max(1, (_��������.Length + 3) / 4)];
                for (var j = 0; j < listArray.Length; j++)
                {
                    listArray[j] = new List<Route>();
                }
                foreach (var ������� in _��������)
                {
                    int num3;
                    do
                    {
                        num3 = Cheats._random.Next(listArray.Length);
                    }
                    while (listArray[num3].Count >= 4);
                    listArray[num3].Add(�������);
                }
                var strArray = new string[4 * listArray.Length];
                var �������Array = new Route[4 * listArray.Length];
                for (var k = 0; k < listArray.Length; k++)
                {
                    for (var num5 = 0; num5 < listArray[k].Count; num5++)
                    {
                        int num6;
                        do
                        {
                            num6 = Cheats._random.Next(4 * k, (4 * k) + 4);
                        }
                        while (strArray[num6] != null);
                        strArray[num6] = listArray[k][num5].number;
                    }
                    if (listArray[k].Count <= 2)
                    {
                        for (var num7 = 0; num7 < listArray[k].Count; num7++)
                        {
                            int num8;
                            do
                            {
                                num8 = Cheats._random.Next(4 * k, (4 * k) + 2);
                            }
                            while (�������Array[num8] != null);
                            �������Array[num8] = listArray[k][num7];
                        }
                        for (var num9 = 0; num9 < listArray[k].Count; num9++)
                        {
                            int num10;
                            do
                            {
                                num10 = Cheats._random.Next((4 * k) + 2, (4 * k) + 4);
                            }
                            while (�������Array[num10] != null);
                            �������Array[num10] = listArray[k][num9];
                        }
                    }
                    else
                    {
                        for (var num11 = 0; num11 < listArray[k].Count; num11++)
                        {
                            int num12;
                            do
                            {
                                num12 = Cheats._random.Next(4 * k, (4 * k) + 4);
                            }
                            while (�������Array[num12] != null);
                            �������Array[num12] = listArray[k][num11];
                        }
                    }
                }
                for (var m = 0; m < strArray.Length; m++)
                {
                    if (strArray[m] == null)
                    {
                        strArray[m] = "";
                    }
                }
                var font = new System.Drawing.Font("Verdana", 7f);
                var font2 = new System.Drawing.Font("Verdana", 12f, FontStyle.Bold);
                var font3 = new System.Drawing.Font("Verdana", 8f, FontStyle.Bold);
                var font4 = new System.Drawing.Font("Verdana", 5.5f);
                var font5 = new System.Drawing.Font("Verdana", 4f);
                Brush brush = new SolidBrush(Color.Black);
                Brush brush2 = new SolidBrush(Color.FromArgb(-13749870));
                Brush brush3 = new SolidBrush(Color.FromArgb(-1237980));
                var pen = new Pen(Color.Black);
                var index = 0;
                for (var n = 0; index < _meshTextures.Length; n++)
                {
                	if (string.IsNullOrEmpty(_meshTextureFilenames[index]) || (_meshTextureFilenames[index].ToLower() != (meshDir + str).ToLower()))
                    {
                        n--;
                    }
                    else
                    {
                        Image image = new Bitmap(base.meshDir + str2);
                        var graphics = Graphics.FromImage(image);//��������
                        DrawCenteredString(graphics, ��������, font, brush, 187f, 30f);//(n / 2) * 4
                        DrawCenteredString(graphics, strArray[(n / 2) * 4], font2, brush, 230f, 70f);
                        DrawCenteredString(graphics, strArray[((n / 2) * 4) + 1], font2, brush, 310f, 70f);
                        DrawCenteredString(graphics, strArray[((n / 2) * 4) + 2], font2, brush, 230f, 110f);
                        DrawCenteredString(graphics, strArray[((n / 2) * 4) + 3], font2, brush, 310f, 110f);
                        for (var num16 = 0; num16 < 2; num16++)
                        {
                            var �������2 = �������Array[(n * 2) + num16];
                            if (�������2 == null) continue;
                            DrawCenteredString(graphics, �������2.number, font3, brush, (0xba * num16) + 33.5f, 179f);
                            var num17 = 0;
                            for (var num18 = 0; num18 < _��������.Length; num18++)
                            {
                                if (_��������[num18] != �������2) continue;
                                num17 = num18;
                                break;
                            }
                            var ���������� = _����������[num17];
                            var num19 = 0;
                            num19 += (int) Math.Ceiling(����������.���������.Length / 10.0);
                            if (!����������.����������)
                            {
                                num19 += (int) Math.Ceiling(����������.����������.Length / 10.0);
                            }
                            if (num19 <= 5)
                            {
                                DrawCenteredString(graphics, "����������", font4, brush, (0xba * num16) + 126.5f, 183f);
                                if (!����������.����������)
                                {
                                    if (����������.����������.Length == 0)
                                    {
                                        graphics.DrawString("������ �� ����.", font5, brush2, (0xba * num16) + 5, 209f);
                                    }
                                    else if (����������.���������.Length > 0)
                                    {
                                        graphics.DrawString("�� ����.", font5, brush2, (0xba * num16) + 5, 209f);
                                    }
                                }
                                else
                                {
                                    graphics.DrawString("���������", font5, brush, (0xba * num16) + 5, 209f);
                                }
                                for (var num20 = 0; num20 < ����������.���������.Length; num20++)
                                {
                                    DrawCenteredString(graphics, ����������.Str���������(num20), font5, brush, ((0xba * num16) + 0x12) + (0x25 * (num20 / 10)), 0xde + (12 * (num20 % 10)));
                                }
                                if (!����������.���������� && (����������.����������.Length > 0))
                                {
                                    if (����������.���������.Length == 0)
                                    {
                                        graphics.DrawString("������ �� ���.", font5, brush3, (0xba * num16) + 5, 209f);
                                        for (var num21 = 0; num21 < ����������.����������.Length; num21++)
                                        {
                                            DrawCenteredString(graphics, ����������.Str����������(num21), font5, brush, ((0xba * num16) + 0x12) + (0x25 * (num21 / 10)), 0xde + (12 * (num21 % 10)));
                                        }
                                    }
                                    else
                                    {
                                        var num22 = 0;
                                        if ((����������.���������.Length <= 20) && (����������.����������.Length <= 20))
                                        {
                                            num22 = 0x5d;
                                        }
                                        else
                                        {
                                            var num23 = (int) Math.Ceiling(����������.���������.Length / 10.0);
                                            if ((num23 == 1) && (����������.����������.Length <= 30))
                                            {
                                                num23 = 2;
                                            }
                                            num22 = 0x25 * num23;
                                        }
                                        graphics.DrawLine(pen, ((0xba * num16) + num22) - 1, 0xd1, ((0xba * num16) + num22) - 1, 0x155);
                                        graphics.DrawString("�� ���.", font5, brush3, ((0xba * num16) + num22) + 5, 209f);
                                        for (int num24 = 0; num24 < ����������.����������.Length; num24++)
                                        {
                                            DrawCenteredString(graphics, ����������.Str����������(num24), font5, brush, (((0xba * num16) + num22) + 0x12) + (0x25 * (num24 / 10)), 0xde + (12 * (num24 % 10)));
                                        }
                                    }
                                }
                            }
                            else
                            {
                                DrawCenteredString(graphics, "����� ������", font4, brush, (0xba * num16) + 126.5f, 183f);
                                graphics.DrawLine(pen, (0xba * num16) + 0x43, 0xd1, (0xba * num16) + 0x43, 0x155);
                                graphics.DrawLine(pen, 0xba * num16, 0xfc, (0xba * num16) + 0xb8, 0xfc);
                                graphics.DrawString("���.����.", font5, brush, 0xba * num16, 225f);
                                graphics.DrawString("�����.����.", font5, brush, 0xba * num16, 237f);
                                graphics.DrawString("06:00-10:00", font5, brush, 0xba * num16, 285f);
                                graphics.DrawString("10:00-16:00", font5, brush, 0xba * num16, 297f);
                                graphics.DrawString("16:00-20:00", font5, brush, 0xba * num16, 309f);
                                graphics.DrawString("20:00-00:00", font5, brush, 0xba * num16, 321f);
                                DrawCenteredString(graphics, "��������", font5, brush, (0xba * num16) + 126.5f, 253f);
                                DrawCenteredString(graphics, "��������", font5, brush, (0xba * num16) + 126.5f, 265f);
                                if ((!����������.���������� && (����������.���������.Length > 0)) && (����������.����������.Length > 0))
                                {
                                    DrawCenteredString(graphics, "�� ����.", font5, brush2, (0xba * num16) + 0x61, 209f);
                                    DrawCenteredString(graphics, ����������.Str���������(0), font5, brush, (0xba * num16) + 0x61, 225f);
                                    DrawCenteredString(graphics, ����������.Str���������(����������.���������.Length - 1), font5, brush, (0xba * num16) + 0x61, 237f);
                                    DrawCenteredString(graphics, ����������.������������������(0), font5, brush, (0xba * num16) + 0x61, 285f);
                                    DrawCenteredString(graphics, ����������.������������������(1), font5, brush, (0xba * num16) + 0x61, 297f);
                                    DrawCenteredString(graphics, ����������.������������������(2), font5, brush, (0xba * num16) + 0x61, 309f);
                                    DrawCenteredString(graphics, ����������.������������������(3), font5, brush, (0xba * num16) + 0x61, 321f);
                                    DrawCenteredString(graphics, "�� ���.", font5, brush3, (0xba * num16) + 0x9c, 209f);
                                    DrawCenteredString(graphics, ����������.Str����������(0), font5, brush, (0xba * num16) + 0x9c, 225f);
                                    DrawCenteredString(graphics, ����������.Str����������(����������.����������.Length - 1), font5, brush, (0xba * num16) + 0x9c, 237f);
                                    DrawCenteredString(graphics, ����������.�������������������(0), font5, brush, (0xba * num16) + 0x9c, 285f);
                                    DrawCenteredString(graphics, ����������.�������������������(1), font5, brush, (0xba * num16) + 0x9c, 297f);
                                    DrawCenteredString(graphics, ����������.�������������������(2), font5, brush, (0xba * num16) + 0x9c, 309f);
                                    DrawCenteredString(graphics, ����������.�������������������(3), font5, brush, (0xba * num16) + 0x9c, 321f);
                                }
                                else if (����������.���������� || (����������.����������.Length == 0))
                                {
                                    DrawCenteredString(graphics, (����������.����������) ? "���������" : "������ �� ����.", font5, brush, (0xba * num16) + 126.5f, 209f);
                                    DrawCenteredString(graphics, ����������.Str���������(0), font5, brush, (0xba * num16) + 126.5f, 225f);
                                    DrawCenteredString(graphics, ����������.Str���������(����������.���������.Length - 1), font5, brush, (0xba * num16) + 126.5f, 237f);
                                    DrawCenteredString(graphics, ����������.������������������(0), font5, brush, (0xba * num16) + 126.5f, 285f);
                                    DrawCenteredString(graphics, ����������.������������������(1), font5, brush, (0xba * num16) + 126.5f, 297f);
                                    DrawCenteredString(graphics, ����������.������������������(2), font5, brush, (0xba * num16) + 126.5f, 309f);
                                    DrawCenteredString(graphics, ����������.������������������(3), font5, brush, (0xba * num16) + 126.5f, 321f);
                                }
                                else
                                {
                                    DrawCenteredString(graphics, "������ �� ���.", font5, brush3, (0xba * num16) + 126.5f, 209f);
                                    DrawCenteredString(graphics, ����������.Str����������(0), font5, brush, (0xba * num16) + 126.5f, 225f);
                                    DrawCenteredString(graphics, ����������.Str����������(����������.����������.Length - 1), font5, brush, (0xba * num16) + 126.5f, 237f);
                                    DrawCenteredString(graphics, ����������.�������������������(0), font5, brush, (0xba * num16) + 126.5f, 285f);
                                    DrawCenteredString(graphics, ����������.�������������������(1), font5, brush, (0xba * num16) + 126.5f, 297f);
                                    DrawCenteredString(graphics, ����������.�������������������(2), font5, brush, (0xba * num16) + 126.5f, 309f);
                                    DrawCenteredString(graphics, ����������.�������������������(3), font5, brush, (0xba * num16) + 126.5f, 321f);
                                }
                            }
                        }
                        Stream stream = new MemoryStream();
                        image.Save(stream, ImageFormat.Bmp);
                        stream.Seek(0L, SeekOrigin.Begin);
//                        _meshTextures[index] = Texture.FromStream(MyDirect3D.device, stream);
                        base.LoadTextureFromStream(index, stream);
                    }
                    index++;
                }
            }
        }

        public void ����������������(Route[] �����������)
        {
            var list = new List<Route>();
            foreach (var ������� in �����������)
            {
                if (typeOfTransport[�������.typeOfTransport])
                {
                    foreach (var ���� in �������.trips)
                    {
                        if (!������������(����.pathes)) continue;
                        list.Add(�������);
                        break;
                    }
                }
           }
            _�������� = list.ToArray();
            _���������� = new ����������[_��������.Length];
            for (var i = 0; i < _��������.Length; i++)
            {
                _����������[i] = new ����������();
                var list2 = new List<int>();
                var list3 = new List<int>();
                foreach (var ����� in _��������[i].orders)
                {
                    foreach (var ����2 in �����.�����)
                    {
                        var ������Array = ����2.pathes;
                        if (����2.inPark)
                        {
                            ������Array = new Road[����2.inParkIndex];
                            for (var j = 0; j < ������Array.Length; j++)
                            {
                                ������Array[j] = ����2.pathes[j];
                            }
                        }
                        if (������������(������Array))
                        {
                            var num3 = 0.0;
                            foreach (var ������1 in ������Array)
                            {
                                num3 += ������1.�����;
                            }
                            var num4 = 0.0;
                            for (var k = 0; k < (������Array.Length - 1); k++)
                            {
                                if (������Array[k] == road)
                                {
                                    var item = (int) (����2.�����_����������� + (((����2.�����_�������� - ����2.�����_�����������) * (num4 + distance)) / num3));
                                    if (�����.���������)
                                    {
                                        list2.Add(item);
                                    }
                                    if (�����.����������)
                                    {
                                        list3.Add(item);
                                    }
                                }
                                num4 += ������Array[k].�����;
                            }
                        }
                    }
                }
                list2.Sort();
                list3.Sort();
                _����������[i].��������� = list2.ToArray();
                _����������[i].���������� = list3.ToArray();
            }
        }

        public bool ������������(Road[] array����)
        {
            var list = new List<Road>(array����);
            if (list.Contains(road))
            {
                if (���������.Length <= 0)
                {
                    return true;
                }
                if (���������.Length == 1)
                {
                    if (list.Contains(���������[0]))
                    {
                        return true;
                    }
                }
                else
                {
                    var list2 = new List<int>();
                    var index = 0;
                    while (list.IndexOf(���������[0], index) >= 0)
                    {
                        var item = list.IndexOf(���������[0], index);
                        list2.Add(item);
                        index = item + 1;
                    }
                    foreach (var num3 in list2)
                    {
                        var flag = true;
                        for (var i = 0; i < ���������.Length; i++)
                        {
                            if (((num3 + i) >= list.Count) || (list[num3 + i] != ���������[i]))
                            {
                                flag = false;
                                break;
                            }
                        }
                        if (flag)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /*public override string Filename
        {
            get
            {
                if ((_��������.Length <= 4) || �������������������)
                {
                    return "stop.x";//"���������.x";
                }
                return _��������.Length <= 8 ? "stop2.x" : "stop3.x";
            }
        }*/

        public class ����������
        {
            public int[] ���������� = new int[0];
            public int[] ��������� = new int[0];

            public string Str����������(int index)
            {
                var num = (����������[index] / 0xe10) % 0x18;
                var num2 = (����������[index] / 60) % 60;
                return (num + ":" + num2.ToString("00"));
            }

            public string Str���������(int index)
            {
                var num = (���������[index] / 0xe10) % 0x18;
                var num2 = (���������[index] / 60) % 60;
                return (num + ":" + num2.ToString("00"));
            }

            public string �������������������(int timeIndex)
            {
                var numArray = new int[5];
                numArray[4] = ����������.Length - 1;
                numArray[0] = 0;
                numArray[1] = numArray[4];
                numArray[2] = numArray[4];
                numArray[3] = numArray[4];
                for (var i = 0; i < ����������.Length; i++)
                {
                    if ((numArray[1] == numArray[4]) && (����������[i] >= 0x8ca0))
                    {
                        numArray[1] = i;
                    }
                    if ((numArray[2] == numArray[4]) && (����������[i] >= 0xe100))
                    {
                        numArray[2] = i;
                    }
                    if ((numArray[3] == numArray[4]) && (����������[i] >= 0x11940))
                    {
                        numArray[3] = i;
                    }
                }
                var numArray2 = new int[4];
                for (var j = 0; j < numArray2.Length; j++)
                {
                    numArray2[j] = numArray[j + 1] - numArray[j];
                }
                if (numArray2[timeIndex] == 0)
                {
                    return "�";
                }
                var index = timeIndex;
                var num4 = numArray[index + 1];
                if (((index + 2) < 5) && (numArray[index + 2] == num4))
                {
                    num4--;
                    if (numArray2[index] > 1)
                    {
                        numArray2[index]--;
                    }
                }
                var num5 = (int) Math.Ceiling(((����������[num4] - ����������[numArray[index]]) / 60.0) / numArray2[index]);
                return (num5 + " ���.");
            }

            public string ������������������(int timeIndex)
            {
                var numArray = new int[5];
                numArray[4] = ���������.Length - 1;
                numArray[0] = 0;
                numArray[1] = numArray[4];
                numArray[2] = numArray[4];
                numArray[3] = numArray[4];
                for (var i = 0; i < ���������.Length; i++)
                {
                    if ((numArray[1] == numArray[4]) && (���������[i] >= 0x8ca0))
                    {
                        numArray[1] = i;
                    }
                    if ((numArray[2] == numArray[4]) && (���������[i] >= 0xe100))
                    {
                        numArray[2] = i;
                    }
                    if ((numArray[3] == numArray[4]) && (���������[i] >= 0x11940))
                    {
                        numArray[3] = i;
                    }
                }
                var numArray2 = new int[4];
                for (var j = 0; j < numArray2.Length; j++)
                {
                    numArray2[j] = numArray[j + 1] - numArray[j];
                }
                if (numArray2[timeIndex] == 0)
                {
                    return "�";
                }
                var index = timeIndex;
                var num4 = numArray[index + 1];
                if (((index + 2) < 5) && (numArray[index + 2] == num4))
                {
                    num4--;
                    if (numArray2[index] > 1)
                    {
                        numArray2[index]--;
                    }
                }
                var num5 = (int) Math.Ceiling(((���������[num4] - ���������[numArray[index]]) / 60.0) / numArray2[index]);
                return (num5 + " ���.");
            }

            public bool ����������
            {
                get
                {
                    if (���������.Length != ����������.Length)
                    {
                        return false;
                    }
                    for (var i = 0; i < ���������.Length; i++)
                    {
                        if (���������[i] != ����������[i])
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
        }
        
        public int CompareTo(Stop stop)
        {
        	if (stop.road != this.road)
        		return 0;
        	return Math.Sign(this.distance - stop.distance);
        }
        
    }
}