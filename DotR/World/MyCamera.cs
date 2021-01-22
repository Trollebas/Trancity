/*
 * Created by SharpDevelop.
 * User: serg
 * Date: 25.07.2013
 * Time: 22:50
 * 
 * This file was originally created for Trancity project.
 * 
 */
#if false
using System;
using SlimDX;

namespace Engine
{
	/// <summary>
	/// 3D camera class
	/// </summary>
    public class MyCamera
    {
    	private Double3DPoint position;
    	private DoublePoint rotation;
    	private float fov;
		private MyCameraType type;
		
		/// <summary>
		/// Create custom-typed camera
		/// </summary>
		/// <param name="type">Camera type</param>
		public MyCamera(MyCameraType type)
		{
			this.type = type;
			fov = 1.0f;
		}
		
		/// <summary>
		/// Create default (perspective) camera
		/// </summary>
		public MyCamera() : this(MyCameraType.Perspective)
		{
		}
		
		/// <summary>
		/// Returns view matrix
		/// </summary>
		public Matrix ViewMatrix
		{
			// TODO: make platform-independet
			get
			{
				switch (type)
				{
					case MyCameraType.Top:
						return /*Matrix.Translation(-((float)position.x), -((float)position.y), -((float)position.z))
								* Matrix.RotationX((float)(-MyFeatures.halfPI));*/
								Matrix.LookAtLH(Vector3.Zero/*(Vector3)(position)*/,
							                new Vector3((float)position.x, 0f, (float)position.z),//-Vector3.UnitY,
							                Vector3.UnitY);
					// TODO: тут намудренно, надо проверять
					case MyCameraType.Perspective:
//						double top_ang = rotation.y + MyFeatures.halfPI;
						Vector3 target = new Vector3((float)(position.x + Math.Cos(rotation.x)), (float)(position.y + Math.Cos(rotation.y)), (float)(position.z + Math.Sin(rotation.x)));
//						Vector3 top = new Vector3(target.X * (float)Math.Cos(top_ang), (float)Math.Sin(top_ang), target.Z * (float)Math.Cos(top_ang));
//						target.Normalize();
//						top.Normalize();
						return /*Matrix.Translation(-((float)position.x), -((float)position.y), -((float)position.z))
								* Matrix.RotationAxis(new Vector3((float)Math.Sin(rotation.x), 0f, (float)(-Math.Cos(rotation.x))), (float)rotation.y)
								* Matrix.RotationY((float)rotation.x)
								* Matrix.LookAtLH(Vector3.Zero, Vector3.UnitX, Vector3.UnitY);/**/
								Matrix.LookAtLH(Vector3.Zero/*(Vector3)(position)*/, target, Vector3.UnitY/*top*/);
				}
				return Matrix.Identity;
			}
		}
		
		public Double3DPoint Position
		{
			get
			{
				return position;
			}
			set
			{
				position = value;
			}
		}
		
		public DoublePoint Rotation
		{
			get
			{
				return rotation;
			}
			set
			{
				rotation = value;
				if (Math.Cos(rotation.y) < 0.0)
				{
					rotation.y = (Math.PI / 2.0) * Math.Sign(rotation.y);
				}
			}
		}
		
		/// <summary>
		/// Get camera field of view (FOV)
		/// </summary>
		public float FOV
		{
			get
			{
				return fov;
			}
		}
		
		/// <summary>
		/// Get or set camera type
		/// </summary>
		public MyCameraType Type
		{
			get
			{
				return type;
			}
			set
			{
				type = value;
			}
		}
    }
    
    /// <summary>
    /// Camera type enumerator
    /// </summary>
    public enum MyCameraType
    {
    	/// <summary>
    	/// Perspective camera
    	/// </summary>
    	Perspective,
    	/// <summary>
    	/// Top-down camera
    	/// </summary>
    	Top
    }
}
#endif