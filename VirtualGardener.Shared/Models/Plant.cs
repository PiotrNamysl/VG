using System.ComponentModel.DataAnnotations;
using VirtualGardener.Shared.Models.Enums;

namespace VirtualGardener.Shared.Models;

public class Plant
{
    [Key] public Guid Id { get; init; }
    public string? Name { get; set; }
    public PlantType Type { get; set; }
    public DateTime PlantingDate { get; set; }
    public Frequency WateringFrequency { get; set; }
    public Frequency FertilizingFrequency => (Frequency)WateringFrequency + 1;
    public string? Location { get; set; }
    public string? Notes { get; set; }
    public bool IsIndoor { get; set; }

    public string? Height =>
        CareTasks?
            .Where(t => t.ActionType == CareTaskType.Measuring)
            .OrderByDescending(t => t.TaskDate).FirstOrDefault()?.Notes ?? "0";

    public DateTime? LastWatering =>
        CareTasks?
            .Where(t => t.ActionType == CareTaskType.Watering)
            .OrderByDescending(t => t.TaskDate).FirstOrDefault()?.TaskDate;

   public DateTime? LastFertilizing =>
        CareTasks?
            .Where(t => t.ActionType == CareTaskType.Fertilizing)
            .OrderByDescending(t => t.TaskDate).FirstOrDefault()?.TaskDate;

    public DateTime? LastMeasuring =>
        CareTasks?
            .Where(t => t.ActionType == CareTaskType.Measuring)
            .OrderByDescending(t => t.TaskDate).FirstOrDefault()?.TaskDate;

    public DateTime? LastPruning =>
        CareTasks?
            .Where(t => t.ActionType == CareTaskType.Pruning)
            .OrderByDescending(t => t.TaskDate).FirstOrDefault()?.TaskDate;

    public DateTime? LastPestControl =>
        CareTasks?
            .Where(t => t.ActionType == CareTaskType.PestControl)
            .OrderByDescending(t => t.TaskDate).FirstOrDefault()?.TaskDate;

    public DateTime? LastRepotting =>
        CareTasks?
            .Where(t => t.ActionType == CareTaskType.Repotting)
            .OrderByDescending(t => t.TaskDate).FirstOrDefault()?.TaskDate;

    public List<CareTask>? CareTasks { get; set; }
    
}