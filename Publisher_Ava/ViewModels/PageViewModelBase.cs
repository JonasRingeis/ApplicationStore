namespace Publisher_Ava.ViewModels;

public abstract class PageViewModelBase : ViewModelBase
{
    public abstract bool CanNavigatePrevious { get; protected set; }
}