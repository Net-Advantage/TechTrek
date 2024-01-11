using FluentValidation.Results;

namespace Nabs.ActivityFramework.Abstractions;

public interface IActivity;

public abstract class Activity<TActivityState>
    : IActivity
    where TActivityState : class, IActivityState
{
    public TActivityState ActivityState { get; set; } = default!;

    public ValidationResult ValidationResult { get; set; } = default!;

    public async Task RunAsync()
    {
        ActivityState = (TActivityState)Activator.CreateInstance(typeof(TActivityState))!;
        
        await Task.CompletedTask;
    }
}

public abstract class Activity<
    TActivityState, 
    TActivityStateFactory>
    : Activity<TActivityState>
    where TActivityState : class, IActivityState
    where TActivityStateFactory : class, IActivityFeature<TActivityState>
{
    public new async Task RunAsync()
    {
        ActivityState = (TActivityState)Activator.CreateInstance(typeof(TActivityState))!;
        var factory = (TActivityStateFactory)Activator.CreateInstance(typeof(TActivityStateFactory), ActivityState)!;
        await factory.RunAsync();
    }
}

public abstract class Activity<
    TActivityState, 
    TActivityStateFactory,
    TActivityStateCalculator>
    : Activity<TActivityState, TActivityStateFactory>
    where TActivityState : class, IActivityState
    where TActivityStateFactory : class, IActivityStateFactory<TActivityState>
    where TActivityStateCalculator : class, IActivityStateCalculator<TActivityState>
{
    public new async Task RunAsync()
    {
        var factory = (TActivityStateFactory)Activator.CreateInstance(typeof(TActivityStateFactory))!;
        await factory.RunAsync();

        var calculator = (TActivityStateCalculator)Activator.CreateInstance(typeof(TActivityStateCalculator), factory.ActivityState)!;
        ActivityState = calculator.ActivityState;
    }
}

public abstract class Activity<
    TActivityState, 
    TActivityStateFactory,
    TActivityStateCalculator,
    TActivityStateValidator>
    : Activity<TActivityState, TActivityStateFactory, TActivityStateCalculator>
    where TActivityState : class, IActivityState
    where TActivityStateFactory : class, IActivityStateFactory<TActivityState>
    where TActivityStateCalculator : class, IActivityStateCalculator<TActivityState>
    where TActivityStateValidator : class, IActivityStateValidator<TActivityState>
{
    public new async Task RunAsync()
    {
        var factory = (TActivityStateFactory)Activator.CreateInstance(typeof(TActivityStateFactory))!;
        await factory.RunAsync();

        var calculator = (TActivityStateCalculator)Activator.CreateInstance(typeof(TActivityStateCalculator), factory.ActivityState)!;
        await calculator.RunAsync();
        ActivityState = calculator.ActivityState;

        var validator = (TActivityStateValidator)Activator.CreateInstance(typeof(TActivityStateValidator), calculator.ActivityState)!;
        await validator.RunAsync();

        ValidationResult = validator.ValidationResult;

        await Task.CompletedTask;
    }
}