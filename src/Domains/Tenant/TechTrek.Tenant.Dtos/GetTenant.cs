using CommunityToolkit.Mvvm.ComponentModel;

namespace TechTrek.Tenant.Dtos;

public sealed partial class GetTenant : ObservableObject
{
    [ObservableProperty]
    private Guid _id = default!;
}
