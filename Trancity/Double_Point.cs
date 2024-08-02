using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Trancity
{
    [StructLayout(LayoutKind.Sequential)]
    public struct DoublePoint
    {
        public double x;
        public double y;
        public static DoublePoint Zero
        {
            get
            {
                return new DoublePoint(0.0, 0.0);
            }
        }
        public DoublePoint(double x, double y)
        {
        	if (double.IsInfinity(x) || double.IsNaN(x))
            {
                throw new Exception("The x coordinate of the point is invalid.");
            }
            if (double.IsInfinity(y) || double.IsNaN(y))
            {
                throw new Exception("The y coordinate of the point is invalid.");
            }
            this.x = x;
            this.y = y;
        }

        public DoublePoint(double angle)
        {
            this.x = Math.Cos(angle);
            this.y = Math.Sin(angle);
        }

        public DoublePoint int_point
        {
            get
            {
                return new DoublePoint(Math.Round(this.x), Math.Round(this.y));
            }
        }
        public DoublePoint floor_point
        {
            get
            {
                return new DoublePoint(Math.Floor(this.x), Math.Floor(this.y));
            }
        }
        public DoublePoint ceiling_point
        {
            get
            {
                return new DoublePoint(Math.Ceiling(this.x), Math.Ceiling(this.y));
            }
        }
        public DoublePoint round_point
        {
            get
            {
                return new DoublePoint(Math.Round(this.x), Math.Round(this.y));
            }
        }
        public DoublePoint abs
        {
            get
            {
                return new DoublePoint(Math.Abs(this.x), Math.Abs(this.y));
            }
        }
        public double модуль
        {
            get
            {
                return Math.Sqrt((this.x * this.x) + (this.y * this.y));
            }
            set
            {
                double num = value / this.модуль;
                this = (DoublePoint) (this * num);
            }
        }
        public double угол
        {
            get
            {
                if ((this.x == 0.0) && (this.y == 0.0))
                {
                    return 0.0;
                }
                double d = this.x / this.модуль;
                if (this.y >= 0.0)
                {
                    return Math.Acos(d);
                }
                return -Math.Acos(d);
            }
            set
            {
                this = (DoublePoint) (new DoublePoint(value) * this.модуль);
            }
        }
        public static explicit operator Point(DoublePoint p)
        {
            return new Point((int) p.int_point.x, (int) p.int_point.y);
        }

        public static Point operator +(Point p1, DoublePoint p2)
        {
            return new Point(p1.X + ((int) p2.int_point.x), p1.Y + ((int) p2.int_point.y));
        }

        public static Point operator -(Point p1, DoublePoint p2)
        {
            return new Point(p1.X - ((int) p2.int_point.x), p1.Y - ((int) p2.int_point.y));
        }

        public static DoublePoint operator +(DoublePoint a)
        {
            return new DoublePoint(a.x, a.y);
        }

        public static DoublePoint operator -(DoublePoint a)
        {
            return new DoublePoint(-a.x, -a.y);
        }

        public static DoublePoint operator +(DoublePoint a, DoublePoint b)
        {
            return new DoublePoint(a.x + b.x, a.y + b.y);
        }

        public static DoublePoint operator -(DoublePoint a, DoublePoint b)
        {
            return new DoublePoint(a.x - b.x, a.y - b.y);
        }

        public static DoublePoint operator *(DoublePoint a, DoublePoint b)
        {
            return new DoublePoint((a.x * b.x) - (a.y * b.y), (a.x * b.y) + (a.y * b.x));
        }

        public static DoublePoint operator /(DoublePoint a, DoublePoint b)
        {
            if (!(b == Zero))
            {
                return new DoublePoint(((a.x * b.x) + (a.y * b.y)) / ((b.x * b.x) + (b.y * b.y)), ((a.y * b.x) - (a.x * b.y)) / ((b.x * b.x) + (b.y * b.y)));
            }
            if (a != Zero)
            {
                throw new DivideByZeroException();
            }
            return Zero;
        }

        public static DoublePoint operator *(DoublePoint a, double d)
        {
            return new DoublePoint(a.x * d, a.y * d);
        }

        public static DoublePoint operator /(DoublePoint a, double d)
        {
            return new DoublePoint(a.x / d, a.y / d);
        }

        public static DoublePoint operator %(DoublePoint a, double d)
        {
            return new DoublePoint(a.x % d, a.y % d);
        }

        public static implicit operator DoublePoint(Point a)
        {
            return new DoublePoint((double) a.X, (double) a.Y);
        }

        public static bool operator ==(DoublePoint a, DoublePoint b)
        {
            return ((a.x == b.x) && (a.y == b.y));
        }

        public static bool operator !=(DoublePoint a, DoublePoint b)
        {
            if ((a.x == b.x) && (a.y == b.y))
            {
                return false;
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            return (this == ((DoublePoint) obj));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator <(DoublePoint a, DoublePoint b)
        {
            return ((a.x < b.x) && (a.y < b.y));
        }

        public static bool operator <=(DoublePoint a, DoublePoint b)
        {
            return ((a.x <= b.x) && (a.y <= b.y));
        }

        public static bool operator >(DoublePoint a, DoublePoint b)
        {
            return ((a.x > b.x) && (a.y > b.y));
        }

        public static bool operator >=(DoublePoint a, DoublePoint b)
        {
            return ((a.x >= b.x) && (a.y >= b.y));
        }

        public override string ToString()
        {
            return (this.x.ToString() + ", " + this.y.ToString());
        }
    }
}