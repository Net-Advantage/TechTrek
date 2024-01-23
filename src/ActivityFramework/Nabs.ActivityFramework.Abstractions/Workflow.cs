namespace Nabs.ActivityFramework.Abstractions;

public abstract class Workflow<TWorkflowState>
	where TWorkflowState : class, IWorkflowState
{
	public TWorkflowState? WorkflowState { get; set; }
	public Dictionary<IActivity, Delegate?> Activities { get; } = [];

	public bool Processed { get; private set; }

	public ValidationResult ValidationResult
	{
		get
		{
			if (!Processed)
			{
				return null!;
			}

			var allFailures = Activities
					   .Where(x => x.Key.ValidationResult != null)
					   .SelectMany(x => x.Key.ValidationResult.Errors)
					   .ToList();

			return new ValidationResult(allFailures);
		}
	}

	protected void AddActivity<TActivity>(Action<TActivity>? action = null)
		where TActivity : class, IActivity, new()
	{
		var activity = new TActivity();
		Activities.Add(activity, action);
	}

	protected void AddActivity<TActivity>(IActivity activity, Action<TActivity>? action = null)
		where TActivity : class, IActivity
	{
		Activities.Add(activity, action);
	}

	protected virtual Task OnDataLoadAsync()
	{
		return Task.CompletedTask;
	}

	protected virtual Task OnDataPersistAsync()
	{
		return Task.CompletedTask;
	}

	public async Task RunAsync()
	{
		Processed = false;
		await ProcessActivitiesAsync();
		Processed = true;
	}

	public virtual async Task ProcessActivitiesAsync()
	{
		foreach (var activity in Activities)
		{
			await activity.Key.RunAsync();
			activity.Value?.DynamicInvoke(activity.Key);
		}
	}
}
