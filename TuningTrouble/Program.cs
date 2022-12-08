using System.Linq;

string[] input = await File.ReadAllLinesAsync(@"input.txt");

string singleLine = string.Join("", input);

int startOfPacketMarket = FindMarker(singleLine, expectedUniqueChars:4);
int startOfMessageMarket = FindMarker(singleLine, expectedUniqueChars:14);

Console.WriteLine($"first packet marker after character {startOfPacketMarket}");
Console.WriteLine($"first message marker after character {startOfMessageMarket}");


static int FindMarker(string input, int expectedUniqueChars)
{
    List<char> chars = new List<char>();

    int scapeCount = 0;

    foreach (var item in input.ToList())
    {
        scapeCount++;
        chars.Add(item);

        if (chars.Count() != expectedUniqueChars)
            continue;

        if (IsMarker(chars, expectedUniqueChars))
            break;

        chars.RemoveAt(0);
    }

    return scapeCount;
}

static bool IsMarker(List<char> chars, int expectedUniqueChars)
{
    return chars.Distinct().Count() == expectedUniqueChars;
}
