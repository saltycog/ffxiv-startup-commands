namespace FfxivStartupCommands.Test
{
    using ImGuiScene;
 
    
    internal class PluginUITest : IPluginUIMock
    {
        private SimpleImGuiScene scene;

        private readonly PluginUI pluginUI;


        private PluginUITest()
        {
            this.pluginUI = new PluginUI();
        }


        public static void Main(string[] args)
        {
            UIBootstrap.Initialize(new PluginUITest());
        }


        public void Dispose()
        {
            this.pluginUI.Dispose();
        }


        public void Initialize(SimpleImGuiScene scene)
        {
            scene.OnBuildUI += Draw;

            this.scene = scene;
            this.pluginUI.ConfigWindow.Show();
        }


        private void Draw()
        {
            this.pluginUI.Draw();

            // Quit UI test when windows is closed.
            if (!this.pluginUI.ConfigWindow.IsVisible)
            {
                this.scene.ShouldQuit = true;
            }
        }
    }
}
