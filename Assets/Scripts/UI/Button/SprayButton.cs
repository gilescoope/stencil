namespace FriendlyMonster.Stencil.UI
{
    public class SprayButton : HoldButton
    {
        public StencilManager StencilManager;
        public PreviewRotator PreviewRotator;

        private void Awake()
        {
            RegisterOnDown(StencilManager.PressSpray);
            RegisterOnDown(isJustPressed => PreviewRotator.SetOn());
            RegisterOnUp(PreviewRotator.SetOff);
        }
    }
}