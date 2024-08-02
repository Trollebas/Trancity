namespace Trancity
{
    public interface IОбъектПривязки3D : IVector
    {
        Double3DPoint Координаты3D { get; }

        double НаправлениеY { get; }
    }
}

