namespace FfxivStartupCommands
{
    using System;
    using System.Collections.Generic;


    [Serializable]
    public class CharacterConfiguration
    {
        #region Properties
        /// <summary>
        /// Which chat channel should be activated upon character login.
        /// </summary>
        public ChatChannel DefaultChatChannel { get; set; } = ChatChannel.None;
        
        /// <summary>
        /// Custom chat commands to be executed upon successful character login.
        /// </summary>
        public List<CustomCommand> CustomCommands { get; set; } = new List<CustomCommand>();
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

        
        [Serializable]
        public class CustomCommand
        {
            public bool Enabled { get; set; } = true; // TODO
            public string Command { get; set; } = string.Empty;
        }
    }
}