using VirtualGardener.Shared.Models.Enums;

namespace VirtualGardener.Client.Models;

public class UserBase
{
    public Guid Id { get; init; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public Role Role { get; init; }
}