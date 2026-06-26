namespace TraineeManagement.DTOs;

public record SubmissionProcessingRequested(
    Guid MessageId,
    Guid CorrelationId,
    int SubmissionId,
    int FileId,
    DateTimeOffset RequestedAt,
    string ContractVersion = "1.0"
);