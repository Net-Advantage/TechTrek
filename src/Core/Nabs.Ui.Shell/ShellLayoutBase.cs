namespace Nabs.Ui.Shell;

public abstract class ShellLayoutBase<TViewModel> : LayoutComponentBase
    where TViewModel : IViewModel
{
    private IDisposable? _subscription;
    private string[]? _propertyNames;

    [Inject]
    private TViewModel ViewModel { get; set; } = default!;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        SetupReaction();
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        SetupReaction();
    }

    private void SetupReaction()
    {
        if (_subscription is not null)
        {
            return;
        }

        _propertyNames ??= ViewModel.GetPropertyNames();

        _subscription = ViewModel.PropertyChanged.Subscribe(async propertyName =>
        {
            if (_propertyNames.Contains(propertyName))
            {
                await InvokeAsync(() =>
                {
                    Console.WriteLine($"Property Name: {propertyName}");
                    StateHasChanged();
                });
            }
        });
    }

    public void Dispose()
    {
        _subscription?.Dispose();
    }
}
