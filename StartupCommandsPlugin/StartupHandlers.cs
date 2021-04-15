namespace FfxivStartupCommands
{
    using System;


    public class StartupHandlers : IDisposable
    {
        private bool chatReady;
        private ThreadLoop waitingForChatThread = null;
        private object waitingForChatLock = new object();


        #region Properties
        public bool ChatReady
        {
            get { return this.chatReady; }
            set
            {
                lock (this.waitingForChatLock)
                {
                    this.chatReady = value;    
                }
            }
        }

        private ThreadLoop WaitingForChatThread
        {
            get { return this.waitingForChatThread; }
            set
            {
                lock (this.waitingForChatLock)
                {
                    this.waitingForChatThread = value;    
                }
            }
        }
        #endregion


        public void Dispose()
        {
            if (this.WaitingForChatThread != null)
            {
                this.WaitingForChatThread.Stop();
                this.WaitingForChatThread = null;
            }
        }


        /// <summary>
        /// Executed upon successful character login.
        /// </summary>
        public unsafe void OnLogin(object sender, EventArgs args)
        {
            if (this.chatReady
                || this.WaitingForChatThread != null)
            {
                return;
            }

            this.WaitingForChatThread = ThreadLoop.Start(
                action: (threadLoop) =>
                    {
                        if (Plugin.GameClient.GetChatVisible())
                        {
                            this.chatReady = true;
                            OnChatReady();
                            threadLoop.Stop();
                            this.WaitingForChatThread = null;
                        }
                    }, interval: 250);
        }


        public void OnLogout(object sender, EventArgs args)
        {
            this.chatReady = false;
            if (this.WaitingForChatThread != null)
            {
                this.WaitingForChatThread.Stop();
                this.WaitingForChatThread = null;
            }
        }


        public void RunStartupBehaviors()
        {
            if (!Plugin.Configuration.HasCommands)
                return;
            
            Plugin.LogToChat("Performing startup behaviors.");
            
            Plugin.GameClient.ChangeChatChannel();
            RunCustomCommands();
        }


        private static void RunCustomCommands()
        {
            foreach (Configuration.CustomCommand customCommand in Plugin.Configuration.CustomCommands)
            {
                Plugin.GameClient.SubmitToChat(customCommand.Command);
            }
        }


        private void OnChatReady()
        {
            RunStartupBehaviors();
        }
    }
}