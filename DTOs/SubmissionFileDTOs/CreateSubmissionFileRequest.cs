using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
 
namespace TraineeManagement.DTOs;

public class CreateSubmissionFileRequest
{
    [Required(ErrorMessage = "File is required.")]
    [MaxFileSize(5 * 1024 * 1024)] // 5 MB max
    [AllowedExtensions([".jpg", ".jpeg", ".png", ".pdf"])]
    public IFormFile File { get; set; } = null!;
 
    [Required(ErrorMessage = "UploadedBy is required.")]
    public int UploadedBy { get; set; }
 
}

public class MaxFileSizeAttribute : ValidationAttribute
{
    private readonly int _maxFileSize;
    public MaxFileSizeAttribute(int maxFileSize)
    {
        _maxFileSize = maxFileSize;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is IFormFile file)
        {
            if (file.Length > _maxFileSize)
            {
                return new ValidationResult(ErrorMessage ?? $"Maximum allowed file size is {_maxFileSize / (1024 * 1024)} MB.");
            }
        }
        return ValidationResult.Success;
    }
}

// 2. Custom Allowed Extensions Attribute
public class AllowedExtensionsAttribute : ValidationAttribute
{
    private readonly string[] _extensions;
    public AllowedExtensionsAttribute(string[] extensions)
    {
        _extensions = extensions;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName);
            bool isAllowed = false;
            foreach (var ext in _extensions)
            {
                if (extension == ext)
                {
                    isAllowed = true;
                    break;
                }
            }
            if (!isAllowed) return new ValidationResult(ErrorMessage ?? "This file extension is not allowed.");
        }
        return ValidationResult.Success;
    }
}
