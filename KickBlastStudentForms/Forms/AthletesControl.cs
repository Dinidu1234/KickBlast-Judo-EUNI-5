using KickBlastStudentForms.Data;
using KickBlastStudentForms.Helpers;
using Microsoft.Data.Sqlite;
using System.Data;

namespace KickBlastStudentForms.Forms;

public partial class AthletesControl : UserControl
{
    private readonly MainForm _main;
    private int _selectedId;

    public AthletesControl(MainForm main)
    {
        _main = main;
        InitializeComponent();
        LoadPlanFilters();
        LoadAthletes();
    }

    private void LoadPlanFilters()
    {
        cboFilterPlan.Items.Clear();
        cboFilterPlan.Items.Add("All");
        cboPlan.Items.Clear();

        using var conn = new SqliteConnection(Db.ConnectionString);
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT Name FROM TrainingPlans ORDER BY Id";
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            var name = reader.GetString(0);
            cboFilterPlan.Items.Add(name);
            cboPlan.Items.Add(name);
        }

        cboFilterPlan.SelectedIndex = 0;
        if (cboPlan.Items.Count > 0) cboPlan.SelectedIndex = 0;
    }

    private void LoadAthletes()
    {
        using var conn = new SqliteConnection(Db.ConnectionString);
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"SELECT a.Id, a.Name, t.Name AS Plan, a.CurrentWeightKg, a.CompetitionCategoryKg
                            FROM Athletes a JOIN TrainingPlans t ON t.Id=a.TrainingPlanId
                            WHERE (@name='' OR a.Name LIKE '%' || @name || '%')
                              AND (@plan='All' OR t.Name=@plan)
                            ORDER BY a.Id DESC";
        cmd.Parameters.AddWithValue("@name", txtSearch.Text.Trim());
        cmd.Parameters.AddWithValue("@plan", cboFilterPlan.SelectedItem?.ToString() ?? "All");

        var table = new DataTable();
        using var reader = cmd.ExecuteReader();
        table.Load(reader);
        dgvAthletes.DataSource = table;
    }

    private bool ValidateInput()
    {
        if (!ValidationHelper.IsRequired(txtName.Text))
        {
            lblValidation.Text = "Name is required.";
            return false;
        }

        if (!ValidationHelper.IsDecimal(txtCurrentWeight.Text, out var curr) || curr <= 0)
        {
            lblValidation.Text = "Current weight must be a positive number.";
            return false;
        }

        if (!ValidationHelper.IsDecimal(txtCategoryWeight.Text, out var cat) || cat <= 0)
        {
            lblValidation.Text = "Category weight must be a positive number.";
            return false;
        }

        lblValidation.Text = string.Empty;
        return true;
    }

    private int GetPlanIdByName(SqliteConnection conn, string name)
    {
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT Id FROM TrainingPlans WHERE Name=@n";
        cmd.Parameters.AddWithValue("@n", name);
        return Convert.ToInt32(cmd.ExecuteScalar());
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        if (!ValidateInput()) return;
        using var conn = new SqliteConnection(Db.ConnectionString);
        conn.Open();

        int planId = GetPlanIdByName(conn, cboPlan.SelectedItem?.ToString() ?? "Beginner");

        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"INSERT INTO Athletes (Name, TrainingPlanId, CurrentWeightKg, CompetitionCategoryKg, CreatedAt)
                            VALUES (@name,@pid,@cw,@cat,@created)";
        cmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
        cmd.Parameters.AddWithValue("@pid", planId);
        cmd.Parameters.AddWithValue("@cw", decimal.Parse(txtCurrentWeight.Text));
        cmd.Parameters.AddWithValue("@cat", decimal.Parse(txtCategoryWeight.Text));
        cmd.Parameters.AddWithValue("@created", DateTime.Now.ToString("s"));
        cmd.ExecuteNonQuery();

        LoadAthletes();
        _main.Dashboard.ReloadStats();
        _main.History.ReloadData();
    }

    private void btnUpdate_Click(object sender, EventArgs e)
    {
        if (_selectedId == 0 || !ValidateInput()) return;
        using var conn = new SqliteConnection(Db.ConnectionString);
        conn.Open();
        int planId = GetPlanIdByName(conn, cboPlan.SelectedItem?.ToString() ?? "Beginner");

        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"UPDATE Athletes SET Name=@name, TrainingPlanId=@pid, CurrentWeightKg=@cw, CompetitionCategoryKg=@cat WHERE Id=@id";
        cmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
        cmd.Parameters.AddWithValue("@pid", planId);
        cmd.Parameters.AddWithValue("@cw", decimal.Parse(txtCurrentWeight.Text));
        cmd.Parameters.AddWithValue("@cat", decimal.Parse(txtCategoryWeight.Text));
        cmd.Parameters.AddWithValue("@id", _selectedId);
        cmd.ExecuteNonQuery();

        LoadAthletes();
        _main.Dashboard.ReloadStats();
        _main.History.ReloadData();
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        if (_selectedId == 0) return;
        if (MessageBox.Show("Delete selected athlete?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

        using var conn = new SqliteConnection(Db.ConnectionString);
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "DELETE FROM Athletes WHERE Id=@id";
        cmd.Parameters.AddWithValue("@id", _selectedId);
        cmd.ExecuteNonQuery();

        btnNew_Click(sender, e);
        LoadAthletes();
        _main.Dashboard.ReloadStats();
        _main.History.ReloadData();
    }

    private void btnNew_Click(object sender, EventArgs e)
    {
        _selectedId = 0;
        txtName.Clear();
        txtCurrentWeight.Clear();
        txtCategoryWeight.Clear();
        lblValidation.Text = string.Empty;
    }

    private void txtSearch_TextChanged(object sender, EventArgs e) => LoadAthletes();
    private void cboFilterPlan_SelectedIndexChanged(object sender, EventArgs e) => LoadAthletes();

    private void dgvAthletes_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || dgvAthletes.Rows[e.RowIndex].Cells["Id"].Value is null) return;
        var row = dgvAthletes.Rows[e.RowIndex];
        _selectedId = Convert.ToInt32(row.Cells["Id"].Value);
        txtName.Text = row.Cells["Name"].Value?.ToString();
        cboPlan.SelectedItem = row.Cells["Plan"].Value?.ToString();
        txtCurrentWeight.Text = row.Cells["CurrentWeightKg"].Value?.ToString();
        txtCategoryWeight.Text = row.Cells["CompetitionCategoryKg"].Value?.ToString();
    }
}
