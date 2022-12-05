using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    public class Round
    {
        public string RoundId { get; set; }
        public string PlayerId { get; set; }
        public Shape Shape { get; set; }

        public int AcumulatedPoints { get; set; }

    }
}
