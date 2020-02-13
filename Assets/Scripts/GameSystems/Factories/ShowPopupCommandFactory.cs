using Zenject;

public class ShowPopupCommandFactory<T> where T : PopupContainerBase
{
    [Inject]
    private readonly PopupSystemContainer _popupSystemContainer;

    public ShowPopupCommand<T> Create()
    {
        return new ShowPopupCommand<T>(_popupSystemContainer);
    }
}