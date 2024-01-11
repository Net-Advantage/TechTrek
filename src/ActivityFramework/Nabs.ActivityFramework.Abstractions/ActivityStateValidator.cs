using FluentValidation.Results;

namespace Nabs.ActivityFramework.Abstractions;

public abstract class ActivityStateValidator<TActivityState> : AbstractValidator<TActivityState>
    where TActivityState : IActivityState
{

}
