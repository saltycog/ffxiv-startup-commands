namespace FfxivStartupCommands
{
    using System;
    using FFXIVClientStructs.Component.GUI;


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
                        if (Plugin.Dalamud.ClientState.LocalPlayer != null)
                        {
                            AtkUnitBase* chatLog = (AtkUnitBase*)Plugin.Dalamud.Framework.Gui.GetUiObjectByName("ChatLog", 1);
                            if (chatLog != null) 
                            {
                                if (chatLog->IsVisible == true)
                                {
                                    this.chatReady = true;
                                    OnChatReady();
                                    threadLoop.Stop();
                                    this.WaitingForChatThread = null;
                                }
                            }    
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


        public void RunCommands()
        {
            Plugin.LogToChat("Executing startup commands.");
            foreach (string command in Plugin.Configuration.ChatCommands)
            {
                Plugin.GameClient.ProcessChatBox(command);
            }
        }


        private void OnChatReady()
        {
            RunCommands();
        }
    }
}