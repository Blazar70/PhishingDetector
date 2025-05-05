namespace YourApp.Models
{
    public class UploadViewModel
    {
        public List<string> FileNames { get; set; } = new();
        public string? Message { get; set; }
    }
}
