namespace Nabs.ActivityFramework.Abstractions;

public interface IActivity
{
    ValidationResult ValidationResult { get; }
    Task RunAsync();
}

public interface IActivity<TActivityState>
    : IActivity
    where TActivityState : class, IActivityState;

/// <summary>
/// An activity that holds the state based on TActivityState.
/// An activity that simply creates and instance of the state defined by TActivityState.
/// It seems useless to have this, but I know we are going to use it - you'll see!
/// </summary>
/// <typeparam name="TActivityState"></typeparam>
public abstract class Activity<
    TActivityState>
    : IActivity<TActivityState>
    where TActivityState : class, IActivityState
{

    public TActivityState? InitialActivityState { get; protected set; }
    public TActivityState ActivityState { get; set; } = default!;

    public bool HasStateChanged => InitialActivityState != ActivityState;

    public ValidationResult ValidationResult { get; set; } = default!;

    public virtual async Task RunAsync()
    {
        if (InitialActivityState is null || ActivityState is null)
        {
            InitialActivityState = (TActivityState)Activator.CreateInstance(typeof(TActivityState))!;
            ActivityState ??= InitialActivityState;
        }

        await Task.CompletedTask;
    }
}

/// <summary>
/// An activity that holds the state based on TActivityState.
/// It will retrieve or create state base on the implementation of the TActivityStateFactory.
/// </summary>
/// <typeparam name="TActivityState"></typeparam>
/// <typeparam name="TActivityStateFactory"></typeparam>
public abstract class Activity<
    TActivityState,
    TActivityStateFactory,
    TActivityStateValidator>
    : Activity<TActivityState>
    where TActivityState : class, IActivityState
    where TActivityStateFactory : class, IActivityStateInitialiser<TActivityState>
    where TActivityStateValidator : class, IActivityStateValidator<TActivityState>
{
    public Activity()
    {

    }

    protected Dictionary<IActivityStateBehaviour<TActivityState>, Action?> Behaviours { get; } = [];

    protected void AddBehaviour(IActivityStateBehaviour<TActivityState> behaviour, Action? action = null)
    {
        Behaviours.Add(behaviour, action);
    }

    public void InitialiseState(TActivityState activityState)
    {
        InitialActivityState = activityState;
        ActivityState = InitialActivityState;
    }

    public sealed override async Task RunAsync()
    {
        if (InitialActivityState is null)
        {
            var factory = (TActivityStateFactory)Activator.CreateInstance(typeof(TActivityStateFactory))!;
            InitialActivityState = await factory.RunAsync();

            ActivityState ??= InitialActivityState;
        }

        if (Behaviours.Count > 0)
        {
            await ProcessBehaviours();
        }

        var validator = (TActivityStateValidator)Activator.CreateInstance(typeof(TActivityStateValidator))!;
        ValidationResult = validator.Run(ActivityState);
    }
    public virtual async Task ProcessBehaviours()
    {
        if(ActivityState is null)
        {
            return;
        }

        foreach (var behaviour in Behaviours)
        {
            ActivityState = await behaviour.Key.RunAsync(ActivityState);
            if (behaviour.Value is not null)
            {
                behaviour.Value();
            }
        }
    }
}
