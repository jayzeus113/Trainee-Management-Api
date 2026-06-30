using System.ComponentModel.DataAnnotations;
using TraineeManagement.Constants;
 
namespace TraineeManagement.DTOs;

public class CreateSubmissionFileRequest
{
    [Required]
    [MaxFileSize(5 * NumberConstants.KB * NumberConstants.KB)]
    [AllowedExtensions([".jpg", ".jpeg", ".png", ".pdf"])]
    public IFormFile File { get; set; } = null!;
    [Range(1, int.MaxValue, ErrorMessage = "Invalid UploadedBy Id")]
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
                return new ValidationResult(ErrorMessage ?? $"Maximum allowed file size is {_maxFileSize / (NumberConstants.KB * NumberConstants.KB)} MB.");
            }
        }
        return ValidationResult.Success;
    }
}

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
