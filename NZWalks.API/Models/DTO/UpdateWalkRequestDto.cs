using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class UpdateWalkRequestDto

    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name cannot exceed more than 100 characters")]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000, ErrorMessage = "Name cannot exceed more than 1000 characters")]
        public string Description { get; set; }

        [Required]
        [Range(0, 50)]
        public Double LengthInKm { get; set; }

        public String? WalklImageURL { get; set; }

        [Required]
        public Guid RegionId { get; set; }

        [Required]
        public Guid DifficultyId { get; set; }
    }
}
