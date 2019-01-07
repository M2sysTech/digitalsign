namespace DigitalSign
{
    using Veros.Framework;
    using Veros.Framework.Service;

    public class Program
    {
        public static void Main(string[] args)
        {
            Bootstrapper.Executar();

            var windowsService = IoC.Current.Resolve<WindowsService>();
            new ServiceRunner(windowsService).Run(args);
        }
    }
}
