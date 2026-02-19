namespace KickBlastStudentForms.Models;

public class MonthlyCalculation
{
    public int Id { get; set; }
    public int AthleteId { get; set; }
    public string AthleteName { get; set; } = string.Empty;
    public int Month { get; set; }
    public int Year { get; set; }
    public decimal TrainingCost { get; set; }
    public decimal CoachingCost { get; set; }
    public decimal CompetitionCost { get; set; }
    public decimal TotalCost { get; set; }
    public int CompetitionsCount { get; set; }
    public decimal CoachingHoursPerWeek { get; set; }
    public string WeightStatusMessage { get; set; } = string.Empty;
    public string SecondSaturdayDate { get; set; } = string.Empty;
    public string CreatedAt { get; set; } = string.Empty;
}
