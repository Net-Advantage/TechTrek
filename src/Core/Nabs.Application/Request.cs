namespace Nabs.Application;

public class Request<TRequestDto>(
    TRequestDto requestDto)
    where TRequestDto : class
{
    public TRequestDto RequestDto { get; } = requestDto;
}
