namespace FfxivStartupCommands.Test
{
    using ImGuiScene;
 
    
    internal class PluginUITest : IPluginUIMock
    {
        private TextureWrap goatImage;
        private SimpleImGuiScene scene;

        private readonly PluginUI pluginUI;


        private PluginUITest()
        {
            this.pluginUI = new PluginUI(new Configuration());
        }


        public static void Main(string[] args)
        {
            UIBootstrap.Inititalize(new PluginUITest());
        }


        public void Dispose()
        {
            this.pluginUI.Dispose();
        }


        public void Initialize(SimpleImGuiScene scene)
        {
            scene.OnBuildUI += Draw;

            this.scene = scene;
            this.pluginUI.Visible = true;
        }


        private void Draw()
        {
            this.pluginUI.Draw();

            // Quit UI test when windows is closed.
            if (!this.pluginUI.Visible)
            {
                this.scene.ShouldQuit = true;
            }
        }
    }
}
