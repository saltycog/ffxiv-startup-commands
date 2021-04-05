namespace FfxivStartupCommands
{
    using ImGuiNET;


    public class DebugWindow : UIWindow
    {
        #region Properties
        protected override ImGuiWindowFlags WindowFlags { get; } =
            ImGuiWindowFlags.NoScrollbar 
            | ImGuiWindowFlags.NoScrollWithMouse
            | ImGuiWindowFlags.NoCollapse;

        protected override string WindowTitle { get; } = "Startup Commands Debug";
        #endregion


        protected override void OnDraw()
        {
            if (ImGui.Button("Activate Login Hook"))
            {
                // TODO
            }
            
            if (ImGui.Button("Show Settings"))
            {
                Plugin.UI.ConfigWindow.IsVisible = true;
            }
        }
    }
}