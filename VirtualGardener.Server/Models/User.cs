namespace VirtualGardenerServer.Models;

public class User
{
    public required string Name { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
}