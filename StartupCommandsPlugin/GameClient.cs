namespace FfxivStartupCommands
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;
    using System.Text;


    public class GameClient
    {
        private readonly GetUiModuleDelegate getUiModule;
        private readonly EasierProcessChatBoxDelegate easierProcessChatBox;
        private readonly IntPtr uiModulePtr;

        private delegate void EasierProcessChatBoxDelegate(IntPtr uiModule, IntPtr message, IntPtr unused, byte a4);
        private delegate IntPtr GetUiModuleDelegate(IntPtr basePtr);


        public GameClient() 
        {
            IntPtr getUiModulePtr = Plugin.Dalamud.TargetModuleScanner.ScanText("E8 ?? ?? ?? ?? 48 83 7F ?? 00 48 8B F0");
            IntPtr easierProcessChatBoxPtr = Plugin.Dalamud.TargetModuleScanner.ScanText("48 89 5C 24 ?? 57 48 83 EC 20 48 8B FA 48 8B D9 45 84 C9");
            this.uiModulePtr = Plugin.Dalamud.TargetModuleScanner.GetStaticAddressFromSig("48 8B 0D ?? ?? ?? ?? 48 8D 54 24 ?? 48 83 C1 10 E8 ?? ?? ?? ??");

            this.getUiModule = Marshal.GetDelegateForFunctionPointer<GetUiModuleDelegate>(getUiModulePtr);
            this.easierProcessChatBox = Marshal.GetDelegateForFunctionPointer<EasierProcessChatBoxDelegate>(easierProcessChatBoxPtr);
        }


        public void ProcessChatBox(string message) 
        {
            IntPtr uiModule = this.getUiModule(Marshal.ReadIntPtr(this.uiModulePtr));

            using (ChatPayload payload = new ChatPayload(message))
            {
                IntPtr mem1 = Marshal.AllocHGlobal(400);
                Marshal.StructureToPtr(payload, mem1, false);

                this.easierProcessChatBox(uiModule, mem1, IntPtr.Zero, 0);

                Marshal.FreeHGlobal(mem1);    
            }
        }
    }

    [StructLayout(LayoutKind.Explicit)]
    [SuppressMessage("ReSharper", "PrivateFieldCanBeConvertedToLocalVariable")]
    internal readonly struct ChatPayload : IDisposable 
    {
        [FieldOffset(0)]
        private readonly IntPtr textPtr;

        [FieldOffset(16)]
        private readonly ulong textLen;

        [FieldOffset(8)]
        private readonly ulong unk1;

        [FieldOffset(24)]
        private readonly ulong unk2;

        internal ChatPayload(string text) 
        {
            byte[] stringBytes = Encoding.UTF8.GetBytes(text);
            this.textPtr = Marshal.AllocHGlobal(stringBytes.Length + 30);
            Marshal.Copy(stringBytes, 0, this.textPtr, stringBytes.Length);
            Marshal.WriteByte(this.textPtr + stringBytes.Length, 0);

            this.textLen = (ulong) (stringBytes.Length + 1);

            this.unk1 = 64;
            this.unk2 = 0;
        }

        public void Dispose() 
        {
            Marshal.FreeHGlobal(this.textPtr);
        }
    }
}