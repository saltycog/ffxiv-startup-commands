namespace FfxivStartupCommands.Test
{
    using ImGuiScene;
    using System;
    
    
    internal interface IPluginUIMock : IDisposable
    {
        void Initialize(SimpleImGuiScene scene);
    }
}
