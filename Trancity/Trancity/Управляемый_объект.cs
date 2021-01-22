namespace Trancity
{
    using System;

    public interface Управляемый_объект : Объект_привязки
    {
        Управление управление { get; set; }
    }
}

