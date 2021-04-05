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
        /// Which chat channel should be activated upon character login.
        /// </summary>
        public ChatChannel DefaultChatChannel { get; set; } = ChatChannel.None;
        
        /// <summary>
        /// Custom chat commands to be executed upon successful character login.
        /// </summary>
        public List<CustomCommand> CustomCommands { get; set; } = new List<CustomCommand>();
        
        public int Version { get; set; } = 0;
        #endregion


        public bool HasCommands
        {
            get
            {
                return
                    this.DefaultChatChannel != ChatChannel.None
                    || this.CustomCommands.Count > 0;
            }
        }

        public void Initialize(DalamudPluginInterface pluginInterface)
        {
            this.pluginInterface = pluginInterface;
        }


        public void Save()
        {
            if (this.pluginInterface != null)
                this.pluginInterface.SavePluginConfig(this);
        }


        public class CustomCommand
        {
            public bool Enabled { get; set; } = true; // TODO
            public string Command { get; set; } = string.Empty;
        }
    }
}
