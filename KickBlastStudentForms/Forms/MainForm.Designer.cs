namespace KickBlastStudentForms.Forms;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;
    private Panel panelSidebar;
    private Button btnDashboard;
    private Button btnAthletes;
    private Button btnCalculator;
    private Button btnHistory;
    private Button btnSettings;
    private Panel panelHeader;
    private Label lblAppName;
    private Label lblDate;
    private Button btnLogout;
    private Panel panelContent;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        panelSidebar = new Panel();
        btnSettings = new Button();
        btnHistory = new Button();
        btnCalculator = new Button();
        btnAthletes = new Button();
        btnDashboard = new Button();
        panelHeader = new Panel();
        btnLogout = new Button();
        lblDate = new Label();
        lblAppName = new Label();
        panelContent = new Panel();
        panelSidebar.SuspendLayout();
        panelHeader.SuspendLayout();
        SuspendLayout();
        // 
        // panelSidebar
        // 
        panelSidebar.BackColor = Color.FromArgb(17, 24, 39);
        panelSidebar.Controls.Add(btnSettings);
        panelSidebar.Controls.Add(btnHistory);
        panelSidebar.Controls.Add(btnCalculator);
        panelSidebar.Controls.Add(btnAthletes);
        panelSidebar.Controls.Add(btnDashboard);
        panelSidebar.Dock = DockStyle.Left;
        panelSidebar.Width = 200;
        // 
        // menu buttons
        // 
        ConfigureMenuButton(btnDashboard, "Dashboard", 30, btnDashboard_Click);
        ConfigureMenuButton(btnAthletes, "Athletes", 80, btnAthletes_Click);
        ConfigureMenuButton(btnCalculator, "Calculator", 130, btnCalculator_Click);
        ConfigureMenuButton(btnHistory, "History", 180, btnHistory_Click);
        ConfigureMenuButton(btnSettings, "Settings", 230, btnSettings_Click);
        // 
        // panelHeader
        // 
        panelHeader.BackColor = Color.White;
        panelHeader.Controls.Add(btnLogout);
        panelHeader.Controls.Add(lblDate);
        panelHeader.Controls.Add(lblAppName);
        panelHeader.Dock = DockStyle.Top;
        panelHeader.Height = 64;
        // 
        // lblAppName
        // 
        lblAppName.AutoSize = true;
        lblAppName.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblAppName.Location = new Point(15, 18);
        lblAppName.Text = "KickBlast Judo - Training Fee Management";
        // 
        // lblDate
        // 
        lblDate.AutoSize = true;
        lblDate.Location = new Point(520, 23);
        // 
        // btnLogout
        // 
        btnLogout.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnLogout.BackColor = Color.FromArgb(220, 38, 38);
        btnLogout.FlatStyle = FlatStyle.Flat;
        btnLogout.ForeColor = Color.White;
        btnLogout.Location = new Point(900, 15);
        btnLogout.Size = new Size(100, 34);
        btnLogout.Text = "Logout";
        btnLogout.UseVisualStyleBackColor = false;
        btnLogout.Click += btnLogout_Click;
        // 
        // panelContent
        // 
        panelContent.BackColor = Color.FromArgb(249, 250, 251);
        panelContent.Dock = DockStyle.Fill;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1040, 680);
        Controls.Add(panelContent);
        Controls.Add(panelHeader);
        Controls.Add(panelSidebar);
        Font = new Font("Segoe UI", 9F);
        StartPosition = FormStartPosition.CenterScreen;
        Text = "KickBlast Student Forms";
        panelSidebar.ResumeLayout(false);
        panelHeader.ResumeLayout(false);
        panelHeader.PerformLayout();
        ResumeLayout(false);
    }

    private void ConfigureMenuButton(Button button, string text, int y, EventHandler click)
    {
        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderSize = 0;
        button.ForeColor = Color.White;
        button.BackColor = Color.FromArgb(17, 24, 39);
        button.Location = new Point(0, y);
        button.Size = new Size(200, 44);
        button.Text = text;
        button.Cursor = Cursors.Hand;
        button.MouseEnter += (_, _) => { if (button.BackColor == Color.FromArgb(17, 24, 39)) button.BackColor = Color.FromArgb(31, 41, 55); };
        button.MouseLeave += (_, _) => { if (button != btnDashboard && button != btnAthletes && button != btnCalculator && button != btnHistory && button != btnSettings) return; };
        button.Click += click;
    }
}
