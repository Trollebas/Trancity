using Common;
using SlimDX;
using System;
using System.IO;

namespace Trancity
{
    public class ќбъект : MeshObject, IMatrixObject, MeshObject.IFromFile, IVector, ITest
    {
        public string filename;
        public double x0;
        public double y0;
        public double angle0;
        public double height0;
        public ObjectModel model = null;
        private int col = 0;
        private int row = 0;

        public ќбъект(string filename, double x0, double y0, double angle0, double height0)
        {
            this.filename = filename;
            
            this.x0 = x0;
            this.y0 = y0;
            this.angle0 = angle0;
			this.height0 = height0;
            while (angle0 > Math.PI)
            {
                angle0 -= Math.PI * 2;
            }
            while (angle0 <= -Math.PI)
            {
                angle0 += Math.PI * 2;
            }
            col = (int)Math.Floor(x0 / (double)Ground.grid_size);
            row = (int)Math.Floor(y0 / (double)Ground.grid_size);
//            base.meshDir = System.Environment.CurrentDirectory + "\\Objects\\"+filename+"\\";
        }
        
		public override void CreateMesh()
		{
			/*foreach (var _model in ObjectLoader.objects[0])
			{
				if (_model.name == filename)
				{
					model = _model;
					break;
				}
			}
			if (model == null)
			{
				Logging.Write("ObjectLoader", "Object " + filename + " not found!");
				return;
			}
			base.meshDir = model.dir;*/
			ObjectLoader.FindModel(0, filename, ref model, ref meshDir);
			if (model == null) return;
			base.CreateMesh();
			base.bounding_sphere = new Sphere(model.bsphere.pos, model.bsphere.radius);
			base.bounding_sphere.Update(Position3D, direction2);
			ComputeMatrix();
		}

        #region „лены IFromFile

//        string MeshObject.IFromFile.Filename
		public string Filename
        {
            get 
            {
//                meshDir = System.Environment.CurrentDirectory + "\\Objects\\"+filename+"\\";
//                return filename+".x";
				return model.filename;
            }
        }

        #endregion

        #region „лены IMatrixObject

//        Matrix IMatrixObject.GetMatrix(int index)
		public Matrix GetMatrix(int index)
        {
//            var point = new Double3DPoint(x0,y0,angle0);
//            var point2 = new DoublePoint(point.x, point.y);
//            return (Matrix.RotationY(-((float)point.z)) * Matrix.Translation((float)point2.x, (float)height0, (float)point2.y));
            return (last_matrix != MyMatrix.Zero) ? last_matrix : (Matrix.RotationY(-((float)angle0)) * Matrix.Translation((float)x0, (float)height0, (float)y0));
        }

//        int IMatrixObject.MatricesCount
		public int MatricesCount
        {
            get
            {
            	if ((Math.Abs(col - Game.col) > 1) || (Math.Abs(row - Game.row) > 1)) return 0;
	            if (!MyDirect3D.SphereInFrustum(bounding_sphere)) return 0;
	            /*else if (bounding_box != null)
	            {
	            	if (!MyDirect3D.AABBInFrustum(bounding_box)) return 0;
	            }
	            else if (World.deleteObj)
            	{
	            	var point = new DoublePoint(this.x0 - MyDirect3D.Camera_Position.x, this.y0 - MyDirect3D.Camera_Position.z);
	            	if (point.модуль > 300)
	            	{
	                	return 0;
	            	}
            	}*/
            	return 1;
            }
        }
        #endregion
        
        public void ComputeMatrix()
        {
        	if (!MainForm.in_editor) last_matrix = GetMatrix(0);
        }
        
        public DoublePoint position
        {
        	get
        	{
        		return new DoublePoint(this.x0, this.y0);
        	}
        }
        
        public Double3DPoint Position3D
        {
        	get
        	{
        		return new Double3DPoint(this.x0, this.height0, this.y0);
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
    }
}