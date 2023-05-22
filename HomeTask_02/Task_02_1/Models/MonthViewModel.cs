using System.Globalization;

namespace Task_02_1.Models;

public class MonthViewModel
{
    private readonly int[,] _daysGrid;
    private readonly int _firstDay;

    public int Month { get; set; }
    public int Year { get; set; }
    public int DaysTotal => DateTime.DaysInMonth(Year, Month);
    public int WeeksTotal => _daysGrid?.GetLength(0) ?? 0;

    public string this[int week, int day] => _daysGrid[week, day] == 0 ? "": _daysGrid[week, day].ToString();

    public MonthViewModel(int y, int m)
    {
        Month = m;
        Year = y;
        _firstDay = (int)new DateOnly(Year, Month, 1).DayOfWeek;
        var weeksTotal = (DaysTotal + _firstDay) / 7 + ((DaysTotal + _firstDay) % 7 == 0 ? 0 : 1);
        _daysGrid = new int[weeksTotal, 7];
        for (var day = 0; day < DaysTotal; day++)
        {
            _daysGrid[(day + _firstDay) / 7, (day + _firstDay) % 7] = day + 1;
        }
    }

    public string Title => $"{new DateTime(Year, Month, 1).ToString("MMMM", new CultureInfo("en-US"))} {Year}";

    public (int, int) NextMonth() => Month + 1 == 13 ? (1, Year + 1) : (Month + 1, Year);

    public (int, int) PrevMonth() => Month - 1 == 0 ? (12, Year - 1) : (Month - 1, Year);

    public bool CanNext() => Year != 2100 || Month != 12;

    public bool CanPrev() => Year != 1900 || Month != 1;
}

