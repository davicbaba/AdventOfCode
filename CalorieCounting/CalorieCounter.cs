using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieCounting
{
    public class CalorieCounter
    {
        public int GetTheMostCalories()
        {
            string[] calories = File.ReadAllLines(@"input.txt");

            List<int> elfCalories = new List<int>();

            int currentElfCalories = 0;
            foreach (var calory in calories)
            {
                if (string.IsNullOrEmpty(calory))
                {
                    elfCalories.Add(currentElfCalories);
                    currentElfCalories = 0;
                    continue;
                }

                currentElfCalories += int.Parse(calory);
            }

            return elfCalories.Max();
        }
        public int[] GetTopThreeCalories()
        {
            string[] calories = File.ReadAllLines(@"input.txt");

            List<int> elfCalories = new List<int>();

            int currentElfCalories = 0;
            foreach (var calory in calories)
            {
                if (string.IsNullOrEmpty(calory))
                {
                    elfCalories.Add(currentElfCalories);
                    currentElfCalories = 0;
                    continue;
                }

                currentElfCalories += int.Parse(calory);
            }

            return elfCalories.OrderByDescending(x => x)
                              .Take(3)
                              .ToArray();
        }


    }
}
