namespace FfxivStartupCommands
{
    using Dalamud.Game.Command;
    using Dalamud.Plugin;
    using System.IO;
    using System.Reflection;
    using ImGuiScene;


    public class Plugin : IDalamudPlugin
    {
        private const string commandName = "/pstartup";

        private DalamudPluginInterface pluginInterface;
        private Configuration configuration;
        private PluginUI ui;


        #region Properties
        public string Name => "Startup Commands";


        public DalamudPluginInterface PluginInterface
        {
            get { return this.pluginInterface; }
        }


        public Configuration Configuration
        {
            get { return this.configuration; }
        }


        public PluginUI UI
        {
            get { return this.ui; }
        }
        #endregion


        public void Dispose()
        {
            this.ui.Dispose();

            this.pluginInterface.CommandManager.RemoveHandler(commandName);
            this.pluginInterface.Dispose();
        }


        public void Initialize(DalamudPluginInterface pluginInterface)
        {
            this.pluginInterface = pluginInterface;
            
            this.configuration = this.pluginInterface.GetPluginConfig() as Configuration ?? new Configuration();
            this.configuration.Initialize(this.pluginInterface);

            // you might normally want to embed resources and load them from the manifest stream
            this.ui = new PluginUI(this.configuration);

            this.pluginInterface.CommandManager.AddHandler(commandName, new CommandInfo(OnCommand)
            {
                HelpMessage = "A useful message to display in /xlhelp"
            });

            this.pluginInterface.UiBuilder.OnBuildUi += DrawUI;
            this.pluginInterface.UiBuilder.OnOpenConfigUi += (sender, args) => DrawConfigUI();
        }


        private void DrawConfigUI()
        {
            this.ui.SettingsVisible = true;
        }


        private void DrawUI()
        {
            this.ui.Draw();
        }


        private void OnCommand(string command, string args)
        {
            // in response to the slash command, just display our main ui
            this.ui.Visible = true;
        }
    }
}
