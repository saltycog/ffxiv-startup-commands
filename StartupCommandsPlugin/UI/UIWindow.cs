namespace FfxivStartupCommands
{
    using System.Numerics;
    using ImGuiNET;


    public abstract class UIWindow
    {
        protected bool isVisible;


        #region Properties
        public bool IsVisible
        {
            get { return this.isVisible; }
            set { this.isVisible = value; }
        }

        public virtual Vector2 WindowSize { get; set; } = new Vector2(0, 0);

        protected abstract ImGuiWindowFlags WindowFlags { get; }

        protected abstract string WindowTitle { get; }
        #endregion


        protected abstract void OnDraw();


        public void Draw()
        {
            if (!this.IsVisible)
                return;

            ImGui.SetNextWindowSize(this.WindowSize, ImGuiCond.Always);
            if (ImGui.Begin(
                this.WindowTitle,
                ref this.isVisible, 
                this.WindowFlags))
            {
                OnDraw();
            }
            ImGui.End();
        }


        public virtual void Hide()
        {
            this.IsVisible = false;
        }


        public virtual void Show()
        {
            this.IsVisible = true;
        }
    }
}