using CommunityToolkit.Mvvm.ComponentModel;

namespace TechTrek.Tenant.Dtos;

public sealed partial class Tenant : ObservableObject
{
    [ObservableProperty]
    private Guid _id;

    [ObservableProperty]
    private string _name = default!;
}
