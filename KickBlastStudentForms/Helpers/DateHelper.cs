namespace KickBlastStudentForms.Helpers;

public static class DateHelper
{
    public static DateTime GetSecondSaturday(int year, int month)
    {
        var date = new DateTime(year, month, 1);
        var saturdayCount = 0;
        while (true)
        {
            if (date.DayOfWeek == DayOfWeek.Saturday)
            {
                saturdayCount++;
                if (saturdayCount == 2)
                {
                    return date;
                }
            }

            date = date.AddDays(1);
        }
    }
}
