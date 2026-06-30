using TraineeManagement.DTOs;
using System.Security.Cryptography;
using Microsoft.AspNetCore.StaticFiles;
 
 
namespace TraineeManagement.Services;
 
public class LocalFileStorageService : IFileStorageService
{
    private readonly string _basePath;
    
    private readonly ILogger<LocalFileStorageService> _logger;
 
    public LocalFileStorageService(IConfiguration configuration, ILogger<LocalFileStorageService> logger)
    {
        _logger = logger;
        _basePath = configuration["LocalFileStorage:BasePath"] ?? "./Uploads";
        _logger.LogInformation("Initializing LocalFileStorageService with BasePath: {BasePath}", _basePath);
        Directory.CreateDirectory(_basePath);
    }
 
    public async Task SaveAsync(string fileName, Stream stream)
    {
        var filePath = GetFullPath(fileName);
        _logger.LogInformation("Successfully saved file: {FileName}", fileName);
        await using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true);
        await stream.CopyToAsync(fileStream);
    }
 
    public Task<FileStream> OpenReadAsync(string fileName)
    {
        var filePath = GetFullPath(fileName);
       
        if (!File.Exists(filePath))
        {
            _logger.LogWarning("Attempted to read non-existent file: {FileName} at path: {FilePath}", fileName, filePath);
            throw new FileNotFoundException($"File not found: {fileName}");
        }
        _logger.LogInformation("Opening file for reading: {FileName}", fileName);
        FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true);
        return Task.FromResult(fileStream);
    }
 
    public Task<bool> ExistsAsync(string fileName)
    {
        bool exists = File.Exists(GetFullPath(fileName));
        _logger.LogDebug("Checking file existence for: {FileName}. Exists: {Exists}", fileName, exists);
        return Task.FromResult(exists);
    }
 
    public Task DeleteAsync(string fileName)
    {
        var filePath = GetFullPath(fileName);
       
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            _logger.LogInformation("Successfully deleted file: {FileName} from disk", fileName);
        } else
        {
            _logger.LogWarning("Attempted to delete file that does not exist: {FileName}", fileName);
        }
        return Task.CompletedTask;
    }
 
    private string GetFullPath(string fileName)
    {
        var safeFileName = Path.GetFileName(fileName);
        return Path.Combine(_basePath, safeFileName);
    }
 
    public string GenerateUniqueFileName(string originalFileName)
    {
        var extension = Path.GetExtension(originalFileName);
        string uniqueName = $"{Guid.NewGuid().ToString("N")}{extension}";
        _logger.LogDebug("Generated unique file name {UniqueName} for original file: {OriginalFileName}", uniqueName, originalFileName);
        return uniqueName;
    }
 
    public string GetContentType(string fileName)
    {
        string filePath = GetFullPath(fileName);
        var provider = new FileExtensionContentTypeProvider();
       
        if (!provider.TryGetContentType(filePath, out string? contentType))
        {
            contentType = "application/octet-stream";
            _logger.LogWarning("Could not determine content type for file: {FileName}. Defaulting to octet-stream", fileName);

        }
       
        return contentType;
    }
 
    public string GetChecksum(Stream stream)
    {
        _logger.LogDebug("Calculating SHA256 checksum for stream");
        using var sha256 = SHA256.Create();
        byte[] hashBytes = sha256.ComputeHash(stream);
        string checksum = Convert.ToHexString(hashBytes).ToLowerInvariant();
        _logger.LogDebug("Calculated Checksum: {Checksum}", checksum);
        return checksum;
    }
}
 
 