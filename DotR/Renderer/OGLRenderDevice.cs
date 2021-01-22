/*
 * Created by SharpDevelop.
 * User: sergey
 * Date: 15.11.2015
 * Time: 14:51
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
#if OGL
using System;
using OpenTK.Graphics.OpenGL;

namespace Engine
{
	/// <summary>
	/// OGLRenderDevice - supports ogl 3.1, mb.
	/// </summary>
	public class OGLRenderDevice : RenderDevice
	{
		public OGLRenderDevice()
		{
		}
		
		public override bool IsDeviceLost
		{
			get
			{
				throw new NotImplementedException();
			}
		}
		
		public override void BeginScene()
		{
			base.BeginScene();
		}
		
		public override void EndScene()
		{
			base.EndScene();
		}
		
		public override void Clear(DeviceClearFlags flags, System.Drawing.Color color)
		{
			ClearBufferMask mask = ClearBufferMask.None;
			if ((flags & DeviceClearFlags.Target) == DeviceClearFlags.Target)
			{
				mask |= ClearBufferMask.ColorBufferBit;
			}
			if ((flags & DeviceClearFlags.Stencil) == DeviceClearFlags.Stencil)
			{
				mask |= ClearBufferMask.StencilBufferBit;
			}
			if ((flags & DeviceClearFlags.ZBuffer) == DeviceClearFlags.ZBuffer)
			{
				mask |= ClearBufferMask.DepthBufferBit;
			}
			GL.ClearColor(color);
			GL.Clear(mask);
		}
	}
}
#endif