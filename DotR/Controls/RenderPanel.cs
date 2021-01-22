/*
 * Created by SharpDevelop.
 * User: sergey
 * Date: 15.11.2015
 * Time: 16:22
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Engine.Controls
{
	/// <summary>
	/// Description of RenderPanel.
	/// </summary>
	public partial class RenderPanel : UserControl, IRenderControl, IInternalRenderControl
	{
		private RenderDevice _renderer = null;
		
		public RenderPanel()
		{
			InitializeComponent();
		}
		
		IntPtr IInternalRenderControl.ControlHandle
		{
			get
			{
				return Handle;
			}
		}
		
		RenderDevice IInternalRenderControl.Renderer
		{
			set
			{
				_renderer = value;
			}
		}
		
		Size IInternalRenderControl.ControlSize
		{
			set
			{
				// TODO: переделать это дерьмо, сейчас не универсально
				if (this.Dock == DockStyle.Fill)
				{
					Parent.ClientSize = value;
				}
				this.ClientSize = value;
			}
		}
		
		public RenderDevice Renderer
		{
			get
			{
				return _renderer;
			}
		}
	}
}
