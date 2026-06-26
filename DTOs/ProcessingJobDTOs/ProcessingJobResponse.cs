using TraineeManagement.Models;

namespace TraineeManagement.DTOs;

public class ProcessingJobResponse
{
    public int Id { get; set; }
    public string Status { get; set; } = null!;
    public int Attempts { get; set; }
    public string ErrorSummary { get; set; } = string.Empty;
    public DateTime StartedTime { get; set; }
    public DateTime CompletedTime { get; set; }
    public Guid CorrelationId {get; set;}
    public ProcessingJobResponse(ProcessingJob pj)
    {
        Id = pj.Id;
        Status = pj.Status;
        Attempts = pj.Attempts;
        ErrorSummary = pj.ErrorSummary;
        StartedTime = pj.StartedTime;
        CompletedTime = pj.CompletedTime;
        CorrelationId = pj.CorrelationId;
    }
}