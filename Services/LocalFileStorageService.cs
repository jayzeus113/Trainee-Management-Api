 
using Microsoft.AspNetCore.StaticFiles;
using System.Security.Cryptography;
 
 
namespace TraineeManagement.Services;
 
public class LocalFileStorageService : IFileStorageService
{
    private readonly string _basePath;
 
    public LocalFileStorageService(IConfiguration configuration)
    {
        _basePath = configuration["FileStorageSettings:BasePath"] ?? "Uploads";
        Directory.CreateDirectory(_basePath);
    }
 
    public async Task SaveAsync(string fileName, Stream stream)
    {
        var filePath = GetFullPath(fileName);
       
        await using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true);
        await stream.CopyToAsync(fileStream);
    }
 
    public Task<FileStream> OpenReadAsync(string fileName)
    {
        var filePath = GetFullPath(fileName);
       
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"File not found: {fileName}");
 
        FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true);
        return Task.FromResult(fileStream);
    }
 
    public Task<bool> ExistsAsync(string fileName)
    {
        return Task.FromResult(File.Exists(GetFullPath(fileName)));
    }
 
    public Task DeleteAsync(string fileName)
    {
        var filePath = GetFullPath(fileName);
       
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
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
        return $"{Guid.NewGuid().ToString("N")}{extension}";
    }
 
    public string GetContentType(string fileName)
    {
        string filePath = GetFullPath(fileName);
        var provider = new FileExtensionContentTypeProvider();
       
        if (!provider.TryGetContentType(filePath, out string? contentType))
        {
            contentType = "application/octet-stream";
        }
       
        return contentType;
    }
 
    public string GetChecksum(string fileName)
    {
        string filePath = GetFullPath(fileName);
 
        if (!File.Exists(filePath))
            throw new FileNotFoundException("File not found.", filePath);
 
        using var sha256 = SHA256.Create();
        using var stream = File.OpenRead(filePath);
        byte[] hashBytes = sha256.ComputeHash(stream);
        return Convert.ToHexString(hashBytes).ToLowerInvariant();
    }
}
 
 