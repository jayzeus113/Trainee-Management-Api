namespace TraineeManagement.Services;

public interface IFileStorageService
{
    Task SaveAsync(string fileName, Stream stream);
    Task<FileStream> OpenReadAsync(string storageName);
    Task<bool> ExistsAsync(string storageName);
    Task DeleteAsync(string storageName);

    string GenerateUniqueFileName(string originalFileName);

    string GetContentType(string fileName);

    string GetChecksum(Stream stream);
}