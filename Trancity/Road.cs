using Common;
using Engine;
using SlimDX;
using SlimDX.Direct3D9;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Trancity
{
    public class Road : Spline, IObjectContainer///MeshObject, MeshObject.ICustomCreation, /*MeshObject.IFromFile, IMatrixObject,*/ IObjectContainer//, ITest
    {
        private int _defaultAmbient;
        private int _defaultDiffuse;
        private int _meshNumParts;
        public readonly double[] ������;
        public List<���������> ����������������;
        public static double �������������� = 1.0;
        public readonly DoublePoint[] �����;
        public bool ������;
        public readonly double[] �����������;
        public Road[] ����������������;
        //private int _�����������������Max;
        //public int �����������������Min;
        //private bool _���������������������;
        public Road[] ���������������;
        public Road[] ��������������;
        public ��������� ���������;
        public double[] ������;
        public static double ������������ = 5.0;
        public const double uklon_koef = 2.2;
        protected Matrix[] matrixes = null;
        protected int matrices_count = -1;//
        protected int col, row;

        public Road(double ������X, double ������Y, double �����X, double �����Y, double �����������, bool ������, double ������0, double ������1)
        {
            ���������������� = new List<���������>();
            ����� = new DoublePoint[2];
            ����������� = new double[2];
            ������ = new double[2];
            ������ = new double[2];
            //_�����������������Max = -1;
            //_��������������������� = true;
            ���������������� = new Road[0];
            ��������������� = new Road[0];
            �������������� = new Road[0];
            _defaultDiffuse = -1;
            _defaultAmbient = -1;
            objects = new ArrayList();
            _meshNumParts = 1;
            �����[0] = new DoublePoint(������X, ������Y);
            �����[1] = new DoublePoint(�����X, �����Y);
            �����������[0] = �����������;
            if (������)
            {
                �����������[1] = ����������� + Math.PI;
            }
            else
            {
                var point = �����[0] - �����[1];
                var point2 = �����[1] - �����[0];
                �����������[1] = point.Angle - (�����������[0] - point2.Angle);
            }
            ������ = !������;
            ������[0] = ������0;
            ������[1] = ������1;

            �����������������();

            while (�����������[0] > Math.PI)
            {
                �����������[0] -= Math.PI * 2.0;
            }
            while (�����������[0] <= -Math.PI)
            {
                �����������[0] += Math.PI * 2.0;
            }
            while (�����������[1] > Math.PI)
            {
                �����������[1] -= Math.PI * 2.0;
            }
            while (�����������[1] <= -Math.PI)
            {
                �����������[1] += Math.PI * 2.0;
            }
        }

        public Road(double ������X, double ������Y, double �����X, double �����Y, double �����������0, double �����������1, double ������0, double ������1)
        {
            ���������������� = new List<���������>();
            ����� = new DoublePoint[2];
            ����������� = new double[2];
            ������ = new double[2];
            ������ = new double[2];
            //_�����������������Max = -1;
            //_��������������������� = true;
            ���������������� = new Road[0];
            ��������������� = new Road[0];
            �������������� = new Road[0];
            _defaultDiffuse = -1;
            _defaultAmbient = -1;
            objects = new ArrayList();
            _meshNumParts = 1;
            �����[0] = new DoublePoint(������X, ������Y);
            �����[1] = new DoublePoint(�����X, �����Y);
            �����������[0] = �����������0;
            �����������[1] = �����������1;
            ������[0] = ������0;
            ������[1] = ������1;
            �����������������();
            ������ = ����������������;
            while (�����������[0] > Math.PI)
            {
                �����������[0] -= Math.PI * 2.0;
            }
            while (�����������[0] <= -Math.PI)
            {
                �����������[0] += Math.PI * 2.0;
            }
            while (�����������[1] > Math.PI)
            {
                �����������[1] -= Math.PI * 2.0;
            }
            while (�����������[1] <= -Math.PI)
            {
                �����������[1] += Math.PI * 2.0;
            }
        }

        public override void CreateCustomMesh()//override void CreateMesh()
        {
            /*base.CreateMesh();
            if (MainForm.in_editor)//(MyDirect3D.���_������)
            {
            	LoadTexture(0, "������_editor.png");//_meshTextures[0] = null;
            }
            else
            {
            	LoadTexture(0, "������.png");
            }*///-----------------------------------------------
            /*if (MainForm.in_editor)//(MyDirect3D.���_������)
            {
            	base.CreateMesh();
            	LoadTexture(0, "������_editor.png");
            	return;
            }
            Double3DPoint[] points = new Double3DPoint[2];
            points[0] = new Double3DPoint(1.0, 0.0, 0.0);
            points[1] = new Double3DPoint(-1.0, 0.0, 1.0);*/
            matrices_count = MatricesCount;
            //            double default_length = 10.0;
            double dist;
            double width;
            double height;
            double additional_angel;
            int arpos = 0;
            DoublePoint pos;
            /*///old version by vertex strip
            bool flag = true;
            int shift;
            vertexes = new MeshVertex[(matrices_count - 1) * model.points.Length * 2];
            for (int i = 0; i < matrices_count - 1; i++)
            {
            	for (int k = 0; k < 2; k++)
            	{
            		dist = ����� * (k + i) / (matrices_count - 1);
            		height = �����������(dist);
            		weigth = �����������(dist) / 2.0;
            		additional_angel = Math.Cos(����������������Y(dist));
            		for (int j = 0; j < model.points.Length; j++)
            		{
            			shift = flag ? j : model.points.Length - 1 - j;
            			pos = ���������������(dist, model.noscale ? model.points[shift].x : model.points[shift].x * weigth).Subtract(�����[0]);
            			vertexes[arpos + shift * 2 + k].Position = new Vector3((float)pos.x, (float)(height + additional_angel * model.points[shift].y), (float)pos.y);
            			//incorrect normal computing
            			vertexes[arpos + shift * 2 + k].Normal = vertexes[arpos + shift * 2 + k].Position + new Vector3(0.0f, (float)additional_angel, 0.0f);
            			vertexes[arpos + shift * 2 + k].texcoord = new Vector2((float)(dist / weigth), (float)model.points[shift].z);
            		}
            	}
            	arpos += model.points.Length * 2;
            	flag = !flag;
            }/**/
            /**/
            //new version by index array
            vertexes = new MeshVertex[matrices_count * model.points.Length];
            double direction;
            Vector3 normal;
            for (int i = 0; i < matrices_count; i++)
            {
                dist = ����� * i / (matrices_count - 1);
                width = �����������(dist) / 2.0;
                ��������������������(dist, out height, out additional_angel);
                direction = ����������������(dist);
                normal = MyFeatures.ToVector3(new Double3DPoint(direction, additional_angel + (Math.PI / 2.0)));
                normal.Normalize();
                for (int j = 0; j < model.points.Length; j++)
                {
                    pos = ���������������(dist, model.noscale ? model.points[j].x : model.points[j].x * width).Subtract(ref ���������.���������);
                    vertexes[i * model.points.Length + j].Position = new Vector3((float)pos.x, (float)(height + Math.Cos(additional_angel) * model.points[j].y), (float)pos.y);
                    //TODO: custom normal vector
                    vertexes[i * model.points.Length + j].Normal = normal;
                    vertexes[i * model.points.Length + j].texcoord = new Vector2((float)(dist / width), (float)model.points[j].z);
                }
            }
            poly_count = (model.points.Length - 1) * (matrices_count - 1) * 2;
            indexes = new int[base.poly_count * 3];
            for (int i = 0; i < matrices_count - 1; i++)
            {
                for (int j = 0; j < model.points.Length - 1; j++)
                {
                    arpos = ((model.points.Length - 1) * i + j) * 6;
                    indexes[arpos] = i * model.points.Length + j;
                    indexes[arpos + 2] = indexes[arpos] + 1;
                    indexes[arpos + 1] = indexes[arpos] + model.points.Length;
                    indexes[arpos + 3] = indexes[arpos + 2];
                    indexes[arpos + 5] = indexes[arpos + 1] + 1;
                    indexes[arpos + 4] = indexes[arpos + 1];
                }
            }
            /**/
            base._meshMaterials = new Material[1];
            _meshMaterials[0] = new Material();
            _meshMaterials[0].Diffuse = new Color4(0.949f, 0.949f, 0.949f, 0.949f);//0.949f 1.0f �����
            _meshMaterials[0].Specular = new Color4(0.949f, 0.949f, 0.949f, 0.949f);
            _meshMaterials[0].Ambient = new Color4(0.949f, 0.949f, 0.949f, 0.949f);
            _meshMaterials[0].Emissive = new Color4(0.949f, 0.949f, 0.949f, 0.949f);
            _meshMaterials[0].Power = 0;
            _meshTextures = new Texture[1];
            LoadTexture(0, meshDir + model.texture_filename);//"������_test.png");
            matrices_count = 1;
            matrixes = new Matrix[1];
            matrixes[0] = Matrix.Translation((float)���������.���������.x, 0.0f, (float)���������.���������.y);
        }

        public override Matrix GetMatrix(int index)
        {
            if ((!MainForm.in_editor) && (matrixes[index] != MyMatrix.Zero)) return matrixes[index];
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
            double length = �����;
            var num = (length * index) / _meshNumParts;
            var num2 = (length * (index + 1)) / _meshNumParts;
            if ((������[0] != ������[1]) && !������)
            {
                if (index <= (_meshNumParts / 2))
                {
                    num = (((length * 2.0) / 10.0) * index) / (_meshNumParts / 2);
                }
                else
                {
                    num = ((length * 8.0) / 10.0) + ((((length * 2.0) / 10.0) * ((index - (_meshNumParts / 2)) - 1)) / (_meshNumParts / 2));
                }
                if ((index + 1) <= (_meshNumParts / 2))
                {
                    num2 = (((length * 2.0) / 10.0) * (index + 1)) / (_meshNumParts / 2);
                }
                else
                {
                    num2 = ((length * 8.0) / 10.0) + ((((length * 2.0) / 10.0) * (index - (_meshNumParts / 2))) / (_meshNumParts / 2));
                }
            }
            var num3 = �����������(num);
            var num4 = �����������(num2);
            var matrix = new Matrix();
            if (!flag)
            {
                num5 = �����������(num);
                num6 = �����������(num2) - num5;
                point = ���������������(num, num3 / 2.0);
                point2 = ���������������(num2, num4 / 2.0);
                num7 = ����������������(num);
                var point3 = point2 - point;
                point3.Angle -= num7;
                matrix.M11 = (float)point3.x;
                matrix.M12 = (float)num6;
                matrix.M13 = (float)point3.y;
                matrix.M33 = (float)num3;
            }
            else
            {
                num5 = �����������(num2);
                num6 = �����������(num) - num5;
                point = ���������������(num2, -num4 / 2.0);
                point2 = ���������������(num, -num3 / 2.0);
                num7 = ����������������(num2) + Math.PI;
                var point4 = point2 - point;
                point4.Angle -= num7;
                matrix.M11 = (float)point4.x;
                matrix.M12 = (float)num6;
                matrix.M13 = (float)point4.y;
                matrix.M33 = (float)num4;
            }
            matrix.M22 = 1f;
            matrix.M44 = 1f;
            return ((matrix * Matrix.RotationY(-((float)num7))) * Matrix.Translation((float)point.x, (float)num5, (float)point.y));
        }

        public double �����������(double ����������)
        {
            double num;
            double num2;
            ��������������������(����������, out num, out num2);
            return num;
        }

        public DoublePoint ���������������(double ����������, double ����������)
        {
            DoublePoint point, point1;
            double num;
            ������������(����������, out point, out num);
            //            return (point + new DoublePoint(num += MyFeatures.halfPI) * ����������);//1.5707963267948966
            point1 = new DoublePoint(num += (Math.PI / 2.0)).Multyply(����������);
            return point.Add(ref point1);
        }

        public double ����������������(double ����������)
        {
            DoublePoint point;
            double num;
            ������������(����������, out point, out num);
            return num;
        }

        public double ����������������Y(double ����������)
        {
            double num;
            double num2;
            ��������������������(����������, out num, out num2);
            return num2;
        }

        public void ������������(double ����������, out DoublePoint ����������, out double �����������)
        {
            if (������)
            {
                if (���������� < ���������.�����0)
                {
                    var point = �����[0] - ���������.�����0;
                    var num = (���������.����0 * ����������) / ���������.�����0;
                    point.Angle += num;
                    ���������� = ���������.�����0 + point;
                    if (���������.������ > 0.0)
                    {
                        ����������� = point.Angle + (Math.PI / 2.0);
                    }
                    else
                    {
                        ����������� = point.Angle - (Math.PI / 2.0);
                    }
                }
                else
                {
                    var point2 = ���������.��������� - ���������.�����1;
                    var num2 = (���������.����1 * (���������� - ���������.�����0)) / ���������.�����1;
                    point2.Angle += num2;
                    ���������� = ���������.�����1 + point2;
                    if (���������.������ < 0.0)
                    {
                        ����������� = point2.Angle + (Math.PI / 2.0);
                    }
                    else
                    {
                        ����������� = point2.Angle - (Math.PI / 2.0);
                    }
                }
            }
            else
            {
                var num3 = ���������� / �����;
                var point3 = �����[1] - �����[0];
                ���������� = �����[0] + point3.Multyply(num3);// * num3;
                ����������� = �����������[0];
            }
        }

        public void ��������������������(double ����������, out double ������������, out double �����������Y)
        {
            if (������[0] == ������[1])
            {
                ������������ = ������[0];
                �����������Y = 0.0;
            }
            else
            {
                var length = �����;
                var num = length / 10.0;
                var point = new DoublePoint(length - (2.0 * num), ������[1] - ������[0]);
                var d = (length - (2.0 * num)) / point.Modulus;
                var num4 = num / Math.Tan(Math.Acos(d) / 2.0);
                var num5 = (������[1] > ������[0]) ? 1 : -1;
                if (���������� < (num + (num * d)))
                {
                    if (���������� < 0.0)
                    {
                        ���������� = 0.0;
                    }
                    ������������ = ������[0] + (num5 * (num4 - Math.Sqrt((num4 * num4) - (���������� * ����������))));
                    �����������Y = num5 * Math.Asin(���������� / num4);
                }
                else if (���������� > ((length - num) - (num * d)))
                {
                    if (���������� > length)
                    {
                        ���������� = length;
                    }
                    ������������ = ������[1] - (num5 * (num4 - Math.Sqrt((num4 * num4) - ((length - ����������) * (length - ����������)))));
                    �����������Y = num5 * Math.Asin((length - ����������) / num4);
                }
                else
                {
                    ������������ = ������[0] + (((������[1] - ������[0]) * (���������� - num)) / (length - (2.0 * num)));
                    �����������Y = Math.Atan((������[1] - ������[0]) / (length - (2.0 * num)));
                }
            }
        }

        public double �����������(double ����������)
        {
            return (������[0] + (((������[1] - ������[0]) * ����������) / �����));
        }

        public virtual void �����������������������(Road[] ������)
        {
            var list = new List<Road>();
            var list2 = new List<Road>();
            var list3 = new List<Road>();
            foreach (var ������ in ������)
            {
                if ((������ == this) || (������ is �����)) continue;
                var point = ������.�����[0] - �����[1];//3.1415926535897931
                if ((point.Modulus < 0.01) && ((Math.Abs((������.�����������[0] - �����������[1]) + Math.PI) < 0.0001) || (Math.Abs((������.�����������[0] - �����������[1]) - Math.PI) < 0.0001)))
                {
                    if ((list.Count > 0) && (������.���������������0 < list[0].���������������0))
                    {
                        list.Insert(0, ������);
                    }
                    else
                    {
                        list.Add(������);
                    }
                }
                var point2 = ������.�����[1] - �����[0];
                if ((point2.Modulus < 0.01) && ((Math.Abs((������.�����������[1] - �����������[0]) + Math.PI) < 0.0001) || (Math.Abs((������.�����������[1] - �����������[0]) - Math.PI) < 0.0001)))
                {
                    if ((list2.Count > 0) && (������.���������������1 > list2[0].���������������1))
                    {
                        list2.Insert(0, ������);
                    }
                    else
                    {
                        list2.Add(������);
                    }
                }
                var point3 = ������.�����[1] - �����[1];
                if ((point3.Modulus < 0.01) && (((Math.Abs(������.�����������[1] - �����������[1]) < 0.0001) || (Math.Abs((������.�����������[1] - �����������[1]) + (Math.PI * 2.0)) < 0.0001)) || (Math.Abs((������.�����������[1] - �����������[1]) - (Math.PI * 2.0)) < 0.0001)))
                {
                    list3.Add(������);
                }
            }
            ��������������� = list.ToArray();
            ���������������� = list2.ToArray();
            �������������� = list3.ToArray();
        }

        public void �����������������()
        {
            var ������������������� = �������������������(false);
            var ���������2 = �������������������(true);
            if (���������2.������ == 0.0)
            {
                ��������� = �������������������;
            }
            else if ((�������������������.������ == 0.0) || ((�������������������.�����0 + �������������������.�����1) > (���������2.�����0 + ���������2.�����1)))
            {
                ��������� = ���������2;
            }
            else
            {
                ��������� = �������������������;
            }
        }

        public ��������� �������������������(bool index)
        {
            DoublePoint point5;
            var ������������������� = new ���������();
            var startpoint = �����[0];
            var endpoint = �����[1];
            var startangle = �����������[0] + (Math.PI / 2.0);
            var endangel = �����������[1] + (Math.PI / 2.0);
            //            var angel_center = (new DoublePoint(startangle) - new DoublePoint(endangel)) / 2.0;
            var dendangel = new DoublePoint(endangel);
            var angel_center = new DoublePoint(startangle).Subtract(ref dendangel).Divide(2.0);
            DoublePoint point_center = (startpoint - endpoint).Divide(2.0);
            dendangel.CopyFromAngle(point_center.Angle);
            angel_center.Divide(ref dendangel);// /= new DoublePoint(point_center.����);
            var num3 = Math.Cos(Math.Asin(angel_center.y));
            /*if (index)
            {
                point5 = new DoublePoint(num3 - angel_center.x, 0.0) * new DoublePoint(point_center.����);
            }
            else
            {
                point5 = new DoublePoint(-num3 - angel_center.x, 0.0) * new DoublePoint(point_center.����);
            }*/
            point5 = new DoublePoint((index ? num3 : -num3) - angel_center.x, 0.0).Multyply(ref dendangel);//(new DoublePoint(point_center.Angle));
            const double num4 = 1E-06;
            if (point5.Modulus >= num4)
            {
                /*point5 = point_center / point5;
                angel_center *= new DoublePoint(point_center.����);*/
                dendangel.CopyFromAngle(point_center.Angle);
                angel_center.Multyply(ref dendangel);
                point5 = point_center.Divide(ref point5);
                �������������������.������ = point5.x;
                //                �������������������.�����0 = startpoint + (new DoublePoint(startangle) * point5);
                //                �������������������.�����1 = endpoint + (new DoublePoint(endangel) * point5);
                �������������������.�����0 = startpoint + new DoublePoint(startangle).Multyply(ref point5);
                �������������������.�����1 = endpoint + new DoublePoint(endangel).Multyply(ref point5);
                �������������������.��������� = (�������������������.�����0 + �������������������.�����1).Divide(2.0);// / 2.0;
                var point61 = startpoint - �������������������.�����0;
                var point6 = (�������������������.��������� - �������������������.�����0).Divide(ref point61);
                �������������������.����0 = (point6.Angle + (Math.PI * 2.0)) % (Math.PI * 2.0);
                point61 = �������������������.��������� - �������������������.�����1;
                var point7 = (endpoint - �������������������.�����1).Divide(ref point61);
                �������������������.����1 = (point7.Angle + (Math.PI * 2.0)) % (Math.PI * 2.0);
                if (�������������������.����0 > ((Math.PI * 2.0) - num4))
                {
                    �������������������.����0 -= (Math.PI * 2.0);
                }
                if (�������������������.����1 > ((Math.PI * 2.0) - num4))
                {
                    �������������������.����1 -= (Math.PI * 2.0);
                }
                if ((point5.x < 0.0) && (�������������������.����0 > num4))
                {
                    �������������������.����0 -= (Math.PI * 2.0);
                }
                if ((point5.x > 0.0) && (�������������������.����1 > num4))
                {
                    �������������������.����1 -= (Math.PI * 2.0);
                }
                �������������������.�����0 = Math.Abs(�������������������.����0 * �������������������.������);
                �������������������.�����1 = Math.Abs(�������������������.����1 * �������������������.������);
            }
            return �������������������;
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

        public override int MatricesCount
        {
            get
            {
                if ((matrices_count != -1) && (((Math.Abs(col - Game.col) > 1) || (Math.Abs(row - Game.row) > 1)))) return 0;
                if (base.bounding_sphere != null)
                {
                    if (!MyDirect3D.SphereInFrustum(bounding_sphere)) return 0;
                }
                if (matrices_count != -1) return matrices_count;
                if (������)
                {
                    _meshNumParts = (int)Math.Floor(((����� / ����������������) * 22.0) / ��������������);
                    if (_meshNumParts > 200)
                    {
                        _meshNumParts = 200;
                    }
                    if (_meshNumParts < 5)
                    {
                        _meshNumParts = 5;
                    }
                }
                else if (������[0] != ������[1])
                {
                    _meshNumParts = (((int)(((����� * 2.0) / 10.0) / ��������������)) * 2) + 1;
                }
                else
                {
                    _meshNumParts = 1;
                }
                return (_meshNumParts * 2);
            }
        }

        public double �����
        {
            get
            {
                if (������)
                {
                    return (���������.�����0 + ���������.�����1);
                }
                //                var point = �����[1] - �����[0];
                //                return point.Modulus;
                return DoublePoint.Distance(ref �����[1], ref �����[0]);
            }
        }

        public ArrayList objects { get; private set; }

        public bool ����������������
        {
            get
            {
                return ������ != 0.0;
            }
        }

        public double ������
        {
            get
            {
                return ���������.������;
            }
        }

        public double ����������������
        {
            get
            {
                return Math.Abs(���������.������);
            }
        }

        public double ���������������0
        {
            get
            {
                var point = ���������������(5.0, 0.0).Subtract(ref �����[0]);// - �����[0];
                point.Angle -= �����������[0];
                return -point.y;
            }
        }

        public double ���������������1
        {
            get
            {
                var point = ���������������(����� - 5.0, 0.0).Subtract(ref �����[1]);// - �����[1];
                point.Angle -= �����������[1];
                return -point.y;
            }
        }

        public void CreateBoundingSphere()
        {
            try
            {
                var length = ����� / 2.0;
                var v0 = �����������(length);
                var p0 = ���������������(length, 0.0);
                var p2 = new Double3DPoint(p0.x, v0, p0.y);
                base.bounding_sphere = new Sphere(Double3DPoint.Zero, length + 5.0);
                base.bounding_sphere.Update(p2, DoublePoint.Zero);
                col = (int)Math.Floor(p0.x / Ground.grid_size);
                row = (int)Math.Floor(p0.y / Ground.grid_size);
            }
            catch { };
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ���������
        {
            public double ������;
            public double ����0;
            public double ����1;
            public DoublePoint �����0;
            public DoublePoint �����1;
            public DoublePoint ���������;
            public double �����0;
            public double �����1;
        }
    }
}