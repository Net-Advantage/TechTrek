using Nabs.Ui.Shell;

namespace Nabs.TechTrek.WebApp.Components.Layout;

public partial class ShellLayoutViewModel : ViewModelBase
{
    private string _headingText = "Loading ...";
    public string HeadingText
    {
        get => _headingText;
        set
        {
            _headingText = value;
            NotifyPropertyChanged(nameof(HeadingText));
        }
    }
}
