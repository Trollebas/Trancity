namespace Trancity
{
    using Common;
    using Engine;
    using SlimDX;
    using System;

    public abstract class BaseStop : MeshObject, MeshObject.IFromFile, IMatrixObject, ITest, ITest2
    {
        public TypeOfTransport typeOfTransport = new TypeOfTransport();
        private Road froad = null;
        private Положение position;
        private Double3DPoint point_position;
        private double vector;
        public double distance;
        public bool serviceStop;
        public ObjectModel model = null;
        public string name;

        protected BaseStop(string _name)
        {
            name = _name;
            ObjectLoader.FindModel(1, _name, ref model, ref meshDir);
            if (model != null) base.meshDir = model.dir;
        }

        public Matrix GetMatrix(int index)
        {
            return (last_matrix != MyMatrix.Zero) ? last_matrix : (Matrix.RotationY(-((float)vector)) * Matrix.Translation((float)point_position.x, (float)point_position.y, (float)point_position.z));
        }

        public void ComputeMatrix()
        {
            if (!MainForm.in_editor) last_matrix = GetMatrix(0);
        }

        public void UpdatePosition(World мир)
        {
            if (this.road != null)
            {
                this.position = new Положение(this.road, this.distance, (-this.road.НайтиШирину(this.distance) / 2.0) - 2.4);
                if (this.road is Рельс)
                {
                    foreach (Road дорога in мир.Дороги)
                    {
                        Положение положение = мир.Найти_положение(this.road.НайтиКоординаты(this.distance, 0.0), дорога);
                        if ((положение.Дорога != null) && (положение.отклонение > 0.0))
                        {
                            double num = (-2.4 - положение.отклонение) - (положение.Дорога.НайтиШирину(положение.расстояние) / 2.0);
                            this.position.отклонение = Math.Min(this.position.отклонение, num);
                        }
                    }
                }
                point_position.XZPoint = this.road.НайтиКоординаты(this.distance, this.position.отклонение);
                point_position.y = this.road.НайтиВысоту(this.distance);
                vector = this.road.НайтиНаправление(this.distance);// - MyFeatures.halfPI;
                if (this.bounding_sphere == null) this.bounding_sphere = new Sphere(new Double3DPoint(2.0125, 1.5, -6.9875/*-20.0074, 1.55, 1.4751*/), 21.0);
                this.bounding_sphere.Update(new Double3DPoint(point_position.x, point_position.y, point_position.z), new DoublePoint(vector));
            }
        }

        public virtual string Filename
        {
            get
            {
                return model.filename;//"sign.x";
            }
        }

        public int MatricesCount
        {
            get
            {
                if (MyDirect3D.SphereInFrustum(bounding_sphere) && !this.serviceStop)
                {
                    return 1;
                }
                return 0;
            }
        }

        public Road road
        {
            get
            {
                return this.froad;
            }
            set
            {
                if (this.froad != null)
                {
                    this.froad.objects.Remove(this);
                }
                this.froad = value;
                if (value != null)
                {
                    this.froad.objects.Add(this);
                }
            }
        }

        public void CheckCondition()
        {
            if (MainForm.in_editor) return;
            base.IsNear = ((Math.Abs(Math.Floor(this.point_position.x / (double)Ground.grid_size) - Game.col) < 1.1) && (Math.Abs(Math.Floor(this.point_position.z / (double)Ground.grid_size) - Game.row) < 1.1));
        }
    }
}

