/*
 * Created by SharpDevelop.
 * User: serg
 * Date: 30.12.2013
 * Time: 23:37
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Common;
using SlimDX;
using SlimDX.Direct3D9;
using Engine;

namespace Trancity
{
	public abstract class BaseSignal : MeshObject, MeshObject.IFromFile, IMatrixObject
	{
		public ObjectModel model = null;
        public string name;
        public Положение положение;
        
        public BaseSignal(string _name, byte type)
        {
        	name = _name;
        	ObjectLoader.FindModel(type, _name, ref model, ref meshDir);
        }
		
        public Matrix GetMatrix(int index)
        {
        	var point = this.положение.Координаты;
        	return (Matrix.RotationY(-((float) this.положение.Направление)) * Matrix.Translation((float) point.x, (float)(point.y + this.положение.высота), (float) point.z));
        }
        
        public string Filename
        {
            get
            {
                return model.filename;
            }
        }

        public virtual int MatricesCount
        {
            get
            {
            	if (model == null) return 0;
            	if (MainForm.in_editor)
            	{
                	Double3DPoint point = this.положение.Координаты - MyDirect3D.Camera_Position;
                	if (point.Modulus > 250.0)
                	{
                    	return 0;
                	}
            	}
            	if (bounding_sphere != null)
            	{
            		if (!MyDirect3D.SphereInFrustum(bounding_sphere)) return 0;
            	}
                return 1;
            }
        }
        
		public void CreateBoundingSphere()
        {
			if (model == null) return;
			base.bounding_sphere = new Sphere(model.bsphere.pos, model.bsphere.radius);
			var pos = this.положение.Координаты;
			pos.y += this.положение.высота;
			base.bounding_sphere.Update(pos, new DoublePoint(this.положение.Направление));
        }
	}
}
