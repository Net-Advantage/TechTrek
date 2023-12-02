namespace Nabs.Ui.Shell;

public interface IGlobalPageContext
{
	IObservable<string> PropertyChanged {get;}

	string[] GetPropertyNames();
}