using FluentValidation.Results;

namespace Nabs.Application.Activities;

public interface IActivityState 
{
    List<ValidationResult> ValidationResults { get; }
}

public interface IActivityState<TRequestDto, TResponseDto> 
    : IActivityState
    where TRequestDto : class
    where TResponseDto : class
{
    Request<TRequestDto>? Request { get; set; }
    Response<TResponseDto>? Response { get; set; }
}