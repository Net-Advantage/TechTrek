using FluentValidation.Results;

namespace Nabs.ActivityFramework.Abstractions;

public interface IActivityStateValidator<TActivityState>
    where TActivityState : class, IActivityState
{
    ValidationResult Run(TActivityState activityState);
}

public abstract class ActivityStateValidator<TActivityState>()
    : AbstractValidator<TActivityState>, IActivityStateValidator<TActivityState>
    where TActivityState : class, IActivityState
{
    public ValidationResult Run(TActivityState activityState)
    {
        return Validate(activityState);
    }
}
