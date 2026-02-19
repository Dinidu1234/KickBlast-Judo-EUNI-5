using KickBlastStudentForms.Data;
using Microsoft.Data.Sqlite;

namespace KickBlastStudentForms.Forms;

public partial class LoginForm : Form
{
    public LoginForm()
    {
        InitializeComponent();
    }

    private void btnLogin_Click(object sender, EventArgs e)
    {
        lblError.Visible = false;

        using var connection = new SqliteConnection(Db.ConnectionString);
        connection.Open();

        using var cmd = connection.CreateCommand();
        cmd.CommandText = "SELECT COUNT(*) FROM Users WHERE Username=@u AND PasswordPlain=@p";
        cmd.Parameters.AddWithValue("@u", txtUsername.Text.Trim());
        cmd.Parameters.AddWithValue("@p", txtPassword.Text);

        var count = Convert.ToInt32(cmd.ExecuteScalar());
        if (count == 1)
        {
            Hide();
            using var main = new MainForm();
            main.ShowDialog();
            Show();
            txtPassword.Clear();
        }
        else
        {
            lblError.Text = "Invalid username or password.";
            lblError.Visible = true;
        }
    }
}
