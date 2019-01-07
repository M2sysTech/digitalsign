namespace Veros.Paperless.Model
{
    using System;

    public interface IRelogio
    {
        DateTime Agora();

        DateTime Hoje();
    }
}