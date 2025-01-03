using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;

namespace Nabs.Application.Activities;

public abstract class Activity<TActivityState>
    where TActivityState : class, IActivityState, new()
{
    private readonly List<int> _steps = [];
    private readonly Dictionary<int, (IValidator validator, Func<TActivityState, object> objectToValidate)> _validatorSteps = [];
    private readonly Dictionary<int, IMapper<TActivityState>> _mapperSteps = [];
    private readonly Dictionary<int, IUnitOfWork<TActivityState>> _unitOfWorkSteps = [];
    private readonly Dictionary<int, Func<Task>> _functionSteps = [];
    private readonly Dictionary<int, Action> _actionSteps = [];

    protected TActivityState State { get; } = new();

    public async Task<Result<TActivityState>> ExecuteAsync()
    {
        foreach (var step in _steps)
        {
            if (_validatorSteps.ContainsKey(step))
            {
                var (validator, objectToValidate) = _validatorSteps[step];
                var itemToValidate = objectToValidate.Invoke(State);
                var itemType = itemToValidate.GetType();
                var validationContextType = typeof(ValidationContext<>).MakeGenericType(itemType);
                var validationContext = (IValidationContext)Activator
                    .CreateInstance(validationContextType, itemToValidate)!;

                var validationResult = validator.Validate(validationContext);
                if (!validationResult.IsValid)
                {
                    var result = Result<TActivityState>.Invalid(validationResult.AsErrors());
                    return result;
                }
            }
            else if (_mapperSteps.ContainsKey(step))
            {
                var mapper = _mapperSteps[step];
                mapper.Map(State);
            }
            else if (_unitOfWorkSteps.ContainsKey(step))
            {
                var unitOfWork = _unitOfWorkSteps[step];
                await unitOfWork.RunAsync(State);
            }
            else if (_functionSteps.ContainsKey(step))
            {
                var function = _functionSteps[step];
                await function();
            }
            else if (_actionSteps.ContainsKey(step))
            {
                var action = _actionSteps[step];
                action();
            }
        }

        return State;
    }

    protected void AddValidator<T>(Func<TActivityState, object> action)
        where T : class, IValidator, new()
    {
        var step = _steps.Count + 1;
        _steps.Add(step);
        var instance = Activator.CreateInstance<T>();
        _validatorSteps.Add(step, (instance, action));
    }

    protected void AddMapper<T>()
        where T : class, IMapper<TActivityState>, new()
    {
        var step = _steps.Count + 1;
        _steps.Add(step);
        var instance = Activator.CreateInstance<T>();
        _mapperSteps.Add(step, instance);
    }

    protected void AddUnitOfWork<T>()
        where T : class, IUnitOfWork<TActivityState>
    {
        var type = typeof(T);
        var constructor = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)[0]!;
        var dbContextFactoryParameter = constructor.GetParameters()
           .Single(param => param.ParameterType.IsGenericType &&
                                    param.ParameterType.GetGenericTypeDefinition().Name
                                        .Contains("IDbContextFactory", StringComparison.OrdinalIgnoreCase));

        var dbContextFactoryType = dbContextFactoryParameter.ParameterType;
        // We are going to use the service discovery to get the instance of the DbContextFactory
        var dbContextFactory = IoC.ServiceProvider.GetRequiredService(dbContextFactoryType);

        var step = _steps.Count + 1;
        _steps.Add(step);
        _unitOfWorkSteps.Add(step, (T)Activator.CreateInstance(typeof(T), dbContextFactory)!);
    }

    protected void AddFunction(Func<Task> function)
    {
        var step = _steps.Count + 1;
        _steps.Add(step);
        _functionSteps.Add(step, function);
    }

    protected void AddAction(Action action)
    {
        var step = _steps.Count + 1;
        _steps.Add(step);
        _actionSteps.Add(step, action);
    }
}
