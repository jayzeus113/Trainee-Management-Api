using TraineeManagement.DTOs;

namespace TraineeManagement.Services
{
    public interface IReviewService
    {
        Task<List<ReviewResponse>>  GetAll();
        Task<ReviewResponse> GetById(int Id);
        Task<ReviewResponse> Create(CreateReviewRequest createReviewRequest);
    }
}