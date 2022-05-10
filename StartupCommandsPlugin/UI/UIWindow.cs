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

        protected abstract string WindowTitle { get; }
        #endregion


        protected abstract void OnDraw();


        public void Draw()
        {
            if (!this.IsVisible)
                return;

            ImGui.SetNextWindowSize(new Vector2(520, 480), ImGuiCond.FirstUseEver);
            if (ImGui.Begin(
                this.WindowTitle,
                ref this.isVisible))
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