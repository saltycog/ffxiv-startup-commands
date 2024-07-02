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
        private IDalamudPluginInterface pluginInterface;

        [NonSerialized]
        private string currentCharacter;

        public Dictionary<string, CharacterConfiguration> CharacterConfigurations { get; set; } =
            new Dictionary<string, CharacterConfiguration>();

        public string CurrentCharacter
        {
            get { return this.currentCharacter; }
        }

        /// <summary>
        /// Custom chat commands to be executed upon successful character login.
        /// </summary>
        public List<CharacterConfiguration.CustomCommand> CustomCommands
        {
            get
            {
                if (this.currentCharacter != null)
                    return GetCurrentCharacterConfiguration().CustomCommands;
                return null;
            }

            set
            {
                if (this.currentCharacter != null)
                    GetCurrentCharacterConfiguration().CustomCommands = value;
            }
        }

        /// <summary>
        /// Which chat channel should be activated upon character login.
        /// </summary>
        public ChatChannel DefaultChatChannel
        {
            get
            {
                if (this.currentCharacter != null)
                    return GetCurrentCharacterConfiguration().DefaultChatChannel;
                return ChatChannel.None;
            }

            set
            {
                if (this.currentCharacter != null)
                    GetCurrentCharacterConfiguration().DefaultChatChannel = value;
            }
        }

        public bool HasCommands
        {
            get
            {
                return
                    this.DefaultChatChannel != ChatChannel.None
                    || this.CustomCommands.Count > 0;
            }
        }

        public int Version { get; set; } = 1;


        public void Initialize(IDalamudPluginInterface pluginInterface)
        {
            this.pluginInterface = pluginInterface;
        }


        public void Save()
        {
            if (this.pluginInterface != null)
                this.pluginInterface.SavePluginConfig(this);
        }


        public void SetCurrentCharacter(string currentCharacter)
        {
            this.currentCharacter = currentCharacter;
        }


        private CharacterConfiguration GetCurrentCharacterConfiguration()
        {
            if (this.currentCharacter == null)
                return null;

            if (!this.CharacterConfigurations.ContainsKey(this.currentCharacter))
            {
                this.CharacterConfigurations.Add(this.currentCharacter, new CharacterConfiguration());
                this.CharacterConfigurations[this.currentCharacter].CustomCommands =
                    new List<CharacterConfiguration.CustomCommand>();
            }

            return this.CharacterConfigurations[this.currentCharacter];
        }
    }
}
