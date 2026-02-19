namespace KickBlastStudentForms.Forms;

partial class HistoryControl
{
    private System.ComponentModel.IContainer components = null;
    private DataGridView dgvHistory;
    private ComboBox cboAthlete;
    private ComboBox cboMonth;
    private ComboBox cboYear;
    private Label lblDetails;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        dgvHistory = new DataGridView();
        cboAthlete = new ComboBox();
        cboMonth = new ComboBox();
        cboYear = new ComboBox();
        lblDetails = new Label();
        var rightPanel = new Panel();
        ((System.ComponentModel.ISupportInitialize)dgvHistory).BeginInit();
        SuspendLayout();

        cboAthlete.SetBounds(20, 15, 180, 27); cboAthlete.DropDownStyle = ComboBoxStyle.DropDownList; cboAthlete.SelectedIndexChanged += FilterChanged;
        cboMonth.SetBounds(210, 15, 110, 27); cboMonth.DropDownStyle = ComboBoxStyle.DropDownList; cboMonth.SelectedIndexChanged += FilterChanged;
        cboYear.SetBounds(330, 15, 110, 27); cboYear.DropDownStyle = ComboBoxStyle.DropDownList; cboYear.SelectedIndexChanged += FilterChanged;

        dgvHistory.SetBounds(20, 55, 690, 530);
        dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvHistory.RowTemplate.Height = 32;
        dgvHistory.CellClick += dgvHistory_CellClick;

        rightPanel.Dock = DockStyle.Right;
        rightPanel.Width = 280;
        rightPanel.Padding = new Padding(12);
        lblDetails.Dock = DockStyle.Fill;
        lblDetails.Text = "Select a record to view details";
        rightPanel.Controls.Add(lblDetails);

        Controls.Add(cboAthlete);
        Controls.Add(cboMonth);
        Controls.Add(cboYear);
        Controls.Add(dgvHistory);
        Controls.Add(rightPanel);
        Dock = DockStyle.Fill;

        ((System.ComponentModel.ISupportInitialize)dgvHistory).EndInit();
        ResumeLayout(false);
    }
}
