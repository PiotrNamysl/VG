using VirtualGardener.Shared.Models.Enums;

namespace VirtualGardener.Client.Models;

public class User : UserBase
{
    public string? Password { get; set; }
}