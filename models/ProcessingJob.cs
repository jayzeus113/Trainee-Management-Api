using System.ComponentModel.DataAnnotations;

namespace TraineeManagement.Models;

public class ProcessingJob
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Status { get; set; } = null!;

    [Required]
    public int Attempts { get; set; }

    public string ErrorSummary { get; set; } = null!;
    
    public DateTime StartedTime { get; set; }
    
    public DateTime CompletedTime { get; set; }

    [Required]
    public Guid CorrelationId {get; set;}
}