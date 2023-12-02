namespace Nabs.Ui.Shell;

public interface IViewModel
{
	IObservable<string> PropertyChanged {get;}

	string[] GetPropertyNames();
}