using KickBlastStudentForms.Helpers;
using Microsoft.Data.Sqlite;
using KickBlastStudentForms.Data;
using System.Data;

namespace KickBlastStudentForms.Forms;

public partial class HistoryControl : UserControl
{
    public HistoryControl()
    {
        InitializeComponent();
        LoadFilters();
        ReloadData();
    }

    private void LoadFilters()
    {
        cboAthlete.Items.Clear();
        cboAthlete.Items.Add("All");
        using var conn = new SqliteConnection(Db.ConnectionString);
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT Name FROM Athletes ORDER BY Name";
        using var reader = cmd.ExecuteReader();
        while (reader.Read()) cboAthlete.Items.Add(reader.GetString(0));
        cboAthlete.SelectedIndex = 0;

        cboMonth.Items.Clear();
        cboMonth.Items.Add("All");
        for (int i = 1; i <= 12; i++) cboMonth.Items.Add(i.ToString());
        cboMonth.SelectedIndex = 0;

        cboYear.Items.Clear();
        cboYear.Items.Add("All");
        for (int y = DateTime.Now.Year - 2; y <= DateTime.Now.Year + 1; y++) cboYear.Items.Add(y.ToString());
        cboYear.SelectedItem = DateTime.Now.Year.ToString();
    }

    public void ReloadData()
    {
        using var conn = new SqliteConnection(Db.ConnectionString);
        conn.Open();

        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"SELECT mc.Id, a.Name Athlete, mc.Month, mc.Year, mc.TrainingCost, mc.CoachingCost, mc.CompetitionCost, mc.TotalCost, mc.WeightStatusMessage, mc.SecondSaturdayDate
                            FROM MonthlyCalculations mc JOIN Athletes a ON a.Id=mc.AthleteId
                            WHERE (@ath='All' OR a.Name=@ath)
                              AND (@m='All' OR mc.Month=@month)
                              AND (@y='All' OR mc.Year=@year)
                            ORDER BY mc.Id DESC";
        var athlete = cboAthlete.SelectedItem?.ToString() ?? "All";
        var mTxt = cboMonth.SelectedItem?.ToString() ?? "All";
        var yTxt = cboYear.SelectedItem?.ToString() ?? "All";

        cmd.Parameters.AddWithValue("@ath", athlete);
        cmd.Parameters.AddWithValue("@m", mTxt);
        cmd.Parameters.AddWithValue("@y", yTxt);
        cmd.Parameters.AddWithValue("@month", mTxt == "All" ? -1 : int.Parse(mTxt));
        cmd.Parameters.AddWithValue("@year", yTxt == "All" ? -1 : int.Parse(yTxt));

        var table = new DataTable();
        using var reader = cmd.ExecuteReader();
        table.Load(reader);
        dgvHistory.DataSource = table;
    }

    private void dgvHistory_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;
        var row = dgvHistory.Rows[e.RowIndex];
        lblDetails.Text = $"Athlete: {row.Cells["Athlete"].Value}\n"
            + $"Training: {CurrencyHelper.ToLkr(Convert.ToDecimal(row.Cells["TrainingCost"].Value))}\n"
            + $"Coaching: {CurrencyHelper.ToLkr(Convert.ToDecimal(row.Cells["CoachingCost"].Value))}\n"
            + $"Competition: {CurrencyHelper.ToLkr(Convert.ToDecimal(row.Cells["CompetitionCost"].Value))}\n"
            + $"Total: {CurrencyHelper.ToLkr(Convert.ToDecimal(row.Cells["TotalCost"].Value))}\n"
            + $"Weight: {row.Cells["WeightStatusMessage"].Value}\n"
            + $"Second Saturday: {row.Cells["SecondSaturdayDate"].Value}";
    }

    private void FilterChanged(object sender, EventArgs e) => ReloadData();
}
