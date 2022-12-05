using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    public class Game
    {
        public Game()
        {
            ShapePoints = new ();
            RoundResultsPoints = new();
            Rounds = new();
            
            ShapePoints.Add(Shape.Rock, 1);
            ShapePoints.Add(Shape.Paper, 2);
            ShapePoints.Add(Shape.Scissors, 3);

            RoundResultsPoints.Add(RoundResult.Lost, 0);
            RoundResultsPoints.Add(RoundResult.Draw, 3);
            RoundResultsPoints.Add(RoundResult.Win, 6);

        }

        private Dictionary<Shape, int> ShapePoints { get; } 
        private Dictionary<RoundResult, int> RoundResultsPoints { get; } 
        private List<Round> Rounds { get; }

        public int GetPlayerPoints(string playerId)
        {
            return Rounds.Where(x => x.PlayerId == playerId).Sum(x => x.AcumulatedPoints);
        }

        public void Play(Player playerOne, Player playerTwo)
        {

            while (playerOne.HasMovements() && playerTwo.HasMovements())
            {
                string roundId = Guid.NewGuid().ToString();
                Shape playerOneMovement = playerOne.GetNextMove();
                Shape playerTwoMovement = playerTwo.GetNextMove();

                Rounds.Add(new Round()
                {
                    PlayerId = playerOne.Id,
                    RoundId = roundId,
                    Shape = playerOneMovement,
                    AcumulatedPoints = GetPlayerRoundPoints(playerOneMovement, playerTwoMovement)
                });

                Rounds.Add(new Round()
                {
                    PlayerId = playerTwo.Id,
                    RoundId = roundId,
                    Shape = playerTwoMovement,
                    AcumulatedPoints = GetPlayerRoundPoints(playerTwoMovement, playerOneMovement)
                });
            }

            

        }

        private int GetPlayerRoundPoints(Shape yourMovement, Shape enemyMovement) 
        {
            RoundResult result = GetPlayerRoundResult(yourMovement, enemyMovement);

            int resultPoints = RoundResultsPoints[result];

            return resultPoints + ShapePoints[yourMovement];
        }


        private RoundResult GetPlayerRoundResult(Shape yourMovement, Shape enemyMovement)
        {
            if (yourMovement == enemyMovement)
                return RoundResult.Draw;

            if (GetShapeThatDefeat(enemyMovement) == yourMovement)
                return RoundResult.Win;

            if (GetShapeThatDefeat(yourMovement) == enemyMovement)
                return RoundResult.Lost;

            throw new ArgumentException("Unknown round result.");

        }

        public Shape GetShapeThatDefeat(Shape shape)
        {
            if (shape == Shape.Paper)
                return Shape.Scissors;

            if (shape == Shape.Rock)
                return Shape.Paper;

            if (shape == Shape.Scissors)
                return Shape.Rock;

            throw new ArgumentException("Unknown shape.");
        }






        public void ClearGame()
        {
            Rounds.Clear();
        }

    }

    public enum RoundResult
    {
        Lost = 1,

        Win = 2,

        Draw = 3
    }


}
