namespace Nabs.ActivityFramework.Abstractions;

public abstract class Workflow<TWorkflowState>(
	TWorkflowState workflowState)
	where TWorkflowState : class, IWorkflowState
{
	public TWorkflowState WorkflowState { get; set; } = workflowState;

	public Dictionary<IActivity, Action<IActivity>?> Activities {get; } = [];

	protected void AddActivity(IActivity activity, Action<IActivity>? action = null)
	{
		Activities.Add(activity, action);
	}

	public async Task RunAsync()
	{
		await ProcessActivitiesAsync();
	}

	public virtual async Task ProcessActivitiesAsync()
	{
		foreach (var activity in Activities)
		{
			await activity.Key.RunAsync();
			activity.Value?.Invoke(activity.Key);
		}
	}
}
