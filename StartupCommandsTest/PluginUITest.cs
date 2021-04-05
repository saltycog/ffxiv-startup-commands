namespace FfxivStartupCommands.Test
{
    using ImGuiScene;
 
    
    internal class PluginUITest : IPluginUIMock
    {
        private SimpleImGuiScene scene;

        private readonly PluginUI pluginUI;


        private PluginUITest()
        {
            this.pluginUI = new PluginUI(new Plugin());
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
            this.pluginUI.DebugWindow.Show();
        }


        private void Draw()
        {
            this.pluginUI.Draw();

            // Quit UI test when windows is closed.
            if (!this.pluginUI.DebugWindow.IsVisible)
            {
                this.scene.ShouldQuit = true;
            }
        }
    }
}
