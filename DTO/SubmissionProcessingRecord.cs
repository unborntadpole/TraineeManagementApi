namespace TraineeManagementApi.DTO;
using TraineeManagementApi.Constants;

public record SubmissionProcessingRequested(
    Guid MessageId,
    Guid CorrelationId,
    long SubmissionId,
    long FileId,
    DateTimeOffset RequestedAt,
    string ContractVersion
);