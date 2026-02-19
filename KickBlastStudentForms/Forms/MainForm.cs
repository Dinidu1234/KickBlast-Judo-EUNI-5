namespace KickBlastStudentForms.Forms;

public partial class MainForm : Form
{
    public DashboardControl Dashboard { get; }
    public AthletesControl Athletes { get; }
    public CalculatorControl Calculator { get; }
    public HistoryControl History { get; }
    public SettingsControl Settings { get; }

    private readonly List<Button> _menuButtons;

    public MainForm()
    {
        InitializeComponent();

        Dashboard = new DashboardControl();
        Athletes = new AthletesControl(this);
        Calculator = new CalculatorControl(this);
        History = new HistoryControl();
        Settings = new SettingsControl();

        _menuButtons = [btnDashboard, btnAthletes, btnCalculator, btnHistory, btnSettings];
        LoadControl(Dashboard, btnDashboard);
    }

    private void LoadControl(UserControl control, Button activeButton)
    {
        panelContent.Controls.Clear();
        control.Dock = DockStyle.Fill;
        panelContent.Controls.Add(control);

        foreach (var button in _menuButtons)
        {
            button.BackColor = Color.FromArgb(17, 24, 39);
        }

        activeButton.BackColor = Color.FromArgb(31, 41, 55);
        lblDate.Text = DateTime.Now.ToString("dddd, dd MMM yyyy");
    }

    private void btnDashboard_Click(object sender, EventArgs e) => LoadControl(Dashboard, btnDashboard);
    private void btnAthletes_Click(object sender, EventArgs e) => LoadControl(Athletes, btnAthletes);
    private void btnCalculator_Click(object sender, EventArgs e) => LoadControl(Calculator, btnCalculator);
    private void btnHistory_Click(object sender, EventArgs e) => LoadControl(History, btnHistory);
    private void btnSettings_Click(object sender, EventArgs e) => LoadControl(Settings, btnSettings);

    private void btnLogout_Click(object sender, EventArgs e)
    {
        Close();
    }
}
