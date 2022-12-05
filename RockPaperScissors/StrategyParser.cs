using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    public class StrategyParser
    {
        public Shape GetShape(string shapeCode)
        {
            if (shapeCode == "A" || shapeCode == "X")
                return Shape.Rock;

            if (shapeCode == "B" || shapeCode == "Y")
                return Shape.Paper;

            return Shape.Scissors;
        }

        public Shape GetRequiredShape(string shapeCode, string enemyShapeCode, Game game)
        {
            Shape enemyShape = GetShape(enemyShapeCode);
            Shape shapeToWin = game.GetShapeThatDefeat(enemyShape);


            if (shapeCode == "Z" )
                return shapeToWin;

            if (shapeCode == "Y")
                return enemyShape;

            //We will asume that here is X that means Lose inmediatly
            return game.GetShapeThatDefeat(shapeToWin);
        }

    }
}
