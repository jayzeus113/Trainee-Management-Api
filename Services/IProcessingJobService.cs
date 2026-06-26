using TraineeManagement.DTOs;

namespace TraineeManagement.Services
{
    public interface IProcessingJobService
    {
        Task<ProcessingJobResponse> GetById(int Id);
    }
}