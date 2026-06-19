namespace TraineeManagement.DTOs;
 
public class SubmissionFileResponse
{
    public int Id { get; set; }
    public int SubmissionId { get; set; }
    public string OriginalFileName { get; set; } = "";
    public string GeneratedStorageName {get; set; } = "";
    public string ContentType { get; set; } = "";
    public long Size { get; set; }
    public string CheckSum { get; set; } = "";
    public string UploadedBy { get; set; } = "";
    public DateTime CreatedDate { get; set; }
}
 