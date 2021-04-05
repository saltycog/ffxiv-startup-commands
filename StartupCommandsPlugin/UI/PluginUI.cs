namespace FfxivStartupCommands
{
    using ImGuiNET;
    using System;
    using System.Numerics;
    
    
    // It is good to have this be disposable in general, in case you ever need it
    // to do any cleanup
    public class PluginUI : IDisposable
    {
        public Plugin Plugin { get; set; }
        
        public Configuration Configuration
        {
            get { return Plugin.Configuration; }
        }
        

        #region Constructors
        // passing in the image here just for simplicity
        public PluginUI(Plugin plugin)
        {
            this.Plugin = plugin;
            
            this.ConfigWindow = new ConfigWindow();
            this.DebugWindow = new DebugWindow();
        }
        #endregion


        #region Properties
        public ConfigWindow ConfigWindow { get; }

        public DebugWindow DebugWindow { get; }
        #endregion


        public void Dispose()
        {
        }


        public void Draw()
        {
            this.ConfigWindow.Draw();
            this.DebugWindow.Draw();
        }
    }
}
