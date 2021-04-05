namespace FfxivStartupCommands
{
    using System.Collections.Generic;
    using System.Numerics;
    using ImGuiNET;


    public class ConfigWindow : UIWindow
    {
        #region Properties
        public override Vector2 WindowSize { get; set; } = new Vector2(300, 500);


        protected override ImGuiWindowFlags WindowFlags { get; } =
            ImGuiWindowFlags.NoScrollbar 
            | ImGuiWindowFlags.NoScrollWithMouse
            | ImGuiWindowFlags.NoCollapse;


        protected override string WindowTitle { get; } = "Startup Commands Configuration";
        #endregion


        protected override void OnDraw()
        {
            // NOTE: Can't ref config properties, so always create a local copy first for UI.
            List<string> chatCommands = Plugin.Configuration.ChatCommands;

            ImGui.Text("Chat Commands:");
            ImGui.SameLine();
            if (ImGui.Button("+"))
            {
                Plugin.Configuration.ChatCommands.Add(string.Empty);
            }
            
            for (int i = 0; i < Plugin.Configuration.ChatCommands.Count; i++)
            {
                string chatCommand = Plugin.Configuration.ChatCommands[i];
                if (ImGui.InputText("", ref chatCommand, 20))
                {
                    Plugin.Configuration.ChatCommands[i] = chatCommand;
                }
                ImGui.SameLine();
                if (ImGui.Button("-"))
                {
                    Plugin.Configuration.ChatCommands.Remove(chatCommand);
                }
            }

            ImGui.NewLine();
            ImGui.NewLine();
            ImGui.NewLine();
            ImGui.NewLine();
            
            if (ImGui.Button("Test"))
            {
                Plugin.StartupHandlers.RunCommands();
            }
            
            ImGui.SameLine();
            
            if (ImGui.Button("Save"))
            {
                Plugin.Configuration.Save();
            }
        }
    }
}