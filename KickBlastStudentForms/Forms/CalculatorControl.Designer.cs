namespace KickBlastStudentForms.Forms;

partial class CalculatorControl
{
    private System.ComponentModel.IContainer components = null;
    private ComboBox cboAthlete;
    private NumericUpDown numCompetitions;
    private NumericUpDown numCoachingHours;
    private Label lblBeginnerNote;
    private Label lblTrainingCost;
    private Label lblCoachingCost;
    private Label lblCompetitionCost;
    private Label lblTotal;
    private Label lblWeightStatus;
    private Label lblSecondSaturday;
    private Button btnCalculate;
    private Button btnSaveCalculation;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        cboAthlete = new ComboBox();
        numCompetitions = new NumericUpDown();
        numCoachingHours = new NumericUpDown();
        lblBeginnerNote = new Label();
        lblTrainingCost = new Label();
        lblCoachingCost = new Label();
        lblCompetitionCost = new Label();
        lblTotal = new Label();
        lblWeightStatus = new Label();
        lblSecondSaturday = new Label();
        btnCalculate = new Button();
        btnSaveCalculation = new Button();
        ((System.ComponentModel.ISupportInitialize)numCompetitions).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numCoachingHours).BeginInit();
        SuspendLayout();

        Controls.AddRange([
            L("Athlete", 30, 30), cboAthlete,
            L("Competitions", 30, 80), numCompetitions,
            L("Coaching Hours/Week", 30, 130), numCoachingHours,
            btnCalculate, lblBeginnerNote,
            L("Training Cost", 30, 250), lblTrainingCost,
            L("Coaching Cost", 30, 290), lblCoachingCost,
            L("Competition Cost", 30, 330), lblCompetitionCost,
            L("Total", 30, 380), lblTotal,
            L("Weight Status", 30, 430), lblWeightStatus,
            L("Second Saturday", 30, 470), lblSecondSaturday,
            btnSaveCalculation
        ]);

        cboAthlete.SetBounds(220, 30, 280, 27);
        numCompetitions.SetBounds(220, 80, 120, 27);
        numCompetitions.Minimum = 0;
        numCoachingHours.SetBounds(220, 130, 120, 27);
        numCoachingHours.DecimalPlaces = 1;
        numCoachingHours.Increment = 0.5m;
        numCoachingHours.Minimum = 0;
        numCoachingHours.Maximum = 5;

        btnCalculate.SetBounds(30, 180, 140, 36);
        btnCalculate.Text = "Calculate";
        btnCalculate.Click += btnCalculate_Click;

        lblBeginnerNote.SetBounds(190, 186, 500, 24);
        lblBeginnerNote.ForeColor = Color.DarkOrange;

        SetValueLabel(lblTrainingCost, 220, 250);
        SetValueLabel(lblCoachingCost, 220, 290);
        SetValueLabel(lblCompetitionCost, 220, 330);
        SetValueLabel(lblTotal, 220, 380, true);
        SetValueLabel(lblWeightStatus, 220, 430);
        SetValueLabel(lblSecondSaturday, 220, 470);

        btnSaveCalculation.SetBounds(30, 525, 180, 38);
        btnSaveCalculation.Text = "Save Calculation";
        btnSaveCalculation.Click += btnSaveCalculation_Click;

        Dock = DockStyle.Fill;
        ((System.ComponentModel.ISupportInitialize)numCompetitions).EndInit();
        ((System.ComponentModel.ISupportInitialize)numCoachingHours).EndInit();
        ResumeLayout(false);
    }

    private Label L(string text, int x, int y) => new() { Text = text, Location = new Point(x, y + 4), AutoSize = true };
    private void SetValueLabel(Label label, int x, int y, bool bold = false)
    {
        label.SetBounds(x, y, 300, 30);
        label.Text = "-";
        if (bold) label.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
    }
}
