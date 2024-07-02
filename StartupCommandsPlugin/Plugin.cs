namespace FfxivStartupCommands
{
    using Dalamud.Game.Command;
    using Dalamud.Plugin;


    /// <summary>
    /// Main plugin entry point.
    /// Direct singleton access to other primary components (for convenience, let's not get fancy here).
    /// </summary>
    public class Plugin : IDalamudPlugin
    {
        private const string mainCommandName = "/startup";

        private static Plugin _instance;

        private Configuration configuration = new Configuration();
        private GameClient gameClient;
        private StartupHandlers startupHandlers;
        private PluginUI ui;

        /// <summary>
        /// Plugin configuration.
        /// </summary>
        public static Configuration Configuration
        {
            get { return Instance.configuration; }
        }

        /// <summary>
        /// Game client utility functions.
        /// </summary>
        public static GameClient GameClient
        {
            get { return Instance.gameClient; }
        }

        /// <summary>
        /// Handlers for plugin startup logic.
        /// </summary>
        public static StartupHandlers StartupHandlers
        {
            get { return Instance.startupHandlers; }
        }

        /// <summary>
        /// All accessible UI windows.
        /// </summary>
        public static PluginUI UI
        {
            get
            {
                return Instance.ui;
            }
        }

        /// <summary>
        /// Name of the plugin.
        /// </summary>
        public string Name => "Startup Commands";

        /// <summary>
        /// Singleton instance access - use this instead of _instance.
        /// </summary>
        private static Plugin Instance
        {
            get
            {
                return _instance;
            }
        }


        public Plugin(IDalamudPluginInterface dalamudPluginInterface)
        {
            _instance = this;

            Services.Initialize(dalamudPluginInterface);
            
            if (Services.PluginInterface != null)
                this.configuration = Services.PluginInterface.GetPluginConfig() as Configuration ?? new Configuration();    
            else
                this.configuration = new Configuration();
            
            this.configuration.Initialize(Services.PluginInterface);
            
            this.ui = new PluginUI();
            
            this.startupHandlers = new StartupHandlers();
            this.gameClient = new GameClient();

            RegisterHooks();
            
            if (Services.ClientState.LocalPlayer != null)
                Configuration.SetCurrentCharacter(Services.ClientState.LocalPlayer.Name.ToString());
        }


        /// <summary>
        ///  Log messages directly to in-game chat box.
        /// </summary>
        /// <param name="text">Text to display in chat box.</param>
        public static void LogToChat(string text)
        {
            Services.ChatGui.Print(text);
        }


        public void Dispose()
        {
            Services.CommandManager.RemoveHandler(mainCommandName);
            Services.PluginInterface.UiBuilder.Draw -= this.ui.Draw;

            Services.ClientState.Login -= this.startupHandlers.OnLogin;
            Services.ClientState.Logout -= this.startupHandlers.OnLogout;
            
            this.ui.Dispose();
            this.startupHandlers.Dispose();
            //PluginInterface.Dispose();
        }


        /// <summary>
        /// Handler for when plugin slash command is entered in chat.
        /// </summary>
        private void OnCommand(string command, string args)
        {
            // in response to the slash command, just display our main ui
            this.ui.ConfigWindow.IsVisible = true;
        }


        /// <summary>
        /// Register all commands plugin will use.
        /// </summary>
        private void RegisterCommandHooks()
        {
            Services.CommandManager.AddHandler(
                mainCommandName,
                new CommandInfo(OnCommand)
                    { 
                        HelpMessage = "Open configuration for Startup Commands to perform behaviors upon character login."
                    });
        }


        private void RegisterHooks()
        {
            if (Services.PluginInterface == null)
                return;
            
            RegisterCommandHooks();
            RegisterStartupHooks();
            RegisterUIHooks();
        }


        private void RegisterStartupHooks()
        {
            Services.ClientState.Login += this.startupHandlers.OnLogin;
            Services.ClientState.Logout += this.startupHandlers.OnLogout;
        }


        private void RegisterUIHooks()
        {
            Services.PluginInterface.UiBuilder.Draw += this.ui.Draw;
            Services.PluginInterface.UiBuilder.OpenConfigUi +=
                () =>
                    {
                        if (Services.ClientState.LocalPlayer == null)
                            return;

                        if (Configuration.CurrentCharacter == null)
                        {
                            Configuration.SetCurrentCharacter(Services.ClientState.LocalPlayer.Name.ToString());
                        }
                        
                        this.ui.ConfigWindow.Show();
                    };
        }
    }
}
