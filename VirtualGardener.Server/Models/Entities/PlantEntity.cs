using System.ComponentModel.DataAnnotations;
using VirtualGardener.Shared.Models;

namespace VirtualGardenerServer.Models;

public class PlantEntity : Plant
{
    public required UserEntity User { get; init; }

    public Plant ToPlant()
    {
        return new Plant
        {
            Id = Id,
            Name = Name,
            Type = Type,
            PlantingDate = PlantingDate,
            WateringFrequency = WateringFrequency,
            Location = Location,
            Notes = Notes,
            IsIndoor = IsIndoor,
            CareTasks = CareTasks
        };
    }
}