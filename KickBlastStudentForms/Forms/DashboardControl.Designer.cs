namespace KickBlastStudentForms.Forms;

partial class DashboardControl
{
    private System.ComponentModel.IContainer components = null;
    private FlowLayoutPanel cardsPanel;
    private Panel card1;
    private Panel card2;
    private Panel card3;
    private Panel card4;
    private Label lblTotalAthletesValue;
    private Label lblCalcsValue;
    private Label lblRevenueValue;
    private Label lblNextCompValue;
    private DataGridView dgvRecent;

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
        cardsPanel = new FlowLayoutPanel();
        card1 = CreateCard("Total Athletes", out lblTotalAthletesValue);
        card2 = CreateCard("Calculations This Month", out lblCalcsValue);
        card3 = CreateCard("Total Revenue This Month", out lblRevenueValue);
        card4 = CreateCard("Next Competition Date", out lblNextCompValue);
        dgvRecent = new DataGridView();
        cardsPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvRecent).BeginInit();
        SuspendLayout();

        cardsPanel.Dock = DockStyle.Top;
        cardsPanel.Height = 150;
        cardsPanel.Padding = new Padding(12);
        cardsPanel.Controls.AddRange([card1, card2, card3, card4]);

        dgvRecent.Dock = DockStyle.Fill;
        dgvRecent.BackgroundColor = Color.White;
        dgvRecent.RowTemplate.Height = 32;
        dgvRecent.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvRecent.EnableHeadersVisualStyles = false;
        dgvRecent.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

        Controls.Add(dgvRecent);
        Controls.Add(cardsPanel);
        Dock = DockStyle.Fill;

        cardsPanel.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvRecent).EndInit();
        ResumeLayout(false);
    }

    private Panel CreateCard(string title, out Label valueLabel)
    {
        var panel = new Panel { Width = 230, Height = 110, BorderStyle = BorderStyle.FixedSingle, BackColor = Color.White, Margin = new Padding(8) };
        var lblTitle = new Label { Text = title, Location = new Point(12, 12), AutoSize = true, ForeColor = Color.DimGray };
        valueLabel = new Label { Text = "0", Location = new Point(12, 45), AutoSize = true, Font = new Font("Segoe UI", 12F, FontStyle.Bold) };
        panel.Controls.Add(lblTitle);
        panel.Controls.Add(valueLabel);
        return panel;
    }
}
