using System.ComponentModel.DataAnnotations;

namespace TraineeManagement.Models;

public class ProcessingJob
{
    [Key]
    public int Id { get; set; }
    public string Status { get; set; } = null!;
    public int Attempts { get; set; }
    public string ErrorSummary { get; set; } = string.Empty;
    public DateTime StartedTime { get; set; }
    public DateTime CompletedTime { get; set; }
    public Guid CorrelationId {get; set;}
}