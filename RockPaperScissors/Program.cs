
using RockPaperScissors;

Console.WriteLine($"------------------------------------------------------------");
Console.WriteLine($"-----------------------First Part---------------------------");


string[] firstPartStrategy = await File.ReadAllLinesAsync(@"input.txt");

StrategyParser strategyParser = new StrategyParser();
Player you = new Player();
Player yourEnemy = new Player();

foreach (var roundStrategy in firstPartStrategy)
{
    string[] splitted = roundStrategy.Split(' ');

    Shape enemyMovement = strategyParser.GetShape(splitted[0]);
    Shape yourMovement = strategyParser.GetShape(splitted[1]);

    you.AddMove(yourMovement);
    yourEnemy.AddMove(enemyMovement);
}

Game game = new Game();
game.Play(you, yourEnemy);

int yourPoints = game.GetPlayerPoints(you.Id);

Console.WriteLine($"Your total points are {yourPoints}");

Console.WriteLine($"------------------------------------------------------------");
Console.WriteLine($"----------------------Second Part---------------------------");


string[] secondPartStrategy = await File.ReadAllLinesAsync(@"secondPartInput.txt");
you.ClearMovements();
yourEnemy.ClearMovements();
game.ClearGame();

foreach (var roundStrategy in secondPartStrategy)
{
    string[] splitted = roundStrategy.Split(' ');

    string enemyShapeCode = splitted[0];
    Shape enemyMovement = strategyParser.GetShape(enemyShapeCode);
    Shape yourMovement = strategyParser.GetRequiredShape(splitted[1], enemyShapeCode, game);

    you.AddMove(yourMovement);
    yourEnemy.AddMove(enemyMovement);
}

game.Play(you, yourEnemy);

int secondPartPoints = game.GetPlayerPoints(you.Id);

Console.WriteLine($"Your total points are {secondPartPoints}");

