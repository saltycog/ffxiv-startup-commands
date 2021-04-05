namespace FfxivStartupCommands
{
    using Dalamud.Configuration;
    using Dalamud.Plugin;
    using System;
    using System.Collections.Generic;


    [Serializable]
    public class Configuration : IPluginConfiguration
    {
        [NonSerialized]
        private DalamudPluginInterface pluginInterface;


        #region Properties
        /// <summary>
        /// Arbitrary chat commands to be executed upon successful character login.
        /// </summary>
        public List<string> ChatCommands { get; set; } = new List<string>();
        
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
