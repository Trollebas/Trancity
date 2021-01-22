namespace Common
{
    using SlimDX;

    public interface IMatrixObject
    {
        Matrix GetMatrix(int index);

        int MatricesCount { get; }
    }
}

