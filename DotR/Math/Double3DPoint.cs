using System;
using System.Runtime.InteropServices;

namespace Engine
{
    /// <summary>
    /// Double3DPoint - another Trancity' dinosaur, and that's prooved by name.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Double3DPoint : IEquatable<Double3DPoint>
    {
        public double x;
        public double y;
        public double z;
        
        public static readonly Double3DPoint Zero = new Double3DPoint(0.0, 0.0, 0.0);
        
        // TODO: rename function as far as understand wat it exactly does
        public static Double3DPoint Rotate(DoublePoint baseAngel, double planeAngel)
        {
            return Rotate(ref baseAngel, planeAngel);
        }
        
        public static Double3DPoint Rotate(ref DoublePoint baseAngel, double planeAngel)
        {
            // ещё вроде как соптимизировал
            if (Math.Abs(Math.Sin(baseAngel.y)) < 1E-5)//(point.y == 0.0)
            {
                return new Double3DPoint(baseAngel.x + planeAngel, baseAngel.y);
            }
            var point = new Double3DPoint(0.0, baseAngel.y);
            var point2 = new DoublePoint(planeAngel);
            point.y *= point2.x;
            point.AngleX = baseAngel.x;
            var addit_ang = new DoublePoint(point.XZPoint.Angle + (Math.PI / 2.0)).Multyply(point.XZPoint.Modulus).Multyply(point2.y);
            point.XZPoint = point.XZPoint.Multyply(point2.x).Add(ref addit_ang);
            return point;
        }
        
        public static Double3DPoint Multiply(Double3DPoint _point, Double3DPoint body, DoublePoint direction)
        {
            Double3DPoint point = new Double3DPoint(ref direction).Multyply(_point.x);
            Double3DPoint point1 = new Double3DPoint(ref direction);
            Double3DPoint point2 = Double3DPoint.Rotate(ref direction, (Math.PI / 2.0)).Multyply(_point.z);
            point1.AngleY += (Math.PI / 2.0);
            point1.Multyply(_point.y);
            point2.Add(ref point1);
            point.Add(ref point2);
            return body.Add(ref point);
        }

        public Double3DPoint(double x, double y, double z)
        {
            if (double.IsInfinity(x) || double.IsNaN(x))
            {
                throw new Exception("X-координата точки является недействительным.");
            }
            if (double.IsInfinity(y) || double.IsNaN(y))
            {
                throw new Exception("Y-координата точки является недействительным.");
            }
            if (double.IsInfinity(z) || double.IsNaN(z))
            {
                throw new Exception("Z-координата точки является недействительным.");
            }
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Double3DPoint(double angleX, double angleY)
        {
            this.x = Math.Cos(angleX) * Math.Cos(angleY);
            this.z = Math.Sin(angleX) * Math.Cos(angleY);
            this.y = Math.Sin(angleY);
        }

        public Double3DPoint(DoublePoint angle) : this(angle.x, angle.y)
        {
        }
        
        public Double3DPoint(ref DoublePoint angle) : this(angle.x, angle.y)
        {
        }

        public DoublePoint XZPoint
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
        
        public DoublePoint XYPoint
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
        
        public DoublePoint YPoint
        {
            get
            {
//                return new DoublePoint(this.XZPoint.Modulus, this.y);
                return new DoublePoint(Math.Sqrt(x * x + z * z), this.y);
            }
            set
            {
                this.XZPoint = new DoublePoint(this.XZPoint.Angle).Multyply(value.x);
                this.y = value.y;
            }
        }
        
        public double Modulus
        {
            get
            {
                return Math.Sqrt(((this.x * this.x) + (this.y * this.y)) + (this.z * this.z));
            }
            set
            {
                double num = value / this.Modulus;
                this.Multyply(num);
            }
        }
        
        public DoublePoint Angle
        {
            get
            {
//                return new DoublePoint(this.XZPoint.Angle, this.YPoint.Angle);
                return new DoublePoint(AngleX, AngleY);
            }
            set
            {
                this = new Double3DPoint(value).Multyply(this.Modulus);
            }
        }
        
        public double AngleX
        {
            get
            {
//                return this.Angle.x;
                return this.XZPoint.Angle;
            }
            set
            {
//                this.Angle = new DoublePoint(value, this.Angle.y);
                this.Angle = new DoublePoint(value, this.AngleY);
            }
        }
        
        public double AngleY
        {
            get
            {
//                return this.Angle.y;
                return this.YPoint.Angle;
            }
            set
            {
//                this.Angle = new DoublePoint(this.Angle.x, value);
                this.Angle = new DoublePoint(this.AngleX, value);
            }
        }
        
        public void CopyTo(ref Double3DPoint point)
        {
            point.x = this.x;
            point.y = this.y;
            point.z = this.z;
        }
        
        public void CopyFromAngle(double angleX, double angleY)
        {
            this.x = Math.Cos(angleX) * Math.Cos(angleY);
            this.z = Math.Sin(angleX) * Math.Cos(angleY);
            this.y = Math.Sin(angleY);
        }
        
        public void CopyFromAngle(DoublePoint angle)
        {
            CopyFromAngle(angle.x, angle.y);
        }
        
        public Double3DPoint Add(ref Double3DPoint point)
        {
            this.x += point.x;
            this.y += point.y;
            this.z += point.z;
            return this;
        }
        
        public Double3DPoint Subtract(ref Double3DPoint point)
        {
            this.x -= point.x;
            this.y -= point.y;
            this.z -= point.z;
            return this;
        }
        
        public Double3DPoint Add(Double3DPoint point)
        {
            this.x += point.x;
            this.y += point.y;
            this.z += point.z;
            return this;
        }
        
        public Double3DPoint Subtract(Double3DPoint point)
        {
            this.x -= point.x;
            this.y -= point.y;
            this.z -= point.z;
            return this;
        }
        
        public Double3DPoint Add(double value)
        {
            this.x += value;
            this.y += value;
            this.z += value;
            return this;
        }
        
        public Double3DPoint Subtract(double value)
        {
            this.x -= value;
            this.y -= value;
            this.z -= value;
            return this;
        }
        
        public Double3DPoint Multyply(double value)
        {
            this.x *= value;
            this.y *= value;
            this.z *= value;
            return this;
        }
        
        public Double3DPoint Divide(double value)
        {
            this.x /= value;
            this.y /= value;
            this.z /= value;
            return this;
        }
        
        public Double3DPoint Mod(double value)
        {
            this.x %= value;
            this.y %= value;
            this.z %= value;
            return this;
        }
        
        public static double Distance(Double3DPoint left, Double3DPoint right)
        {
            return Distance(ref left, ref right);
        }
        
        public static double Distance(ref Double3DPoint left, ref Double3DPoint right)
        {
            return Math.Sqrt((left.x - right.x) * (left.x - right.x) + (left.y - right.y) * (left.y - right.y) + (left.z - right.z) * (left.z - right.z));
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
        
        public override string ToString()
        {
            return string.Format("{0}; {1}; {2};", this.x.ToString(), this.y.ToString(), this.z.ToString());
        }
        
        #region Equals and GetHashCode implementation
        public override bool Equals(object obj)
        {
            return (obj is Double3DPoint) && Equals((Double3DPoint)obj);
        }
        
        public bool Equals(Double3DPoint other)
        {
            return object.Equals(this.x, other.x) && object.Equals(this.y, other.y) && object.Equals(this.z, other.z);
        }
        
        public override int GetHashCode()
        {
            int hashCode = 0;
            unchecked {
                hashCode += 1000000007 * x.GetHashCode();
                hashCode += 1000000009 * y.GetHashCode();
                hashCode += 1000000021 * z.GetHashCode();
            }
            return hashCode;
        }
        
        public static bool operator ==(Double3DPoint lhs, Double3DPoint rhs)
        {
            return lhs.Equals(rhs);
        }
        
        public static bool operator !=(Double3DPoint lhs, Double3DPoint rhs)
        {
            return !(lhs == rhs);
        }
        #endregion

    }
}