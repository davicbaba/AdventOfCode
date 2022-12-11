// See https://aka.ms/new-console-template for more information
using NoSpaceLeftOnDevice.Model;
using System.IO;

string[] input = await File.ReadAllLinesAsync(@"input.txt");

SystemDirectory currentDirectory = new SystemDirectory("root", null);

foreach(var command in GetCommands(input))
{
    if (command is ListFile listFileCommand)
    {
        if (listFileCommand.Size != null)
            currentDirectory.AddFiles(new SimpleFile(listFileCommand.FileName, listFileCommand.Size.Value));

        if (listFileCommand.Size == null)
            currentDirectory.AddFiles(new SystemDirectory(listFileCommand.FileName, currentDirectory));

    }
    if (command is NavigateTo navigateToCommand)
    {
        var target = currentDirectory.NavigateTo(navigateToCommand.FileName);

        if (target != null)
            currentDirectory = target;

    }
}

var root = currentDirectory.NavigateTo("/");

var directories = root.FindDirectoriesWithSizeLowerThan(100000);


Console.WriteLine($"Suma {directories.Sum(x => x.GetTotalSize())}");

var bestChoise = root.FindBestFileToReduceSpace(expectedUnusedSpace: 30000000, totalDiskSpace: 70000000);

Console.WriteLine($"Tamano del archivo a borrar {bestChoise.GetTotalSize()}");


static IEnumerable<Command> GetCommands(string[] input)
{

    foreach (var line in input)
    {
        if (line.StartsWith("$"))
        {
            string commandWithoutDollar = line.Replace("$ ", string.Empty);

            string[] splitted = commandWithoutDollar.Split(' ');

            string command = splitted[0];

            if(command == "cd")
            {
                yield return new NavigateTo(splitted[1]);
                continue;
            }

            if (command == "ls")
                continue;
        }


        string[] fileSplitted = line.Split(' ');

        string first = fileSplitted[0];
        string second = fileSplitted[1];

        if (first == "dir")
        {
            yield return new ListFile(second, null);
            continue;
        }

        yield return new ListFile(second, int.Parse(first));
    }


}


abstract class Command 
{

}


class ListFile : Command
{
    public ListFile(string fileName, int? size)
    {
        FileName = fileName;
        Size = size;
    }

    public string FileName { get; set; }

    public int? Size { get; set; }
}


class NavigateTo : Command
{
    public string FileName { get; set; }

    public NavigateTo(string command)
    {
        FileName = command;
    }
}





