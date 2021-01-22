using System;
using System.Drawing;

namespace Engine
{
    /// <summary>
    /// DoublePoint - Trancity' dinosaur, and that's prooved by name.
    /// </summary>
    public struct DoublePoint : IEquatable<DoublePoint>
    {
        public double x;
        public double y;
        
        public static readonly DoublePoint Zero = new DoublePoint(0.0, 0.0);
        
        public DoublePoint(double x, double y)
        {
            if (double.IsInfinity(x) || double.IsNaN(x))
            {
                throw new Exception("X-координата точки является недействительным.");
            }
            if (double.IsInfinity(y) || double.IsNaN(y))
            {
                throw new Exception("Y-координата точки является недействительным.");
            }
            this.x = x;
            this.y = y;
        }

        public DoublePoint(double angle)
        {
            this.x = Math.Cos(angle);
            this.y = Math.Sin(angle);
        }
        
        public DoublePoint FloorPoint
        {
            get
            {
                return new DoublePoint(Math.Floor(this.x), Math.Floor(this.y));
            }
        }
        
        public DoublePoint CeilingPoint
        {
            get
            {
                return new DoublePoint(Math.Ceiling(this.x), Math.Ceiling(this.y));
            }
        }
        
        public DoublePoint RoundPoint
        {
            get
            {
                return new DoublePoint(Math.Round(this.x), Math.Round(this.y));
            }
        }
        
        public DoublePoint Abs
        {
            get
            {
                return new DoublePoint(Math.Abs(this.x), Math.Abs(this.y));
            }
        }
        
        public double Modulus
        {
            get
            {
                return Math.Sqrt((this.x * this.x) + (this.y * this.y));
            }
            set
            {
                double num = value / this.Modulus;
                this.Multyply(num);
            }
        }
        
        public double Angle
        {
            get
            {
                if ((this.x == 0.0) && (this.y == 0.0))
                    return 0.0;
                double d = this.x / this.Modulus;
                return (this.y >= 0.0) ? Math.Acos(d) : -Math.Acos(d);
            }
            set
            {
                this = new DoublePoint(value).Multyply(this.Modulus);
            }
        }
        
        public void CopyTo(ref DoublePoint point)
        {
            point.x = this.x;
            point.y = this.y;
        }
        
        public void CopyFromAngle(double angle)
        {
            this.x = Math.Cos(angle);
            this.y = Math.Sin(angle);
        }
        
        public DoublePoint Add(ref DoublePoint point)
        {
            this.x += point.x;
            this.y += point.y;
            return this;
        }
        
        public DoublePoint Add(DoublePoint point)
        {
            this.x += point.x;
            this.y += point.y;
            return this;
        }
        
        public DoublePoint Subtract(ref DoublePoint point)
        {
            this.x -= point.x;
            this.y -= point.y;
            return this;
        }
        
        public DoublePoint Subtract(DoublePoint point)
        {
            this.x -= point.x;
            this.y -= point.y;
            return this;
        }
        
        public DoublePoint Multyply(ref DoublePoint point)
        {
            double x1 = this.x;
            double y1 = this.y;
            this.x = (x1 * point.x) - (y1 * point.y);
            this.y = (x1 * point.y) + (y1 * point.x);
            return this;
        }
        
        public DoublePoint Multyply(DoublePoint point)
        {
            double x1 = this.x;
            double y1 = this.y;
            this.x = (x1 * point.x) - (y1 * point.y);
            this.y = (x1 * point.y) + (y1 * point.x);
            return this;
        }
        
        public DoublePoint Divide(ref DoublePoint point)
        {
            double x1 = this.x;
            double y1 = this.y;
            this.x = ((x1 * point.x) + (y1 * point.y)) / ((point.x * point.x) + (point.y * point.y));
            this.y = ((y1 * point.x) - (x1 * point.y)) / ((point.x * point.x) + (point.y * point.y));
            return this;
        }
        
        public DoublePoint Divide(DoublePoint point)
        {
            double x1 = this.x;
            double y1 = this.y;
            this.x = ((x1 * point.x) + (y1 * point.y)) / ((point.x * point.x) + (point.y * point.y));
            this.y = ((y1 * point.x) - (x1 * point.y)) / ((point.x * point.x) + (point.y * point.y));
            return this;
        }
        /// <summary>
        /// Прибавить
        /// </summary>
        public DoublePoint Add(double value)
        {
            this.x += value;
            this.y += value;
            return this;
        }
        /// <summary>
        /// Отнять
        /// </summary>
        public DoublePoint Subtract(double value)
        {
            this.x -= value;
            this.y -= value;
            return this;
        }
        /// <summary>
        /// Умножить
        /// </summary>
        public DoublePoint Multyply(double value)
        {
            this.x *= value;
            this.y *= value;
            return this;
        }
        /// <summary>
        /// Делить
        /// </summary>
        public DoublePoint Divide(double value)
        {
            this.x /= value;
            this.y /= value;
            return this;
        }
        /// <summary>
        /// Процент
        /// </summary>
        public DoublePoint Mod(double value)
        {
            this.x %= value;
            this.y %= value;
            return this;
        }
        
        public static implicit operator DoublePoint(Point a)
        {
            return new DoublePoint((double) a.X, (double) a.Y);
        }
        
        public static explicit operator Point(DoublePoint p)
        {
            return new Point((int) p.RoundPoint.x, (int) p.RoundPoint.y);
        }

        public static Point operator +(Point p1, DoublePoint p2)
        {
            return new Point(p1.X + ((int) p2.RoundPoint.x), p1.Y + ((int) p2.RoundPoint.y));
        }

        public static Point operator -(Point p1, DoublePoint p2)
        {
            return new Point(p1.X - ((int) p2.RoundPoint.x), p1.Y - ((int) p2.RoundPoint.y));
        }
        
        public static double Distance(DoublePoint left, DoublePoint right)
        {
            return Distance(ref left, ref right);
        }
        
        public static double Distance(ref DoublePoint left, ref DoublePoint right)
        {
            return Math.Sqrt((left.x - right.x) * (left.x - right.x) + (left.y - right.y) * (left.y - right.y));
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
            return string.Format("{0}; {1};", this.x.ToString(), this.y.ToString());
        }
        
        public override bool Equals(object obj)
        {
            return (obj is DoublePoint) && Equals((DoublePoint)obj);
        }
        
        public bool Equals(DoublePoint other)
        {
            return object.Equals(this.x, other.x) && object.Equals(this.y, other.y);
        }
        
        public override int GetHashCode()
        {
            int hashCode = 0;
            unchecked {
                hashCode += 1000000007 * x.GetHashCode();
                hashCode += 1000000009 * y.GetHashCode();
            }
            return hashCode;
        }
        
        public static bool operator ==(DoublePoint lhs, DoublePoint rhs)
        {
            return lhs.Equals(rhs);
        }
        
        public static bool operator !=(DoublePoint lhs, DoublePoint rhs)
        {
            return !(lhs == rhs);
        }

    }
}