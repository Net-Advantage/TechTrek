namespace Nabs.Application.Activities;

public interface IUnitOfWork
{
    Task Run<TActivityState>(TActivityState activityState);
}

public interface IUnitOfWork<TActivityState>
    where TActivityState : class, IActivityState, new()
{
    Task RunAsync(TActivityState activityState);
}

