namespace FfxivStartupCommands
{
    using Dalamud.Configuration;
    using Dalamud.Plugin;
    using System;
    
    
    [Serializable]
    public class Configuration : IPluginConfiguration
    {
        [NonSerialized]
        private DalamudPluginInterface pluginInterface;


        #region Properties
        public bool SomePropertyToBeSavedAndWithADefault { get; set; } = true;
        public int Version { get; set; } = 0;
        #endregion


        public void Initialize(DalamudPluginInterface pluginInterface)
        {
            this.pluginInterface = pluginInterface;
        }


        public void Save()
        {
            if (this.pluginInterface != null)
                this.pluginInterface.SavePluginConfig(this);
        }
    }
}
