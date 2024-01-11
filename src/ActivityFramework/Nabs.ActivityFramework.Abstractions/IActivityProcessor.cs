namespace Nabs.ActivityFramework.Abstractions;

public interface IActivityProcessor
{
    List<IActivityFeature> ActivityFeatures { get; }
    Task Process();
}
