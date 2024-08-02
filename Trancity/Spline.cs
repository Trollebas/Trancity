/*
 * Created by SharpDevelop.
 * User: serg
 * Date: 12.08.2012
 * Time: 21:59
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using SlimDX;
using SlimDX.Direct3D9;
using Common;

namespace Trancity
{
	public abstract class Spline : MeshObject, MeshObject.IFromFile, MeshObject.ICustomCreation
	{
		public SplineModel model = null;
		protected MeshVertex[] vertexes = null;
		public string name;
		
		public override void CreateMesh()
		{
			if (string.IsNullOrEmpty(name))
			{
				name = (this is Рельс) ? "Rails" : "Road";
			}
			if (model == null)
			{
				foreach (var spline in SplineLoader.splines)
				{
					if (spline.name == name)//(((this is Рельс) && (spline.name == "Rails")) || ((this is Road) && (spline.name == "Road")))
					{
						model = spline;
						break;
					}
				}
				if (model == null)
				{
					Logging.Write("SplineLoader", "Spline " + name + " not found!");
					return;
				}
			}
			base.meshDir = model.dir;
			if (MainForm.in_editor)
			{
				base.CreateMesh();
				return;
			}
			CreateCustomMesh();
		}
		
		public abstract void CreateCustomMesh();
		
		public virtual string Filename
		{
			get
			{
				return model.mesh_filename;
			}
		}
		
		public virtual Matrix GetMatrix(int index)
		{
			return MyMatrix.Zero;
		}
		
		public virtual int MatricesCount
		{
			get
			{
				return 0;
			}
		}
		
		public virtual void CustomRender()
		{
			if ((!MyDirect3D.Alpha) || (model == null)) return;
			/*using (VertexDeclaration decl = new VertexDeclaration(MyDirect3D.device, MeshVertex.Format))
			{
				MyDirect3D.device.VertexDeclaration = decl;*/
				MyDirect3D.device.SetTransform(TransformState.World, ((IMatrixObject)this).GetMatrix(0));
				MyDirect3D.device.Material = _meshMaterials[0];
				MyDirect3D.device.SetTexture(0, _meshTextures[0]);/**/
//						MyDirect3D.test_effect.SetTexture("texture0", _meshTextures[0]);
//						MyDirect3D.test_effect.SetValue("hasTexture", true);
//						MyDirect3D.test_effect.SetValue("intencity", MyDirect3D.light_intency);
//						MyDirect3D.test_effect.SetValue("worldViewProjection", ((IMatrixObject)this).GetMatrix(0) * MyDirect3D.device.GetTransform(TransformState.Projection));
//						MyDirect3D.test_effect.Begin(0);
//						MyDirect3D.test_effect.BeginPass(0);
						MyDirect3D.device.DrawUserPrimitives<MeshVertex>(PrimitiveType.TriangleStrip/*.TriangleList*/, vertexes.Length - 2, vertexes);
//						MyDirect3D.test_effect.EndPass();
//                		MyDirect3D.test_effect.End();
						base.AttachToManager(_meshMaterials[0], _meshTextures[0]);
						if ((_addTexList[0].loaded) && ((_meshTextures[0] == null) || (_meshTextures[0].Disposed)))
                        {
                        	var str = _textureFileStructs[_addTexList[0].arpos];
                            if ((str.texture == null) || (str.texture.Disposed)) str.needed = true;
                            else _meshTextures[0] = str.texture;
                            _textureFileStructs[_addTexList[0].arpos]= str;
                        }/**/
//			}
		}
	}
}
