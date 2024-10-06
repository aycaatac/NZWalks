using System.ComponentModel.DataAnnotations.Schema;

namespace NZWalks.API.Models.Domain
{
    public class Image
    {
        public Guid Id { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }

        public string imageName { get; set; }
        public string? imageDescription { get; set; }
        public string imageExtension { get; set; }
        public long imageSizeInBytes { get; set; }
        public string imagePath { get; set; }
    }
}
