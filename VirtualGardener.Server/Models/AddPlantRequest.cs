using VirtualGardener.Shared.Models.Enums;

namespace VirtualGardenerServer.Models;

public class AddPlantRequest
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public PlantType Type { get; init; }
    public DateTime PlantingDate { get; init; }
    public Frequency WateringFrequency { get; init; }
    public Frequency FertilizingFrequency => (Frequency)WateringFrequency + 1;
    public required string Location { get; set; }
    public string? Notes { get; set; }
    public bool IsIndoor { get; set; }
}