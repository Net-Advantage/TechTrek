using Nabs.ActivityFramework.Abstractions;

namespace Nabs.ActivityFramework;

public abstract class WorkflowOrchestrator
{
    public List<IActivity> Activities {get; } = [];

}
