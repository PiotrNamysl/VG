using VirtualGardener.Shared.Models.Enums;

namespace VirtualGardener.Shared.Models;

public class CareTask
{
    public Guid Id { get; init; }
    public Guid PlantId { get; init; }
    public CareTaskType ActionType { get; set; }
    public DateTime TaskDate { get; set; }
    public string? Notes { get; set; }
}