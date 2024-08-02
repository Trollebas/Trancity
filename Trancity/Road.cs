using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Common;
using SlimDX;
using SlimDX.Direct3D9;

namespace Trancity
{
    public class Road : Spline, IObjectContainer///MeshObject, MeshObject.ICustomCreation, /*MeshObject.IFromFile, IMatrixObject,*/ IObjectContainer//, ITest
    {
        private int _defaultAmbient;
        private int _defaultDiffuse;
        private int _meshNumParts;
        public readonly double[] высота;
        public List<Положение> занятыеПоложения;
        public static double качествоДороги = 1.0;
        public readonly DoublePoint[] концы;
        public bool кривая;
        public readonly double[] направления;
        public Road[] предыдущиеДороги;
        //private int _разрешеннаяПолосаMax;
        //public int разрешеннаяПолосаMin;
        //private bool _разрешеноПерестроение;
        public Road[] следующиеДороги;
        public Road[] соседниеДороги;
        public Структура структура;
        public double[] ширина;
        public static double ширинаПолосы = 4.0;
        public const double uklon_koef = 2.2;
//        protected Matrix[] matrixes = null;
        protected int matrices_count = -1;//
        protected int col, row;

        public Road(double началоX, double началоY, double конецX, double конецY, double направление, bool прямая, double ширина0, double ширина1)
        {
            занятыеПоложения = new List<Положение>();
            концы = new DoublePoint[2];
            направления = new double[2];
            ширина = new double[2];
            высота = new double[2];
            //_разрешеннаяПолосаMax = -1;
            //_разрешеноПерестроение = true;
            предыдущиеДороги = new Road[0];
            следующиеДороги = new Road[0];
            соседниеДороги = new Road[0];
            _defaultDiffuse = -1;
            _defaultAmbient = -1;
            objects = new ArrayList();
            _meshNumParts = 1;
            концы[0] = new DoublePoint(началоX, началоY);
            концы[1] = new DoublePoint(конецX, конецY);
            направления[0] = направление;
            if (прямая)
            {
                направления[1] = направление + Math.PI;//3.1415926535897931;
            }
            else
            {
                var point = концы[0] - концы[1];
                var point2 = концы[1] - концы[0];
                направления[1] = point.угол - (направления[0] - point2.угол);
            }
            кривая = !прямая;
            ширина[0] = ширина0;
            ширина[1] = ширина1;
            
            ОбновитьСтруктуру();
            
            while (направления[0] > Math.PI)//3.1415926535897931)
            {
                направления[0] -= Math.PI * 2.0;//6.2831853071795862;
            }
            while (направления[0] <= -Math.PI)//-3.1415926535897931)
            {
                направления[0] += Math.PI * 2.0;//6.2831853071795862;
            }
            while (направления[1] > Math.PI)//3.1415926535897931)
            {
                направления[1] -= Math.PI * 2.0;//6.2831853071795862;
            }
            while (направления[1] <= -Math.PI)//-3.1415926535897931)
            {
                направления[1] += Math.PI * 2.0;//6.2831853071795862;
            }
        }

        public Road(double началоX, double началоY, double конецX, double конецY, double направление0, double направление1, double ширина0, double ширина1)
        {
            занятыеПоложения = new List<Положение>();
            концы = new DoublePoint[2];
            направления = new double[2];
            ширина = new double[2];
            высота = new double[2];
            //_разрешеннаяПолосаMax = -1;
            //_разрешеноПерестроение = true;
            предыдущиеДороги = new Road[0];
            следующиеДороги = new Road[0];
            соседниеДороги = new Road[0];
            _defaultDiffuse = -1;
            _defaultAmbient = -1;
            objects = new ArrayList();
            _meshNumParts = 1;
            концы[0] = new DoublePoint(началоX, началоY);
            концы[1] = new DoublePoint(конецX, конецY);
            направления[0] = направление0;
            направления[1] = направление1;
            ширина[0] = ширина0;
            ширина[1] = ширина1;
            ОбновитьСтруктуру();
            кривая = ОпределитьКривой;
            while (направления[0] > Math.PI)//3.1415926535897931)
            {
                направления[0] -= Math.PI * 2.0;//6.2831853071795862;
            }
            while (направления[0] <= -Math.PI)//-3.1415926535897931)
            {
                направления[0] += Math.PI * 2.0;//6.2831853071795862;
            }
            while (направления[1] > Math.PI)//3.1415926535897931)
            {
                направления[1] -= Math.PI * 2.0;//6.2831853071795862;
            }
            while (направления[1] <= -Math.PI)//-3.1415926535897931)
            {
                направления[1] += Math.PI * 2.0;//6.2831853071795862;
            }
        }

        public override void CreateCustomMesh()//override void CreateMesh()
        {
            /*base.CreateMesh();
            if (MainForm.in_editor)//(MyDirect3D.вид_сверху)
            {
            	LoadTexture(0, "Дорога_editor.png");//_meshTextures[0] = null;
            }
            else
            {
            	LoadTexture(0, "Дорога.png");
            }*///-----------------------------------------------
            /*if (MainForm.in_editor)//(MyDirect3D.вид_сверху)
            {
            	base.CreateMesh();
            	LoadTexture(0, "Дорога_editor.png");
            	return;
            }*/
//            Double3DPoint[] points = new Double3DPoint[2];
//            points[0] = new Double3DPoint(1.0, 0.0, 0.0);
//            points[1] = new Double3DPoint(-1.0, 0.0, 1.0);
            matrices_count = MatricesCount;
//            double default_length = 10.0;//
            bool flag = true;
//            bool noscale = false;
            double dist;
            double weigth;
            double height;
            double additional_angel;
            int arpos = 0;
            int shift;
            DoublePoint pos;
            if (matrices_count == 2)
            {
            	base.vertexes = new MeshVertex[matrices_count * model.points.Length];
            }
            else
            {
            	base.vertexes = new MeshVertex[(matrices_count - 1) * model.points.Length * 2];
            }
//            for (int i = 0; i < vertexes.Length - 1; i += 2)
            for (int i = 0; i < matrices_count - 1; i++)
            {
            	/*dist = Длина * i / (vertexes.Length - 2);
            	height = (float)НайтиВысоту(dist);
            	weigth = flag ? -НайтиШирину(dist) / 2.0 : НайтиШирину(dist) / 2.0;
            	pos = НайтиКоординаты(dist, -weigth);
            	vertexes[i].Position = new Vector3((float)pos.x, height, (float)pos.y);
            	vertexes[i].Normal = vertexes[i].Position + new Vector3(0.0f, 1.0f, 0.0f);
            	vertexes[i].texcoord = new Vector2(1.0f * (i / 2.0f), flag ? 0.0f : 1.0f);
            	pos = НайтиКоординаты(dist, weigth);
            	vertexes[i + 1].Position = new Vector3((float)pos.x, height, (float)pos.y);
            	vertexes[i + 1].Normal = vertexes[i + 1].Position + new Vector3(0.0f, 1.0f, 0.0f);
            	vertexes[i + 1].texcoord = new Vector2(1.0f * (i / 2.0f), !flag ? 0.0f : 1.0f);/
            	dist = Длина * i / (vertexes.Length - 2);
            	height = НайтиВысоту(dist);
            	weigth = НайтиШирину(dist) / 2.0;
            	additional_angel = Math.Sin(НайтиНаправлениеY(dist));
            	/*if ((i > 1) && (matrices_count > 2))
            	{
            		
            	}*/
//            	if ((matrices_count == 2) && (i > 0)) continue;
            	for (int k = 0; k < 2; k++)//((i < matrices_count - 1) ? 2 : 1)
            	{
            		dist = Длина * (k + i) / (matrices_count - 1);//(vertexes.Length - 2);
//            		if (matrices_count == 2) dist *= 2;
            		height = НайтиВысоту(dist);
            		weigth = НайтиШирину(dist) / 2.0;
            		additional_angel = Math.Cos(НайтиНаправлениеY(dist));
            		for (int j = 0; j < model.points.Length; j++)
            		{
            			shift = flag ? j : model.points.Length - 1 - j;
            			pos = НайтиКоординаты(dist, model.noscale ? model.points[shift].x : model.points[shift].x * weigth);
            			vertexes[arpos + shift * 2 + k].Position = new Vector3((float)pos.x, (float)(height + additional_angel * model.points[shift].y), (float)pos.y);
            			vertexes[arpos + shift * 2 + k].Normal = vertexes[arpos + shift * 2 + k].Position + new Vector3(0.0f, (float)additional_angel, 0.0f);
            			vertexes[arpos + shift * 2 + k].texcoord = new Vector2((float)(dist / weigth), (float)model.points[shift].z);
            		}
            	}
            	arpos += model.points.Length * 2;
            	flag = !flag;
            }
            base._meshMaterials = new Material[1];
            _meshMaterials[0] = new Material();
            _meshMaterials[0].Diffuse = new Color4(1.0f, 1.0f, 1.0f, 1.0f);//0.949f
            _meshMaterials[0].Specular = new Color4(1.0f, 1.0f, 1.0f, 1.0f);
            _meshMaterials[0].Ambient = new Color4(1.0f, 0.0f, 0.0f, 0.0f);
            _meshMaterials[0].Emissive = new Color4(1.0f, 0.0f, 0.0f, 0.0f);
            _meshMaterials[0].Power = 0;
            _meshTextures = new Texture[1];
//            _addTexList.Add(new MeshObject.AdditionalTexStruct() { loaded = false, arpos = -1 } );
            _addTexList = new MeshObject.AdditionalTexStruct[1];
            LoadTexture(0, meshDir + model.texture_filename);//"Дорога_test.png");
            matrices_count = 1;
            /*matrixes = new Matrix[1];
        		matrixes[0] = Matrix.Translation(0.0f, 0.0f, 0.0f);*/
        }
        
        /*public void ComputeMatrix()
        {
        	if (MainForm.in_editor) return;
        	matrices_count = MatricesCount;
        	matrixes = new Matrix[matrices_count];
        	for (int i = 0; i < matrices_count; i++)
        	{
        		matrixes[i] = GetMatrix(i);
        	}
        }
        
        public void Test()
        {
        	base.SaveCustomMesh();
        	matrixes = new Matrix[1];
        		matrixes[0] = Matrix.Translation(0.0f, 0.0f, 0.0f);
        	matrices_count = 1;
        }*/
        
        public /*virtual*/ override Matrix GetMatrix(int index)
        {
        	if (!MainForm.in_editor) return Matrix.Identity;
//        	if ((!MainForm.in_editor) && (matrixes[index] != MyMatrix.Zero)) return matrixes[index];
            double num5;
            double num6;
            DoublePoint point;
            DoublePoint point2;
            double num7;
            var flag = false;
            if (index >= _meshNumParts)
            {
                index -= _meshNumParts;
                flag = true;
            }
            var num = (Длина * index) / _meshNumParts;
            var num2 = (Длина * (index + 1)) / _meshNumParts;
            if ((высота[0] != высота[1]) && !кривая)
            {
                if (index <= (_meshNumParts / 2))
                {
                    num = (((Длина * 2.0) / 10.0) * index) / (_meshNumParts / 2);
                }
                else
                {
                    num = ((Длина * 8.0) / 10.0) + ((((Длина * 2.0) / 10.0) * ((index - (_meshNumParts / 2)) - 1)) / (_meshNumParts / 2));
                }
                if ((index + 1) <= (_meshNumParts / 2))
                {
                    num2 = (((Длина * 2.0) / 10.0) * (index + 1)) / (_meshNumParts / 2);
                }
                else
                {
                    num2 = ((Длина * 8.0) / 10.0) + ((((Длина * 2.0) / 10.0) * (index - (_meshNumParts / 2))) / (_meshNumParts / 2));
                }
            }
            var num3 = НайтиШирину(num);
            var num4 = НайтиШирину(num2);
            var matrix = new Matrix();
            if (!flag)
            {
                num5 = НайтиВысоту(num);
                num6 = НайтиВысоту(num2) - num5;
                point = НайтиКоординаты(num, num3 / 2.0);
                point2 = НайтиКоординаты(num2, num4 / 2.0);
                num7 = НайтиНаправление(num);
                var point3 = point2 - point;
                point3.угол -= num7;
                matrix.M11 = (float) point3.x;
                matrix.M12 = (float) num6;
                matrix.M13 = (float) point3.y;
                matrix.M33 = (float) num3;
            }
            else
            {
                num5 = НайтиВысоту(num2);
                num6 = НайтиВысоту(num) - num5;
                point = НайтиКоординаты(num2, -num4 / 2.0);
                point2 = НайтиКоординаты(num, -num3 / 2.0);
                num7 = НайтиНаправление(num2) + Math.PI;//3.1415926535897931;
                var point4 = point2 - point;
                point4.угол -= num7;
                matrix.M11 = (float) point4.x;
                matrix.M12 = (float) num6;
                matrix.M13 = (float) point4.y;
                matrix.M33 = (float) num4;
            }
            matrix.M22 = 1f;
            matrix.M44 = 1f;
            return ((matrix * Matrix.RotationY(-((float) num7))) * Matrix.Translation((float) point.x, (float) num5, (float) point.y));
        }

        public double НайтиВысоту(double расстояние)
        {
            double num;
            double num2;
            НайтиПозициюПоВысоте(расстояние, out num, out num2);
            return num;
        }

        public DoublePoint НайтиКоординаты(double расстояние, double отклонение)
        {
            DoublePoint point;
            double num;
            НайтиПозицию(расстояние, out point, out num);
            return (point + new DoublePoint(num += (Math.PI / 2.0)) * отклонение);//1.5707963267948966
        }

        public double НайтиНаправление(double расстояние)
        {
            DoublePoint point;
            double num;
            НайтиПозицию(расстояние, out point, out num);
            return num;
        }

        public double НайтиНаправлениеY(double расстояние)
        {
            double num;
            double num2;
            НайтиПозициюПоВысоте(расстояние, out num, out num2);
            return num2;
        }

        public void НайтиПозицию(double расстояние, out DoublePoint координаты, out double направление)
        {
            if (кривая)
            {
                if (расстояние < структура.длина0)
                {
                    var point = концы[0] - структура.центр0;
                    var num = (структура.угол0 * расстояние) / структура.длина0;
                    point.угол += num;
                    координаты = структура.центр0 + point;
                    if (структура.радиус > 0.0)
                    {
                        направление = point.угол + MyFeatures.halfPI;//(Math.PI / 2.0);//1.5707963267948966;
                    }
                    else
                    {
                        направление = point.угол - MyFeatures.halfPI;//(Math.PI / 2.0);//1.5707963267948966;
                    }
                }
                else
                {
                    var point2 = структура.серединка - структура.центр1;
                    var num2 = (структура.угол1 * (расстояние - структура.длина0)) / структура.длина1;
                    point2.угол += num2;
                    координаты = структура.центр1 + point2;
                    if (структура.радиус < 0.0)
                    {
                        направление = point2.угол + MyFeatures.halfPI;//(Math.PI / 2.0);//1.5707963267948966;
                    }
                    else
                    {
                        направление = point2.угол - MyFeatures.halfPI;//(Math.PI / 2.0);//1.5707963267948966;
                    }
                }
            }
            else
            {
                var num3 = расстояние / Длина;
                var point3 = концы[1] - концы[0];
                координаты = концы[0] + point3 * num3;
                направление = направления[0];
            }
        }

        public void НайтиПозициюПоВысоте(double расстояние, out double нужнаяВысота, out double направлениеY)
        {
            if (высота[0] == высота[1])
            {
                нужнаяВысота = высота[0];
                направлениеY = 0.0;
            }
            else
            {
            	var length = Длина;
                var num = length / 10.0;
                var point = new DoublePoint(length - (2.0 * num), высота[1] - высота[0]);
                var d = (length - (2.0 * num)) / point.модуль;
                var num3 = Math.Acos(d);
                var num4 = num / Math.Tan(num3 / 2.0);
                var num5 = (высота[1] > высота[0]) ? 1 : -1;
                if (расстояние < (num + (num * d)))
                {
                    if (расстояние < 0.0)
                    {
                        расстояние = 0.0;
                    }
                    нужнаяВысота = высота[0] + (num5 * (num4 - Math.Sqrt((num4 * num4) - (расстояние * расстояние))));
                    направлениеY = num5 * Math.Asin(расстояние / num4);
                }
                else if (расстояние > ((length - num) - (num * d)))
                {
                    if (расстояние > length)
                    {
                        расстояние = length;
                    }
                    нужнаяВысота = высота[1] - (num5 * (num4 - Math.Sqrt((num4 * num4) - ((length - расстояние) * (length - расстояние)))));
                    направлениеY = num5 * Math.Asin((length - расстояние) / num4);
                }
                else
                {
                    нужнаяВысота = высота[0] + (((высота[1] - высота[0]) * (расстояние - num)) / (length - (2.0 * num)));
                    направлениеY = Math.Atan((высота[1] - высота[0]) / (length - (2.0 * num)));
                }
            }
        }

        public double НайтиШирину(double расстояние)
        {
            return (ширина[0] + (((ширина[1] - ширина[0]) * расстояние) / Длина));
        }

        public virtual void ОбновитьСледующиеДороги(Road[] дороги)
        {
            var list = new List<Road>();
            var list2 = new List<Road>();
            var list3 = new List<Road>();
            foreach (var дорога in дороги)
            {
                if ((дорога == this) || (дорога is Рельс)) continue;
                var point = дорога.концы[0] - концы[1];//3.1415926535897931
                if ((point.модуль < 0.01) && ((Math.Abs((дорога.направления[0] - направления[1]) + Math.PI) < 0.0001) || (Math.Abs((дорога.направления[0] - направления[1]) - Math.PI) < 0.0001)))
                {
                    if ((list.Count > 0) && (дорога.СтепеньПоворота0 < list[0].СтепеньПоворота0))
                    {
                        list.Insert(0, дорога);
                    }
                    else
                    {
                        list.Add(дорога);
                    }
                }
                var point2 = дорога.концы[1] - концы[0];
                if ((point2.модуль < 0.01) && ((Math.Abs((дорога.направления[1] - направления[0]) + Math.PI) < 0.0001) || (Math.Abs((дорога.направления[1] - направления[0]) - Math.PI) < 0.0001)))
                {
                    if ((list2.Count > 0) && (дорога.СтепеньПоворота1 > list2[0].СтепеньПоворота1))
                    {
                        list2.Insert(0, дорога);
                    }
                    else
                    {
                        list2.Add(дорога);
                    }
                }
                var point3 = дорога.концы[1] - концы[1];
                if ((point3.модуль < 0.01) && (((Math.Abs(дорога.направления[1] - направления[1]) < 0.0001) || (Math.Abs((дорога.направления[1] - направления[1]) + (Math.PI * 2.0)) < 0.0001)) || (Math.Abs((дорога.направления[1] - направления[1]) - (Math.PI * 2.0)) < 0.0001)))
                {
                    list3.Add(дорога);
                }
            }
            следующиеДороги = list.ToArray();
            предыдущиеДороги = list2.ToArray();
            соседниеДороги = list3.ToArray();
        }

        public void ОбновитьСтруктуру()
        {
            var определитьСтруктуру = ОпределитьСтруктуру(false);
            var структура2 = ОпределитьСтруктуру(true);
            if (структура2.радиус == 0.0)
            {
                структура = определитьСтруктуру;
            }
            else if ((определитьСтруктуру.радиус == 0.0) || ((определитьСтруктуру.длина0 + определитьСтруктуру.длина1) > (структура2.длина0 + структура2.длина1)))
            {
                структура = структура2;
            }
            else
            {
                структура = определитьСтруктуру;
            }
        }

        public Структура ОпределитьСтруктуру(bool index)
        {
            DoublePoint point5;
            var определитьСтруктуру = new Структура();
            var startpoint = концы[0];
            var endpoint = концы[1];
            var startangle = направления[0] + MyFeatures.halfPI;//(Math.PI / 2.0);
            var endangel = направления[1] + MyFeatures.halfPI;//(Math.PI / 2.0);
            var angel_center = (new DoublePoint(startangle) - new DoublePoint(endangel)) / 2.0;
            var point_center = (startpoint - endpoint) / 2.0;
            angel_center /= new DoublePoint(point_center.угол);
            var num3 = Math.Cos(Math.Asin(angel_center.y));
            if (index)
            {
                point5 = new DoublePoint(num3 - angel_center.x, 0.0) * new DoublePoint(point_center.угол);
            }
            else
            {
                point5 = new DoublePoint(-num3 - angel_center.x, 0.0) * new DoublePoint(point_center.угол);
            }
            const double num4 = 1E-06;
            if (point5.модуль >= num4)
            {
                point5 = point_center / point5;
                angel_center *= new DoublePoint(point_center.угол);
                определитьСтруктуру.радиус = point5.x;
                определитьСтруктуру.центр0 = startpoint + (new DoublePoint(startangle) * point5);
                определитьСтруктуру.центр1 = endpoint + (new DoublePoint(endangel) * point5);
                определитьСтруктуру.серединка = (определитьСтруктуру.центр0 + определитьСтруктуру.центр1) / 2.0;
                var point6 = (определитьСтруктуру.серединка - определитьСтруктуру.центр0) / (startpoint - определитьСтруктуру.центр0);
                определитьСтруктуру.угол0 = (point6.угол + (Math.PI * 2.0)) % (Math.PI * 2.0);
                var point7 = (endpoint - определитьСтруктуру.центр1) / (определитьСтруктуру.серединка - определитьСтруктуру.центр1);
                определитьСтруктуру.угол1 = (point7.угол + (Math.PI * 2.0)) % (Math.PI * 2.0);
                if (определитьСтруктуру.угол0 > ((Math.PI * 2.0) - num4))
                {
                    определитьСтруктуру.угол0 -= (Math.PI * 2.0);
                }
                if (определитьСтруктуру.угол1 > ((Math.PI * 2.0) - num4))
                {
                    определитьСтруктуру.угол1 -= (Math.PI * 2.0);
                }
                if ((point5.x < 0.0) && (определитьСтруктуру.угол0 > num4))
                {
                    определитьСтруктуру.угол0 -= (Math.PI * 2.0);
                }
                if ((point5.x > 0.0) && (определитьСтруктуру.угол1 > num4))
                {
                    определитьСтруктуру.угол1 -= (Math.PI * 2.0);
                }
                определитьСтруктуру.длина0 = Math.Abs(определитьСтруктуру.угол0 * определитьСтруктуру.радиус);
                определитьСтруктуру.длина1 = Math.Abs(определитьСтруктуру.угол1 * определитьСтруктуру.радиус);
            }
            return определитьСтруктуру;
        }

        public int Color
        {
            get
            {
                if ((_meshMaterials != null) && (_meshMaterials.Length > 0))
                {
                    return (_meshMaterials[0].Diffuse.ToArgb() & 0xffffff);
                }
                return 0;
            }
            set
            {
                if ((_meshMaterials == null) || (_meshMaterials.Length <= 0)) return;
                if (_defaultDiffuse == -1)
                {
                    _defaultDiffuse = _meshMaterials[0].Diffuse.ToArgb();
                    _defaultAmbient = _meshMaterials[0].Ambient.ToArgb();
                }
                if (value == 0)
                {
                    _meshMaterials[0].Diffuse = System.Drawing.Color.FromArgb(_defaultDiffuse);
                    _meshMaterials[0].Ambient = System.Drawing.Color.FromArgb(_defaultAmbient);
                }
                else
                {
                    _meshMaterials[0].Diffuse = System.Drawing.Color.FromArgb(value | -16777216);
                    _meshMaterials[0].Ambient = System.Drawing.Color.FromArgb(value | -16777216);
                }
            }
        }

        /*public virtual string Filename
        {
            get
            {
                return "дорога.x";
            }
        }*/

        public /*virtual*/ override int MatricesCount
        {
            get
            {
            	if ((matrices_count != -1) && (((Math.Abs(col - Game.col) > 1) || (Math.Abs(row - Game.row) > 1)))) return 0;
            	/*if (!MyDirect3D.карта)
                {
                    var point = НайтиКоординаты(0.0, 0.0);
                    var point2 = НайтиКоординаты(Длина, 0.0);
                    var point3 = new DoublePoint(point.x - MyDirect3D.Camera_Position.x, point.y - MyDirect3D.Camera_Position.z);
                    if (point3.модуль > (250.0 + Длина))
                    {
                        var point4 = new DoublePoint(point2.x - MyDirect3D.Camera_Position.x, point2.y - MyDirect3D.Camera_Position.z);
                        if (point4.модуль > (250.0 + Длина))
                        {
                            return 0;
                        }
                    }
                }*/
            	if (base.bounding_sphere != null)
            	{
            		if (!MyDirect3D.SphereInFrustum(bounding_sphere)) return 0;
            	}
            	if (matrices_count != -1) return matrices_count;
            	if (кривая)
                {
                    _meshNumParts = (int) (((Длина / АбсолютныйРадиус) * 22.0) / качествоДороги);
                    if (_meshNumParts > 200)
                    {
                        _meshNumParts = 200;
                    }
                    if (_meshNumParts < 5)
                    {
                        _meshNumParts = 5;
                    }
                }
                else if (высота[0] != высота[1])
                {
                    _meshNumParts = (((int) (((Длина * 2.0) / 10.0) / качествоДороги)) * 2) + 1;
                }
                else
                {
                    _meshNumParts = 1;
                }
                return (_meshNumParts * 2);
            }
        }

        public double Длина
        {
            get
            {
                if (кривая)
                {
                    return (структура.длина0 + структура.длина1);
                }
                var point = концы[1] - концы[0];
                return point.модуль;
            }
        }

        public ArrayList objects { get; private set; }

        public bool ОпределитьКривой
        {
            get
            {
                return Радиус != 0.0;
            }
        }

        public double Радиус
        {
            get
            {
                return структура.радиус;
            }
        }

        public double АбсолютныйРадиус
        {
            get
            {
                return Math.Abs(структура.радиус);
            }
        }

        public double СтепеньПоворота0
        {
            get
            {
                var point = НайтиКоординаты(5.0, 0.0) - концы[0];
                point.угол -= направления[0];
                return -point.y;
            }
        }

        public double СтепеньПоворота1
        {
            get
            {
                var point = НайтиКоординаты(Длина - 5.0, 0.0) - концы[1];
                point.угол -= направления[1];
                return -point.y;
            }
        }
        
        public void CreateBoundingSphere()
        {
        	try
        	{
        		var length = Длина / 2.0;
				var v0 = НайтиВысоту(length);
				var p0 = НайтиКоординаты(length, 0.0);
        		var p2 = new Double3DPoint(p0.x, v0, p0.y);
        		base.bounding_sphere = new Sphere(Double3DPoint.Zero, length + 5.0);
        		base.bounding_sphere.Update(p2, DoublePoint.Zero);
        		col = (int)Math.Floor(p0.x / Ground.grid_size);
        		row = (int)Math.Floor(p0.y / Ground.grid_size);
        	}
        	catch {};
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Структура
        {
            public double радиус;
            public double угол0;
            public double угол1;
            public DoublePoint центр0;
            public DoublePoint центр1;
            public DoublePoint серединка;
            public double длина0;
            public double длина1;
        }
    }
}