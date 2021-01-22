#if false
using SlimDX;
using SlimDX.Direct3D9;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Engine
{
	public static class MyDirect3D
	{
		public static Double3DPoint Camera_Position;
		public static DoublePoint Camera_Rotation;
		private static Device device;
		public const int players_max = 4;
		public const int players_min = 1;
		public static int viewport_height;
		public static int viewport_width;
		public static int[] viewport_x;
		public static int[] viewport_y;
		public static int Window_Height = 960;
		public static int Window_Width = 0x500;
		public static bool windowed;
		public static bool вид_сверху;
		public static bool карта;
		public static double масштаб = 1.0;
		private static Direct3D direct3d;
		private static bool _alpha = false;
		public static float[,] frustum = new float[6,4];
		private static float t;
		
		public static bool PreInit()
		{
			try
			{
				direct3d = new Direct3D();
				return true;
			}
			catch (Exception e)
			{
				Logger.LogException(e, "D3D preint failed!");
				return false;
			}
		}
		
		public static DisplayModeCollection GetAvailableDisplayModes(int adapter_id)
		{
			return direct3d.Adapters[adapter_id].GetDisplayModes(direct3d.Adapters[adapter_id].CurrentDisplayMode.Format);
		}

		public static bool Initialize(Control control, DeviceOptions options)
		{
			try
			{
				var parameters = new PresentParameters
				{
					Windowed = false,
					FullScreenRefreshRateInHertz = 60,
					SwapEffect = SwapEffect.Discard,
					EnableAutoDepthStencil = true,
					AutoDepthStencilFormat = Format.D16
				};
				CreateFlags vertex_processing_flag;
				switch (options.vertexProcessingMode)
				{
					case 0:
						vertex_processing_flag = CreateFlags.SoftwareVertexProcessing;
						break;
					
					case 2:
						vertex_processing_flag = CreateFlags.MixedVertexProcessing;
						break;
					
					case 1:
					default:
						vertex_processing_flag = CreateFlags.HardwareVertexProcessing;
						break;
				}
				DeviceType device_type;
				switch (options.deviceType)
				{
					//software not supported!
					default:
						device_type = DeviceType.Hardware;
						break;
				}
				MyDirect3D.windowed = options.windowed;
				if (windowed)
				{
					Window_Width = options.windowedX;
					Window_Height = options.windowedY;
					if (control is Form)
					{
						control.ClientSize = new Size(Window_Width, Window_Height);
						control.Location = new Point(Screen.PrimaryScreen.WorkingArea.X + ((Screen.PrimaryScreen.WorkingArea.Width - Window_Width) / 2), Screen.PrimaryScreen.WorkingArea.Y + ((Screen.PrimaryScreen.WorkingArea.Height - Window_Height) / 2));
					}
					parameters.Windowed = true;
					parameters.FullScreenRefreshRateInHertz = 0;
				}
				else
				{
					parameters.FullScreenRefreshRateInHertz = options.fullscreenRate;
					Window_Width = options.fullscreenX;
					Window_Height = options.fullscreenY;
				}
				parameters.BackBufferWidth = Window_Width;
				parameters.BackBufferHeight = Window_Height;
				parameters.BackBufferCount = 1;
				parameters.BackBufferFormat = Format.A8R8G8B8;
				parameters.PresentationInterval = PresentInterval.Immediate;
				parameters.DeviceWindowHandle = control.Handle;
				device = new Device(direct3d, options.adapterID, device_type, control.Handle, CreateFlags.Multithreaded | vertex_processing_flag, new[] { parameters });
				
				device.SetRenderState(RenderState.Lighting, false);
				device.SetRenderState(RenderState.ZEnable, true);
				device.SetRenderState(RenderState.Ambient, Color.White.ToArgb());
				Alpha = true;
				device.Clear(ClearFlags.ZBuffer | ClearFlags.Target, 0, 1f, 0);
				device.Present();

				return true;
			}
			catch (Exception e)
			{
				Logger.LogException(e, "D3D init failed!");
				return false;
			}
		}

		public static void ResetViewports(int count)
		{
			if (viewport_x == null)
			{
				viewport_x = new int[count];
				viewport_y = new int[count];
				switch (count)
				{
					case 1:
						viewport_width = Window_Width;
						viewport_height = Window_Height;
						viewport_x[0] = 0;
						viewport_y[0] = 0;
						return;

					case 2:
						viewport_width = Window_Width;
						viewport_height = Window_Height / 2;
						viewport_x[0] = 0;
						viewport_y[0] = 0;
						viewport_x[1] = 0;
						viewport_y[1] = Window_Height / 2;
						return;

					case 3:
						viewport_width = Window_Width / 2;
						viewport_height = Window_Height / 2;
						viewport_x[0] = 0;
						viewport_y[0] = 0;
						viewport_x[1] = Window_Width / 2;
						viewport_y[1] = 0;
						viewport_x[2] = 0;
						viewport_y[2] = Window_Height / 2;
						return;

					case 4:
						viewport_width = Window_Width / 2;
						viewport_height = Window_Height / 2;
						viewport_x[0] = 0;
						viewport_y[0] = 0;
						viewport_x[1] = Window_Width / 2;
						viewport_y[1] = 0;
						viewport_x[2] = 0;
						viewport_y[2] = Window_Height / 2;
						viewport_x[3] = Window_Width / 2;
						viewport_y[3] = Window_Height / 2;
						return;
				}
				viewport_width = Window_Width;
				viewport_height = Window_Height;
			}
		}

		public static void SetProjectionMatrix(MyCamera camera)
		{
			Camera_Position = camera.Position;
			Camera_Rotation = camera.Rotation;
			device.SetTransform(TransformState.View, camera.ViewMatrix);
			switch (camera.Type)
			{
				case MyCameraType.Perspective:
					device.SetTransform(TransformState.Projection, Matrix.PerspectiveFovLH(camera.FOV, ((float)(viewport_width)) / ((float)(viewport_height)), 1f, 1000f));
					break;
					
				case MyCameraType.Top:
					device.SetTransform(TransformState.Projection, Matrix.OrthoLH((float)(viewport_width * масштаб), (float)(viewport_height * масштаб), 0.1f, 100f));
					break;
			}
		}

		public static void SetViewport(int index)
		{
			Viewport viewport = device.Viewport;
			if ((index == -1) || ((index == 0) && (viewport_x.Length == 1)))
			{
				viewport.Width = Window_Width;
				viewport.Height = Window_Height;
				viewport.X = 0;
				viewport.Y = 0;
				device.Viewport = viewport;
				return;
			}
			if (((viewport_x != null) && (viewport_y != null)) && (((index >= 0) && (index < viewport_x.Length)) && (index < viewport_y.Length)))
			{
				viewport.Width = viewport_width - 10;
				viewport.Height = viewport_height - 10;
				viewport.X = viewport_x[index] + 5;
				viewport.Y = viewport_y[index] + 5;
				if (viewport.X == 5)
				{
					viewport.X = 0;
					viewport.Width += 5;
				}
				if (viewport.Y == 5)
				{
					viewport.Y = 0;
					viewport.Height += 5;
				}
				if ((viewport.X + viewport.Width) == (Window_Width - 5))
				{
					viewport.Width += 5;
				}
				if ((viewport.Y + viewport.Height) == (Window_Height - 5))
				{
					viewport.Height += 5;
				}
				device.Viewport = viewport;
			}
		}

		public static bool Alpha
		{
			get
			{
				return ((device != null) && _alpha);
			}
			set
			{
				if (device == null) return;
				device.SetRenderState(RenderState.AlphaBlendEnable, value);
				device.SetRenderState(RenderState.SourceBlend, value ? Blend.SourceAlpha : Blend.One);
				device.SetRenderState(RenderState.DestinationBlend, value ? Blend.InverseSourceAlpha : Blend.Zero);
				_alpha = value;
			}
		}
		
		// TODO: выкинуть в отдельный модуль, передавть апи-независимую матрицу (а её ещё реализовать надо, лол)
		public static void ComputeFrustum()
		{
			var projmat = device.GetTransform(TransformState.Projection);
			frustum[0,0] = projmat.M14 + projmat.M11;
			frustum[0,1] = projmat.M24 + projmat.M21;
			frustum[0,2] = projmat.M34 + projmat.M31;
			frustum[0,3] = projmat.M44 + projmat.M41;
			
			t = (float)Math.Sqrt(frustum[0,0] * frustum[0,0] + frustum[0,1] * frustum[0,1] + frustum[0,2] * frustum[0,2]);
   				frustum[0,0] /= t;
   				frustum[0,1] /= t;
   				frustum[0,2] /= t;
   				frustum[0,3] /= t;
			
			frustum[1,0] = projmat.M14 - projmat.M11;
			frustum[1,1] = projmat.M24 - projmat.M21;
			frustum[1,2] = projmat.M34 - projmat.M31;
			frustum[1,3] = projmat.M44 - projmat.M41;
			
			t = (float)Math.Sqrt(frustum[1,0] * frustum[1,0] + frustum[1,1] * frustum[1,1] + frustum[1,2] * frustum[1,2]);
   				frustum[1,0] /= t;
   				frustum[1,1] /= t;
   				frustum[1,2] /= t;
   				frustum[1,3] /= t;
			
			frustum[2,0] = projmat.M14 + projmat.M12;
			frustum[2,1] = projmat.M24 + projmat.M22;
			frustum[2,2] = projmat.M34 + projmat.M32;
			frustum[2,3] = projmat.M44 + projmat.M42;
			
			t = (float)Math.Sqrt(frustum[2,0] * frustum[2,0] + frustum[2,1] * frustum[2,1] + frustum[2,2] * frustum[2,2]);
   				frustum[2,0] /= t;
   				frustum[2,1] /= t;
   				frustum[2,2] /= t;
   				frustum[2,3] /= t;
			
			frustum[3,0] = projmat.M14 - projmat.M12;
			frustum[3,1] = projmat.M24 - projmat.M22;
			frustum[3,2] = projmat.M34 - projmat.M32;
			frustum[3,3] = projmat.M44 - projmat.M42;
			
			t = (float)Math.Sqrt(frustum[3,0] * frustum[3,0] + frustum[3,1] * frustum[3,1] + frustum[3,2] * frustum[3,2]);
   				frustum[3,0] /= t;
   				frustum[3,1] /= t;
   				frustum[3,2] /= t;
   				frustum[3,3] /= t;
			
			frustum[4,0] = projmat.M14 - projmat.M13;
			frustum[4,1] = projmat.M24 - projmat.M23;
			frustum[4,2] = projmat.M34 - projmat.M33;
			frustum[4,3] = projmat.M44 - projmat.M43;
			
			t = (float)Math.Sqrt(frustum[4,0] * frustum[4,0] + frustum[4,1] * frustum[4,1] + frustum[4,2] * frustum[4,2]);
   				frustum[4,0] /= t;
   				frustum[4,1] /= t;
   				frustum[4,2] /= t;
   				frustum[4,3] /= t;
			
			frustum[5,0] = projmat.M13;
			frustum[5,1] = projmat.M23;
			frustum[5,2] = projmat.M33;
			frustum[5,3] = projmat.M43;
			
			t = (float)Math.Sqrt(frustum[5,0] * frustum[5,0] + frustum[5,1] * frustum[5,1] + frustum[5,2] * frustum[5,2]);
   				frustum[5,0] /= t;
   				frustum[5,1] /= t;
   				frustum[5,2] /= t;
   				frustum[5,3] /= t;
		}
		
		public static bool SphereInFrustum(Sphere sphere)
		{
			float d;
			for(int p = 0; p < 6; p++)
			{
				d = frustum[p,0] * (float)sphere.position.x + frustum[p,1] * (float)sphere.position.y + frustum[p,2] * (float)sphere.position.z + frustum[p,3];
				if (d > (float)-sphere.radius)
				{
					if (p < 5) continue;
					sphere.LODnum = 0;
					if (d > 150)
					{
						sphere.LODnum = 1;
					}
					continue;
				}
				return false;
			}
			return true;
		}
		
		/*public static bool PointInFrustum(Double3DPoint point)
		{
   			for(int p = 0; p < 6; p++)
      			if( frustum[p,0] * point.x + frustum[p,1] * point.y + frustum[p,2] * point.z + frustum[p,3] > 0 )
        		 return false;
   			return true;
		}*/
		
		public static bool AABBInFrustum(AABB aabb)
		{
			for(int p = 0; p < 6; p++)
			{
				if( frustum[p,0] * aabb.min.x + frustum[p,1] * aabb.min.y + frustum[p,2] * aabb.min.z + frustum[p,3] > 0 )
					continue;
				if( frustum[p,0] * aabb.max.x + frustum[p,1] * aabb.min.y + frustum[p,2] * aabb.min.z + frustum[p,3] > 0 )
					continue;
				if( frustum[p,0] * aabb.min.x + frustum[p,1] * aabb.max.y + frustum[p,2] * aabb.min.z + frustum[p,3] > 0 )
					continue;
				if( frustum[p,0] * aabb.max.x + frustum[p,1] * aabb.max.y + frustum[p,2] * aabb.min.z + frustum[p,3] > 0 )
					continue;
				if( frustum[p,0] * aabb.min.x + frustum[p,1] * aabb.min.y + frustum[p,2] * aabb.max.z + frustum[p,3] > 0 )
					continue;
				if( frustum[p,0] * aabb.max.x + frustum[p,1] * aabb.min.y + frustum[p,2] * aabb.max.z + frustum[p,3] > 0 )
					continue;
				if( frustum[p,0] * aabb.min.x + frustum[p,1] * aabb.max.y + frustum[p,2] * aabb.max.z + frustum[p,3] > 0 )
					continue;
				if( frustum[p,0] * aabb.max.x + frustum[p,1] * aabb.max.y + frustum[p,2] * aabb.max.z + frustum[p,3] > 0 )
					continue;
				return false;
			}
			return true;
		}
	}
}

#endif