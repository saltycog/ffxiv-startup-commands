namespace FfxivStartupCommands
{
    using ImGuiNET;
    using System;
    using System.Numerics;
    
    
    // It is good to have this be disposable in general, in case you ever need it
    // to do any cleanup
    public class PluginUI : IDisposable
    {
        private Configuration configuration;
        private bool visible = false;
        private bool settingsVisible = false;


        #region Constructors
        // passing in the image here just for simplicity
        public PluginUI(Configuration configuration)
        {
            this.configuration = configuration;
        }
        #endregion


        #region Properties
        public bool SettingsVisible
        {
            get { return this.settingsVisible; }
            set { this.settingsVisible = value; }
        }


        public bool Visible
        {
            get { return this.visible; }
            set { this.visible = value; }
        }
        #endregion


        public void Dispose()
        {
        }


        public void Draw()
        {
            DrawMainWindow();
            DrawSettingsWindow();
        }


        private void DrawMainWindow()
        {
            if (!this.Visible)
            {
                return;
            }

            ImGui.SetNextWindowSize(new Vector2(375, 330), ImGuiCond.FirstUseEver);
            ImGui.SetNextWindowSizeConstraints(new Vector2(375, 330), new Vector2(float.MaxValue, float.MaxValue));
            if (ImGui.Begin("My Amazing Window", ref this.visible, ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse))
            {
                ImGui.Text($"The random config bool is {this.configuration.SomePropertyToBeSavedAndWithADefault}");

                if (ImGui.Button("Show Settings"))
                {
                    this.SettingsVisible = true;
                }

                ImGui.Spacing();

                ImGui.Text("Placeholder");
                ImGui.Indent(55);
                ImGui.Unindent(55);
            }
            ImGui.End();
        }


        private void DrawSettingsWindow()
        {
            if (!this.SettingsVisible)
            {
                return;
            }

            ImGui.SetNextWindowSize(new Vector2(232, 75), ImGuiCond.Always);
            if (ImGui.Begin("A Wonderful Configuration Window", ref this.settingsVisible,
                ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse))
            {
                // can't ref a property, so use a local copy
                bool configValue = this.configuration.SomePropertyToBeSavedAndWithADefault;
                if (ImGui.Checkbox("Random Config Bool", ref configValue))
                {
                    this.configuration.SomePropertyToBeSavedAndWithADefault = configValue;
                    // can save immediately on change, if you don't want to provide a "Save and Close" button
                    this.configuration.Save();
                }
            }
            ImGui.End();
        }
    }
}
