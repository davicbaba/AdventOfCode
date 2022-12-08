



using SupplyStacks;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

var columns = GetColumns();

List<ElfAction> actions = await GetActions();

List<Stack<char>> stacks = ToStacks(columns);

MoveSingleCrates(actions, stacks);

MoveManyCrates(actions, ToStacks(columns));


static void MoveSingleCrates(List<ElfAction> actions, List<Stack<char>> stacks)
{
    foreach (var action in actions)
    {
        Stack<char> stackToMove = stacks[action.ColumnToMove - 1];
        Stack<char> targetStack = stacks[action.Target - 1];

        for (int i = 0; i < action.Quantity; i++)
        {
            char crateToMove = stackToMove.Pop();
            targetStack.Push(crateToMove);
        }

    }

    List<char> result = new List<char>();

    foreach (var stack in stacks)
    {
        result.Add(stack.Pop());
    }

    Console.WriteLine($"ExecuteActionInStacks: {new string(result.ToArray())}");
}

static void MoveManyCrates(List<ElfAction> actions, List<Stack<char>> stacks)
{
    foreach (var action in actions)
    {
        Stack<char> stackToMove = stacks[action.ColumnToMove - 1];
        Stack<char> targetStack = stacks[action.Target - 1];

        List<char> listToKeepOrder = new List<char>();

        for (int i = 0; i < action.Quantity; i++)
        {
            char crateToMove = stackToMove.Pop();
            listToKeepOrder.Add(crateToMove);
        }

        for (int i = listToKeepOrder.Count - 1; i >= 0; i--)
        {
            targetStack.Push(listToKeepOrder[i]);
        }

    }

    List<char> result = new List<char>();

    foreach (var stack in stacks)
    {
        result.Add(stack.Pop());
    }

    Console.WriteLine($"ExecuteActionInQueues: {new string(result.ToArray())}");
}

static async Task<List<ElfAction>> GetActions()
{
    string[] actionsAsString = await File.ReadAllLinesAsync(@"input.txt");

    List<ElfAction> actions = new List<ElfAction>();


    foreach (string actionAsString in actionsAsString)
    {
        var regex = new Regex(@"\d+");

        var matches = regex.Matches(actionAsString);

        actions.Add(new ElfAction()
        {
            Quantity = int.Parse(matches[0].Value),
            ColumnToMove = int.Parse(matches[1].Value),
            Target = int.Parse(matches[2].Value),

        });
    }
    
    return actions;
}


static List<char>[] GetColumns()
{
    string requiredStack = @"[N] [ ] [ ] [C] [ ] [Z] [ ] [ ] [ ]         
                             [Q] [G] [ ] [V] [ ] [S] [ ] [ ] [V]
                             [L] [C] [ ] [M] [ ] [T] [ ] [W] [L]
                             [S] [H] [ ] [L] [ ] [C] [D] [H] [S]
                             [C] [V] [F] [D] [ ] [D] [B] [Q] [F]
                             [Z] [T] [Z] [T] [C] [J] [G] [S] [Q]
                             [P] [P] [C] [W] [W] [F] [W] [J] [C]
                             [T] [L] [D] [G] [P] [P] [V] [N] [R]
                              1   2   3   4   5   6   7   8   9 ";

    string[] allLines = requiredStack.Split("\n").Select(x => x.Trim()).ToArray();

    List<char>[] allColumns = new List<char>[9];



    foreach (var line in allLines)
    {
        //every 4 chars we have a column
        string[] columns = line.ToCharArray().Chunk(4).Select(x => new string(x)).ToArray();

        for(int i = 0; i < columns.Length; i++)
        {
            string column = columns[i];

            if (string.IsNullOrEmpty(column.Trim()))
                continue;

            if (column.Any(x => char.IsLetter(x)) == false)
                continue;

            var regex = new Regex(@"\[(\w)\]");

            var match = regex.Match(column);

            var value = match.Value.ToArray()[1];

            var stack = allColumns[i];

            if (stack == null)
                allColumns[i] = new List<char>();

            allColumns[i].Add(value);
        }

    }

    return allColumns;
}

static List<Stack<char>> ToStacks(List<char>[] allColumns)
{
    List<Stack<char>> stacks = new List<Stack<char>>();

    foreach (var column in allColumns)
    {
        Stack<char> stack = new Stack<char>();

        for (int i = column.Count - 1; i >= 0; i--)
        {
            stack.Push(column[i]);
        }

        stacks.Add(stack);
    }

    return stacks; 
}

