public class ShowPopupCommand<T> : SynchronousCommand where T : PopupContainerBase
{
    private readonly PopupSystemContainer _popupSystemContainer;

    public ShowPopupCommand(PopupSystemContainer popupSystemContainer)
    {
        _popupSystemContainer = popupSystemContainer;
    }

    public override void Execute()
    {
        _popupSystemContainer.ShowPopup<T>();
    }
}