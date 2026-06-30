namespace TraineeManagement.DTOs;

public record SubmissionProcessingRequested(
    Guid MessageId,
    Guid CorrelationId,
    int SubmissionId,
    int FileId,
    DateTime RequestedAt,
    string ContractVersion = "1.0"
);