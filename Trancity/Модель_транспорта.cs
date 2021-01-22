using System.Collections.Generic;
using System.Runtime.InteropServices;
using Engine;

namespace Trancity
{
    [StructLayout(LayoutKind.Sequential)]
    public class МодельТранспорта
    {
    	public bool hasnt_bbox = false;
        public List<МодельДверей> модельДверей = new List<МодельДверей>();
        public string name;
        public string dir;
        public string filename;
//        public int количествоХвостов;
        //
        public string[] хвостFilename;
        public double[] хвостDist1;
        public double[] хвостDist2;
        public string[] сочленениеFilename;
        //
        public Хвост[] tails;
//        public int количествоСочленений;
        public Сочленение_new[] сочленения;
        //
        public Дополнение[] дополнения;
        public int количествоДверей;
        public Дверь[] двери;
        //
        public string axisfilename;
        public string telegafilename;
        public double расстояние_между_осями;
//        public double расстояние_между_тележками;
        public double axis_radius;
        public Тележка[] тележки;
        //
        public double радиусКолёс;
        public КолёснаяПара[] колёсныеПары;
        public string штангиDir;
        public string штангиFilename;
        public double штангиПолнаяДлина;
        public double штангиУголMin;
        public Штанга[] штанги;
        public Руль руль;
        public АХ ах;
        //
        public Пантограф пантограф;
        //
        public Double3DPoint нарядPos;
        //
        public Табличка табличка;
        //
        public Double3DPoint[] bbox;
        public Double3DPoint[][] tails_bbox;
        public SphereModel bsphere;
        public SphereModel[] tails_bsphere;
        //
        public Camera[] cameras;
        //
        public DoublePoint[] занятыеПоложения;
        public DoublePoint[][] занятыеПоложенияХвостов;
        public string системаУправления;
        [StructLayout(LayoutKind.Sequential)]
        public struct Дверь
        {
            public МодельДверей модель;
            public int часть;
            public Double3DPoint p1;
            public Double3DPoint p2;
            public bool правые;
            public bool дверьВодителя;
            public int номер;
            public Дверь(МодельДверей модель, int часть, double x1, double z1, double x2, double z2, double y1, double y2, bool правые, bool дверьВодителя, int номер)
            {
                this.модель = модель;
                this.часть = часть;
                p1 = new Double3DPoint(x1, y1, z1);
                p2 = new Double3DPoint(x2, y2, z2);
                this.правые = правые;
                this.дверьВодителя = дверьВодителя;
                this.номер = номер;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Дополнение
        {
            public int часть;
            public string filename;
            public Transport.Тип_дополнения тип;
            public Дополнение(int часть, string filename, Transport.Тип_дополнения тип)
            {
                this.часть = часть;
                this.filename = filename;
                this.тип = тип;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct КолёснаяПара
        {
            public string dir;
            public string filename;
            public int часть;
            public DoublePoint pos;
            public КолёснаяПара(string dir, string filename, int часть, double x, double y)
            {
                this.dir = dir;
                this.filename = filename;
                this.часть = часть;
                pos = new DoublePoint(x, y);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Штанга
        {
            public Double3DPoint pos;
            public Штанга(double x, double y, double z)
            {
                pos = new Double3DPoint(x, y, z);
            }
        }
		
        [StructLayout(LayoutKind.Sequential)]
        public class Руль
        {
            public string dir;
            public string filename;
            public Double3DPoint pos;
            public double angle;
            public Руль(string d, string f, double x, double y, double z, double a)
            {
                dir = d;
                filename = f;
                pos = new Double3DPoint(x, y, z);
                angle = a;
            }
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public class АХ
        {
            public double полная_ёмкость;
            public double ускорение;
            public double расход;
            public АХ(double полная_ёмкость, double ускорение, double расход)
            {
                this.полная_ёмкость = полная_ёмкость;
                this.ускорение = ускорение;
                this.расход = расход;
            }
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct Хвост
        {
        	public double dist;
        	public string filename;
//        	public bool have;
//        	public string m_f;
//            public double t_dist;
            public Хвост(double ds, string filename)//, double t_d)//string middle_f, bool h,
            {
            	this.dist = ds;
                this.filename = filename;
//                this.have = h;
//                this.m_f = middle_f;
//                this.t_dist = t_d;
            }
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct Сочленение_new
        {
        	public double dist;
        	public string filename;
        	public int index;
        	public int target;
//        	public bool flag;
        	public Сочленение_new(double ds, string filename, int ind, int att)//, bool flag)
            {
            	this.dist = ds;
                this.filename = filename;
                this.index = ind;
                this.target = att;
//                this.flag = flag;
            }
        }
        
        /*[StructLayout(LayoutKind.Sequential)]
        public struct Пантограф
        {
        	public _Type type;
            public string dir;
            public string osn_filename;
            public string part_filename;
			public string pant_filename;            
           	public Double3DPoint pos;
            public Пантограф(_Type _type, string d, string of, string prf, string pnf, double x, double y, double z)
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
        public class Пантограф
        {
            public string dir;
            public Часть_пантографа[] parts;
           	public Double3DPoint pos;
           	public double dist;
           	public double min_height;
           	public double max_height;
            public Пантограф(string d, double x, double y, double z, double minh, double maxh, double _dist, Часть_пантографа[] prts)
            {
                dir = d;
                pos = new Double3DPoint(x, y, z);
                dist = _dist;
                min_height = minh;
                max_height = maxh;
                parts = (Часть_пантографа[])prts.Clone();
            }
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public class Часть_пантографа
        {
//        	public int index;//int _index, 
            public string filename;
            public double height;
            public double width;
            public double length;
            public double ang;
            public Часть_пантографа(string _filename, double _height, double _width, double _length, double _ang)
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
        public class Табличка
        {
            public string filename;            
           	public Double3DPoint pos;
            public Табличка(string f, double x, double y, double z)
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
        public struct Тележка
        {
           	public int index;
           	public double dist;
           	public string filename;
            public Тележка(int _index, double _dist, string _filename)
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