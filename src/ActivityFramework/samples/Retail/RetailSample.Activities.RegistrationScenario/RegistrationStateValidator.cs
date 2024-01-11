using FluentValidation;

namespace RetailSample.Activities.RegistrationScenario;

public sealed class RegistrationStateValidator 
	: ActivityStateValidator<RegistrationActivityState>
{
	public RegistrationStateValidator(RegistrationActivityState activityState) 
		: base(activityState)
	{
		RuleFor(x => x.ProcessedOn)
			.LessThanOrEqualTo((state) => DateTime.UtcNow);
	}

	
}