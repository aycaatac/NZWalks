using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NZWalks.API.Models.DTOs
{
    public class InputImageDto
    {

        [Required]
        public IFormFile File { get; set; }

        [Required]
        public string imageName { get; set; }

        public string? imageDescription { get; set; }
       
    }
}
