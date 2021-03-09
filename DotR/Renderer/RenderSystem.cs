/*
 * Created by SharpDevelop.
 * User: sergey
 * Date: 15.11.2015
 * Time: 16:45
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using Engine.Controls;
using System;

namespace Engine
{
    /// <summary>
    /// RenderSystem - manages render device and form.
    /// Бесполезный класс
    /// </summary>
    public static class RenderSystem
    {
        #region Previous class structure

        /*private RenderDevice device;
		
		public RenderSystem(RenderDeviceType deviceType, IRenderControl control, DeviceOptions parametrs)
		{
			//
		}
		
		public RenderDevice CurrentDevice
		{
			get
			{
				return device;
			}
		}*/

        #endregion

        public static RenderDevice CreateDevice(/*this*/ IRenderControl control, RenderDeviceType deviceType, DeviceOptions parameters)
        {
            RenderDevice device = null;

            //check for exist renderer
            if (control.Renderer != null)
            {
                throw new InvalidOperationException("Не может заменить существующий рендерер!");
            }

            //convert to internal interface and check if successed
            IInternalRenderControl internalControl = control as IInternalRenderControl;
            if (internalControl == null)
            {
                throw new NotSupportedException("Недопустимый контроль");
            }

            //create our device
            switch (deviceType)
            {
                case RenderDeviceType.DirectX9:
#if DX9
                    device = new DX9RenderDevice(internalControl, parameters);
                    break;
#else
						throw new NotImplementedException("DX9 not included in this version!");
#endif

                case RenderDeviceType.OpenGL:
#if OGL
						throw new NotImplementedException();
						break;
#else
                    throw new NotImplementedException("OpenGL не входит в эту версию!");
#endif

                default:
                    throw new NotSupportedException(string.Format("Указанный тип устройства {0} недоступен!", deviceType.ToString()));
            }

            //sve current renderer
            internalControl.Renderer = device;

            //overhead of forms
            internalControl.ControlSize = new System.Drawing.Size(parameters.windowedX, parameters.windowedY);

            if ((control as IInternalRenderForm) != null)
            {
                ((IInternalRenderForm)control).ShowForm();
            }

            //and exit
            return device;
        }
    }

    public enum RenderDeviceType
    {
        DirectX9,
        OpenGL
    }
}
