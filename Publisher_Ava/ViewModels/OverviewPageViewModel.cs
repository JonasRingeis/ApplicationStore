using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Publisher_Ava.ViewModels;

public partial class OverviewPageViewModel : PageViewModelBase
{
    public override bool CanNavigatePrevious { get; protected set; }

    [ObservableProperty]
    private List<string> _publishedApps = ["Chrome", "Firefox"];
}