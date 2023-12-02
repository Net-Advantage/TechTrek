namespace Nabs.Ui.Shell;

public abstract class ShellBase<TGlobalPageContext> : LayoutComponentBase
    where TGlobalPageContext : IGlobalPageContext
{
    private IDisposable? _subscription;
    private string[]? _propertyNames;

    [Inject]
    private IGlobalPageContext TheGlobalPageContext { get; set; } = default!;

    public TGlobalPageContext GlobalPageContext
    {
        get
        {
            return (TGlobalPageContext)TheGlobalPageContext;
        }
        set
        {
            _ = value;
        }
    }

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

        _propertyNames ??= GlobalPageContext.GetPropertyNames();

        _subscription = GlobalPageContext.PropertyChanged.Subscribe(async propertyName =>
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
