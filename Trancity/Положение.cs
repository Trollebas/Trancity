using Engine;
using System.Runtime.InteropServices;

namespace Trancity
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ���������
    {
        private IObjectContainer _container;
        public double ����������;
        public double ����������;
        public double ������;
        public object comment;
        public ���������(IObjectContainer container, double ����������) : this(container, ����������, 0.0, 0.0)
        {
        }

        public ���������(IObjectContainer container, double ����������, double ����������) : this(container, ����������, ����������, 0.0)
        {
        }

        public ���������(IObjectContainer container, double ����������, double ����������, double ������)
        {
            _container = container;
            this.���������� = ����������;
            this.���������� = ����������;
            this.������ = ������;
            comment = null;
        }

        public Road ������
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
        public ����� �����
        {
            get
            {
                if ((_container != null) && (_container is �����))
                {
                    return (�����)_container;
                }
                return null;
            }
        }
        public Double3DPoint ����������
        {
            get
            {
                if (������ != null)
                {
                    var point = ������.���������������(����������, ����������);
                    return new Double3DPoint(point.x, ������.�����������(����������), point.y);
                }
                return Double3DPoint.Zero;
            }
        }
        public double �����������
        {
            get
            {
                return ������ != null ? ������.����������������(����������) : 0.0;
            }
        }
    }
}