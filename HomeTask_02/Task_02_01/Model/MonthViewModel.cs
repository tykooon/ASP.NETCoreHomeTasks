using System.Globalization;

namespace Task_02_01.Model;

public class MonthViewModel
{
    public int Month { get; set; }
    public int Year { get; set; }
    public List<int[]> Weeks { get; set; } = new List<int[]>();


    public string Title => $"{new DateTime(Year, Month, 1).ToString("MMMM", CultureInfo.CurrentCulture)} {Year}";

    public (int, int) NextMonth() =>
        Month + 1 == 13 ? (1, Year + 1) : (Month + 1, Year);

    public (int, int) PrevMonth() =>
        Month - 1 == 0 ? (12, Year - 1) : (Month - 1, Year);

    public void MakeWeeks()
    {
        Weeks.Clear();
        var daysTotal = DateTime.DaysInMonth(Year, Month);
        var firstDay = new DateOnly(Year, Month, 1).DayOfWeek;
        var temp = new List<int>();
        for (var i = 0; i < (int)firstDay; i++)
        {
            temp.Add(0);
        }
        temp.AddRange(Enumerable.Range(1, daysTotal));
        var tail = temp.Count % 7;
        if (tail >0)
        {
            for (var i = 0; i < 7-tail; i++)
            {
                temp.Add(0);
            }
        }
        for (var i = 0; i < temp.Count / 7; i++)
        {
            Weeks.Add(temp.GetRange(i * 7, 7).ToArray());
        }
    }
}

