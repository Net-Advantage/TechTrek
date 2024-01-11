using Nabs.ActivityFramework.Abstractions;

namespace Nabs.ActivityFramework;

public sealed class ActivityOrchestrator
{
    public List<IActivity> Activities {get; } = [];

}
