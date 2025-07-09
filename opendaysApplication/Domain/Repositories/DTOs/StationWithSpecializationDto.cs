namespace Domain.Repositories.DTOs;

public class StationWithSpecializationDto
{
    public int StationId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string SpecializationName { get; set; }
    public string SpecializationDescription { get; set; }
}