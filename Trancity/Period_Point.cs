/*using System.Runtime.InteropServices;

namespace Trancity
{
    [StructLayout(LayoutKind.Sequential)]
    public struct PeriodPoint
    {
        public DoublePoint point;
        public DoublePoint period;
        public PeriodPoint(DoublePoint point, DoublePoint period)
        {
            this.point = point;
            this.period = period;
        }

        public double x
        {
            get
            {
                return this.point.x;
            }
            set
            {
                this.point.x = value;
            }
        }
        public double y
        {
            get
            {
                return this.point.y;
            }
            set
            {
                this.point.y = value;
            }
        }
        public PeriodPoint int_point
        {
            get
            {
                return new PeriodPoint(this.point.int_point, this.period);
            }
        }
        public static DoublePoint operator -(PeriodPoint a, DoublePoint b)
        {
            double x = a.point.x - b.x;
            if (a.period.x > 0.0)
            {
                while (x < (-a.period.x / 2.0))
                {
                    x += a.period.x;
                }
                while (x > (a.period.x / 2.0))
                {
                    x -= a.period.x;
                }
            }
            double y = a.point.y - b.y;
            if (a.period.y > 0.0)
            {
                while (y < (-a.period.y / 2.0))
                {
                    y += a.period.y;
                }
                while (y > (a.period.y / 2.0))
                {
                    y -= a.period.y;
                }
            }
            return new DoublePoint(x, y);
        }

        public static DoublePoint operator -(DoublePoint a, PeriodPoint b)
        {
            return -(((DoublePoint) b) - a);
        }

        public static DoublePoint operator -(PeriodPoint a, PeriodPoint b)
        {
            return (((DoublePoint) a) - b.point);
        }

        public static PeriodPoint operator +(PeriodPoint a, DoublePoint b)
        {
            return new PeriodPoint(a.point + b, a.period);
        }

        public static bool operator <(PeriodPoint a, DoublePoint b)
        {
            return ((a - b) < DoublePoint.Zero);
        }

        public static bool operator <=(PeriodPoint a, DoublePoint b)
        {
            return ((a - b) <= DoublePoint.Zero);
        }

        public static bool operator >(PeriodPoint a, DoublePoint b)
        {
            return ((a - b) > DoublePoint.Zero);
        }

        public static bool operator >=(PeriodPoint a, DoublePoint b)
        {
            return ((a - b) >= DoublePoint.Zero);
        }

        public static implicit operator DoublePoint(PeriodPoint a)
        {
            return a.point;
        }

        public override string ToString()
        {
            if (this.period != DoublePoint.Zero)
            {
                return (this.point.ToString() + " (period: " + this.period.ToString() + ")");
            }
            return this.point.ToString();
        }
    }
}*/