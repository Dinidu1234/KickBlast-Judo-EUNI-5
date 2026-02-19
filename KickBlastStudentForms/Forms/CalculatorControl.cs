using KickBlastStudentForms.Data;
using KickBlastStudentForms.Helpers;
using Microsoft.Data.Sqlite;

namespace KickBlastStudentForms.Forms;

public partial class CalculatorControl : UserControl
{
    private readonly MainForm _main;
    private int _athleteId;
    private decimal _trainingCost;
    private decimal _coachingCost;
    private decimal _competitionCost;
    private decimal _total;
    private string _weightStatus = "";
    private string _secondSaturday = "";

    public CalculatorControl(MainForm main)
    {
        _main = main;
        InitializeComponent();
        LoadAthletes();
    }

    private void LoadAthletes()
    {
        cboAthlete.Items.Clear();
        using var conn = new SqliteConnection(Db.ConnectionString);
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT Id, Name FROM Athletes ORDER BY Name";
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            cboAthlete.Items.Add(new ComboItem(reader.GetInt32(0), reader.GetString(1)));
        }
        if (cboAthlete.Items.Count > 0) cboAthlete.SelectedIndex = 0;
    }

    private void btnCalculate_Click(object sender, EventArgs e)
    {
        if (cboAthlete.SelectedItem is not ComboItem selected)
        {
            MessageBox.Show("Please select an athlete.");
            return;
        }

        _athleteId = selected.Id;
        using var conn = new SqliteConnection(Db.ConnectionString);
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"SELECT t.Name, t.WeeklyFee, a.CurrentWeightKg, a.CompetitionCategoryKg
                            FROM Athletes a JOIN TrainingPlans t ON t.Id = a.TrainingPlanId
                            WHERE a.Id=@id";
        cmd.Parameters.AddWithValue("@id", _athleteId);
        using var reader = cmd.ExecuteReader();
        if (!reader.Read()) return;

        string plan = reader.GetString(0);
        decimal weeklyFee = reader.GetDecimal(1);
        decimal curr = reader.GetDecimal(2);
        decimal cat = reader.GetDecimal(3);

        var competitions = (int)numCompetitions.Value;
        if (plan == "Beginner")
        {
            competitions = 0;
            numCompetitions.Value = 0;
            lblBeginnerNote.Text = "Beginner plan: competitions forced to 0.";
        }
        else
        {
            lblBeginnerNote.Text = "";
        }

        _trainingCost = weeklyFee * 4;
        _coachingCost = numCoachingHours.Value * 4 * Db.Pricing.CoachingHourlyRate;
        _competitionCost = competitions * Db.Pricing.CompetitionFee;
        _total = _trainingCost + _coachingCost + _competitionCost;

        _weightStatus = curr > cat ? "Over target" : curr < cat ? "Under target" : "On target";
        _secondSaturday = DateHelper.GetSecondSaturday(DateTime.Now.Year, DateTime.Now.Month).ToString("dd MMM yyyy");

        lblTrainingCost.Text = CurrencyHelper.ToLkr(_trainingCost);
        lblCoachingCost.Text = CurrencyHelper.ToLkr(_coachingCost);
        lblCompetitionCost.Text = CurrencyHelper.ToLkr(_competitionCost);
        lblTotal.Text = CurrencyHelper.ToLkr(_total);
        lblWeightStatus.Text = _weightStatus;
        lblSecondSaturday.Text = _secondSaturday;
    }

    private void btnSaveCalculation_Click(object sender, EventArgs e)
    {
        if (_athleteId == 0 || _total <= 0)
        {
            MessageBox.Show("Please calculate first.");
            return;
        }

        using var conn = new SqliteConnection(Db.ConnectionString);
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"INSERT INTO MonthlyCalculations
            (AthleteId, Month, Year, TrainingCost, CoachingCost, CompetitionCost, TotalCost, CompetitionsCount, CoachingHoursPerWeek, WeightStatusMessage, SecondSaturdayDate, CreatedAt)
            VALUES (@aid,@m,@y,@tc,@cc,@compc,@tot,@count,@hrs,@msg,@sat,@created)";

        cmd.Parameters.AddWithValue("@aid", _athleteId);
        cmd.Parameters.AddWithValue("@m", DateTime.Now.Month);
        cmd.Parameters.AddWithValue("@y", DateTime.Now.Year);
        cmd.Parameters.AddWithValue("@tc", _trainingCost);
        cmd.Parameters.AddWithValue("@cc", _coachingCost);
        cmd.Parameters.AddWithValue("@compc", _competitionCost);
        cmd.Parameters.AddWithValue("@tot", _total);
        cmd.Parameters.AddWithValue("@count", (int)numCompetitions.Value);
        cmd.Parameters.AddWithValue("@hrs", numCoachingHours.Value);
        cmd.Parameters.AddWithValue("@msg", _weightStatus);
        cmd.Parameters.AddWithValue("@sat", _secondSaturday);
        cmd.Parameters.AddWithValue("@created", DateTime.Now.ToString("s"));
        cmd.ExecuteNonQuery();

        MessageBox.Show("Calculation saved successfully.");
        _main.Dashboard.ReloadStats();
        _main.History.ReloadData();
    }

    private record ComboItem(int Id, string Name)
    {
        public override string ToString() => Name;
    }
}
