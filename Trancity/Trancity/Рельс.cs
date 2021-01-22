using Common;
using Engine;
using SlimDX;
using System;
using System.Collections;
using System.Drawing;

namespace Trancity
{
    public class Рельс : Road
    {
        private int mesh_num_parts;
        public static double высота_контактной_сети = 5.7;
        public static double длина_стрелки = 4.0;
        public Добавочные_провода добавочные_провода;
        public static double качество_рельсов = 4.0;
        public int предыдущий_рельс;
        public double расстояние_добавочных_проводов;
        public static double расстояние_между_путями = 3.2;
        public int следующий_рельс;
        public static double смещение_стрелки = 0.15;
        public static bool стрелки_наоборот;

        public Рельс(double начало_x, double начало_y, double конец_x, double конец_y, double направление, bool прямой) : base(начало_x, начало_y, конец_x, конец_y, направление, прямой, расстояние_между_путями, расстояние_между_путями)
        {
            this.расстояние_добавочных_проводов = 14.0;
            this.mesh_num_parts = 1;
            this.добавочные_провода = new Добавочные_провода(this);
        }

        public Рельс(double начало_x, double начало_y, double конец_x, double конец_y, double направление_0, double направление_1) : base(начало_x, начало_y, конец_x, конец_y, направление_0, направление_1, расстояние_между_путями, расстояние_между_путями)
        {
            this.расстояние_добавочных_проводов = 14.0;
            this.mesh_num_parts = 1;
            this.добавочные_провода = new Добавочные_провода(this);
        }

        public override Matrix GetMatrix(int index)
        {
//        	if ((!MainForm.in_editor) && (matrixes[index] != Matrix.Identity)) return matrixes[index];
        	if ((!MainForm.in_editor) && (matrixes[index] != MyMatrix.Zero)) return matrixes[index];
//			if (!MainForm.in_editor) return Matrix.Identity;
            bool flag = false;
//            bool flag2 = false;
            if (index >= this.mesh_num_parts)
            {
                index -= this.mesh_num_parts;
                flag = true;
            }
//            if (index >= this.mesh_num_parts)
//            {
//                index -= this.mesh_num_parts;
//                flag2 = true;
//            }
            double num = flag ? -0.762 : 0.762;
            double num2 = flag ? -0.762 : 0.762;
//            if (flag2)
//            {
//                num = 0.0;
//                num2 = 0.0;
//            }
            double num3 = 0.05;//flag2 ? высота_контактной_сети : 0.05;
            double num4 = num3;
            double num5 = 1.5;//flag2 ? 0.3 : 1.5;
            if (MyDirect3D.карта)
            {
                num5 *= 10.0;
            }
            double num6 = (base.Длина * index) / ((double) this.mesh_num_parts);
            double num7 = (base.Длина * (index + 1)) / ((double) this.mesh_num_parts);
            if ((base.высота[0] != base.высота[1]) && !base.кривая)
            {
                if (index <= (this.mesh_num_parts / 2))
                {
                    num6 = (((base.Длина * 2.0) / 10.0) * index) / ((double) (this.mesh_num_parts / 2));
                }
                else
                {
                    num6 = ((base.Длина * 8.0) / 10.0) + ((((base.Длина * 2.0) / 10.0) * ((index - (this.mesh_num_parts / 2)) - 1)) / ((double) (this.mesh_num_parts / 2)));
                }
                if ((index + 1) <= (this.mesh_num_parts / 2))
                {
                    num7 = (((base.Длина * 2.0) / 10.0) * (index + 1)) / ((double) (this.mesh_num_parts / 2));
                }
                else
                {
                    num7 = ((base.Длина * 8.0) / 10.0) + ((((base.Длина * 2.0) / 10.0) * (index - (this.mesh_num_parts / 2))) / ((double) (this.mesh_num_parts / 2)));
                }
            }
            num3 += base.НайтиВысоту(num6);
            num4 += base.НайтиВысоту(num7);
//            if (!flag2)
//            {
                if (this.стрелка_пошёрстная && (index == 0))
                {
                    num7 = длина_стрелки;
                    if ((flag && (this == this.предыдущие_рельсы[this.предыдущий_рельс].следующие_рельсы[0])) && ((this.предыдущие_рельсы[this.предыдущий_рельс].следующий_рельс != 0) && this.предыдущие_рельсы[this.предыдущий_рельс].следующие_рельсы[1].кривая))
                    {
                        num += смещение_стрелки;
                    }
                    if ((!flag && (this == this.предыдущие_рельсы[this.предыдущий_рельс].следующие_рельсы[1])) && ((this.предыдущие_рельсы[this.предыдущий_рельс].следующий_рельс != 1) && this.предыдущие_рельсы[this.предыдущий_рельс].следующие_рельсы[0].кривая))
                    {
                        num -= смещение_стрелки;
                    }
                }
                else if (this.стрелка_противошёрстная && (index == (this.mesh_num_parts - 1)))
                {
                    num6 = num7 - длина_стрелки;
                }
                else
                {
                    if (this.стрелка_пошёрстная && !this.стрелка_противошёрстная)
                    {
                        num6 = длина_стрелки + (((base.Длина - длина_стрелки) * (index - 1)) / ((double) (this.mesh_num_parts - 1)));
                        num7 = длина_стрелки + (((base.Длина - длина_стрелки) * index) / ((double) (this.mesh_num_parts - 1)));
                    }
                    if (!this.стрелка_пошёрстная && this.стрелка_противошёрстная)
                    {
                        num6 = ((base.Длина - длина_стрелки) * index) / ((double) (this.mesh_num_parts - 1));
                        num7 = ((base.Длина - длина_стрелки) * (index + 1)) / ((double) (this.mesh_num_parts - 1));
                    }
                    if (this.стрелка_пошёрстная && this.стрелка_противошёрстная)
                    {
                        num6 = длина_стрелки + (((base.Длина - (2.0 * длина_стрелки)) * (index - 1)) / ((double) (this.mesh_num_parts - 2)));
                        num7 = длина_стрелки + (((base.Длина - (2.0 * длина_стрелки)) * index) / ((double) (this.mesh_num_parts - 2)));
                    }
                }
//            }
            DoublePoint point = base.НайтиКоординаты(num6, num);
            DoublePoint point3 = base.НайтиКоординаты(num7, num2) - point;
            double num8 = point3.Modulus;
            double num9 = point3.Angle;
            float y = ((float) num5) / 2f;
            Matrix matrix = new Matrix();
            matrix.M11 = 1f;
            matrix.M12 = ((float) (num4 - num3)) / y;
            matrix.M22 = 1f;
            matrix.M33 = 1f;
            matrix.M44 = 1f;
            return (((matrix * Matrix.Scaling((float) num8, y, (float) num5)) * Matrix.RotationY(-((float) num9))) * Matrix.Translation((float) point.x, (float) num3, (float) point.y));
        }

        public override void ОбновитьСледующиеДороги(Road[] дороги)
        {
            ArrayList list = new ArrayList();
            ArrayList list2 = new ArrayList();
            ArrayList list3 = new ArrayList();
            foreach (Road дорога in дороги)
            {
                //if ((дорога != this) && (дорога is Рельс))
                if ((дорога == this) || (!(дорога is Рельс))) continue;
                //{
                    DoublePoint point = дорога.концы[0] - base.концы[1];
                    if ((point.Modulus < 0.01) && ((Math.Abs((double) ((дорога.направления[0] - base.направления[1]) + Math.PI)) < 0.0001) || (Math.Abs((double) ((дорога.направления[0] - base.направления[1]) - Math.PI)) < 0.0001)))
                    {
                        if ((list.Count > 0) && (дорога.СтепеньПоворота0 < ((Рельс) list[0]).СтепеньПоворота0))
                        {
                            list.Insert(0, дорога);
                        }
                        else
                        {
                            list.Add(дорога);
                        }
                    }
                    DoublePoint point2 = дорога.концы[1] - base.концы[0];
                    if ((point2.Modulus < 0.01) && ((Math.Abs((double) ((дорога.направления[1] - base.направления[0]) + Math.PI)) < 0.0001) || (Math.Abs((double) ((дорога.направления[1] - base.направления[0]) - Math.PI)) < 0.0001)))
                    {
                        if ((list2.Count > 0) && (дорога.СтепеньПоворота1 > ((Рельс) list2[0]).СтепеньПоворота1))
                        {
                            list2.Insert(0, дорога);
                        }
                        else
                        {
                            list2.Add(дорога);
                        }
                    }
                    DoublePoint point3 = дорога.концы[1] - base.концы[1];
                    if ((point3.Modulus < 0.01) && (((Math.Abs((double) (дорога.направления[1] - base.направления[1])) < 0.0001) || (Math.Abs((double) ((дорога.направления[1] - base.направления[1]) + (Math.PI * 2.0))) < 0.0001)) || (Math.Abs((double) ((дорога.направления[1] - base.направления[1]) - (Math.PI * 2.0))) < 0.0001)))
                    {
                        list3.Add(дорога);
                    }
                //}
            }
            base.следующиеДороги = (Рельс[]) list.ToArray(typeof(Рельс));
            base.предыдущиеДороги = (Рельс[]) list2.ToArray(typeof(Рельс));
            base.соседниеДороги = (Рельс[]) list3.ToArray(typeof(Рельс));
        }

        /*public override string Filename
        {
            get
            {
                return "рельс.x";
            }
        }*/

        public override int MatricesCount
        {
            get
            {
            	if ((matrices_count != -1) && (((Math.Abs(col - Game.col) > 1) || (Math.Abs(row - Game.row) > 1)))) return 0;
                /*if (!MyDirect3D.карта)
                {
                    DoublePoint point = base.НайтиКоординаты(0.0, 0.0);
                    DoublePoint point2 = base.НайтиКоординаты(base.Длина, 0.0);
                    DoublePoint point3 = new DoublePoint(point.x - MyDirect3D.Camera_Position.x, point.y - MyDirect3D.Camera_Position.z);
                    if (point3.модуль > (250.0 + base.Длина))
                    {
                        DoublePoint point4 = new DoublePoint(point2.x - MyDirect3D.Camera_Position.x, point2.y - MyDirect3D.Camera_Position.z);
                        if (point4.модуль > (250.0 + base.Длина))
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
                if (base.кривая)
                {
                    this.mesh_num_parts = (int) (((base.Длина / base.АбсолютныйРадиус) * 22.0) / качество_рельсов);
                    if (this.mesh_num_parts > 200)
                    {
                        this.mesh_num_parts = 200;
                    }
                    if (this.mesh_num_parts < 5)
                    {
                        this.mesh_num_parts = 5;
                    }
                }
                else if (base.высота[0] != base.высота[1])
                {
                    this.mesh_num_parts = (((int) (((base.Длина * 2.0) / 10.0) / качество_рельсов)) * 2) + 1;
                }
                else
                {
                    this.mesh_num_parts = 1;
                    if (this.стрелка_пошёрстная)
                    {
                        this.mesh_num_parts++;
                    }
                    if (this.стрелка_противошёрстная)
                    {
                        this.mesh_num_parts++;
                    }
                }
//                if (!MyDirect3D.вид_сверху)
//                {
//                    return (this.mesh_num_parts * 3);
//                }
                return (this.mesh_num_parts * 2);
            }
        }

        public Рельс[] предыдущие_рельсы
        {
            get
            {
            	return (Рельс[]) base.предыдущиеДороги;
            }
        }

        public Рельс[] следующие_рельсы
        {
            get
            {
            	return (Рельс[]) base.следующиеДороги;
            }
        }

        public Рельс[] соседние_рельсы
        {
            get
            {
            	return (Рельс[]) base.соседниеДороги;
            }
        }

        public bool стрелка_пошёрстная
        {
            get
            {
                return ((this.предыдущие_рельсы.Length > 0) && (this.предыдущие_рельсы[this.предыдущий_рельс].следующие_рельсы.Length > 1));
            }
        }

        public bool стрелка_противошёрстная
        {
            get
            {
                return ((this.следующие_рельсы.Length > 0) && (this.следующие_рельсы[this.следующий_рельс].предыдущие_рельсы.Length > 1));
            }
        }

        public class Добавочные_провода : MeshObject, MeshObject.IFromFile, IMatrixObject//, IVector
        {
            private Рельс рельс;

            public Добавочные_провода(Рельс рельс)
            {
                this.рельс = рельс;
            }

            public Matrix GetMatrix(int index)
            {
                Matrix matrix = Matrix.Scaling(0.5f, 0.5f, 0.5f) * Matrix.RotationY(-((float) this.направление));
                return (matrix * Matrix.Translation((float) координаты.x, ((float) Рельс.высота_контактной_сети) + 0.05f, (float) координаты.y));
            }

            public string Filename
            {
                get
                {
                    return "box.x";
                }
            }

            public int MatricesCount
            {
                get
                {
                    if ((this.рельс != null) && (this.рельс.MatricesCount > 0))
                    {
                        return 1;
                    }
                    return 0;
                }
            }

            public DoublePoint координаты
            {
                get
                {
                    return this.рельс.НайтиКоординаты(this.расстояние, 0.0);
                }
            }

            public double направление
            {
                get
                {
                    return this.рельс.НайтиНаправление(this.расстояние);
                }
            }

            private double расстояние
            {
                get
                {
                    return Math.Max(this.рельс.Длина - this.рельс.расстояние_добавочных_проводов, 0.1);
                }
            }
        }
    }
}

