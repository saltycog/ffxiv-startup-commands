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

        private DalamudPluginInterface dalamud;
        private Configuration configuration = new Configuration();
        private GameClient gameClient;
        private StartupHandlers startupHandlers;
        private PluginUI ui;


        #region Properties
        /// <summary>
        /// Plugin configuration.
        /// </summary>
        public static Configuration Configuration
        {
            get { return Instance.configuration; }
        }

        /// <summary>
        /// Dalamud plugin interface.
        /// </summary>
        public static DalamudPluginInterface Dalamud
        {
            get { return Instance.dalamud; }
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
                if (_instance == null)
                {
                    _instance = new Plugin();
                }
                return _instance;
            }
        }
        #endregion


        /// <summary>
        ///  Log messages directly to in-game chat box.
        /// </summary>
        /// <param name="text">Text to display in chat box.</param>
        public static void LogToChat(string text)
        {
            Dalamud.Framework.Gui.Chat.Print(text);
        }


        public void Dispose()
        {
            this.dalamud.CommandManager.RemoveHandler(mainCommandName);
            this.dalamud.UiBuilder.OnBuildUi -= this.ui.Draw;
            this.dalamud.ClientState.OnLogin -= this.startupHandlers.OnLogin;
            this.dalamud.ClientState.OnLogout -= this.startupHandlers.OnLogout;
            
            this.ui.Dispose();
            this.startupHandlers.Dispose();
            this.dalamud.Dispose();
        }


        public void Initialize(DalamudPluginInterface pluginInterface)
        {
            _instance = this;
            
            this.dalamud = pluginInterface;

            if (this.dalamud != null)
                this.configuration = this.dalamud.GetPluginConfig() as Configuration ?? new Configuration();    
            else
                this.configuration = new Configuration();
            
            this.configuration.Initialize(this.dalamud);
            
            this.ui = new PluginUI();
            
            this.startupHandlers = new StartupHandlers();
            this.gameClient = new GameClient();

            RegisterHooks();
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
            this.dalamud.CommandManager.AddHandler(
                mainCommandName,
                new CommandInfo(OnCommand)
                    { 
                        HelpMessage = "Open configuration for Startup Commands to perform behaviors upon character login."
                    });
        }


        private void RegisterHooks()
        {
            if (this.dalamud == null)
                return;
            
            RegisterCommandHooks();
            RegisterStartupHooks();
            RegisterUIHooks();
        }


        private void RegisterStartupHooks()
        {
            this.dalamud.ClientState.OnLogin += this.startupHandlers.OnLogin;
            this.dalamud.ClientState.OnLogout += this.startupHandlers.OnLogout;
        }


        private void RegisterUIHooks()
        {
            this.dalamud.UiBuilder.OnBuildUi += this.ui.Draw;
            this.dalamud.UiBuilder.OnOpenConfigUi +=
                (sender, args) =>
                    {
                        this.ui.ConfigWindow.Show();
                    };
        }
    }
}
