/*
 * Created by SharpDevelop.
 * User: sergey
 * Date: 15.11.2015
 * Time: 17:54
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Engine.Controls
{
    /// <summary>
    /// IRenderControl - интерфейс для используемых контролов.
    /// Будет сильно торчать наружу, надо подумать как обрубить концы
    /// </summary>
    public interface IRenderControl
    {


        RenderDevice Renderer { get; }
    }

    /// <summary>
    /// накрутим немного колхоза
    /// </summary>
    internal interface IInternalRenderControl
    {
        IntPtr ControlHandle { get; }

        System.Drawing.Size ControlSize { set; }

        RenderDevice Renderer { set; }
    }

    /// <summary>
    /// и ещё немного колхоза
    /// </summary>
    internal interface IInternalRenderForm
    {
        void ShowForm();
    }
}
