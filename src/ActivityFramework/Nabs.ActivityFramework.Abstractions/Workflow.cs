namespace Nabs.ActivityFramework.Abstractions;

public abstract class Workflow<TWorkflowState>(
    TWorkflowState workflowState)
    where TWorkflowState : class, IWorkflowState
{
    public TWorkflowState WorkflowState { get; set; } = workflowState;
    public List<IActivityState> ChangedActivityStates { get; } = [];

    public Dictionary<IActivity, Action<IActivity>?> Activities { get; } = [];

    public bool Processed { get; private set; }

    public ValidationResult ValidationResult
    {
        get
        {
            if(!Processed)
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

    protected void AddActivity(IActivity activity, Action<IActivity>? action = null)
    {
        Activities.Add(activity, action);
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
            activity.Value?.Invoke(activity.Key);
        }
    }
}
