using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    public class Player
    {

        public Player()
        {
            Strategy = new Queue<Shape>();
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; }

        public Queue<Shape> Strategy { get; }

        public void AddMove(Shape shape)
        {
            Strategy.Enqueue(shape);
        }

        public Shape GetNextMove()
        {
           
            return Strategy.Dequeue();
        }

        public bool HasMovements()
        {
            return Strategy.Count > 0;
        }

        public void ClearMovements()
        {
            Strategy.Clear();
        }
        


    }

    public enum Shape
    {
        Rock = 1,

        Paper = 2,

        Scissors = 3
    }
}

