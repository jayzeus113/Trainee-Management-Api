using TraineeManagement.DTOs;

namespace TraineeManagement.Services
{
    public interface ISubmissionService
    {
        Task<List<SubmissionResponse>>  GetAll();
        Task<SubmissionResponse> GetById(int Id);
        Task<SubmissionResponse> Create(CreateSubmissionRequest createSubmissionRequest);
    }
}