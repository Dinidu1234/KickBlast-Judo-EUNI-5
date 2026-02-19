using KickBlastStudentForms.Data;
using System.Text.Json;

namespace KickBlastStudentForms.Forms;

public partial class SettingsControl : UserControl
{
    private readonly string _configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");

    public SettingsControl()
    {
        InitializeComponent();
        LoadPricing();
    }

    private void LoadPricing()
    {
        Db.LoadPricing();
        txtBeginner.Text = Db.Pricing.BeginnerWeeklyFee.ToString("0.##");
        txtIntermediate.Text = Db.Pricing.IntermediateWeeklyFee.ToString("0.##");
        txtElite.Text = Db.Pricing.EliteWeeklyFee.ToString("0.##");
        txtCompetition.Text = Db.Pricing.CompetitionFee.ToString("0.##");
        txtCoachingRate.Text = Db.Pricing.CoachingHourlyRate.ToString("0.##");
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        if (!decimal.TryParse(txtBeginner.Text, out var beginner) ||
            !decimal.TryParse(txtIntermediate.Text, out var intermediate) ||
            !decimal.TryParse(txtElite.Text, out var elite) ||
            !decimal.TryParse(txtCompetition.Text, out var competition) ||
            !decimal.TryParse(txtCoachingRate.Text, out var coachingRate))
        {
            MessageBox.Show("Please enter valid numbers.");
            return;
        }

        var payload = new
        {
            Pricing = new
            {
                BeginnerWeeklyFee = beginner,
                IntermediateWeeklyFee = intermediate,
                EliteWeeklyFee = elite,
                CompetitionFee = competition,
                CoachingHourlyRate = coachingRate
            }
        };

        var json = JsonSerializer.Serialize(payload, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_configPath, json);
        Db.LoadPricing();
        MessageBox.Show("Pricing updated successfully.");
    }
}
