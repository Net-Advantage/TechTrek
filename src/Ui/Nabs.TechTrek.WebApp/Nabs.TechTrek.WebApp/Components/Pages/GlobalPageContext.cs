using Nabs.Ui.Shell;

namespace Nabs.TechTrek.WebApp.Components.Pages;

public partial class GlobalPageContext : GlobalPageContextBase
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
