namespace AdventOfCode2023.CSharp.Day07;

public static class CardHelper
{
    public static Dictionary<char, string> CardValues = new Dictionary<char, string>()
    {
        { 'A', "14" },
        { 'K', "13" },
        { 'Q', "12" },
        { 'J', "11" },
        { 'T', "10" },
        { '9', "09" },
        { '8', "08" },
        { '7', "07" },
        { '6', "06" },
        { '5', "05" },
        { '4', "04" },
        { '3', "03" },
        { '2', "02" }
    };

    public static Dictionary<char, string> CardValuesWithJokers = new Dictionary<char, string>()
    {
        { 'A', "14" },
        { 'K', "13" },
        { 'Q', "12" },
        { 'J', "11" },
        { 'T', "10" },
        { '9', "09" },
        { '8', "08" },
        { '7', "07" },
        { '6', "06" },
        { '5', "05" },
        { '4', "04" },
        { '3', "03" },
        { '2', "02" },
        { '*', "01" }
    };
}
