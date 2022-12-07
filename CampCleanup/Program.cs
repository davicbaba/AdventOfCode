using System.Collections.Generic;
using System.Linq;

string[] pairs = await File.ReadAllLinesAsync(@"input.txt");

int pairsThatContainsFullyTheOther = 0;
int overlapedPairs = 0;

foreach(var pair in pairs)
{
    string[] ranges = pair.Split(',');

    int[] firstElfRange = ranges[0].Split('-').Select(int.Parse).ToArray();
    int[] secondElfRange = ranges[1].Split('-').Select(int.Parse).ToArray();

    if (pairFullyContainsTheOther(firstElfRange, secondElfRange) ||
        pairFullyContainsTheOther(secondElfRange, firstElfRange))
        pairsThatContainsFullyTheOther++;

    if(pairOverLap(firstElfRange, secondElfRange))
        overlapedPairs++;

}

Console.WriteLine($"pairsThatContainsFullyTheOther {pairsThatContainsFullyTheOther}");
Console.WriteLine($"overlapedPairs {overlapedPairs}");

static bool pairFullyContainsTheOther(int[] elfRange, int[] containedRange)
{
    
    int firstElvInit = elfRange[0];
    int firstElvEnd = elfRange[1];

    int containedInit = containedRange[0];
    int containedEnd = containedRange[1];

    return firstElvInit <= containedInit && firstElvEnd >= containedEnd;

}

static bool pairOverLap(int[] first, int[] second)
{
    List<int> firstElvSections = GetRangeBetweenTwoNumbers(first[0], first[1]);
    List<int> secondElvSections = GetRangeBetweenTwoNumbers(second[0], second[1]);

    return firstElvSections.Intersect(secondElvSections).Count() > 0;

}

static List<int> GetRangeBetweenTwoNumbers(int init, int end)
{
    List<int> range = new();

    for (int i = init; i <= end; i++)
    {
        range.Add(i);
    }

    return range;
}


