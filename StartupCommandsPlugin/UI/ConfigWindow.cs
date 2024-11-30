namespace FfxivStartupCommands
{
    using System;
    using System.Numerics;
    using ImGuiNET;


    public class ConfigWindow : UIWindow
    {
        #region Properties
        protected override string WindowTitle { get; } = "Startup Commands Configuration";
        #endregion


        protected override void OnDraw()
        {
            // NOTE: Can't ref config properties, so always create a local copy first for UI.
            
            DrawChatChannelSelection();

            DrawCustomCommands();

            DrawExecuteButton();
        }


        private static void DrawExecuteButton()
        {
            if (ImGui.Button("Execute Now"))
            {
                Plugin.StartupHandlers.RunStartupBehaviors();
            }
            if (ImGui.IsItemHovered())
            {
                ImGui.SetTooltip("Run all startup functions now.");
            }
        }


        private void DrawCustomCommands()
        {
            ImGui.Separator();
            ImGui.Text("Custom Startup Commands:");
            if (ImGui.IsItemHovered())
            {
                ImGui.SetTooltip("Custom chat commands to execute on startup.");
            }
            
            ImGui.SameLine();
            if (ImGui.Button("Add"))
            {
                Plugin.Configuration.CustomCommands.Add(new CharacterConfiguration.CustomCommand());
            }
            if (ImGui.IsItemHovered())
            {
                ImGui.SetTooltip("Add a new custom startup command.");
            }

            bool updated = false;
            CharacterConfiguration.CustomCommand commandToRemove = null;
            int index = 0;
            foreach (CharacterConfiguration.CustomCommand customCommand in Plugin.Configuration.CustomCommands)
            {
                string currentChatCommand = customCommand.Command;
                ImGui.Bullet();
                ImGui.SameLine();

                updated = ImGui.InputText($"###inputText_Command_{index}", ref currentChatCommand, 512);

                ImGui.SameLine();
                if (ImGui.Button($"Remove###button_Remove{index}"))
                {
                    commandToRemove = customCommand;
                }

                if (updated)
                {
                    Plugin.Configuration.CustomCommands[index].Command = currentChatCommand;
                    Plugin.Configuration.Save();
                }

                index++;
            }

            if (commandToRemove != null)
            {
                Plugin.Configuration.CustomCommands.Remove(commandToRemove);
                Plugin.Configuration.Save();
            }
        }


        private static void DrawChatChannelSelection()
        {
            string defaultChatChannelName = Plugin.Configuration.DefaultChatChannel.ToName();

            if (ImGui.BeginCombo("Default Chat Channel", defaultChatChannelName))
            {
                foreach (ChatChannel channel in Enum.GetValues(typeof(ChatChannel)))
                {
                    string channelName = channel.ToName();
                    bool selected = channelName == defaultChatChannelName;
                    if (ImGui.Selectable(channelName, selected))
                        Plugin.Configuration.DefaultChatChannel = channel;
                    if (selected)
                    {
                        ImGui.SetItemDefaultFocus();
                    }
                }

                ImGui.EndCombo();
                Plugin.Configuration.Save();
            }

            if (ImGui.IsItemHovered())
            {
                ImGui.SetTooltip("In-game chat channel to switch to upon character login. Use None to disable.");
            }
        }
    }
}