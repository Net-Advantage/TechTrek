using FluentValidation.Results;

namespace Nabs.ActivityFramework.Abstractions;

public abstract class Activity<TActivityState>
    : IActivity
    where TActivityState : IActivityState, new()
{
    public Activity()
    {
        FeatureBuilder = new(this);
    }

    public ActivityFeatureBuilder<TActivityState> FeatureBuilder { get; set; }
    public ValidationResult ValidationResult { get; set; } = default!;

    public async Task RunAsync()
    {
        var featureBuilder = FeatureBuilder;
        await featureBuilder.Factory.RunAsync();
        await featureBuilder.Calculator.RunAsync(featureBuilder.Factory.ActivityState);
        ValidationResult = featureBuilder.Validator.Validate(featureBuilder.Calculator.ActivityState);


    }
}

public sealed class ActivityFeatureBuilder<TActivityState>
    where TActivityState : IActivityState, new()
{
    private readonly Activity<TActivityState> _activity;
    private TActivityState _activityState = default!;

    public ActivityFeatureBuilder(Activity<TActivityState> activity)
    {
        _activity = activity;
    }

    public ActivityStateFactory<TActivityState> Factory { get; private set; } = default!;
    public ActivityStateCalculator<TActivityState> Calculator { get; private set; } = default!;
    public ActivityStateValidator<TActivityState> Validator { get; private set; } = default!;

    public TActivityState ActivityState => _activityState;

    public void AutoAddFeatures()
    {
        //TODO: DWS: Add all the feature by scanning this assembly.
    }

    public ActivityFeatureBuilder<TActivityState> AddFactory<TStateFactory>()
        where TStateFactory : ActivityStateFactory<TActivityState>, new()
    {
        Factory = new TStateFactory();
        return this;
    }

    public ActivityFeatureBuilder<TActivityState> AddCalculator<TStateCalculator>()
        where TStateCalculator : ActivityStateCalculator<TActivityState>, new()
    {
        Calculator = new TStateCalculator();
        return this;
    }

    public ActivityFeatureBuilder<TActivityState> AddValidator<TStateValidator>()
        where TStateValidator : ActivityStateValidator<TActivityState>, new()
    {
        Validator = new TStateValidator();
        return this;
    }

}