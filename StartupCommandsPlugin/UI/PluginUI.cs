namespace FfxivStartupCommands
{
    using ImGuiNET;
    using System;
    using System.Numerics;
    
    
    // It is good to have this be disposable in general, in case you ever need it
    // to do any cleanup
    public class PluginUI : IDisposable
    {
        #region Properties
        public ConfigWindow ConfigWindow { get; } = new ConfigWindow();
        #endregion


        public void Dispose()
        {
        }


        public void Draw()
        {
            this.ConfigWindow.Draw();
        }
    }
}
