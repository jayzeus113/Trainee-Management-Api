using TraineeManagement.Models;
using TraineeManagement.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace TraineeManagement.Services;

public interface ILearningTaskService
{
    public Task<PagedResponse<LearningTaskResponse>> GetAll(LearningTaskSearchParameters learningTaskSearchParameters);

    public Task<LearningTaskResponse?> GetById(int Id);

    public Task<LearningTaskResponse> Create(CreateLearningTaskRequest createLearningTaskRequest);

    public Task<LearningTaskResponse?> Update(int Id, UpdateLearningTask updateLearningTask);

    public Task<bool> Delete(int Id);
}