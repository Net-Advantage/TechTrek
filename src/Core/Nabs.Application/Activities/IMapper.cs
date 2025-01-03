namespace Nabs.Application.Activities;

public interface IMapper<TActivityState>
{
    void Map(TActivityState state);
}
