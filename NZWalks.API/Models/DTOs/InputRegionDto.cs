using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTOs
{
    public class InputRegionDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "The code must be 3 characters at minimum")] //this message doesnt show??
        [MaxLength(3)]
        public string Code { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; } 
    }
}
