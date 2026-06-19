using TraineeManagement.DTOs;

namespace TraineeManagement.Services
{
    public interface ISubmissionService
    {
        Task<List<SubmissionResponse>>  GetAll();
        Task<SubmissionResponse> GetById(int Id);
        Task<SubmissionResponse> Create(CreateSubmissionRequest createSubmissionRequest);

        Task<SubmissionFileResponse> UploadFile(int userId, int submissionId, CreateSubmissionFileRequest createSubmissionFileRequest);

        Task<FileStream> DownloadFile(int submissionFileId);

        Task<bool> DeleteFile(int submissionFileId);
    }
}