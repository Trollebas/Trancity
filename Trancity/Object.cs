using Common;
using Engine;
using SlimDX;
using System;

namespace Trancity
{
    public class Îáúåêò : MeshObject, IMatrixObject, MeshObject.IFromFile, IVector, ITest, ITest2
    {
        public string filename;
        public double x0;
        public double y0;
        public double angle0;
        public double height0;
        public ObjectModel model = null;
        private int col = 0;
        private int row = 0;

        public Îáúåêò(string filename, double x0, double y0, double angle0, double height0)
        {
            this.filename = filename;

            this.x0 = x0;
            this.y0 = y0;
            this.angle0 = angle0;
            this.height0 = height0;
        }

        public override void CreateMesh()
        {
            ObjectLoader.FindModel(0, filename, ref model, ref meshDir);
            if (model == null)
                return;
            ComputeMatrix();
            CheckCondition();
            base.CreateMesh();
            base.bounding_sphere = new Sphere(model.bsphere.pos, model.bsphere.radius);
            base.bounding_sphere.Update(Position3D, direction2);
        }

        private void OnPositionUpdate()
        {
            this.bounding_sphere.Update(this.Position3D, this.direction2);
            this.ComputeMatrix();
        }

        #region IFromFile implementation

        public string Filename
        {
            get
            {
                return model.filename;
            }
        }

        #endregion

        #region IMatrixObject implementation

        public Matrix GetMatrix(int index)
        {
            return (last_matrix != MyMatrix.Zero) ? last_matrix : (Matrix.RotationY(-((float)angle0)) * Matrix.Translation((float)x0, (float)height0, (float)y0));
        }

        public int MatricesCount
        {
            get
            {
                if ((Math.Abs(col - Game.col) > 1) || (Math.Abs(row - Game.row) > 1))
                    return 0;
                if (!MyDirect3D.SphereInFrustum(bounding_sphere))
                    return 0;
                return 1;
            }
        }

        #endregion

        public void ComputeMatrix()
        {
            if (!MainForm.in_editor)
                last_matrix = GetMatrix(0);
            col = (int)Math.Floor(x0 / (double)Ground.grid_size);
            row = (int)Math.Floor(y0 / (double)Ground.grid_size);
        }

        public DoublePoint position
        {
            get
            {
                return new DoublePoint(this.x0, this.y0);
            }
            set
            {
                this.x0 = value.x;
                this.y0 = value.y;
                this.OnPositionUpdate();
            }
        }

        public Double3DPoint Position3D
        {
            get
            {
                return new Double3DPoint(this.x0, this.height0, this.y0);
            }
            set
            {
                this.x0 = value.x;
                this.y0 = value.z;
                this.height0 = value.y;
                this.OnPositionUpdate();
            }
        }

        public DoublePoint direction2
        {
            get
            {
                return new DoublePoint(angle0);
            }
        }

        public double direction
        {
            get
            {
                return this.angle0;
            }
        }

        public void CheckCondition()
        {
            if (MainForm.in_editor)
                return;
            base.IsNear = ((Math.Abs(col - Game.col) < 1.1) && (Math.Abs(row - Game.row) < 1.1));
        }
    }
}