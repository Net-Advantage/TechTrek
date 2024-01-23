
namespace RetailSample.Workflows.UserManagementScenarios;

public sealed class NewUserWorkflowParameters : IWorkflowParameters
{
    public Guid UserId { get; set; }
}
