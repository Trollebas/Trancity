namespace Trancity
{
    using Common;
    using Microsoft.DirectX;
    using System;
    using System.Drawing;

    public abstract class Двери
    {
        public Double_3D_Point pos1;
        public Double_3D_Point pos2;
        protected double высота = 2.557;
        public bool дверь_водителя;
        protected double длина = 0.425;
        public int номер;
        public IMatrixObject объект;
        public bool открываются;
        public bool правые;
        private double состояние;
        protected Дверь створка;
        protected double ширина = 0.05;

        protected Двери()
        {
        }

        public void CreateMesh()
        {
            this.створка.CreateMesh();
        }

        public abstract void Render();
        public void обновить()
        {
            this.состояние += this.скорость * Мир.прошло_времени;
            if (this.состояние < 0.0)
            {
                this.состояние = 0.0;
            }
            if (this.состояние > 1.0)
            {
                this.состояние = 1.0;
            }
        }

        public static Двери Построить(Модель_дверей модель, IMatrixObject объект, Double_3D_Point p1, Double_3D_Point p2, bool правые)
        {
            switch (модель.тип)
            {
                case Модель_дверей.Тип.Двустворчатые:
                    return new Двустворчатые(объект, p1.x, p1.z, p2.x, p2.z, p1.y, p2.y, правые, модель.dir, модель.filename, модель.длина, модель.высота, модель.ширина);

                case Модель_дверей.Тип.Шарнирно_поворотные:
                    return new Шарнирно_поворотные(объект, p1.x, p1.z, p2.x, p2.z, p1.y, p2.y, правые, модель.dir, модель.filename, модель.длина, модель.высота, модель.ширина);
            }
            throw new ArgumentOutOfRangeException("модель.тип", модель.тип, "Неизвестный тип дверей!");
        }

        public virtual string[] extra_mesh_dirs
        {
            get
            {
                return this.створка.extra_mesh_dirs;
            }
            set
            {
                this.створка.extra_mesh_dirs = value;
            }
        }

        public bool закрыты
        {
            get
            {
                return (this.состояние <= 0.0);
            }
        }

        public bool открыты
        {
            get
            {
                return (this.состояние >= 1.0);
            }
        }

        protected virtual double скорость
        {
            get
            {
                if (this.открываются)
                {
                    return 0.8;
                }
                return -0.8;
            }
        }

        protected class Дверь : Mesh_Object, Mesh_Object.IFromFile, IMatrixObject
        {
            private string fname;
            public Matrix matrix;

            public Дверь(string filename)
            {
                this.fname = filename;
            }

            public Дверь(string filename, string dir)
            {
                base.mesh_dir = dir;
                this.fname = filename;
            }

            public Matrix get_matrix(int index)
            {
                return this.matrix;
            }

            public string filename
            {
                get
                {
                    return this.fname;
                }
                set
                {
                    this.fname = value;
                }
            }

            public int matrices_count
            {
                get
                {
                    return 1;
                }
            }
        }

        public class Двустворчатые : Двери
        {
            public Двустворчатые(IMatrixObject объект, double x1, double z1, double x2, double z2, double y1, double y2, bool правые, string dir, string filename, double длина, double высота, double ширина)
            {
                base.pos1 = new Double_3D_Point(x1, y1, z1);
                base.pos2 = new Double_3D_Point(x2, y2, z2);
                base.правые = правые;
                base.длина = длина;
                base.высота = высота;
                base.ширина = ширина;
                base.объект = объект;
                base.створка = new Двери.Дверь(filename, dir);
            }

            public override void Render()
            {
                Matrix matrix = base.объект.get_matrix(0);
                float num = base.правые ? ((float) (-1)) : ((float) 1);
                double d = (base.состояние * 3.1415926535897931) / 2.0;
                Matrix matrix2 = (Matrix.Translation(((float) base.длина) / 2f, 0f, (num * -((float) base.ширина)) / 2f) * Matrix.RotationY(num * -((float) d))) * Matrix.Translation(0f, 0f, num * ((float) (Math.Cos(d) * base.ширина)));
                Matrix matrix3 = ((Matrix.Translation(((float) base.длина) / 2f, 0f, (num * -((float) base.ширина)) / 2f) * Matrix.RotationY(num * -((float) (3.1415926535897931 - d)))) * Matrix.Translation(-((float) (Math.Sin(d) * base.ширина)), 0f, 0f)) * Matrix.Translation(((float) ((Math.Sin(d) * base.ширина) + (Math.Cos(d) * base.длина))) * 2f, 0f, 0f);
                Double_Point point = new Double_Point(this.pos2.x - this.pos1.x, this.pos2.z - this.pos1.z);
                Matrix matrix4 = (Matrix.Scaling((float) ((point.модуль / 2.0) / base.длина), (float) ((this.pos2.y - this.pos1.y) / base.высота), (float) ((point.модуль / 2.0) / base.длина)) * Matrix.RotationY(-((float) point.угол))) * Matrix.Translation((float) this.pos1.x, (float) this.pos1.y, (float) this.pos1.z);
                if (base.правые)
                {
                    matrix2 = Matrix.RotationY(3.141593f) * matrix2;
                }
                else
                {
                    matrix3 = Matrix.RotationY(3.141593f) * matrix3;
                }
                base.створка.matrix = (matrix2 * matrix4) * matrix;
                base.створка.Render();
                base.створка.matrix = (matrix3 * matrix4) * matrix;
                base.створка.Render();
            }
        }

        public class Шарнирно_поворотные : Двери
        {
            public Шарнирно_поворотные(IMatrixObject объект, double x1, double z1, double x2, double z2, double y1, double y2, bool правые, string dir, string filename, double длина, double высота, double ширина)
            {
                base.pos1 = new Double_3D_Point(x1, y1, z1);
                base.pos2 = new Double_3D_Point(x2, y2, z2);
                base.правые = правые;
                base.длина = длина;
                base.высота = высота;
                base.ширина = ширина;
                base.объект = объект;
                base.створка = new Двери.Дверь(filename, dir);
            }

            public override void Render()
            {
                Matrix matrix = base.объект.get_matrix(0);
                float num = base.правые ? ((float) (-1)) : ((float) 1);
                double d = (base.состояние * 3.1415926535897931) / 2.0;
                Matrix matrix2 = (Matrix.Translation(((float) base.длина) / 2f, 0f, (num * -((float) base.ширина)) / 2f) * Matrix.RotationY(num * -((float) (3.1415926535897931 - d)))) * Matrix.Translation((float) (Math.Cos(d) * base.длина), 0f, 0f);
                Double_Point point = new Double_Point(this.pos2.x - this.pos1.x, this.pos2.z - this.pos1.z);
                Matrix matrix3 = (Matrix.Scaling((float) (point.модуль / base.длина), (float) ((this.pos2.y - this.pos1.y) / base.высота), (float) (point.модуль / base.длина)) * Matrix.RotationY(-((float) point.угол))) * Matrix.Translation((float) this.pos1.x, (float) this.pos1.y, (float) this.pos1.z);
                if (!base.правые)
                {
                    matrix2 = Matrix.RotationY(3.141593f) * matrix2;
                }
                base.створка.matrix = (matrix2 * matrix3) * matrix;
                base.створка.Render();
            }
        }
    }
}

