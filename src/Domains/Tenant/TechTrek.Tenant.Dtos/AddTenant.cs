using CommunityToolkit.Mvvm.ComponentModel;

namespace TechTrek.Tenant.Dtos;

public sealed partial class AddTenant : ObservableObject
{
    [ObservableProperty]
    private string _name = default!;
}
