using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO.Walk
{
    public class AddWalkRequestDTO
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name is maximum of 100 chars")]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000, ErrorMessage = "Description is maximum of 1000 chars")]
        public string Description { get; set; }

        [Required]
        [Range(0, 50)]
        public double LengthInKm { get; set; }

        public string? WalkImageUrl { get; set; }

        [Required]
        public Guid RegionId { get; set; }

        [Required]
        public Guid DifficultyId { get; set; }
    }
}
