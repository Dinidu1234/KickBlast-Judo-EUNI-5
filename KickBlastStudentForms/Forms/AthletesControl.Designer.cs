namespace KickBlastStudentForms.Forms;

partial class AthletesControl
{
    private System.ComponentModel.IContainer components = null;
    private DataGridView dgvAthletes;
    private TextBox txtSearch;
    private ComboBox cboFilterPlan;
    private Label lblValidation;
    private TextBox txtName;
    private ComboBox cboPlan;
    private TextBox txtCurrentWeight;
    private TextBox txtCategoryWeight;
    private Button btnNew;
    private Button btnSave;
    private Button btnUpdate;
    private Button btnDelete;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        dgvAthletes = new DataGridView();
        txtSearch = new TextBox();
        cboFilterPlan = new ComboBox();
        txtName = new TextBox();
        cboPlan = new ComboBox();
        txtCurrentWeight = new TextBox();
        txtCategoryWeight = new TextBox();
        btnNew = new Button();
        btnSave = new Button();
        btnUpdate = new Button();
        btnDelete = new Button();
        lblValidation = new Label();
        var leftPanel = new Panel();
        var rightPanel = new Panel();
        ((System.ComponentModel.ISupportInitialize)dgvAthletes).BeginInit();
        SuspendLayout();

        leftPanel.Dock = DockStyle.Fill;
        rightPanel.Dock = DockStyle.Right;
        rightPanel.Width = 300;
        rightPanel.Padding = new Padding(12);

        txtSearch.PlaceholderText = "Search athlete";
        txtSearch.Location = new Point(12, 10);
        txtSearch.Width = 220;
        txtSearch.TextChanged += txtSearch_TextChanged;

        cboFilterPlan.Location = new Point(240, 10);
        cboFilterPlan.Width = 160;
        cboFilterPlan.DropDownStyle = ComboBoxStyle.DropDownList;
        cboFilterPlan.SelectedIndexChanged += cboFilterPlan_SelectedIndexChanged;

        dgvAthletes.Location = new Point(12, 45);
        dgvAthletes.Size = new Size(620, 530);
        dgvAthletes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvAthletes.RowTemplate.Height = 32;
        dgvAthletes.CellClick += dgvAthletes_CellClick;

        leftPanel.Controls.Add(txtSearch);
        leftPanel.Controls.Add(cboFilterPlan);
        leftPanel.Controls.Add(dgvAthletes);

        int y = 20;
        rightPanel.Controls.Add(MkLabel("Name", y)); y += 24;
        txtName.SetBounds(10, y, 270, 27); y += 40;
        rightPanel.Controls.Add(txtName);

        rightPanel.Controls.Add(MkLabel("Plan", y)); y += 24;
        cboPlan.SetBounds(10, y, 270, 27); cboPlan.DropDownStyle = ComboBoxStyle.DropDownList; y += 40;
        rightPanel.Controls.Add(cboPlan);

        rightPanel.Controls.Add(MkLabel("Current Weight (Kg)", y)); y += 24;
        txtCurrentWeight.SetBounds(10, y, 270, 27); y += 40;
        rightPanel.Controls.Add(txtCurrentWeight);

        rightPanel.Controls.Add(MkLabel("Category Weight (Kg)", y)); y += 24;
        txtCategoryWeight.SetBounds(10, y, 270, 27); y += 50;
        rightPanel.Controls.Add(txtCategoryWeight);

        btnNew.SetBounds(10, y, 60, 32); btnNew.Text = "New"; btnNew.Click += btnNew_Click;
        btnSave.SetBounds(80, y, 60, 32); btnSave.Text = "Save"; btnSave.Click += btnSave_Click;
        btnUpdate.SetBounds(150, y, 60, 32); btnUpdate.Text = "Update"; btnUpdate.Click += btnUpdate_Click;
        btnDelete.SetBounds(220, y, 60, 32); btnDelete.Text = "Delete"; btnDelete.Click += btnDelete_Click;
        rightPanel.Controls.AddRange([btnNew, btnSave, btnUpdate, btnDelete]);

        lblValidation.ForeColor = Color.Red;
        lblValidation.SetBounds(10, y + 40, 270, 50);
        rightPanel.Controls.Add(lblValidation);

        Controls.Add(leftPanel);
        Controls.Add(rightPanel);
        Dock = DockStyle.Fill;

        ((System.ComponentModel.ISupportInitialize)dgvAthletes).EndInit();
        ResumeLayout(false);
    }

    private Label MkLabel(string text, int y) => new() { Text = text, AutoSize = true, Location = new Point(10, y) };
}
