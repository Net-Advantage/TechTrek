using FluentValidation.Results;

namespace Nabs.ActivityFramework.Abstractions;

public interface IActivityStateValidator<TActivityState>
    : IActivityFeature<TActivityState>
    where TActivityState : class, IActivityState
{
    ValidationResult ValidationResult { get; }
}

public abstract class ActivityStateValidator<TActivityState>(
    TActivityState activityState)
    : AbstractValidator<TActivityState>, IActivityStateValidator<TActivityState>
    where TActivityState : class, IActivityState
{
    public ValidationResult ValidationResult { get; private set; } = default!;
    public TActivityState ActivityState { get; protected set; } = activityState;

    public Task RunAsync()
    {
        ValidationResult = Validate(ActivityState);
        return Task.CompletedTask;
    }
}
