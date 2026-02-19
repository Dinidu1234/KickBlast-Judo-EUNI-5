namespace KickBlastStudentForms.Forms;

partial class SettingsControl
{
    private System.ComponentModel.IContainer components = null;
    private TextBox txtBeginner;
    private TextBox txtIntermediate;
    private TextBox txtElite;
    private TextBox txtCompetition;
    private TextBox txtCoachingRate;
    private Button btnSave;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        txtBeginner = new TextBox();
        txtIntermediate = new TextBox();
        txtElite = new TextBox();
        txtCompetition = new TextBox();
        txtCoachingRate = new TextBox();
        btnSave = new Button();
        SuspendLayout();

        int y = 40;
        Controls.Add(Mk("Beginner Weekly Fee", txtBeginner, ref y));
        Controls.Add(Mk("Intermediate Weekly Fee", txtIntermediate, ref y));
        Controls.Add(Mk("Elite Weekly Fee", txtElite, ref y));
        Controls.Add(Mk("Competition Fee", txtCompetition, ref y));
        Controls.Add(Mk("Coaching Hourly Rate", txtCoachingRate, ref y));

        btnSave.SetBounds(40, y + 20, 130, 36);
        btnSave.Text = "Save Settings";
        btnSave.Click += btnSave_Click;
        Controls.Add(btnSave);

        Dock = DockStyle.Fill;
        ResumeLayout(false);
    }

    private Panel Mk(string label, TextBox textBox, ref int y)
    {
        var p = new Panel { Location = new Point(40, y), Size = new Size(420, 48) };
        var l = new Label { Text = label, AutoSize = true, Location = new Point(0, 5) };
        textBox.SetBounds(220, 0, 180, 27);
        p.Controls.Add(l);
        p.Controls.Add(textBox);
        y += 56;
        return p;
    }
}
