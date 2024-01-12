namespace Nabs.ActivityFramework.Abstractions;

public interface IWorkflowState
{
    Guid Id { get; set; }
}

public abstract class WorkflowState : IWorkflowState
{
    public Guid Id { get; set; }
}
