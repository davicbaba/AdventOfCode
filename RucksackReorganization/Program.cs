// See https://aka.ms/new-console-template for more information

Console.WriteLine($"------------------------------------------------------------");
Console.WriteLine($"----------------------First Part---------------------------");



string[] ruckSackContent = await File.ReadAllLinesAsync(@"input.txt");

List<char> itemsInBothCompartmentsPriority = new List<char>();


foreach (string content in ruckSackContent)
{
    int half = (content.Length )/ 2;

    string firstHalf = content.Substring(0, half);
    string lastHalf = content.Replace(firstHalf, string.Empty);

    HashSet<char> currentItemsInBoth = new HashSet<char>();

    foreach (var firstHalfItem in firstHalf.ToCharArray())
    {
        int priority = GetPriority(firstHalfItem);

        if (lastHalf.Any(x => GetPriority(x) == priority) &&
            currentItemsInBoth.Contains(firstHalfItem) == false)
        {
            currentItemsInBoth.Add(firstHalfItem);
            //itemsInBothCompartmentsPriority.Add(priority);
        }
    }

    List<char> repeatedItems = FindRepeatedItemsInAllRuckSacks(firstHalf, lastHalf);

    itemsInBothCompartmentsPriority.AddRange(repeatedItems);
}

int prioritySum = itemsInBothCompartmentsPriority.Select(x => GetPriority(x)).Sum();

Console.WriteLine($"The priority sum is {prioritySum}");


Console.WriteLine($"------------------------------------------------------------");
Console.WriteLine($"----------------------Second Part---------------------------");


ruckSackContent = await File.ReadAllLinesAsync(@"input.txt");

List<string[]> elvesGroups = ruckSackContent.Chunk(3).ToList();

List<char> elvesBadges = new List<char>();

foreach (string[] elvesGroup in elvesGroups)
{
    char badge = FindRepeatedItemsInAllRuckSacks(elvesGroup)[0];

    elvesBadges.Add(badge);

}

int badgePrioritySum = elvesBadges.Select(x => GetPriority(x)).Sum();

Console.WriteLine($"The badge priority sum is {badgePrioritySum}");



static int GetPriority(char character)
{
    if (char.IsUpper(character))
        return character - 'A' + 27;

    return character - 'a' + 1;
}

static List<char> FindRepeatedItemsInAllRuckSacks(params string[] ruckSacks)
{
    int requiredRepeatition = ruckSacks.Count();
    // Create a dictionary to store the count of each character
    var characterCounts = new Dictionary<char, int>();

    foreach (string ruckSack in ruckSacks)
    {
        foreach (char item in ruckSack.Distinct())
        {
            if (characterCounts.ContainsKey(item) == false)
            {
                characterCounts[item] = 1;
                continue;
            }

            characterCounts[item]++;
        }
    }

    return characterCounts.Where(x => x.Value >= requiredRepeatition).Select(x => x.Key).ToList();
}
