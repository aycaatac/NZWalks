namespace NZWalks.API.Models.DTOs
{
    public class WalkDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        //public Guid RegionId { get; set; } we can already see these below so we can remove these
        //public Guid DifficultyId { get; set; }
        public RegionDto Region { get; set; } //should these be nullable
        public DifficultyDto Difficulty { get; set; }
    }
}
