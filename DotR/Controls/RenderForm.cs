/*
 * Created by SharpDevelop.
 * User: sergey
 * Date: 15.11.2015
 * Time: 16:22
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using Engine;

namespace Engine.Controls
{
	/// <summary>
	/// Description of RenderForm.
	/// </summary>
	public partial class RenderForm : Form, IRenderControl, IInternalRenderControl, IInternalRenderForm
	{
		private RenderDevice _renderer = null;
		
		public RenderForm()
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
				ClientSize = value;
			}
		}
		
		public RenderDevice Renderer
		{
			get
			{
				return _renderer;
			}
		}
		
		void IInternalRenderForm.ShowForm()
		{
			// TODO: сделать обработчик стартовых позиций формы (см. баг с неправильным CenterScreen)
			Show();
		}
	}
}
