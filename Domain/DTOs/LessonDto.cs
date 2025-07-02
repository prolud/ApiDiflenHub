using Domain.Models;

namespace Domain.DTOs;

public class LessonDtoOut
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? VideoUrl { get; set; }
}
