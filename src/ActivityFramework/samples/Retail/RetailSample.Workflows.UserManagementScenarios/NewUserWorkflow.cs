using RetailSample.Activities.RegistrationScenario;

namespace RetailSample.Workflows.UserManagementScenarios;

public sealed class NewUserWorkflow : Workflow<NewUserWorkflowState>
{
    public NewUserWorkflow() : base(new NewUserWorkflowState())
    {
        AddActivity(new RegistrationActivity(), ActivityPostProcessor);
    }

    private void ActivityPostProcessor(IActivity activity)
    {
        switch (activity)
        {
            case RegistrationActivity typedActivity:
                PostProcessRegistrationActivity(typedActivity);
                break;

            default:
                throw new NotSupportedException($"Activity type {activity.GetType().Name} is not supported.");
        }
    }

    private void PostProcessRegistrationActivity(RegistrationActivity registrationActivity)
    {
        if (registrationActivity.HasStateChanged)
        {
            ChangedActivityStates.Add(registrationActivity.ActivityState);
        }
    }
}


