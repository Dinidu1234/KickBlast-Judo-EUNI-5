using KickBlastStudentForms.Data;
using KickBlastStudentForms.Forms;

namespace KickBlastStudentForms;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Db.Initialize();
        Application.Run(new LoginForm());
    }
}
