namespace Trancity
{
    using Common;
//    using Microsoft.DirectX;
    using SlimDX;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;

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
        	double num = !MyDirect3D.вид_сверху ? 0.5 : 1.5;
            if (MyDirect3D.карта)
            {
                num *= 10.0;
            }
            DoublePoint point = this.конец - this.начало;
            double num2 = point.модуль;
            double num3 = point.угол;
            double num4 = this.высота[0] + высота_контактной_сети;
            float y = ((float) num) / 2f;
            var matrix = new Matrix();
            matrix.M11 = 1f;
            matrix.M22 = 1f;
            matrix.M33 = 1f;
            matrix.M44 = 1f;
            matrix.M12 = ((float) (this.высота[1] - this.высота[0])) / y;
            return (((matrix * Matrix.Scaling((float) num2, y, (float) num)) * Matrix.RotationY(-((float) num3))) * Matrix.Translation((float) this.начало.x, (float) num4, (float) this.начало.y));
        }

        public void ComputeMatrix()
        {
        	if (MainForm.in_editor) return;
        	last_matrix = GetMatrix(0);
        }

        public double найти_высоту(double расстояние)
        {
            return (this.высота[0] + (((this.высота[1] - this.высота[0]) * расстояние) / this.длина));
        }

        public DoublePoint найти_координаты(double расстояние, double отклонение)
        {
            DoublePoint point = new DoublePoint(0.0, 0.0);
            double num = расстояние / this.длина;
            DoublePoint point2 = this.конец - this.начало;
            point = this.начало + ((DoublePoint) (point2 * num));
            point2.угол += Math.PI / 2.0;//1.5707963267948966;
            point2.модуль = отклонение;
            return (point + point2);
        }

        public virtual void обновить_следующие_провода(Контактный_провод[] провода)
        {
            List<Контактный_провод> list = new List<Контактный_провод>();
            List<Контактный_провод> list2 = new List<Контактный_провод>();
            foreach (Контактный_провод _провод in провода)
            {
            	if ((_провод == this) || (_провод is Трамвайный_контактный_провод)) continue;
                DoublePoint point4;
                /*if (_провод == this)
                {
                    goto Label_011C;
                }*/
                DoublePoint point = _провод.начало - this.конец;
                if (point.модуль < 0.01)
                {
                    if (list.Count > 0)
                    {
                        DoublePoint point2 = new DoublePoint(_провод.направление - this.направление);
                        DoublePoint point3 = new DoublePoint(list[0].направление - this.направление);
                        if (point2.угол > point3.угол)
                        {
                            list.Insert(0, _провод);
                            goto Label_009F;
                        }
                    }
                    list.Add(_провод);
                }
            Label_009F:
                point4 = _провод.конец - this.начало;
                if (point4.модуль < 0.01)
                {
                    if (list2.Count > 0)
                    {
                        DoublePoint point5 = new DoublePoint(_провод.направление - this.направление);
                        DoublePoint point6 = new DoublePoint(list2[0].направление - this.направление);
                        if (point5.угол > point6.угол)
                        {
                            list2.Insert(0, _провод);
                            continue;//goto Label_011C;
                        }
                    }
                    list2.Add(_провод);
                }
//            Label_011C:;
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
                    DoublePoint point = this.найти_координаты(0.0, 0.0);
                    DoublePoint point2 = this.найти_координаты(this.длина, 0.0);
                    DoublePoint point3 = new DoublePoint(point.x - MyDirect3D.Camera_Position.x, point.y - MyDirect3D.Camera_Position.z);
                    if (point3.модуль > (250.0 + this.длина))
                    {
                        DoublePoint point4 = new DoublePoint(point2.x - MyDirect3D.Camera_Position.x, point2.y - MyDirect3D.Camera_Position.z);
                        if (point4.модуль > (250.0 + this.длина))
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
                return point.модуль;
            }
        }

        public double направление
        {
            get
            {
                DoublePoint point = this.конец - this.начало;
                return point.угол;
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
    	
    	public override void обновить_следующие_провода(Контактный_провод[] провода)
        {
            List<Трамвайный_контактный_провод> list = new List<Трамвайный_контактный_провод>();
            List<Трамвайный_контактный_провод> list2 = new List<Трамвайный_контактный_провод>();
            foreach (Контактный_провод провод in провода)
            {
            	if ((провод == this) || !(провод is Трамвайный_контактный_провод)) continue;
            	var _провод = (Трамвайный_контактный_провод)провод;
                DoublePoint point4;
                DoublePoint point = _провод.начало - base.конец;
                if (point.модуль < 0.01)
                {
                	for (int i = 0; i < list.Count; i++)
                	{
                		DoublePoint point2 = new DoublePoint(((Трамвайный_контактный_провод)_провод).tan_z - this.tan_z);
                    	DoublePoint point3 = new DoublePoint(((Трамвайный_контактный_провод)list[i]).tan_z - this.tan_z);
                        if (point2.угол < point3.угол)
                        {
                            list.Insert(i, _провод);
                            goto Label_009F;
                        }
                	}
                    /*if (list.Count > 0)
                    {
                    	DoublePoint point2 = new DoublePoint(((Трамвайный_контактный_провод)_провод).угол_z - this.угол_z);
                    	DoublePoint point3 = new DoublePoint(((Трамвайный_контактный_провод)list[0]).угол_z - this.угол_z);
                        if (point2.угол < point3.угол)
                        {
                            list.Insert(0, _провод);
                            goto Label_009F;
                        }
                    }*/
                    list.Add(_провод);
                }
            Label_009F:
                point4 = _провод.конец - base.начало;
                if (point4.модуль < 0.01)
                {
                	for (int k = 0; k < list2.Count; k++)
                	{
                		DoublePoint point5 = new DoublePoint(((Трамвайный_контактный_провод)_провод).tan_z - this.tan_z);
                    	DoublePoint point6 = new DoublePoint(((Трамвайный_контактный_провод)list2[k]).tan_z - this.tan_z);
                        if (point5.угол < point6.угол)
                        {
                            list2.Insert(k, _провод);
                            goto Label_009F2;
                        }
                	}
                    /*if (list2.Count > 0)
                    {
                    	DoublePoint point5 = new DoublePoint(((Трамвайный_контактный_провод)_провод).угол_z - this.угол_z);
                    	DoublePoint point6 = new DoublePoint(((Трамвайный_контактный_провод)list2[0]).угол_z - this.угол_z);
                        if (point5.угол < point6.угол)
                        {
                            list2.Insert(0, _провод);
                            continue;
                        }
                    }*/
                    list2.Add(_провод);
                }
                Label_009F2:;
            }
            this.следующие_провода2 = list.ToArray();
            this.предыдущие_провода2 = list2.ToArray();
        }
    	
    	/*public Контактный_провод2[] следующие_провода2
    	{
    		get
    		{
    			return (Контактный_провод2[]) base.следующие_провода;
    		}
    	}
    	
    	public Контактный_провод2[] предыдущие_провода2
    	{
    		get
    		{
    			return (Контактный_провод2[]) base.предыдущие_провода;
    		}
    	}*/
    	
    	private double tan_z
    	{
    		get
    		{
    			/*var point0 = new Double3DPoint(this.начало.x, this.высота[0], this.начало.y);
    			var point1 = new Double3DPoint(this.конец.x, this.высота[0], this.конец.y);
    			var ang = point1 - point0;
    			return ang.угол_y;*/
    			return (this.высота[1] - this.высота[0]) / base.длина;
    		}
    	}
    }
}

