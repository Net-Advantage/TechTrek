namespace Nabs.Ui.Shell;

public abstract class ViewModelBase : IViewModel
{
    private readonly Subject<string> _propertyChanged = new();

    public IObservable<string> PropertyChanged => _propertyChanged
        .AsObservable();

    public string[] GetPropertyNames()
    {
        return GetType()
            .GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public)
            .Select(p => p.Name)
            .ToArray();
    }

    protected void NotifyPropertyChanged(string propertyName)
    {
        _propertyChanged.OnNext(propertyName);
    }
}
