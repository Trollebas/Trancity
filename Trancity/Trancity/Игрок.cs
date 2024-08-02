namespace Trancity
{
    using System;

    public class Игрок
    {
        public Double3DPoint cameraPosition;
        public Double3DPoint cameraPositionChange;
        public DoublePoint cameraRotation;
        public DoublePoint cameraRotationChange;
        public Guid inputGuid = Guid.Empty;//Microsoft.DirectX.DirectInput.SystemGuid.Keyboard;
        public string имя;
        public IVector объектПривязки;
        public bool поворачиватьКамеру;
        public IControlledObject управляемыйОбъект;
    }
}

