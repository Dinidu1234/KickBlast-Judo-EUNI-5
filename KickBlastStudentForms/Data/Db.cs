using KickBlastStudentForms.Models;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace KickBlastStudentForms.Data;

public static class Db
{
    private static readonly string BaseDir = AppDomain.CurrentDomain.BaseDirectory;
    private static readonly string DataDir = Path.Combine(BaseDir, "Data");
    private static readonly string DbPath = Path.Combine(DataDir, "kickblast_student.db");
    public static string ConnectionString => $"Data Source={DbPath}";
    public static PricingSettings Pricing { get; private set; } = new();

    public static void Initialize()
    {
        Directory.CreateDirectory(DataDir);
        LoadPricing();

        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();

        CreateTables(connection);
        SeedIfNeeded(connection);
    }

    public static void LoadPricing()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(BaseDir)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        Pricing = config.GetSection("Pricing").Get<PricingSettings>() ?? new PricingSettings();
    }

    private static void CreateTables(SqliteConnection connection)
    {
        var sql = @"
CREATE TABLE IF NOT EXISTS Users (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Username TEXT UNIQUE,
    PasswordPlain TEXT,
    CreatedAt TEXT
);
CREATE TABLE IF NOT EXISTS TrainingPlans (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT UNIQUE,
    WeeklyFee REAL
);
CREATE TABLE IF NOT EXISTS Athletes (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT,
    TrainingPlanId INTEGER,
    CurrentWeightKg REAL,
    CompetitionCategoryKg REAL,
    CreatedAt TEXT
);
CREATE TABLE IF NOT EXISTS MonthlyCalculations (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    AthleteId INTEGER,
    Month INTEGER,
    Year INTEGER,
    TrainingCost REAL,
    CoachingCost REAL,
    CompetitionCost REAL,
    TotalCost REAL,
    CompetitionsCount INTEGER,
    CoachingHoursPerWeek REAL,
    WeightStatusMessage TEXT,
    SecondSaturdayDate TEXT,
    CreatedAt TEXT
);";

        using var cmd = connection.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }

    private static void SeedIfNeeded(SqliteConnection connection)
    {
        using var countCmd = connection.CreateCommand();
        countCmd.CommandText = "SELECT COUNT(*) FROM Users";
        var userCount = Convert.ToInt32(countCmd.ExecuteScalar());
        if (userCount > 0)
        {
            return;
        }

        using var tx = connection.BeginTransaction();

        using var userCmd = connection.CreateCommand();
        userCmd.Transaction = tx;
        userCmd.CommandText = "INSERT INTO Users (Username, PasswordPlain, CreatedAt) VALUES ('rashmika','123456', @createdAt)";
        userCmd.Parameters.AddWithValue("@createdAt", DateTime.Now.ToString("s"));
        userCmd.ExecuteNonQuery();

        InsertPlan(connection, tx, "Beginner", Pricing.BeginnerWeeklyFee);
        InsertPlan(connection, tx, "Intermediate", Pricing.IntermediateWeeklyFee);
        InsertPlan(connection, tx, "Elite", Pricing.EliteWeeklyFee);

        var athletes = new[]
        {
            ("Ayesha Fernando", "Beginner", 49.5m, 52m),
            ("Kasun Silva", "Beginner", 62m, 60m),
            ("Nimal Perera", "Intermediate", 73m, 73m),
            ("Dilan Jayasuriya", "Intermediate", 68m, 66m),
            ("Ravindu Madushan", "Elite", 81m, 81m),
            ("Tharushi Lakmali", "Elite", 57m, 57m)
        };

        foreach (var a in athletes)
        {
            using var athleteCmd = connection.CreateCommand();
            athleteCmd.Transaction = tx;
            athleteCmd.CommandText = @"INSERT INTO Athletes (Name, TrainingPlanId, CurrentWeightKg, CompetitionCategoryKg, CreatedAt)
                                      VALUES (@name, (SELECT Id FROM TrainingPlans WHERE Name=@plan), @currentW, @categoryW, @createdAt)";
            athleteCmd.Parameters.AddWithValue("@name", a.Item1);
            athleteCmd.Parameters.AddWithValue("@plan", a.Item2);
            athleteCmd.Parameters.AddWithValue("@currentW", a.Item3);
            athleteCmd.Parameters.AddWithValue("@categoryW", a.Item4);
            athleteCmd.Parameters.AddWithValue("@createdAt", DateTime.Now.ToString("s"));
            athleteCmd.ExecuteNonQuery();
        }

        tx.Commit();
    }

    private static void InsertPlan(SqliteConnection connection, SqliteTransaction tx, string name, decimal fee)
    {
        using var cmd = connection.CreateCommand();
        cmd.Transaction = tx;
        cmd.CommandText = "INSERT INTO TrainingPlans (Name, WeeklyFee) VALUES (@name, @fee)";
        cmd.Parameters.AddWithValue("@name", name);
        cmd.Parameters.AddWithValue("@fee", fee);
        cmd.ExecuteNonQuery();
    }
}

public class PricingSettings
{
    public decimal BeginnerWeeklyFee { get; set; } = 2500;
    public decimal IntermediateWeeklyFee { get; set; } = 3500;
    public decimal EliteWeeklyFee { get; set; } = 5000;
    public decimal CompetitionFee { get; set; } = 1500;
    public decimal CoachingHourlyRate { get; set; } = 800;
}
