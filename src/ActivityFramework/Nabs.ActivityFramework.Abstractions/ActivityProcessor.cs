namespace Nabs.ActivityFramework.Abstractions;

public abstract class ActivityProcessor<TActivity>
    : IActivityProcessor
    where TActivity : IActivity
{
    public List<IActivityFeature> ActivityFeatures { get; } = [];

    public async Task Process()
    {
        foreach (var activityFeature in ActivityFeatures)
        {
            await activityFeature.RunAsync();
        }
    }
}
