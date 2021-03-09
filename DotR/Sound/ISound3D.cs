/*
 * Created by SharpDevelop.
 * User: sergey
 * Date: 13.02.2016
 * Time: 20:45
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace Engine.Sound
{
    /// <summary>
    /// ISound3D - обобщённый интерфейс для источника звука в трёхмерном пространстве.
    /// --По идее, должен наследоваться (см. методы Play, Stop)--
    /// </summary>
    public interface ISound3D : ISound2D
    {
        void Update(ref Double3DPoint position/*, ref DoublePoint rotation*/);
    }
}
