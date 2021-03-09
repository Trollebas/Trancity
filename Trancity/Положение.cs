using Engine;
using System.Runtime.InteropServices;

namespace Trancity
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Положение
    {
        private IObjectContainer _container;
        public double расстояние;
        public double отклонение;
        public double высота;
        public object comment;
        public Положение(IObjectContainer container, double расстояние) : this(container, расстояние, 0.0, 0.0)
        {
        }

        public Положение(IObjectContainer container, double расстояние, double отклонение) : this(container, расстояние, отклонение, 0.0)
        {
        }

        public Положение(IObjectContainer container, double расстояние, double отклонение, double высота)
        {
            _container = container;
            this.расстояние = расстояние;
            this.отклонение = отклонение;
            this.высота = высота;
            comment = null;
        }

        public Road Дорога
        {
            get
            {
                if ((_container != null) && (_container is Road))
                {
                    return (Road)_container;
                }
                return null;
            }
            set
            {
                _container = value;
            }
        }
        public Рельс Рельс
        {
            get
            {
                if ((_container != null) && (_container is Рельс))
                {
                    return (Рельс)_container;
                }
                return null;
            }
        }
        public Double3DPoint Координаты
        {
            get
            {
                if (Дорога != null)
                {
                    var point = Дорога.НайтиКоординаты(расстояние, отклонение);
                    return new Double3DPoint(point.x, Дорога.НайтиВысоту(расстояние), point.y);
                }
                return Double3DPoint.Zero;
            }
        }
        public double Направление
        {
            get
            {
                return Дорога != null ? Дорога.НайтиНаправление(расстояние) : 0.0;
            }
        }
    }
}