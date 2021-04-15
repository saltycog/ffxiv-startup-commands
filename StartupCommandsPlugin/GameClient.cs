namespace FfxivStartupCommands
{
    using System;
    using System.Runtime.InteropServices;
    using FFXIVClientStructs.Component.GUI;


    public class GameClient
    {
        private readonly GetUiModuleDelegate getUI;
        private readonly GetChatBoxModuleDelegate getChatBox;
        private readonly IntPtr uiModulePointer;

        private delegate void GetChatBoxModuleDelegate(IntPtr uiModule, IntPtr message, IntPtr unused, byte a4);
        private delegate IntPtr GetUiModuleDelegate(IntPtr basePtr);


        public GameClient()
        {
            if (Plugin.Dalamud == null)
                return;
            
            // Get UI module.
            IntPtr getUiModulePointer = Plugin.Dalamud.TargetModuleScanner.ScanText("E8 ?? ?? ?? ?? 48 83 7F ?? 00 48 8B F0");
            this.uiModulePointer = Plugin.Dalamud.TargetModuleScanner.GetStaticAddressFromSig("48 8B 0D ?? ?? ?? ?? 48 8D 54 24 ?? 48 83 C1 10 E8 ?? ?? ?? ??");
            this.getUI = Marshal.GetDelegateForFunctionPointer<GetUiModuleDelegate>(getUiModulePointer);
            
            // Get chat box module.
            IntPtr chatBoxModulePointer = Plugin.Dalamud.TargetModuleScanner.ScanText("48 89 5C 24 ?? 57 48 83 EC 20 48 8B FA 48 8B D9 45 84 C9");
            this.getChatBox = Marshal.GetDelegateForFunctionPointer<GetChatBoxModuleDelegate>(chatBoxModulePointer);
        }


        /// <summary>
        /// Returns whether the Chat window is visible (and thus interactable). 
        /// </summary>
        public unsafe bool GetChatVisible()
        {
            if (Plugin.Dalamud.ClientState.LocalPlayer != null)
            {
                AtkUnitBase* chatLog = (AtkUnitBase*)Plugin.Dalamud.Framework.Gui.GetUiObjectByName("ChatLog", 1);
                
                if (chatLog != null)
                    return chatLog->IsVisible;
            }

            return false;
        }
        
        
        /// <summary>
        /// Convenience function to change active chat channel.
        /// </summary>
        public void ChangeChatChannel()
        {
            if (Plugin.Configuration.DefaultChatChannel == ChatChannel.None)
                return;
            
            Plugin.GameClient.SubmitToChat(Plugin.Configuration.DefaultChatChannel.ToCommand());
        }


        /// <summary>
        /// Submit text/command to outgoing chat.
        /// Can be used to enter chat commands.
        /// </summary>
        /// <param name="text">Text to submit.</param>
        public void SubmitToChat(string text) 
        {
            IntPtr uiModule = this.getUI(Marshal.ReadIntPtr(this.uiModulePointer));

            using (ChatPayload payload = new ChatPayload(text))
            {
                IntPtr mem1 = Marshal.AllocHGlobal(400);
                Marshal.StructureToPtr(payload, mem1, false);

                this.getChatBox(uiModule, mem1, IntPtr.Zero, 0);

                Marshal.FreeHGlobal(mem1);    
            }
        }
    }

    
}