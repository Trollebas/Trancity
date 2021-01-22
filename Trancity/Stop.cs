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
        private Route[] _маршруты;
        public string название;
        public static bool не«агружаем артинки;
        private –асписание[] _расписани€;
        public Road[] частьѕути;

        public Stop(string mname, TypeOfTransport вид“ранспорта, Road дорога, double рассто€ние) : base(mname)
        {
            частьѕути = new Road[0];
            название = "";
            _маршруты = new Route[0];
            _расписани€ = new –асписание[0];
            typeOfTransport = вид“ранспорта;
            base.road = дорога;
            base.distance = рассто€ние;
        }

        public Stop(string mname, TypeOfTransport вид“ранспорта, string название, Road дорога, double рассто€ние) : this(mname, вид“ранспорта, дорога, рассто€ние)
        {
            this.название = название;
        }

        public Stop(string mname, TypeOfTransport вид“ранспорта, Road дорога, double рассто€ние, bool служебна€) : this(mname, вид“ранспорта, дорога, рассто€ние)
        {
            base.serviceStop = служебна€;
        }

        private void DrawCenteredString(Graphics graphics, string s, System.Drawing.Font font, Brush brush, float x, float y)
        {
            graphics.DrawString(s, font, brush, x - (0.5f * graphics.MeasureString(s, font).Width), y);
        }

        public void ќбновить артинку()
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
            if (не«агружаем артинки)
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
                var listArray = new List<Route>[Math.Max(1, (_маршруты.Length + 3) / 4)];
                for (var j = 0; j < listArray.Length; j++)
                {
                    listArray[j] = new List<Route>();
                }
                foreach (var маршрут in _маршруты)
                {
                    int num3;
                    do
                    {
                        num3 = Cheats._random.Next(listArray.Length);
                    }
                    while (listArray[num3].Count >= 4);
                    listArray[num3].Add(маршрут);
                }
                var strArray = new string[4 * listArray.Length];
                var маршрутArray = new Route[4 * listArray.Length];
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
                            while (маршрутArray[num8] != null);
                            маршрутArray[num8] = listArray[k][num7];
                        }
                        for (var num9 = 0; num9 < listArray[k].Count; num9++)
                        {
                            int num10;
                            do
                            {
                                num10 = Cheats._random.Next((4 * k) + 2, (4 * k) + 4);
                            }
                            while (маршрутArray[num10] != null);
                            маршрутArray[num10] = listArray[k][num9];
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
                            while (маршрутArray[num12] != null);
                            маршрутArray[num12] = listArray[k][num11];
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
                        var graphics = Graphics.FromImage(image);//название
                        DrawCenteredString(graphics, название, font, brush, 187f, 30f);//(n / 2) * 4
                        DrawCenteredString(graphics, strArray[(n / 2) * 4], font2, brush, 230f, 70f);
                        DrawCenteredString(graphics, strArray[((n / 2) * 4) + 1], font2, brush, 310f, 70f);
                        DrawCenteredString(graphics, strArray[((n / 2) * 4) + 2], font2, brush, 230f, 110f);
                        DrawCenteredString(graphics, strArray[((n / 2) * 4) + 3], font2, brush, 310f, 110f);
                        for (var num16 = 0; num16 < 2; num16++)
                        {
                            var маршрут2 = маршрутArray[(n * 2) + num16];
                            if (маршрут2 == null) continue;
                            DrawCenteredString(graphics, маршрут2.number, font3, brush, (0xba * num16) + 33.5f, 179f);
                            var num17 = 0;
                            for (var num18 = 0; num18 < _маршруты.Length; num18++)
                            {
                                if (_маршруты[num18] != маршрут2) continue;
                                num17 = num18;
                                break;
                            }
                            var расписание = _расписани€[num17];
                            var num19 = 0;
                            num19 += (int) Math.Ceiling(расписание.по–абочим.Length / 10.0);
                            if (!расписание.≈жедневное)
                            {
                                num19 += (int) Math.Ceiling(расписание.по¬ыходным.Length / 10.0);
                            }
                            if (num19 <= 5)
                            {
                                DrawCenteredString(graphics, "–асписание", font4, brush, (0xba * num16) + 126.5f, 183f);
                                if (!расписание.≈жедневное)
                                {
                                    if (расписание.по¬ыходным.Length == 0)
                                    {
                                        graphics.DrawString("только по будн.", font5, brush2, (0xba * num16) + 5, 209f);
                                    }
                                    else if (расписание.по–абочим.Length > 0)
                                    {
                                        graphics.DrawString("по будн.", font5, brush2, (0xba * num16) + 5, 209f);
                                    }
                                }
                                else
                                {
                                    graphics.DrawString("ежедневно", font5, brush, (0xba * num16) + 5, 209f);
                                }
                                for (var num20 = 0; num20 < расписание.по–абочим.Length; num20++)
                                {
                                    DrawCenteredString(graphics, расписание.Strѕо–абочим(num20), font5, brush, ((0xba * num16) + 0x12) + (0x25 * (num20 / 10)), 0xde + (12 * (num20 % 10)));
                                }
                                if (!расписание.≈жедневное && (расписание.по¬ыходным.Length > 0))
                                {
                                    if (расписание.по–абочим.Length == 0)
                                    {
                                        graphics.DrawString("только по вых.", font5, brush3, (0xba * num16) + 5, 209f);
                                        for (var num21 = 0; num21 < расписание.по¬ыходным.Length; num21++)
                                        {
                                            DrawCenteredString(graphics, расписание.Strѕо¬ыходным(num21), font5, brush, ((0xba * num16) + 0x12) + (0x25 * (num21 / 10)), 0xde + (12 * (num21 % 10)));
                                        }
                                    }
                                    else
                                    {
                                        var num22 = 0;
                                        if ((расписание.по–абочим.Length <= 20) && (расписание.по¬ыходным.Length <= 20))
                                        {
                                            num22 = 0x5d;
                                        }
                                        else
                                        {
                                            var num23 = (int) Math.Ceiling(расписание.по–абочим.Length / 10.0);
                                            if ((num23 == 1) && (расписание.по¬ыходным.Length <= 30))
                                            {
                                                num23 = 2;
                                            }
                                            num22 = 0x25 * num23;
                                        }
                                        graphics.DrawLine(pen, ((0xba * num16) + num22) - 1, 0xd1, ((0xba * num16) + num22) - 1, 0x155);
                                        graphics.DrawString("по вых.", font5, brush3, ((0xba * num16) + num22) + 5, 209f);
                                        for (int num24 = 0; num24 < расписание.по¬ыходным.Length; num24++)
                                        {
                                            DrawCenteredString(graphics, расписание.Strѕо¬ыходным(num24), font5, brush, (((0xba * num16) + num22) + 0x12) + (0x25 * (num24 / 10)), 0xde + (12 * (num24 % 10)));
                                        }
                                    }
                                }
                            }
                            else
                            {
                                DrawCenteredString(graphics, "–ежим работы", font4, brush, (0xba * num16) + 126.5f, 183f);
                                graphics.DrawLine(pen, (0xba * num16) + 0x43, 0xd1, (0xba * num16) + 0x43, 0x155);
                                graphics.DrawLine(pen, 0xba * num16, 0xfc, (0xba * num16) + 0xb8, 0xfc);
                                graphics.DrawString("нач.движ.", font5, brush, 0xba * num16, 225f);
                                graphics.DrawString("оконч.движ.", font5, brush, 0xba * num16, 237f);
                                graphics.DrawString("06:00-10:00", font5, brush, 0xba * num16, 285f);
                                graphics.DrawString("10:00-16:00", font5, brush, 0xba * num16, 297f);
                                graphics.DrawString("16:00-20:00", font5, brush, 0xba * num16, 309f);
                                graphics.DrawString("20:00-00:00", font5, brush, 0xba * num16, 321f);
                                DrawCenteredString(graphics, "интервал", font5, brush, (0xba * num16) + 126.5f, 253f);
                                DrawCenteredString(graphics, "движени€", font5, brush, (0xba * num16) + 126.5f, 265f);
                                if ((!расписание.≈жедневное && (расписание.по–абочим.Length > 0)) && (расписание.по¬ыходным.Length > 0))
                                {
                                    DrawCenteredString(graphics, "по будн.", font5, brush2, (0xba * num16) + 0x61, 209f);
                                    DrawCenteredString(graphics, расписание.Strѕо–абочим(0), font5, brush, (0xba * num16) + 0x61, 225f);
                                    DrawCenteredString(graphics, расписание.Strѕо–абочим(расписание.по–абочим.Length - 1), font5, brush, (0xba * num16) + 0x61, 237f);
                                    DrawCenteredString(graphics, расписание.»нтервалыѕо–абочим(0), font5, brush, (0xba * num16) + 0x61, 285f);
                                    DrawCenteredString(graphics, расписание.»нтервалыѕо–абочим(1), font5, brush, (0xba * num16) + 0x61, 297f);
                                    DrawCenteredString(graphics, расписание.»нтервалыѕо–абочим(2), font5, brush, (0xba * num16) + 0x61, 309f);
                                    DrawCenteredString(graphics, расписание.»нтервалыѕо–абочим(3), font5, brush, (0xba * num16) + 0x61, 321f);
                                    DrawCenteredString(graphics, "по вых.", font5, brush3, (0xba * num16) + 0x9c, 209f);
                                    DrawCenteredString(graphics, расписание.Strѕо¬ыходным(0), font5, brush, (0xba * num16) + 0x9c, 225f);
                                    DrawCenteredString(graphics, расписание.Strѕо¬ыходным(расписание.по¬ыходным.Length - 1), font5, brush, (0xba * num16) + 0x9c, 237f);
                                    DrawCenteredString(graphics, расписание.»нтервалыѕо¬ыходным(0), font5, brush, (0xba * num16) + 0x9c, 285f);
                                    DrawCenteredString(graphics, расписание.»нтервалыѕо¬ыходным(1), font5, brush, (0xba * num16) + 0x9c, 297f);
                                    DrawCenteredString(graphics, расписание.»нтервалыѕо¬ыходным(2), font5, brush, (0xba * num16) + 0x9c, 309f);
                                    DrawCenteredString(graphics, расписание.»нтервалыѕо¬ыходным(3), font5, brush, (0xba * num16) + 0x9c, 321f);
                                }
                                else if (расписание.≈жедневное || (расписание.по¬ыходным.Length == 0))
                                {
                                    DrawCenteredString(graphics, (расписание.≈жедневное) ? "ежедневно" : "только по будн.", font5, brush, (0xba * num16) + 126.5f, 209f);
                                    DrawCenteredString(graphics, расписание.Strѕо–абочим(0), font5, brush, (0xba * num16) + 126.5f, 225f);
                                    DrawCenteredString(graphics, расписание.Strѕо–абочим(расписание.по–абочим.Length - 1), font5, brush, (0xba * num16) + 126.5f, 237f);
                                    DrawCenteredString(graphics, расписание.»нтервалыѕо–абочим(0), font5, brush, (0xba * num16) + 126.5f, 285f);
                                    DrawCenteredString(graphics, расписание.»нтервалыѕо–абочим(1), font5, brush, (0xba * num16) + 126.5f, 297f);
                                    DrawCenteredString(graphics, расписание.»нтервалыѕо–абочим(2), font5, brush, (0xba * num16) + 126.5f, 309f);
                                    DrawCenteredString(graphics, расписание.»нтервалыѕо–абочим(3), font5, brush, (0xba * num16) + 126.5f, 321f);
                                }
                                else
                                {
                                    DrawCenteredString(graphics, "только по вых.", font5, brush3, (0xba * num16) + 126.5f, 209f);
                                    DrawCenteredString(graphics, расписание.Strѕо¬ыходным(0), font5, brush, (0xba * num16) + 126.5f, 225f);
                                    DrawCenteredString(graphics, расписание.Strѕо¬ыходным(расписание.по¬ыходным.Length - 1), font5, brush, (0xba * num16) + 126.5f, 237f);
                                    DrawCenteredString(graphics, расписание.»нтервалыѕо¬ыходным(0), font5, brush, (0xba * num16) + 126.5f, 285f);
                                    DrawCenteredString(graphics, расписание.»нтервалыѕо¬ыходным(1), font5, brush, (0xba * num16) + 126.5f, 297f);
                                    DrawCenteredString(graphics, расписание.»нтервалыѕо¬ыходным(2), font5, brush, (0xba * num16) + 126.5f, 309f);
                                    DrawCenteredString(graphics, расписание.»нтервалыѕо¬ыходным(3), font5, brush, (0xba * num16) + 126.5f, 321f);
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

        public void ќбновитьћаршруты(Route[] всећаршруты)
        {
            var list = new List<Route>();
            foreach (var маршрут in всећаршруты)
            {
                if (typeOfTransport[маршрут.typeOfTransport])
                {
                    foreach (var рейс in маршрут.trips)
                    {
                        if (!ѕутьѕодходит(рейс.pathes)) continue;
                        list.Add(маршрут);
                        break;
                    }
                }
           }
            _маршруты = list.ToArray();
            _расписани€ = new –асписание[_маршруты.Length];
            for (var i = 0; i < _маршруты.Length; i++)
            {
                _расписани€[i] = new –асписание();
                var list2 = new List<int>();
                var list3 = new List<int>();
                foreach (var нар€д in _маршруты[i].orders)
                {
                    foreach (var рейс2 in нар€д.рейсы)
                    {
                        var дорогаArray = рейс2.pathes;
                        if (рейс2.inPark)
                        {
                            дорогаArray = new Road[рейс2.inParkIndex];
                            for (var j = 0; j < дорогаArray.Length; j++)
                            {
                                дорогаArray[j] = рейс2.pathes[j];
                            }
                        }
                        if (ѕутьѕодходит(дорогаArray))
                        {
                            var num3 = 0.0;
                            foreach (var дорога1 in дорогаArray)
                            {
                                num3 += дорога1.ƒлина;
                            }
                            var num4 = 0.0;
                            for (var k = 0; k < (дорогаArray.Length - 1); k++)
                            {
                                if (дорогаArray[k] == road)
                                {
                                    var item = (int) (рейс2.врем€_отправлени€ + (((рейс2.врем€_прибыти€ - рейс2.врем€_отправлени€) * (num4 + distance)) / num3));
                                    if (нар€д.по–абочим)
                                    {
                                        list2.Add(item);
                                    }
                                    if (нар€д.по¬ыходным)
                                    {
                                        list3.Add(item);
                                    }
                                }
                                num4 += дорогаArray[k].ƒлина;
                            }
                        }
                    }
                }
                list2.Sort();
                list3.Sort();
                _расписани€[i].по–абочим = list2.ToArray();
                _расписани€[i].по¬ыходным = list3.ToArray();
            }
        }

        public bool ѕутьѕодходит(Road[] arrayѕуть)
        {
            var list = new List<Road>(arrayѕуть);
            if (list.Contains(road))
            {
                if (частьѕути.Length <= 0)
                {
                    return true;
                }
                if (частьѕути.Length == 1)
                {
                    if (list.Contains(частьѕути[0]))
                    {
                        return true;
                    }
                }
                else
                {
                    var list2 = new List<int>();
                    var index = 0;
                    while (list.IndexOf(частьѕути[0], index) >= 0)
                    {
                        var item = list.IndexOf(частьѕути[0], index);
                        list2.Add(item);
                        index = item + 1;
                    }
                    foreach (var num3 in list2)
                    {
                        var flag = true;
                        for (var i = 0; i < частьѕути.Length; i++)
                        {
                            if (((num3 + i) >= list.Count) || (list[num3 + i] != частьѕути[i]))
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
                if ((_маршруты.Length <= 4) || не«агружаем артинки)
                {
                    return "stop.x";//"остановка.x";
                }
                return _маршруты.Length <= 8 ? "stop2.x" : "stop3.x";
            }
        }*/

        public class –асписание
        {
            public int[] по¬ыходным = new int[0];
            public int[] по–абочим = new int[0];

            public string Strѕо¬ыходным(int index)
            {
                var num = (по¬ыходным[index] / 0xe10) % 0x18;
                var num2 = (по¬ыходным[index] / 60) % 60;
                return (num + ":" + num2.ToString("00"));
            }

            public string Strѕо–абочим(int index)
            {
                var num = (по–абочим[index] / 0xe10) % 0x18;
                var num2 = (по–абочим[index] / 60) % 60;
                return (num + ":" + num2.ToString("00"));
            }

            public string »нтервалыѕо¬ыходным(int timeIndex)
            {
                var numArray = new int[5];
                numArray[4] = по¬ыходным.Length - 1;
                numArray[0] = 0;
                numArray[1] = numArray[4];
                numArray[2] = numArray[4];
                numArray[3] = numArray[4];
                for (var i = 0; i < по¬ыходным.Length; i++)
                {
                    if ((numArray[1] == numArray[4]) && (по¬ыходным[i] >= 0x8ca0))
                    {
                        numArray[1] = i;
                    }
                    if ((numArray[2] == numArray[4]) && (по¬ыходным[i] >= 0xe100))
                    {
                        numArray[2] = i;
                    }
                    if ((numArray[3] == numArray[4]) && (по¬ыходным[i] >= 0x11940))
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
                    return "Ч";
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
                var num5 = (int) Math.Ceiling(((по¬ыходным[num4] - по¬ыходным[numArray[index]]) / 60.0) / numArray2[index]);
                return (num5 + " мин.");
            }

            public string »нтервалыѕо–абочим(int timeIndex)
            {
                var numArray = new int[5];
                numArray[4] = по–абочим.Length - 1;
                numArray[0] = 0;
                numArray[1] = numArray[4];
                numArray[2] = numArray[4];
                numArray[3] = numArray[4];
                for (var i = 0; i < по–абочим.Length; i++)
                {
                    if ((numArray[1] == numArray[4]) && (по–абочим[i] >= 0x8ca0))
                    {
                        numArray[1] = i;
                    }
                    if ((numArray[2] == numArray[4]) && (по–абочим[i] >= 0xe100))
                    {
                        numArray[2] = i;
                    }
                    if ((numArray[3] == numArray[4]) && (по–абочим[i] >= 0x11940))
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
                    return "Ч";
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
                var num5 = (int) Math.Ceiling(((по–абочим[num4] - по–абочим[numArray[index]]) / 60.0) / numArray2[index]);
                return (num5 + " мин.");
            }

            public bool ≈жедневное
            {
                get
                {
                    if (по–абочим.Length != по¬ыходным.Length)
                    {
                        return false;
                    }
                    for (var i = 0; i < по–абочим.Length; i++)
                    {
                        if (по–абочим[i] != по¬ыходным[i])
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