using FluentValidation.Results;

namespace Nabs.Application;

public class Response<TResponseDto>(
    TResponseDto responseDto)
    where TResponseDto : class
{
    public TResponseDto ResponseDto { get; } = responseDto;

    public ValidationResult? ValidationResult { get; set; }
}