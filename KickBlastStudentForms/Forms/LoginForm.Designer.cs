namespace KickBlastStudentForms.Forms;

partial class LoginForm
{
    private System.ComponentModel.IContainer components = null;
    private Label lblTitle;
    private Label lblUsername;
    private Label lblPassword;
    private TextBox txtUsername;
    private TextBox txtPassword;
    private Button btnLogin;
    private Label lblError;
    private Label lblHint;

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
        lblTitle = new Label();
        lblUsername = new Label();
        lblPassword = new Label();
        txtUsername = new TextBox();
        txtPassword = new TextBox();
        btnLogin = new Button();
        lblError = new Label();
        lblHint = new Label();
        SuspendLayout();
        // 
        // lblTitle
        // 
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        lblTitle.Location = new Point(90, 35);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(326, 41);
        lblTitle.Text = "KickBlast Judo Login";
        // 
        // lblUsername
        // 
        lblUsername.AutoSize = true;
        lblUsername.Location = new Point(60, 110);
        lblUsername.Text = "Username";
        // 
        // lblPassword
        // 
        lblPassword.AutoSize = true;
        lblPassword.Location = new Point(60, 175);
        lblPassword.Text = "Password";
        // 
        // txtUsername
        // 
        txtUsername.Location = new Point(60, 133);
        txtUsername.Size = new Size(360, 27);
        // 
        // txtPassword
        // 
        txtPassword.Location = new Point(60, 198);
        txtPassword.Size = new Size(360, 27);
        txtPassword.UseSystemPasswordChar = true;
        // 
        // btnLogin
        // 
        btnLogin.BackColor = Color.FromArgb(17, 24, 39);
        btnLogin.FlatStyle = FlatStyle.Flat;
        btnLogin.ForeColor = Color.White;
        btnLogin.Location = new Point(60, 247);
        btnLogin.Size = new Size(360, 40);
        btnLogin.Text = "Login";
        btnLogin.UseVisualStyleBackColor = false;
        btnLogin.Click += btnLogin_Click;
        // 
        // lblError
        // 
        lblError.AutoSize = true;
        lblError.ForeColor = Color.Red;
        lblError.Location = new Point(60, 300);
        lblError.Text = "error";
        lblError.Visible = false;
        // 
        // lblHint
        // 
        lblHint.AutoSize = true;
        lblHint.ForeColor = Color.DimGray;
        lblHint.Location = new Point(60, 335);
        lblHint.Text = "Default: rashmika / 123456";
        // 
        // LoginForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(500, 410);
        Controls.Add(lblHint);
        Controls.Add(lblError);
        Controls.Add(btnLogin);
        Controls.Add(txtPassword);
        Controls.Add(txtUsername);
        Controls.Add(lblPassword);
        Controls.Add(lblUsername);
        Controls.Add(lblTitle);
        Font = new Font("Segoe UI", 9F);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;
        Text = "KickBlast Student";
        ResumeLayout(false);
        PerformLayout();
    }
}
