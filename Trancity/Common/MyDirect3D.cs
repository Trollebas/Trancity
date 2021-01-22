using Trancity;
using Engine;

namespace Common
{
	using SlimDX;
	using SlimDX.Direct3D9;
	using System;
	using System.Drawing;
	using System.Windows.Forms;

	public class MyDirect3D
	{
		public static Double3DPoint Camera_Position;
		public static DoublePoint Camera_Rotation;
		public static Device device;
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
		public static double масштаб = 10.0;
		public static float zfarplane = 4000f;
		public static float[,] frustum = new float[6,4];
		private static float t;
		private static Direct3D d3d9;
		private static bool _alpha = false;
		public static Effect test_effect;
		public static float light_intency = 1.0f;
		public static int light_color = 120;
		private static readonly Matrix matr_hpi = Matrix.RotationX((float)(-(Math.PI / 2.0)));
		private static readonly Matrix matr_scale = Matrix.Scaling(20f, 20f, 20f);
		private static readonly Matrix matr_lookatLH = Matrix.LookAtLH(new Vector3(0f, 0f, 0f), new Vector3(1f, 0f, 0f), new Vector3(0f, 1f, 0f));
		
		public static RenderDevice _newDevice;

		public static bool Initialize(Control control, DeviceType device_type, CreateFlags create_flags, bool windowed)
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
				d3d9 = new Direct3D();
				if ((Window_Width == 0) || (Window_Height == 0))
				{
					var dialog = DeviceOptionsDialog.LoadDeviceOptions("DeviceOptions.xml");
					if (dialog.windowed)
					{
						windowed = true;
						Window_Width = dialog.windowedX;
						Window_Height = dialog.windowedY;
					}
					else
					{
						windowed = false;
						parameters.FullScreenRefreshRateInHertz = dialog.fullscreenRate;
						Window_Width = dialog.fullscreenX;
						Window_Height = dialog.fullscreenY;
					}
				}
				MyDirect3D.windowed = windowed;
				if (windowed)
				{
					if (control is Form)
					{
						control.ClientSize = new Size(Window_Width, Window_Height);
						control.Location = new Point(Screen.PrimaryScreen.WorkingArea.X + ((Screen.PrimaryScreen.WorkingArea.Width - Window_Width) / 2), Screen.PrimaryScreen.WorkingArea.Y + ((Screen.PrimaryScreen.WorkingArea.Height - Window_Height) / 2));
					}
					parameters.Windowed = true;
					parameters.FullScreenRefreshRateInHertz = 0;
					Window_Width = control.ClientSize.Width;
					Window_Height = control.ClientSize.Height;
				}
				parameters.BackBufferWidth = Window_Width;
				parameters.BackBufferHeight = Window_Height;
				parameters.BackBufferCount = 1;
				parameters.BackBufferFormat = Format.A8R8G8B8;
				parameters.PresentationInterval = PresentInterval.Immediate;
				parameters.DeviceWindowHandle = control.Handle;
				device = new Device(d3d9, 0, device_type, control.Handle, create_flags | CreateFlags.Multithreaded, new[] { parameters });
				try
				{
					for (var i = 0; i < 11; i++)
					{
						device.EnableLight(i, false);
					}
					for (var i = 0; i < 11; i+=2)
					{
						var lightsrc = new Light();
						lightsrc.Type = LightType.Directional;
						int red = (int)((120 + Cheats._random.Next(11)) * light_intency);
						lightsrc.Diffuse = Color.FromArgb(red, red, red);
						switch (i)
						{
							case 0:
								lightsrc.Direction = new Vector3(0f, -1f, 0f);
								break;

							case 1:
								lightsrc.Direction = new Vector3(0f, 1f, 0f);
								break;

							case 2:
								lightsrc.Direction = new Vector3(1f, 0f, 0f);
								break;

							case 3:
								lightsrc.Direction = new Vector3(1f, 0f, 1f);
								break;

							case 4:
								lightsrc.Direction = new Vector3(0f, 0f, 1f);
								break;

							case 5:
								lightsrc.Direction = new Vector3(-1f, 0f, 1f);
								break;

							case 6:
								lightsrc.Direction = new Vector3(-1f, 0f, 0f);
								break;

							case 7:
								lightsrc.Direction = new Vector3(-1f, 0f, -1f);
								break;

							case 8:
								lightsrc.Direction = new Vector3(0f, 0f, -1f);
								break;

							case 9:
								lightsrc.Direction = new Vector3(1f, 0f, -1f);
								break;

							case 10:
								lightsrc.Direction = new Vector3(0f, 1f, 0f);
								break;
						}
						device.SetLight(i, lightsrc);
						device.EnableLight(i, true);
					}
					device.SetRenderState(RenderState.ZEnable, true);
					device.SetRenderState(RenderState.Ambient, Color.White.ToArgb());
					try
					{
						test_effect = Effect.FromFile(device, @"Skybox\shader_test.fx", ShaderFlags.SkipValidation);
						test_effect.Technique = "_ambient";
					}
					catch (Exception exc)
					{
						Logger.LogException(exc, "Обойдёмся без шейдеров");
					}
					MyGUI.Initialize();
					device.Clear(ClearFlags.ZBuffer | ClearFlags.Target, 0, 1f, 0);
					device.Present();
				}
				catch (Exception exc)
				{
					MessageBox.Show("Unexpected error occured while creating a device.\nThe created device may work incorrectly.\n" + exc.ToString(), "Direct3D", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				return true;
			}
			catch (Direct3D9Exception exc)
			{
				MessageBox.Show("Direct3D9Exception\n\n" + exc.ToString());
				return false;
			}
			catch (NullReferenceException exception)
			{
				MessageBox.Show("NullReferenceException\n\n" + exception.ToString());
				return false;
			}
		}
		
		public static bool Initialize2(Engine.Controls.RenderForm control)
		{
			var dialog = DeviceOptionsDialog.LoadDeviceOptions("DeviceOptions.xml");
			return InitializeWOpt(control, dialog);
		}
		
		public static bool InitializeWOpt(Engine.Controls.IRenderControl control, DeviceOptions dialog)
		{
			try
			{
				if (dialog.windowed)
				{
					windowed = true;
					Window_Width = dialog.windowedX;
					Window_Height = dialog.windowedY;
				}
				else
				{
					windowed = false;
					Window_Width = dialog.fullscreenX;
					Window_Height = dialog.fullscreenY;
				}
				
				_newDevice = RenderSystem.CreateDevice(control, RenderDeviceType.DirectX9, dialog);
				if (control is Form)
					((Form)control).Location = new Point(Screen.PrimaryScreen.WorkingArea.X + ((Screen.PrimaryScreen.WorkingArea.Width - Window_Width) / 2),
				                             Screen.PrimaryScreen.WorkingArea.Y + ((Screen.PrimaryScreen.WorkingArea.Height - Window_Height) / 2));
				device = ((DX9RenderDevice)_newDevice).RawDevice;
				try
				{
					for (var i = 0; i < 11; i++)
					{
						device.EnableLight(i, false);
					}
					for (var i = 0; i < 11; i+=2)
					{
						var lightsrc = new Light();
						lightsrc.Type = LightType.Directional;
						int red = (int)((120 + Cheats._random.Next(11)) * light_intency);
						lightsrc.Diffuse = Color.FromArgb(red, red, red);
						switch (i)
						{
							case 0:
								lightsrc.Direction = new Vector3(0f, -1f, 0f);
								break;

							case 1:
								lightsrc.Direction = new Vector3(0f, 1f, 0f);
								break;

							case 2:
								lightsrc.Direction = new Vector3(1f, 0f, 0f);
								break;

							case 3:
								lightsrc.Direction = new Vector3(1f, 0f, 1f);
								break;

							case 4:
								lightsrc.Direction = new Vector3(0f, 0f, 1f);
								break;

							case 5:
								lightsrc.Direction = new Vector3(-1f, 0f, 1f);
								break;

							case 6:
								lightsrc.Direction = new Vector3(-1f, 0f, 0f);
								break;

							case 7:
								lightsrc.Direction = new Vector3(-1f, 0f, -1f);
								break;

							case 8:
								lightsrc.Direction = new Vector3(0f, 0f, -1f);
								break;

							case 9:
								lightsrc.Direction = new Vector3(1f, 0f, -1f);
								break;

							case 10:
								lightsrc.Direction = new Vector3(0f, 1f, 0f);
								break;
						}
						device.SetLight(i, lightsrc);
						device.EnableLight(i, true);
					}
					device.SetRenderState(RenderState.ZEnable, true);
					device.SetRenderState(RenderState.Ambient, Color.White.ToArgb());
					try
					{
						test_effect = Effect.FromFile(device, @"Skybox\shader_test.fx", ShaderFlags.SkipValidation);
						test_effect.Technique = "_ambient";
					}
					catch (Exception exc)
					{
						Logger.LogException(exc, "Обойдёмся без шейдеров");
					}
					MyGUI.Initialize();
					device.Clear(ClearFlags.ZBuffer | ClearFlags.Target, 0, 1f, 0);
					device.Present();
				}
				catch (Exception exc)
				{
					MessageBox.Show("Unexpected error occured while creating a device.\nThe created device may work incorrectly.\n" + exc.ToString(), "Direct3D", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				return true;
			}
			catch (Direct3D9Exception exc)
			{
				MessageBox.Show("Direct3D9Exception\n\n" + exc.ToString());
				return false;
			}
			catch (NullReferenceException exception)
			{
				MessageBox.Show("NullReferenceException\n\n" + exception.ToString());
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

		public static void SetCameraPos(Double3DPoint position, DoublePoint rotation)
		{
			Camera_Position = position;
			Camera_Rotation = rotation;
			if (!вид_сверху)
			{
				DoublePoint point = new DoublePoint(rotation.x - (Math.PI / 2.0));
				device.SetTransform(TransformState.Projection, Matrix.Translation(-((float)position.x), -((float)position.y), -((float)position.z)) * (matr_scale * Matrix.RotationAxis(new Vector3((float)point.x, 0f, (float)point.y), (float)rotation.y)) * Matrix.RotationY((float)rotation.x) * matr_lookatLH * Matrix.PerspectiveLH((4f * viewport_width) / ((float)viewport_height), 4f, 4f, zfarplane));
			}
			else
			{
				device.SetTransform(TransformState.Projection, Matrix.Translation(-((float)position.x), -50f, -((float)position.z)) * matr_hpi * Matrix.OrthoLH(((float)viewport_width) / ((float)масштаб), ((float)viewport_height) / ((float)масштаб), 0.1f, 100f));
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
		
		public static bool SphereInFrustum(Engine.Sphere sphere)
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
		
		public static bool AABBInFrustum(Engine.AABB aabb)
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

