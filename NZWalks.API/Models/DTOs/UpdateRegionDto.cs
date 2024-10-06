using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTOs
{
    //why didnt we make code or name nullable? maybe the client only wants to 
    //change the name or code ??
    public class UpdateRegionDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "The code must be 3 characters at minimum")] //this message doesnt show??
        [MaxLength(3)]
        public string Code { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
    //sanırım nullable diğerleri de ??????????????? nullable değil boş bırakılabiliyo. birini değiştirmek istemesen napıcan
}
