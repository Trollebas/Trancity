namespace Trancity
{
    public interface IОбъектПривязки3D : IVector
    {
        Engine.Double3DPoint Координаты3D { get; }

        double НаправлениеY { get; }
    }
}

