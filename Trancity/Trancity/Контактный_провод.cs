using Common;
using Engine;
using SlimDX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace Trancity
{
    public class Контактный_провод : MeshObject, MeshObject.IFromFile, IMatrixObject, IObjectContainer, ITest
    {
        private ArrayList fобъекты = new ArrayList();
        public double[] высота = new double[2];
        public DoublePoint конец;
        public DoublePoint начало;
        public bool обесточенный;
        public bool правый;
        public Контактный_провод[] предыдущие_провода = new Контактный_провод[0];
        public static double расстояние_между_проводами = 0.65;
        public Контактный_провод[] следующие_провода = new Контактный_провод[0];

        public Контактный_провод(double начало_x, double начало_y, double конец_x, double конец_y, bool правый)
        {
            this.начало = new DoublePoint(начало_x, начало_y);
            this.конец = new DoublePoint(конец_x, конец_y);
            this.правый = правый;
        }

        public Matrix GetMatrix(int index)
        {
            if (last_matrix != MyMatrix.Zero)//.Zero)
            {
                return last_matrix;
            }
            float num = MyDirect3D.вид_сверху ? 1.5f : 0.5f;
            if (MyDirect3D.карта)
            {
                num *= 10.0f;
            }
            DoublePoint point = this.конец - this.начало;
            double num2 = point.Modulus;
            double num3 = point.Angle;
            double num4 = this.высота[0] + высота_контактной_сети;
            float y = num / 2.0f;
            var matrix = new Matrix();
            matrix.M11 = 1f;
            matrix.M22 = 1f;
            matrix.M33 = 1f;
            matrix.M44 = 1f;
            matrix.M12 = ((float)(this.высота[1] - this.высота[0])) / y;
            return (((matrix * Matrix.Scaling((float)num2, y, num)) * Matrix.RotationY(-((float)num3))) * Matrix.Translation((float)this.начало.x, (float)num4, (float)this.начало.y));
        }

        public void ComputeMatrix()
        {
            if (MainForm.in_editor) return;
            last_matrix = GetMatrix(0);
        }

        public double FindHeight(double расстояние)
        {
            return (this.высота[0] + (((this.высота[1] - this.высота[0]) * расстояние) / this.длина));
        }

        public DoublePoint FindCoords(double расстояние, double отклонение)
        {
            DoublePoint point2 = this.конец - this.начало;
            DoublePoint point = this.начало + (point2 * (расстояние / this.длина));
            point2.Angle += (Math.PI / 2.0);
            point2.Modulus = отклонение;
            return point.Add(ref point2);
        }

        public virtual void UpdateNextWires(Контактный_провод[] провода)
        {
            List<Контактный_провод> list = new List<Контактный_провод>();
            List<Контактный_провод> list2 = new List<Контактный_провод>();
            foreach (Контактный_провод _провод in провода)
            {
                if ((_провод == this) || (_провод is Трамвайный_контактный_провод)) continue;
                DoublePoint point4;
                DoublePoint point = _провод.начало - this.конец;
                if (point.Modulus < 0.01)
                {
                    if (list.Count > 0)
                    {
                        DoublePoint point2 = new DoublePoint(_провод.направление - this.направление);
                        DoublePoint point3 = new DoublePoint(list[0].направление - this.направление);
                        if (point2.Angle > point3.Angle)
                        {
                            list.Insert(0, _провод);
                            goto Label_009F;
                        }
                    }
                    list.Add(_провод);
                }
            Label_009F:
                point4 = _провод.конец - this.начало;
                if (point4.Modulus < 0.01)
                {
                    if (list2.Count > 0)
                    {
                        DoublePoint point5 = new DoublePoint(_провод.направление - this.направление);
                        DoublePoint point6 = new DoublePoint(list2[0].направление - this.направление);
                        if (point5.Angle > point6.Angle)
                        {
                            list2.Insert(0, _провод);
                            continue;
                        }
                    }
                    list2.Add(_провод);
                }
            }
            this.следующие_провода = list.ToArray();
            this.предыдущие_провода = list2.ToArray();
        }

        public int color
        {
            get
            {
                if ((base._meshMaterials != null) && (base._meshMaterials.Length > 0))
                {
                    return base._meshMaterials[0].Diffuse.ToArgb();
                }
                return 0;
            }
            set
            {
                if ((base._meshMaterials != null) && (base._meshMaterials.Length > 0))
                {
                    base._meshMaterials[0].Diffuse = Color.FromArgb(value | -16777216);
                    base._meshMaterials[0].Ambient = Color.FromArgb(value | -16777216);
                }
            }
        }

        public string Filename
        {
            get
            {
                return "wire.x";
            }
        }

        public int MatricesCount
        {
            get
            {
                if (!MyDirect3D.карта)
                {
                    DoublePoint point = this.FindCoords(0.0, 0.0);
                    DoublePoint point2 = this.FindCoords(this.длина, 0.0);
                    DoublePoint point3 = new DoublePoint(point.x - MyDirect3D.Camera_Position.x, point.y - MyDirect3D.Camera_Position.z);
                    if (point3.Modulus > (250.0 + this.длина))
                    {
                        DoublePoint point4 = new DoublePoint(point2.x - MyDirect3D.Camera_Position.x, point2.y - MyDirect3D.Camera_Position.z);
                        if (point4.Modulus > (250.0 + this.длина))
                        {
                            return 0;
                        }
                    }
                }
                return 1;
            }
        }

        public static double высота_контактной_сети
        {
            get
            {
                return Рельс.высота_контактной_сети;
            }
        }

        public double длина
        {
            get
            {
                DoublePoint point = this.конец - this.начало;
                return point.Modulus;
            }
        }

        public double направление
        {
            get
            {
                DoublePoint point = this.конец - this.начало;
                return point.Angle;
            }
        }

        public ArrayList objects
        {
            get
            {
                return this.fобъекты;
            }
        }
    }


    public class Трамвайный_контактный_провод : Контактный_провод
    {
        public Трамвайный_контактный_провод[] следующие_провода2 = new Трамвайный_контактный_провод[0];
        public Трамвайный_контактный_провод[] предыдущие_провода2 = new Трамвайный_контактный_провод[0];

        public Трамвайный_контактный_провод(double начало_x, double начало_y, double конец_x, double конец_y) : base(начало_x, начало_y, конец_x, конец_y, false)
        {

        }

        public override void UpdateNextWires(Контактный_провод[] провода)
        {
            List<Трамвайный_контактный_провод> list = new List<Трамвайный_контактный_провод>();
            List<Трамвайный_контактный_провод> list2 = new List<Трамвайный_контактный_провод>();
            foreach (Контактный_провод провод in провода)
            {
                if ((провод == this) || !(провод is Трамвайный_контактный_провод)) continue;
                var _провод = (Трамвайный_контактный_провод)провод;
                DoublePoint point4;
                DoublePoint point = _провод.начало - base.конец;
                if (point.Modulus < 0.01)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        DoublePoint point2 = new DoublePoint(((Трамвайный_контактный_провод)_провод).tan_z - this.tan_z);
                        DoublePoint point3 = new DoublePoint(((Трамвайный_контактный_провод)list[i]).tan_z - this.tan_z);
                        if (point2.Angle < point3.Angle)
                        {
                            list.Insert(i, _провод);
                            goto Label_009F;
                        }
                    }
                    list.Add(_провод);
                }
            Label_009F:
                point4 = _провод.конец - base.начало;
                if (point4.Modulus < 0.01)
                {
                    for (int k = 0; k < list2.Count; k++)
                    {
                        DoublePoint point5 = new DoublePoint(((Трамвайный_контактный_провод)_провод).tan_z - this.tan_z);
                        DoublePoint point6 = new DoublePoint(((Трамвайный_контактный_провод)list2[k]).tan_z - this.tan_z);
                        if (point5.Angle < point6.Angle)
                        {
                            list2.Insert(k, _провод);
                            goto Label_009F2;
                        }
                    }
                    list2.Add(_провод);
                }
            Label_009F2:;
            }
            this.следующие_провода2 = list.ToArray();
            this.предыдущие_провода2 = list2.ToArray();
        }

        private double tan_z
        {
            get
            {
                return (this.высота[1] - this.высота[0]) / base.длина;
            }
        }
    }
}

