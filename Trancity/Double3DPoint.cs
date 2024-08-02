using System;
using System.Runtime.InteropServices;
using SlimDX;
using Common;

namespace Trancity
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Double3DPoint
    {
        public double x;
        public double y;
        public double z;
        public static Double3DPoint Zero
        {
            get
            {
                return new Double3DPoint(0.0, 0.0, 0.0);
            }
        }
        public static Double3DPoint Ident
        {
        	get
        	{
        		return new Double3DPoint();
        	}
        }
        public static Double3DPoint ѕоворот(DoublePoint начальный”гол, double плоский”гол)
        {
            var point = new Double3DPoint(0.0, начальный”гол.y);
            if (point.y == 0.0)
            {
                return new Double3DPoint(начальный”гол.x + плоский”гол, начальный”гол.y);
            }
            var point2 = new DoublePoint(плоский”гол);
            point.y *= point2.x;
            point.угол_x = начальный”гол.x;
            point.xz_point = (point.xz_point * point2.x) + ((new DoublePoint(point.xz_point.угол + MyFeatures.halfPI) * point.xz_point.модуль) * point2.y);
            return point;
        }
        public static Double3DPoint Multiply(Double3DPoint _point, Double3DPoint body, DoublePoint direction)
        {
//        	var pi2 = (Math.PI / 2.0);
        	Double3DPoint point = new Double3DPoint(direction);
			Double3DPoint point1 = new Double3DPoint(direction);
			Double3DPoint point2 = Double3DPoint.ѕоворот(direction, MyFeatures.halfPI);			
			point1.угол_y += MyFeatures.halfPI;
			return (Double3DPoint)(body + (((point * _point.x) + (point2 * _point.z)) + point1 * _point.y));
        }

        public Double3DPoint(double x, double y, double z)
        {
            if (double.IsInfinity(x) || double.IsNaN(x))
            {
                throw new Exception("The x coordinate of the point is invalid.");
            }
            if (double.IsInfinity(y) || double.IsNaN(y))
            {
                throw new Exception("The y coordinate of the point is invalid.");
            }
            if (double.IsInfinity(z) || double.IsNaN(z))
            {
                throw new Exception("The z coordinate of the point is invalid.");
            }
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Double3DPoint(double angle_x, double angle_y)
        {
            this.x = Math.Cos(angle_x);
            this.z = Math.Sin(angle_x);
            this.y = Math.Sin(angle_y);
            this.x *= Math.Cos(angle_y);
            this.z *= Math.Cos(angle_y);
        }

        public Double3DPoint(DoublePoint angle) : this(angle.x, angle.y)
        {
        }

        public DoublePoint xz_point
        {
            get
            {
                return new DoublePoint(this.x, this.z);
            }
            set
            {
                this.x = value.x;
                this.z = value.y;
            }
        }
        public DoublePoint xy_point
        {
            get
            {
                return new DoublePoint(this.x, this.y);
            }
            set
            {
                this.x = value.x;
                this.y = value.y;
            }
        }
        public DoublePoint y_point
        {
            get
            {
                return new DoublePoint(this.xz_point.модуль, this.y);
            }
            set
            {
                this.xz_point = (DoublePoint) (new DoublePoint(this.xz_point.угол) * value.x);
                this.y = value.y;
            }
        }
        public double модуль
        {
            get
            {
                return Math.Sqrt(((this.x * this.x) + (this.y * this.y)) + (this.z * this.z));
            }
            set
            {
                double num = value / this.модуль;
                this = (Double3DPoint) (this * num);
            }
        }
        public DoublePoint угол
        {
            get
            {
                return new DoublePoint(this.xz_point.угол, this.y_point.угол);
            }
            set
            {
                this = (Double3DPoint) (new Double3DPoint(value) * this.модуль);
            }
        }
        public double угол_x
        {
            get
            {
                return this.угол.x;
            }
            set
            {
                this.угол = new DoublePoint(value, this.угол.y);
            }
        }
        public double угол_y
        {
            get
            {
                return this.угол.y;
            }
            set
            {
                this.угол = new DoublePoint(this.угол.x, value);
            }
        }
        public static Double3DPoint operator +(Double3DPoint a)
        {
            return new Double3DPoint(a.x, a.y, a.z);
        }

        public static Double3DPoint operator -(Double3DPoint a)
        {
            return new Double3DPoint(-a.x, -a.y, -a.z);
        }

        public static Double3DPoint operator +(Double3DPoint a, Double3DPoint b)
        {
            return new Double3DPoint(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static Double3DPoint operator -(Double3DPoint a, Double3DPoint b)
        {
            return new Double3DPoint(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static Double3DPoint operator *(Double3DPoint a, double d)
        {
            return new Double3DPoint(a.x * d, a.y * d, a.z * d);
        }

        public static Double3DPoint operator /(Double3DPoint a, double d)
        {
            return new Double3DPoint(a.x / d, a.y / d, a.z / d);
        }

        public static Double3DPoint operator %(Double3DPoint a, double d)
        {
            return new Double3DPoint(a.x % d, a.y % d, a.z % d);
        }

        public static implicit operator Double3DPoint(Vector3 item)
        {
        	return new Double3DPoint(item.X, item.Y, item.Z);
        }
        
        public static implicit operator Vector3(Double3DPoint item)
        {
        	return new Vector3((float)item.x, (float)item.y, (float)item.z);
        }

        public static implicit operator Double3DPoint(Matrix item)
        {
        	return new Double3DPoint((double)item.M41, (double)item.M42, (double)item.M43);
        }
        
        public override string ToString()
        {
            return (this.x.ToString() + ", " + this.y.ToString() + ", " + this.z.ToString());
        }
    }
}