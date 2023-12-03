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

    private string _username = "anon";

    public string Username
    {
        get { return _username; }
        set { _username = value; }
    }

    private string _displayFullName = "Anon";

    public string DisplayFullName
    {
        get { return _displayFullName; }
        set { _displayFullName = value; }
    }

}
