using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO.Region
{
    public class UpdateRegionsRequestDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code has to be minimum of 3 chars")]
        [MaxLength(3, ErrorMessage = "Code has to be maximum of 3 chars")]
        public string Code { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Name is maximum of 100 chars")]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
