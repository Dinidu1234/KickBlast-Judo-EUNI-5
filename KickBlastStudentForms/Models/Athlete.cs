namespace KickBlastStudentForms.Models;

public class Athlete
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int TrainingPlanId { get; set; }
    public string TrainingPlanName { get; set; } = string.Empty;
    public decimal CurrentWeightKg { get; set; }
    public decimal CompetitionCategoryKg { get; set; }
    public string CreatedAt { get; set; } = string.Empty;
}
