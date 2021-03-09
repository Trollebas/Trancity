using Engine;
using System;

namespace Trancity
{
    public class Игрок
    {
        /// <summary>
        /// Позиция камеры
        /// </summary>
        public Double3DPoint cameraPosition;
        /// <summary>
        /// Проверка позиции камеры
        /// </summary>
        public Double3DPoint cameraPositionChange;
        /// <summary>
        /// Поворот камеры
        /// </summary>
        public DoublePoint cameraRotation;
        /// <summary>
        /// Проверка поворота камеры
        /// </summary>
        public DoublePoint cameraRotationChange;
        public Guid inputGuid = Guid.Empty;//Microsoft.DirectX.DirectInput.SystemGuid.Keyboard;
        public string имя;
        public IVector объектПривязки;
        public bool поворачиватьКамеру;
        public IControlledObject управляемыйОбъект;
        //threading test:
        public Double3DPoint excameraPosition;
        public DoublePoint excameraRotation;
    }
}

