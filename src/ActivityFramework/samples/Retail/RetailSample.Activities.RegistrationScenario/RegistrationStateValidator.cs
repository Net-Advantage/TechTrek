using FluentValidation;

namespace RetailSample.Activities.RegistrationScenario;

public sealed class RegistrationStateValidator 
	: ActivityStateValidator<RegistrationActivityState>
{
	public RegistrationStateValidator() 
	{
		RuleFor(x => x.Id)
			.NotEmpty();

		RuleFor(x => x.Username)
			.EmailAddress();

		RuleFor(x => x.FirstName)
			.NotEmpty();

		RuleFor(x => x.LastName)
			.NotEmpty();

		RuleFor(x => x.ProcessedOn)
			.LessThanOrEqualTo((state) => DateTime.UtcNow);
	}
}

public static partial class DefaultValidatorExtensions
{

}