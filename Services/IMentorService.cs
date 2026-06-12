using TraineeManagement.DTOs;
using TraineeManagement.Models;

namespace TraineeManagement.Services
{
    public interface IMentorService
    {
        Task<PagedResponse<MentorResponse>>  GetAll(MentorSearchParameters traineeSearchParameters);
        Task<MentorResponse?> GetById(int Id);
        Task<MentorResponse> Create(CreateMentorRequest createMentorRequest);

        Task<MentorResponse?> Update(int Id, UpdateMentorRequest updateMentorRequest);

        Task<bool> Delete(int Id);
    }
}