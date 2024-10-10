using SharpGDX.Desktop;

namespace SuperJumper.Desktop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DesktopApplicationConfiguration config = new DesktopApplicationConfiguration();
            config.title = "Super Jumper";
            config.windowWidth = 480;
            config.windowHeight = 800;
            new DesktopApplication(new SuperJumper(), config);
        }
    }
}
