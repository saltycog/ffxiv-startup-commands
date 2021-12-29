namespace FfxivStartupCommands
{
    using Dalamud.Game;
    using Dalamud.Game.ClientState;
    using Dalamud.Game.Command;
    using Dalamud.Game.Gui;
    using Dalamud.IoC;
    using Dalamud.Plugin;


    /// <summary>
    /// Main plugin entry point.
    /// Direct singleton access to other primary components (for convenience, let's not get fancy here).
    /// </summary>
    public class Plugin : IDalamudPlugin
    {
        private const string mainCommandName = "/startup";

        private static Plugin _instance;

        [PluginService]
        public static DalamudPluginInterface PluginInterface { get; private set; }
        
        [PluginService]
        public static CommandManager CommandManager { get; private set; }
        
        [PluginService]
        public static ClientState ClientState { get; private set; }
        
        [PluginService]
        public static Framework Framework { get; private set; }
        
        [PluginService]
        public static ChatGui ChatGui { get; private set; }
        
        [PluginService]
        public static GameGui GameGui { get; private set; }
        
        [PluginService]
        public static SigScanner TargetModuleScanner { get; private set; }
        
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
            ChatGui.Print(text);
        }


        public void Dispose()
        {
            CommandManager.RemoveHandler(mainCommandName);
            PluginInterface.UiBuilder.Draw -= this.ui.Draw;
            
            ClientState.Login -= this.startupHandlers.OnLogin;
            ClientState.Logout -= this.startupHandlers.OnLogout;
            
            this.ui.Dispose();
            this.startupHandlers.Dispose();
            PluginInterface.Dispose();
        }


        public Plugin()
        {
            _instance = this;
            
            

            if (PluginInterface != null)
                this.configuration = PluginInterface.GetPluginConfig() as Configuration ?? new Configuration();    
            else
                this.configuration = new Configuration();
            
            this.configuration.Initialize(PluginInterface);
            
            this.ui = new PluginUI();
            
            this.startupHandlers = new StartupHandlers();
            this.gameClient = new GameClient();

            RegisterHooks();
            
            if (ClientState.LocalPlayer != null)
                Plugin.Configuration.SetCurrentCharacter(Plugin.ClientState.LocalPlayer.Name.ToString());
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
            CommandManager.AddHandler(
                mainCommandName,
                new CommandInfo(OnCommand)
                    { 
                        HelpMessage = "Open configuration for Startup Commands to perform behaviors upon character login."
                    });
        }


        private void RegisterHooks()
        {
            if (PluginInterface == null)
                return;
            
            RegisterCommandHooks();
            RegisterStartupHooks();
            RegisterUIHooks();
        }


        private void RegisterStartupHooks()
        {
            ClientState.Login += this.startupHandlers.OnLogin;
            ClientState.Logout += this.startupHandlers.OnLogout;
        }


        private void RegisterUIHooks()
        {
            PluginInterface.UiBuilder.Draw += this.ui.Draw;
            PluginInterface.UiBuilder.OpenConfigUi +=
                () =>
                    {
                        if (ClientState.LocalPlayer == null)
                            return;

                        if (Configuration.CurrentCharacter == null)
                        {
                            Configuration.SetCurrentCharacter(ClientState.LocalPlayer.Name.ToString());
                        }
                        
                        this.ui.ConfigWindow.Show();
                    };
        }
    }
}
