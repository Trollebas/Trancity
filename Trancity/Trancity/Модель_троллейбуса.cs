namespace Trancity
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct Модель_троллейбуса
    {
        public string name;
        public string dir;
        public string filename;
        public int количество_хвостов;
        public string[] хвост_filename;
        public double[] хвост_dist_1;
        public double[] хвост_dist_2;
        public string[] сочленение_filename;
        public Дополнение[] дополнения;
        public int количество_дверей;
        public Дверь[] двери;
        public double радиус_колёс;
        public Колёсная_пара[] колёсные_пары;
        public string штанги_dir;
        public string штанги_filename;
        public double штанги_полная_длина;
        public double штанги_угол_min;
        public Штанга[] штанги;
        public Double_3D_Point наряд_pos;
        public Double_Point[] занятые_положения;
        public Double_Point[][] занятые_положения_хвостов;
        public string система_управления;
        [StructLayout(LayoutKind.Sequential)]
        public struct Дверь
        {
            public Модель_дверей модель;
            public int часть;
            public Double_3D_Point p1;
            public Double_3D_Point p2;
            public bool правые;
            public bool дверь_водителя;
            public int номер;
            public Дверь(Модель_дверей модель, int часть, double x1, double z1, double x2, double z2, double y1, double y2, bool правые, bool дверь_водителя, int номер)
            {
                this.модель = модель;
                this.часть = часть;
                this.p1 = new Double_3D_Point(x1, y1, z1);
                this.p2 = new Double_3D_Point(x2, y2, z2);
                this.правые = правые;
                this.дверь_водителя = дверь_водителя;
                this.номер = номер;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Дополнение
        {
            public int часть;
            public string filename;
            public Троллейбус.Обычный_троллейбус.Дополнение.Тип тип;
            public Дополнение(int часть, string filename, Троллейбус.Обычный_троллейбус.Дополнение.Тип тип)
            {
                this.часть = часть;
                this.filename = filename;
                this.тип = тип;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Колёсная_пара
        {
            public string dir;
            public string filename;
            public int часть;
            public Double_Point pos;
            public Колёсная_пара(string dir, string filename, int часть, double x, double y)
            {
                this.dir = dir;
                this.filename = filename;
                this.часть = часть;
                this.pos = new Double_Point(x, y);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Штанга
        {
            public Double_3D_Point pos;
            public Штанга(double x, double y, double z)
            {
                this.pos = new Double_3D_Point(x, y, z);
            }
        }
    }
}

