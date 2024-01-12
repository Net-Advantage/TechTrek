using FluentValidation.Results;

namespace Nabs.ActivityFramework.Abstractions;

public interface IActivityStateValidator<TActivityState>
    where TActivityState : class, IActivityState
{
    ValidationResult Run(TActivityState? activityState);
}

public abstract class ActivityStateValidator<TActivityState>()
    : AbstractValidator<TActivityState>, IActivityStateValidator<TActivityState>
    where TActivityState : class, IActivityState
{
    public ValidationResult Run(TActivityState? activityState)
    {
        if(activityState is null)
        {
            return new ValidationResult(new List<ValidationFailure>()
            {
                new("ActivityState", $"ActivityState: {typeof(TActivityState)} cannot be null.")
            });
        }

        return Validate(activityState);
    }
}
