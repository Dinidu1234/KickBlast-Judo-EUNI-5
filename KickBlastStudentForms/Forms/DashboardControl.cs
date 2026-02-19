using KickBlastStudentForms.Data;
using KickBlastStudentForms.Helpers;
using Microsoft.Data.Sqlite;

namespace KickBlastStudentForms.Forms;

public partial class DashboardControl : UserControl
{
    public DashboardControl()
    {
        InitializeComponent();
        ReloadStats();
    }

    public void ReloadStats()
    {
        using var conn = new SqliteConnection(Db.ConnectionString);
        conn.Open();

        lblTotalAthletesValue.Text = ExecuteScalar(conn, "SELECT COUNT(*) FROM Athletes").ToString();

        var month = DateTime.Now.Month;
        var year = DateTime.Now.Year;
        lblCalcsValue.Text = ExecuteScalar(conn, "SELECT COUNT(*) FROM MonthlyCalculations WHERE Month=@m AND Year=@y", ("@m", month), ("@y", year)).ToString();

        var revenue = Convert.ToDecimal(ExecuteScalar(conn, "SELECT IFNULL(SUM(TotalCost),0) FROM MonthlyCalculations WHERE Month=@m AND Year=@y", ("@m", month), ("@y", year)));
        lblRevenueValue.Text = CurrencyHelper.ToLkr(revenue);

        lblNextCompValue.Text = DateHelper.GetSecondSaturday(year, month).ToString("dd MMM yyyy");

        using var da = conn.CreateCommand();
        da.CommandText = @"SELECT mc.Id, a.Name Athlete, mc.Month, mc.Year, mc.TotalCost, mc.CreatedAt
                           FROM MonthlyCalculations mc JOIN Athletes a ON a.Id=mc.AthleteId
                           ORDER BY mc.Id DESC LIMIT 5";
        using var reader = da.ExecuteReader();
        var table = new System.Data.DataTable();
        table.Load(reader);
        dgvRecent.DataSource = table;
        if (dgvRecent.Columns.Contains("TotalCost"))
        {
            dgvRecent.Columns["TotalCost"].DefaultCellStyle.Format = "N2";
        }
    }

    private object ExecuteScalar(SqliteConnection conn, string sql, params (string, object)[] parameters)
    {
        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        foreach (var p in parameters)
        {
            cmd.Parameters.AddWithValue(p.Item1, p.Item2);
        }
        return cmd.ExecuteScalar() ?? 0;
    }
}
