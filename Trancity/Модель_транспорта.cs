using System.Collections.Generic;
using System.Runtime.InteropServices;
using Engine;

namespace Trancity
{
    [StructLayout(LayoutKind.Sequential)]
    public class ����������������
    {
    	public bool hasnt_bbox = false;
        public List<������������> ������������ = new List<������������>();
        public string name;
        public string dir;
        public string filename;
//        public int �����������������;
        //
        public string[] �����Filename;
        public double[] �����Dist1;
        public double[] �����Dist2;
        public string[] ����������Filename;
        //
        public �����[] tails;
//        public int ��������������������;
        public ����������_new[] ����������;
        //
        public ����������[] ����������;
        public int ����������������;
        public �����[] �����;
        //
        public string axisfilename;
        public string telegafilename;
        public double ����������_�����_�����;
//        public double ����������_�����_���������;
        public double axis_radius;
        public �������[] �������;
        //
        public double ����������;
        public �����������[] �����������;
        public string ������Dir;
        public string ������Filename;
        public double �����������������;
        public double ����������Min;
        public ������[] ������;
        public ���� ����;
        public �� ��;
        //
        public ��������� ���������;
        //
        public Double3DPoint �����Pos;
        //
        public �������� ��������;
        //
        public Double3DPoint[] bbox;
        public Double3DPoint[][] tails_bbox;
        public SphereModel bsphere;
        public SphereModel[] tails_bsphere;
        //
        public Camera[] cameras;
        //
        public DoublePoint[] ����������������;
        public DoublePoint[][] �����������������������;
        public string �����������������;
        [StructLayout(LayoutKind.Sequential)]
        public struct �����
        {
            public ������������ ������;
            public int �����;
            public Double3DPoint p1;
            public Double3DPoint p2;
            public bool ������;
            public bool �������������;
            public int �����;
            public �����(������������ ������, int �����, double x1, double z1, double x2, double z2, double y1, double y2, bool ������, bool �������������, int �����)
            {
                this.������ = ������;
                this.����� = �����;
                p1 = new Double3DPoint(x1, y1, z1);
                p2 = new Double3DPoint(x2, y2, z2);
                this.������ = ������;
                this.������������� = �������������;
                this.����� = �����;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ����������
        {
            public int �����;
            public string filename;
            public Transport.���_���������� ���;
            public ����������(int �����, string filename, Transport.���_���������� ���)
            {
                this.����� = �����;
                this.filename = filename;
                this.��� = ���;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct �����������
        {
            public string dir;
            public string filename;
            public int �����;
            public DoublePoint pos;
            public �����������(string dir, string filename, int �����, double x, double y)
            {
                this.dir = dir;
                this.filename = filename;
                this.����� = �����;
                pos = new DoublePoint(x, y);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ������
        {
            public Double3DPoint pos;
            public ������(double x, double y, double z)
            {
                pos = new Double3DPoint(x, y, z);
            }
        }
		
        [StructLayout(LayoutKind.Sequential)]
        public class ����
        {
            public string dir;
            public string filename;
            public Double3DPoint pos;
            public double angle;
            public ����(string d, string f, double x, double y, double z, double a)
            {
                dir = d;
                filename = f;
                pos = new Double3DPoint(x, y, z);
                angle = a;
            }
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public class ��
        {
            public double ������_�������;
            public double ���������;
            public double ������;
            public ��(double ������_�������, double ���������, double ������)
            {
                this.������_������� = ������_�������;
                this.��������� = ���������;
                this.������ = ������;
            }
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct �����
        {
        	public double dist;
        	public string filename;
//        	public bool have;
//        	public string m_f;
//            public double t_dist;
            public �����(double ds, string filename)//, double t_d)//string middle_f, bool h,
            {
            	this.dist = ds;
                this.filename = filename;
//                this.have = h;
//                this.m_f = middle_f;
//                this.t_dist = t_d;
            }
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct ����������_new
        {
        	public double dist;
        	public string filename;
        	public int index;
        	public int target;
//        	public bool flag;
        	public ����������_new(double ds, string filename, int ind, int att)//, bool flag)
            {
            	this.dist = ds;
                this.filename = filename;
                this.index = ind;
                this.target = att;
//                this.flag = flag;
            }
        }
        
        /*[StructLayout(LayoutKind.Sequential)]
        public struct ���������
        {
        	public _Type type;
            public string dir;
            public string osn_filename;
            public string part_filename;
			public string pant_filename;            
           	public Double3DPoint pos;
            public ���������(_Type _type, string d, string of, string prf, string pnf, double x, double y, double z)
            {
            	type = _type;
                dir = d;
                osn_filename = of;
                part_filename = prf;
                pant_filename = pnf;
                pos = new Double3DPoint(x, y, z);
            }
            
            public enum _Type
            {
            	Pantograph,
            	SemiPantograph
            }
        }*/
        
        [StructLayout(LayoutKind.Sequential)]
        public class ���������
        {
            public string dir;
            public �����_����������[] parts;
           	public Double3DPoint pos;
           	public double dist;
           	public double min_height;
           	public double max_height;
            public ���������(string d, double x, double y, double z, double minh, double maxh, double _dist, �����_����������[] prts)
            {
                dir = d;
                pos = new Double3DPoint(x, y, z);
                dist = _dist;
                min_height = minh;
                max_height = maxh;
                parts = (�����_����������[])prts.Clone();
            }
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public class �����_����������
        {
//        	public int index;//int _index, 
            public string filename;
            public double height;
            public double width;
            public double length;
            public double ang;
            public �����_����������(string _filename, double _height, double _width, double _length, double _ang)
            {
//            	index = _index;
                filename = _filename;
                height = _height;
                width = _width;
                length = _length;
                ang = _ang;
            }
        }
		
        [StructLayout(LayoutKind.Sequential)]
        public class ��������
        {
            public string filename;            
           	public Double3DPoint pos;
            public ��������(string f, double x, double y, double z)
            {
                filename = f;
                pos = new Double3DPoint(x, y, z);
            }
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public class SphereModel
        {
           	public Double3DPoint pos;
           	public double radius;
            public SphereModel(double _radius, double x, double y, double z)
            {
                pos = new Double3DPoint(x, y, z);
                radius = _radius;
            }
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct �������
        {
           	public int index;
           	public double dist;
           	public string filename;
            public �������(int _index, double _dist, string _filename)
            {
                index = _index;
                dist = _dist;
                filename = _filename;
            }
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public class Camera
        {
           	public Double3DPoint pos;
           	public DoublePoint rot;
            public Camera(double x, double y, double z, double rx, double ry)
            {
                pos = new Double3DPoint(x, y, z);
                rot = new DoublePoint(rx, ry);
            }
        }
    }
}