using Nabs.TechTrek.Persistence.Entities;

namespace RetailSample.Workflows.UserManagementScenarios;

public sealed class NewUserWorkflowState : WorkflowState
{
    public Guid UserId { get; set; }

    public UserEntity? User { get; set; }
}